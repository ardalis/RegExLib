<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RegExLib.Web.UserPatterns" Codebehind="UserPatterns.aspx.cs" %>

<%@ Register Src="~/UserControls/RatingImage.ascx" TagName="RatingImage" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="commonContainerHeader">
        <h2>
            Expressions by User</h2>
    </div>
    <div class="commonContainerSubHead">
        <asp:HyperLink runat="server" ID="NewExpressionLink" NavigateUrl="Add.aspx"
            OnDataBinding="MyExpressionsCommandArea_DataBinding" CssClass="buttonSmallLong">Add Expression</asp:HyperLink>
        <uc1:Pager runat="server" ID="Pager1" />
    </div>
    <asp:Repeater ID="SearchResultsRepeater" runat="server" DataSourceID="ObjectDataSource1" OnItemDataBound="SearchResultsRepeater_ItemDataBound">
        <ItemTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="searchResultsTable">
                <tr valign="top" class="title">
                    <th scope="row" width="23%">
                        Title</th>
                    <td width="77%">
                        <a href='RETester.aspx?regexp_id=<%# Eval("Id") %>' class="buttonSmall">Test</a>
                        <a href='REDetails.aspx?regexp_id=<%# Eval("Id") %>' class="buttonSmall">Details</a>
                        <asp:PlaceHolder runat="server" ID="EditButtonArea" OnDataBinding="EditButtonArea_DataBinding">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "Edit.aspx?regexp_id=" + Eval( "Id" ) %>'
                                class="buttonSmall">
                                Edit</asp:HyperLink>
                        </asp:PlaceHolder>
                        <a href='REDetails.aspx?regexp_id=<%# Eval("Id") %>'>
                            <%# Eval("TitleAsHtml")%>
                        </a>
                    </td>
                </tr>
                <tr class="expression">
                    <th scope="row">
                        Expression</th>
                    <td>
                        <div class="expressionDiv">
                            <%# Eval("PatternAsHtml")%>
                        </div>
                    </td>
                </tr>
                <tr class="description">
                    <th scope="row">
                        Description</th>
                    <td>
                        <div class="overflowFixDiv"><%# Eval("DescriptionAsHtml")%></div>
                    </td>
                </tr>
                <tr class="matches">
                    <th scope="row">
                        Matches</th>
                    <td>
                        <div class="overflowFixDiv"><%# Eval("MatchingTextAsHtml")%></div>
                    </td>
                </tr>
                <tr class="nonmatches">
                    <th scope="row">
                        Non-Matches</th>
                    <td>
                        <div class="overflowFixDiv"><%# Eval("NonMatchingTextAsHtml")%></div>
                    </td>
                </tr>
                <tr class="author">
                    <th scope="row">
                        Author</th>
                    <td>
                        <span class="rating">Rating:
                            <uc1:RatingImage ID="RatingImage1" runat="server" Rating='<%# ((int)Eval("Rating") % 6) %>' />
                        </span><a href='UserPatterns.aspx?authorId=<%# Eval("AuthorId") %>'>
                            <%# Eval("AuthorName") %>
                        </a>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <SeparatorTemplate>
            <%-- PUT THIS DIV AFTER EVERY 5 SEARCH RESULTS --%>
            <div class="commonContainer paddingNoneSides marginNoneBottom center">
                <rel:LQBannerControl runat="server" ID="LQBannerControl1" Zone="1" />
            </div>
        </SeparatorTemplate>
    </asp:Repeater>
    <div class="commonContainerSubHead">
        <asp:HyperLink runat="server" ID="NewExpressionLinkBottom" NavigateUrl="Add.aspx"
            OnDataBinding="MyExpressionsCommandArea_DataBinding" CssClass="buttonSmallLong">Add Expression</asp:HyperLink>
        <uc1:Pager runat="server" ID="Pager2" />
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListExpressionsByAuthorPaged" OnSelecting="ObjectDataSource1_Selecting"
        TypeName="RegExLib.Data.ExpressionManager">
        <SelectParameters>
            <asp:QueryStringParameter Name="authorId" QueryStringField="authorId" Type="Object" />
            <asp:Parameter Name="pageSize" Type="Int32" />
            <asp:QueryStringParameter Name="page" Type="Int32" QueryStringField="p" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
