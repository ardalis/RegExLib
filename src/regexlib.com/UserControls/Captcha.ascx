<%@ Control Language="C#" AutoEventWireup="true" Inherits="CaptchaControl" Codebehind="Captcha.ascx.cs" %>

<asp:Label ID="lblCMessage" runat="server"></asp:Label><br />
<asp:Image ID="imgCaptcha" runat="server" Height="92px" Width="206px" CssClass="captchaImage" />
<asp:TextBox ID="txtCaptcha" runat="server" CssClass="formField"></asp:TextBox>
<asp:Button ID="btnCSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="buttonLarge" style="margin-top: 6px;" />