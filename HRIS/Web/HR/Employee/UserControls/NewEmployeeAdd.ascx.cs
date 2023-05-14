using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using Common.BLL;

using System.Web.Configuration;
using System.IO;

public partial class HR_Employee_UserControls_NewEmployeeAdd : System.Web.UI.UserControl
{
    
    DataTable dtEmployee;
    DataTable dtEmployeeOrg;
    Employee objEmployee;
    DataTable dtBranch;
    bool isResign = false;
    string FileName = string.Empty;
    string ImageFilepath = string.Empty;
    string isPersonalChangeRequest = string.Empty;
    User objEmail = new User();
    CompanyProfile objCompanyProfile = new CompanyProfile();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["EmployeeId"] = 0;
            if (Request.QueryString["EmployeeId"] != null)
            {

                hfCompanyId.Value = Session["CompanyId"].ToString();
                ViewState["EmployeeId"] = Convert.ToInt64(Request.QueryString["EmployeeId"]);
                GetEmployee(Convert.ToInt64(ViewState["EmployeeId"]));

                DataTable dtPersonalChangeInfo = ((DataTable)objEmployee.GetEmployeeOfPersonalInfoChange(Convert.ToInt64(Request.QueryString["EmployeeId"]), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common"));
                if (dtPersonalChangeInfo.Rows.Count > 0)
                {
                    lbtnModify.Visible = true;
                }
                else
                {
                    lbtnModify.Visible = false;
                }

                SetValues();
            }
        }
    }

    #region Form Methods

    public long EmployeeId
    {
        set
        {
            ViewState["EmployeeId"] = value;
            if (ViewState["EmployeeId"] != null)
            {
                GetEmployee(Convert.ToInt64(ViewState["EmployeeId"]));
                SetValues();
               // GetEmployeeOfPersonalInfoChange(Convert.ToInt64(ViewState["EmployeeId"]));
              
            }
        }
    }


