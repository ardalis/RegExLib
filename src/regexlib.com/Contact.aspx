<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.Contact" Title="Untitled Page" Codebehind="Contact.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="commonContainerHeader">
        <h2>Contact Us</h2>
    </div>
    <div class="commonContainer">
<asp:MultiView ID="ContactMultiView" runat="server" ActiveViewIndex="0">
    <asp:View ID="ContactForm" runat="server">
		<p>
		    Your Name:<br />
		    <asp:TextBox ID="NameTextBox" runat="server" CssClass="formField" Width="200" /><br />
		    Your Email:<br />
		    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="formField" Width="200" /><br />
		    Subject:<br />
		    <asp:TextBox ID="SubjectTextBox" runat="server" CssClass="formField" Width="200" /><br />
		    Message:<br />
		    <asp:TextBox ID="MessageTextBox" runat="server" TextMode="MultiLine" Width="460px" Rows="20" CssClass="formField" />
		</p>
        <asp:Button runat="server" ID="SendButton" Text="Send" OnClick="SendButton_Click" CssClass="buttonLarge marginAddTop marginAddBottom" />
    </asp:View>
    <asp:View ID="Confirmation" runat="server">
        <h3><asp:Label runat="server" id="MessageLabel" Text="Message sent successfully!" /></h3>
    </asp:View>
</asp:MultiView>
    </div>
</asp:Content>