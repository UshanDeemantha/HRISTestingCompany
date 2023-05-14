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

public partial class Common_Settings_NewAdminForms_SystemForms : System.Web.UI.Page
{
    MksSecurity objSecurity = new MksSecurity();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void EnableDisable(bool IsEnable)
    {
        btnSaves.Enabled = IsEnable;
        btnUpdate.Enabled = !IsEnable;
        btnDelete.Enabled = !IsEnable;
    }
    protected void radcboApplication_DataBound(object sender, EventArgs e)
    {

    }

    protected void radcboMenu_DataBound(object sender, EventArgs e)
    {

    }

    protected void btnSaves_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.AddForm(radcboMenu.SelectedValue.ToString(), radtxtFormName.Text, radtxtFormDescription.Text, Convert.ToInt32(radntxtFormIndexNo.Value));
            if (objSecurity.IsSuccess)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = objSecurity.Result;
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.UpdateForm(hfFormId.Value, radtxtFormName.Text, radtxtFormDescription.Text, Convert.ToInt32(radntxtFormIndexNo.Value));
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Record Updated Successfully..";
                InitializeControls();
                EnableDisable(true);
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
        radgvDetails.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.DeleteForm(Convert.ToString(hfFormId.Value));
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Record Deleted Successfully..";
                InitializeControls();
                EnableDisable(true);
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
        radgvDetails.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        radcboApplication.SelectedIndex = -1;
        radcboMenu.SelectedIndex = -1;
        EnableDisable(true);
    }
    public void InitializeControls()
    {
        radtxtFormDescription.Text = string.Empty;
        radntxtFormIndexNo.Text = string.Empty;
        radtxtFormName.Text = string.Empty;
    }
    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        hfMenuId.Value = gridView.GetRowValues(index, "MenuId").ToString();
        hfFormId.Value = gridView.GetRowValues(index, "FormId").ToString();
        DataTable dtApplication = objSecurity.GetForm(hfMenuId.Value, hfFormId.Value);
        if (dtApplication.Rows.Count > 0)
        {
            hfFormId.Value = dtApplication.Rows[0]["FormId"].ToString();
            radtxtFormName.Text = dtApplication.Rows[0]["FormName"].ToString();
            radtxtFormDescription.Text = dtApplication.Rows[0]["FormDescription"].ToString();
            radntxtFormIndexNo.Value = Convert.ToInt32(dtApplication.Rows[0]["FormIndexNo"]);
        }
        //lblResult.Visible = false;
        EnableDisable(false);
    }
}