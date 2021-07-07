using System;
using System.Web.UI.WebControls;

namespace RssEngine
{
	/// <summary>
	/// The RssFeed control can have a page developer-defined template.  This template might include Buttons or LinkButtons
	/// (or other Web controls).  If a button is in the template, and if it raises its Command event, the event is
	/// bubbled up from the RssFeedItem to RssFeed, which then fires its ItemCommand event, much like the
	/// DataGrid/DataList/Repeater controls do.  
	/// 
	/// This EventArgs class is the EventArgs that an event handler responding to ItemCommand will accept.  Just like
	/// the DataGridItemCommandEventArgs class, this class has an Item, CommandSource, CommandName, and CommandArgument
	/// properties...	/// 
	/// </summary>
	public class RssFeedItemCommandEventArgs : CommandEventArgs
	{
		// private member variables
		private RssFeedItem _item;
		private object _commandSource;

		#region Constructors
		/// <summary>
		/// Creates a new RssFeedItemCommandEventArgs instance.
		/// </summary>
		/// <param name="item">The RssFeedItem that contained the button that was clicked.</param>
		/// <param name="commandSource">The reference to the button that was clicked.</param>
		/// <param name="cea">The CommandEventArgs instance passed through the button's Command event.</param>
		public RssFeedItemCommandEventArgs(RssFeedItem item, object commandSource, CommandEventArgs cea) : base(cea)
		{
			_item = item;
			_commandSource = commandSource;	
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns a reference to the RssFeedItem that contains the button that was clicked.
		/// </summary>
		public RssFeedItem Item
		{
			get
			{
				return _item;
			}			
		}

		/// <summary>
		/// Returns a reference to the Button/LinkButton Web control that was clicked.
		/// </summary>
		public object CommandSource
		{
			get
			{
				return _commandSource;
			}
		}
		#endregion
	}
}
