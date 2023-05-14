using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Settings_SystemUserForms : System.Web.UI.Page
{
    #region Security
    private const string ApplicationName = "Common";
    private const string PageName = "SystemUserPermissions";
    MksSecurity objSecurity = new MksSecurity();
    #endregion

    DataTable dtForms = new DataTable();

    #region Page Load

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
        GetPages();
        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;


    }

    #endregion

    #region Form Methods



    private void BindData()
    {

        for (var i = 0; i < rdForms.Items.Count; i++)
        {
            if (chApplication.SelectedIndex >= 0)
            {
                HRM.AccessRights s = objSecurity.GetUserPermissions(txtUserRole.SelectedItem.Text, chApplication.SelectedItem.Text, rdForms.Items[i].Text);
                if (s.ToString() == RbListPermission.SelectedItem.Text)
                {
                    rdForms.Items[i].Selected = true;
                }
                else
                {
                    rdForms.Items[i].Selected = false;
                }
            }
        }



    }

    #endregion

    private bool IsValidate()
    {
        /* if (txtUserRole.Value.ToString().Length <= 0)
         {
             lblResult.Text = "Select User To Set Permission!";
             return false;
         }

         if (radcboApplication.SelectedIndex <= 0)
         {
             lblResult.Text = "Select Application To Set Permission!";
             return false;
         }

         if (radcboMenu.SelectedIndex <= 0)
         {
             lblResult.Text = "Select Menu Name!";
             return false;
         }

         if (radcboPermissions.SelectedIndex <= 0)
         {
             lblResult.Text = "Select Permission Type";
             return false;
         }*/
        return true;
    }
    #region Buttons
    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            for (var i = 0; i < rdForms.Items.Count; i++)
            {
                if (rdForms.Items[i].Selected == true)
                {
                    bool Active = true;
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        Convert.ToString(RbListPermission.Value.ToString()),
                        Convert.ToString(rdForms.Items[i].Value), Active
                    );
                }
                if (rdForms.Items[i].Selected == false)
                {
                    bool Active = false;
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        Convert.ToString(RbListPermission.Value.ToString()),
                        Convert.ToString(rdForms.Items[i].Value), Active
                    );
                }
            }
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = objSecurity.Result;
                //InitializeControls2();

            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objSecurity.ErrorMsg;
            }
        }
        catch
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Some Error Occured!";
        }
    }


    #endregion

    #region ComboBox Data Binds

    private void GetPages()
    {

        try
        {
            DataTable dtLoginTypes = new DataTable();
            dtLoginTypes = objSecurity.GetApplication("0");
            chApplication.DataSource = dtLoginTypes;
            chApplication.TextField = "ApplicationCode";
            chApplication.ValueField = "ApplicationId";
            chApplication.DataBind();


            DataTable dtPermission = new DataTable();
            dtPermission = objSecurity.GetPermission("0");
            RbListPermission.DataSource = dtPermission;
            RbListPermission.TextField = "Permission";
            RbListPermission.ValueField = "PermissionId";
            RbListPermission.DataBind();

        }
        catch (Exception ex)
        {


        }

    }






    #endregion
    protected void txtUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void chApplication_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtLoginTypes = new DataTable();
            dtLoginTypes = objSecurity.GetMenu(chApplication.Value.ToString(), "0");
            chMenu.DataSource = dtLoginTypes;
            chMenu.TextField = "MenuName";
            chMenu.ValueField = "MenuId";
            chMenu.DataBind();

        }
        catch (Exception ex)
        {


        }
    }
    protected void RbListPermission_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void chMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            for (var i = 0; i < chMenu.Items.Count; i++)
            {
                if (!Equals(chMenu.Items[i].Selected, true)) continue;


                //  user.Save_UserRoleList(id, int.Parse(chadminAdd.Items[i].Value.ToString()), true, true, true, 1);
            }


            DataTable dtForms = new DataTable();
            dtForms = objSecurity.GetForm(chMenu.Value.ToString(), "0");
            rdForms.DataSource = dtForms;
            rdForms.TextField = "FormName";
            rdForms.ValueField = "FormId";
            rdForms.DataBind();

        }
        catch (Exception ex)
        {


        }
        BindData();
    }
    protected void radbtnCancel_Click(object sender, EventArgs e)
    {

    }
}