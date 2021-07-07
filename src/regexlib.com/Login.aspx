<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.Login" Codebehind="Login.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div class="commonContainerHeader">
	    <h2>Login</h2>
	</div>
    <div class="commonContainer loginContainer">
        <asp:Login ID="Login1" runat="server" >
            <LayoutTemplate>
                <h3>Login to contribute</h3>
                <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                    <tbody>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" CssClass="formField" Width="160"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" CssClass="formField" OnTextChanged="Password_TextChanged"
                                                    TextMode="Password" Width="160"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="color: red">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="buttonLarge"
                                                    Text="Log In" ValidationGroup="Login1" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:Login>
        <p>
            Don't have an account yet? <a href="Register.aspx">Register Here</a>.
        </p>
        <div>
        <h4>Don't know your username?</h4>
        <p>
          We upgraded our user database in February 2006, replacing email address logins with username logins.  If you know you've registered already and want to know your user name, <a href="/contact.aspx">contact us</a> with the email you used as your login previously and we will send you your user name (which you can then use below to get your password reset).  Thanks!
        </p>
        </div>
        <hr />
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" >
            <UserNameTemplate>
                <h3>Forgot Your Password?</h3>
                <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                    <tbody>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td colspan="2">Enter your User Name to reset your password.</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" CssClass="formField"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                                                <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" CssClass="buttonLarge"
                                                    Text="Submit" ValidationGroup="PasswordRecovery1" />
                                                    </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="color: red">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </UserNameTemplate>
        </asp:PasswordRecovery>
    </div>
</asp:Content>

