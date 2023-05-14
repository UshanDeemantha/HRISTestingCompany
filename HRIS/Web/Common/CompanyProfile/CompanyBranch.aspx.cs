using System;
using System.Data;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Common_CompanyProfile_CompanyBranch : System.Web.UI.Page
{
 
    DataTable dtTable = new DataTable();
    CompanyProfile Getcompany = new CompanyProfile();
    CompanyProfile objCompanyProfile = new CompanyProfile();

    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "CompanyBranch";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CompanyId"] != null)
        {
            radcboCompany.Value = Session["CompanyId"].ToString();
        }
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (Session["CompanyId"].ToString() != null && Session["CompanyId"].ToString() != "")
            {
                //   Session["CompanyId"].ToString();



                GEtDataIncompany(Convert.ToInt32(Session["CompanyId"]));
                //Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));
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

                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        radbtnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);

                        if (radbtnDelete.Visible == true || radbtnUpdate.Visible == true)
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
            EnableDisable(true);
        }
    }
    public void GEtDataIncompany(int CompanyId)
    {

        dtTable = Getcompany.GetCompanyProfile(CompanyId);

        radcboCompany.Text = dtTable.Rows[0]["CompanyName"].ToString();


    }
    #endregion

    #region Form Methods
    private void InitializeControls()
    {
        hfCompanyBranchId .Value = string.Empty;
        txtBarnchCode.Text = string.Empty;
        txtLocation.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    public void EnableDisable(bool IsEnable)
    {
        radbtnSave.Enabled = IsEnable;
        radbtnUpdate.Enabled = !IsEnable;
        radbtnDelete.Enabled = !IsEnable;
    }

    #endregion


    #region Button Methods

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objCompanyProfile.AddCompanyBranch(Convert.ToInt32(Session["CompanyId"].ToString()), txtBarnchCode.Text, txtLocation.Text,txtAddress.Text,txtEmail.Text, txtContactPerson.Text, txtContactNo.Text);
            if (!objCompanyProfile.IsError)
            {
                InitializeControls();
                EnableDisable(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Entered...";
            }
            else
            {
                lblResult.Text = objCompanyProfile.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "408px");
        }
        catch(Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "408px");
            lblResult.Text = "Unable to save!";
        }

        radgvDetails.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objCompanyProfile.UpdateCompanyBranch(Convert.ToInt32(hfCompanyBranchId.Value), txtBarnchCode.Text.ToString(), txtLocation.Text.ToString(),txtAddress.Text.ToString(),txtEmail.Text.ToString(), txtContactPerson.Text.ToString(), txtContactNo.Text.ToString());
            if (!objCompanyProfile.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
                InitializeControls();
                EnableDisable(true);
            }
            else
            {
                lblResult.Text = objCompanyProfile.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "408px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "408px");
            lblResult.Text = "Unable to Update!";
        }
        radgvDetails.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DataTable dtTable = new DataTable();
        dtTable = objCompanyProfile.DeleteCompanyBranchwise(Convert.ToInt32(Session["CompanyId"]));

        try
        {
            if (dtTable.Rows.Count > 0)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Cannot Delete This Record";
            }
            else
            {
                objCompanyProfile.DeleteCompanyBranch(Convert.ToInt32(hfCompanyBranchId.Value));
                if (!objCompanyProfile.IsError)
                {
                    lblResult.ForeColor = Color.Green;
                    lblResult.Text = "Successfully Deleted...";
                    InitializeControls();
                    EnableDisable(true);
                }
                else
                {
                    lblResult.Text = "Unable to Delete!" + objCompanyProfile.ErrorMsg;
                }
                formContainer.Attributes.CssStyle.Add("height", "408px");

                radgvDetails.DataBind();
            }

   
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "408px");
            lblResult.Text = "Unable to Delete";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        lblResult.Text = string.Empty;
        EnableDisable(true);
       // formContainer.Attributes.CssStyle.Add("height", "408px");
    }

    #endregion

    #region GridView Databind

    private void BindData(DataTable dtTable)
    {
        txtBarnchCode.Text = dtTable.Rows[0]["BranchCode"].ToString();
        txtLocation.Text = dtTable.Rows[0]["Location"].ToString();
        txtEmail.Text = dtTable.Rows[0]["Email"].ToString();
        txtContactPerson.Text = dtTable.Rows[0]["CountactPerson"].ToString();
        txtContactNo.Text = dtTable.Rows[0]["ContactNo"].ToString();
        txtAddress.Text = dtTable.Rows[0]["Address"].ToString();
       
    }

    //protected void radgvDetails_ItemCommand(object sender, GridCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        try
    //        {
    //            GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
    //            int id = Convert.ToInt32(dataItem.GetDataKeyValue("BranchId"));
    //            int companyid = Convert.ToInt32(dataItem.GetDataKeyValue("CompanyId"));
    //            DataTable dtBranch = objCompanyProfile.GetCompanyBranch(id, companyid);

    //            if (dtBranch.Rows.Count > 0)
    //            {
    //                BindData(dtBranch);
    //                hfCompanyBranchId.Value = dtBranch.Rows[0]["BranchId"].ToString();
    //                EnableDisable(false);
    //            }
    //        }
    //        catch
    //        {
    //            if (radgvDetails.SelectedItems.Count <= 0)
    //            {
    //                lblResult.ForeColor = Color.Red;
    //                lblResult.Text = "Select item for modify!";
    //            }
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

    #endregion
    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            EnableDisable(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfCompanyBranchId.Value = radgvDetails.GetRowValues(index, "BranchId").ToString();
           // hfCompanyBranchId.Value = radgvDetails.GetRowValues(index, "CompanyId").ToString();
            txtBarnchCode.Text = radgvDetails.GetRowValues(index, "BranchCode").ToString();
            txtLocation.Text = radgvDetails.GetRowValues(index, "Location").ToString();
            txtEmail.Text = radgvDetails.GetRowValues(index, "Email").ToString();
            txtContactPerson.Text = radgvDetails.GetRowValues(index, "CountactPerson").ToString();
            txtContactNo.Text = radgvDetails.GetRowValues(index, "ContactNo").ToString();
            txtAddress.Text = radgvDetails.GetRowValues(index, "Address").ToString();
            formContainer.Attributes.CssStyle.Add("height", "408px");


        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "408px");
            lblResult.Text = "Cannot Updated!";
        }

    }

    //protected void radcboCompany_DataBinding(object sender, EventArgs e)
    //{
    //    objCompanyProfile.setcompany(radcboCompany.Text);
    //}
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public String downloadExcelFromGrid()
    //{


    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    return js.Serialize("ok");
    //}


    protected void RadButton1_Click(object sender, EventArgs e)
    {
        radgvDetails.Columns[8].Visible = false;
        radgvDetails.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
     
      
    }
}