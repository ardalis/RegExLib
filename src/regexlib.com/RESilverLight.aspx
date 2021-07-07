<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RegExLib.Web.ErrorPage" Codebehind="RESilverlight.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="http://regexhero.net/Silverlight.js"></script>
<script type="text/javascript">
    function onSilverlightError(sender, args) {

        var appSource = "";
        if (sender != null && sender != 0) {
            appSource = sender.getHost().Source;
        }
        var errorType = args.ErrorType;
        var iErrorCode = args.ErrorCode;

        var errMsg = "Unhandled Error in Silverlight 2 Application " + appSource + "\n";

        errMsg += "Code: " + iErrorCode + "    \n";
        errMsg += "Category: " + errorType + "       \n";
        errMsg += "Message: " + args.ErrorMessage + "     \n";

        if (errorType == "ParserError") {
            errMsg += "File: " + args.xamlFile + "     \n";
            errMsg += "Line: " + args.lineNumber + "     \n";
            errMsg += "Position: " + args.charPosition + "     \n";
        }
        else if (errorType == "RuntimeError") {
            if (args.lineNumber != 0) {
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            errMsg += "MethodName: " + args.methodName + "     \n";
        }

        throw new Error(errMsg);
    }
</script>

<div class="commonContainerHeader"><h2>Silverlight Regular Expression Tester</h2></div>
<div class="commonContainer">
<div id="silverlightControlHost">
 <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="600">
  <param name="source" value="http://regexhero.net/ClientBin/RegexHero.xap"/>
  <param name="onerror" value="onSilverlightError" />
  <param name="background" value="white" />
  <param name="minRuntimeVersion" value="4.0.50524.0" />
  <param name="autoUpgrade" value="true" />
  <param name="initParams" value="partner=regexlib" />
  <a href="http://go.microsoft.com/fwlink/?LinkID=124807" style="text-decoration: none;">
   <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none"/>
  </a>
 </object>
 <iframe id="_sl_historyFrame" style="visibility:hidden;height:0;width:0;border:0px"></iframe>
</div>
</div>

<div class="regexHero">Silverlight regular expression tester provided by <a href="http://regexhero.net/">Regex Hero</a></div>

</asp:Content>