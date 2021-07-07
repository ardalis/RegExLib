using System;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using RssEngine.Engine;

namespace RssEngine
{
	#region Delegates
	public delegate void RssFeedItemEventHandler(object sender, RssFeedItemEventArgs e);
	public delegate void RssFeedItemCommandEventHandler(object sender, RssFeedItemCommandEventArgs e);
	#endregion
	
	#region RssFeed Class
	/// <summary>
	/// The RssFeed Web control displays a DataGrid-like view of an RSS feed.
	/// Examples of the RssFeed in action can be seen at <a href="http://aspnet.4guysfromrolla.com/blogs/aspnet.aspx">http://aspnet.4guysfromrolla.com/blogs/aspnet.aspx</a>
	/// and <a href="http://scottonwriting.net/demos/RssFeedDemos.aspx">http://scottonwriting.net/demos/RssFeedDemos.aspx</a>.
	/// </summary>
	/// <example>
	/// [Visual Basic]<code>
	/// &lt;%@ Register TagPrefix="skm" Namespace="skmRss" Assembly="skmRss" %&gt;
	/// &lt;skm:RssFeed runat="server" id="myFeed"&gt;&lt;/skm:RssFeed&gt;
	/// 
	/// &lt;script runat="server" language="VB"&gt;
	///		Sub Page_Load(sender as Object, e as EventArgs)
	///			myFeed.DataSource = "http://www.myserver.com/Rss.xml"
	///			myFeed.DataBind()
	///		End Sub
	/// &lt;/script&gt;</code>
	/// [C#]<code>
	/// &lt;%@ Register TagPrefix="skm" Namespace="skmRss" Assembly="skmRss" %&gt;
	/// &lt;skm:RssFeed runat="server" id="myFeed"&gt;&lt;/skm:RssFeed&gt;
	/// 
	/// &lt;script runat="server" language="C#"&gt;
	///		void Page_Load(object sender, EventArgs e) {
	///			myFeed.DataSource = "http://www.myserver.com/Rss.xml";
	///			myFeed.DataBind();
	///		}
	/// &lt;/script&gt;</code>
	/// </example>
	/// <remarks>RssFeed works with RSS feeds.  The RSS 2.0 specification
	/// can be viewed at: <a href="http://blogs.law.harvard.edu/tech/rss">http://blogs.law.harvard.edu/tech/rss</a>.<p />
	/// The RSS 0.92 specification can be viewed at: <a href="http://backend.userland.com/rss092">http://backend.userland.com/rss092</a>.
	/// <p /><b>Limitations:</b> RssFeed does not work if the connection to the Internet is through a proxy.  To add
	/// proxy support, you'll need to use the WebProxy class when making an outgoing connection.  See
	/// <a href="http://scottonwriting.net/sowblog/posts/406.aspx#429">http://scottonwriting.net/sowblog/posts/406.aspx#429</a> for more
	/// information.</remarks>
	[DefaultProperty("Text"),
	ToolboxData("<{0}:RssFeed runat=server></{0}:RssFeed>"),
	Designer("skmRss.Design.RssFeedDesign")]
	public class RssFeed : System.Web.UI.WebControls.WebControl, INamingContainer
	{
		#region Private Member Variables
		// styles
		private TableItemStyle headerStyle = null;
		private TableItemStyle itemStyle = null;
		private TableItemStyle alternatingItemStyle = null;

		private object dataSource = null;
		private ArrayList rssItemsArrayList = null;
		private RssFeedItemCollection rssItemsCollection = null;

		// define the templates
		private ITemplate _itemTemplate;
		private ITemplate _headerTemplate;

		private IWebProxy proxy;			// used for proxy support
		private ICredentials credentials;	// used for credentials support
		#endregion

		#region Events and Event-Related Methods
		/// <summary>
		/// Occurs when an RssFeedItem is created.
		/// </summary>
		public event RssFeedItemEventHandler ItemCreated;

		/// <summary>
		/// Occurs after an RssFeedItem is databound.
		/// </summary>
		public event RssFeedItemEventHandler ItemDataBound;

		/// <summary>
		/// Occurs when a button in an RssFeedItem is clicked.
		/// </summary>
		public event RssFeedItemCommandEventHandler ItemCommand;

		/// <summary>
		/// Raises the <see cref="ItemCreated"/> event.  This allows you to provide a custom handler for the event.
		/// </summary>
		/// <remarks>The <b>ItemCreated</b> event is raised when an item in the <see cref="RssFeed"/> control is created, both during round-trips and at the time data is bound to the control.<p />
		/// The <b>ItemCreated</b> event is commonly used to control the content and appearance of a row in the <see cref="RssFeed"/> control.</remarks>
		/// <param name="e">A <see cref="RssFeedItemEventArgs"/> that contains event data.</param>
		protected virtual void OnItemCreated(RssFeedItemEventArgs e)
		{
			if (ItemCreated != null)
				ItemCreated(this, e);
		}

		/// <summary>
		/// Raises the <see cref="ItemDataBound"/> event.  This allows you to provide a custom handler for the event.
		/// </summary>
		/// <remarks>The <b>ItemDataBound</b> event is raised after an item is data bound to the <see cref="RssFeed"/> control. This event provides you with the last opportunity to access the data item before it is displayed on the client. After this event is raised, the data item is nulled out and no longer available.</remarks>
		/// <param name="e">A <see cref="RssFeedItemEventArgs"/> that contains event data.</param>
		protected virtual void OnItemDataBound(RssFeedItemEventArgs e)
		{
			if (ItemDataBound != null)
				ItemDataBound(this, e);
		}

