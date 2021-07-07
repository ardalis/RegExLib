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

    public partial class Contact : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }
        protected void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(EmailTextBox.Text, NameTextBox.Text);
                System.Net.Mail.MailAddress toAddress = new System.Net.Mail.MailAddress(Globals.AdminEmail);
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddress, toAddress);
                message.Subject = SubjectTextBox.Text;
                message.Body = NameTextBox.Text + " writes..." + System.Environment.NewLine + MessageTextBox.Text;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Globals.MailServer);
                //System.Net.NetworkCredential credential = new System.Net.NetworkCredential("login", "password");
                //client.Credentials = credential;
                client.Send(message);
            }
            // TODO: Implement better exception handling
            catch (Exception ex)
            {
                MessageLabel.Text = "Error sending message: " + ex.Message;
            }
            finally
            {
                ContactMultiView.ActiveViewIndex = 1;
            }
        }
}
}