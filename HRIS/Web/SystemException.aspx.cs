using System;

public partial class SystemException : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            try
            {
                if (Session["CURRENTURL"] != null)
                {
                    radbtnGoBack.Enabled = true;
                }
                else
                {
                    radbtnGoBack.Enabled = false;
                }

                if (Session["APPERROROBJ"] != null)
                {
                    radbtnViewErrors.Visible = true;

                    Exception exx_ = (Exception)Session["APPERROROBJ"];
                    lblError.Text = exx_.Message;
                    lblErrorDesc.Text = exx_.InnerException.Message;
                    lblErrorStack.Text = exx_.InnerException.StackTrace;
                    lblErrorSource.Text = exx_.InnerException.Source + " :: " + exx_.TargetSite.Name;
                }
                else
                {
                    radbtnViewErrors.Visible = false;

                    lblError.Text = "";
                    lblErrorDesc.Text = "";
                    lblErrorStack.Text = "";
                    lblErrorSource.Text = "";
                }
            }
            catch { }
        }
    }

    protected void radbtnViewErrors_Click(object sender, EventArgs e)
    {
        if (radbtnViewErrors.Text != "Hide error details!")
        {
            errorPanel.Visible = true;
            radbtnViewErrors.Text = "Hide error details!";
        }
        else
        {
            errorPanel.Visible = false;
            radbtnViewErrors.Text = "View error details...";
        }
    }

    protected void radbtnGoToDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.html");
    }

    protected void radbtnGoBack_Click(object sender, EventArgs e)
    {
        if (Session["CURRENTURL"] != null)
        {
            Response.Redirect(Session["CURRENTURL"] as string);
        }
    }
}