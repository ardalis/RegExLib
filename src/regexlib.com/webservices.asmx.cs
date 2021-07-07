using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient ;
using System.Diagnostics;
using System.Text.RegularExpressions ;
using System.Web;
using System.Web.Services;


using System.Web.Security ;
using System.Configuration ;
using RegexLib ;
//using RegexLib.Common ;

namespace RegExLib.Services
{

	/// <summary>
	/// Summary description for Webservices.
	/// </summary>
	[WebService(Namespace="http://regexlib.com/webservices.asmx")]
	public class Webservices : System.Web.Services.WebService
	{
		public Webservices()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
		}

		[WebMethod(CacheDuration=60, Description="Returns information about a particular regular expression as a RegExpDetails struct object.", EnableSession=false)]
		public ASPAlliance.Common.RegExpDetails getRegExpDetails(int regexpId)
		{
            RegExLib.Data.Expression myExpression = RegExLib.Data.ExpressionManager.GetExpression(regexpId);
            ASPAlliance.Common.RegExpDetails details = myExpression.ToRegExpDetails();

			return details;
		}

        [WebMethod(Description="Returns N expressions in order by date added to library, descending.", EnableSession=false)]
        public RegExLib.Data.Expression[] ListAllAsXml(int maxrows)
        {
            return RegExLib.Data.ExpressionManager.ListExpressionsByMostRecent(maxrows, true).ToArray();
        }


		/// <summary>
		/// Returns a dataset holding information about all of the regular expressions that matched the query provided.
		/// </summary>
		[WebMethod(CacheDuration=60, Description="Returns a dataset holding information about all of the regular expressions that matched the query provided.", EnableSession=false)]
		public DataSet listRegExp(string keyword, string regexp_substring, int min_rating, int howmanyrows)
		{
            DataSet ds = new DataSet();
            ds.Tables.Add(RegExLib.Data.ExpressionManager.ListExpressionsBySearchDS(keyword, regexp_substring, min_rating, "", howmanyrows));
            return ds;
//			return SAS.Utility.Utility.convertDataReaderToDataSet(RegexHelper.listRegExp(keyword,regexp_substring, min_rating, false, howmanyrows));
		}	


		/// <summary>
		/// The GetAuthorizationTicket method authenticates the user against the database
		/// and, if successful, returns an encrypted AuthenticationTicket.
		/// </summary>
		/// <param name="email">The username of the user</param>
		/// <param name="password">The password of the user</param>
		/// <returns>string AuthenticationTicket</returns>
        //[WebMethod(
        //     Description="The GetAuthorizationTicket method authenticates the user against the database and, if successful, returns an encrypted AuthenticationTicket")]
        //private string GetAuthorizationTicket( string email, string password )
        //{
        //    object userID = null ;
        //    RegexLib.Common.Security sec = new RegexLib.Common.Security();	

        //    try
        //    {
        //        sec.login(email, password);
        //        userID = sec.user_id ;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex ;
        //    }

        //    // The user name and password combination is not valid.
        //    if( !sec.isAuthenticated )
        //        return null ;

        //    return GetAuthenticationTicket( sec.user_id ) ;
        //}


		/// <summary>
		/// The GetAuthorizationTicket method authenticates the user against the database
		/// and, if successful, returns an encrypted AuthenticationTicket.
		/// </summary>
		/// <param name="email">The email address of the user</param>
		/// <param name="password">The details of the regular expression to be saved</param>
		/// <remarks>
		/// If the e-mail address doesn't exist, a new user is created and the password is e-mail to them.  
		/// To call this method, you set the properties and get the same structure back but with the Ticket, 
		/// RegularExpression and PatternId properties filled in.
		/// </remarks>
		/// <returns>PatternInfo - Pattern information</returns>
        //[WebMethod(
        //     Description="Saves a pattern in the central repository.  If email which is passed in is not found, a new user is created, otherwise the is added against that user.")]
        //public RegexResult Save( PatternInfo patternInfo )
        //{
        //    UserDetails user = new UserDetails() ;
        //    RegexActionStatus status = RegexActionStatus.None ;
        //    string message = String.Empty ;

        //    // message += "000" ;
			
        //    // attempt to identify the current user
        //    if( !IsTicketValid( patternInfo.UserInfo.Ticket ) )
        //    {
        //        patternInfo.UserInfo.Ticket = null ;
        //        if(  patternInfo.UserInfo.Email.Trim().Length > 0 )
        //        {
        //            // message += "111" ;
        //            int id = Globals.GetUserDetails( patternInfo.UserInfo.Email ) ;

