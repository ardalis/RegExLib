<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.Register" Codebehind="Register.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="commonContainerHeader">
        <h2>Register</h2>
    </div>
    
    <div class="commonContainer registerContainer">
    
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="~/Default.aspx" OnCreatedUser="CreateUserWizard1_CreatedUser">
        <HeaderTemplate><h3>Sign up for your new account</h3></HeaderTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="2">
                        <tr>
                            <td align="right"><asp:label AssociatedControlID="FullName" ID="FullNameLabel" runat="server">Full Name:</asp:label></td>
                            <td>
                                <asp:TextBox ID="FullName" runat="server" CssClass="formField" Width="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FullName"
                                        ErrorMessage="FullName is required." ToolTip="Full Name is required." ValidationGroup="CreateUserWizard1">*
                                    </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label for="UserName">User Name:</label></td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" CssClass="formField" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label for="Password">Password:</label></td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="formField"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label for="ConfirmPassword">Confirm Password:</label></td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="formField"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label for="Email">E-mail:</label></td>
                            <td>
                                <asp:TextBox ID="Email" runat="server" CssClass="formField" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                    ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: red">
                                <asp:ValidationSummary DisplayMode="BulletList" runat="server" ID="valSummary" ValidationGroup="CreateUserWizard1" />
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server" >
                <ContentTemplate>
                    <table border="0">
                        <tbody>
                            <tr>
                                <td align="left" colspan="2">
                                    Complete</td>
                            </tr>
                            <tr>
                                <td>
                                    Your account has been successfully created.</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue"
                                        CssClass="buttonLarge" Text="Continue" ValidationGroup="CreateUserWizard1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
        <FinishCompleteButtonStyle CssClass="buttonLarge" />
        <CreateUserButtonStyle CssClass="buttonLarge" />
        <FinishPreviousButtonStyle CssClass="buttonLarge" />
    </asp:CreateUserWizard>
    
    </div>
</asp:Content>
