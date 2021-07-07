using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ardalis.Framework;
using Ardalis.Framework.Diagnostics;
using RegexLib.com.UserControls;
using RegExLib.Data;

namespace RegExLib.Web
{
    public partial class UserPatterns : BasePage
    {
        private int repeaterItemCount = 0;
        public const int PAGESIZE = 20;
        public const int ADREPEATITEMS = 3;
        private int? _currentPage = 1;
        public int CurrentPage
        {
            get
            {
                if (!this._currentPage.HasValue)
                {
                    _currentPage = Parse.To<int>(this.Request["p"]);
                }
                return _currentPage.Value;
            }
        }

        private string _authorId;
        public string AuthorId
        {
            get
            {
                return _authorId;
            }
        }

        protected Boolean ShowMyExpressions
        {
            get
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    // If there isn't an author then show the current authorId
                    if (String.IsNullOrEmpty(AuthorId))
                    {
                        return true;
                    }

                    return AuthorId == Membership.GetUser().ProviderUserKey.ToString();
                }
                return false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!ShowMyExpressions)
            {
                ObjectDataSource1.SelectParameters["enabledOnly"] = new Parameter("enabledOnly", TypeCode.Boolean, "true");
            }
            ObjectDataSource1.SelectParameters["pageSize"].DefaultValue = PAGESIZE.ToString();
        }

        protected override void LoadParametersFromQueryString()
        {
            base.LoadParametersFromQueryString();
            if (!IsPostBack)
            {
                _currentPage = Parse.To<int>(this.Request["p"]);
                _authorId = Request.QueryString["authorId"];
                if(String.IsNullOrEmpty(_authorId))
                {
                    if(Request.IsAuthenticated)
                    {
                        _authorId = Membership.GetUser().ProviderUserKey.ToString();
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx?ReturnUrl=%2fUserPatterns.aspx");
                    }
                }
            }
            if (CurrentPage == 0) _currentPage = 1;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            int expressionCount = GetTotalExpressions();
            int pageCount = 1;
            if (expressionCount > 0)
            {
                pageCount = (int)System.Math.Ceiling((double)expressionCount / (double)PAGESIZE);
            }

            // set up pager
            SetPagerValues(Pager1 as Pager, pageCount, expressionCount);
            SetPagerValues(Pager2 as Pager, pageCount, expressionCount);

            if (!ShowMyExpressions)
            {
                NewExpressionLink.Visible = false;
                NewExpressionLinkBottom.Visible = false;
            }
        }

        private int GetTotalExpressions()
        {
            if (this.AuthorId == null) return 0;

            try
            {
                Guid authorGuid = new Guid(this.AuthorId);
                List<Expression> exprs =
                    ExpressionManager.ListExpressionsByAuthor(authorGuid, -1, -1);
                if (exprs != null)
                {
                    return exprs.Count;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        private void SetPagerValues(Pager myPager, int pageCount, int itemCount)
        {
            if (myPager == null) return;
            if (itemCount == 0)
            {
                myPager.Visible = false;
                return;
            }

            Debug.Print("SetPagerValues:CurrentPage: " + CurrentPage);
            Debug.Print("SetPagerValues:PageCount: " + pageCount);
            Debug.Print("SetPagerValues:PageSize: " + PAGESIZE);
            Debug.Print("SetPagerValues:Itemcount: " + itemCount);
            myPager.FirstItemOnPage = (CurrentPage - 1) * PAGESIZE + 1;
            myPager.LastItemOnPage = CurrentPage * PAGESIZE;


            if (myPager.LastItemOnPage > itemCount) myPager.LastItemOnPage = itemCount;
            myPager.CurrentPage = CurrentPage;
            myPager.LastPage = pageCount;
            myPager.QueryString = "authorid=" + this.AuthorId;
        }

        protected void EditButtonArea_DataBinding(Object sender, EventArgs e)
        {
            MyExpressionsCommandArea_DataBinding(sender, e);
        }

        protected void MyExpressionsCommandArea_DataBinding(Object sender, EventArgs e)
        {
            Control source = sender as Control;
            source.Visible = ShowMyExpressions;
        }

        protected void AreaToHideIfMyExpressions_DataBinding(Object sender, EventArgs e)
        {
            Control source = sender as Control;
            source.Visible = !(ShowMyExpressions);
        }

        protected void SearchResultsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                repeaterItemCount++;
            }
            if (e.Item.ItemType == ListItemType.Separator)
            {
                if (repeaterItemCount % ADREPEATITEMS != 0 || repeaterItemCount > ADREPEATITEMS * 2)
                {
                    e.Item.Visible = false;
                }
            }

        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                Guid foo = new Guid(this.AuthorId);
            }
            catch
            {
                e.Cancel = true;
            }
        }
    }
}