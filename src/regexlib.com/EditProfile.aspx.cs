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
using RegExLib.Web;

public partial class EditProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if ( !Page.IsPostBack )
		{
			this.FullName.Text = WebProfile.Current.FullName;
			this.Email.Text = Membership.GetUser().Email;
		}
    }
	protected void SaveProfileChanges_Click( object sender, EventArgs e )
	{
		this.UpdateProblem.Visible = false;
		this.UpdateProblemText.Text = "";

		if ( FullNameRequired.IsValid )
		{
			try
			{
				WebProfile.Current.FullName = this.FullName.Text;
			}
			catch
			{
				this.UpdateProblem.Visible = true;
				this.UpdateProblemText.Text += "There was an unknown problem updating your Full Name.";
			}
		}

		MembershipUser member = Membership.GetUser();

		if ( EmailRequired.IsValid && member.Email != this.Email.Text ) {
			member.Email = this.Email.Text;
			try
			{
				Membership.UpdateUser( member );
			}
			catch
			{
				this.UpdateProblem.Visible = true;
				this.UpdateProblemText.Text += "There was a problem updating your email. It's either an invalid email address or already taken.";
			}
		}

		if ( this.NewPassword.Text.Length > 0 && this.OldPassword.Text.Length > 0 && this.ConfirmPasswordMatches.IsValid && this.NewPassword.Text == this.NewPasswordConfirm.Text )
		{
			if ( !member.ChangePassword( this.OldPassword.Text, this.NewPassword.Text ) )
			{
				this.UpdateProblem.Visible = true;
				this.UpdateProblemText.Text += "There was a problem changing your password. It may not be complex enough. Please try another password.";
			}
		}
		if ( !this.UpdateProblem.Visible )
		{
			Response.Redirect( "~/" );
		}
	}
	protected void Cancel_Click( object sender, EventArgs e )
	{
		Response.Redirect( "~/" );
	}
}
