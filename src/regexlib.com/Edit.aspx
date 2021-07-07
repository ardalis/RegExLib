<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.Edit" 
ValidateRequest="false" Codebehind="Edit.aspx.cs" %>

<h1>Disabled for now</h1>
<!-- <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="commonContainerHeader">
        <h2>Maintain your regular expressions</h2>
    </div>

    <div class="commonContainer addEditContainer">
    <asp:ValidationSummary ID="ValidationSummary1" Runat="server" />
    <h3>
        <asp:HyperLink ID="TestLink" runat="server" CssClass="buttonSmall" Text="Test" Visible="false" />
        <asp:HyperLink ID="DetailsLink" runat="server" CssClass="buttonSmall" Text="Details" Visible="false" />
        Add or Edit an Expression
    </h3>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateEditButton="False" DataKeyNames="Id"
            AutoGenerateInsertButton="False" AutoGenerateRows="False" DataSourceID="ObjectDataSource1" DefaultMode="Edit" 
            OnItemCommand="DetailsView1_ItemCommand" OnItemInserted="DetailsView1_ItemInserted" 
            OnItemInserting="DetailsView1_ItemInserting" OnItemUpdated="DetailsView1_ItemUpdated" 
            OnItemUpdating="DetailsView1_ItemUpdating" GridLines="None" CellPadding="2" CellSpacing="2">
        <EmptyDataTemplate>
            <p>
                You currently have no patterns saved.  Click here to save a new pattern: 
                <asp:ImageButton ID="btnAddNew" runat="server" SkinID="AddImageButton" OnClick="btnAddNew_Click" />
            </p>
        </EmptyDataTemplate>

        <Fields>
            <asp:TemplateField HeaderText="Title:">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditTitle" runat="server" Text='<%# Bind("Title") %>' CssClass="formField" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEditTitle"
                        ErrorMessage="You must enter a title.">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertTitle" runat="server" Text='<%# Bind("Title") %>' CssClass="formField" Width="200px"></asp:TextBox>                       <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInsertTitle"
                        ErrorMessage="You must enter a title.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="TitleLengthValidator" runat="server" 
                        ControlToValidate="txtInsertTitle" ValidationExpression=".{1,150}"
                        ErrorMessage="The title must be less than 150 characters.">*</asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Expression:">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Pattern") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditPattern" runat="server" Text='<%# Bind("Pattern") %>' SkinID="MultiLineTextBox" 
                        TextMode="MultiLine" CssClass="formField" Width="200px" Rows="6"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEditPattern"
                        ErrorMessage="You must enter a pattern.">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertPattern" runat="server" Text='<%# Bind("Pattern") %>' SkinID="MultiLineTextBox" 
                        CssClass="formField" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInsertPattern"
                        ErrorMessage="You must enter a pattern.">*</asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Description:">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("Description") %>' SkinID="MultiLineTextBox" 
                        TextMode="MultiLine" CssClass="formField" Width="200px" Rows="8"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEditDescription"
                        ErrorMessage="You must enter a description.">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertDescription" runat="server" Text='<%# Bind("Description") %>' SkinID="MultiLineTextBox" 
                        TextMode="MultiLine" CssClass="formField" Width="200px" Rows="4"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtInsertDescription"
                        ErrorMessage="You must enter a description.">*</asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Matching Examples:">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("MatchingText") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditMatchingText" runat="server" Text='<%# Bind("MatchingText") %>' CssClass="formField" Width="200px"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="overlib('Please enter at least 3 matching text examples. Seperate each example with 3 vertical pipes |||.',
                          CAPTION, 'Tips for Entering Matches',
                          STICKY, STATUS, 'HELP: Singleline', 
                          FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                          CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                          TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                          SHADOW, SHADOWX, '4', SHADOWY, '4', 
                          FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                          WIDTH, '225'); return false;" onmouseout="nd();">
                        <asp:Image SkinID="HelpImage" ID="MatchingTipsHelpImage" runat="server" />
                    </a>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEditMatchingText"
                        ErrorMessage="You must enter some matching text.">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertMatchingText" runat="server" Text='<%# Bind("MatchingText") %>' CssClass="formField" Width="200px"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="overlib('Please enter at least 3 matching text examples. Seperate each example with 3 vertical pipes |||.',
                          CAPTION, 'Tips for Entering Matches',
                          STICKY, STATUS, 'HELP: Singleline', 
                          FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                          CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                          TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                          SHADOW, SHADOWX, '4', SHADOWY, '4', 
                          FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                          WIDTH, '225'); return false;" onmouseout="nd();">
                        <asp:Image SkinID="HelpImage" ID="MatchingTipsHelpImage2" runat="server" />
                    </a>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtInsertMatchingText"
                        ErrorMessage="You must enter some matching text.">*</asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Non-Matching Examples:">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("NonMatchingText") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditNonMatchingText" runat="server" Text='<%# Bind("NonMatchingText") %>' CssClass="formField" Width="200px"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="overlib('Please enter at least 3 examples of non-matches. Seperate each example with 3 vertical pipes |||.',
                          CAPTION, 'Tips for Entering Non-matches',
                          STICKY, STATUS, 'HELP: Singleline', 
                          FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                          CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                          TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                          SHADOW, SHADOWX, '4', SHADOWY, '4', 
                          FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                          WIDTH, '225'); return false;" onmouseout="nd();">
                        <asp:Image SkinID="HelpImage" ID="NonMatchingTipsHelpImage" runat="server" />
                    </a>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEditNonMatchingText"
                        ErrorMessage="You must enter some non-matching text.">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertNonMatchingText" runat="server" Text='<%# Bind("NonMatchingText") %>' CssClass="formField" Width="200px"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="overlib('Please enter at least 3 examples of non-matches. Seperate each example with 3 vertical pipes |||.',
                          CAPTION, 'Tips for Entering Non-matches',
                          STICKY, STATUS, 'HELP: Singleline', 
                          FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                          CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                          TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                          SHADOW, SHADOWX, '4', SHADOWY, '4', 
                          FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                          WIDTH, '225'); return false;" onmouseout="nd();">
                        <asp:Image SkinID="HelpImage" ID="NonMatchingTipsHelpImage2" runat="server" />
                    </a>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtInsertNonMatchingText"
                        ErrorMessage="You must enter some non-matching text.">*</asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
            <asp:CheckBoxField DataField="Enabled" HeaderText="Enabled:">
                <HeaderStyle HorizontalAlign="Right" />
            </asp:CheckBoxField>
            
            <asp:TemplateField HeaderText="Author:">
                <ItemTemplate>
                    <asp:Label ID="Label0" runat="server" Text='<%# Bind("AuthorName") %>' style="font-weight: bold;"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Source:">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Source") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditSource" runat="server" Text='<%# Bind("Source") %>' CssClass="formField"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertSource" runat="server" Text='<%# Bind("Source") %>' CssClass="formField"></asp:TextBox>
                </InsertItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
			<asp:BoundField DataField="ProviderId" HeaderText="ProviderId:" SortExpression="ProviderId" Visible="False" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
			<asp:BoundField DataField="AuthorId" HeaderText="AuthorId:" SortExpression="AuthorId" Visible="False" >
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
                    
            <asp:TemplateField>
                <ItemTemplate>
                    &nbsp;
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button Text="Update"  ID="UpdateButton" runat="server" CommandName="Edit" CssClass="buttonLarge" />
                    <asp:Button Text="Cancel"  ID="CancelButton" runat="server" CommandName="Cancel" CausesValidation="false" CssClass="buttonLarge" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:Button Text="Insert"  ID="InsertButton" runat="server" CommandName="Insert" CssClass="buttonLarge" />
                    <asp:Button Text="Cancel"  ID="CancelButton" runat="server" CommandName="Cancel" CausesValidation="false" CssClass="buttonLarge" />
                </InsertItemTemplate>
            </asp:TemplateField> 
        </Fields>
    </asp:DetailsView>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RegExLib.Data.Expression"
        InsertMethod="CreateExpression" SelectMethod="GetExpression" TypeName="RegExLib.Data.ExpressionManager"
        UpdateMethod="UpdateExpression">
        <SelectParameters>
			<asp:QueryStringParameter DefaultValue="-1" Name="expressionId" QueryStringField="regexp_id" Type="Int32" />
			<asp:Parameter DefaultValue="true" Name="forceRefresh" Type="Boolean" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter Name="pattern" Type="String" />
            <asp:Parameter Name="matchingText" Type="String" />
            <asp:Parameter Name="nonMatchingText" Type="String" />
            <asp:Parameter Name="enabled" Type="Boolean" />
            <asp:Parameter Name="source" Type="String" />
            <asp:Parameter Name="description" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>

</asp:Content> -->

