<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="true"
    Inherits="RegExLib.Web.UserControls.LeftNavigation" Codebehind="LeftNavigation.ascx.cs" %>

    <div class="commonContainerHeader"><h3>Site Links</h3></div>
    <div class="commonContainer">
        <ul class="sidebarList">
            <asp:Repeater ID="LeftMenu" runat="server" DataSourceID="SiteMapDataSource1" OnItemDataBound="LeftMenu_ItemDataBound">
                <ItemTemplate>
                    <li id="ListItem1" runat="server"><a href='<%# Eval("Url") %>'><%# Eval("Title") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>

<asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="False" />
