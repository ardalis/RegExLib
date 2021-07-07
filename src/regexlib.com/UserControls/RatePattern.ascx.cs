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
using RegExLib.Data;

public partial class RatePattern : System.Web.UI.UserControl
{

    private int _expressionId = 0;
    public int ExpressionId {
        get { return _expressionId; }
        set { _expressionId = value; }
    }


    protected int UserRating {
        get { return (ViewState["UserRating"] == null) ? 0 : (int)ViewState["UserRating"]; }
        set { ViewState["UserRating"] = value; }
    }

    protected override void OnLoad(EventArgs e) 
    {
        int.TryParse(Request.QueryString["regexp_id"], out this._expressionId);

        if (!IsPostBack) {
            if (Membership.GetUser() != null) {
                object userId = Membership.GetUser().ProviderUserKey;
                this.UserRating = ExpressionManager.GetUserRating(this._expressionId, userId);
           
                
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e) 
    {

        int rating = 0;
        if (this.Rating1.Checked) rating = 1;
        else if (this.Rating2.Checked) rating = 2;
        else if (this.Rating3.Checked) rating = 3;
        else if (this.Rating4.Checked) rating = 4;
        else if (this.Rating5.Checked) rating = 5;

        object userId = Guid.Empty ;
        if( Membership.GetUser() != null ) {
            userId = Membership.GetUser().ProviderUserKey ;
        }

        this.UserRating = rating;
        ExpressionManager.AddRating(this.ExpressionId, rating, userId);
        pnlYourRating.Visible = true;
        pnlRating.Visible = false;
    }


    protected void lnkRateAgain_Click(object sender, EventArgs e)
    {
        this.UserRating = 0;
        this.pnlYourRating.Visible = false;
        this.pnlRating.Visible = true;
    }


    protected override void OnPreRender(EventArgs e) 
    {

        if (this._expressionId > 0) {
            pnlRating.Visible = true;
            Expression expression = ExpressionManager.GetExpression(this.ExpressionId);

            if (this.UserRating > 0) {
                this.pnlYourRating.Visible = true;
                this.pnlRating.Visible = false;
                this.RatingImage2.Rating = this.UserRating;
            } else {
                this.pnlYourRating.Visible = false;
                this.pnlRating.Visible = true;
            }
        } else {
            pnlRating.Visible = false;
            this.pnlYourRating.Visible = false;
        }

    }
}
