using System;

namespace RssEngine.Engine
{
	/// <summary>
	/// The RssItem class represents an "item" from the RSS feed.  Its properties abstract the properties of
	/// an RSS "item," such as Title, Link, Description, and so forth.<p />
	/// From <a href="http://blogs.law.harvard.edu/tech/rss#ltcategorygtSubelementOfLtitemgt">http://blogs.law.harvard.edu/tech/rss#ltcategorygtSubelementOfLtitemgt</a>:<p /><blockquote>
	/// "A channel may contain any number of &lt;item&gt;s. An item may represent a "story" -- much like a 
	/// story in a newspaper or magazine; if so its description is a synopsis of the story, and 
	/// the link points to the full story. An item may also be complete in itself, if so, the 
	/// description contains the text (entity-encoded HTML is allowed), and the link and title may 
	/// be omitted. All elements of an item are optional, however at least one of title or 
	/// description must be present."</blockquote>
	/// </summary>
	public class RssItem
	{
		#region Private Member Variables
		// private member variables
		private string title;
		private string link;
		private string description;
		private string author;
		private string category;
		private DateTime pubDate;
		private string guid;
		private RssEnclosure rssEnclosure;
		#endregion

		#region Constructors
		private RssItem() {}	// make the default constructor private, so it can't be called

		/// <summary>
		/// Creates a new RssItem instance.
		/// </summary>
		/// <param name="title">The title of the RSS item.</param>
		/// <param name="link">The link (URL) to the RSS item.</param>
		/// <param name="description">A description of the RSS item.</param>
		/// <param name="author">The author of the RSS item.</param>
		/// <param name="category">The category the item belongs to.</param>
		/// <param name="guid">The RSS item GUID.</param>
		/// <param name="pubDate">The published date of the RSS item.</param>
		/// <param name="rssEnclosure">An enclosure included within the RSS item; often used for delivering podcasts.</param>
		public RssItem(string title, string link, string description, string author, string category, string guid, DateTime pubDate, RssEnclosure rssEnclosure)
		{
			this.title = title;
			this.link = link;
			this.description = description;
			this.author = author;
			this.category = category;
			this.pubDate = pubDate;
			this.guid = guid;
			this.rssEnclosure = rssEnclosure;
		}
		#endregion

		#region Property Statements
		/// <summary>
		/// The title of the RSS item.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The link (URL) to the RSS item.
		/// </summary>
		public string Link
		{
			get
			{
				return link;
			}
		}

		/// <summary>
		/// The description of the RSS item.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// The author of the RSS item.
		/// </summary>
		public string Author
		{
			get
			{
				return author;
			}
		}

		/// <summary>
		/// The category of the RSS item.
		/// </summary>
		public string Category
		{
			get
			{
				return category;
			}
		}

		/// <summary>
		/// The GUID for the RSS item.
		/// </summary>
		public string Guid
		{
			get
			{
				return guid;
			}
		}

		/// <summary>
		/// The published date of the RSS item.
		/// </summary>
		public DateTime PubDate
		{
			get
			{
				return pubDate;
			}
		}

		/// <summary>
		/// An enclosure included within the RSS item; often used for delivering podcasts.
		/// </summary>
		public RssEnclosure RssEnclosure
		{
			get
			{
				return rssEnclosure;
			}
		}
		#endregion
	}
}