    public void GetEmployee(long EmployeeId)
    {
        objEmployee = new Employee();
        dtEmployee = objEmployee.GetEmployee(EmployeeId, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
        dtEmployeeOrg = objEmployee.GetEmployeeOrganizationFlow(EmployeeId);
    }

    public void GetEmployeeOfPersonalInfoChange(long EmployeeId)
    {
        objEmployee = new Employee();
        dtEmployee = objEmployee.GetEmployeeOfPersonalInfoChange(EmployeeId, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
        dtEmployeeOrg = objEmployee.GetEmployeeOrganizationFlow(EmployeeId);
       
    }

    public void SetValues()
    {
        if (dtEmployee.Rows.Count > 0)
        {
            txtEmployeeCode.Text = dtEmployee.Rows[0]["EmployeeCode"].ToString();
            txtEPFNo.Text = dtEmployee.Rows[0]["EPFNO"].ToString();
            txtFirstName.Text = dtEmployee.Rows[0]["FirstName"].ToString();
            txtLastName.Text = dtEmployee.Rows[0]["LastName"].ToString();
            txtFullName.Text = dtEmployee.Rows[0]["FullName"].ToString();
            txtCallName.Text = dtEmployee.Rows[0]["CallName"].ToString();
            txtNameInitials.Text = dtEmployee.Rows[0]["NameWithInitial"].ToString();
            txtDateOfBirth.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["DateOfBirth"]);
            rblGender.SelectedValue = dtEmployee.Rows[0]["Gender"].ToString();
            rblStatus.SelectedValue = dtEmployee.Rows[0]["Status"].ToString();
            ddlJobCatogory.SelectedValue = dtEmployee.Rows[0]["JobCategoryID"].ToString();
            ddlJobGrade.SelectedValue = dtEmployee.Rows[0]["JobGradeID"].ToString();
            ddlEmployementType.SelectedValue = dtEmployee.Rows[0]["EmploymentTypeID"].ToString();
            ddlEmploymentGrade.SelectedValue = dtEmployee.Rows[0]["EmploymentGradeID"].ToString();
            txtRecruitementDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["RecruitementDate"].ToString());
            cmbDesignation.Text = dtEmployee.Rows[0]["DesignationName"].ToString();
            hfDesignationId.Value = dtEmployee.Rows[0]["DesignationID"].ToString();
            hfOrganizationStructure.Value = dtEmployee.Rows[0]["OrgStructureID"].ToString();
            txtOccupationGrade.Text = dtEmployee.Rows[0]["OccupationGrade"].ToString();
            radCmbPayrollAct.SelectedValue = dtEmployee.Rows[0]["PayrollAct"].ToString();
            radCmbCostCenter.SelectedValue = dtEmployee.Rows[0]["CostCenter"].ToString();
            radCmbDirectIndirect.SelectedValue = dtEmployee.Rows[0]["Direct/Indirect"].ToString();
           

            //Set FromDate and ToDate according to EmploymentType
            DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(ddlEmployementType.SelectedValue));
            if (dtEmpTypes.Rows.Count > 0)
            {
                string empTypeCode = dtEmpTypes.Rows[0]["EmploymentTypeCode"].ToString();
                if (empTypeCode == "Probation" || empTypeCode == "FixedTermContarct" || empTypeCode == "Consultancy")
                {
                    //tddpFromDate.Visible = true;
                    tdlblFromDate.Visible = true;
                   // tddpToDate.Visible = true;
                    tdlblToDate.Visible = true;
                }
                else
                {
                    //tddpFromDate.Visible = false;
                    tdlblFromDate.Visible = false;
                    //tddpToDate.Visible = false;
                    tdlblToDate.Visible = false;
                }
                if (empTypeCode == "Permanent")
                {
                    lblConfirmationdate.Visible = true;
                    txtConfirmationDate.Visible = true;
                }
                else
                {
                    lblConfirmationdate.Visible = false;
                    txtConfirmationDate.Visible = false;
                }

                if (empTypeCode == "Probation")
                {
                    dxEmpTypeFromDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["ProbationStartDate"].ToString());
                    dxEmpTypeEndDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["ProbationEndDate"].ToString());
                }
                else if (empTypeCode == "FixedTermContarct")
                {
                    dxEmpTypeFromDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["FixedTermContarctStartDate"].ToString());
                    dxEmpTypeEndDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["FixedTermContarctEndDate"].ToString());
                }
                else if (empTypeCode == "Consultancy")
                {
                    dxEmpTypeFromDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["ConsultancyAgreementStratDate"].ToString());
                    dxEmpTypeEndDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["ConsultancyAgreementEndDate"].ToString());
                }
            }

            if (dtEmployeeOrg.Rows.Count > 0)
            {
                lblOrganization.Text = dtEmployeeOrg.Rows[0]["OrgFlow"].ToString();
            }
            //lblOrganization.Text = dtEmployee.Rows[0]["OrganizationTypeName"].ToString();
            rbEmpCard.SelectedValue = dtEmployee.Rows[0]["EMPCard"].ToString();
            Image1.ImageUrl = "~/HR/EmployeeImages/" + dtEmployee.Rows[0]["Image"].ToString();
            if (dtEmployee.Rows[0]["ConfirmationDate"] != DBNull.Value)
                txtConfirmationDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["ConfirmationDate"].ToString());
            if (dtEmployee.Rows[0]["RetirementDate"] != DBNull.Value)
                txtRetirementDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["RetirementDate"].ToString());
            if (dtEmployee.Rows[0]["EPFNoDate"] != DBNull.Value)
                txtEPFNoDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["EPFNoDate"].ToString());
          
            FileName = dtEmployee.Rows[0]["Image"].ToString();
            Session["ImageName"] = FileName;
            if (!Convert.ToBoolean(dtEmployee.Rows[0]["Active"]))
            {
                cbIsActive.Checked = false;
                string sss = dtEmployee.Rows[0]["InactiveStatus"].ToString();
                rblInactiveStatus.SelectedValue = dtEmployee.Rows[0]["InactiveStatus"].ToString();
                rblInactiveStatus.Visible = true;
                txtResignDate.Visible = true;
                if (dtEmployee.Rows[0]["InactiveStatus"].ToString() == "2")
                {
                    txtResignDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["InactiveDate"].ToString());
                }
                else if (dtEmployee.Rows[0]["InactiveStatus"].ToString() == "3")
                {
                    txtResignDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["ResignDate"].ToString());
                }
                else if (dtEmployee.Rows[0]["InactiveStatus"].ToString() == "4")
                {
                    txtResignDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["TerminatedDate"].ToString());
                }
            }
            else
            {
                cbIsActive.Checked = true;
                rblInactiveStatus.Visible = false;
                txtResignDate.Visible = false;
            }
        }

    }

    private void InitializeEmployee()
    {
        try
        {
            objEmployee = new Employee();
            dtBranch = objEmployee.GetEmployeeBrancId(Convert.ToInt32(hfOrganizationStructure.Value), Convert.ToInt32(Session["CompanyId"]));
            if (dtBranch.Rows.Count >= 0)
            {
                string BranchId = dtBranch.Rows[0]["OrganizationTypeID"].ToString();
                objEmployee.BranchID = Convert.ToInt32(BranchId);
            }
            objEmployee.EmployeeCode = txtEmployeeCode.Text.Trim();
            objEmployee.EPFNo = txtEPFNo.Text.Trim();
            objEmployee.FirstName = txtFirstName.Text.Trim();
            objEmployee.LastName = txtLastName.Text.Trim();
            objEmployee.FullName = txtFullName.Text.Trim();
            objEmployee.CallName = txtCallName.Text.Trim();
            objEmployee.NameInitials = txtNameInitials.Text.Trim();
            objEmployee.Gender = rblGender.SelectedValue.ToString();
            objEmployee.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.SelectedDate.Value);
            objEmployee.Status = Convert.ToInt32(rblStatus.SelectedValue);
            objEmployee.OccupationGrade = txtOccupationGrade.Text.Trim();
            objEmployee.PayrollAct = radCmbPayrollAct.SelectedValue;
            objEmployee.DirectIndirect = radCmbDirectIndirect.SelectedValue;
            objEmployee.CostCenter = Convert.ToInt32(radCmbCostCenter.SelectedValue);

            if (Session["ImageName"].ToString() == "DefaultEmployee.jpg")
            {
                objEmployee.Image = Session["ImageName"].ToString();
            }
            else
            {
                objEmployee.Image = Session["ImageName"].ToString();
            }
            objEmployee.JobCategoryID = Convert.ToInt32(ddlJobCatogory.SelectedValue);
            objEmployee.OrgStructureID = Convert.ToInt32(hfOrganizationStructure.Value);
            objEmployee.DesignationID = Convert.ToInt32(hfDesignationId.Value);
            objEmployee.JobGradeID = Convert.ToInt32(ddlJobGrade.SelectedValue);
            objEmployee.EmploymentTypeID = Convert.ToInt32(ddlEmployementType.SelectedValue);
            objEmployee.EmploymentGradeID = Convert.ToInt32(ddlEmploymentGrade.SelectedValue);
            objEmployee.RecruitementDate = Convert.ToDateTime(txtRecruitementDate.SelectedDate.Value);
            objEmployee.ConfirmationDate = Convert.ToDateTime(txtConfirmationDate.SelectedDate.Value);
            objEmployee.RetirementDate = Convert.ToDateTime(txtRetirementDate.SelectedDate.Value);
            objEmployee.EpfNoDate = Convert.ToDateTime(txtEPFNoDate.SelectedDate.Value);
            objEmployee.UpdateUser = Session["UserName"].ToString();

            DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(ddlEmployementType.SelectedValue));
            if (dtEmpTypes.Rows.Count > 0)
            {
                objEmployee.ProbationStartDate = null;
                objEmployee.ProbationEndDate = null;
                objEmployee.FixedTermContarctStartDate = null;
                objEmployee.FixedTermContarctEndDate = null;
                objEmployee.ConsultancyStartDate = null;
                objEmployee.ConsultancyEndDate = null;
                string empTypeCode = dtEmpTypes.Rows[0]["EmploymentTypeCode"].ToString();

                if (empTypeCode == "Probation")
                {
                    objEmployee.ProbationStartDate = Convert.ToDateTime(dxEmpTypeFromDate.SelectedDate.Value);
                    objEmployee.ProbationEndDate = Convert.ToDateTime(dxEmpTypeEndDate.SelectedDate.Value);
                }
                else if (empTypeCode == "FixedTermContarct")
                {
                    objEmployee.FixedTermContarctStartDate = Convert.ToDateTime(dxEmpTypeFromDate.SelectedDate.Value);
                    objEmployee.FixedTermContarctEndDate = Convert.ToDateTime(dxEmpTypeEndDate.SelectedDate.Value);
                }
                else if (empTypeCode == "Consultancy")
                {
                    objEmployee.ConsultancyStartDate = Convert.ToDateTime(dxEmpTypeFromDate.SelectedDate.Value);
                    objEmployee.ConsultancyEndDate = Convert.ToDateTime(dxEmpTypeEndDate.SelectedDate.Value);
                }

            }

            if (fuEmployeeImage.HasFile)
            {
                FileName = GetEmployeeImagepathChange1(txtFirstName.Text.Trim());
                objEmployee.Image = FileName;
            }
            else
            {
                FileName = GetEmployeeImagepathChange2(txtFirstName.Text.Trim());
                objEmployee.Image = FileName;
            }

            objEmployee.EMPCard = rbEmpCard.SelectedValue.ToString();
            objEmployee.InactiveDate = null;
            objEmployee.TerminateDate = null;
            if (!cbIsActive.Checked)
            {
                objEmployee.Active = false;
                objEmployee.InactiveStatus = Convert.ToInt32(rblInactiveStatus.SelectedValue);

                if (objEmployee.InactiveStatus == 2)
                {
                    objEmployee.InactiveDate = Convert.ToDateTime(txtResignDate.SelectedDate);
                }
                else if (objEmployee.InactiveStatus == 3)
                {
                    objEmployee.ResignDate = Convert.ToDateTime(txtResignDate.SelectedDate);
                }
                else if (objEmployee.InactiveStatus == 4)
                {
                    objEmployee.TerminateDate = Convert.ToDateTime(txtResignDate.SelectedDate);
                }

            }
            else
            {
                objEmployee.Active = true;
                objEmployee.InactiveStatus = 1;
                objEmployee.ReActivateDate = Convert.ToDateTime(txtResignDate.SelectedDate);
            }
        }
        catch(Exception ex)
        { }
    }

    public string GetEmployeeImagepathChange1(string UserName)
    {
        string fileName;
        Random RandNo = new Random();
        RandNo.Next();

        fileName = "EMP_" + RandNo.Next().ToString() + "_" + UserName + "_" + DateTime.Now.ToString("yyyy-dd-MM") + ".jpg";

        if (fuEmployeeImage.HasFile)
        {
            return fileName;
        }
        else
        {
            return fileName;
        }
    }

    public string GetEmployeeImagepathChange2(string UserName)
    {
        string fileName;
        Random RandNo = new Random();
        RandNo.Next();

        fileName = Session["ImageName"].ToString();

        return fileName;

    }

    public void SaveFile(string Filename)
    {
        if (Session["ImageName"].ToString() == "DefaultEmployee.jpg")
        {
            Image1.ImageUrl = Server.MapPath("~/App_Themes/Images/DefaultEmployee.jpg");

        }
        if (Session["ImageName"].ToString() == "DefaultEmployee.jpg" && fuEmployeeImage.HasFile)
        {
            ImageFilepath = Server.MapPath("~/HR/EmployeeImages") + "/" + Filename;
            fuEmployeeImage.SaveAs(ImageFilepath);
            Image1.ImageUrl = ImageFilepath;

        }
        if (Session["ImageName"].ToString() != "DefaultEmployee.jpg" && fuEmployeeImage.HasFile)
        {
            ImageFilepath = Server.MapPath("~/HR/EmployeeImages") + "/" + Filename;
            File.Delete(ImageFilepath);
            fuEmployeeImage.SaveAs(ImageFilepath);
            Image1.ImageUrl = ImageFilepath;

        }
    }

    public void Clear()
    {
        txtEmployeeCode.Text = string.Empty;
        txtEPFNo.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtFullName.Text = string.Empty;
        txtCallName.Text = string.Empty;
        txtNameInitials.Text = string.Empty;
        //txtConfirmationDate.Text="";
        //txtRetirementDate.Text = "";
       // txtEPFNoDate.SelectedDate = "";
        //txtDateOfBirth.Text = "";
        //lblDesignation.Text = string.Empty;
        lblOrganization.Text = string.Empty;
        rbEmpCard.Enabled = false;
        ddlEmployementType.SelectedIndex = -1;
        ddlEmploymentGrade.SelectedIndex = -1;
        ddlJobCatogory.SelectedIndex = -1;
        ddlJobGrade.SelectedIndex = -1;
        cbIsActive.Checked = true;
        txtResignDate.Visible = false;
        rblInactiveStatus.Visible = false;
        lblInactiveDate.Visible = false;
        isResign = false;
    }

    private bool ValidateUser()
    {
        if (lblOrganization.Text == string.Empty)
        {
            SetNotification("delete", "Error!", "delete", "Select Organization Unit!");
            RadNotification1.Show();
            lblResult.Text = "Select Organization Unit!";
            return false;
        }
        //if (lblDesignation.Text == string.Empty)
        //{
        //    SetNotification("delete", "Error!", "delete", "Select Designation!");
        //    RadNotification1.Show();
        //    lblResult.Text = "Select Designation!";
        //    return false;
        //}

        if (txtRecruitementDate.SelectedDate.Value == null)
        {
            SetNotification("delete", "Error!", "delete", "Select Recruitment Date!");
            RadNotification1.Show();
            lblResult.Text = "Select Recruitment Date!";
            return false;
        }

        if (txtDateOfBirth.SelectedDate.Value == null)
        {
            SetNotification("delete", "Error!", "delete", "Select Date of Birth!");
            RadNotification1.Show();
            lblResult.Text = "Select Date of Birth!";
            return false;
        }

        if (ddlJobCatogory.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Job Category!");
            RadNotification1.Show();
            lblResult.Text = "Select Job Category!";
            return false;
        }
        if (ddlJobGrade.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Job Grade!");
            RadNotification1.Show();
            lblResult.Text = "Select Job Grade!";
            return false;
        }
        if (ddlEmployementType.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Employment Type!");
            RadNotification1.Show();
            lblResult.Text = "Select Employment Type!";
            return false;
        }
        if (ddlEmploymentGrade.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Employment Grade!");
            RadNotification1.Show();
            lblResult.Text = "Select Employment Grade!";
            return false;
        }

        DataTable dtCompnay = objCompanyProfile.GetCompanyProfile(Convert.ToInt32(Session["CompanyId"]));
        if(dtCompnay.Rows.Count > 0)
        {
            string prefix = dtCompnay.Rows[0]["Prefix"].ToString();
            bool isCustomEmployeeCode = Convert.ToBoolean(dtCompnay.Rows[0]["CustomEmployeeCode"].ToString());
            if (isCustomEmployeeCode)
            {
                if(txtEmployeeCode.Text.Length > 3)
                {
                    string cuPrefix = txtEmployeeCode.Text.Substring(0, 3);
                    if (prefix != cuPrefix)
                    {
                        SetNotification("delete", "Error!", "delete", "Must have '" + prefix + "' prefix to EmployeeCode");
                        RadNotification1.Show();
                        return false;
                    }
                }
                else
                {
                    SetNotification("delete", "Error!", "delete", "Must have '" + prefix + "' prefix to EmployeeCode");
                    RadNotification1.Show();
                    return false;
                }
                
            }
        }

        objEmployee = new Employee();
        DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(ddlEmployementType.SelectedValue));
        if (dtEmpTypes.Rows.Count > 0)
        {
            string empTypeCode = dtEmpTypes.Rows[0]["EmploymentTypeCode"].ToString();
            if (empTypeCode == "Probation" || empTypeCode == "FixedTermContarct" || empTypeCode == "Consultancy")
            {
                if(dxEmpTypeFromDate.SelectedDate.Value == null || dxEmpTypeEndDate.SelectedDate.Value == null)
                {
                    SetNotification("delete", "Error!", "delete", "If Employment Type is 'Probation' or 'FixedTermContarct' or 'Consultancy' then 'From' and 'To' dates are require.");
                    RadNotification1.Show();
                    //lblResult.Text = "Select Employment Grade!";
                    return false;
                }
            }
        }

        if((rblInactiveStatus.SelectedValue == "2" || rblInactiveStatus.SelectedValue == "3" || rblInactiveStatus.SelectedValue == "4") && txtResignDate.SelectedDate == null && !cbIsActive.Checked)
        {
            SetNotification("delete", "Error!", "delete", "If Employee is 'Inactive' or 'Terminate' or 'Resing' then 'Change Date' is require.");
            RadNotification1.Show();
            //lblResult.Text = "Select Employment Grade!";
            return false;
        }

        return true;
    }

    private void SetNotification(string TitleIcon, string Title, string ContentIcon, string Content)
    {
        RadNotification1.TitleIcon = TitleIcon;
        RadNotification1.Title = Title;
        RadNotification1.ContentIcon = ContentIcon;
        RadNotification1.Text = Content;
    }

    private void UpdateEmployee(long EmoloyeeId)
    {
        if (ValidateUser())
        {
            InitializeEmployee();
            DataTable checkEpfCode = objEmployee.CheckEpfCode(txtEPFNo.Text, Convert.ToInt32(Session["CompanyId"]), EmoloyeeId);
            DataTable checkEmpCode = objEmployee.CheckEmployeeCodeWhenUpdate(txtEmployeeCode.Text, Convert.ToInt32(Session["CompanyId"]), EmoloyeeId);
            if (checkEpfCode.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete", "EPF Number is Already Exsist ");
                RadNotification1.Show();
            }
            else if (checkEmpCode.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete", "Employee Code is Already Exsist ");
                RadNotification1.Show();
            }
            else
            {
                objEmployee.UpdateEmployee(EmoloyeeId, true);
                if (!objEmployee.IsError)
                {
                    SaveFile(FileName);
                    lblResult.ForeColor = Color.Green;

                    Image1.ImageUrl = "~/HR/EmployeeImages/" + FileName;
                    lblResult.Text = "Successfully Updated.";
                }
                else
                {
                    lblResult.Text = objEmployee.ErrorMsg;
                }
            }
        }
    }

    private void UpdateApprovedPersonalInfo(long EmoloyeeId, string Status)
    {
        InitializeEmployee();
        objEmployee.UpdateEmployee(EmoloyeeId, true);
        if (!objEmployee.IsError)
        {
            SaveFile(FileName);
            lblResult.ForeColor = Color.Green;
            Image1.ImageUrl = "~/HR/EmployeeImages/" + FileName;
            lblResult.Text = "Successfully Updated the Personal Info Change Request.";
            objEmployee.UpdatePersonalChangeInfoStat(EmoloyeeId, Status);
        }
        else
        {
            lblResult.Text = objEmployee.ErrorMsg;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx");
        }
        InitializeEmployee();
        if (ValidateUser())
        {
            UpdateEmployee(Convert.ToInt64(ViewState["EmployeeId"]));
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            objEmployee = new Employee();

            objEmployee.DeleteEmployee(Convert.ToInt64(ViewState["EmployeeId"]));
            if (!objEmployee.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Deleted...";
                //EnableDisable(true);
                Clear();
            }
            else
            {
                lblResult.Text = "Unable to Delete!" + objEmployee.ErrorMsg;
            }
        }
        catch (Exception s)
        {
            string a = s.InnerException.ToString();
            lblResult.Text = "Unable to Delete";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GetEmployee(Convert.ToInt64(ViewState["EmployeeId"]));
        SetValues();
        lblResult.Text = "";
    }


    private void disableControls()
    {
        txtEmployeeCode.Enabled = false;
        txtEPFNo.Enabled = false;
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        txtFullName.Enabled = false;
        txtCallName.Enabled = false;
        txtDateOfBirth.Enabled = false;
        rblGender.Enabled = false;
        rblStatus.Enabled = false;
        ddlJobCatogory.Enabled = false;
        ddlJobGrade.Enabled = false;
        ddlEmployementType.Enabled = false;
        ddlEmploymentGrade.Enabled = false;
        txtRecruitementDate.Enabled = false;
        txtEPFNoDate.Enabled = false;
        fuEmployeeImage.Enabled = false;
        btnOrganizationSelect.Enabled = false;
       // btnDesignationStucture.Enabled = false;
        cbIsActive.Enabled = false;
        rblInactiveStatus.Enabled = false;
        txtResignDate.Enabled = false;

    }

    protected void txtEPFNo_TextChanged(object sender, EventArgs e)
    {
        if (txtEPFNo.Text == string.Empty)
        {
            txtEPFNoDate.Enabled = false;
            rbEmpCard.Enabled = false;
        }
        else
        {
            rbEmpCard.Enabled = true;
            txtEPFNoDate.Enabled = true;
            //txtConfirmationDate.Enabled = true;

        }
    }

    #region Checked Box
    protected void cbIsResign_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsActive.Checked == false)
        {
            txtResignDate.Visible = true;
            rblInactiveStatus.Visible = true;
            lblInactiveDate.Visible = true;
            isResign = true;
        }
        else
        {
            txtResignDate.Visible = false;
            rblInactiveStatus.Visible = false;
            lblInactiveDate.Visible = false;
            isResign = false;
        }
    }
    #endregion

    #region Drop Down
    protected void ddlJobCatogory_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Job Category >>", "0");
        if (!ddlJobCatogory.Items.Contains(newItem))
        {
            ddlJobCatogory.Items.Insert(0, newItem);
        }
    }

    protected void ddlJobGrade_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Job Grade >>", "0");
        if (!ddlJobGrade.Items.Contains(newItem))
        {
            ddlJobGrade.Items.Insert(0, newItem);
        }
    }

    protected void ddlEmployementType_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Employee Type >>", "0");
        if (!ddlEmployementType.Items.Contains(newItem))
        {
            ddlEmployementType.Items.Insert(0, newItem);
        }
    }

    protected void ddlEmploymentGrade_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Employee Grade >>", "0");
        if (!ddlEmploymentGrade.Items.Contains(newItem))
        {
            ddlEmploymentGrade.Items.Insert(0, newItem);
        }
    }

    protected void btnUpdatePersonalInfoEditRequest_Click(object sender, EventArgs e)
    {
        GetEmployeeOfPersonalInfoChange(Convert.ToInt64(ViewState["EmployeeId"]));
        SetValues();
        //disableControls();
        //btnUpdatePersonalInfoEditRequest.Visible = false;
        lbtnModify.Visible = false;
        btnPanel.Visible = true;
        btnPanelUpdate.Visible = false;
    }


    #endregion




    protected void ddlEmployementType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        objEmployee = new Employee();
        DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(ddlEmployementType.SelectedValue));
        if (dtEmpTypes.Rows.Count > 0)
        {
            string empTypeCode = dtEmpTypes.Rows[0]["EmploymentTypeCode"].ToString();
            if (empTypeCode == "Probation" || empTypeCode == "FixedTermContarct" || empTypeCode == "Consultancy")
            {
                //tddpFromDate.Visible = true;
                tdlblFromDate.Visible = true;
                //tddpToDate.Visible = true;
                tdlblToDate.Visible = true;
            }
            else
            {
                //tddpFromDate.Visible = false;
                tdlblFromDate.Visible = false;
                //tddpToDate.Visible = false;
                tdlblToDate.Visible = false;
            }

            if (empTypeCode == "Permanent")
            {
                lblConfirmationdate.Visible = true;
                txtConfirmationDate.Visible = true;
            }
            else
            {
                lblConfirmationdate.Visible = false;
                txtConfirmationDate.Visible = false;
            }
        }
        else
        {
           // tddpFromDate.Visible = false;
            tdlblFromDate.Visible = false;
            //tddpToDate.Visible = false;
            tdlblToDate.Visible = false;
        }
    }
    
    protected void rblInactiveStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblInactiveStatus.SelectedValue == "1")
        {
            cbIsActive.Checked = true;
            lblInactiveDate.Visible = true;
        }
        else
        {
            cbIsActive.Checked = false;
        }
    }

    protected void cmbDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbDesignation_TextChanged(object sender, EventArgs e)
    {

    }



    protected void btnClosePopup_Click(object sender, EventArgs e)
    {

    }

    protected void txtDateOfBirth_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        DateTime BirthDay = Convert.ToDateTime(txtDateOfBirth.SelectedDate);
        int BirthYear = BirthDay.Year;
        int CuruntYear = DateTime.Today.Year;
        int CuruntAge = CuruntYear - BirthYear;
        int RetireToYears = 0;

        if (rblGender.SelectedValue.ToString().Equals("Male"))
        {
            RetireToYears = 55;
            txtRetirementDate.SelectedDate = BirthDay.AddYears(RetireToYears);
        }
        else
        {
            RetireToYears = 50;
            txtRetirementDate.SelectedDate = BirthDay.AddYears(RetireToYears);
        }
    }

}
#endregion