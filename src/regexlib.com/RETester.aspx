<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.RETester" Codebehind="RETester.aspx.cs" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>
<%@ Register Src="~/WebParts/ConfigurationOptions.ascx" TagName="ConfigurationOptions" TagPrefix="uc1" %>
<%@ Register Src="~/WebParts/UserInput.ascx" TagName="UserInput" TagPrefix="uc2" %>
<%@ Register Src="~/WebParts/Results.ascx" TagName="Results" TagPrefix="uc3" %>
<%@ Register Src="~/WebParts/RectangleAd.ascx" TagName="RectangleAd" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="OptionsHeaderPanel" runat="server" CssClass="commonContainerHeader" Height="16px">
        <div style="float: right; vertical-align: middle;">
            <asp:Image ID="ExpandImage" runat="server" AlternateText="^" ImageUrl="~/Images/collapse.jpg" Visible="false" />
        </div>
        <h2 style="float: left; vertical-align: middle;">Test Your Regular Expressions</h2>
    </asp:Panel>
    <asp:Panel ID="OptionsBodyPanel" runat="server" CssClass="commonContainer advancedSearchContainer" style="overflow: hidden;">
        <div class="testerCriteria">
            <h4>Select Regex Engine</h4>
            <asp:UpdatePanel ID="OptionsUpdatePanel" runat="server">
                <ContentTemplate>
                    <p><asp:DropDownList ID="EngineDropDownList" runat="server" CssClass="formStyleDropDown" AutoPostBack="true" OnSelectedIndexChanged="EngineDropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text=".NET Engine" Selected="True" />
                        <asp:ListItem Value="2" Text="Client-side Engine" />
                        <asp:ListItem Value="3" Text="Silverlight Tester" />
                    </asp:DropDownList></p>
                    
                    <a href="RESilverlight.aspx">New Silverlight Tester</a>
                    <h4>Current Options</h4>
                    <asp:Panel ID="DotNetEngineOptionsPanel" runat="server">
                        <asp:CheckBox ID="SinglelineCheckBox" runat="server" Text="Singleline" />
                        <a href="javascript:void(0);" onclick="overlib('Changes the meaning of dot (.) so that it matches every character (instead of every character except \\n).',
                              CAPTION, 'Singleline (?s), (?-s)',
                              STICKY, STATUS, 'HELP: Singleline', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '200'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="SinglineHelpImage" runat="server" />
                        </a>
                        <br /><hr />
                        
                        <asp:CheckBox ID="CaseInsensitiveCheckBox" runat="server" Text="Case Insensitive" Checked="true" />
                        <a href="javascript:void(0);" onclick="overlib('Specifies case-insensitive matching.',
                              CAPTION, 'Case Insensitivity (?i), (?-i)',
                              STICKY, STATUS, 'HELP: Singleline', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '225'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="CaseInsensitiveHelpImage" runat="server" />
                        </a>
                        <br /><hr />
                        
                        <asp:CheckBox ID="MultilineCheckBox" runat="server" Text="Multiline" Checked="true" />
                        <a href="javascript:void(0);" onclick="overlib('Changes the meaning of ^ and $ so that they match at the beginning and end of each line and not just at the beginning and end of the entire string.',
                              CAPTION, 'Multiline (?m), (?-m)',
                              STICKY, STATUS, 'HELP: Singleline', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '200'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="MultilineHelpImage" runat="server" />
                        </a>
                        <br /><hr />
                        
                        <asp:CheckBox ID="IgnoreWhitespaceCheckBox" runat="server" Text="Ignore Whitespace in Expression" />
                        <a href="javascript:void(0);" onclick="overlib('Eliminates unescaped whitespace from the pattern and enables comments marked with #.',
                              CAPTION, 'IgnorePatternWhitespace (?x), (?-x)',
                              STICKY, STATUS, 'HELP: Singleline', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '200'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="IgnoreWhitespaceHelpImage" runat="server" />
                        </a>
                        <br /><hr />
                        
                        <asp:CheckBox ID="ExplicitCaptureCheckBox" runat="server" Text="Explicit Capture" />
                        <a href="javascript:void(0);" onclick="overlib('Specifies that only named or numbered groups are captured. This allows unnamed parenthesis to act as noncapturing groups.',
                              CAPTION, 'ExplicitCapture (?n), (?-n)',
                              STICKY, STATUS, '', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '220'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="ExplicitCaptureHelpImage" runat="server" />
                        </a>

                    </asp:Panel>
                    <asp:Panel ID="ClientSideEnginePanel" runat="server" Visible="false">
                        <asp:CheckBox ID="CaseInsensitiveCheckBox2" runat="server" Text="Case Insensitive" Checked="true" />
                        <a href="javascript:void(0);" onclick="overlib('Specifies case-insensitive matching.',
                              CAPTION, 'Case Insensitivity (?i), (?-i)',
                              STICKY, STATUS, 'HELP: Singleline', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '200'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="CaseInsensitiveHelpImage2" runat="server" />
                        </a>
                        <br /><hr />
                        
                        <asp:CheckBox ID="IgnoreWhitespaceCheckBox2" runat="server" Text="Ignore Whitespace in Expression" />
                        <a href="javascript:void(0);" onclick="overlib('Eliminates unescaped whitespace from the pattern and enables comments marked with #.',
                              CAPTION, 'IgnorePatternWhitespace (?x), (?-x)',
                              STICKY, STATUS, 'HELP: Singleline', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '200'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="IgnoreWhitespaceHelpImage2" runat="server" />
                        </a>
                        <br /><hr />
                        
                        <asp:CheckBox ID="GlobalCheckBox" runat="server" Text="Global" />
                        <a href="javascript:void(0);" onclick="overlib('Specifies that all matches be returned instead of just the first match.',
                              CAPTION, 'Global',
                              STICKY, STATUS, 'HELP: Singleline', 
                              FGCOLOR, 'white', BGCOLOR, '#2A96FF', CGCOLOR, '#2A96FF', TEXTPADDING, '5', 
                              CLOSETEXT,closeimg, CLOSECLICK, DRAGGABLE, CAPTIONSIZE, '11px', TEXTCOLOR, 'black',
                              TEXTFONT, 'Tahoma, Arial, sans-serif', TEXTSIZE, '10px',
                              SHADOW, SHADOWX, '4', SHADOWY, '4', 
                              FILTER, FADEIN, '25', FADEOUT, '25', FILTEROPACITY, '100',
                              WIDTH, '200'); return false;" onmouseout="nd();">
                            <asp:Image SkinID="HelpImage" ID="GlobalHelpImage" runat="server" />
                        </a>
                    </asp:Panel>
                </ContentTemplate>
           </asp:UpdatePanel>
        </div>
        <div class="largeRectangleAd">
            <div class="lqm_ad" lqm_publisher="lqm.regexlib.site" lqm_zone="ron" lqm_format="336x280"></div>
        </div>
    </asp:Panel>

    <div class="commonContainerHeader">
        <h2>Regex Input</h2>
    </div>
    <div class="commonContainer">
        <h4>External Data Source</h4>
        <table border="0" cellpadding="0" cellspacing="0" style="position: relative;">
            <tr>
                <td>
                    <table class="searchResultsTable" border="0" cellpadding="0" cellspacing="0" style="position: relative; width: auto;">
                        <tr>
                            <td>
                                URL<br />
                                <asp:textbox Id="URLTextbox" Runat="server" Columns="50" CssClass="formField"  />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                File<br />
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="formField" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="bottom">
                    <asp:Button ID="LoadExternalDataButton" runat="server" Text="Load Data Source" 
                        CssClass="buttonLarge" style="margin-left: 6px" OnClick="LoadExternalDataButton_Click" />
                </td>
            </tr>
        </table>
        <p>
            Source<br />
            <asp:textbox textmode="MultiLine" CssClass="formField" Height="80px" Width="97%" 
                Id="SourceTextBox" Runat="server" />
            <br />
            Regular Expression<br />
            <asp:textbox textmode="MultiLine" CssClass="formField" Height="80px" Width="97%" 
                Id="ExpressionTextbox" Runat="server" />
        </p>
        <asp:UpdatePanel ID="ClientEngineSelectUpdatePanel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="ClientEngineSelectPlaceholder" runat="server" Visible="false">
                    <p>
                        Engine 
                        <asp:DropDownList ID="SelectedEngineDropDownList" runat="server" class="formStyleDropDown">
                            <asp:ListItem Value="1" Text="VBScript" Selected="True" />
                            <asp:ListItem Value="2" Text="JavaScript" />
                        </asp:DropDownList>
                    </p>
                </asp:PlaceHolder>
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" CssClass="buttonLarge" style="margin-top: 6px;" />
                <asp:PlaceHolder ID="ClientButtonPlaceholder" runat="server" Visible="false">
                    <input id="ExecuteClientButton" onclick="execute(this.form);" class="buttonLarge" style="margin-top: 6px;" type="button" value="Execute" />
                </asp:PlaceHolder>
        </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="SubmitButton" />
                <asp:AsyncPostBackTrigger ControlID="EngineDropDownList" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <a name="results" id="results"></a>
    <div class="commonContainerHeader">
        <h2>Results</h2>
    </div>
    <div class="commonContainer">
        <asp:UpdatePanel ID="ResultsUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Panel ID="ResultsPanel" runat="server" Visible="false" CssClass="overflowFixDiv" style="min-height: 200px;" />

            <asp:Panel ID="ClientResultsPanel" runat="server" Visible="false" CssClass="overflowFixDiv">
                <div id="clientresults" class="overflowFixDiv" style="min-height: 200px;" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="EngineDropDownList" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>
        <asp:Label ID="NoResultsLabel" runat="server" Text="No Results" Visible="false" CssClass="overflowFixDiv" />
        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="overflowFixDiv" />
        
    </div>
    
