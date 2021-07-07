<%@ Control Language="C#" AutoEventWireup="True" Inherits="UserControls_RegexAdviceBlogs" Codebehind="RegexAdviceBlogs.ascx.cs" %>
<%@ OutputCache Duration="3600" VaryByParam="none" %>
	<div class="commonContainerHeader">
        <h3>Latest Regex Blog Posts (<a href="http://RegexAdvice.com/Blogs">http://RegexAdvice.com/Blogs</a>)</h3>
	</div>
    <div class="commonContainer">
        <rss:RssFeed horizontalalign="center" id="RssFeed1" runat="server" CssClass="listTable" HeaderStyle-CssClass="invisible" BorderWidth="2" OnItemDataBound="RssFeed1_ItemDataBound">
	        <itemtemplate>
		        <asp:Label ID="HiddenLabel" runat="server" Visible="false"></asp:Label>
		        <a href='<%# Container.DataItem.Link %>'>
			        <%# Server.HtmlEncode(Container.DataItem.Title) %>
			    </a>
	        </itemtemplate>
        </rss:RssFeed>
    </div>