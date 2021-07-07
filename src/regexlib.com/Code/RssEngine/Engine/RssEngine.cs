using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Collections;

namespace RssEngine.Engine
{
	/// <summary>
	/// RssEngine is the workhorse that grabs the RSS/RDF feed, parses it, and transforms it into an
	/// RssDocument instance.  It is responsible for using an HTTP request to grab the feed as well as
	/// enumerating the items that exist within the feed.
	/// </summary>
	public class RssEngine
	{
		#region GetDataSource Method
		/// <summary>
		/// Returns a new RssDocument instance representing the items specified by the passed-in datasource.
		/// </summary>
		/// <param name="dataSource">Can be assigned a file path, URL,
		/// XmlReader, TextReader, XPathDocument, or <see cref="RssDocument"/> instance.</param>
		/// <returns>An <see cref="RssDocument"/> instance representing the items in the passed-in <b>dataSource</b>.</returns>
		/// <remarks>If a request is made and the wait period exceeds the specified <b>timeout</b> value,
		/// a <see cref="FeedTimeoutException"/> exception is thrown.<br /><br />
		/// For more information on making HTTP requests in .NET refer to
		/// <a href="http://aspnet.4guysfromrolla.com/articles/122204-1.aspx">A Deeper Look at Performing HTTP Requests in and ASP.NET Page</a>.</remarks>
		public virtual RssDocument GetDataSource(object dataSource)
		{
			return GetDataSource(dataSource, 15000, null, null);
		}
		
		/// <summary>
		/// Returns a new RssDocument instance representing the items specified by the passed-in datasource.
		/// </summary>
		/// <param name="dataSource">Can be assigned a file path, URL,
		/// XmlReader, TextReader, XPathDocument, or <see cref="RssDocument"/> instance.</param>
		/// <param name="timeout">The timeout for the HTTP request (in milliseconds).</param>
		/// <returns>An <see cref="RssDocument"/> instance representing the items in the passed-in <b>dataSource</b>.</returns>
		/// <remarks>If a request is made and the wait period exceeds the specified <b>timeout</b> value,
		/// a <see cref="FeedTimeoutException"/> exception is thrown.<br /><br />
		/// For more information on making HTTP requests in .NET refer to
		/// <a href="http://aspnet.4guysfromrolla.com/articles/122204-1.aspx">A Deeper Look at Performing HTTP Requests in and ASP.NET Page</a>.</remarks>
		public virtual RssDocument GetDataSource(object dataSource, int timeout)
		{
			return GetDataSource(dataSource, timeout, null, null);
		}

		/// <summary>
		/// Returns a new RssDocument instance representing the items specified by the passed-in datasource.
		/// </summary>
		/// <param name="dataSource">Can be assigned a file path, URL,
		/// XmlReader, TextReader, XPathDocument, or <see cref="RssDocument"/> instance.</param>
		/// <param name="timeout">The timeout for the HTTP request (in milliseconds).</param>
		/// <param name="proxy">Information used to connect through a proxy.</param>
		/// <returns>An <see cref="RssDocument"/> instance representing the items in the passed-in <b>dataSource</b>.</returns>
		/// <remarks>If a request is made and the wait period exceeds the specified <b>timeout</b> value,
		/// a <see cref="FeedTimeoutException"/> exception is thrown.<br /><br />
		/// For more information on making HTTP requests in .NET refer to
		/// <a href="http://aspnet.4guysfromrolla.com/articles/122204-1.aspx">A Deeper Look at Performing HTTP Requests in and ASP.NET Page</a>.</remarks>
		public virtual RssDocument GetDataSource(object dataSource, int timeout, IWebProxy proxy)
		{
			return GetDataSource(dataSource, timeout, proxy, null);
		}

