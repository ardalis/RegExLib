<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.REDetails" Codebehind="REDetails.aspx.cs" %>
<%@ Register Src="UserControls/UserComments.ascx" TagName="UserComments" TagPrefix="uc" %>
<%@ Register Src="UserControls/RatePattern.ascx" TagName="RatePattern" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/RatingImage.ascx" TagName="RatingImage" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="commonContainerHeader">
        <h2>Regular Expression Details</h2>
    </div>
    <asp:Panel ID="DetailsPanel" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" class="searchResultsTable">
            <tr valign="top" class="title">
                <th scope="row" width="23%">Title</th>
                <td width="77%">
                    <asp:HyperLink ID="TestLink" runat="server" CssClass="buttonSmall" Text="Test" />
                    <a href='Search.aspx' class="buttonSmall">Find</a>
                    <asp:HyperLink ID="EditLink" runat="server" CssClass="buttonSmall" Text="Edit" />
                    <asp:Label runat="server" ID="TitleLabel" />
                </td>
            </tr>
            <tr class="expression">
                <th scope="row">Expression</th>
                <td><div class="expressionDiv"><asp:Label runat="server" ID="ExpressionLabel" /></div></td>
            </tr>
            <tr class="description">
                <th scope="row">Description</th>
                <td><div class="overflowFixDiv"><asp:Label runat="server" ID="DescriptionLabel" /></div></td>
            </tr>
            <tr class="matches">
                <th scope="row">Matches</th>
                <td><div class="overflowFixDiv"><asp:Label runat="server" ID="MatchesLabel" /></div></td>
            </tr>
            <tr class="nonmatches">
                <th scope="row">Non-Matches</th>
                <td><div class="overflowFixDiv"><asp:Label runat="server" ID="NonMatchesLabel" /></div></td>
            </tr>
            <tr class="author paddingNoneBottom">
                <th scope="row">Author</th>
                <td>
                    <span class="rating">Rating:
                        <uc1:RatingImage ID="RatingImage1" runat="server" /></span>
                    <asp:HyperLink runat="server" ID="AuthorHyperlink"></asp:HyperLink>
                </td>
            </tr>
            <tr class="source">
                <th scope="row">Source</th>
                <td><asp:Label runat="server" ID="SourceLabel" /></td>
            </tr>
            <tr class="yourRating">
              <th scope="row">Your Rating</th>
              <td>
                <uc:RatePattern runat="server" id="RatePattern1" />
		      </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="InvalidExpression" runat="server" Visible="false" CssClass="commonContainer">
    <h4>Oops!</h4><p>The expression you're trying to display doesn't exist.</p>
    </asp:Panel>


<br />
<uc:UserComments runat="server" id="UserComments1" />

</asp:Content>

