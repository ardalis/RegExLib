<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="True"
    Inherits="RegExLib.Web.Contributors" Title="Contributors" EnableViewState="false"
    Codebehind="Contributors.aspx.cs" %>

<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="commonContainerHeader">
        <h2>
            Contributors</h2>
    </div>
    <div class="commonContainerSubHead">
        <uc1:Pager runat="server" ID="Pager1" />
    </div>
    <div class="commonContainerSubHead">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="50%">
                    <h4>
                        Name<%-- [<a href="#">sort</a>]--%></h4>
                </td>
                <td width="50%">
                    <h4>
                        Expressions<%-- [<a href="#">sort</a>]--%></h4>
                </td>
            </tr>
        </table>
    </div>
    <div class="commonContainer marginNoneBottom">
        <asp:Repeater runat="server" ID="ContributorsRepeater" DataSourceID="SqlDataSource1"
            OnItemDataBound="ContributorsRepeater_ItemDataBound">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listTable">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td width="50%" class="linkBulleted">
                        <%# Eval("FullName") %>
                    </td>
                    <td width="50%">
                        <a href="/UserPatterns.aspx?authorid=<%# Eval("userid") %>">View Expressions (<%# Eval("ExpressionCount") %>)</a>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color: White;">
                    <td width="50%" class="linkBulleted" style="background-color: White;">
                        <%# Eval("FullName") %>
                    </td>
                    <td width="50%">
                        <a href="/UserPatterns.aspx?authorid=<%# Eval("userid") %>">View Expressions (<%# Eval("ExpressionCount") %>)</a>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <SeparatorTemplate>
                <tr>
                    <td colspan="2" align="center" class="banner">
                        <rel:LQBannerControl runat="server" ID="LQBannerControl1" Zone="1" />
                    </td>
                </tr>
            </SeparatorTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="commonContainerSubHead">
        <uc1:Pager runat="server" ID="Pager2" />
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:regexlibConnectionString %>"
        SelectCommand="SELECT userid, FullName, ExpressionCount
FROM          (SELECT TOP 5000 m.UserId, p.FullName, COUNT(ex.ExpressionId) AS ExpressionCount, 
ROW_NUMBER() OVER (order by COUNT(ex.ExpressionId) desc) AS RowNum
                        FROM           aspnet_Membership AS m INNER JOIN
                                                rxl_Expression AS ex ON m.UserId = ex.UserId INNER JOIN
                                                rxl_Profile AS p ON m.UserId = p.UserId
                        WHERE       (ex.Enabled = 1)
                        GROUP BY m.Email, m.UserId, p.FullName
                        ORDER BY ExpressionCount DESC, p.FullName) AS derivedtbl_1
WHERE      (RowNum BETWEEN (@PageId - 1) * 50 AND @PageId * 50)"
        EnableCaching="true" CacheDuration="120">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Type="int32" Name="PageId" QueryStringField="p" />
            </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>