		/// <summary>
		/// Raises the <see cref="ItemCommand"/> event.  This allows you to provide a custom handler for the event.
		/// </summary>
		/// <remarks>The <b>ItemCommand</b> event is raised when any button is clicked in the <see cref="RssFeed"/> control. 
		/// This event is commonly used to handle buttons controls with a custom <b>CommandName</b> value.</remarks>
		/// <param name="e">A <see cref="RssFeedItemCommandEventArgs"/> that contains event data.</param>
		protected virtual void OnItemCommand(RssFeedItemCommandEventArgs e)
		{
			if (ItemCommand != null)
				ItemCommand(this, e);
		}
		#endregion

		#region Properties
		#region Style Properties
		/// <summary>
		/// A TableItemStyle object that contains the style properties for the header section of the RssFeed table. The default value is an empty TableItemStyle object.
		/// </summary>
		/// <value>A TableItemStyle object that contains the style properties of the heading section in the <see cref="RssFeed"/> control. 
		/// The default value is an empty TableItemStyle object.</value>
		/// <remarks>Use this property to provide a custom style for the heading section of the <see cref="RssFeed"/> control. 
		/// Common style attributes that can be adjusted include forecolor, backcolor, font, and content alignment 
		/// within the cell. Providing a different style enhances the appearance of the <see cref="RssFeed"/> control.</remarks>
		[Browsable(true),
		Category("Appearance"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies the style for the header.")]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (headerStyle == null)
					headerStyle = new TableItemStyle();

				if (IsTrackingViewState)
					((IStateManager) headerStyle).TrackViewState();

				return headerStyle;
			}
		}

		/// <summary>
		/// A TableItemStyle object that contains the style properties for each RssItem in the RssFeed table. The default value is an empty TableItemStyle object.
		/// </summary>
		/// <value>A TableItemStyle object that contains the style properties of the items in the <see cref="RssFeed"/> control. 
		/// The default value is an empty TableItemStyle object.</value>
		/// <remarks>Use this property to provide a custom style for the item cells of the RssFeed control. 
		/// Common style attributes that can be adjusted include forecolor, backcolor, font, and content alignment 
		/// within the cell.</remarks>
		[Browsable(true),
		Category("Appearance"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies the style for RSS items.")]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (itemStyle == null)
					itemStyle = new TableItemStyle();

				if (IsTrackingViewState)
					((IStateManager) itemStyle).TrackViewState();

				return itemStyle;
			}
		}
		
		/// <summary>
		/// A TableItemStyle object that contains the style properties for each alternating RssItem in the RssFeed table. The default value is an empty TableItemStyle object.
		/// </summary>
		/// <value>A TableItemStyle object that contains the style properties of the alternating items in the <see cref="RssFeed"/> control. 
		/// The default value is an empty TableItemStyle object.</value>
		/// <remarks>Use this property to provide a custom style for the alternating item cells of the RssFeed control. 
		/// Common style attributes that can be adjusted include forecolor, backcolor, font, and content alignment 
		/// within the cell.</remarks>
		[Browsable(true),
		Category("Appearance"),
		PersistenceMode(PersistenceMode.InnerProperty),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies the style for alternating RSS items.")]
		public virtual TableItemStyle AlternatingItemStyle
		{
			get
			{
				if (alternatingItemStyle == null)
					alternatingItemStyle = new TableItemStyle();

				if (IsTrackingViewState)
					((IStateManager) alternatingItemStyle).TrackViewState();

				return alternatingItemStyle;
			}
		}
		#endregion

		#region Appearance Properties
		/// <summary>
		/// Gets or sets the URL of an image to display in the background of the <see cref="RssFeed"/> control.
		/// </summary>
		/// <value>
		/// The URL of an image to display in the background of a table control. The default is String.Empty.
		/// </value>
		/// <remarks>Use the <b>BackImageUrl</b> property to specify an image to display in the background of the <see cref="RssFeed"/> control.</remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue("")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!ControlStyleCreated)
					return String.Empty;
				else
					return ((TableStyle) ControlStyle).BackImageUrl;
			}
			set
			{
				((TableStyle) ControlStyle).BackImageUrl = value;
			}
		}

