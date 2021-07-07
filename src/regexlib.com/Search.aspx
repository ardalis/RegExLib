<%@ Page Language="C#" Theme="Green" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RegExLib.Web.Search" Codebehind="Search.aspx.cs" EnableViewState="false" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="~/UserControls/RatingImage.ascx" TagName="RatingImage" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="commonContainerHeader">
        <h2>Advanced Search </h2>
    </div>
    <div class="commonContainer advancedSearchContainer">
        <div class="searchCriteria">
            Keywords<br />
            <asp:TextBox ID="txtKeywords" runat="server" class="formField" /><br />
            Category<br />
            <asp:DropDownList ID="ddCategory" runat="server" DataSourceID="ObjectDataSource1" class="formStyleDropDown"
                DataTextField="Description" DataValueField="Id" OnDataBound="ddCategory_DataBound">
            </asp:DropDownList><br />
            Minimum Rating<br />
            <asp:DropDownList ID="ddMinimumRating" runat="server" class="formStyleDropDown">
                <asp:ListItem Value="-1" Text="  ( All )   " Selected="True" />
                <asp:ListItem Value="5" Text="The Best" />
                <asp:ListItem Value="4" Text="Above Average" />
                <asp:ListItem Value="3" Text="Average" />
                <asp:ListItem Value="2" Text="Below Average" />
                <asp:ListItem Value="1" Text="Pretty Bad" />
            </asp:DropDownList><br />
            Results per Page<br />
            <asp:DropDownList ID="ddPageSize" runat="server" class="formStyleDropDown">
                <asp:ListItem Value="10" Text="10" />
                <asp:ListItem Selected="True" Value="20" Text="20" />
                <asp:ListItem Value="50" Text="50" />
                <asp:ListItem Value="100" Text="100" />
            </asp:DropDownList><br />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="buttonLarge" style="margin-top: 6px;" />
        </div>
        <div class="largeRectangleAd">
            <rel:LQLargeRectangleControl ID="Large1" runat="server" Zone="1" />
        </div>
    </div>
    <div class="commonContainerHeader">
        <h2>Search Results: <asp:Label ID="ResultCountLabel1" runat="server" /> regular expressions found.</h2>
    </div>
	<div class="commonContainerSubHead">
	    <uc1:Pager runat="server" id="Pager1" />
	</div>

    <asp:Repeater ID="SearchResultsRepeater" runat="server" DataSourceID="ObjectDataSource2" OnItemDataBound="SearchResultsRepeater_ItemDataBound" >
        <ItemTemplate>
            <table border="0" cellspacing="0" cellpadding="0" class="searchResultsTable">
                <tr valign="top" class="title">
                    <th scope="row" width="23%">Title</th>
                    <td width="77%">
                        <a href='RETester.aspx?regexp_id=<%# Eval("Id") %>' class="buttonSmall">Test</a>
                        <a href='REDetails.aspx?regexp_id=<%# Eval("Id") %>' class="buttonSmall">Details</a>
                        <a href='REDetails.aspx?regexp_id=<%# Eval("Id") %>'>
                            <%# Eval("TitleAsHtml") %>
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
<%--	  	Change page: 
	  	<asp:HyperLink runat="server" ID="StepBackAllImage2" SkinID="BackAll" NavigateUrl="#" />
	  	<asp:HyperLink runat="server" ID="StepBackOneImage2" SkinID="BackOne" NavigateUrl="#" />
	  	<asp:HyperLink runat="server" ID="StepForwardOneImage2" SkinID="ForwardOne" NavigateUrl="#" />
	  	<asp:HyperLink runat="server" ID="StepForwardAllImage2" SkinID="ForwardAll" NavigateUrl="#" />
	  	&nbsp;&nbsp;|&nbsp;&nbsp; <strong><asp:Label ID="ResultCountLabel2" runat="server" /></strong> Results Found. Displaying page 1 of <asp:Label runat="server" ID="TotalPagesLabel2" />, items 1 to 25--%>
	</div>
	

    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="ListExpressionsBySearch"
        TypeName="RegExLib.Data.ExpressionManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtKeywords" Name="keywords" PropertyName="Text"
                Type="String" DefaultValue="" />
            <asp:ControlParameter ControlID="ddCategory" DefaultValue="1" Name="categoryId" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="ddMinimumRating" DefaultValue="-1" Name="minimumRating"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" DefaultValue="" />
            <asp:ControlParameter Name="maximumRows" ControlID="ddPageSize" DefaultValue="20" PropertyName="SelectedValue" />
            <asp:QueryStringParameter QueryStringField="p" Name="page" Type="int32" DefaultValue="1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListCategories"
        TypeName="RegExLib.Data.CategoryManager"></asp:ObjectDataSource>
</asp:Content>
