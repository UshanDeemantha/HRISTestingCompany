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

public partial class Common_Settings_NewAdminForms_SystemMenu : System.Web.UI.Page
{
    MksSecurity objSecurity = new MksSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void InitializeControls()
    {
        radtxtMenuName.Text = string.Empty;
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
            objSecurity.AddMenu(radcboApplicationId.SelectedValue.ToString(), radtxtMenuName.Text);
            if (objSecurity.IsSuccess)
            {
                lblResult.ForeColor = Color.Blue;
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.UpdateMenu(hfMenuId.Value, radcboApplicationId.SelectedValue.ToString(), radtxtMenuName.Text);
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
            radgvDetails.DataBind();
        }
        catch
        {
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.DeleteMenu(Convert.ToString(hfMenuId.Value));
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
            radgvDetails.DataBind();
        }
        catch
        {
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnableDisable(true);
        radcboApplicationId.SelectedIndex = -1;
    }

    protected void radcboApplicationId_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select >>", "0");
        radcboApplicationId.Items.Insert(0, newItem);
    }
    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        hfApplicationId.Value = gridView.GetRowValues(index, "ApplicationId").ToString();
        hfMenuId.Value = gridView.GetRowValues(index, "MenuId").ToString();
        DataTable dtMenu = objSecurity.GetMenu(hfApplicationId.Value, hfMenuId.Value);
        if (dtMenu.Rows.Count > 0)
        {
            hfMenuId.Value = dtMenu.Rows[0]["MenuId"].ToString();
            radtxtMenuName.Text = dtMenu.Rows[0]["MenuName"].ToString();
        }
        EnableDisable(false);
    }
}