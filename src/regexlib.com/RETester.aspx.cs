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
using System.Net;
using System.Text.RegularExpressions;
using RegExLib.Data;
using RegExLib.Web;
using System.IO;
using System.Text;
using Ardalis.Framework;

namespace RegExLib.Web {

    public partial class RETester : BasePage
    {
        public bool IsServerSide
	    {
            get { return EngineDropDownList.SelectedValue == "1"; }
	    }

        public bool IsClientSide
        {
            get { return EngineDropDownList.SelectedValue == "2"; }
        }

        public RegexOptions Options 
        {
            get 
            {
                RegexOptions opts = RegexOptions.None;

                if (IsServerSide) 
                {
                    if (this.MultilineCheckBox.Checked) opts |= RegexOptions.Multiline;
                    if (this.IgnoreWhitespaceCheckBox.Checked) opts |= RegexOptions.IgnorePatternWhitespace;
                    if (this.CaseInsensitiveCheckBox.Checked) opts |= RegexOptions.IgnoreCase;
                    if (this.SinglelineCheckBox.Checked) opts |= RegexOptions.Singleline;
                    if (this.ExplicitCaptureCheckBox.Checked) opts |= RegexOptions.ExplicitCapture;
                } 
                else if (IsClientSide)
                {
                    if (this.CaseInsensitiveCheckBox2.Checked) opts |= RegexOptions.IgnoreCase;
                    if (this.IgnoreWhitespaceCheckBox2.Checked) opts |= RegexOptions.IgnorePatternWhitespace;
                    if (this.GlobalCheckBox.Checked) opts |= RegexOptions.Multiline;
                }
                return opts;
            }
        }

        private int _expressionId = 0;
        public int ExpressionId
        {
            get { return _expressionId; }
        }

        private Expression _myExpression;
        public Expression MyExpression
        {
            get
            {
                if (_myExpression == null)
                {
                    _myExpression = RegExLib.Data.ExpressionManager.GetExpression(ExpressionId);
                }
                return _myExpression;
            }
        }

        protected override void LoadParametersFromQueryString()
        {
            base.LoadParametersFromQueryString();
            if (!IsPostBack)
            {
                _expressionId = Parse.To<int>(this.Request["regexp_id"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (MyExpression != null)
                {
                    ExpressionTextbox.Text = MyExpression.Pattern;
                }

                HandleQueryStringEngineSelection();
            }
        }

        private void HandleQueryStringEngineSelection()
        {
            string engineSelection = Request.QueryString["engine"];

            if (string.Compare(engineSelection, "JavaScript", true) == 0)
            {
                SetUpJavaScriptEngine();
            }
        }

        protected void EngineDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsServerSide)
            {
                SetUpServerSide();
            }
            else if (IsClientSide)
            {
                SetUpClientSide();
            }
            else
            {
                SetupSilverlight();
            }
        }

        protected void SetupSilverlight()
        {
            Response.Redirect("RESilverlight.aspx");
        }

        protected void SetUpJavaScriptEngine()
        {
            SelectedEngineDropDownList.SelectedValue = "2";
            SetUpClientSide();
        }

        protected void SetUpClientSide()
        {
            EngineDropDownList.SelectedValue = "2";
            ResultsPanel.Visible = false;
            ClientResultsPanel.Visible = true;
            DotNetEngineOptionsPanel.Visible = false;
            ClientSideEnginePanel.Visible = true;
            ClientEngineSelectPlaceholder.Visible = true;
            ClientButtonPlaceholder.Visible = true;
            SubmitButton.Visible = false;
        }

