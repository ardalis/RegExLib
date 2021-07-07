<%@ Control Language="C#" AutoEventWireup="true" Inherits="Results" Codebehind="Results.ascx.cs" %>

<asp:label id="lblNoResults" runat="server" visible="False">No Matches</asp:label>
<asp:Label ID="lblProblemWithRegex" runat="server" Visible="false" EnableViewState="false">
	There was a problem with your regular expression which prevents testing it.
</asp:Label>
<asp:Panel ID="pnlGrid" runat="server" Visible="false" />

<asp:Panel ID="pnlTreeView" runat="server" Visible="false">
    
</asp:Panel>

<asp:Panel ID="pnlClient" runat="server" Visible="false">
    <div id="resulttable" />
</asp:Panel>