        //            if( id > 0 )
        //            {
        //                user = Globals.GetUserDetails( id ) ; 
        //                if( user.UserId  > 0 )
        //                    patternInfo.UserInfo.Ticket = GetAuthorizationTicket( user.Email, user.Password ) ;
        //            }
        //        }
        //        else
        //        {
        //            message += "You must supply an e-mail address." ;
        //            status = RegexActionStatus.Failed ;
        //            return new RegexResult(patternInfo, message, status ) ;
        //        }
				
        //    }
        //    else
        //    {
        //        // message += "222" ;
        //        int userID = int.Parse (FormsAuthentication.Decrypt(patternInfo.UserInfo.Ticket).Name) ;
        //        user = Globals.GetUserDetails( userID ) ;
        //    }

        //    // message += "333" ;

        //    // user not found or authenticated ... attempt to add
        //    RegexLib.Common.Security sec = new RegexLib.Common.Security();

        //    if( user.UserId == 0 )
        //    {

        //        // message += "444" ;
        //        patternInfo.UserInfo.Ticket = String.Empty ;
        //        patternInfo.PatternId = 0 ;
 
        //        if( patternInfo.UserInfo.FirstName == String.Empty || patternInfo.UserInfo.Surname == String.Empty )
        //        {
        //            message += "Could not create new user because FirstName or Surname not present." ;
        //            status = RegexActionStatus.Failed ;
        //            return new RegexResult(patternInfo, message, status ) ;
        //        }

        //        // message += "555" ;
        //        string pwd = Globals.CreateRandomPassword(patternInfo.UserInfo.FirstName, patternInfo.UserInfo.Surname) ;
        //        try
        //        {
        //            user.UserId = sec.addUser(patternInfo.UserInfo.FirstName, patternInfo.UserInfo.Surname, patternInfo.UserInfo.Email, pwd ) ;
        //            if ( !sec.isAuthenticated )
        //            {
        //                // message += "666" ;
        //                message += "Registration failed.  Have you already registered?" ;
        //            }
        //            else
        //            {
        //                // message += "777" ;
        //                string body = "A new user account has been added for " + patternInfo.UserInfo.FirstName + " " + patternInfo.UserInfo.Surname + "\n\n" ;
        //                body += "Email address: " + patternInfo.UserInfo.Email + "\n" ;
        //                body += "Password: " + pwd + "\n" ;
        //                Globals.NotifyUser( user.UserId, "User account added", body ) ;

        //                patternInfo.UserInfo.Ticket = GetAuthenticationTicket( sec.user_id  ) ;
        //            }
        //        }
        //        catch(HttpException ex)
        //        {
        //            string exception = ex.ToString() ;
        //            Globals.NotifyAdministrator("WebService Error :: Save :: Mail Register User", exception ) ;
        //            if( exception.IndexOf("CDO.Message") > 0 )
        //            {
        //                message += "INFO: User registration was successful but confirmation e-mail failed." ;
        //            }
					
        //        }
        //        catch( Exception ex )
        //        {
        //            string exception = ex.ToString() ;
        //            Globals.NotifyAdministrator("WebService Error :: Save :: Register User", exception ) ;
        //            status = RegexActionStatus.Failed ;
        //            message += "User Registration failed." ;
        //        }
        //    }

        //    // message += "888" ;

        //    if( status == RegexActionStatus.Failed )
        //        return new RegexResult(patternInfo, message, status ) ;

        //    string errors = PatternsAreValid( patternInfo ) ;

        //    // message += "999" ;

        //    if( errors.Length > 0 )
        //    {
        //        status = RegexActionStatus.Failed ;
        //        message = errors ;
        //        return new RegexResult(patternInfo, message, status ) ;
        //    }


        //    // join the pattern strings
        //    string match = String.Empty ;
        //    string notmatch = String.Empty ;
			
        //    for( int i = 0 ; i < patternInfo.Matches.Length ; i++ )
        //    {
        //        if( i == 0 )
        //            match = patternInfo.Matches[i] ;
        //        else
        //            match += "|||" + patternInfo.Matches[i] ;
        //    }

        //    for( int i = 0 ; i < patternInfo.NotMatches.Length ; i++ )
        //    {	
        //        if( i == 0 )
        //            notmatch = patternInfo.NotMatches[i] ;
        //        else
        //            notmatch += "|||" + patternInfo.NotMatches[i] ;
        //    }

        //    // message += "101010" ;


        //    // adding or updating ?
        //    if( patternInfo.PatternId == 0 )
        //    {
        //        try
        //        {
        //            patternInfo.PatternId = RegexHelper.addRegExp( user.UserId, 
        //                Server.HtmlEncode( patternInfo.RegularExpression ), 
        //                Server.HtmlEncode( match ), 
        //                Server.HtmlEncode( notmatch ), 
        //                Server.HtmlEncode( patternInfo.Source ),
        //                Server.HtmlEncode( patternInfo.Description ) ) ;

        //            // message += "111111" ;

