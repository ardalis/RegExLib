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

public partial class UserControls_RegexAdviceForums : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                RssFeed1.DataSource = @"http://regexadvice.com/forums/rss.aspx?ForumID=68&Mode=0";
                RssFeed1.DataBind();
            }
            catch
            {
                //gulp
            }
        }
    }
    protected void RssFeed1_ItemDataBound(object sender, RssEngine.RssFeedItemEventArgs e)
    {
        if (e.Item.ItemType == RssEngine.RssFeedItemType.AlternatingItem || e.Item.ItemType == RssEngine.RssFeedItemType.Item)
        {
            Label hiddenLabel = e.Item.Cells[0].FindControl("HiddenLabel") as Label;
            if (hiddenLabel != null)
            {
                ((System.Web.UI.WebControls.WebControl)hiddenLabel.Parent).CssClass = "linkBulleted";
            }
        }
    }
}
