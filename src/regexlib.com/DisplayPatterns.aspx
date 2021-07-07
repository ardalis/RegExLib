<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.DisplayPatterns" EnableViewState="false" Codebehind="DisplayPatterns.aspx.cs" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="~/UserControls/RatingImage.ascx" TagName="RatingImage" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="commonContainerHeader">
        <h2>Browse Expressions by Category</h2>
    </div>
    <div class="highlightContainer searchContainer">
        <ul class="tabnav">
            <asp:Repeater ID="rptCategoryTabs" runat="server" DataSourceID="ObjectDataSource2" EnableViewState="False">
                <ItemTemplate>
                    <li><a href='DisplayPatterns.aspx?cattabindex=<%# Container.ItemIndex %>&categoryId=<%# Eval("Id") %>'><%# Eval("Description") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <h3><asp:label id="lblResultCount" runat="server" style="text-decoration: underline;" /> regular expressions found in this category!</h3>
    </div>
    
    <div class="commonContainerHeader">
        <h2>Expressions in category: <asp:Label ID="CategoryShownLabel" runat="server" style="color: #000000;"></asp:Label></h2>
    </div>
	<div class="commonContainerSubHead">
	    <uc1:Pager runat="server" id="Pager1" />
	</div>    
    <asp:Repeater ID="SearchResultsRepeater" runat="server" DataSourceID="ObjectDataSource1" OnItemDataBound="SearchResultsRepeater_ItemDataBound">
        <ItemTemplate>
            <table border="0" cellspacing="0" cellpadding="0" class="searchResultsTable">
                <tr valign="top" class="title">
                    <th scope="row" width="23%">Title</th>
                    <td width="77%">
                        <a href='RETester.aspx?regexp_id=<%# Eval("Id") %>' class="buttonSmall">Test</a>
                        <a href='REDetails.aspx?regexp_id=<%# Eval("Id") %>' class="buttonSmall">Details</a>
                        <a href='REDetails.aspx?regexp_id=<%# Eval("Id") %>'>
                            <%# Eval("TitleAsHtml")%>
                        </a>
                    </td>
                </tr>
                <tr class="expression">
                    <th scope="row">Expression</th>
                    <td><div class="expressionDiv"><%# Eval("PatternAsHtml")%></div></td>
                </tr>
                <tr class="description">
                    <th scope="row">Description</th>
                    <td><div class="overflowFixDiv"><%# Eval("DescriptionAsHtml")%></div></td>
                </tr>
                <tr class="matches">
                    <th scope="row">Matches</th>
                    <td><div class="overflowFixDiv"><%# Eval("MatchingTextAsHtml")%></div></td>
                </tr>
                <tr class="nonmatches">
                    <th scope="row">Non-Matches</th>
                    <td><div class="overflowFixDiv"><%# Eval("NonMatchingTextAsHtml")%></div></td>
                </tr>
                <tr class="author">
                    <th scope="row">Author</th>
                    <td>
                        <span class="rating">Rating:
                        <uc1:RatingImage ID="RatingImage1" runat="server" Rating='<%# ((int)Eval("Rating") % 6) %>' /></span>
                        <a href='UserPatterns.aspx?authorId=<%# Eval("AuthorId") %>'>
                            <%# Eval("AuthorName") %>
                        </a>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <SeparatorTemplate>
        <%-- PUT THIS DIV AFTER EVERY 5 SEARCH RESULTS --%>
	    <div class="commonContainer paddingNoneSides marginNoneBottom center">
		    <rel:LQBannerControl runat="server" id="LQBannerControl1" Zone="1" />
        </div>
        </SeparatorTemplate>
    </asp:Repeater>
	<div class="commonContainerSubHead">
	    <uc1:Pager runat="server" id="Pager2" />
	</div>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListExpressionsByCategoryPaged"
        TypeName="RegExLib.Data.ExpressionManager" EnablePaging="False">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="1" Name="categoryId" QueryStringField="categoryId"
                Type="Int32" />
            <asp:Parameter DefaultValue="rating DESC" Name="sortExpression" Type="String" />
            <asp:Parameter DefaultValue="20" Name="pagesize" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="1" Name="pageId" QueryStringField="p"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
        
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="ListCategories"
    TypeName="RegExLib.Data.CategoryManager" />

</asp:Content>

