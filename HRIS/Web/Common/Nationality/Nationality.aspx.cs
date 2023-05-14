

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Nationality_Nationality : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "Nationality";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Reference objNationality = new Reference();
    DataTable dtNationality = new DataTable();

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
                    btnSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;

                    ViewState["IsModify"] = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSave.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;

                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);

                        if (btnDelete.Visible == true || btnUpdate.Visible == true)
                        {
                            ViewState["IsModify"] = true;
                        }
                    }
                }
            }
        }

        lblResult.Text = string.Empty;
        lblResult.ForeColor = Color.Red;

        if (!IsPostBack)
        {
            InitializeControls();
            EnabledDisabled(true);
            ViewState["dtNationality"] = dtNationality;
        }
    }

    #region Methods

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfNationalityId.Value = dtTable.Rows[0]["NationalityID"].ToString();
            txtNationalityName.Text = dtTable.Rows[0]["NationalityName"].ToString();
            ddlCountry.SelectedValue = dtTable.Rows[0]["Country"].ToString();
        }
    }

    private void InitializeControls()
    {
        hfNationalityId.Value = string.Empty;
        txtNationalityName.Text = string.Empty;       
        ddlCountry.SelectedIndex =-1;
    }

    private void EnabledDisabled(bool Enabled)
    {
        btnSave.Enabled = Enabled;
        btnUpdate.Enabled = !Enabled;
        btnDelete.Enabled = !Enabled;
    }

    private bool IsValidate()
    {
        if (ddlCountry.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Country!";
            return false;
        }

        return true;
    }

    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidate())
            {
                return;
            }
            objNationality.AddNationality(ddlCountry.SelectedItem.Text,txtNationalityName.Text);
            if (!objNationality.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved...";
            }
            else
            {
                lblResult.Text = objNationality.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save!";
        }
        radgvDetails.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidate())
            {
                return;
            }
            objNationality.UpdateNationality(Convert.ToInt32(hfNationalityId.Value),ddlCountry.SelectedItem.Text, txtNationalityName.Text);
            if (!objNationality.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
            }
            else
            {
                lblResult.Text = objNationality.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save!";
        }
        radgvDetails.DataBind();       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objNationality.DeleteNationality(Convert.ToInt32(hfNationalityId.Value));
        if (!objNationality.IsError)
        {
            InitializeControls();
            EnabledDisabled(true);
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objNationality.ErrorMsg;
        }
        radgvDetails.DataBind();       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnabledDisabled(true);
        radgvDetails.DataBind();
    }

    #endregion

    #region Grid View
    
    protected void radgvDetails_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
                BindData(objNationality.GetNationality(Convert.ToInt32(dataItem.GetDataKeyValue("NationalityID"))));
                EnabledDisabled(false);
            }
            catch
            {
                if (radgvDetails.SelectedItems.Count <= 0)
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = "Select item for modify!";
                }
            }
        }
    }

    protected void radgvDetails_ItemCreated(object sender, GridItemEventArgs e)
    {
        #region Grid Permitions

        GridItem cmdItem = radgvDetails.MasterTableView.GetItems(GridItemType.CommandItem)[0];
        LinkButton lbtnEdit = cmdItem.FindControl("radgrdbtnEditSelected") as LinkButton;
        lbtnEdit.Visible = false;

        if (Convert.ToBoolean(ViewState["IsModify"]) == true)
        {
            lbtnEdit.Visible = true;
        }
        else
        {
            lbtnEdit.Visible = false;
        }

        #endregion
    }

    #endregion
}
