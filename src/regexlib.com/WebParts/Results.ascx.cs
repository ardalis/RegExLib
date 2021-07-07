using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RegExLib;
using System.Text.RegularExpressions;


public partial class Results : System.Web.UI.UserControl, IWebPart, IWebActionable {


    private const string IMG_FOLDER = "Images/Generic Folder 16.jpg";
    private const string IMG_TEXT = "Images/Clipping Text 16.jpg";

    public enum ResultsMode {
        None,
        TreeView,
        Grid,
    }

    
    private IInputSettings _userInputData;
    [ConnectionConsumer("User Input", "inputConnectionPoint")]
    public void SetOptions(IInputSettings provider) {
        this._userInputData = provider;
    }


    private IModeSettings _modeData;
    [ConnectionConsumer("Mode Data", "modeConnectionPoint")]
    public void SetOptions(IModeSettings provider) {
        this._modeData = provider;
    }


    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);

        this.lblNoResults.Visible = false;
        this.pnlClient.Visible = false;
        this.pnlGrid.Visible = false;
        this.pnlTreeView.Visible = false;

        if (_modeData != null) {
            if (_modeData.RunatMode == RunatMode.Server) {
                if (_userInputData != null && !string.IsNullOrEmpty(_userInputData.Pattern)) {
                    try {
                        DisplayResults(this._modeData.Options, this._userInputData.Pattern, this._userInputData.Source);
                    } catch (ApplicationException ex) {
                        this.lblNoResults.Text = ex.Message;
                    }
                }
            } else {
                this.pnlClient.Visible = true;
            }
        }
    }


    void DisplayResults( RegexOptions opts, string pattern, string source ) {

		try
		{

			Regex re = new Regex( pattern, opts );

			//AsynchronousRegex.AsynchronousRegexResult handle = (AsynchronousRegex.AsynchronousRegexResult) AsynchronousRegex.BeginInvoke(re, source, null, null) ;
			//handle.AsyncWaitHandle.WaitOne(5000, false) ;

			MatchCollection mc = re.Matches( source );

			//if (handle.IsCompleted) {
			// if (handle.Result.Count > 0) {
			if ( mc.Count > 0 )
			{
				if ( this.DisplayMode == ResultsMode.Grid )
				{
					this.pnlGrid.Visible = true;
					DisplayGrid( mc, re );
				}
				else
				{
					this.pnlTreeView.Visible = true;
					DisplayTree( mc, re );
				}
			}
			else
			{
				lblNoResults.Visible = true;
			}
			// handle.Cancel();
			// throw new ApplicationException("Pattern " + pattern + " timed out on the following input string: \n" + source);
			// }

		}
		catch( Exception ex )
		{
			lblProblemWithRegex.Text += "<br/><b>" + ex.Message + "</b>";
			lblProblemWithRegex.Visible = true;
		}
    }


    void DisplayGrid(MatchCollection mc, Regex re) {
        int rows = mc.Count;
        int cols = mc[0].Groups.Count;

        System.Web.UI.WebControls.Table tbl = new Table();
        tbl.Width = Unit.Percentage(100);
        tbl.BorderWidth = Unit.Pixel(1);
        tbl.CellPadding = 3;
        tbl.CellSpacing = 0;
        tbl.CssClass = "BrdrdTable";

        TableRow tr;
        TableHeaderCell th;

        tr = new TableRow();
        tr.CssClass = "TitleRow";
        th = new TableHeaderCell();
        th.CssClass = "NrmlTD";
        th.Text = "Match";
        th.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(th);


        for (int i = 0; i < cols - 1; i++) {
            th = new TableHeaderCell();
            th.Text = "$" + (1 + i);
            th.CssClass = "NrmlTD";
            th.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(th);
        }

        tbl.Rows.Add(tr);

        foreach (Match m in mc) {
            tr = new TableRow();
            tr.CssClass = (tbl.Rows.Count % 2 == 0) ? "StandardRow" : "HighlightRow";
            for (int j = 0; j < cols; j++) {
                TableCell td = new TableCell();
                td.Text = Server.HtmlEncode(m.Groups[j].Value);
                td.Attributes.Add("onclick", "HighlightMatch('" + m.Groups[j].Index + "', '" + m.Groups[j].Length + "') ;");
                td.CssClass = "NrmlTD";
                td.Wrap = false;
                tr.Cells.Add(td);
            }

            tbl.Rows.Add(tr);
        }
        this.pnlGrid.Controls.Add(tbl);
    }


    private void DisplayTree(MatchCollection mc, Regex re) {
        TreeView tvw = new TreeView();
        TreeNode node;
        
        foreach (Match m in mc) {
            node = new TreeNode();
            node.Text = Server.HtmlEncode(m.Value);
            node.Expanded = true;
            node.ImageUrl = IMG_FOLDER;
            node.Value = "foo";
            node.ToolTip = m.Index.ToString() + "_" + m.Length.ToString();

            tvw.Nodes.Add(node);

            if (m.Groups.Count > 1) {
                for (int grpCount = 1; grpCount < m.Groups.Count; grpCount++) {
                    Group g = m.Groups[grpCount];
                    TreeNode grpNode = new TreeNode();
                    grpNode.Text = Server.HtmlEncode(g.Value);
                    grpNode.ImageUrl = IMG_TEXT;
                    grpNode.ToolTip = g.Index.ToString() + "_" + g.Length.ToString();

                    node.Value = "foo";
                    node.ChildNodes.Add(grpNode);

                    if (m.Groups[grpCount].Captures.Count > 1) {
                        for (int capCount = 0; capCount < m.Groups[grpCount].Captures.Count; capCount++) {
                            Capture c = m.Groups[grpCount].Captures[capCount];
                            TreeNode capNode = new TreeNode();
                            capNode.Text = Server.HtmlEncode(c.Value);
                            capNode.ImageUrl = IMG_TEXT;
                            capNode.ToolTip = c.Index.ToString() + "_" + c.Length.ToString();
                            grpNode.ChildNodes.Add(capNode);
                        }
                    }
                }

            }
        }

        EmitTreeScript(tvw);
        this.pnlTreeView.Controls.Add(tvw);
    }


    private void EmitTreeScript(TreeView tvw) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("<script language=JavaScript>");
        sb.Append(tvw.ClientID + ".onselectedindexchange = function() {" + Environment.NewLine);
        sb.Append("    var node = " + tvw.ClientID + ".getTreeNode(" + tvw.ClientID + ".selectedNodeIndex);" + Environment.NewLine);
        sb.Append("    if( node.getAttribute(\"NodeData\") != null && node.getAttribute(\"NodeData\") != \"undefined\" ) {" + Environment.NewLine);
        sb.Append("        var s = node.getAttribute(\"NodeData\").split('_') ;" + Environment.NewLine);
        sb.Append("        if( s.length == 2 ) " + Environment.NewLine);
        sb.Append("        {" + Environment.NewLine);
        sb.Append("            HighlightMatch( s[0], s[1] ) ;" + Environment.NewLine);
        sb.Append("        }" + Environment.NewLine);
        sb.Append("    }" + Environment.NewLine);
        sb.Append("}" + Environment.NewLine);
        sb.Append("</script>");

        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("TreeScript"))
            this.Page.ClientScript.RegisterStartupScript(typeof(string), "TreeScript", sb.ToString());
    }


    #region IWebPart Members

    public string CatalogIconImageUrl {
        get { return string.Empty; }
        set { ; }
    }

    public string Description {
        get { return string.Empty; }
        set { ; }
    }

    public string Subtitle {
        get { return string.Empty; }
        set { ; }
    }

    public string Title {
        get { return "Results"; }
        set { ; }
    }

    public string TitleIconImageUrl {
        get { return string.Empty; }
        set { ; }
    }

    public string TitleUrl {
        get { return string.Empty; }
        set { ; }
    }

    #endregion

    #region IWebActionable Members

    public WebPartVerbCollection Verbs {
        get {
            
            ArrayList verbs = new ArrayList();
            
            if (_modeData != null) {

                if (_modeData.RunatMode == RunatMode.Server) {
                    WebPartVerb v1 = new WebPartVerb("Grid", new WebPartEventHandler(Toggle));
                    v1.Text = "Grid Output Mode";
                    v1.Checked = (this.DisplayMode == ResultsMode.Grid);
                    verbs.Add(v1);

                    WebPartVerb v2 = new WebPartVerb("Tree", new WebPartEventHandler(Toggle));
                    v2.Text = "Tree Output Mode";
                    v2.Checked = (this.DisplayMode == ResultsMode.TreeView);
                    verbs.Add(v2);
                }
            }

            return new WebPartVerbCollection((WebPartVerb[])verbs.ToArray(typeof(WebPartVerb)));
        }
    }


    //protected void Toggle(object sender, WebPartEventArgs e) {
    //    WebPartVerb verb = sender as WebPartVerb;
    //    if (verb != null) {
    //        if (verb.ID == "Grid") {
    //            this.DisplayMode = ResultsMode.Grid;
    //        } else {
    //            this.DisplayMode = ResultsMode.TreeView;
    //        }
    //    }
    //}

    public void Toggle(object sender, WebPartEventArgs e) {
        object foo = "bar";
    }


    // TODO: Replace with Personalization
    private ResultsMode _mode = ResultsMode.Grid;
    [Personalizable(PersonalizationScope.User)]
    [WebBrowsable]
    public ResultsMode DisplayMode {
        get { return _mode; }
        set { _mode = value; }
    }

    #endregion
}
