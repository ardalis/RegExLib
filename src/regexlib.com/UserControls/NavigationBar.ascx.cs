using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace RegExLib.Web.UserControls
{

    public partial class NavigationBar : System.Web.UI.UserControl
    {


        protected void NavBarRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            SiteMapNode node = e.Item.DataItem as SiteMapNode;

            if (node != null)
            {
                string displayWhere = node["DisplayOn"];
                if (!string.IsNullOrEmpty(displayWhere))
                {
                    displayWhere = displayWhere.ToLower();
                    if (displayWhere != "top" && displayWhere != "both")
                    {
                        e.Item.Visible = false;
                    }
                }
            }

        }

    }
}