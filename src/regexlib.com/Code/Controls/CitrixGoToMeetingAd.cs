using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RegExLib.Web.UI.Controls
{
    /// <summary>
    /// Summary description for CitrixGoToMeetingAd
    /// </summary>
    public class CitrixGoToMeetingAd : System.Web.UI.WebControls.WebControl
    {
        public CitrixGoToMeetingAd()
        {
            this.EnableViewState=false;
        }
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            writer.Write(@"<div style=""width:180px;background-color:white;margin-left:8px;	border: 1px solid #009900;""><iframe src=""http://ads.aspalliance.com/displayad.aspx?a=972&amp;m=50"" height=""120"" width=""150"" marginwidth=""0"" marginheight=""0"" frameborder=""0"" scrolling=""no""><script type=""text/javascript"" src=""http://ads.aspalliance.com/displayad.aspx?a=972&amp;m=50&amp;target=_parent&amp;js=1""></script></iframe></div>");

            base.Render(writer);
        }
    }
}