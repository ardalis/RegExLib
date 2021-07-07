using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Text;

namespace RssEngine
{
	namespace Design
	{
		public class RssFeedDesign : ControlDesigner
		{
			private RssFeed rssFeedInstance;
			
			public RssFeedDesign() : base()  {}

			public override void Initialize(System.ComponentModel.IComponent component)
			{
				this.rssFeedInstance = (RssFeed) component;
				base.Initialize (component);
			}


			public override string GetDesignTimeHtml() 
			{
				StringWriter sw = new StringWriter();
				HtmlTextWriter writer = new HtmlTextWriter(sw);

				// create a dummy DataSource
				string xmlDump = @"<rss><channel>
					<item><title>Lorem ipsum dolor sit amet, consetetur sadipscing elitr</title><category>diam nonumy</category><pubDate>Sat, 25 Oct 2003 00:21:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>At vero eos et accusam et justo duo dolores et ea rebum</title><category>diam nonumy</category><pubDate>Thu, 23 Oct 2003 18:16:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Duis autem vel eum iriure dolor in hendrerit in vulputate</title><category>magna aliquyam</category><pubDate>Thu, 16 Oct 2003 14:22:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Ut wisi enim ad minim veniam, quis nostrud exerci</title><category>dolor sit</category><pubDate>Wed, 15 Oct 2003 22:58:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Duis autem vel eum iriure dolor in hendrerit in vulputate velit</title><category>magna aliquyam</category><pubDate>Wed, 15 Oct 2003 05:16:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Nam liber tempor cum soluta nobis eleifend option</title><category>diam nonumy</category><pubDate>Tue, 14 Oct 2003 18:44:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Vel illum dolore eu feugiat nulla facilisis</title><category>diam nonumy</category><pubDate>Tue, 14 Oct 2003 17:35:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Consetetur sadipscing elitr,  sed diam nonumy</title><category>dolor sit</category><pubDate>Tue, 14 Oct 2003 04:07:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>At vero eos et accusam et justo duo dolores et ea rebum</title><category>diam nonumy</category><pubDate>Tue, 14 Oct 2003 02:45:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet</title><category>diam nonumy</category><pubDate>Mon, 13 Oct 2003 10:55:00 GMT</pubDate><link>http://www.google.com/</link></item>
					<item><title>Stet clita kasd gubergren, no sea takimata</title><category>magna aliquyam</category><pubDate>Sat, 11 Oct 2003 03:22:00 GMT</pubDate><link>http://www.google.com/</link></item>
					</channel></rss>";

				StringReader sr = new StringReader(xmlDump);
				
				rssFeedInstance.DataSource = sr;
				rssFeedInstance.DataBind();

				rssFeedInstance.RenderControl(writer);
				return sw.ToString();
			}
		}
	}
}