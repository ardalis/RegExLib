<%@ Page Language="C#" AutoEventWireup="true" Inherits="RegExLib.Web.CheatSheet" Codebehind="CheatSheet.aspx.cs" %>
<%@ Import Namespace="RegExLib" %>
<html>
	<head runat="server">
		<title>RegExLib.com Regular Expression Cheat Sheet (.NET Framework)</title>
</head>
	<body class="whiteBg">
	<table class="pageTop outerTable">
		<tr>
			<td class="imageCell">
				<a href="<%= Globals.BaseUrl %>"><img src="App_Themes/Green/Images/RegExLib_logo.png" alt="RegExLib - Regular Expression Library"></a></td>
			<td style="vertical-align: middle;"><h1>RegExLib.com Regular Expression Cheat Sheet (.NET)</h1></td>
		</tr>
	</table>
	<table class="outerTable">
		<tr>
			<td>
			<table border="1" class="innerTable">
				<tr class="heading"><th scope="colgroup" colspan="2"><h2>Metacharacters Defined</h2></th></tr>
				<tr><th scope="col">MChar</th><th scope="col">Definition</th></tr>
				<tr><td>^</td><td>Start of a string.</td></tr>
				<tr><td>$</td><td>End of a string.</td></tr>
				<tr><td>.</td><td>Any character (except \n newline)</td></tr>
				<tr><td>|</td><td>Alternation.</td></tr>
				<tr><td>{...}</td><td>Explicit quantifier notation.</td></tr>
				<tr><td>[...]</td><td>Explicit set of characters to match.</td></tr>
				<tr><td>(...)</td><td>Logical grouping of part of an expression.</td></tr>
				<tr><td>*</td><td>0 or more of previous expression.</td></tr>
				<tr><td>+</td><td>1 or more of previous expression.</td></tr>
				<tr><td>?</td><td>0 or 1 of previous expression; also forces minimal matching when an expression might match several strings within a search string.</td></tr>
				<tr><td>\</td><td>Preceding one of the above, it makes it a literal instead of a special character.  Preceding a special matching character, see below.</td></tr>
			</table>
			</td>
			<td width="26%">
			<table border="1" class="innerTable" ID="Table1">
				<tr class="heading">
			  <th scope="colgroup" colspan="2"><h2>Metacharacter Examples</h2></th></tr>
				<tr><th scope="col">Pattern</th><th scope="col">Sample Matches</th></tr>
				<tr><td>^abc</td><td>abc, abcdefg, abc123, ...</td></tr>
				<tr><td>abc$</td><td>abc, endsinabc, 123abc, ...</td></tr>
				<tr><td>a.c</td><td>abc, aac, acc, adc, aec, ...</td></tr>
				<tr><td>bill|ted</td><td>ted, bill</td></tr>
				<tr><td>ab{2}c</td><td>abbc</td></tr>
				<tr><td>a[bB]c</td><td>abc, aBc</td></tr>
				<tr><td>(abc){2}</td><td>abcabc</td></tr>
				<tr><td>ab*c</td><td>ac, abc, abbc, abbbc, ...</td></tr>
				<tr><td>ab+c</td><td>abc, abbc, abbbc, ...</td></tr>
				<tr><td>ab?c</td><td>ac, abc</td></tr>
				<tr><td>a\sc</td><td>a c</td></tr>
			</table>			
			</td>
		</tr>
	</table>
	
	<table border="1" class="innerTable">
		<tr class="heading">
	  <th scope="colgroup" colspan="2"><h2>Character Escapes <a href="http://tinyurl.com/5wm3wl">http://tinyurl.com/5wm3wl</a></h2></th></tr>
		<tr><th scope="col">Escaped Char</th><th scope="col">Description</th></tr>
		<tr><td>ordinary characters</td><td>Characters other than . $ ^ { [ ( | ) ] } * + ? \ match themselves.</td></tr>
		<tr><td>\a</td><td>Matches a bell (alarm) \u0007.</td></tr>
		<tr><td>\b</td><td>Matches a backspace \u0008 if in a []; otherwise matches a word boundary (between \w and \W characters).</td></tr>
		<tr><td>\t</td><td>Matches a tab \u0009.</td></tr>
		<tr><td>\r</td><td>Matches a carriage return \u000D.</td></tr>
		<tr><td>\v</td><td>Matches a vertical tab \u000B.</td></tr>
		<tr><td>\f</td><td>Matches a form feed \u000C.</td></tr>
		<tr><td>\n</td><td>Matches a new line \u000A.</td></tr>
		<tr><td>\e</td><td>Matches an escape \u001B.</td></tr>
		<tr><td>\040</td><td>Matches an ASCII character as octal (up to three digits); numbers with no leading zero are backreferences if they have only one digit or if they correspond to a capturing group number. (For more information, see Backreferences.) For example, the character \040 represents a space.</td></tr>
		<tr>
    <td>\x20</td><td>Matches an ASCII character using hexadecimal representation (exactly two digits).</td></tr>
		<tr><td>\cC</td><td>Matches an ASCII control character; for example \cC is control-C.</td></tr>
		<tr><td>\u0020</td><td>Matches a Unicode character using a hexadecimal representation (exactly four digits).</td></tr>
		<tr><td>\*</td><td>When followed by a character that is not recognized as an escaped character, matches that character. For example, <b>\*</b> is the same as <b>\x2A</b>.</td></tr>
	</table>
	
	<table border="1" class="innerTable">
		<tr class="heading">
	  <th scope="colgroup" colspan="2"><h2>Character Classes <a href="http://tinyurl.com/5ck4ll">http://tinyurl.com/5ck4ll</a></h2></th></tr>
		<tr><th scope="col">Char Class</th><th scope="col">Description</th></tr>
		<tr><td>.</td><td>Matches any character except \n. If modified by the Singleline option, a period character matches any character. For more information, see Regular Expression Options.</td></tr>
		<tr><td>[aeiou]</td><td>Matches any single character included in the specified set of characters.</td></tr>
		<tr><td>[^aeiou]</td><td>Matches any single character not in the specified set of characters.</td></tr>
		<tr><td>[0-9a-fA-F]</td><td>Use of a hyphen (–) allows specification of contiguous character ranges.</td></tr>
		<tr><td>\p{name}</td><td>Matches any character in the named character class specified by {name}. Supported names are Unicode groups and block ranges. For example, Ll, Nd, Z, IsGreek, IsBoxDrawing.</td></tr>
		<tr><td>\P{name}</td><td>Matches text not included in groups and block ranges specified in {name}.</td></tr>
		<tr><td>\w</td><td>Matches any word character. Equivalent to the Unicode character categories
[\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Pc}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \w is equivalent to [a-zA-Z_0-9].</td></tr>
		<tr><td>\W</td><td>Matches any nonword character. Equivalent to the Unicode categories [^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Pc}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \W is equivalent to [^a-zA-Z_0-9].</td></tr>
		<tr><td>\s</td><td>Matches any white-space character. Equivalent to the Unicode character categories [\f\n\r\t\v\x85\p{Z}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \s is equivalent to [ \f\n\r\t\v].</td></tr>
		<tr><td>\S</td><td>Matches any non-white-space character. Equivalent to the Unicode character categories [^\f\n\r\t\v\x85\p{Z}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \S is equivalent to [^ \f\n\r\t\v].</td></tr>
		<tr><td>\d</td><td>Matches any decimal digit. Equivalent to \p{Nd} for Unicode and [0-9] for non-Unicode, ECMAScript behavior.</td></tr>
		<tr><td>\D</td><td>Matches any nondigit. Equivalent to \P{Nd} for Unicode and [^0-9] for non-Unicode, ECMAScript behavior.</td></tr>
	</table>
	<script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
</script>
<script type="text/javascript">
_uacct = "UA-470225-2";
urchinTracker();
</script>
	</body>
</html>
