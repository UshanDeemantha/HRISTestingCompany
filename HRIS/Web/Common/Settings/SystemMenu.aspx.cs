using System;
using System.Data;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Settings_SystemMenu : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "SystemMenus";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

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
            radcboApplicationId.SelectedIndex = -1;
            EnableDisable(true);
        }
    }

    #endregion

    #region Form Methods
    public void InitializeControls()
    { 
        radtxtMenuName.Text = string.Empty;
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
            objSecurity.AddMenu(radcboApplicationId.SelectedValue.ToString(), radtxtMenuName.Text);
            if (objSecurity.IsSuccess)
            {
                lblResult.ForeColor = Color.Green;
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
            objSecurity.UpdateMenu(hfMenuId.Value,  radcboApplicationId.SelectedValue.ToString(), radtxtMenuName.Text);
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

    protected void radbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.DeleteMenu(Convert.ToString(hfMenuId.Value));
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
        radcboApplicationId.SelectedIndex = -1;
    }

    #endregion

    #region Data Grid Data Bind

    protected void radgvDetails_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            DataTable dtMenu = objSecurity.GetMenu(
                dataItem.GetDataKeyValue("ApplicationId").ToString(),
                dataItem.GetDataKeyValue("MenuId").ToString());
            if (dtMenu.Rows.Count > 0)
            {
                hfMenuId.Value = dtMenu.Rows[0]["MenuId"].ToString();
                radtxtMenuName.Text = dtMenu.Rows[0]["MenuName"].ToString();
            }
            EnableDisable(false);
        }
    }

    #endregion

    #region ComboBox Data Bind

    protected void radcboApplicationId_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select >>", "0");
        radcboApplicationId.Items.Insert(0, newItem);
    }

    #endregion    
}
    