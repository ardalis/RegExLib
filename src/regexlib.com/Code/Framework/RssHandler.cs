using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace RegExLib.Framework {

    public class RssHandler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/xml";
            XmlTextWriter writer = new XmlTextWriter(context.Response.Output);
            writer.Formatting = Formatting.Indented;

            BaseFeedWriter feed = null;

            if (context.Request.FilePath.EndsWith("/PrivateRss.aspx", StringComparison.InvariantCultureIgnoreCase)) {
                feed = new ExpressionFeedWriter();
            }
            else if (context.Request.FilePath.EndsWith("/PrivateRssComments.aspx", StringComparison.InvariantCultureIgnoreCase))
            {
                feed = new CommentFeedWriter();
            }
            else if (context.Request.FilePath.EndsWith("/Rss.aspx", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response.Redirect("http://feeds.feedburner.com/Regexlibcom-RecentPatterns", true);
                return;
            }
            else if (context.Request.FilePath.EndsWith("/RssComments.aspx", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response.Redirect("http://feeds.feedburner.com/Regexlibcom-RecentComments", true);
                return;
            }
            if (feed != null)
            {
                feed.WriteFeed(writer);
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }

    }
}