<script type="text/javascript" language="javascript">
<!--
    function execute(frm)
    {
        if (document.getElementById("ctl00_ContentPlaceHolder1_SelectedEngineDropDownList").value == "2")
        {
            executeJS(frm);
        }
        else
        {
            executeVBS(frm);
        }        
    }
    
    function executeJS(frm)
    {
        var pattern = document.getElementById("ctl00_ContentPlaceHolder1_ExpressionTextbox").value;
        var ignoreCase = document.getElementById("ctl00_ContentPlaceHolder1_CaseInsensitiveCheckBox2").value == "on";
        var ignoreWS = document.getElementById("ctl00_ContentPlaceHolder1_IgnoreWhitespaceCheckBox2").value == "on";
        var isGlobal = document.getElementById("ctl00_ContentPlaceHolder1_GlobalCheckBox").value == "on";
        
        //var t = frm.test.value;
        var t = document.getElementById("ctl00_ContentPlaceHolder1_SourceTextBox").value;
        var s = "";
        var x = 'code';
        var ao = document.getElementsByName('rettype');
        for (i=0; i < ao.length; i++)
        {
        var o = ao(i);
        if ( o.checked == true ){ var x = o.id; }
        }
        var rx = new RegExp( pattern, "g" );
        if ((rx.test(t)) && (pattern!="")){
        var r  = t.match(rx);
        for (i = 0; i < r.length; i++){
                if (x == 'text'){
              s += "\n" + r[i];
                }else{
            s += "<br />" + r[i];
                }
        }
        }
        if (x == 'text')
        {
          document.getElementById("clientresults").innerText = s;
        }
        else
        {
          document.getElementById("clientresults").innerHTML = s;
        }
    }

