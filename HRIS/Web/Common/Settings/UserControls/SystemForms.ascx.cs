using System;
using System.Data;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Common_Settings_UserControls_SystemForms : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "SystemForms";

    MksSecurity objSecurity = new MksSecurity();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            radcboApplication.SelectedIndex = -1;
            radcboMenu.SelectedIndex = -1;
            EnableDisable(true);
        }
    }

    #region Form Methods
    public void InitializeControls()
    {
        radtxtFormDescription.Text = string.Empty;
        radntxtFormIndexNo.Text = string.Empty;
        radtxtFormName.Text = string.Empty;
    }

    public void EnableDisable(bool IsEnable)
    {
        radbtnSave.Enabled = IsEnable;
        radbtnUpdate.Enabled = !IsEnable;
        radbtnDelete.Enabled = !IsEnable;
    }

    #endregion

    #region Buttons
    protected void radbtnSave_Click(object sender, EventArgs e)
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

    protected void radbtnUpdate_Click(object sender, EventArgs e)
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

    protected void radbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.DeleteForm(Convert.ToString(hfFormId.Value));
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
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

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        radcboApplication.SelectedIndex = -1;
        radcboMenu.SelectedIndex = -1;
        EnableDisable(true);
    }
    #endregion

    #region Data Grid Data Bind
    protected void radgvDetails_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            DataTable dtApplication = objSecurity.GetForm(
                dataItem.GetDataKeyValue("MenuId").ToString(),
                dataItem.GetDataKeyValue("FormId").ToString());

            if (dtApplication.Rows.Count > 0)
            {
                hfFormId.Value = dtApplication.Rows[0]["FormId"].ToString();
                radtxtFormName.Text = dtApplication.Rows[0]["FormName"].ToString();
                radtxtFormDescription.Text = dtApplication.Rows[0]["FormDescription"].ToString();
                radntxtFormIndexNo.Value = Convert.ToInt32(dtApplication.Rows[0]["FormIndexNo"]);
            }
            EnableDisable(false);
        }
    }
    #endregion

    #region ComboBox Data Bind
    protected void radcboApplication_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select >>", "0");
        radcboApplication.Items.Insert(0, newItem);
    }

    protected void radcboMenu_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select >>", "0");
        radcboMenu.Items.Insert(0, newItem);
    }
    #endregion
}