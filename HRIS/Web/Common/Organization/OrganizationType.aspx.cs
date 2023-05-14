using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Organization_OrganizationType : System.Web.UI.Page
{
    #region Security
    private const string ApplicationName = "Common";
    private const string PageName = "OrganizationTypes";
    MksSecurity objSecurity = new MksSecurity();
    #endregion

    Organization objOrganization = new Organization();
    DataTable dtOrganizationTypes;
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

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;
        if (!Page.IsPostBack)
        {
            InitializeControls();
            EnabledDisabled(true);
            if (Session["CompanyId"] != null)
            {
                radcboCompany.SelectedValue = Session["CompanyId"].ToString();
            }
            else
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }
        }
    }

    #region Methods
    private void InitializeControls()
    {
        hfOrgTypeID.Value = "0";
        txtOrganizationTypeCode.Text = string.Empty;
        txtOrganizationName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        radtxtFax.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddlIndex.SelectedIndex = -1;
        ddlOrganizationLevel.SelectedIndex = -1;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void EnabledDisabled(bool Enabled)
    {
        btnSave.Enabled = Enabled;
        btnUpdate.Enabled = !Enabled;
        btnDelete.Enabled = !Enabled;
    }

    private void DataBindToControl(int OrganizationTypeID)
    {
        DataTable dtTable = objOrganization.GetOrganizationTypes(Convert.ToInt32(radcboCompany.SelectedValue), OrganizationTypeID);
        if (dtTable.Rows.Count > 0)
        {
            hfOrgTypeID.Value = dtTable.Rows[0]["OrganizationTypeID"].ToString();
            txtOrganizationTypeCode.Text = dtTable.Rows[0]["OrganizationTypeCode"].ToString();
            txtOrganizationName.Text = dtTable.Rows[0]["OrganizationTypeName"].ToString();
            txtAddress.Text = dtTable.Rows[0]["Address"].ToString();
            txtContactNo.Text = dtTable.Rows[0]["ContactNo"].ToString();
            txtEmail.Text = dtTable.Rows[0]["Email"].ToString();
            radtxtFax.Text = dtTable.Rows[0]["Fax"].ToString();
            ddlIndex.Value = dtTable.Rows[0]["OrganizationalIndex"].ToString();
            ddlOrganizationLevel.Value = dtTable.Rows[0]["OrganizationalLevelID"].ToString();
        }
    }

    private bool IsValidate()
    {
        if (ddlOrganizationLevel.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Department Level!";
            return false;
        }
        return true;
    }
    #endregion

    #region DropDown
    protected void radcboCompany_DataBound(object sender, EventArgs e)
    {
        radcboCompany.Items.Insert(0, new RadComboBoxItem("<< Select Company >>", "0"));
    }

    protected void ddlOrganizationLevel_DataBound(object sender, EventArgs e)
    {
        //ddlOrganizationLevel.Items.Insert(0, new DevExpress.Web.ListEditItem("<< Select Department Level >>", "0"));
    }

    protected void radcboCompany_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
    #endregion

    //#region Grid View

    //protected void radgvDetails_ItemCommand(object sender, GridCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "Select")
    //        {
    //            GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
    //            DataBindToControl(Convert.ToInt32(dataItem.GetDataKeyValue("OrganizationTypeID")));
    //        }
    //        EnabledDisabled(false);
    //    }
    //    catch
    //    {
    //        if (radgvDetails.SelectedItems.Count <= 0)
    //        {
    //            lblResult.ForeColor = Color.Red;
    //            lblResult.Text = "Select item for modify!";
    //            InitializeControls();
    //            EnabledDisabled(true);
    //        }
    //    }
    //}

    //protected void radgvDetails_ItemCreated(object sender, GridItemEventArgs e)
    //{
    //    #region Grid Permitions

    //    GridItem cmdItem = radgvDetails.MasterTableView.GetItems(GridItemType.CommandItem)[0];
    //    LinkButton lbtnEdit = cmdItem.FindControl("radgrdbtnEditSelected") as LinkButton;
    //    lbtnEdit.Visible = false;

    //    if (Convert.ToBoolean(ViewState["IsModify"]) == true)
    //    {
    //        lbtnEdit.Visible = true;
    //    }
    //    else
    //    {
    //        lbtnEdit.Visible = false;
    //    }

    //    #endregion
    //}

    //#endregion

    #region Buttons
    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            objOrganization.AddOrganizationType(txtOrganizationTypeCode.Text, txtOrganizationName.Text, Convert.ToInt32(ddlOrganizationLevel.Value), Convert.ToInt32(ddlIndex.Value), txtAddress.Text, txtContactNo.Text, radtxtFax.Text, txtEmail.Text, Convert.ToInt32(radcboCompany.SelectedValue));
            if (!objOrganization.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved...";
                InitializeControls();
                EnabledDisabled(true);
                radgvDetails.DataBind();
            }
            else
            {
                if (objOrganization.ErrorNo == 2601)
                {
                    lblResult.Text = "Cannot duplicate  Department Type ";
                }
                else
                    lblResult.Text = objOrganization.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "425px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "425px");
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            objOrganization.UpdateOrganizationType(Convert.ToInt32(hfOrgTypeID.Value), txtOrganizationTypeCode.Text, txtOrganizationName.Text, 
                Convert.ToInt32(ddlOrganizationLevel.Value), Convert.ToInt32(ddlIndex.Value), txtAddress.Text, txtContactNo.Text, radtxtFax.Text, txtEmail.Text);
            if (!objOrganization.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
                InitializeControls();
                EnabledDisabled(true);
                radgvDetails.DataBind();
            }
            else
            {
                if (objOrganization.ErrorNo == 2601)
                {
                    lblResult.Text = "Cannot duplicate  Department Type";
                }
                else
                    lblResult.Text = objOrganization.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "425px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "425px");
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void radbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objOrganization.DeleteOrganizationType(Convert.ToInt32(hfOrgTypeID.Value));
            if (!objOrganization.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Deleted...";
                InitializeControls();
                EnabledDisabled(true);
                radgvDetails.DataBind();
            }
            else
            {
                lblResult.Text = objOrganization.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "425px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "425px");
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnabledDisabled(true);
        //formContainer.Attributes.CssStyle.Add("height", "425px");
    }
    #endregion
    protected void ddlOrganizationLevel_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dtOrganizationIndex = objOrganization.GetOrganizationLevel1(Convert.ToInt32(ddlOrganizationLevel.Value));

        if (dtOrganizationIndex.Rows.Count > 0)
        {
            string ChildTypeId = dtOrganizationIndex.Rows[0]["OrganizationalIndex"].ToString();
            ddlIndex.Value = ChildTypeId;
        }
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            EnabledDisabled(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfOrgTypeID.Value = radgvDetails.GetRowValues(index, "OrganizationTypeID").ToString();
            txtOrganizationTypeCode.Text = radgvDetails.GetRowValues(index, "OrganizationTypeCode").ToString();
            txtOrganizationName.Text = radgvDetails.GetRowValues(index, "OrganizationTypeName").ToString();
            txtAddress.Text = radgvDetails.GetRowValues(index, "Address").ToString();
            txtContactNo.Text = radgvDetails.GetRowValues(index, "ContactNo").ToString();
            txtEmail.Text = radgvDetails.GetRowValues(index, "Email").ToString();
            radtxtFax.Text = radgvDetails.GetRowValues(index, "Fax").ToString();
            ddlIndex.Value = radgvDetails.GetRowValues(index, "OrganizationalIndex").ToString();
            ddlOrganizationLevel.Value = radgvDetails.GetRowValues(index, "OrganizationalLevelID").ToString();
            formContainer.Attributes.CssStyle.Add("height", "425px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "425px");
            lblResult.Text = "";
        }
    }

    protected void ddlOrganizationLevel_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataTable dtOrganizationIndex = objOrganization.GetOrganizationLevel1(Convert.ToInt32(ddlOrganizationLevel.Value));
        if (dtOrganizationIndex.Rows.Count > 0)
        {
            string ChildTypeId = dtOrganizationIndex.Rows[0]["OrganizationalIndex"].ToString();
            ddlIndex.Value = ChildTypeId;
        }
        formContainer.Attributes.CssStyle.Add("height", "425px");
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String downloadExcelFromGrid()
    {


        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize("ok");
    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        radgvDetails.Columns[10].Visible = false;
        radgvDetails.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
     
    }
}