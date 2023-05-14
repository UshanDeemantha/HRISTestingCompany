using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Common_Settings_NewAdminForms_SystemApplication : System.Web.UI.Page
{
    MksSecurity objSecurity = new MksSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            EnableDisable(true);
        }
    }
    public void InitializeControls()
    {
        radtxtApplicationCode.Text = string.Empty;
        radtxtApplicationName.Text = string.Empty;
    }
    public void EnableDisable(bool IsEnable)
    {
        btnSaves.Enabled = IsEnable;
        btnUpdate.Enabled = !IsEnable;
        btnDelete.Enabled = !IsEnable;
    }
    protected void btnSaves_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.AddApplication(radtxtApplicationCode.Text, radtxtApplicationName.Text);
            if (objSecurity.IsSuccess)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = objSecurity.Result;
                InitializeControls();
                EnableDisable(true);
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objSecurity.ErrorMsg;
            }
            radgvDetails.DataBind();
        }
        catch
        {
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.UpdateApplication(hfApplicationId.Value, radtxtApplicationCode.Text,
                radtxtApplicationName.Text);
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Record Updated Successfully..";
                InitializeControls();
                EnableDisable(true);
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objSecurity.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Some Error Occured!";
        }
        radgvDetails.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.DeleteApplication(Convert.ToString(hfApplicationId.Value));
            if (objSecurity.IsSuccess)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objSecurity.Result;
                InitializeControls();
                EnableDisable(true);
                radgvDetails.DataBind();
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objSecurity.ErrorMsg;
            }
            EnableDisable(true);
        }
        catch
        {
            lblResult.Text = "Some Error Occured!";
        }
        radgvDetails.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnableDisable(true);
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        hfApplicationId.Value = gridView.GetRowValues(index, "ApplicationId").ToString();
        DataTable dtApplication = objSecurity.GetApplication(hfApplicationId.Value);
        if (dtApplication.Rows.Count > 0)
        {
            hfApplicationId.Value = dtApplication.Rows[0]["ApplicationId"].ToString();
            radtxtApplicationCode.Text = dtApplication.Rows[0]["ApplicationCode"].ToString();
            radtxtApplicationName.Text = dtApplication.Rows[0]["ApplicationName"].ToString();
        }
        EnableDisable(false);
    }
}