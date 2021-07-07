using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ardalis.Framework;
using RegexLib.com.UserControls;
using RegExLib.Data;
using Ardalis.Framework.Diagnostics;

namespace RegExLib.Web
{

    public partial class Search : BasePage
    {
        private int repeaterItemCount = 0;
        public const int ADREPEATITEMS = 3;
        #region Properties
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

        private int _categoryId = 0;
        public int CategoryId
        {
            get { return _categoryId; }
        }
        private int _minRating = 0;
        public int MinRating
        {
            get { return _minRating; }
        }
        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; }
        }

        private string _keywords;
        public string Keywords
        {
            get { return _keywords; }
        }
        #endregion

        protected override void LoadParametersFromQueryString()
        {
            base.LoadParametersFromQueryString();
            if (!IsPostBack)
            {
                _currentPage = Parse.To<int>(this.Request["p"]);
                _categoryId = Parse.To<int>(this.Request["c"]);
                _minRating = Parse.To<int>(this.Request["m"]);
                _pageSize = Parse.To<int>(this.Request["ps"]);
                _keywords = Request["k"];


            }
            if (CurrentPage == 0) _currentPage = 1;
            if (PageSize == 0)
            {
                _pageSize = 20;
                ddPageSize.Items[1].Selected = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsPostBack)
            {
                // postback always resets page
                _currentPage = 1;
                _categoryId = Parse.To<int>(ddCategory.SelectedValue);
                _minRating = Parse.To<int>(ddMinimumRating.SelectedValue);
                _pageSize = Parse.To<int>(ddPageSize.SelectedValue);
                _keywords = txtKeywords.Text;
                RedirectWithQuerystring();
            }
            else
            {
                txtKeywords.Text = Keywords;
            }
        }

        private void RedirectWithQuerystring()
        {
            string url = Request.Path + "?" + BuildQueryString();
            Response.Redirect(url, true);
        }

        private string BuildQueryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("k=" + Server.UrlEncode(Keywords));
            sb.Append("&c=" + CategoryId.ToString());
            sb.Append("&m=" + MinRating.ToString());
            sb.Append("&ps=" + PageSize.ToString());
            return sb.ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            try
            {
                int expressionCount = GetTotalExpressions(CategoryId, MinRating);
                int pageCount = 1;
                if (PageSize > 0 && expressionCount > 0)
                {
                    pageCount = (int)System.Math.Ceiling((double)expressionCount / (double)PageSize);
                }

                // set up form
                ListItem selectedItem;

                Debug.Print("MinRating: " + MinRating.ToString());
                Debug.Print("ddMinRating: " + ddMinimumRating.Items.Count.ToString());

                selectedItem = ddMinimumRating.Items.FindByValue(MinRating.ToString());
                if (selectedItem != null) ddMinimumRating.SelectedValue = MinRating.ToString(); else Debug.Print("MinRating Item Not Found");

                selectedItem = ddPageSize.Items.FindByValue(PageSize.ToString());
                if (selectedItem != null) ddPageSize.SelectedValue = PageSize.ToString(); else Debug.Print("PageSize Item Not Found");

                // set up pagers
                SetPagerValues(Pager1 as Pager, pageCount, expressionCount);
                SetPagerValues(Pager2 as Pager, pageCount, expressionCount);

                ResultCountLabel1.Text = expressionCount.ToString();
            }
            catch (Exception ex)
            {
                // do nothing for now
                // TODO: Implement exception logging here
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }

        protected void ddCategory_DataBound(object sender, EventArgs e)
        {
            this.ddCategory.Items.Insert(0, new ListItem("(   All   )", "-1"));
            if (!IsPostBack)
            {
                ListItem selectedItem = ddCategory.Items.FindByValue(CategoryId.ToString());
                if (selectedItem != null)
                {
                    Debug.Print("Setting Category to " + selectedItem.Value);
                    ddCategory.SelectedValue = CategoryId.ToString();
                }
            }
        }

        private int GetTotalExpressions(int categoryId, int minRating)
        {
            List<Expression> exprs = ExpressionManager.ListExpressionsBySearch(this.txtKeywords.Text,
                                                                   categoryId, minRating, -1, -1);
            if (exprs != null)
            {
                return exprs.Count;
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
            Debug.Print("SetPagerValues:PageSize: " + PageSize);
            Debug.Print("SetPagerValues:Itemcount: " + itemCount);
            myPager.FirstItemOnPage = (CurrentPage - 1) * PageSize + 1;
            myPager.LastItemOnPage = CurrentPage * PageSize;


            if (myPager.LastItemOnPage > itemCount) myPager.LastItemOnPage = itemCount;
            myPager.CurrentPage = CurrentPage;
            myPager.LastPage = pageCount;
            myPager.QueryString = BuildQueryString();
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
    }
}