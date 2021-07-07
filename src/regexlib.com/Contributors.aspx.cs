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
using RegexLib.com.UserControls;
using Ardalis.Framework.Diagnostics;
using Ardalis.Framework;

namespace RegExLib.Web
{
    public partial class Contributors : BasePage
    {
        private int repeaterItemCount = 0;
        public const int PAGESIZE = 50;
        public const int ADREPEATITEMS = 20;
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
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            int pageCount = 1;
            int expressionCount = GetTotalExpressions();
            if (expressionCount > 0)
            {
                pageCount = (int)System.Math.Ceiling((double)expressionCount / (double)PAGESIZE);
            }

            // set up pager
            SetPagerValues(Pager1 as Pager, pageCount, expressionCount);
            SetPagerValues(Pager2 as Pager, pageCount, expressionCount);

        }

        private int GetTotalExpressions()
        {
            string sql =
                "select count(distinct(ex.userid)) from rxl_Expression ex where ex.Enabled=1";

            int count = Parse.To<int>(Ardalis.Framework.Data.SqlHelper.ExecuteScalar(
                ConfigurationManager.ConnectionStrings["regexlibConnectionString"].ConnectionString, CommandType.Text,
                sql).ToString());

            return count;
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
            myPager.QueryString = "";
        }

        protected void ContributorsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || 
                e.Item.ItemType == ListItemType.AlternatingItem)
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