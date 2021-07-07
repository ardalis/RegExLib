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


public partial class RatingImage : System.Web.UI.UserControl
{

    private int _rating;

    public int Rating {
        get { return _rating; }
        set { _rating = value; }
    }

    protected override void OnPreRender(EventArgs e) {
        this.lblRating.Visible = (Rating == 0);
        
        switch (this.Rating) {
            case 1:
                this.Image1.Visible = true;
                break;
            case 2:
                this.Image2.Visible = true;
                break;
            case 3:
                this.Image3.Visible = true;
                break;
            case 4:
                this.Image4.Visible = true;
                break;
            case 5:
                this.Image5.Visible = true;
                break;
            default:
                this.lblRating.Visible = true;
                break;
        }
    }
}
