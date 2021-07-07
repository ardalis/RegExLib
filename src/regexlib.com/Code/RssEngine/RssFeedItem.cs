using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RssEngine.Engine;

namespace RssEngine
{
	#region RssFeedItemType Enumeration
	/// <summary>
	/// Specifies the type of an <see cref="RssFeedItem"/> in the <see cref="RssFeed"/> control.
	/// </summary>
	public enum RssFeedItemType
	{
		Item,
		AlternatingItem
	}
	#endregion


	#region RssFeedItem Class
	/// <summary>
	/// Represents and item (row) in the <see cref="RssFeed"/> control.
	/// </summary>
	/// <remarks>You can use the <b>RssFeedItem</b> to programmatically access the contents of an item in the
	/// RssFeed control.</remarks>
	[ToolboxItem(false)]
	public class RssFeedItem : TableRow, INamingContainer
	{
		#region Private Member Variables
		private RssItem rssItem;
		private RssFeedItemType itemType;
		private int itemIndex;
		#endregion

		#region Constructors
		private RssFeedItem()  {}		// make the default constructor private, so it can't be called

		/// <summary>
		/// Creates a new RssFeedItem.
		/// </summary>
		/// <param name="itemType">The RssFeedItemType enumeration value representing this RssFeedItem's item type.</param>
		public RssFeedItem(RssFeedItemType itemType, int itemIndex)
		{
			this.itemType = itemType;
			this.itemIndex = itemIndex;
		}
		#endregion

		#region Event Bubbling Methods
		/// <summary>
		/// When the RssFeedItem contains a Button/LinkButton, if that button is clicked, a postback occurs and
		/// the button's Command event is fired (assuming the button's CommandName and/or CommandArgument properties
		/// were set).  This event is bubbled up to the RssFeedItem, which then passes it up to its parent - RssFeed -
		/// via RaiseBubbleEvent.  Before doing so, it packages the EventArgs into an RssFeedItemCommandEventArgs instance.
		/// </summary>
		/// <param name="source">The button that was clicked.</param>
		/// <param name="args">The button's EventArgs.</param>
		/// <returns>A Boolean, indicating if the event was bubbled up to the RssFeed control.</returns>
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			// only bother bubbling appropriate events
			if (args is CommandEventArgs)
			{
				RssFeedItemCommandEventArgs e = new RssFeedItemCommandEventArgs(this, source, (CommandEventArgs) args);
				base.RaiseBubbleEvent(this, e);

				return true;
			}
			else
				return false;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the data item represented by the RssFeedItem object in the RssFeed control.
		/// </summary>
		/// <remarks>Use the <b>DataItem</b> property to specify or determine the properties of a data item represented by 
		/// the RssFeedItem object in the <see cref="RssFeed"/> control.  Typically, you'll reference this property in
		/// the RssFeed's ItemTemplate or in the ItemCommand event handler.</remarks>
		/// <example>
		/// &lt;%@ Register TagPrefix="skm" Namespace="skmRss" Assembly="skmRss" %&gt;
		/// &lt;skm:RssFeed runat="server" id="myFeed"&gt;
		///   &lt;ItemTemplate&gt;
		///     &lt;b&gt;Title: &lt;/b&gt; &lt;%# Container.DataItem.Title %&gt;
		///   &lt;/ItemTemplate&gt;
		/// &lt;/skm:RssFeed&gt;
		/// </example>
		public virtual RssItem DataItem
		{
			get
			{
				return rssItem;
			}
			set
			{
				rssItem = value;
			}
		}

		/// <summary>
		/// Gets the RssFeedItem's ItemType.
		/// </summary>
		/// <value>One of the <see cref="RssFeedItemType"/> values.</value>
		/// <remarks>Use the <b>ItemType</b> property to determine the type of an item in the <see cref="RssFeed"/> 
		/// control. The following table lists the various item types.
		/// <list type="table">
		///		<listheader><term>Item Type</term><term>Description</term></listheader>
		///		<item><term>Item</term><description>An item in the RssFeed control.</description></item>
		///		<item><term>AlternatingItem</term><description>An alternating item in the RssFeed control.</description></item>
		/// </list>
		/// </remarks>
		public virtual RssFeedItemType ItemType
		{
			get
			{
				return itemType;
			}
		}

		/// <summary>
		/// Gets the index of the <see cref="RssFeedItem"/> object from the <see cref="RssFeed.Items"/> collection 
		/// of the <see cref="RssFeed"/> control.
		/// </summary>
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}
		#endregion
	}
	#endregion
}
