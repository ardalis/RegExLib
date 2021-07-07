<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage.master" Inherits="RegExLib.Web._Default" Title="Find Regular Expressions" Codebehind="Default.aspx.cs" %>
<%@ Import Namespace="RegExLib" %>
<%@ Register Src="~/UserControls/RegexAdviceBlogs.ascx" TagPrefix="rxl" TagName="RegexAdviceBlogs"  %>
<%@ Register Src="~/UserControls/RegexAdviceForums.ascx" TagPrefix="rxl" TagName="RegexAdviceForums"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="highlightContainer welcomeContainer">
        <p>Welcome to <strong>RegExLib.com</strong>, the Internet's first Regular Expression Library. Currently we 
            have indexed <asp:label runat="server" id="reCount" Font-Bold="true" /> expressions from 
            <asp:label runat="server" id="contributorCountLabel" Font-Bold="true" /> contributors around the world. 
            We hope you'll find this site useful and come back whenever you 
            <a href="http://regexadvice.com/forums/">need help</a> writing an expression, 
            you're<a href="/Search.aspx"> looking for an expression</a> for a particular task, or are ready to 
            <a href="/Add.aspx">contribute  new expressions</a> you&rsquo;ve just figured out. Thanks!</p>
        <p class="linkBoldBulleted"><a href="/Add.aspx">Add Regex</a></p>
    </div>

	<div class="commonContainerHeader"><h2>Find Expressions</h2></div>
    <div class="commonContainer searchContainer">
        <table border="0" cellpadding="0" cellspacing="0" class="notAspNetControl">
            <tr>
	            <td>
		            Enter Keywords (e.g. email) <br />
		            <asp:TextBox id="txtSearch" runat="server" CssClass="formField" />
		            <%--<asp:TextBox ID="TextBox1" runat="server" style="visibility:hidden;display:none;" />--%>
		            <asp:Button id="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="buttonLarge"></asp:Button>
                </td>
	            <td valign="middle" class="advancedSearchTD">
		            <p class="linkBoldBulleted paddingNoneTopBottom"><a href="/Search.aspx">Advanced <br />
	                Search</a></p>
              </td>
            </tr>
        </table>
    </div>

	<div class="commonContainerHeader">
	    <h2>Regex Resources </h2>
	</div>
    <div class="commonContainer">
    <table border="0" cellpadding="0" cellspacing="0" class="notAspNetControl">
        <tr>
            <td width="112" align="left" valign="top" class="resourcesImageCell">
                <p><asp:AdRotator ID="AdRotator1" runat="server" AdvertisementFile="~/App_Data/Advertisements.xml" 
                    KeywordFilter="HomePage" OnAdCreated="AdRotator1_AdCreated" /></p>
                <p id="lblResourceAltText" runat="server"></p>
            </td>
            <td align="left" valign="top">
                <h3 runat="server" id="lblResourceTitle"></h3>
                <asp:PlaceHolder id="phFeaturedResource" runat="server" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" class="notAspNetControl">
        <tr>
          <td width="112" align="left" valign="top">&nbsp;</td>
          <td><p class="linkBoldBulleted"><a href="Resources.aspx">View all Regular Expression resources...</a></p></td>
        </tr>
    </table>
    </div>
	<div class="nonFramedContainer">
	    <h2>Latest News</h2>
	    <p>
		    <a href="http://RegexAdvice.com">
		    RegexAdvice.com</a> is a community devoted to 
		    the topic of regular expressions.  If you would like a free account to blog about regex related 
		    content you can request one via: <a href="http://regexadvice.com/blogs/ssmith/contact.aspx">
		    http://regexadvice.com/blogs/ssmith/contact.aspx
	        </a>
	    </p>
	</div>
    <rxl:RegexAdviceForums ID="RegexAdviceForums1" runat="server" />
	<div class="commonContainer paddingNoneSides center">
		<rel:LQBannerControl runat="server" id="LQBannerControl1" Zone="1" />
    </div>
    <rxl:RegexAdviceBlogs ID="RegexAdviceBlogs1" runat="server" />
    
    
</asp:Content>

