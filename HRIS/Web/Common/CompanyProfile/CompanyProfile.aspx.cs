using System;
using System.Data;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using DevExpress.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;


public partial class CompanyProfile_CompanyProfile : System.Web.UI.Page
{
    DataTable dtSelectedSeats = new DataTable();
    public int namesCounter;
    CompanyProfile objCompanyProfile = new CompanyProfile();
    DataTable bankDetails = new DataTable();
    private int _companyId = 0;
    public int CompanyId
    {
        get { return _companyId; }
    }
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "CompanyProfile";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {

        dtSelectedSeats.Columns.Add("Id", typeof(int));
        dtSelectedSeats.Columns.Add("BankId", typeof(int));
        dtSelectedSeats.Columns.Add("BranchId", typeof(int));
        dtSelectedSeats.Columns.Add("BankName", typeof(string));
        dtSelectedSeats.Columns.Add("Branch", typeof(string));
        dtSelectedSeats.Columns.Add("AccountNumber", typeof(string));
        dtSelectedSeats.Columns.Add("Description", typeof(string));
        dtSelectedSeats.Columns.Add("UserName", typeof(string));
 
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

                    int i = 0;
                    ViewState["recordIndex"] = i;

                }
                if (NameStatus.Checked == true)
                {
                    txtCusCode.Text = "All the Employee's Name Shown as ''Intials + Last Name''";
                }
                else
                {
                    txtCusCode.Text = "All the Employee's Name Shown as ''First Name + Last Name''";
                }

                if (cbCustom.Checked == true)
                {
                    txtPrefix.Enabled = true;
                }
                else
                {
                    txtPrefix.Enabled = false;
                }

               
                Session["dt"] = dtSelectedSeats;
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

    #endregion

    #region Form Methods

