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

namespace RegExLib.Web {

    public partial class Register : System.Web.UI.Page {

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e) {
            TextBox tbox = this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Fullname") as TextBox;
            if (tbox != null) { WebProfile.Current.FullName = tbox.Text; }
        }
    }
}