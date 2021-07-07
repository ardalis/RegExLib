<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="true"
    Inherits="RegExLib.Web.UserControls.NavigationBar" Codebehind="NavigationBar.ascx.cs" %>
    <asp:Repeater runat="server" ID="NavBarRepeater" DataSourceID="SiteMapDataSource1" OnItemDataBound="NavBarRepeater_ItemDataBound">
        <ItemTemplate><li><a href="<%# Eval("Url") %>""><%# Eval("Title") %></a></li></ItemTemplate>
    </asp:Repeater>
    <asp:LoginView ID="MyPatternsView" runat="server">
        <LoggedInTemplate><li><a href="/UserPatterns.aspx">My Expressions</a></li></LoggedInTemplate>
    </asp:LoginView>
<asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="False" />

