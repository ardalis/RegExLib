using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;

/// <summary>
/// Summary description for EmailErrorModule
/// </summary>
public class EmailErrorModule : IHttpModule
{
	public EmailErrorModule()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	#region IHttpModule Members

	public void Dispose()
	{
		
	}

	public void Init( HttpApplication context )
	{
		context.Error += new EventHandler( context_Error );
	}

	void context_Error( object sender, EventArgs e )
	{
		try
		{
			HttpApplication app = sender as HttpApplication;
			Exception ex = app.Server.GetLastError();
			if ( ex is HttpUnhandledException && ex.InnerException != null )
			{
				SendMailErrorEventArgs( app );
			}
		}
		catch
		{
			// gulp
		}
	}

	private void SendMailErrorEventArgs( HttpApplication app )
	{
		String messageBody = GenerateMessageBody( app );
		MailMessage msg = new MailMessage();
		msg.To.Add( ConfigurationManager.AppSettings[ "AdminEmail" ] );
		if ( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DeveloperEmail" ] ) )
		{
			msg.To.Add( ConfigurationManager.AppSettings[ "DeveloperEmail" ] );
		}

		msg.From = new MailAddress( "UnhandledException@RegexLib.com" );
		msg.Subject = "Unhandled Exception From RegexLib.com";
		msg.Body = messageBody;
		msg.IsBodyHtml = true;
		SmtpClient client = new SmtpClient();
		client.Send( msg );
	}

	private string GenerateMessageBody( HttpApplication app )
	{
		Exception ex = app.Server.GetLastError();
		if ( ex is HttpUnhandledException && ex.InnerException != null )
		{
			ex = ex.InnerException;
		}

		StringBuilder msg = new StringBuilder();
		msg.Append( "<html><body>" );
		msg.Append( "<h1>The Following " );
		msg.Append( ex.GetType().ToString() );
		msg.Append( " Was Unhandled On The Website</h1>" );
		msg.Append( "<p>" );
		msg.Append( ex.Message );
		msg.Append( "</p>" );

		msg.Append( "<h2>Details</h2><div><dl>" );
		msg.Append( GenerateDefinition( ex.Source, "Source" ) );
		msg.Append( GenerateDefinition( "<pre>" + ex.StackTrace + "</pre>", "StackTrace" ) );
		if ( ex.InnerException != null )
		{
			msg.Append( GenerateDefinition( ex.InnerException.ToString(), "Inner Exception" ) );
		}
		msg.Append( "</dl></div>" );

		msg.Append( "<h2>Request Details</h2><div><dl>" );
		msg.Append( GenerateDefinition( app.Request.ApplicationPath, "ApplicationPath" ) );
		msg.Append( GenerateDefinition( app.Request.RawUrl, "RawUrl" ) );
		msg.Append( GenerateDefinition( app.Request.Url.ToString(), "Url" ) );
		msg.Append( GenerateDefinition( app.Request.FilePath, "FilePath" ) );
		msg.Append( GenerateDefinition( app.Request.HttpMethod, "HttpMethod" ) );
		msg.Append( GenerateDefinition( app.Request.UrlReferrer.ToString(), "Referrer" ) );
		msg.Append( GenerateDefinition( app.Request.UserAgent, "UserAgent" ) );
		msg.Append( GenerateDefinition( app.Request.IsAuthenticated.ToString(), "IsAuthenticated" ) );
		if ( app.Request.IsAuthenticated )
		{
			MembershipUser user = Membership.GetUser();
			if ( user != null )
			{
				msg.Append( GenerateDefinition( user.UserName, "Username" ) );
				msg.Append( GenerateDefinition( user.Email, "User Email" ) );
				msg.Append( GenerateDefinition( user.ProviderUserKey.ToString(), "User Membership Key" ) );
			}
		}
		msg.Append( "</dl></div>" );

		msg.Append( GenerateBlockFromNameValueCollection( app.Request.QueryString, "QueryString" ) );
		msg.Append( GenerateBlockFromNameValueCollection( app.Request.Form, "Posted Form Vars" ) );
		msg.Append( GenerateBlockFromNameValueCollection( app.Request.ServerVariables, "ServerVariables" ) );
		msg.Append( GenerateBlockFromNameValueCollection( app.Request.Headers, "Headers" ) );

		msg.Append( "</body></html>" );

		return msg.ToString();
	}

	private String GenerateBlockFromNameValueCollection( System.Collections.Specialized.NameValueCollection items, string name )
	{
		StringBuilder block = new StringBuilder();
		block.Append( "<h2>" );
		block.Append( name );
		block.Append( "</h2><div><dl>" );
		foreach ( String key in items.Keys )
		{
			block.Append( "<dt>" );
			block.Append( key );
			block.Append( "</dt><dd>" );
			block.Append( items[ key ] );
			block.Append( "</dd>" );
		}
		block.Append( "</dl></div>" );
		return block.ToString();
	}

	private String GenerateDefinition( String value, String name )
	{
		return String.Format( "<dt>{0}</dt><dd>{1}</dd>", name, value );
	}

	#endregion
}