		/// <summary>
		/// Returns a new RssDocument instance representing the items specified by the passed-in datasource.
		/// </summary>
		/// <param name="dataSource">Can be assigned a file path, URL,
		/// XmlReader, TextReader, XPathDocument, or <see cref="RssDocument"/> instance.</param>
		/// <param name="timeout">The timeout for the HTTP request (in milliseconds).</param>
		/// <param name="proxy">Information used to connect through a proxy.</param>
		/// <param name="credentials">The credentials used for a request that requires authentication.</param>
		/// <returns>An <see cref="RssDocument"/> instance representing the items in the passed-in <b>dataSource</b>.</returns>
		/// <remarks>If a request is made and the wait period exceeds the specified <b>timeout</b> value,
		/// a <see cref="FeedTimeoutException"/> exception is thrown.<br /><br />
		/// For more information on making HTTP requests in .NET refer to
		/// <a href="http://aspnet.4guysfromrolla.com/articles/122204-1.aspx">A Deeper Look at Performing HTTP Requests in and ASP.NET Page</a>.</remarks>
		public virtual RssDocument GetDataSource(object dataSource, int timeout, IWebProxy proxy, ICredentials credentials)
		{
			if (dataSource == null)
				return null;

			if (dataSource is RssDocument)
				return (RssDocument) dataSource;

			// Load the data into an XmlDocument object
			XPathDocument xpDoc = null;
			if (dataSource is XPathDocument)
				xpDoc = (XPathDocument) dataSource;
			else
			{
				if (dataSource is string)
				{
					// get the data remotely
					WebRequest wRequest = WebRequest.Create((string) dataSource);
					
					// Set the WebRequest properties
					wRequest.Timeout = timeout;
					if (proxy != null) wRequest.Proxy = proxy;
					if (credentials != null) wRequest.Credentials = credentials;

					WebResponse wResponse = null;

					try
					{
						wResponse = wRequest.GetResponse();
						xpDoc = new XPathDocument(wResponse.GetResponseStream());
					}
					catch(WebException wException)
					{
						if (wException.Status == WebExceptionStatus.Timeout)
							// There was a timeout...
							throw new FeedTimeoutException("Connection timed out when trying to access " + (string) dataSource, 
								wException, (string) dataSource, timeout);
						else
							// There was some unknown feed exception
							throw new FeedException("There was an exception when attempting to access " + (string) dataSource,
								wException, (string) dataSource);
					}
					catch(Exception eUnknown)
					{
						throw eUnknown;
					}
					finally
					{
						if (wResponse != null)
							wResponse.Close();
					}
				}
				else if (dataSource is TextReader)
					xpDoc = new XPathDocument((TextReader) dataSource);
				else if (dataSource is XmlReader)
					xpDoc = new XPathDocument((XmlReader) dataSource);
			}			

			XPathNavigator xpNav = xpDoc.CreateNavigator();
			
			// May 11, 2004: Move to first child - bug found and fixed by James Crowley
			xpNav.MoveToRoot();
			xpNav.MoveToFirstChild();
			while (xpNav.NodeType != XPathNodeType.Element && xpNav.MoveToNext()) {}				

			RssDocument feedItems = null;

			// See if we are working with RSS or RDF
			if (xpNav.Name == "rdf:RDF")
				// we're working with an RDF document here
				feedItems = GetRDFData(xpNav);
			else if (xpNav.Name == "rss")
				// we're working with an RSS document
				feedItems = GetRSSData(xpNav);
			else
				// unknown format
				throw new ApplicationException("RssFeed only works with RDF or RSS feeds.");

			// return the collection
			return feedItems;
		}

