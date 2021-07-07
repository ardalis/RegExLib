<%@ Control Language="C#" AutoEventWireup="False" Inherits="RegExLib.Web.UserControls.UserComments" Codebehind="UserComments.ascx.cs" %>
<%@ Register TagName="Captcha" TagPrefix="Controls" Src="~/UserControls/Captcha.ascx" %>

<div class="commonContainerHeader">
    <h2>Enter New Comment</h2>
</div>
<asp:Panel ID="CommentSubmittedPanel" runat="server" CssClass="commonContainer" Visible="false">
    <p>Thank you for submitting your comment. <br />
        Please allow a few minutes before returning to this page to see your comment in the section below.</p>
</asp:Panel>
<asp:FormView ID="CommentFormView" DefaultMode="Insert" Runat="server" CssClass="controlGeneratedTableContainer" 
        OnItemInserting="CommentFormView_ItemInserting" DataSourceID="ObjectDataSource2" Width="100%">
     <InsertItemTemplate>
        <asp:ValidationSummary id="ValidationSummary1" 
            runat="server" 
            ValidationGroup="UserCommentGroup" 
            HeaderText="Please fix the following errors before attempting to re-submit your comment:" 
            DisplayMode="BulletList"
            EnableViewState="false"  />
        <div class="commonContainer">
	        <p>Title<br />
                <asp:textbox Id="txtCommentTitle" Runat="server" Text='<%# Bind("Title") %>' Columns="80" MaxLength="150" 
                    ValidationGroup="UserCommentGroup" CssClass="formField"  />
                <asp:requiredfieldvalidator id="RequiredFieldValidator1" ValidationGroup="UserCommentGroup" runat="server" 
                    errormessage="You must enter a title for your comment." controltovalidate="txtCommentTitle">*</asp:requiredfieldvalidator>
                <br />
	            Name<br />
                <asp:textbox Id="txtName" Runat="server" Text='<%# Bind("Name") %>' Columns="80" MaxLength="120" 
                    ValidationGroup="UserCommentGroup" CssClass="formField" />
                <asp:requiredfieldvalidator id="RequiredFieldValidator2" ValidationGroup="UserCommentGroup" runat="server" 
                    errormessage="You must enter your name." controltovalidate="txtName">*</asp:requiredfieldvalidator>
                <br />
	            Comment<br />
                <asp:textbox textmode="MultiLine" CssClass="formField" Height="80px" Width="97%" 
                    Id="txtComment" Runat="server" 
                    Text='<%# Bind("Comment") %>'
                    ValidationGroup="UserCommentGroup" />
                <asp:requiredfieldvalidator id="RequiredFieldValidator3" ValidationGroup="UserCommentGroup" runat="server" 
                    errormessage="You must enter a comment." controltovalidate="txtComment">*</asp:requiredfieldvalidator>
	            <br />
	            
                <Controls:Captcha id="captcha1" Message="Spammers suck - we apologize. Please enter the text shown below to enable your comment (not case sensitive - try as many times as you need to if the first ones are too hard):" runat="server" OnSuccess="OnSuccess" OnFailure="OnFailure" />
	            <asp:button id="btnAddComment" runat="server" CommandName="Insert" text="Enter Comment" CssClass="buttonLarge"
                    ValidationGroup="UserCommentGroup" />
            </p>
        </div>
     </InsertItemTemplate>  
</asp:FormView>


    <div class="commonContainerHeader">
        <h2>Existing User Comments  </h2>
    </div>
    <div class="commonContainer"> 
        <asp:Repeater ID="SearchResultsRepeater" runat="server" DataSourceID="ObjectDataSource1">
            <ItemTemplate>
                <p>
	                <b>Title: </b><%# Server.HtmlEncode(Eval("Title").ToString()) %><br />
	                <b>Name: </b><%# Server.HtmlEncode(Eval("Name").ToString()) %><br />
	                <b>Date: </b><%# Eval("DateCreated") %><br />
	                <b>Comment: </b><br /><%# Server.HtmlEncode(Eval("Comment").ToString()) %>
                </p>
                <hr />
            </ItemTemplate>
        </asp:Repeater>
    </div>

<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListCommentsByExpression" TypeName="RegExLib.Data.CommentManager">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="0" Name="expressionId" QueryStringField="regexp_id" Type="Int32" />
        <asp:Parameter Name="maximumRows" Type="Int32" />
        <asp:Parameter Name="startRowIndex" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" InsertMethod="CreateComment"
    SelectMethod="ListCommentsByExpression" TypeName="RegExLib.Data.CommentManager">
    <InsertParameters>
        <asp:Parameter Name="comment" Type="String" />
        <asp:Parameter Name="expressionId" Type="Int32" />
        <asp:Parameter Name="name" Type="String" />
        <asp:Parameter Name="title" Type="String" />
        <asp:Parameter Name="url" Type="String" />
    </InsertParameters>
</asp:ObjectDataSource>