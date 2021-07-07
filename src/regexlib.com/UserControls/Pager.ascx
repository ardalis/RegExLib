<%@ Control Language="C#" AutoEventWireup="true" Codebehind="Pager.ascx.cs" Inherits="RegexLib.com.UserControls.Pager" EnableViewState="false" %>
<asp:Label runat="server" ID="PagerPanel1" >
Change page:
<asp:HyperLink runat="server" ID="StepBackAllHyperLink" SkinID="BackAll" NavigateUrl="#" />
<asp:HyperLink runat="server" ID="StepBackOneHyperLink" SkinID="BackOne" NavigateUrl="#" />
<asp:HyperLink runat="server" ID="StepForwardOneHyperLink" SkinID="ForwardOne" NavigateUrl="#" />
<asp:HyperLink runat="server" ID="StepForwardAllHyperLink" SkinID="ForwardAll" NavigateUrl="#" />
&nbsp;&nbsp;|
</asp:Label>
&nbsp;&nbsp; Displaying page
<asp:Label runat="server" ID="CurrentPageLabel" />
of
<asp:Label runat="server" ID="TotalPagesLabel" /> pages;
Items <asp:Label runat="server" ID="FirstItemLabel" /> to <asp:Label runat="server" ID="LastItemLabel" />