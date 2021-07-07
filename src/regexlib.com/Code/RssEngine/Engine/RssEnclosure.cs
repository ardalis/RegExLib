using System;

namespace RssEngine.Engine
{
	/// <summary>
	/// RssEnclosure represents an enclosure element in an RSS item.  Typically RssEnclosure's are used to
	/// provide a link to an MP3 file for use in podcasts.  For more on the &lt;enclosure&gt; element, refer
	/// to <a href="http://blogs.law.harvard.edu/tech/rss#ltenclosuregtSubelementOfLtitemgt">http://blogs.law.harvard.edu/tech/rss#ltenclosuregtSubelementOfLtitemgt</a>.
	/// </summary>
	public class RssEnclosure
	{
		#region Private Member Variables
		// private member variables
		private string url;
		private int length;
		private string type;
		#endregion

		#region Constructors
		private RssEnclosure() {}	// make the default constructor private, so it can't be called

		/// <summary>
		/// Creates a new RssEnclosure instance.
		/// </summary>
		/// <param name="url">The url of the enclosure.</param>
		/// <param name="length">The length (in bytes) of the enclosure.</param>
		/// <param name="type">A type of the enclosure.</param>
		public RssEnclosure(string url, int length, string type)
		{
			this.url = url;
			this.length = length;
			this.type = type;
		}
		#endregion

		#region Property Statements
		/// <summary>
		/// The url of the enclosure.
		/// </summary>
		public string Url
		{
			get
			{
				return url;
			}
		}

		/// <summary>
		/// The length (in bytes) of the enclosure.
		/// </summary>
		public int Length
		{
			get
			{
				return length;
			}
		}

		/// <summary>
		/// A type of the enclosure.
		/// </summary>
		public string Type
		{
			get
			{
				return type;
			}
		}
		#endregion
	}
}
