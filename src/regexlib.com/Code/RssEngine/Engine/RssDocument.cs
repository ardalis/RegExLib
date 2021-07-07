using System;

namespace RssEngine.Engine
{
	/// <summary>
	/// An enumeration of the possible feed types supported by RssFeed.
	/// </summary>
	public enum FeedTypeEnum
	{
		RDF,
		RSS
		//, ATOM  -- Coming soon!  (Time permitting!)
	}


	/// <summary>
	/// The <b>RssDocument</b> class represents a syndicated feed.  It contains properties that model the feed's attributes:
	/// <b>Title</b>, <b>Link</b>, <b>Description</b>, <b>Items</b>, and so on.  The <see cref="Items"/> property 
	/// is a collection of <see cref="RssItem"/> instances, which represent the items that makeup the feed.
	/// <br /><br />
	/// The <see cref="RssEngine"/> class's <b>GetDataSource</b> method returns an <b>RssDocument</b> instance.
	/// </summary>
	public class RssDocument
	{
		#region Private Member Variables
		// private member variables
		private string title;
		private string link;
		private string description;
		private DateTime pubDate;
		private string version;
		private FeedTypeEnum feedType;
		private RssItemList items;
		#endregion

		#region Constructors
		private RssDocument() {}	// make the default constructor private, so it can't be called

		/// <summary>
		/// Creates an RssDocument, which represents a syndicated feed.
		/// </summary>
		/// <param name="title">The title of the feed.</param>
		/// <param name="link">The link (URL) to the feed.</param>
		/// <param name="description">A description of the feed.</param>
		/// <param name="version">The version of the feed.</param>
		/// <param name="feedType">The type of feed - RDF, RSS, or ATOM.</param>
		/// <param name="pubDate">The published date of the feed.</param>
		public RssDocument(string title, string link, string description, string version, FeedTypeEnum feedType, DateTime pubDate)
		{
			this.title = title;
			this.link = link;
			this.description = description;
			this.version = version;
			this.feedType = feedType;
			this.pubDate = pubDate;
		}
		#endregion

		#region Property Statements
		/// <summary>
		/// The title of the feed.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The link (URL) to the feed.
		/// </summary>
		public string Link
		{
			get
			{
				return link;
			}
		}

		/// <summary>
		/// The description of the feed.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// The version of the feed.
		/// </summary>
		public string Version
		{
			get
			{
				return version;
			}
		}

		/// <summary>
		/// The <see cref="FeedTypeEnum"/> of the feed.
		/// </summary>
		public FeedTypeEnum FeedType
		{
			get
			{
				return feedType;
			}
		}

		/// <summary>
		/// The published date of the feed.
		/// </summary>
		public DateTime PubDate
		{
			get
			{
				return pubDate;
			}
		}

		/// <summary>
		/// The set of items for the feed.
		/// </summary>
		public RssItemList Items
		{
			get
			{
				if (items == null)
					items = new RssItemList();
				return items;
			}
		}
		#endregion	
	}
}
