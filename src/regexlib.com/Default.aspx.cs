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

namespace RegExLib.Web {

    public partial class _Default : System.Web.UI.Page {

		protected void Page_Load( Object sender, EventArgs e )
		{
			if ( !Page.IsPostBack )
			{

				Int32 totalExpressions = 0;
				Int32 totalAuthors = 0;
				ExpressionManager.GetCounts( out totalExpressions, out totalAuthors );
				this.reCount.Text = ( totalExpressions == 0 ) ? "" : totalExpressions.ToString();
				this.contributorCountLabel.Text = ( totalAuthors == 0 ) ? "" : totalAuthors.ToString();

			}
		}

        protected void Button1_Click(object sender, EventArgs e) {
            if (this.txtSearch.Text == string.Empty) {
                Response.Redirect("~/Search.aspx");
            } else {
                Response.Redirect("~/Search.aspx?k=" + this.txtSearch.Text);
            }
        }


        protected void AdRotator1_AdCreated(object sender, AdCreatedEventArgs e) {
            string description = e.AdProperties["Description"].ToString();
            string title = e.AdProperties["Title"].ToString();
            string alt = e.AdProperties["AlternateText"].ToString();

            this.lblResourceTitle.InnerText = title;
            this.lblResourceAltText.InnerText = alt;
            this.phFeaturedResource.Controls.Add(new LiteralControl(description));
        }
    }
}