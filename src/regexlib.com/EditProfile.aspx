<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="EditProfile" Codebehind="EditProfile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	
	<asp:Panel runat="server" ID="UpdateProblem" Visible="False" ForeColor="red" >
		<asp:Label runat="server" ID="UpdateProblemText" />
		<br />Contact the administrator if this problem persists.
	</asp:Panel>
	
	<div class="Form">
        <div class="commonContainerHeader">
		    <h2>Edit Your Login Information</h2>
        </div>
        <div class="commonContainer updateContainer">
                <table border="0" cellpadding="2">
                        <tr>
                            <td colspan="2"><h3>Update your profile</h3></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label runat="server" ID="FullNameLabel" Text="Full Name" AssociatedControlID="FullName" /></td>
                            <td><asp:TextBox runat="server" ID="FullName" CssClass="formField" Width="200" /><asp:RequiredFieldValidator runat="server" ID="FullNameRequired" ControlToValidate="FullName" ErrorMessage="Full Name Is Required" Display="Dynamic" /></td>
                       </tr>
                       <tr>
                           <td align="right"><asp:Label runat="server" ID="EmailLabel" Text="Email" AssociatedControlID="Email" /></td>
                           <td><asp:TextBox runat="server" ID="Email" CssClass="formField" Width="200" /><asp:RequiredFieldValidator runat="server" ID="EmailRequired" ControlToValidate="Email" ErrorMessage="Email Is Required" Display="Dynamic" /></td>
                       </tr>
                       <tr>
                           <td colspan="2"><hr /></td>
                       </tr>
                       <tr>
                            <td colspan="2"><h3>Change Your Password</h3></td>
                        </tr>
                        <tr>
                            <td align="right"><asp:Label runat="server" ID="OldPasswordLabel" Text="Old Password" AssociatedControlID="OldPassword" /></td>
                            <td><asp:TextBox runat="server" ID="OldPassword" TextMode="Password" CssClass="formField" Width="200" /></td>
                       </tr>
                       <tr>
                            <td align="right"><asp:Label runat="server" ID="NewPasswordLabel" Text="New Password" AssociatedControlID="NewPassword" /></td>
                            <td><asp:TextBox runat="server" ID="NewPassword" TextMode="Password" CssClass="formField" Width="200" /><br /></td>
                       </tr>
                       <tr>
                            <td align="right"><asp:Label runat="server" ID="NewPasswordConfirmLabel" Text="Confirm New Password" AssociatedControlID="NewPasswordConfirm" /></td>
                            <td><asp:TextBox runat="server" ID="NewPasswordConfirm" TextMode="Password" CssClass="formField" Width="200" /></td>
                       </tr>
                       <tr>
                            <td colspan="2"><asp:CompareValidator ID="ConfirmPasswordMatches" runat="server" ErrorMessage="Passwords Don't Match" Display="Dynamic" ControlToValidate="NewPasswordConfirm" ControlToCompare="NewPassword" ></asp:CompareValidator></td>
                       </tr>
                       <tr>
                            <td colspan="2" align="right">
                                <asp:Button runat="server" ID="SaveProfileChanges" Text="Save" CssClass="buttonLarge" OnClick="SaveProfileChanges_Click" />
		                        <asp:Button runat="server" ID="Cancel" Text="Cancel" CssClass="buttonLarge" OnClick="Cancel_Click" />
                            </td>
                       </tr>
                   </table>
	    </div>
	</div>
	
</asp:Content>
