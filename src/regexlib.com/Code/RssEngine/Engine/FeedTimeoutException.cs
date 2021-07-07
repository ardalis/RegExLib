using System;

namespace RssEngine.Engine
{
	/// <summary>
	/// The FeedTimeoutException is raised whenever RssFeed attempts to consume an external RSS feed, and times out.
	/// </summary>
	public class FeedTimeoutException : FeedException
	{
		// private member variables
		private int triedFor;

		#region Constructors
		public FeedTimeoutException(string message, Exception innerException) : base(message, innerException) {}
		public FeedTimeoutException(string message, Exception innerException, string url, int triedFor) : base(message, innerException, url)
		{
			this.triedFor = triedFor;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns the number of milliseconds the connection was attempted before bailing out.
		/// </summary>
		public int TriedFor
		{
			get
			{
				return triedFor;
			}
		}
		#endregion		
	}
}
