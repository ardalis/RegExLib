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



namespace RegExLib.Web
{
    public partial class OuterShell : System.Web.UI.MasterPage
    {
        protected string defaultPageTitle = "Regular Expression Library";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // TODO: This isn't working yet -- intent is to set the title on each page and add Regular Expression Library as a suffix
            if (this.Page.Title != defaultPageTitle)
                this.Page.Title += " : " + defaultPageTitle;
        }

        protected void Logout_Click(Object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }

    }

}