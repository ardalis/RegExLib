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
using RegExLib;

public partial class ConfigurationOptions : System.Web.UI.UserControl, IWebPart, IWebActionable, IModeSettings {

    [ConnectionProvider("Mode Options")]
    public IModeSettings GetModeOptions() {
        return this;
    }


    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        this.pnlVisible.Visible = !this.IsMinimized;
        this.phClientModeOptions.Visible = (this.Mode == RunatMode.Client);
        this.phServerModeOptions.Visible = (this.Mode == RunatMode.Server);
    }


    #region IModeSettings Members

    public RunatMode RunatMode {
        get {
            return this.Mode;
        }
    }

    // TODO: Personalize the options settings
    public RegexOptions Options {
        get {
            RegexOptions opts = RegexOptions.None;

            if (this.Mode == RunatMode.Server) {
                if (this.cbSe_Multiline.Checked) opts |= RegexOptions.Multiline;
                if (this.cbSe_IgnoreWhiteSpace.Checked) opts |= RegexOptions.IgnorePatternWhitespace;
                if (this.cbSe_CaseInsensitive.Checked) opts |= RegexOptions.IgnoreCase;
                if (this.cbSe_Singleline.Checked) opts |= RegexOptions.Singleline;
                if (this.cbSe_ExplicitCapture.Checked) opts |= RegexOptions.ExplicitCapture;
            } else {
                if (this.cbCl_CaseInsensitive.Checked) opts |= RegexOptions.IgnoreCase;
                if (this.cbCl_IgnoreWhiteSpace.Checked) opts |= RegexOptions.IgnorePatternWhitespace;
                if (this.cbCl_Global.Checked) opts |= RegexOptions.Multiline;
            }
            return opts;
        }
    }

    #endregion

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
        get { return "Current Options : (" + this.Options.ToString() + ")"; }
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

            WebPartVerb v1 = new WebPartVerb("PanelDisplay", new WebPartEventHandler(Toggle));
            v1.Text = (this.IsMinimized) ? "Show Options Panel" : "Hide Options Panel" ;
            verbs.Add(v1) ;


            WebPartVerb v2 = new WebPartVerb("ServerMode", new WebPartEventHandler(SetMode));
            v2.Text = "Use .NET Regex Engine";
            v2.Checked = (this.Mode == RunatMode.Server);
            verbs.Add(v2);

            WebPartVerb v3 = new WebPartVerb("ClientMode", new WebPartEventHandler(SetMode));
            v3.Text = "Use Client-side Regex Engines";
            v3.Checked = (this.Mode == RunatMode.Client);
            verbs.Add(v3);
 
            return new WebPartVerbCollection((WebPartVerb[])verbs.ToArray(typeof(WebPartVerb)));
        }
    }


    public void Toggle(object sender, WebPartEventArgs e) {
        IsMinimized = !IsMinimized;
    }


    public void SetMode(object sender, WebPartEventArgs e) {
        WebPartVerb verb = sender as WebPartVerb;
        if (verb != null) {
            Mode = (verb.ID == "ServerMode") ? RunatMode.Server : RunatMode.Client;
        }
    }


    private bool _isMinimized = false;
    [Personalizable]
    [WebBrowsable]
    public bool IsMinimized {
        get { return _isMinimized; }
        set { _isMinimized = value; }
    }

    
    private RunatMode _mode = RunatMode.Server;
    [Personalizable]
    [WebBrowsable]
    public RunatMode Mode {
        get { return _mode; }
        set { _mode = value; }
    }

    #endregion
}
