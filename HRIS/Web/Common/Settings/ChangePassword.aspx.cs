using System;
using System.Drawing;
using HRM.Common.BLL;

public partial class Settings_ChangePassword : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "ChangePassword";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (IsPostBack == false)
            {
                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/Common/NoPermissions.aspx");
                    return;
                }
                else
                {
                    radbtnSave.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                    }
                }
            }
        }

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;
        
        if (!IsPostBack)
        {
            InitializeControls();
        }
    }
  
    #region Form Methods

    public void InitializeControls()
    {
        if (Session["UserName"] != null)
        {
            radtxtUserName.Text = Session["UserName"].ToString().Trim();
        }
        else
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        radtxtPassword.Text = string.Empty;
        radtxtNewPassword.Text = string.Empty;
        radtxtConformNewPassword.Text = string.Empty;
    }

    protected void radbtnSave_Click(object sender, EventArgs e)
    {     
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }

            objSecurity.ChangeUserPassword(radtxtUserName.Text, radtxtPassword.Text, radtxtNewPassword.Text, Session["UserName"].ToString());
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = objSecurity.Result;
                InitializeControls();               
            }
            else
            {
                lblResult.Text = objSecurity.ErrorMsg;
            }
        }
        catch 
        {
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
    }

    #endregion
}