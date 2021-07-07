using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RegExLib.Web.UserControls {

    public partial class CommunityLeftNavigation : System.Web.UI.UserControl {


        protected void CommunityMenu_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            RepeaterItem item = e.Item;
            SiteMapNode node = e.Item.DataItem as SiteMapNode;

            if (node != null) {
                string displayWhere = node["DisplayOn"];
                if (!string.IsNullOrEmpty(displayWhere)) {
                    displayWhere = displayWhere.ToLower();
                    if (displayWhere != "community")
                    {
                        item.Visible = false;
                    }
                }

                if (!String.IsNullOrEmpty(node["CssClass"]))
                {
                    HtmlControl li = item.FindControl("ListItem1") as HtmlControl;
                    if (li != null)
                    {
                        li.Attributes.Add("class", node["CssClass"]);
                    }
                }
            }
        }
    }
}