		#region Get RSS Data
		/// <summary>
		/// Parses the retrieved XML feed as an RSS 2.0 document.
		/// </summary>
		protected virtual RssDocument GetRSSData(XPathNavigator xpNav)
		{
			// Get the version of the RSS feed
			string version = "2.0";
			if (xpNav.MoveToAttribute("version", xpNav.NamespaceURI))
				version = xpNav.Value;

			// Move back to the root element
			xpNav.MoveToRoot();
			xpNav.MoveToFirstChild();

			RssDocument feedItems = null;

			XPathNodeIterator items = xpNav.Select("/rss/channel/item");

			// pre-compile XPath evaluation expressions
			XPathExpression titleExpr = xpNav.Compile("string(title/text())");
			XPathExpression linkExpr = xpNav.Compile("string(link/text())");
			XPathExpression descExpr = xpNav.Compile("string(description/text())");
			XPathExpression authorExpr = xpNav.Compile("string(author/text())");
			XPathExpression categoryExpr = xpNav.Compile("string(category/text())");
			XPathExpression pubDateExpr = xpNav.Compile("string(pubDate/text())");
			XPathExpression guidExpr = xpNav.Compile("string(guid/text())");
			XPathExpression enclosureUrlExpr = xpNav.Compile("string(enclosure/@url)");
			XPathExpression enclosureLengthExpr = xpNav.Compile("string(enclosure/@length)");
			XPathExpression enclosureTypeExpr = xpNav.Compile("string(enclosure/@type)");

			if (items == null)
				// XML not in expected format
				throw new FormatException("RSS feed is not in expected format.  See http://blogs.law.harvard.edu/tech/rss for the specification.");
			else
			{
				// get the header/url of the blog, if needed
				string docTitle = (string) xpNav.Evaluate("string(/rss/channel/title/text())");
				string docLink = (string) xpNav.Evaluate("string(/rss/channel/link/text())");
				string docDesc = (string) xpNav.Evaluate("string(/rss/channel/description/text())");
				string docPubDate = (string) items.Current.Evaluate(pubDateExpr);

				DateTime docPubDateDT = DateTime.Now;
				try
				{
					if (docPubDate.Length > 0)
						// Parse the time to GMT and convert it to local time...
						docPubDateDT = DateTimeExt.Parse(docPubDate).ToLocalTime();
				}
				catch
				{
					// pubDateNode's value is null, or not in a correct format...
					docPubDateDT = DateTime.Now;
				}

				feedItems = new RssDocument(docTitle, docLink, docDesc, version, FeedTypeEnum.RSS, docPubDateDT);

				while (items.MoveNext())
				{
					// read in the properties
					string title = (string) items.Current.Evaluate(titleExpr);			// <title>
					string link = (string) items.Current.Evaluate(linkExpr);			// <link>
					string description = (string) items.Current.Evaluate(descExpr);		// <description>
					string author = (string) items.Current.Evaluate(authorExpr);		// <author>
					string category = (string) items.Current.Evaluate(categoryExpr);	// <category>
					string pubDate = (string) items.Current.Evaluate(pubDateExpr);		// <pubDate>
					string guid = (string) items.Current.Evaluate(guidExpr);			// <guid>
					string enclosureUrl = (string) items.Current.Evaluate(enclosureUrlExpr);	// <enclosure>
					string enclosureLength = (string) items.Current.Evaluate(enclosureLengthExpr);	// <enclosure>
					string enclosureType = (string) items.Current.Evaluate(enclosureTypeExpr);	// <enclosure>

					RssEnclosure rssEnclosure = null;
					
					// Add an enclosure, if it exists...
					// See http://blogs.law.harvard.edu/tech/rss#ltenclosuregtSubelementOfLtitemgt for more details
					if (enclosureUrl != null && enclosureUrl.Length > 0 &&
						enclosureLength != null && enclosureLength.Length > 0 &&
						enclosureType != null && enclosureType.Length > 0)
					{
						try
						{
							rssEnclosure = new RssEnclosure(enclosureUrl, Convert.ToInt32(enclosureLength), enclosureType);
						}
						catch
						{
							// Likely an error in parsing enclosure's length
							rssEnclosure = null;
						}
					}							

					DateTime pubDateDT = DateTime.MinValue;
					bool titlePresent = title.Length > 0;
					if (description.Length == 0)
						// description is not present, make sure title is
						if (!titlePresent)
							throw new FormatException("The RSS specification requires that each item have at minimum a title or description.  Item found with neither title nor description.");

					try
					{
						if (pubDate.Length > 0)
							// Parse the time to GMT and convert it to local time...
							pubDateDT = DateTimeExt.Parse(pubDate).ToLocalTime();
					}
					catch
					{
						// pubDateNode's value is null, or not in a correct format...
						pubDateDT = DateTime.Now;
					}

					// Create a new RssItem instance and add it to the collection
					feedItems.Items.Add(new RssItem(title, link, description, author, category, guid, pubDateDT, rssEnclosure));
				}
			}

			return feedItems;
		}
		#endregion

