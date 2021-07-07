<%@ Control Language="C#" AutoEventWireup="true" Inherits="ClientTesterControls" Codebehind="ClientTesterControls.ascx.cs" %>
<b>Engine:</b>
<select name="ret_engine" runat="server" id="ret_engine" onchange="checkEngine(this.form)">
	<option value="JavaScript">JavaScript</option>
	<option value="VBScript" selected="selected">VBScript</option>
</select>
<input type="button" value=" Info " runat="server" onclick="showInfo(this.form)" id="Button1"
	name="Button1" /> 
	<p><input id="btnExecuteClient" onclick="execute(this.form)" type="button" value="Execute" /></p>
<div id="debugger"></div>
<!--<script type='text/javascript' language='Javascript' src="scripts/ClientTester.js"></script>-->
<script type="text/javascript" language="javascript">
<!--

//    function checkEngine(frm)
//    {
//    }
//    function showInfo(frm)
//    {
//    }
//    function execute(frm)
//    {
//        executeJS(frm);
//        executeVBS(frm);
//        
//    }
//    function executeJS(frm)
//    {
//        alert("here");
//        var myTest = "";
//        myTest = document.getElementById("ctl00$ContentPlaceHolder1$WebPartManager1$gwpUserInput1$UserInput1$txtPattern").value;
//        alert(myTest);

//        var pattern = document.getElementById("ctl00_ContentPlaceHolder1_WebPartManager1_gwpUserInput1_UserInput1_txtSource".value;
//        var ignoreCase = document.getElementById("ctl00_ContentPlaceHolder1_WebPartManager1_gwpConfigurationOptions1_ConfigurationOptions1_cbCl_CaseInsensitive").value;
//        var ignoreWS = document.getElementById("ctl00_ContentPlaceHolder1_WebPartManager1_gwpConfigurationOptions1_ConfigurationOptions1_cbCl_IgnoreWhiteSpace").value;
//        var isGlobal;
//        var t = frm.test.value;
//        var s = "";
//        var x = 'code';
//        var ao = document.getElementsByName('rettype');
//        for (i=0; i < ao.length; i++)
//        {
//        var o = ao(i);
//        if ( o.checked == true ){ var x = o.id; }
//        }
//        var rx = new RegExp( p, "g" );
//        if ((rx.test(t)) && (p!="")){
//        var r  = t.match(rx);
//        for (i = 0; i < r.length; i++){
//                if (x == 'text'){
//              s += "\n" + r[i];
//                }else{
//            s += "<br />" + r[i];
//                }
//        }
//        }
//        if (x == 'text')
//        {
//          document.getElementById("res").innerText = s;
//        }
//        else
//        {
//          document.getElementById("res").innerHTML = s;
//        }
//    }

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
		
		If (IsValidVersion()) Then
			source = SelectedSource( frm )
			pattern = SelectedRegex( frm )
			blnGlobal = SelectedGlobal( frm )
			ignoreCase = SelectedIgnoreCase( frm ) 
			
			If Len(source) = 0 Then
				msgbox "Please provide the source text to be searched on"
				Exit Sub
			End If
			
			If Len(pattern) = 0 Then
				msgbox "Please define the regular expression pattern to be used for the search"
				Exit Sub
			End If
			
			Set re = New RegExp
			re.Pattern = pattern
			re.Global = blnGlobal
			re.IgnoreCase = ignoreCase
			
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
				document.getElementById("resulttable").innerHTML = "<font class=nores>No Matches</font>"
			End If
			
		Else
			msg = "This application requires Windows Script Host 5.5 or Greater. " & vbcrlf
			msg = msg & "Your current version is " + ScriptEngineMajorVersion + "." + ScriptEngineMinorVersion + ". "
			msg = msg & "You can download an upgrade " & vbcrlf & "at http://msdn.microsoft.com/scripting/"
			msgbox msg
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
	
		blnEscapeWS = SelectedIgnoreWS( frm ) 'document.frmMain.escapeWS.checked
	
		rows = Ubound(object) + 1
		cols = Ubound(object(0)) + 1
		
		out = out & "<table width=100% border=0 cellpadding=2 cellspacing=1 bgcolor=#dcdcdc>" & vbCrLf
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

		document.getElementById("resulttable").innerHTML = out
		
	End Sub

//-->
</script>