    private void InitializeControls()
    {
        hfCompanyId.Value = string.Empty;
        txtCompanyCode.Text = string.Empty;
        txtCompanyName.Text = string.Empty;
        txtCompanyAddress.Text = string.Empty;
        txtContactNo1.Text = string.Empty;
        txtContactNo2.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtWeb.Text = string.Empty;
        txtRegistrationNo.Text = string.Empty;
        txtTaxRegNo.Text = string.Empty;
        txtPrefix.Text = string.Empty;
        cbCustom.Checked = false;
        txtPrefix.Enabled = false;
        ddlReportViewName.SelectedIndex = 0;
        txtCusCode.Text = "All the Employee's Name Shown as ''Intials + Last Name''";
        NameStatus.Checked = true;

        radtxtCompanyAccountNo.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtBankBranch.Text = string.Empty;
        radtxtCompanyAccountNo2.Text = string.Empty;
        btnADDCOM.Visible = true;
        btnPRVICOM.Visible = false;
        btnNEXTCOM.Visible = false;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    public void EnableDisable(bool IsEnable)
    {
        btnSave.Enabled = IsEnable;
        btnUpdate.Enabled = !IsEnable;
        btnDelete.Enabled = !IsEnable;
    }

    #endregion

    #region Button Methods 

    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        try
        {
            if (Validate("Add"))
            {
                string prefix = "";
                if (cbCustom.Checked)
                {
                    prefix = txtPrefix.Text.PadLeft(3, '0');
                }
                objCompanyProfile.AddCompanyProfile(txtCompanyCode.Text, txtCompanyName.Text, txtCompanyAddress.Text, txtContactNo1.Text, txtContactNo2.Text, txtFax.Text,
                    txtEmail.Text, txtWeb.Text, txtRegistrationNo.Text, txtTaxRegNo.Text, Convert.ToBoolean(NameStatus.Checked), radtxtCompanyAccountNo.Text.ToString(), 0, 0,
                     radtxtCompanyAccountNo2.Text.ToString(), 0, 0, cbCustom.Checked, prefix,Convert.ToInt32(ddlReportViewName.Value));
                _companyId = objCompanyProfile.CompanyId;
               // int i = _companyId;
                Session["_companyId"] = _companyId;
                dtSelectedSeats = (DataTable)Session["dt"];
                foreach (DataRow item in dtSelectedSeats.Rows)
                {
              
                    objCompanyProfile.SaveNewCompanyBankDetails(Convert.ToInt32(Session["_companyId"]), Convert.ToInt32(item["BankId"].ToString()), Convert.ToInt32(item["BranchId"].ToString()), item["AccountNumber"].ToString(), item["Description"].ToString(), Session["UserName"].ToString());
           
                }
               

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
                formContainer.Attributes.CssStyle.Add("height", "585px");
                dtSelectedSeats = (DataTable)Session["dt"];

                for (int i = ((DataTable)Session["dt"]).Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = ((DataTable)Session["dt"]).Rows[i];
                    dr.Delete();
                    ((DataTable)Session["dt"]).AcceptChanges();
                    gvPopup.DataSource = dtSelectedSeats;
                    gvPopup.DataBind();
                }
            }
        }
        catch(Exception ex)
        {
            lblResult.Text = "Unable to save!";
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }

        radgvDetails.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Validate("Update"))
            {
                string prefix = "";
                if (cbCustom.Checked)
                {
                    prefix = txtPrefix.Text.PadLeft(3, '0');
                }
                objCompanyProfile.UpdateCompanyProfile(
                     Convert.ToInt32(hfCompanyId.Value), txtCompanyCode.Text.ToString(), txtCompanyName.Text.ToString(), txtCompanyAddress.Text.ToString(), txtContactNo1.Text.ToString(), txtContactNo2.Text.ToString(), txtFax.Text.ToString(), txtEmail.Text.ToString(),
                     txtWeb.Text.ToString(), txtRegistrationNo.Text.ToString(), txtTaxRegNo.Text.ToString(), Convert.ToBoolean(NameStatus.Checked), radtxtCompanyAccountNo.Text.ToString(), Convert.ToInt32(0), Convert.ToInt32(0),
                     radtxtCompanyAccountNo2.Text.ToString(), Convert.ToInt32(0), Convert.ToInt32(0), cbCustom.Checked, prefix, Convert.ToInt32(ddlReportViewName.Value));
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
                formContainer.Attributes.CssStyle.Add("height", "585px");
            }
        }
        catch(Exception ex)
        {
            lblResult.Text = "Unable to Update!";
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }
        radgvDetails.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objCompanyProfile.DeleteCompanyProfile(Convert.ToInt32(hfCompanyId.Value));
            if (!objCompanyProfile.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Deleted...";
                EnableDisable(true);
            }
            else
            {
                lblResult.Text = "Unable to Delete!" + objCompanyProfile.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }
        catch
        {
            lblResult.Text = "Unable to Delete";
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        lblResult.Text = string.Empty;
        EnableDisable(true);
        cbCustom.Enabled = false;
        //formContainer.Attributes.CssStyle.Add("height", "585px");
    }

    private bool Validate(string Action)
    {
        if (cbCustom.Checked)
        {
            DataTable dtCompnay = objCompanyProfile.GetCompanyProfile(0);
            if (Action == "Add")
            {
                dtCompnay.DefaultView.RowFilter = null;
                dtCompnay.DefaultView.RowFilter = "Prefix = '" + txtPrefix.Text.PadLeft(3, '0') + "'";
                if (dtCompnay.DefaultView.Count > 0)
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = "Prefix Already Exists.";
                    return false;
                }
            }
            else
            {
                dtCompnay.DefaultView.RowFilter = null;
                dtCompnay.DefaultView.RowFilter = "Prefix = '" + txtPrefix.Text.PadLeft(3, '0') + "' And CompanyId <> '"+ hfCompanyId.Value + "'";
                if (dtCompnay.DefaultView.Count > 0)
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = "Prefix Already Exists.";
                    return false;
                }
            }
        }

        return true;
    }

    #endregion

    #region GridView Databind

    private void BindData(DataTable dtTable)
    {
        txtCompanyCode.Text = dtTable.Rows[0]["CompanyCode"].ToString();
        txtCompanyName.Text = dtTable.Rows[0]["CompanyName"].ToString();
        txtCompanyAddress.Text = dtTable.Rows[0]["CompanyAddress"].ToString();
        txtContactNo1.Text = dtTable.Rows[0]["CompanyContactNo1"].ToString();
        txtContactNo2.Text = dtTable.Rows[0]["CompanyContactNo2"].ToString();
        txtFax.Text = dtTable.Rows[0]["CompanyFax"].ToString();
        txtEmail.Text = dtTable.Rows[0]["CompanyEmail"].ToString();
        txtWeb.Text = dtTable.Rows[0]["CompanyWeb"].ToString();
        txtRegistrationNo.Text = dtTable.Rows[0]["CompanyRegistrationNo"].ToString();
        txtTaxRegNo.Text = dtTable.Rows[0]["CompanyTaxRegistrationNo"].ToString();
        NameStatus.Checked = Convert.ToBoolean(dtTable.Rows[0]["EmployeeNameStatus"]);

        bankDetails = objCompanyProfile.GetCompanyBankDetails(Convert.ToInt32(dtTable.Rows[0]["CompanyID"].ToString()));
        if (bankDetails.Rows.Count > 0)
        {
            radtxtCompanyAccountNo.Text = bankDetails.Rows[0]["AccountNumber"].ToString();
            txtBankName.Text = bankDetails.Rows[0]["BankId"].ToString();
            txtBankName.Text = bankDetails.Rows[0]["BranchId"].ToString();
        }


        btnADDCOM.Visible = true;
        btnPRVICOM.Visible = true;
        btnNEXTCOM.Visible = true;
    }
    #endregion

    #region Combobox Databind

    protected void radcboBankName_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Bank >>", "0");
    }

    protected void radcboBankName2_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Bank >>", "0");
    }

    protected void radcboBankBranch2_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem2 = new RadComboBoxItem("<< Select Branch >>", "0");
    }

    #endregion 
    protected void btnPRVICOM_Click(object sender, EventArgs e)
    {
        bankDetails = objCompanyProfile.GetCompanyBankDetails(Convert.ToInt32(hfCompanyId.Value));
        int i = (int)ViewState["recordIndex"];
        i = i - 1;

        if (i >= 0)
        {

            ViewState["recordIndex"] = i;
            System.Data.DataRow row = bankDetails.Rows[0].Table.Rows[i];
            radtxtCompanyAccountNo.Text = row["AccountNumber"].ToString();
            txtBankName.Text = row["BankName"].ToString();
            txtBankBranch.Text = row["Branch"].ToString();
        }
        if (Convert.ToInt32(ViewState["recordIndex"])>0)
            btnNEXTCOM.Enabled = true;
    }
    protected void btnNEXTCOM_Click(object sender, EventArgs e)
    {
        bankDetails = objCompanyProfile.GetCompanyBankDetails(Convert.ToInt32(hfCompanyId.Value));

        int i = (int)ViewState["recordIndex"];
        i = i >= bankDetails.Rows[0].Table.Rows.Count - 1 ? 0 : i + 1;
        PopulateForm(i);
        if (ViewState["recordIndex"].ToString() ==(Convert.ToUInt32( bankDetails.Rows.Count)-1).ToString())
            btnNEXTCOM.Enabled = false;

        btnPRVICOM.Enabled = true;
    }
    protected void btnADDCOM_Click(object sender, EventArgs e)
    {
        ASPxPopupControl1.ShowOnPageLoad = true;
        fillPopupGrid();
        dxPopCmbBank.Value = String.Empty;
        dxPopCmbBranch.Value = String.Empty;
        dxPopTbAcNo.Text = String.Empty;
        dxTbDiscription.Text = String.Empty;
        lblResultPop.Text = "";
        btnPopupAdd.Text = "Save";
    }

    protected void PopulateForm(int i)
    {
        if (i != 0)
        {
            ViewState["recordIndex"] = i;
            System.Data.DataRow row = bankDetails.Rows[0].Table.Rows[i];
            radtxtCompanyAccountNo.Text = row["AccountNumber"].ToString();
            txtBankName.Text = row["BankName"].ToString();
            txtBankBranch.Text = row["Branch"].ToString();
        }
        else
        {

        }

    }
    protected void testBtn_Click(object sender, EventArgs e)
    {

    }
    protected void btnPopupCancel_Click(object sender, EventArgs e)
    {
        if (hfCompanyId.Value == "")
        {
            dxPopCmbBank.Value = "";
            dxPopCmbBranch.Value = "";
            // dxPopCmbBank.Text = string.Empty;
            dxPopTbAcNo.Text = string.Empty;
            dxTbDiscription.Text = string.Empty;
        }
        else
        {
            ASPxPopupControl1.ShowOnPageLoad = false;
            bankDetails = objCompanyProfile.GetCompanyBankDetails(Convert.ToInt32(hfCompanyId.Value));
            if (bankDetails.Rows.Count > 0)
            {
                radtxtCompanyAccountNo.Text = bankDetails.Rows[0]["AccountNumber"].ToString();
                txtBankName.Text = bankDetails.Rows[0]["BankName"].ToString();
                txtBankBranch.Text = bankDetails.Rows[0]["Branch"].ToString();
            }
        }

    }
    protected void btnPopupAdd_Click(object sender, EventArgs e)
    {
        if (PopupValidation())
        {
            if (btnPopupAdd.Text == "Update")
            {
                objCompanyProfile.UpdateCompanyBankDetails(Convert.ToInt32(Session["Id"].ToString()), Convert.ToInt32(dxPopCmbBank.Value), Convert.ToInt32(dxPopCmbBranch.Value), dxPopTbAcNo.Text, dxTbDiscription.Text, Session["UserName"].ToString());
                if (!objCompanyProfile.IsError)
                {
                    dxPopCmbBank.Value = String.Empty;
                    dxPopCmbBranch.Value = String.Empty;
                    dxPopTbAcNo.Text = String.Empty;
                    dxTbDiscription.Text = String.Empty;
                    lblResultPop.ForeColor = Color.Green;
                    lblResultPop.Text = "Successfully Entered...";
                    btnPopupAdd.Text = "Save";
                }
                else
                {
                    lblResult.Text = objCompanyProfile.ErrorMsg;
                }

            }
            else
            {
                if (hfCompanyId.Value == "")
                {
                    if (!objCompanyProfile.IsError)
                    {
                        dxPopCmbBank.Value = String.Empty;
                        dxPopCmbBranch.Value = String.Empty;
                        dxPopTbAcNo.Text = String.Empty;
                        dxTbDiscription.Text = String.Empty;
                        lblResultPop.ForeColor = Color.Green;
                        lblResultPop.Text = "Successfully Entered...";
                    }
                    else
                    {
                        lblResultPop.Text = objCompanyProfile.ErrorMsg;
                    }
                }
                else
                {
                    objCompanyProfile.SaveCompanyBankDetails(Convert.ToInt32(hfCompanyId.Value), Convert.ToInt32(dxPopCmbBank.Value), Convert.ToInt32(dxPopCmbBranch.Value), dxPopTbAcNo.Text, dxTbDiscription.Text, Session["UserName"].ToString());
                    if (!objCompanyProfile.IsError)
                    {
                        dxPopCmbBank.Value = String.Empty;
                        dxPopCmbBranch.Value = String.Empty;
                        dxPopTbAcNo.Text = String.Empty;
                        dxTbDiscription.Text = String.Empty;
                        lblResultPop.ForeColor = Color.Green;
                        lblResultPop.Text = "Successfully Entered...";
                    }
                    else
                    {
                        lblResultPop.Text = objCompanyProfile.ErrorMsg;
                    }
                }

            }

            fillPopupGrid();
        }
    }
    protected void lkSelectPopup_Click(object sender, EventArgs e)
    {
        btnAdd.Text = "Update";
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).BackColor = System.Drawing.Color.Blue;
        if (Session["Id"] == null)
        {
            var BankId = gridView.GetRowValues(index, "BankId").ToString();
            var BranchId = gridView.GetRowValues(index, "BranchId").ToString();
            var AccountNumber = gridView.GetRowValues(index, "AccountNumber").ToString();
            //var Id = gridView.GetRowValues(index, "Id").ToString();
            var Description = gridView.GetRowValues(index, "Description").ToString();

            dxPopCmbBank.Value = BankId;
            dxPopCmbBranch.Value = BranchId;
            dxPopTbAcNo.Text = AccountNumber;
            dxTbDiscription.Text = Description;

            LinkButton btn = (LinkButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;
            object[] values = (object[])container.Grid.GetRowValues(container.VisibleIndex, new string[] { "Id", "BankName", "Branch", "AccountNumber", "Description"});



            Session["idPopUP"] = values[0].ToString();
            dxPopCmbBank.Text = values[1].ToString();
            dxPopCmbBranch.Text = values[2].ToString();
            dxPopTbAcNo.Text = values[3].ToString();
            dxTbDiscription.Text = values[4].ToString();
           

        }
        else
        {
            var BankId = gridView.GetRowValues(index, "BankId").ToString();
            var BranchId = gridView.GetRowValues(index, "BranchId").ToString();
            var AccountNumber = gridView.GetRowValues(index, "AccountNumber").ToString();
            var Id = gridView.GetRowValues(index, "Id").ToString();
            var Description = gridView.GetRowValues(index, "Description").ToString();

            dxPopCmbBank.Value = BankId;
            dxPopCmbBranch.Value = BranchId;
            dxPopTbAcNo.Text = AccountNumber;
            dxTbDiscription.Text = Description;
            Session["Id"] = Id;
            btnPopupAdd.Text = "Update";
        }
   
    }

    protected void fillPopupGrid()
    {
        if (hfCompanyId.Value == "")
        {
            btnAdd.Visible = true;
            btnPopupAdd.Visible = false;
            ASPxPopupControl1.ShowOnPageLoad = true;
            //_companyId = objCompanyProfile.CompanyId;
            // SavePopupData();
        }
        else
        {
            btnAdd.Visible = false;
            DataTable dt = objCompanyProfile.GetCompanyBankDetailsFull(Convert.ToInt32(hfCompanyId.Value));
            gvPopup.DataSource = dt;
            gvPopup.DataBind();
        }
        
    }
    protected void SavePopupData()
    {
        try
        {
            objCompanyProfile.SaveCompanyBankDetails(Convert.ToInt32(Session["_companyId"]), Convert.ToInt32(dxPopCmbBank.Value), Convert.ToInt32(dxPopCmbBranch.Value), dxPopTbAcNo.Text, dxTbDiscription.Text, Session["UserName"].ToString());
            if (!objCompanyProfile.IsError)
            {
                dxPopCmbBank.Value = String.Empty;
                dxPopCmbBranch.Value = String.Empty;
                dxPopTbAcNo.Text = String.Empty;
                dxTbDiscription.Text = String.Empty;
                lblResultPop.ForeColor = Color.Green;
                lblResultPop.Text = "Successfully Entered...";
            }
            else
            {
                lblResultPop.Text = objCompanyProfile.ErrorMsg;
            }
        }
        catch (Exception ex)
        {

        }
       
    }
    protected bool PopupValidation()
    {
        if (dxPopCmbBank.Value == "" || dxPopCmbBank.Value == null)
        {
            lblResultPop.ForeColor = Color.Red;
            lblResultPop.Text = "Please select a bank.";
            return false;
        }
        else if (dxPopCmbBranch.Value == "" || dxPopCmbBranch.Value == null)
        {
            lblResultPop.ForeColor = Color.Red;
            lblResultPop.Text = "Please select a branch.";
            return false;
        }
        else if (dxPopCmbBank.Value == "")
        {
            lblResultPop.ForeColor = Color.Red;
            lblResultPop.Text = "Please insert account no.";
            return false;
        }
        return true;
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
       
        DataTable dtTable = new DataTable();
        try
        {
            EnableDisable(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            
            txtCompanyCode.Text = radgvDetails.GetRowValues(index, "CompanyCode").ToString();
            txtCompanyName.Text = radgvDetails.GetRowValues(index, "CompanyName").ToString();
            txtCompanyAddress.Text = radgvDetails.GetRowValues(index, "CompanyAddress").ToString();
            txtContactNo1.Text = radgvDetails.GetRowValues(index, "CompanyContactNo1").ToString();
            txtContactNo2.Text = radgvDetails.GetRowValues(index, "CompanyContactNo2").ToString();
            txtFax.Text = radgvDetails.GetRowValues(index, "CompanyFax").ToString();
            txtEmail.Text = radgvDetails.GetRowValues(index, "CompanyEmail").ToString();
            txtWeb.Text = radgvDetails.GetRowValues(index, "CompanyWeb").ToString();
            txtRegistrationNo.Text = radgvDetails.GetRowValues(index, "CompanyRegistrationNo").ToString();
            txtTaxRegNo.Text = radgvDetails.GetRowValues(index, "CompanyTaxRegistrationNo").ToString();
            hfCompanyId.Value = radgvDetails.GetRowValues(index, "CompanyID").ToString();
            NameStatus.Checked= Convert.ToBoolean(radgvDetails.GetRowValues(index, "EmployeeNameStatus").ToString());
            if (Convert.ToBoolean(radgvDetails.GetRowValues(index, "EmployeeNameStatus").ToString())==true)
            {
                txtCusCode.Text = "All the Employee's Name Shown as ''Intials + Last Name''";
            }
            else
            {
                txtCusCode.Text = "All the Employee's Name Shown as ''First Name + Last Name''";
            }

                radtxtCompanyAccountNo.Text = radgvDetails.GetRowValues(index, "CompanyAccountNo").ToString();
            cbCustom.Checked = Convert.ToBoolean(radgvDetails.GetRowValues(index, "CustomEmployeeCode").ToString());
            txtPrefix.Enabled = false;
            cbCustom.Enabled = false;
            if (Convert.ToBoolean(radgvDetails.GetRowValues(index, "CustomEmployeeCode").ToString()))
            {
                txtPrefix.Text = radgvDetails.GetRowValues(index, "Prefix").ToString();
            }
            else
            {
                txtPrefix.Text = "";
            }
            ddlReportViewName.Text = (radgvDetails.GetRowValues(index, "EmployeeReportViewName").ToString());
            int companyId = Convert.ToInt32(radgvDetails.GetRowValues(index, "CompanyID").ToString());
            formContainer.Attributes.CssStyle.Add("height", "585px");
            bankDetails = objCompanyProfile.GetCompanyBankDetails(companyId);
            if (bankDetails.Rows.Count > 0)
            {
                radtxtCompanyAccountNo.Text = bankDetails.Rows[0]["AccountNumber"].ToString();
                txtBankName.Text = bankDetails.Rows[0]["BankName"].ToString();
                txtBankBranch.Text = bankDetails.Rows[0]["Branch"].ToString();
               
                btnPRVICOM.Visible = true;
                btnPRVICOM.Enabled = false;

                btnNEXTCOM.Visible = true;
            }
         
            btnAdd.Visible = false;
            btnADDCOM.Visible = true;


        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "585px");

        }
    }

    protected void NameStatus_CheckedChanged(object sender, EventArgs e)
    {
        if (NameStatus.Checked == true)
        {
            txtCusCode.Text = "All the Employee's Name Shown as ''Intials + Last Name''";
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }
        else
        {
            txtCusCode.Text = "All the Employee's Name Shown as ''First Name + Last Name''";
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }
    }

    protected void cbCustomize_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCustom.Checked == true)
        {
            txtPrefix.Enabled=true;
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }
        else
        {
            txtPrefix.Enabled = false;
            formContainer.Attributes.CssStyle.Add("height", "585px");
        }
    }

    protected void ddlReportViewName_SelectedIndexChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "585px");
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String downloadExcelFromGrid()
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize("ok");
    }

    protected void RadButton1_Click2(object sender, EventArgs e)
    {
        radgvDetails.Columns[18].Visible = false;
        radgvDetails.DataBind();
        radgvDetails.Columns[18].Visible = false;
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Century Gothic";
        GridExporter.Styles.Default.Font.Size = 10;
        radgvDetails.Columns[18].Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if(btnAdd.Text == "Add")
        {
            if (Session["dt"] != null)
            {
                dtSelectedSeats = (DataTable)Session["dt"];
                DataRow dr = dtSelectedSeats.NewRow();
                //dr["CompanyId"] = Session["_companyId"].ToString();
                namesCounter = Convert.ToInt32(ViewState["Number"]) + 1;
                ViewState["Number"] = namesCounter;
                dr[0] = namesCounter;
                dr["Id"] = namesCounter;
                dr["BankId"] = dxPopCmbBank.Value;
                dr["BranchId"] = dxPopCmbBranch.Value;
                dr["BankName"] = dxPopCmbBank.Text;
                dr["Branch"] = dxPopCmbBranch.Text;
                dr["AccountNumber"] = dxPopTbAcNo.Text;
                dr["Description"] = dxTbDiscription.Text;
                dr["UserName"] = Session["UserName"].ToString();
                dtSelectedSeats.Rows.Add(dr);
                Session["dt"] = dtSelectedSeats;


                gvPopup.DataSource = dtSelectedSeats;
                gvPopup.DataBind();
                dxPopCmbBank.Value = "";
                dxPopCmbBranch.Value = "";
                // dxPopCmbBank.Text = string.Empty;
                dxPopTbAcNo.Text = string.Empty;
                dxTbDiscription.Text = string.Empty;
               
            }
        }
        else
        {
            if (Session["idPopUP"] != null)
            {
                dtSelectedSeats = (DataTable)Session["dt"];
                for (int i = ((DataTable)Session["dt"]).Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drs = ((DataTable)Session["dt"]).Rows[i];
                    if (drs["id"].ToString() == Session["idPopUP"].ToString())
                        drs.Delete();
                    ((DataTable)Session["dt"]).AcceptChanges();
                    gvPopup.DataSource = dtSelectedSeats;
                    gvPopup.DataBind();
                }
            }
            dtSelectedSeats = (DataTable)Session["dt"];
            DataRow dr = dtSelectedSeats.NewRow();
            //dr["CompanyId"] = Session["_companyId"].ToString();
            namesCounter = Convert.ToInt32(ViewState["Number"]) + 1;
            ViewState["Number"] = namesCounter;
            dr[0] = namesCounter;
            dr["Id"] = namesCounter;
            dr["BankId"] = dxPopCmbBank.Value;
            dr["BranchId"] = dxPopCmbBranch.Value;
            dr["BankName"] = dxPopCmbBank.Text;
            dr["Branch"] = dxPopCmbBranch.Text;
            dr["AccountNumber"] = dxPopTbAcNo.Text;
            dr["Description"] = dxTbDiscription.Text;
            dr["UserName"] = Session["UserName"].ToString();
            dtSelectedSeats.Rows.Add(dr);
            Session["dt"] = dtSelectedSeats;


            gvPopup.DataSource = dtSelectedSeats;
            gvPopup.DataBind();
            dxPopCmbBank.Value = "";
            dxPopCmbBranch.Value = "";
            // dxPopCmbBank.Text = string.Empty;
            dxPopTbAcNo.Text = string.Empty;
            dxTbDiscription.Text = string.Empty;

        }


    }

    protected void Unnamed_Click(object sender, ImageClickEventArgs e)
    {
        ASPxPopupControl1.ShowOnPageLoad = false;
        formContainer.Attributes.CssStyle.Add("height", "585px");

    }
}