-->
</script>
<script type="text/vbscript" language="vbscript">
<!--

	Function getVBSEngineInfo()
		Dim engineInfo
		engineInfo = ScriptEngineMajorVersion & "." & ScriptEngineMinorVersion & "." & ScriptEngineBuildVersion
		getVBSEngineInfo = engineInfo
	End Function
	
	Sub executeVBS(frm)
		Dim msg
		Dim table
		Dim record()
		Dim source
		Dim pattern
		Dim blnGlobal
		Dim ignoreCase
		Dim re
		Dim colMatches
		Dim objMatch
		Dim colSubMatches
		Dim i
		Dim j
		Dim index : index = 0
		Dim intSubMatchCount
		
		source = document.getElementById("ctl00_ContentPlaceHolder1_SourceTextBox").value 
		pattern = document.getElementById("ctl00_ContentPlaceHolder1_ExpressionTextbox").value 
		blnGlobal = document.getElementById("ctl00_ContentPlaceHolder1_GlobalCheckBox").value = "on" 
		ignoreCase = document.getElementById("ctl00_ContentPlaceHolder1_CaseInsensitiveCheckBox2").value = "on"  	
		
		If Len(source) = 0 Then
			msgbox "Please provide the source text to be searched on"
			Exit Sub
		End If
		
		If Len(pattern) = 0 Then
			msgbox "Please define the regular expression pattern to be used for the search"
			Exit Sub
		End If
		
		Set re = New RegExp
		With re
		.Pattern = pattern
		.Global = blnGlobal
		.IgnoreCase = ignoreCase
		End With
		
		'on error resume next
		
		Set colMatches = re.Execute(source)
		
		If Err.Number <> 0 Then
			msgbox "The regular expression is invalid"
			Err.Clear
			Exit Sub
		End if

		For i = 0 to colMatches.Count - 1
			Set objMatch = colMatches.Item(i)
			
			Redim record(0)
			record(0) = objMatch.Value
			
			intSubMatchCount = objMatch.SubMatches.Count
			If objMatch.SubMatches.Count > 0 Then Redim Preserve record(intSubMatchCount)

			For j = 1 to intSubMatchCount 
				record(j) = objMatch.SubMatches(j - 1)
			Next
			
			If IsArray(table) Then
				Redim Preserve table(index)
			Else
				table = Array(0)
			End If
			
			table(index) = record
			index = index + 1
		Next
		
		If IsArray(table) Then
			writeTableVBS table, frm
		Else
			document.getElementById("clientresults").innerHTML = "<font class=nores>No Matches</font>"
		End If
	End Sub
	
	
	Sub writeTableVBS(object, frm)
		Dim out : out = ""
		Dim rows : rows = 0
		Dim cols : cols = 0
		Dim i, j
		Dim temp
		Dim regx1, regx2
		Dim blnEscapeWS
	
		blnEscapeWS = document.getElementById("ctl00_ContentPlaceHolder1_IgnoreWhitespaceCheckBox2").value = "on" 'SelectedIgnoreWS( frm ) 'document.frmMain.escapeWS.checked
	
		rows = Ubound(object) + 1
		cols = Ubound(object(0)) + 1
		
		out = out & "<table border=0 cellpadding=2 cellspacing=1 bgcolor=#dcdcdc>" & vbCrLf
		out = out & "<tr>" & vbCrLf
		out = out & "<th>Match</th>" & vbCrLf
		
		For i = 0 to cols-2
			out = out & "<th> $" & (1+i) & " </th>" & vbCrLf
		Next
		
		out = out & "</tr>" & vbCrLf
		out = out & "<tr>" & vbCrLf
		
		For i = 0 to rows - 1
			For j = 0 to cols - 1
				temp = object(i)(j)
				
				temp = replace(temp, ">", "&gt;")
				temp = replace(temp, "<", "&lt;")
				
				If blnEscapeWS Then
					temp = replace(temp, vbCR, "<font class='ws'>\r</font>")
					temp = replace(temp, vbCRLF, "<font class='ws'>\n</font>")
					temp = replace(temp, vbTAB, "<font class='ws'>\t</font>")
				End If
				
				out = out & "<td class=result nowrap>" + temp + "&nbsp;</td>" & vbCrLf
			Next
			out = out & "</tr>" & vbCrLf
		Next

		out = out & "</table>"

		document.getElementById("clientresults").innerHTML = out
		
	End Sub

//-->
</script>
    
</asp:Content>
