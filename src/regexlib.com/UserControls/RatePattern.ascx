<%@ Control Language="C#" AutoEventWireup="true" Inherits="RatePattern" Codebehind="RatePattern.ascx.cs" %>
<%@ Register Src="~/UserControls/RatingImage.ascx" TagName="RatingImage" TagPrefix="uc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlRating" runat="server" Visible="false">
                    <span class="rating">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit Rating" OnClick="btnSubmit_Click" CssClass="buttonLarge" />
                    </span>
                    <strong>Bad</strong> 
                    <asp:RadioButton GroupName="RatingValues" Text="1" TextAlign="Right" ID="Rating1" runat="server" />
                    <asp:RadioButton GroupName="RatingValues" Text="2" TextAlign="Right" ID="Rating2" runat="server" />
                    <asp:RadioButton GroupName="RatingValues" Text="3" TextAlign="Right" ID="Rating3" runat="server" />
                    <asp:RadioButton GroupName="RatingValues" Text="4" TextAlign="Right" ID="Rating4" runat="server" />
                    <asp:RadioButton GroupName="RatingValues" Text="5" TextAlign="Right" ID="Rating5" runat="server" />
			        <strong>Good</strong>
        </asp:Panel>

        <asp:Panel ID="pnlYourRating" runat="server" Visible="false">
            <uc1:RatingImage id="RatingImage2" runat="server" Rating="0" />
            <asp:LinkButton ID="lnkRateAgain" runat="server" Text="Rate again" OnClick="lnkRateAgain_Click" />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
