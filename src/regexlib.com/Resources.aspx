<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.Resources" Codebehind="Resources.aspx.cs" %>
<%@ Import Namespace="RegExLib" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="nonFramedContainer">
        <h2>Regex Resources</h2>
        <p>
            <a name="tools"></a>
            This is a collection of regular expresion related <a href="#tools">tools</a> and <a href="#books">books</a> from around the 
            internet. Please contact us via the <a href="Contact.aspx">Contact page</a> if 
            you have a resource that you would like added to the list. 
        </p>
    </div>
	<div class="commonContainerHeader">
        <h3>Tools</h3>
	</div>
    <div class="commonContainer">
        <asp:Repeater ID="ResourcesRepeater" runat="server" DataSourceID="XmlDataSource1">
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="notAspNetControl">
	                <tr>
		                <td width="112" align="left" valign="top" class="resourcesImageCell">
        		            <p><a href='<%# XPath("NavigateUrl") %>'>
        		            <asp:Image ID="imgResource" runat="server" Visible='<%# XPath("ImageUrl").ToString().Length > 0 %>' ImageUrl='<%# XPath("ImageUrl") %>' BorderWidth="0" AlternateText='<%# XPath("AlternateText") %>' /></a></p>
			                <p><%# XPath("AlternateText") %></p>
		                </td>
		                <td valign="top">
			                <h3><a href='<%# XPath("NavigateUrl") %>'><%# XPath("Title") %></a></h3> 
			                <%# XPath("Description") %>
                        </td>
	                </tr>
                </table>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
        </asp:Repeater>
    </div>

    <p><a name="books"></a></p>

	<div class="commonContainerHeader">
        <h3>Books</h3>
	</div>
    <div class="commonContainer">
        <asp:Repeater ID="BooksRepeater" runat="server" DataSourceID="XmlDataSource2">
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="notAspNetControl">
	                <tr>
		                <td width="112" align="left" valign="top" class="resourcesImageCell">
                            <p><a href='<%# XPath("NavigateUrl") %>'>
                            <asp:Image ImageAlign="TextTop" ID="imgResource" runat="server" ImageUrl='<%# XPath("ImageUrl") %>' BorderWidth="0" AlternateText='<%# XPath("AlternateText") %>' /></a></p>
                        </td>
                        <td valign="top">
                            <h3><a href='<%# XPath("NavigateUrl") %>'><%# XPath("Title") %></a></h3> 
                            <%# XPath("Description") %>
                        </td>
	                </tr>
                </table>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
        </asp:Repeater>
    </div>

<asp:XmlDataSource EnableCaching="false" ID="XmlDataSource1" runat="server" DataFile="~/App_Data/Advertisements.xml" XPath="Advertisements/Ad[@type='tool']">
</asp:XmlDataSource>

<asp:XmlDataSource EnableCaching="false" ID="XmlDataSource2" runat="server" DataFile="~/App_Data/Advertisements.xml" XPath="Advertisements/Ad[@type='book']">
</asp:XmlDataSource>
</asp:Content>





