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

namespace RegexLib.com.UserControls
{
    public partial class Pager : System.Web.UI.UserControl
    {
        #region Properties
        private int _currentPage = 0;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set { _currentPage = value; }
        }
        private int _lastPage = 0;
        public int LastPage
        {
            get
            {
                return _lastPage;
            }
            set { _lastPage = value; }
        }
        private int _firstItemOnPage = 0;
        public int FirstItemOnPage
        {
            get
            {
                return _firstItemOnPage;
            }
            set { _firstItemOnPage = value; }
        }
        private int _lastItemOnPage = 0;
        public int LastItemOnPage
        {
            get
            {
                return _lastItemOnPage;
            }
            set { _lastItemOnPage = value; }
        }

        private string _queryString;
        public string QueryString
        {
            get { return _queryString; }
            set { _queryString = value;}
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        { 

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if(!Visible) return;

            if (CurrentPage == 0) throw new ArgumentException("CurrentPage property not set.");
            if (LastPage == 0) throw new ArgumentException("LastPage property not set.");
            if (FirstItemOnPage == 0) throw new ArgumentException("FirstItemOnPage property not set.");
            if (LastItemOnPage == 0) throw new ArgumentException("LastItemOnPage property not set.");

            CurrentPageLabel.Text = CurrentPage.ToString();
            TotalPagesLabel.Text = LastPage.ToString();
            FirstItemLabel.Text = FirstItemOnPage.ToString();
            LastItemLabel.Text = LastItemOnPage.ToString();

            if(CurrentPage==1)
            {
                StepBackAllHyperLink.Visible = false;
                StepBackOneHyperLink.Visible = false;
            }

            if(CurrentPage==LastPage)
            {
                StepForwardAllHyperLink.Visible = false;
                StepForwardOneHyperLink.Visible = false;
                if(CurrentPage==1)
                {
                    PagerPanel1.Visible = false;
                }
            }
            

            string basePath = Request.Path + "?" + QueryString;
            StepBackOneHyperLink.NavigateUrl = basePath + "&p=" + (CurrentPage - 1).ToString();
            StepBackAllHyperLink.NavigateUrl = basePath + "&p=1";
            StepForwardOneHyperLink.NavigateUrl = basePath + "&p=" + (CurrentPage + 1).ToString();
            StepForwardAllHyperLink.NavigateUrl = basePath + "&p=" + LastPage.ToString();
        }
    }
}