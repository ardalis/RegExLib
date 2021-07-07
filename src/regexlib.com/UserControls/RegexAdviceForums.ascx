<%@ Control Language="C#" AutoEventWireup="True" Inherits="UserControls_RegexAdviceForums" Codebehind="RegexAdviceForums.ascx.cs" %>
<%@ OutputCache Duration="3600" VaryByParam="none" %>
	<div class="commonContainerHeader">
        <h3>Get Help with a Regex (<a href="http://RegexAdvice.com/Forums">http://RegexAdvice.com/Forums</a>)</h3>
	</div>
    <div class="commonContainer">
        <rss:RssFeed horizontalalign="center" id="RssFeed1" runat="server" CssClass="listTable" HeaderStyle-CssClass="invisible" BorderWidth="2" OnItemDataBound="RssFeed1_ItemDataBound">
<%--            <HeaderTemplate>
		        <div class="commonContainerHeader">
                    <h2>Latest posts from (<a href="#">RegexAdvice.com/Forums</a>)</h2>
                </div>
            </HeaderTemplate>
--%>
	        <itemtemplate>
	            <asp:Label ID="HiddenLabel" runat="server" Visible="false"></asp:Label>
		        <a href='<%# Container.DataItem.Link %>'>
				        <%# Server.HtmlEncode(Container.DataItem.Title) %>
	            </a>
	        </itemtemplate>
        </rss:RssFeed>
    </div>