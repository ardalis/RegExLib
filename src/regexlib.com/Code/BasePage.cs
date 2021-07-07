#define TRACE
#define DEBUG
using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RegExLib.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadParametersFromQueryString();
        }

        /// <summary>
        /// Called by OnInit.  Override in any page that requires querystring parameters to be set.  Be sure to call base.LoadParametersFromQueryString().
        /// </summary>
        protected virtual void LoadParametersFromQueryString()
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (Trace.IsEnabled)
            {
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter localWriter = new HtmlTextWriter(sw);
                base.Render(localWriter);
                string output = sw.ToString();
                int pageSize = output.Length;
                Ardalis.Framework.Diagnostics.Trace.Warn("Page Size: " + pageSize.ToString());
                writer.Write(output);
            }
            else
            {
                base.Render(writer);
            }
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            base.SavePageStateToPersistenceMedium(viewState);
            if (Trace.IsEnabled)
            {
                LosFormatter format = new LosFormatter();
                StringWriter writer = new StringWriter();
                format.Serialize(writer, viewState);
                int viewStateLength = writer.ToString().Length;
                Ardalis.Framework.Diagnostics.Trace.Warn("ViewState Size: " + viewStateLength.ToString());
            }
        }
    }
}