        //            Globals.SetProvider( patternInfo.PatternId, patternInfo.ProviderId ) ;
        //            status = RegexActionStatus.Inserted ;
        //        }
        //        catch( SqlException ex )
        //        {
        //            string exception = ex.ToString() ;
        //            // TODO: Log exceptions
        //            //Globals.NotifyAdministrator("WebService Error :: Adding:SqlException:: ASPAlliance.BLL.Regexp.addRegExp", exception ) ;
					
        //            if( ex.ToString().IndexOf( "UNIQUE KEY" ) > 0 )
        //            {
        //                status = RegexActionStatus.Failed ;
        //                message = "Could not insert that pattern as it already exists." ;
        //            }
        //            else
        //            {
        //                status = RegexActionStatus.Failed ;
        //                message = "Failed to insert pattern." ;											
        //            }
					
        //        }
        //        catch( Exception ex )
        //        {
        //            string exception = ex.ToString() ;
        //            //TODO: Log Exceptions
        //            //Globals.NotifyAdministrator("WebService Error :: Adding:SystemException :: ASPAlliance.BLL.Regexp.addRegExp", exception ) ;
        //            status = RegexActionStatus.Failed ;
        //            message = "Failed to insert pattern." ;
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            RegExLib.Data.Expression myExpression = new RegExLib.Data.Expression();
        //            myExpression.AuthorId = 
        //            RegExLib.Data.ExpressionManager.UpdateExpression(myExpression);

        //            //RegexHelper.updateRegExp(patternInfo.PatternId,  user.UserId, 
        //            //    Server.HtmlEncode( patternInfo.RegularExpression ), 
        //            //    Server.HtmlEncode( match ), 
        //            //    Server.HtmlEncode( notmatch ), 
        //            //    Server.HtmlEncode( patternInfo.Source ),
        //            //    Server.HtmlEncode( patternInfo.Description ),
        //            //    0 );

        //            // message += "121212" ;
        //            status = RegexActionStatus.Updated ;
        //        }
        //        catch( Exception ex )
        //        {
        //            string exception = ex.ToString() ;
        //            // TODO: Log Error
        //            //Globals.NotifyAdministrator("WebService Error :: Adding:SystemException :: ASPAlliance.BLL.Regexp.updateRegExp", exception ) ;
        //            status = RegexActionStatus.Failed ;
        //            message = "Error occurred while trying to update that pattern." ;
        //        }
        //    }

        //    return new RegexResult( patternInfo, message, status ) ;
        //}


		private string GetAuthenticationTicket( int userId )
		{
			//create the ticket
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket( userId.ToString(), false, 1 ) ;
			string encryptedTicket = FormsAuthentication.Encrypt( ticket ) ;

			//get the ticket timeout in minutes
			AppSettingsReader configurationAppSettings = new AppSettingsReader() ;
			int timeout = Convert.ToInt32(configurationAppSettings.GetValue("WSAuthenticationTicket.Timeout", typeof(int))) ;

			//cache the ticket
			Context.Cache.Insert( encryptedTicket, userId, null, DateTime.Now.AddMinutes(timeout), TimeSpan.Zero ) ;

			return encryptedTicket ;
		}


		private string PatternsAreValid( PatternInfo _info )
		{
			string retVal = String.Empty ;
			int lenCount = 0 ;
			
			for( int i = 0 ; i < _info.Matches.Length ; i++ )
			{	
				if( _info.Matches[i].Length > 0 ) 
				{
					lenCount += 1 ;
					if( !TestPatternMatches( _info.Matches[i].ToString(), _info.RegularExpression.ToString() ) )
						retVal += _info.Matches[i] + " does not match the regular expression." + Environment.NewLine ;
				}
					
			}

			if( lenCount == 0 )
				retVal += "You must provide at least one match example." + Environment.NewLine ;

			for( int i = 0 ; i < _info.NotMatches.Length ; i++ )
			{	
				if( _info.NotMatches[i].Length > 0 ) 
				{
					lenCount += 1 ;
					if( TestPatternMatches( _info.NotMatches[i].ToString(), _info.RegularExpression.ToString() ) )
						retVal += _info.NotMatches[i] + " does match the regular expression." + Environment.NewLine ;
				}
					
			}

			if( lenCount == 0 )
				retVal += "You must provide at least one non-match example." + Environment.NewLine ;
			

			return retVal ;
		}


		private bool TestPatternMatches( string source, string ptrn )
 		{
			return Regex.Matches( source, ptrn ).Count > 0 ;
		}


		private bool IsTicketValid(string ticket)
		{
			if(ticket == null || System.Web.HttpContext.Current.Cache[ticket] == null)
			{
				return false ;
			}
			else
			{
				// TODO: this method should extract the Name from the ticket and do a dblookup
				return true ;
			}
		}
	}

}
