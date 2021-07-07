using System;
using System.Web.UI.WebControls;
using RegExLib.Data;

namespace RegExLib.Web.UserControls {
    public partial class UserComments : System.Web.UI.UserControl
    {
        #region Properties
        protected CaptchaControl _captcha;
        protected CaptchaControl captcha1
        {
            get
            {
                if (_captcha == null)
                {
                    _captcha = CommentFormView.FindControl("captcha1") as CaptchaControl;
                }
                return _captcha;
            }
        }

        protected Button _btnAdd;
        protected Button btnAdd
        {
            get
            {
                if (_btnAdd == null)
                {
                    _btnAdd = CommentFormView.FindControl("btnAddComment") as Button;
                }
                return _btnAdd;
            }
        }

        int ExpressionId
        {
            get {
                int expressionId = -1;
                int.TryParse(Request.QueryString["regexp_id"], out expressionId);
                return expressionId;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            System.Diagnostics.Debug.Print("Load");
            this.CommentFormView.Visible = (this.ExpressionId > 0);
            this.btnAdd.Enabled = !captcha1.Visible;
            if (this.btnAdd.Enabled)
            {
                this.btnAdd.CssClass = "buttonLarge";
            }
            else
            {
                this.btnAdd.CssClass = "buttonLargeDisabled";
            }
        }

        protected void CommentFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            if (this.ExpressionId <= 0 ||
                this.btnAdd.Enabled == false) 
            {
                e.Cancel = true;
                return; 
            }
            e.Values.Add("expressionId", this.ExpressionId);
            this.CommentFormView.Visible = false;
            this.CommentSubmittedPanel.Visible = true;
        }

        #region CAPTCHA Events
        protected void OnSuccess()
        {
            captcha1.Visible = false;
            btnAdd.Enabled = true;
            btnAdd.CssClass = "buttonLarge";
        }
        protected void OnFailure()
        {
            captcha1.Message = "The text you entered was not correct. Please try again:";
        }
        #endregion
    }
}