		#region Get RDF Data
		/// <summary>
		/// Parses the retrieved XML feed as an RSS 1.0 (i.e., RDF) document.
		/// </summary>
		protected virtual RssDocument GetRDFData(XPathNavigator xpNav)
		{
			const string RDF_VERSION = "1.0";

			RssDocument feedItems = null;

			XmlNamespaceManager mgr = new XmlNamespaceManager(xpNav.NameTable);
			mgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
			mgr.AddNamespace("defNS", "http://purl.org/rss/1.0/");

			// pre-compile XPath evaluation expressions and assign namespace manager
			XPathExpression itemsExpr = xpNav.Compile("/rdf:RDF/defNS:item");
			itemsExpr.SetContext(mgr);

			XPathExpression topTitleExpr = xpNav.Compile("string(/rdf:RDF/defNS:channel/defNS:title/text())");
			topTitleExpr.SetContext(mgr);
			XPathExpression topLinkExpr = xpNav.Compile("string(/rdf:RDF/defNS:channel/defNS:link/text())");
			topLinkExpr.SetContext(mgr);
			XPathExpression topDescExpr = xpNav.Compile("string(/rdf:RDF/defNS:channel/defNS:description/text())");
			topDescExpr.SetContext(mgr);
			
			XPathExpression titleExpr = xpNav.Compile("string(defNS:title/text())");
			titleExpr.SetContext(mgr);
			XPathExpression linkExpr = xpNav.Compile("string(defNS:link/text())");
			linkExpr.SetContext(mgr);
			XPathExpression descExpr = xpNav.Compile("string(defNS:description/text())");
			descExpr.SetContext(mgr);

			XPathNodeIterator items = xpNav.Select(itemsExpr);
			if (items == null)
				// XML not in expected format
				throw new FormatException("RDF feed is not in expected format.  See http://web.resource.org/rss/1.0/spec for the specification.");
			else
			{
				// get the header/url of the blog, if needed
				string docTitle = (string) xpNav.Evaluate(topTitleExpr);
				string docLink = (string) xpNav.Evaluate(topLinkExpr);
				string docDesc = (string) xpNav.Evaluate(topDescExpr);

				feedItems = new RssDocument(docTitle, docLink, docDesc, RDF_VERSION, FeedTypeEnum.RDF, DateTime.Now);

				while (items.MoveNext())
				{
					// read in the properties
					string title = (string) items.Current.Evaluate(titleExpr);		// <title>
					string link = (string) items.Current.Evaluate(linkExpr);		// <link>
					string description = (string) items.Current.Evaluate(descExpr);	// <description>

					if (title.Length == 0)
						// RDF requires a title
						throw new FormatException("The RDF specification requires that each item have a title.");
					
					if (link.Length == 0)
						// RDF requires a title
						throw new FormatException("The RDF specification requires that each item have a link.");

					// Create a new RssItem instance and add it to the collection
					feedItems.Items.Add(new RssItem(title, link, description, string.Empty, string.Empty, string.Empty, DateTime.Now, null));
				}
			}

			return feedItems;
		}
		#endregion
		#endregion
	}
}
