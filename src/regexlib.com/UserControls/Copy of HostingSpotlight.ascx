<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="true" Inherits="RegExLib.Web.UserControls.HostingSpotlight" Codebehind="HostingSpotlight.ascx.cs" %>
<div class="productSpotlight">
    <table width="100%" id="Table1">
        <tr><td><b>Hosting Spotlight</b></td></tr>
        <tr><td>
            <img src="http://www.aspalliance.com/images/icons_16x16/server-active_icon.gif" alt="Hosting Spotlight" align="left" height="16" width="16" />&nbsp;
                <%--<script language="javascript" src="http://ads.aspalliance.com/displayad.aspx?t=10&amp;m=50&amp;page=1&amp;awr=1"></script>--%>
                <rel:LQHostingSpotlightControl ID="HostingSpotlight1" runat="server" Zone="1" />
            </td>
        </tr>
    </table>
</div>