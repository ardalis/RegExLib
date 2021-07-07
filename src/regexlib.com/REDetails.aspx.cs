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

namespace RegExLib.Web {

    public partial class REDetails : System.Web.UI.Page 
    {
        private int _expressionId;
        public int ExpressionId
        {
            get
            {
                if (_expressionId == 0)
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["regexp_id"]))
                    {
                        int.TryParse(Request.QueryString["regexp_id"], out _expressionId);
                    }
                }
                return _expressionId;
            }
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



        protected override void OnLoad(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            FillExpressionDetailsData();
            base.OnLoad(e);
        }

        private void FillExpressionDetailsData()
        {
            if (MyExpression != null)
            {
                TitleLabel.Text = MyExpression.TitleAsHtml;
                ExpressionLabel.Text = MyExpression.PatternAsHtml;
                DescriptionLabel.Text = MyExpression.DescriptionAsHtml;
                MatchesLabel.Text = MyExpression.MatchingTextAsHtml;
                NonMatchesLabel.Text = MyExpression.NonMatchingTextAsHtml;
                AuthorHyperlink.Text = MyExpression.AuthorName;
                AuthorHyperlink.NavigateUrl = "UserPatterns.aspx?authorId=" + MyExpression.AuthorId;
                SourceLabel.Text = MyExpression.Source;
                RatingImage1.Rating = (int)MyExpression.Rating;
            }
            else
            {
                DetailsPanel.Visible = false;
                InvalidExpression.Visible = true;
                UserComments1.Visible = false;
            }
        }

        protected override void OnPreRender(EventArgs e) 
        {
            if (MyExpression != null)
            {
                Guid userId = MyExpression.AuthorId;

                Author currentAuthor = AuthorManager.GetCurrentAuthor();
                if (currentAuthor != null && currentAuthor.Id == userId)
                {
                    EditLink.Visible = true;
                    EditLink.NavigateUrl = "~/Edit.aspx?regexp_id=" + ExpressionId;
                }
                else
                {
                    EditLink.Visible = false;
                }

                TestLink.NavigateUrl = "RETester.aspx?regexp_id=" + ExpressionId;
            }
        }
    }
}