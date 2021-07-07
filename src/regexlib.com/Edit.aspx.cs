using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RegExLib.Data;

namespace RegExLib.Web
{

	public partial class Edit : System.Web.UI.Page
	{
        private int _expressionId;
        public int ExpressionId
        {
            get
            {
                if (_expressionId == 0)
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["regexp_id"]))
                    {
                        int.TryParse(Request.QueryString["regexp_id"], out _expressionId);
                    }
                }
                return _expressionId;
            }
        }

		private String OriginalPage
		{
			get
			{
				return "~/UserPatterns.aspx";
			}
		}

		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad( e );
			// I only want to do this the first time so that a new user doesn't have to perform 
			// an unneccessary click just to get the Form into Insert mode the first time they 
			// arrive at this page...
			if ( !IsPostBack )
			{

				if ( String.IsNullOrEmpty( Request.QueryString[ "regexp_id" ] ) )
				{
					this.DetailsView1.ChangeMode( DetailsViewMode.Insert );
				}
				else
				{
					Int32 id;
					if ( Int32.TryParse( Request.QueryString[ "regexp_id" ], out id ) )
					{
						Expression expr = ExpressionManager.GetExpression( id );
						EnsureUserIsAuthor( expr.AuthorId.ToString() );
					}
					else
					{
						Response.Redirect( this.OriginalPage );
					}
				}
			}
		}

		protected void btnAddNew_Click( object sender, ImageClickEventArgs e )
		{
			this.DetailsView1.ChangeMode( DetailsViewMode.Insert );
		}

		protected void DetailsView1_ItemCommand( object sender, DetailsViewCommandEventArgs e )
		{
			if ( e.CommandName == "Cancel" )
			{
				Response.Redirect( this.OriginalPage );
			}
            else if (e.CommandName == "Edit")
            {
                DetailsView1.UpdateItem(true);
            }
		}

		protected void DetailsView1_ItemUpdated( object sender, DetailsViewUpdatedEventArgs e )
		{
			if ( e.Exception != null )
			{
                //throw e.Exception;
			}
			else
			{
				Response.Redirect( this.OriginalPage );
			}
		}
		protected void DetailsView1_ItemInserted( object sender, DetailsViewInsertedEventArgs e )
		{
            if (e.Exception != null)
            {
                //throw e.Exception;
            }
            else
            {
                Response.Redirect(this.OriginalPage);
            }
		}
		protected void DetailsView1_ItemUpdating( object sender, DetailsViewUpdateEventArgs e )
		{
			Expression expr = ExpressionManager.GetExpression( Convert.ToInt32( Request.QueryString[ "regexp_id" ] ) );
			EnsureUserIsAuthor( expr.AuthorId.ToString() );
			e.NewValues[ "ProviderId" ] = expr.ProviderId;
		}
		protected void DetailsView1_ItemInserting( object sender, DetailsViewInsertEventArgs e )
		{
			e.Values[ "ProviderId" ] = 1;
		}

		private void EnsureUserIsAuthor( String authorId )
		{
			if ( authorId != Membership.GetUser().ProviderUserKey.ToString() )
			{
				Response.Redirect( this.OriginalPage );
			}
		}

        protected override void OnPreRender(EventArgs e)
        {
            if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
            {
                DetailsLink.Visible = true;
                DetailsLink.NavigateUrl = "REDetails.aspx?regexp_id=" + ExpressionId;
                TestLink.Visible = true;
                TestLink.NavigateUrl = "RETester.aspx?regexp_id=" + ExpressionId;
            }
            else
            {
                DetailsLink.Visible = false;
                TestLink.Visible = false;
            }
        }
	}
}