        protected void SetUpServerSide()
        {
            ResultsPanel.Visible = true;
            ClientResultsPanel.Visible = false;
            DotNetEngineOptionsPanel.Visible = true;
            ClientSideEnginePanel.Visible = false;
            ClientEngineSelectPlaceholder.Visible = false;
            ClientButtonPlaceholder.Visible = false;
            SubmitButton.Visible = true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.NoResultsLabel.Visible = false;
            this.ResultsPanel.Visible = false;
            this.ErrorPanel.Visible = false;

            if (IsServerSide)
            {
                if (!string.IsNullOrEmpty(ExpressionTextbox.Text) && !string.IsNullOrEmpty(SourceTextBox.Text))
                {
                    try
                    {
                        DisplayResults(this.Options, ExpressionTextbox.Text, SourceTextBox.Text);
                    }
                    catch (ApplicationException ex)
                    {
                        this.ErrorPanel.Visible = true;
                        this.ErrorPanel.Controls.Add(new LiteralControl("<br/><b>" + ex.Message + "</b>"));
                    }
                }
            }
            else if (IsClientSide)
            {
                this.ClientResultsPanel.Visible = true;
            }
        }

        private void DisplayResults(RegexOptions opts, string pattern, string source)
        {
            try
            {
                Regex re = new Regex(pattern, opts);

                MatchCollection mc = re.Matches(source);

                if (mc.Count > 0)
                {
                    this.ResultsPanel.Visible = true;
                    DisplayGrid(mc, re);
                }
                else
                {
                    NoResultsLabel.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ErrorPanel.Controls.Add(new LiteralControl("<br/><b>" + ex.Message + "</b>"));
                ErrorPanel.Visible = true;
            }
        }

        private void DisplayGrid(MatchCollection mc, Regex re)
        {
            int rows = mc.Count;
            int cols = mc[0].Groups.Count;

            System.Web.UI.WebControls.Table tbl = new Table();
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

            for (int i = 0; i < cols - 1; i++)
            {
                th = new TableHeaderCell();
                th.Text = "$" + (1 + i);
                th.CssClass = "NrmlTD";
                th.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(th);
            }

            tbl.Rows.Add(tr);

            foreach (Match m in mc)
            {
                tr = new TableRow();
                tr.CssClass = (tbl.Rows.Count % 2 == 0) ? "StandardRow" : "HighlightRow";
                for (int j = 0; j < cols; j++)
                {
                    TableCell td = new TableCell();
                    td.Text = Server.HtmlEncode(m.Groups[j].Value);
                    td.Attributes.Add("onclick", "HighlightMatch('" + m.Groups[j].Index + "', '" + m.Groups[j].Length + "') ;");
                    td.CssClass = "NrmlTD";
                    td.Wrap = false;
                    tr.Cells.Add(td);
                }

                tbl.Rows.Add(tr);
            }
            this.ResultsPanel.Controls.Add(tbl);
            Request.QueryString.Add("","#results");
        }

        protected void LoadExternalDataButton_Click(object sender, EventArgs e)
        {
            ErrorPanel.Controls.Clear();
            string source = string.Empty;
            try
            {
                if (URLTextbox.Text.Trim().Length > 0)
                {
                    source = GetUrlSource(URLTextbox.Text);
                    URLTextbox.Text = String.Empty;
                }
                else if (FileUpload1.HasFile && FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0)
                {
                    System.IO.Stream s;

                    s = this.FileUpload1.PostedFile.InputStream;
                    s.Position = 0;
                    System.IO.StreamReader sr = new System.IO.StreamReader(s);
                    source = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                ErrorPanel.Controls.Add(new LiteralControl("<span>" + ex.ToString() + "</span>"));
            }

            this.SourceTextBox.Text = source; ;
        }

        private string GetUrlSource(string url)
        {
            string tmp = string.Empty;
            try
            {
                if (!url.ToLower().StartsWith("http://"))
                {
                    url = "http://" + url;
                }

                using (WebClient wc = new WebClient())
                using (Stream s = wc.OpenRead(url))
                {
                    StreamReader sr = new StreamReader(s);
                    tmp = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                ErrorPanel.Controls.Add(new LiteralControl("<span>" + ex.ToString() + "</span>"));
            }

            return tmp;
        }
    }
}