<%@ Control Language="C#" AutoEventWireup="true" Inherits="ConfigurationOptions" Codebehind="ConfigurationOptions.ascx.cs" %>
<asp:Panel ID="pnlVisible" runat="server">
    <script language="javascript" type="text/javascript">
        
        var gCrntTextPnl ;
        
        function ShowOptionInfo( optKey ) {
	        if( gCrntTextPnl != null ) {
		        gCrntTextPnl.className = "tt_hText" ;
	        }
        	
	        gCrntTextPnl = document.getElementById("mode" + optKey)
	        if( gCrntTextPnl != null )
		        gCrntTextPnl.className = "" ;	
        }
    </script>

    <table width="100%" cellpadding="2" border="0">
		<tr class="tt_middle">
			<td class="tt_left">
				<ul class="tt_options">
					<asp:placeholder id="phServerModeOptions" runat="server" visible="True">
						<LI>
							<asp:checkbox id="cbSe_Singleline" runat="server" checked="False" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('sl')" href="javascript:void(0);">Singleline</A></LI>
						<LI>
							<asp:checkbox id="cbSe_CaseInsensitive" runat="server" checked="True" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('ci')" href="javascript:void(0);">Case 
								Insensitive</A></LI>
						<LI>
							<asp:checkbox id="cbSe_Multiline" runat="server" checked="True" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('ml')" href="javascript:void(0);">Multiline</A></LI>
						<LI>
							<asp:checkbox id="cbSe_IgnoreWhiteSpace" runat="server" checked="False" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('iw')" href="javascript:void(0);">Ignore 
								Pattern Whitespace</A></LI>
						<LI class="tt_none">
							<asp:checkbox id="cbSe_ExplicitCapture" runat="server" checked="False" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('ex')" href="javascript:void(0);">Explicit 
								Capture</A></LI>
					</asp:placeholder>
					<asp:placeholder id="phClientModeOptions" runat="server" visible="False">
						<LI>
							<asp:checkbox id="cbCl_CaseInsensitive" runat="server" checked="True" name="ret_ignoreCase" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('ci')" href="javascript:void(0);">Case 
								Insensitive</A></LI>
						<LI>
							<asp:checkbox id="cbCl_IgnoreWhiteSpace" runat="server" name="ret_escapeWS" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('iw')" href="javascript:void(0);">Ignore 
								Whitespace</A></LI>
						<LI class="tt_none">
							<asp:checkbox id="cbCl_Global" runat="server" checked="True" name="ret_global" AutoPostBack="true"></asp:checkbox><A onclick="ShowOptionInfo('gl')" href="javascript:void(0);">Global</A></LI>
					</asp:placeholder>
					
				</ul>
			</td>
			<td class="tt_main" rowspan="3">
				<div class="tt_innerMiddle">
					<div class="tt_hText" id="modesl">
						<h1>Singleline (?s), (?-s)</h1>
						<p>Changes the meaning of dot (.) so that it matches every character (instead of 
							every character except \n).</p>
					</div>
					<div class="tt_hText" id="modeci">
						<h1>Case Insensitivity (?i), (?-i)</h1>
						<p>Specifies case-insensitive matching.</p>
					</div>
					<div class="tt_hText" id="modeml">
						<h1>Multiline (?m), (?-m)</h1>
						<p>Changes the meaning of ^ and $ so that they match at the beginning and end of 
							each line and not just at the beginning and end of the entire string.
						</p>
					</div>
					<div class="tt_hText" id="modegl">
						<h1>Global</h1>
						<p>Specifies that all matches be returned instead of just the first match.
						</p>
					</div>
					<div class="tt_hText" id="modeiw">
						<h1>IgnorePatternWhitespace (?x), (?-x)</h1>
						<p>Eliminates unescaped whitespace from the pattern and enables comments marked 
							with #.
						</p>
					</div>
					<div class="tt_hText" id="modeex">
						<h1>ExplicitCapture (?n), (?-n)</h1>
						<p>Specifies that only named or numbered groups are captured. This allows unnamed 
							parenthesis to act as noncapturing groups.
						</p>
					</div>
					<div class="tt_hText" id="modeec">
						<h1>ECMAScript</h1>
						<p>Enables EMCAScript-compliant behavior for the expression. This flag can be used 
							only in conjunction with the IgnoreCase and Multiline flags.
						</p>
					</div>
					<div class="tt_hText" id="moderl">
						<h1>RightToLeft</h1>
						<p>Specifies that the search will be from right to left instead of the default left 
							to right.
						</p>
					</div>
					<div class="tt_hText" id="modeServer">
						<h1>.NET Server-side testing</h1>
						<p>Test your regular expressions against the .NET Regex engine. Tests are performed 
							server side and the results can be rendered optionally in either a grid or 
							treeview.
						</p>
						<p>Displaying results in a grid provides a good overview of which groups are 
							participating in matches while the Treeview allows you to drill-in and see the 
							results of actual Captures.
						</p>
					</div>
					<div class="tt_hText" id="modeClient">
						<h1>Client-side testing</h1>
						<p>Test your regular expressions against either the VBScript or Javascript Regex 
							engines. Tests are performed client-side and the results are rendered in a 
							grid.
						</p>
						<p>The grid provides a good overview of which groups are participating in matches.
						</p>
						<p>Much of this tool was inspired by Moshe Solomon'S great client-side tester which can be 
							found on his personal webpage: <a href="http://www.geocities.com/udeleng/regex.htm">
								http://www.geocities.com/udeleng/regex.htm</a>.
						</p>
					</div>
				</div>
			</td>
			<%-- <td class="tt_right"></td>--%>
		</tr>
	</table>
</asp:Panel>

