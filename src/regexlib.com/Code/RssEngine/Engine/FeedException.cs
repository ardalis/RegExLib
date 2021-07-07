using System;

namespace RssEngine.Engine
{
	/// <summary>
	/// The FeedException is raised whenever RssFeed attempts to consume an external RSS feed and there is
	/// some HTTP-based exception in accessing the feed URL.
	/// </summary>
	public class FeedException : Exception
	{
		// private member variables
		private string url;

		#region Constructors
		public FeedException(string message, Exception innerException) : base(message, innerException) {}
		public FeedException(string message, Exception innerException, string url) : base(message, innerException)
		{
			this.url = url;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns the URL that was being accessed when the exception occurred.
		/// </summary>
		public string Url
		{
			get
			{
				return url;
			}
		}
		#endregion	
	}
}
