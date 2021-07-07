using System;

namespace RssEngine
{
	/// <summary>
	/// When the RssFeed creates a new RssFeedItem and DataBind()s it, it raises its ItemCreated and
	/// ItemDataBound events, respectively.  Event handlers for these events accept an EventArgs parameter of
	/// type RssFeedItemEventArgs, which includes a reference to the RssFeedItem that was created/databound.
	/// </summary>
	public class RssFeedItemEventArgs : EventArgs
	{
		// private member variables
		private RssFeedItem _item;

		#region Constructor
		private RssFeedItemEventArgs() {}		// private default constructor

		/// <summary>
		/// Creates a new instance of the RssFeedItemEventArgs object.
		/// </summary>
		/// <param name="item">The RssFeedItem that was created/databound.</param>
		public RssFeedItemEventArgs(RssFeedItem item)
		{
			_item = item;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the referenced item in the RssFeed control when the event is raised.
		/// </summary>
		public RssFeedItem Item
		{
			get
			{
				return _item;
			}
		}
		#endregion
	}
}
