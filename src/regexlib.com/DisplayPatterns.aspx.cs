using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Ardalis.Framework;
using Ardalis.Framework.Diagnostics;
using RegexLib.com.UserControls;
using RegExLib.Data;

namespace RegExLib.Web
{

    public partial class DisplayPatterns : BasePage
    {
        private int repeaterItemCount = 0;
        public const int ADREPEATITEMS = 3;
        public const int PAGESIZE = 20;

        /// <summary>
        /// Determines which Category is selected.
        /// </summary>
        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
        }

        private int _selectedId;
        public int SelectedId
        {
            get
            {
                if (_selectedId > 0)
                {
                    return _selectedId;
                }
                return 1;
            }
        }

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

        protected override void LoadParametersFromQueryString()
        {
            base.LoadParametersFromQueryString();
            if (!IsPostBack)
            {
                _currentPage = Parse.To<int>(this.Request["p"]);
            }
            if (CurrentPage == 0) _currentPage = 1;
            _selectedIndex = Parse.To<int>(Request["cattabindex"]);
            _selectedId = Parse.To<int>(Request["categoryId"]);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            int expressionCount = GetTotalExpressions();
            this.lblResultCount.Text = expressionCount.ToString();

            int pageCount = 1;
            if (expressionCount > 0)
            {
                pageCount = (int)System.Math.Ceiling((double)expressionCount / (double)PAGESIZE);
            }

            // set up pagers
            SetPagerValues(Pager1 as Pager, pageCount, expressionCount);
            SetPagerValues(Pager2 as Pager, pageCount, expressionCount);

            if (this.SelectedId > 0)
            {
                CategoryShownLabel.Text = CategoryManager.GetCategory(this.SelectedId).Description;
            }
        }

        private int GetTotalExpressions()
        {
            List<Expression> exprs = ExpressionManager.ListExpressionsBySearch(string.Empty,
                this.SelectedId, -1, string.Empty, -1, -1);
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
            Debug.Print("SetPagerValues:PageSize: " + PAGESIZE);
            Debug.Print("SetPagerValues:Itemcount: " + itemCount);
            myPager.FirstItemOnPage = (CurrentPage - 1) * PAGESIZE + 1;
            myPager.LastItemOnPage = CurrentPage * PAGESIZE;


            if (myPager.LastItemOnPage > itemCount) myPager.LastItemOnPage = itemCount;
            myPager.CurrentPage = CurrentPage;
            myPager.LastPage = pageCount;
            myPager.QueryString = "cattabindex=" + this.SelectedIndex + "&categoryid=" + this.SelectedId;
        }

        protected void SearchResultsRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
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