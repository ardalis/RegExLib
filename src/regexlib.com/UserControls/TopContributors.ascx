<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="true" Inherits="RegExLib.Web.UserControls.TopContributors" Codebehind="TopContributors.ascx.cs" %>
    <div class="commonContainerHeader"><h3>Top Contributors</h3></div>
    <div class="commonContainer">
        <ul class="sidebarList">
            <asp:Repeater ID="TopContributorList" runat="server" DataSourceID="ObjectDataSource1">
                <ItemTemplate>
                    <li>
                        <a href='UserPatterns.aspx?authorId=<%# Eval("UserId") %>'><%# Eval("FullName") %> (<%# Eval("PatternCount") %>)</a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListTopContributors"
                    TypeName="RegExLib.Data.ExpressionManager"></asp:ObjectDataSource>
            <li style="margin-top: 12px; font-weight: bold;"><asp:HyperLink runat="server" ID="contributorslink" Text="All Contributors" NavigateUrl="~/Contributors.aspx" /></li>
        </ul>
    </div>
