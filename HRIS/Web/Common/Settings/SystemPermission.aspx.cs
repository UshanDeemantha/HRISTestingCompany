using System;
using System.Data;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Settings_SystemPermission : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "SystemPermission";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    #region Page_Load

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
                    radbtnUpdate.Visible = false;
                    radbtnDelete.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                        radbtnUpdate.Visible = true;
                        radbtnDelete.Visible = true;
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        radbtnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                    }
                }
            }
        }

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            EnableDisable(true);
        }
    }

    #endregion

    #region Form Methods

    public void InitializeControls()
    {
        radtxtPermission.Text = string.Empty;
        radtxtRemarks.Text = string.Empty;
        radntxtPermissionOrderNo.Text = string.Empty;
    }
    
    public void EnableDisable(bool IsEnable)
    {
        radbtnSave.Enabled = IsEnable;
        radbtnUpdate.Enabled = !IsEnable;
        radbtnDelete.Enabled = !IsEnable;
    }

    #endregion

    #region Button Methods

    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.AddPermission(radtxtPermission.Text, Convert.ToInt32(radntxtPermissionOrderNo.Value), radtxtRemarks.Text);
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor =  Color.Green;
                lblResult.Text = objSecurity.Result;
                InitializeControls();
            }
            else
            {
                lblResult.Text = objSecurity.ErrorMsg;
            }
            radgvDetails.DataBind();
        }
        catch 
        {
            lblResult.Text = "Some Error Occured!";            
        }       
    }

    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.UpdatePermission(hfPermissionId.Value, radtxtPermission.Text, Convert.ToInt32(radntxtPermissionOrderNo.Value), radtxtRemarks.Text);
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Record Updated Successfully.";
                InitializeControls();
                EnableDisable(true);
            }
            else
            {
                lblResult.Text = objSecurity.ErrorMsg;
            }
            radgvDetails.DataBind();          
        }
        catch 
        {
            lblResult.Text = "Some Error Occured!";            
        }        
    }

    protected void radbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.DeletePermission(hfPermissionId.Value);

            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Record Deleted Successfully.";
                InitializeControls();
                EnableDisable(true);
            }
            else
            {
                lblResult.Text = objSecurity.ErrorMsg;
            }

            radgvDetails.DataBind();           
        }
        catch 
        {
            lblResult.Text = "Some Error Occured!";
        }        
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnableDisable(true);
    }

    #endregion
     
    #region Data Bind

    protected void radgvDetails_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            DataTable dtPermission = objSecurity.GetPermission(dataItem.GetDataKeyValue("PermissionId").ToString());

            if (dtPermission.Rows.Count > 0)
            {
                hfPermissionId.Value = dtPermission.Rows[0]["PermissionId"].ToString();
                radtxtPermission.Text = dtPermission.Rows[0]["Permission"].ToString();
                radntxtPermissionOrderNo.Value = Convert.ToUInt32(dtPermission.Rows[0]["PermissionOrderNo"]);
                radtxtRemarks.Text = dtPermission.Rows[0]["Remarks"].ToString();
            }

            EnableDisable(false);
        }    
    }

    #endregion

    protected void radtxtRemarks_TextChanged(object sender, EventArgs e)
    {
    }
}