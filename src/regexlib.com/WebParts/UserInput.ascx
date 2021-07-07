<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserInput" Codebehind="UserInput.ascx.cs" %>
<%@ Register Src="../UserControls/ClientTesterControls.ascx" TagName="ClientTesterControls" TagPrefix="uc1" %>
<asp:Panel ID="pnlAdvancedFeatures" runat="server">
    <asp:label id="lblErrorMessage" runat="server" SkinID="ErrorLabel" />
    <table id="Table1" cellspacing="0" cellpadding="3" width="100%" border="0">
	<tr>
		<td colspan="2"><strong>External Data Source</strong></td>
	</tr>
	<tr valign="bottom">
		<td>
			<fieldset title="External Data Source">
				Url: <asp:textbox id="txtUrl" runat="server" columns="40"></asp:textbox>
				<br />File: <asp:FileUpload ID="filUpload" runat="server" />
			</fieldset>
		</td>
		<td><asp:Button id="btnExternalData" runat="server" text="Get External Data" OnClick="btnExternalData_Click" CssClass="buttonLarge" /></td>
	</tr>
	</table>
</asp:Panel>

Source:<br />
<asp:TextBox ID="txtSource" runat="server" TextMode="MultiLine" CssClass="formField" Height="80px" Width="97%" /><br />
<br />
Pattern:<br />
<asp:TextBox ID="txtPattern" runat="server" TextMode="MultiLine" CssClass="formField" Height="80px" Width="97%" /><br />

<br />

<asp:Panel ID="pnlClient" runat="Server" Visible="false">
    <uc1:ClientTesterControls ID="ClientTesterControls1" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlServer" runat="Server" Visible="true">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonLarge" />
</asp:Panel>