		/// <summary>
		/// The distance (in pixels) between the contents of a cell and the cell's border. The default is -1, which indicates that this property is not set.
		/// </summary>
		/// <remarks>Use the <b>CellPadding</b> property to control the spacing between the contents of a cell and the cell's border. The padding amount specified is added to all four sides of the cell.</remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(0)]
		public virtual int CellPadding
		{
			get
			{
				if (!ControlStyleCreated)
					return -1;
				else
					return ((TableStyle) ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle) ControlStyle).CellPadding = value;
			}
		}

		/// <summary>
		/// The distance (in pixels) between table cells. The default is -1, which indicates that this property is not set.
		/// </summary>
		/// <remarks>Use the <b>CellSpacing</b> property to control the spacing between adjacent cells in a data listing 
		/// control. This spacing is applied both vertically and horizontally. The cell spacing is uniform for the 
		/// entire data listing control. Individual cell spacing between each row or column cannot be specified.</remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(0)]
		public virtual int CellSpacing
		{
			get
			{
				if (!ControlStyleCreated)
					return 0;
				else
					return ((TableStyle) ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle) ControlStyle).CellSpacing = value;
			}
		}

		/// <summary>
		/// One of the GridLines enumeration values. The default is Both.
		/// </summary>
		/// <remarks>
		/// Use the <b>GridLines</b> property to specify whether the border between the cells of a data listing control is 
		/// displayed. This property is set with one of the <b>GridLines</b> enumeration values. The following table 
		/// lists the possible values:
		/// <list type="table">
		///		<listheader><term>Value</term><term>Description</term></listheader>
		///		<item><term>None</term><description>No cell border is displayed.</description></item>
		///		<item><term>Horizontal</term><description>Only the upper and lower borders of the cells in a data listing control are displayed.</description></item>
		///		<item><term>Vertical</term><description>Only the left and right borders of the cells in the data list control are displayed.</description></item>
		///		<item><term>Both</term><description>All borders of the cells in a data listing control are displayed.</description></item>
		/// </list>
		/// </remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(GridLines.Both)]
		public virtual GridLines GridLines
		{
			get
			{
				if (!ControlStyleCreated)
					return GridLines.Both;
				else
					return ((TableStyle) ControlStyle).GridLines;
			}
			set
			{
				((TableStyle) ControlStyle).GridLines = value;
			}
		}

		/// <summary>
		/// One of the HorizontalAlign enumeration values. The default is NotSet.
		/// </summary>
		/// <remarks>Use the <b>HorizontalAlign</b> property to specify the horizontal alignment of a data listing control 
		/// within its container. This property is set with one of the HorizontalAlign enumeration values.</remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!ControlStyleCreated)
					return HorizontalAlign.NotSet;
				else
					return ((TableStyle) ControlStyle).HorizontalAlign;
			}
			set
			{
				((TableStyle) ControlStyle).HorizontalAlign = value;
			}
		}

		/// <summary>
		/// Gets or sets the header text to display at the top of the list of RSS feed items.
		/// </summary>
		/// <value>The (optional) value to display at the top fo the RSS feed items.  The default is String.Empty.</value>
		/// <remarks>The <b>HeaderText</b> will only show up if <see cref="ShowHeader"/> is set to True.<p />
		/// If <b>HeaderText</b> is <i>not</i> specified and <see cref="ShowHeader"/> is true, then the RSS/RDF's 
		/// title is used, if present.</remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue("")]
		public virtual string HeaderText
		{
			get
			{
				object o = ViewState["RssFeedHeaderText"];
				if (o == null)
					return String.Empty;	// default value
				else
					return (string) o;
			}
			set
			{
				ViewState["RssFeedHeaderText"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the URL to link the header to.
		/// </summary>
		/// <remarks>
		/// The header is only rendered as a hyperlink if
		/// <see cref="ShowHeader"/> is True and <see cref="LinkHeader"/> is True.<p />If no value is
		/// provided for <b>HeaderNavigateUrl</b>, the RSS/RDF's link URL is used, if present.
		/// </remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue("")]
		public virtual string HeaderNavigateUrl
		{
			get
			{
				object o = ViewState["RssFeedHeaderNavigateUrl"];
				if (o == null)
					return String.Empty;	// default value
				else
					return (string) o;
			}
			set
			{
				ViewState["RssFeedHeaderNavigateUrl"] = value;
			}
		}

		/// <summary>
		/// Gets or sets whether the header should be shown.
		/// </summary>
		/// <value>A true or false value that indicates if the header is shown.  The default is True.</value>
		/// <remarks>The header appears above the RSS feed items, and is composed of precisely one TableCell (regardless
		/// of how many TableCells make up the feed items).</remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(true)]
		public virtual bool ShowHeader
		{
			get
			{
				object o = ViewState["RssFeedShowHeader"];
				if (o == null)
					return true;		// default value
				else
					return (bool) o;
			}
			set
			{
				ViewState["RssFeedShowHeader"] = value;
			}
		}

		/// <summary>
		/// Gets or sets whether the header should be rendered as a hyperlink.
		/// </summary>
		/// <value>A true or false value that indicates if the header text is rendered as a hyperlink.  
		/// The default is True.</value>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(true)]
		public virtual bool LinkHeader
		{
			get
			{
				object o = ViewState["RssFeedLinkHeader"];
				if (o == null)
					return true;		// default value
				else
					return (bool) o;
			}
			set
			{
				ViewState["RssFeedLinkHeader"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the DateFormatString to be used when viewing the published date for RssItems.  
		/// </summary>
		/// <remarks>The default value is g, which displays the short date format followed by a space 
		/// followed by the short time format.
		/// </remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue("g"),
		Description("Specifies the date format string for the published date.")]
		public virtual string DateFormatString
		{
			get
			{
				object o = ViewState["RssFeedDateFormatString"];
				if (o == null)
					return "g";		// default value
				else
					return (string) o;
			}
			set
			{
				ViewState["RssFeedDateFormatString"] = value;
			}
		}
		
		/// <summary>
		/// Specifies whether the category for the RssItems are shown.
		/// </summary>
		/// <value>A true or false value that indicates if the category is shown.  Default value is False.</value>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(false),
		Description("Indicates if the RSS item's category should be displayed")]
		public virtual bool ShowCategory
		{
			get
			{
				object o = ViewState["RssFeedShowCategory"];
				if (o == null)
					return false;		// default value
				else
					return (bool) o;
			}
			set
			{
				ViewState["RssFeedShowCategory"] = value;
			}
		}

		/// <summary>
		/// Specifies if the RssItem's published date is displayed.  Default value is True.
		/// </summary>
		/// <remarks>The format of the date/time can be configured via the <see cref="DateFormatString"/> property.</remarks>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("Indicates if the RSS item's published date should be displayed.")]
		public virtual bool ShowPubDate
		{
			get
			{
				object o = ViewState["RssFeedShowPubDate"];
				if (o == null)
					return true;		// default value
				else
					return (bool) o;
			}
			set
			{
				ViewState["RssFeedShowPubDate"] = value;
			}
		}

		/// <summary>
		/// Specifies if the RssItem's enclosure is displayed.  Default value is False.
		/// </summary>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(false),
		Description("Indicates if the RSS item's enclosure should be displayed.")]
		public virtual bool ShowEnclosure
		{
			get
			{
				object o = ViewState["RssFeedShowEnclosure"];
				if (o == null)
					return false;		// default value
				else
					return (bool) o;
			}
			set
			{
				ViewState["RssFeedShowEnclosure"] = value;
			}
		}

		/// <summary>
		/// Specifies the RssItem's enclosure link text.  Default is "Download"
		/// </summary>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("Specifies the RssItem's enclosure link text.  Default is 'Download'")]
		public virtual string EnclosureLinkText
		{
			get
			{
				object o = ViewState["RssFeedEnclosureLinkText"];
				if (o == null)
					return "Download";		// default value
				else
					return o.ToString();
			}
			set
			{
				ViewState["RssFeedEnclosureLinkText"] = value;
			}
		}

		/// <summary>
		/// Determines the maximum number of items to show in the list of items.
		/// </summary>
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(10),
		Description("Specifies the maximum number of RSS items to display.")]
		public virtual int MaxItems
		{
			get
			{
				object o = ViewState["RssFeedMaxItems"];
				if (o == null)
					return 10;		// default value
				else
					return (int) o;
			}
			set
			{
				if (value < 1)
					throw new ArgumentException("MaxItems must be a value greater than or equal to 1.");

				ViewState["RssFeedMaxItems"] = value;
			}
		}
		#endregion

		#region Behavior Properties
		/// <summary>
		/// Indicates the number of minutes the RSS syndication feed is cached.
		/// </summary>
		/// <remarks>  A value of 0 indicates that no
		/// caching should be performed.  The default value is 60.<p />
		/// The RSS feed is cached using the in-memory ASP.NET data cache. Therefore, the RSS feed
		/// might exist in the cache for a time period less than specified due to scavenging or Web server
		/// restarts.</remarks>
		/// <exception cref="System.ArgumentException">Raised if a <b>CacheDuration</b> value less than 0 is entered.</exception>
		[Bindable(true),
		Category("Behavior"),
		DefaultValue(60),
		Description("Specifies the number of minutes the RSS feed is cached.")]
		public virtual int CacheDuration
		{
			get
			{
				object o = ViewState["RssFeedCacheDuration"];
				if (o == null)
					return 60;		// default value
				else
					return (int) o;
			}
			set
			{
				if (value < 0)
					throw new ArgumentException("CacheDuration must be a non-negative value.");

				ViewState["RssFeedCacheDuration"] = value;
			}
		}

		/// <summary>
		/// If specified, indicates what <code>target</code> the hyperlink will open in.
		/// </summary>
		/// <remarks>The default behavior of <see cref="RssFeed"/> is to have each RssItem's title rendered as a 
		/// hyperlink, provided that there is a link element in the RSS syndication feed for the item in question.  
		/// <b>Target</b> specifies the target to open this link in.<p />
		/// To have the link open in a new window, set the target to <code>_blank</code></remarks>
		[Bindable(true),
		Category("Behavior"),
		DefaultValue(""),
		Description("Specifies the target the hyperlink will open in.")]
		public virtual string Target
		{
			get
			{
				object o = ViewState["RssFeedTarget"];
				if (o == null)
					return String.Empty;		// default value
				else
					return (string) o;
			}
			set
			{
				ViewState["RssFeedTarget"] = value;
			}
		}

		
		/// <summary>
		/// Specifies the amount of time (in milliseconds) to try to access the requested RSS feed.
		/// </summary>
		/// <remarks>Must be a value greater than 0.  When reading an RSS feed from an external URL, indicates 
		/// the amount of time (in milliseconds) to wait for the response.  If the request takes longer
		/// than the time specified by <b>Timeout</b>, the RSS feed control throws a <see cref="FeedTimeoutException"/> exception.</remarks>
		/// <value>The maximum number of milliseconds to wait for a response from a remote RSS feed.  
		/// Defaults to a value of 5000 (5 seconds).</value>
		/// <exception cref="System.ArgumentException">Raised if a <b>Timeout</b> value less than or equal to 500 is entered.</exception>
		[Bindable(true),
		Category("Behavior"),
		DefaultValue(5000),
		Description("When accessing a remote RSS feed, specifies the Timeout (in milliseconds).")]
		public virtual int Timeout
		{
			get
			{
				object o = ViewState["RssFeedTimeout"];
				if (o == null)
					return 5000;		// default value
				else
					return (int) o;
			}
			set
			{
				if (value <= 0)
					throw new ArgumentException("Timeout must be a value greater than 0.");

				ViewState["RssFeedTimeout"] = value;
			}
		}


		
		/// <summary>
		/// Specifies a WebProxy to use to connect to an external URL.
		/// </summary>
		/// <value>An instance of an object that implements IWebProxy.</value>
		/// <remarks>If you are accessing a remote RSS feed through a website that uses a proxy, you will need
		/// to create a functional WebProxy object and assign it to the <b>Proxy</b> property.  This property needs to be assigned
		/// <i>before</i> the call to <see cref="DataBind"/> is made (assuming you are accessing a remote RSS feed and
		/// need to tunnel through a proxy).<p /><b>Note:</b> The <b>Proxy</b> property is <i>not</i> stored in ViewState,
		/// meaning it is <i>not</i> persisted across postbacks.  Each time, then, that you call <see cref="DataBind"/> be
		/// certain to create a new IWebProxy object and assign it to this property.</remarks>
		[Browsable(false)]
		public virtual IWebProxy Proxy
		{
			get
			{
				return proxy;
			}
			set
			{
				proxy = value;
			}
		}

		/// <summary>
		/// Specifies credentials to use to connect to an external URL that required authentication.
		/// </summary>
		/// <value>An instance of an object that implements ICredentials.</value>
		/// <remarks>If you are accessing a remote RSS feed through a website that uses authentication, you will need
		/// to create a functional ICredentials object and assign it to the <b>Credentials</b> property.  This 
		/// property needs to be assigned <i>before</i> the call to <see cref="DataBind"/> is made (assuming you are accessing a remote RSS feed
		/// that requires authentication).<p /><b>Note:</b> The <b>Credentials</b> property is <i>not</i> stored in ViewState,
		/// meaning it is <i>not</i> persisted across postbacks.  Each time, then, that you call <see cref="DataBind"/> be
		/// certain to create a new ICredentials object and assign it to this property.</remarks>
		[Browsable(false)]
		public virtual ICredentials Credentials
		{
			get
			{
				return credentials;
			}
			set
			{
				credentials = value;
			}
		}
		#endregion

		#region Template Properties
		/// <summary>
		/// Gets or sets the template for the items in the <see cref="RssFeed"/> control.
		/// </summary>
		/// <value>A System.Web.UI.ITemplate that contains the template for the items in the <see cref="RssFeed"/> control. 
		/// The default value is a null reference (Nothing in Visual Basic).</value>
		/// <remarks>Use the <b>ItemTemplate</b> property to control the contents of the items in the RssFeed control. 
		/// The appearance of the items in the RssFeed control is controlled by the <see cref="ItemStyle"/> property.<p />
		/// The contents of the <b>ItemTemplate</b> can be customized via the <see cref="RssItem"/> properties.  The syntax
		/// to use is illustrated in the example.
		/// </remarks>
		/// <example><code>
		/// &lt;%@ Register TagPrefix="skm" Namespace="skmRss" Assembly="skmRss" %&gt;
		/// &lt;skm:RssFeed runat="server" id="myFeed"&gt;
		///   &lt;ItemTemplate&gt;
		///     &lt;# DataBinder.Eval(Container.DataItem, "Title") %&gt;
		///     &lt;br /&gt;
		///     &lt;i&gt;&lt;# DataBinder.Eval(Container.DataItem, "PubDate") %&gt;&lt;/i&gt;
		///   &lt;/ItemTemplate&gt;
		/// &lt;/skm:RssFeed&gt;</code>
		/// </example>
		[Browsable(false),
		TemplateContainer(typeof(RssFeedItem)),
		PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ItemTemplate
		{
			get
			{
				return _itemTemplate;
			}
			set
			{
				_itemTemplate = value;
			}
		}

		/// <summary>
		/// Specifies the optional HeaderTemplate for the control.
		/// </summary>
		/// <remarks>The <b>HeaderTemplate</b> cannot contain any databinding syntax - just HTML and Web control syntax.</remarks>
		[Browsable(false),
		PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate HeaderTemplate
		{
			get
			{
				return _headerTemplate;
			}
			set
			{
				_headerTemplate = value;
			}
		}
		#endregion

		#region Miscellaneous Properties
		/// <summary>
		/// Sets the DataSource (the RSS feed) for the control.
		/// </summary>
		/// <value>The <see cref="RssFeed"/>'s DataSource - used for databinding.  Can be assigned a file path, URL,
		/// XmlReader, TextReader, XPathDocument, or <see cref="RssDocument"/> instance.</value>
		/// <example>[C#]<code>
		/// // The DataSource can be a URL
		/// RssFeed1.DataSource = "http://www.ScottOnWriting.NET/sowBlog/rss.aspx";
		/// 
		/// // The DataSource can be an XPathDocument
		/// XPathDocument doc = new XPathDocument("http://ScottOnWriting.NET/sowBlog/rss.aspx");
		/// RssFeed1.DataSource = doc;
		/// 
		/// // You can load an XML string into the control too!
		/// StringReader sr = new StringReader("...XML string...");
		/// RssFeed1.DataSource = sr;
		/// 
		/// // You can also use an RssDocument
		/// RssEngine engine = new RssEngine();
		/// RssDocument doc = engine.GetDataSource("http://ScottOnWriting.NET/sowBlog/rss.aspx");
		/// RssFeed1.DataSource = doc;
		/// </code></example>
		[Browsable(false)]
		public virtual object DataSource
		{
			get
			{
				return dataSource;
			}
			set
			{
				// make sure we're working with a string, XmlReader, or TextReader
				if (value == null || value is string || value is XmlReader || value is TextReader || value is XPathDocument
					|| value is RssDocument)
					dataSource = value;
				else
					throw new ArgumentException("DataSource must be assigned a string, XmlReader, TextReader, or XPathDocument.");
			}
		}

		/// <summary>
		/// Gets a <see cref="RssFeedItemCollection"/> of <see cref="RssFeedItem"/> objects that represents the 
		/// individual items in the <see cref="RssFeed"/> control.
		/// </summary>
		/// <remarks>Use the <b>Items</b> collection to programmatically control the items in the 
		/// <see cref="RssFeed"/> control. The <b>Items</b> collection does not provide any methods to add or 
		/// remove items to the collection. However, you can control the contents of an item by providing a handler 
		/// for the <see cref="ItemCreated"/> event.</remarks>
		[Browsable(false)]
		public virtual RssFeedItemCollection Items
		{
			get
			{
				// see if we have an instance of the rssItemsCollection
				if (rssItemsCollection == null)
				{
					// if not, see if we have built up the ArrayList
					if (rssItemsArrayList == null)
					{
						// if not, make sure that the child controls have been built
						EnsureChildControls();
					}

					// check again to see if we still don't have an ArrayList
					if (rssItemsArrayList == null)
					{
						// if we still don't have an ArrayList, create a new ArrayList
						rssItemsArrayList = new ArrayList();
					}

					rssItemsCollection = new RssFeedItemCollection(rssItemsArrayList);
				}

				return rssItemsCollection;
			}
		}

		/// <summary>
		/// Gets a ControlCollection object that represents the child controls for the control in the UI hierarchy.
		/// </summary>
		[Browsable(false)]
		public override ControlCollection Controls
		{
			get
			{
				// Make sure the child controls have been created before allowing programmatic
				// access to the control hierarchy
				EnsureChildControls();
				return base.Controls;
			}
		}
		#endregion
		#endregion

		#region Control Hierarchy Creation Methods
		#region DataBind Override
		/// <summary>
		/// Binds the control and all its child controls to the data source specified by the <see cref="DataSource"/> property.
		/// </summary>
		/// <remarks>
		/// To bind the data to the <see cref="RssFeed"/> control, perform the following two steps:
		/// <list type="number">
		///		<item>Set the <see cref="DataSource"/>.</item>
		///		<item>Call the DataBind() method.</item>
		/// </list>
		/// You can also (between steps 1 and 2), indicate that a proxy class should be used via the <see cref="Proxy"/>
		/// property.
		/// The DataBind() method only needs to be called on the page's first load.  The RSS items
		/// are, by default, stored in the ViewState, so they persist across postbacks.</remarks>
		/// <exception cref="FeedTimeoutException">When using remote RSS feeds, if the time to access the feed exceeds the
		/// specified <see cref="Timeout"/>, this exception is thrown.</exception>
		public override void DataBind()
		{
			base.OnDataBinding(EventArgs.Empty);

			// Create the control hierarchy.  First, clear out the child controls
			Controls.Clear();
			ClearChildViewState();
			TrackViewState();

			// Create the control hierarchy
			CreateControlHierarchy(true);

			// Mark the hierarchy as having been created
			ChildControlsCreated = true;
		}
		#endregion

		#region CreateChildControls Override
		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
		/// </summary>
		/// <remarks>When you develop a composite or templated server control, you must override this method.</remarks>
		protected override void CreateChildControls()
		{
			// Clear out the control hiearchy
			Controls.Clear();

			// see if we need to build up the hierarchy
			if (ViewState["RssItemCount"] != null)
				CreateControlHierarchy(false);
		}
		#endregion

		#region CreateControlHierarchy Method
		protected virtual void CreateControlHierarchy(bool useDataSource)
		{
			IEnumerable rssData = null;
			RssDocument rssDoc = null;

			// Clear out and/or create the rssItemsArrayList
			if (rssItemsArrayList == null)
				rssItemsArrayList = new ArrayList();
			else
				rssItemsArrayList.Clear();
			

			// Get the rssData
			bool isValidXml = true;
			if (useDataSource)
			{
				try
				{
					// get the proper dataSource (based on if the DataSource is a URL, file path, XmlReader, etc.)

                    RssEngine.Engine.RssEngine rssEng = new RssEngine.Engine.RssEngine();

					// Get the RssDocument - either from cache or not...
					if (dataSource is string && this.CacheDuration > 0)
					{
						// See if we have a cached version
						object cachedRssDoc = HttpContext.Current.Cache.Get(string.Concat("RssFeed-", dataSource.ToString()));
						if (cachedRssDoc != null)
							rssDoc = (RssDocument) cachedRssDoc;
						else
						{
							// get the DataSource and cache it
							rssDoc = rssEng.GetDataSource(dataSource, Timeout, Proxy, Credentials);
							HttpContext.Current.Cache.Insert(string.Concat("RssFeed-", dataSource.ToString()), rssDoc, null, DateTime.Now.AddMinutes(this.CacheDuration), TimeSpan.Zero);
						}
					}
					else
						// We are NOT using caching, so grab the DataSource
						rssDoc = rssEng.GetDataSource(dataSource, Timeout, Proxy, Credentials);

					rssData = rssDoc.Items;

					// get the header/url of the blog, if needed
					if (ShowHeader && this.HeaderText == String.Empty)
						HeaderText = rssDoc.Title;
					if (this.LinkHeader && this.HeaderNavigateUrl == String.Empty)
						HeaderNavigateUrl = rssDoc.Link;
				}
				catch(XmlException)
				{
					// whoops, there was a problem parsing the data.
					isValidXml = false;
				}
			}
			else
			{
				// Create a dummy DataSource
				rssData = new object[(int) ViewState["RssItemCount"]];
				rssItemsArrayList.Capacity = (int) ViewState["RssItemCount"];
			}

			if (!isValidXml || rssData != null)
			{
				// create a Table
				Table outerTable = new Table();
				Controls.Add(outerTable);

				// Add a header, if needed
				if (this.ShowHeader)
				{
					TableRow headerRow = new TableRow();
					TableCell headerCell = new TableCell();

					// see if we should use the template or the default
					if (_headerTemplate != null)
					{
						_headerTemplate.InstantiateIn(headerCell);
					}
					else
					{
						// add a default header
						if (this.LinkHeader)
						{
							HyperLink lnk = new HyperLink();
							lnk.Text = this.HeaderText;
							lnk.NavigateUrl = this.HeaderNavigateUrl;
							if (this.Target != String.Empty)
								lnk.Target = Target;
							headerCell.Controls.Add(lnk);
						}
						else
							headerCell.Text = this.HeaderText;
					}
					
					// ensure that the ColumnSpan is correct
					headerCell.ColumnSpan = 1;
					if (ShowCategory)
						headerCell.ColumnSpan++;
					if (ShowPubDate)
						headerCell.ColumnSpan++;
					if (ShowEnclosure)
						headerCell.ColumnSpan++;

					// Add the cell and row to the row/table
					headerRow.Cells.Add(headerCell);					
					outerTable.Rows.Add(headerRow);
				}

				// Check to make sure that the XML received was in a valid format...
				if (!isValidXml)
				{
					// add an appropriate message
					TableRow badXMLrow = new TableRow();
					TableCell badXMLcell = new TableCell();
					badXMLcell.ForeColor = Color.Red;
					badXMLcell.Font.Italic = true;
					badXMLcell.Text = "There was an error reading this XML feed...";
					badXMLrow.Cells.Add(badXMLcell);
					outerTable.Rows.Add(badXMLrow);
					return;
				}

				int itemCount = 0;
				foreach(RssItem item in rssData)
				{
					// Determine if this item is an Item or AlternatingItem
					RssFeedItemType itemType = RssFeedItemType.Item;
					if (itemCount % 2 == 1)
						itemType = RssFeedItemType.AlternatingItem;

					// Create the RssFeedItem
					RssFeedItem feedItem = CreateRssFeedItem(outerTable.Rows, itemType, itemCount, item, useDataSource);
					this.rssItemsArrayList.Add(feedItem);

					itemCount++;

					// See if this exceeds the MaxItems value... if so, break out of foreach loop
					if (itemCount >= this.MaxItems)
						break;
				}

				// Instantiate the RssItems collection
				this.rssItemsCollection = new RssFeedItemCollection(rssItemsArrayList);

				// set the RssItemCount ViewState variable if needed
				if (useDataSource)
					ViewState["RssItemCount"] = itemCount;
			}
		}
		#endregion

		#region CreateRssFeedItem Method
		protected virtual RssFeedItem CreateRssFeedItem(TableRowCollection rows, RssFeedItemType itemType, int itemIndex, RssItem item, bool useDataSource)
		{
			RssFeedItem feedItem = new RssFeedItem(itemType, itemIndex);
			RssFeedItemEventArgs e = new RssFeedItemEventArgs(feedItem);

			// see if there is an ItemTemplate
			if (_itemTemplate != null)
			{
				TableCell dummyCell = new TableCell();

				// instantiate in the ItemTemplate
				_itemTemplate.InstantiateIn(dummyCell);

				feedItem.Cells.Add(dummyCell);

				OnItemCreated(e);	// raise the ItemCreated event

				rows.Add(feedItem);

				if (useDataSource)
				{
					feedItem.DataItem = item;
					feedItem.DataBind();

					OnItemDataBound(e);		// raise the ItemDataBound event
				}
			}
			else
			{
				// manually create the item
				TableCell titleCell = new TableCell();
				TableCell categoryCell = new TableCell();
				TableCell pubDateCell = new TableCell();
				TableCell enclosureCell = new TableCell();

				HyperLink lnkItem = new HyperLink();
				lnkItem.Target = this.Target;
				titleCell.Controls.Add(lnkItem);

				feedItem.Cells.Add(titleCell);
				if (ShowCategory)
					feedItem.Cells.Add(categoryCell);
				if (ShowPubDate)
					feedItem.Cells.Add(pubDateCell);
				if (ShowEnclosure)
					feedItem.Cells.Add(enclosureCell);

				OnItemCreated(e);	// raise the ItemCreated event

				rows.Add(feedItem);

				if (useDataSource)
				{
					feedItem.DataItem = item;
				
					if (item.Link == String.Empty)
						titleCell.Text = item.Title;
					else
					{
						lnkItem.NavigateUrl = item.Link;
						lnkItem.Text = item.Title;
						titleCell.Controls.Add(lnkItem);
					}

					if (ShowCategory)
					{
						if (item.Category == String.Empty)
							categoryCell.Text = "&nbsp;&nbsp;&nbsp;";
						else
							categoryCell.Text = item.Category;
					}

					if (ShowPubDate)
						pubDateCell.Text = item.PubDate.ToString(this.DateFormatString);

					if (ShowEnclosure)
					{
						if (item.RssEnclosure == null)
							enclosureCell.Text = "&nbsp;";
						else
							enclosureCell.Text = string.Format("<a href=\"{0}\">{1}</a>", item.RssEnclosure.Url, EnclosureLinkText);
					}

					OnItemDataBound(e);		// raise the ItemDataBound event
				}
			}

			return feedItem;
		}
		#endregion
		#endregion

		#region Event Bubbling Methods
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			// only bother bubbling appropriate events
			if (args is RssFeedItemCommandEventArgs)
			{
				OnItemCommand((RssFeedItemCommandEventArgs) args);
				return true;
			}
			else
				return false;
		}
		#endregion

		#region Rendering and Style Methods
		/// <summary>
		/// The CreateControl() style method is called internally by WebControl to create the control's style.
		/// Since RssFeed is displayed in a table, we want to return a TableStyle instance (as opposed to a generic
		/// Style instance).
		/// </summary>
		/// <returns>A TableStyle instance.</returns>
		protected override Style CreateControlStyle()
		{
			// since we are rendering a table directly, we want to create a
			// TableStyle instance rather than a plain Style
			TableStyle style = new TableStyle(ViewState);

			style.BackImageUrl = this.BackImageUrl;
			style.CellSpacing = this.CellSpacing;
			style.CellPadding = this.CellPadding;
			style.GridLines = this.GridLines;
			style.HorizontalAlign = this.HorizontalAlign;

			return style;
		}

		/// <summary>
		/// PrepareControlHierarchyForRendering() applies styles to the control hierarchy immediately before rendering.
		/// </summary>
		protected virtual void PrepareControlHierarchyForRendering()
		{
			// Make sure we have a control to work with
			if (Controls.Count != 1)
				return;

			// Apply the table style
			Table outerTable = (Table) Controls[0];
			outerTable.CopyBaseAttributes(this);
			if (ControlStyleCreated)
				outerTable.ApplyStyle(ControlStyle);
			else
			{
				outerTable.GridLines = GridLines.Both;
				outerTable.CellSpacing = 0;
			}

			// apply the header formatting
			int startIndex = 0;
			if (this.ShowHeader)
			{
				outerTable.Rows[0].ApplyStyle(this.HeaderStyle);
				if (outerTable.Rows[0].Cells[0].Controls.Count > 0 && outerTable.Rows[0].Cells[0].Controls[0] is HyperLink)
					((HyperLink) outerTable.Rows[0].Cells[0].Controls[0]).ApplyStyle(this.HeaderStyle);
				startIndex = 1;
			}

			// Apply styling for all items in table, if styles are specified...
			if (this.itemStyle == null && this.alternatingItemStyle == null)
				return;

			// First, get alternatingItemStyle setup...			
			TableItemStyle mergedAltItemStyle = null;
			if (this.alternatingItemStyle != null)
			{
				mergedAltItemStyle = new TableItemStyle();
				mergedAltItemStyle.CopyFrom(this.itemStyle);
				mergedAltItemStyle.CopyFrom(this.alternatingItemStyle);
			}
			else
				mergedAltItemStyle = itemStyle;

			bool isAltItem = false;
			for (int i = startIndex; i < outerTable.Rows.Count; i++)
			{
				if (isAltItem)									
					outerTable.Rows[i].MergeStyle(mergedAltItemStyle);
				else
					outerTable.Rows[i].MergeStyle(ItemStyle);

				isAltItem = !isAltItem;
			}
		}

		/// <summary>
		/// Render generates the control's HTML markup.
		/// </summary>
		/// <param name="writer">The HtmlTextWriter instance that is populated with the control's rendered HTML markup.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			// Parepare the control hiearchy for rendering
			PrepareControlHierarchyForRendering();

			// We call RenderContents instead of Render() so that the encasing tag (<span>)
			// is not included; rather, just a <table> is emitted...
			RenderContents(writer);
		}
		#endregion

		#region State Management
		/// <summary>
		/// Saves the ViewState - needed for programmatic changes to the styles over postbacks.
		/// </summary>
		/// <returns>An object representing the control's composite ViewState.</returns>
		protected override object SaveViewState()
		{
			// Save the base ViewState and styles
			object [] state = new object[4];
			state[0] = base.SaveViewState();
			
			if (headerStyle != null)
				state[1] = ((IStateManager) headerStyle).SaveViewState();
			if (itemStyle != null)
				state[2] = ((IStateManager) itemStyle).SaveViewState();
			if (alternatingItemStyle != null)
				state[3] = ((IStateManager) alternatingItemStyle).SaveViewState();

			return state;
		}

		/// <summary>
		/// Loads the ViewState of the control.
		/// </summary>
		/// <param name="savedState">The state saved in the previous postback.</param>
		protected override void LoadViewState(object savedState)
		{
			// retrieve the state from the object array
			object [] state = (object[]) savedState;
			base.LoadViewState(state[0]);

			if (state[1] != null)
				((IStateManager) HeaderStyle).LoadViewState(state[1]);
			if (state[2] != null)
				((IStateManager) ItemStyle).LoadViewState(state[1]);
			if (state[3] != null)
				((IStateManager) AlternatingItemStyle).LoadViewState(state[1]);
		}

		/// <summary>
		/// Informs the styles to begin tracking their ViewState.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState ();

			if (headerStyle != null)
				((IStateManager) headerStyle).TrackViewState();
			if (itemStyle != null)
				((IStateManager) itemStyle).TrackViewState();
			if (alternatingItemStyle != null)
				((IStateManager) alternatingItemStyle).TrackViewState();
		}		
		#endregion
	}
	#endregion
}
