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

public partial class WebParts_RectangleAd : System.Web.UI.UserControl, IWebPart
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        get { return "Sponsor"; }
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

}
