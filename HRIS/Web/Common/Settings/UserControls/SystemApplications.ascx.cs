using System;
using System.Data;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Common_Settings_UserControls_SystemApplications : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "SystemApplications";

    MksSecurity objSecurity = new MksSecurity();

    #endregion
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
    #region Form Methods
    public void InitializeControls()
    {
        radtxtApplicationCode.Text = string.Empty;
        radtxtApplicationName.Text = string.Empty;
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

    protected void radbtnUpdate_Click(object sender, EventArgs e)
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

    protected void radbtnDelete_Click(object sender, EventArgs e)
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

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnableDisable(true);
    }

    protected void radgvDetails_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            DataTable dtApplication = objSecurity.GetApplication(dataItem.GetDataKeyValue("ApplicationId").ToString());
            if (dtApplication.Rows.Count > 0)
            {
                hfApplicationId.Value = dtApplication.Rows[0]["ApplicationId"].ToString();
                radtxtApplicationCode.Text = dtApplication.Rows[0]["ApplicationCode"].ToString();
                radtxtApplicationName.Text = dtApplication.Rows[0]["ApplicationName"].ToString();
            }
            EnableDisable(false);
        }
    }

    #endregion
}