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
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Text;
using RegExLib;
using RegExLib.Data;

public partial class UserInput : System.Web.UI.UserControl, IWebPart, IWebActionable, IInputSettings {

    private IModeSettings _modeData;
    
    
    [ConnectionProvider("User Input")]
    public IInputSettings GetUserInput() {
        return this;
    }

    [ConnectionConsumer("Mode Options")]
    public void SetMode(IModeSettings provider) {
        this._modeData = provider;
    }


    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        if (!IsPostBack && Request.Params["regexp_id"] != null) {
            int expressionid = 0;
            int.TryParse(Request.Params["regexp_id"], out expressionid);
            if (expressionid > 0) {
				Expression target = ExpressionManager.GetExpression( expressionid );
				if ( target != null )
				{
					string ptrn = CleanString( Server.HtmlDecode( target.Pattern ) );
					EmitLoadScript( ptrn );
				}
            }
        }
    }


    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        this.pnlAdvancedFeatures.Visible = this.IsAdvancedMode;
        this.pnlClient.Visible = (this._modeData.RunatMode == RunatMode.Client);
        this.pnlServer.Visible = (this._modeData.RunatMode == RunatMode.Server);
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
        get { return "Regex Input"; }
        set { ; }
    }

    public string TitleIconImageUrl {
        get { return string.Empty; }
        set { ; }
    }

    public string TitleUrl {
        get { return string.Empty ; }
        set { ; }
    }


    #endregion

    #region IInputSettings Members

    public string Source {
        get { return this.txtSource.Text ; }
    }

    public string Pattern {
        get { return this.txtPattern.Text ; }
    }

    #endregion

    #region IWebActionable Members

    public WebPartVerbCollection Verbs {
        get {
            ArrayList verbs = new ArrayList();

            WebPartVerb v1 = new WebPartVerb("Mode", new WebPartEventHandler(Toggle)); ;
            if (this.IsAdvancedMode) {
                v1.Text = "Hide Advanced Input Controls";
            } else {
                v1.Text = "Display Advanced Input Controls";
            }

            verbs.Add(v1);

            WebPartVerb v2 = new WebPartVerb("AddPattern", new WebPartEventHandler(AddPattern));
            v2.Text = "Add Current Pattern";
            verbs.Add(v2);

            return new WebPartVerbCollection((WebPartVerb[])verbs.ToArray(typeof(WebPartVerb)));
        }
    }


    public void Toggle(object sender, WebPartEventArgs e) {
        IsAdvancedMode = !IsAdvancedMode;
    }

    public void AddPattern(object sender, WebPartEventArgs e) {
        if (!string.IsNullOrEmpty(this.txtPattern.Text)) {
            Response.Redirect("Add.aspx?regular_expression=" + Server.UrlEncode(this.txtPattern.Text));
        }
    }


    // TODO: Replace with Personalization
    [Personalizable]
    [WebBrowsable]
    public bool IsAdvancedMode {
        get { return ViewState["IsAdvancedMode"] == null ? false : (bool)ViewState["IsAdvancedMode"]; }
        set { ViewState["IsAdvancedMode"] = value; }
    }

    #endregion
    

    protected void btnExternalData_Click(object sender, EventArgs e) {

        lblErrorMessage.Text = string.Empty;
        string source = string.Empty;
        try {
            if (this.txtUrl.Text.Trim().Length > 0) {
                source = GetUrlSource(this.txtUrl.Text);
                this.txtUrl.Text = String.Empty;
            } else if (this.filUpload.PostedFile != null && this.filUpload.PostedFile.ContentLength > 0) {
                System.IO.Stream s;

                s = this.filUpload.PostedFile.InputStream;
                s.Position = 0;
                System.IO.StreamReader sr = new System.IO.StreamReader(s);
                source = sr.ReadToEnd();
            }
        } catch (Exception ex) {
            lblErrorMessage.Text = ex.ToString();
        }

        this.txtSource.Text = source;;
	}


    private void EmitLoadScript(string s) {
        EmitLoadScript(s, this.txtPattern.ClientID);
    }


    private void EmitLoadScript(string s, string ctlId) {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat(@"{0}<script>{0}", Environment.NewLine);
        sb.Append(@"document.forms[0].elements[""");
        sb.AppendFormat(@"{0}""].value =""{1}"" ;", ctlId, s);
        sb.AppendFormat("{0}</script>{0}", Environment.NewLine);

        Page.ClientScript.RegisterStartupScript(typeof(string), "populateTB", sb.ToString());
    }


    private string GetUrlSource(string url) {
        string tmp = String.Empty;

        try {
            if (!url.ToLower().StartsWith("http://")) {
                url = "http://" + url;
            }

            using (WebClient wc = new WebClient())
            using (Stream s = wc.OpenRead(url)) {

                StreamReader sr = new StreamReader(s);
                tmp = sr.ReadToEnd();
            }
        } catch (Exception ex) {
            lblErrorMessage.Text = ex.ToString();
        }

        return tmp;
    }


    private string CleanString(string s) {
        string tmp = s;
        if (tmp.Trim().Length > 0) {
            tmp = Regex.Replace(
                tmp, @"(?'cleanItem'[\\\/'""])", "\\${cleanItem}").Trim();

            tmp = tmp.Replace(Environment.NewLine, @"\n");
        }

        return tmp;
    }

}
