using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Telerik.Web.UI;
using System.Drawing;
using HRM.HR.BLL;
using DevExpress.Web.Rendering;


public partial class Employee_EmployeeAditionalInfo : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion
    bool DetatilsSave;
    public int namesCounter;
    public int Id;
    private long EmployeeId;
    private bool isResign = false;
    private string ImageFilepath = string.Empty;
    private string Filename = string.Empty;
    string EmpCode;
    string DefaultEmpCode;
    DataTable dtQulification = new DataTable();
    DataTable dtCertification = new DataTable();
    DataTable dtMembership = new DataTable();
    DataTable dtFluency = new DataTable();
    DataTable dtSpouse = new DataTable();
    DataTable dtChild = new DataTable();
    DataTable dtSkill = new DataTable();
    DataTable dtSport = new DataTable();
    DataTable dtEmployee;
     DataTable dtBranch;
    Employee objEmployee;
    EmployeeAdditional objEmloyeeAditional;
    Qulification objQulification = new Qulification();
    Certification objCertification = new Certification();
    Membership objMembership = new Membership();
    Spouse objSpouce = new Spouse();
    Fluency objFluency = new Fluency();
    Child objChild = new Child();
    Skills objSkill = new Skills();
    Sport objSport = new Sport();
    EmployeeAdditional objEmloyeeAditionals = new EmployeeAdditional();

    protected void Page_Load(object sender, EventArgs e)
    {
        btndvUpdate.Enabled = false;
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
                    Response.Redirect("~/HR/NoPermissions.aspx");
                    return;
                }
            }
        }
        if (!IsPostBack)
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            dateTime();
        }

        

        if (!Page.IsPostBack)
        {
            BindMembership();
            BindSkill();
            BindSkillGrade();
            TablePostback();
            TableCertificationPostback();
            TableMembershipPostback();
            TableFluencyPostback();
            TableSpousePostback();
            TableChildPostback();
            TableSkillPostback();
            TableSportPostback();
            try
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    EmployeeId = Convert.ToInt64(Request.QueryString["EmployeeID"]);

                    if (EmployeeId > 0 && EmployeeId != null)
                    {
                        EmployeeAditionalInfoWizard.ActiveStepIndex = 0;
                    }
                    else
                    {
                        EmployeeAditionalInfoWizard.ActiveStepIndex = 0;
                    }

                    ViewState["Status"] = 0;
                }
                else
                {
                    EmployeeAditionalInfoWizard.ActiveStepIndex = 0;
                }
            }
            catch
            { }
        }
        else
        {
           
        }
        GetNewEmployeeCode();
        CreateQulificationTable();
        CreateCertificationTable();
        CreateMembershipTable();
        CreateFluencyTable();
        CreateSpouseTable();
        CreateChildTable();
        CreateSkillTable();
        CreateSportTable();
    }

    protected void EmployeeAditionalInfoWizard_ActiveStepChanged(object sender, EventArgs e)
    {
       
    }

    private void dateTime()
    {
        dxEmpTypeFromDate.SelectedDate= DateTime.Today;
        dxEmpTypeEndDate.SelectedDate = DateTime.Today;
        txtDateOfBirth.SelectedDate = DateTime.Today;
        txtRecruitementDate.SelectedDate = DateTime.Today;
        txtRetirementDate.SelectedDate = DateTime.Today;
    }

    protected void EmployeeAditionalInfoWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {

        try
        {
            AddEmployee();

        }
            

        catch
        { }
    }


    private void SetNotification(string TitleIcon, string Title, string ContentIcon, string Content)
    {
        RadNotification1.TitleIcon = TitleIcon;
        RadNotification1.Title = Title;
        RadNotification1.ContentIcon = ContentIcon;
        RadNotification1.Text = Content;
    }

    #region Add Employee Details
    private void GetNewEmployeeCode()
    {
        objEmployee = new Employee();
        dtEmployee = objEmployee.GetEmployeeCode(Convert.ToInt32(Session["CompanyId"].ToString()));
        if (dtEmployee.Rows.Count >= 0)
        {
            string Code = dtEmployee.Rows[0]["EMPCode"].ToString();
            txtEmployeeCode.Text = Code;
            //txtEPFNo.Text = Code;
        }
    }

    private void AddEmployee()
    {

        InitializeAddEmployee();
        if (ValidateUser())
        {
            DataTable CheckNic = objEmployee.CheckNicNo(txtNIC.Text.ToString());
            DataTable checkEmpCode = objEmployee.CheckEmployeeCode(txtEmployeeCode.Text, Convert.ToInt32(Session["CompanyId"]));
            DataTable checkEpfCode = objEmployee.CheckEpfCode(txtEPFNo.Text, Convert.ToInt32(Session["CompanyId"]));

            if (CheckNic.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete", "NIC No is Already Exsist ");
                RadNotification1.Show();
            }
            //else if (checkEmpCode.Rows.Count > 0)
            //{
            //    SetNotification("delete", "Error!", "delete ", "Employee code is Already Exsist ");
            //    RadNotification1.Show();
            //}
            //else if (checkEpfCode.Rows.Count > 0)
            //{
            //    SetNotification("delete", "Error!", "delete ", "EPF Number is Already Exsist ");
            //    RadNotification1.Show();
            //}
            else
            {
                objEmployee.AddEmployee();
                EmployeeId = Convert.ToInt64(objEmployee.GetEmployeeID);
                Session["EmpCode"] = objEmployee.EmployeeCode;
                //set probation leave for probation employee
                objEmployee.SetProbationEmployeeLeaveEntitlment(EmployeeId);
                // set employee leave leave
                objEmployee.SetLeave_LeaveEntitlment(EmployeeId, Session["UserName"].ToString());


                if (RcmbPayPeriodCatogory.SelectedValue != null && Convert.ToDecimal(RN_txtBasicsalary.Text)>0)
                {
                    AddEmployeePay(EmployeeId);
                }
                if (radcboShiftCode.SelectedValue != null && radcboRosterGroup.SelectedValue != null)
                {
                    AddTimeEmployee(EmployeeId);
                }
                AddAdditionalDetails();
                AddQulificationDetails((DataTable)(ViewState["QulificationTable"]));
                if (DetatilsSave == true)
                {
                    AddCertificationDetails((DataTable)(ViewState["CertificationTable"]));
                }
                if (DetatilsSave == true)
                {
                    AddMembershipDetails((DataTable)(ViewState["Membershiptable"]));
                }
                if (DetatilsSave == true)
                {
                    AddFluencyDetails((DataTable)(ViewState["Fluencytable"]));
                }
                if (DetatilsSave == true)
                {
                    AddSpouseDetails((DataTable)(ViewState["Spousetable"]));
                }
                if (DetatilsSave == true)
                {
                    AddChildDetails((DataTable)(ViewState["Childtable"]));
                }
                if (DetatilsSave == true)
                {
                    AddSkillDetails((DataTable)(ViewState["Skilltable"]));
                }
                if (DetatilsSave == true)
                {
                    AddSportDetails((DataTable)(ViewState["Sporttable"]));
                }
                if (!objEmployee.IsError)
                {
                    DetatilsSave = true;
                    SaveFile(Filename);
                   // Image1.ImageUrl = "~/HR/EmployeeImages/" + Filename;
                    SetNotification("ok", "Success!", "ok", "Successfully Saved! Employee Code Is " + Session["EmpCode"].ToString());
                    RadNotification1.Show();
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objEmployee.ErrorMsg;
                }
            }
            


        }
        else
        {
            RadNotification1.Show();
        }

       }

    

    private void InitializeAddEmployee()
    {
        try
        {
            objEmployee = new Employee();
            dtEmployee = objEmployee.GetEmployeeCode(Convert.ToInt32(Session["CompanyId"].ToString()));
            if (dtEmployee.Rows.Count >= 0)
            {
                string Code = dtEmployee.Rows[0]["EMPCode"].ToString();
                if (Code == " ".ToString())
                {
                    Session["EmpCode"] = "0002";
                }
                else
                {
                    Session["EmpCode"] = Code;
                }
            }

            //DataTable getJobCode = objEmployee.GetJobCategoryCode(Convert.ToInt32(ddlJobCatogory.SelectedValue));
            //if (getJobCode.Rows.Count >= 0)
            //{
            //    if (Convert.ToInt32(ddlJobCatogory.SelectedValue)==1043)
            //    {
            //        DataTable TrEmpCode = objEmployee.GetTraineeEmpCode(Convert.ToInt32(ddlJobCatogory.SelectedValue), Convert.ToInt32(Session["CompanyId"]));
            //        if (TrEmpCode.Rows.Count >= 0)
            //        {
            //            Session["EmpCode"] = TrEmpCode.Rows[0]["EmployeeCode"].ToString();
            //        }
            //    }
            //}

            dtBranch = objEmployee.GetEmployeeBrancId(Convert.ToInt32(hfOrganizationStructure.Value), Convert.ToInt32(Session["CompanyId"]));
            if (dtBranch.Rows.Count >= 0)
            {
                string BranchId = dtBranch.Rows[0]["OrganizationTypeID"].ToString();
                objEmployee.BranchID = Convert.ToInt32(BranchId);
            }

            objEmployee.CompanyID = Convert.ToInt32(Session["CompanyId"]);
            objEmployee.EmployeeCode = Session["EmpCode"].ToString();
            objEmployee.EPFNo = txtEPFNo.Text.Trim();
            objEmployee.EMPCard = rbEmpCard.SelectedValue.ToString();
            objEmployee.FirstName = txtFirstName.Text.Trim();
            objEmployee.LastName = txtLastName.Text.Trim();
            objEmployee.FullName = txtFullName.Text.Trim();
            objEmployee.CallName = txtCallName.Text.Trim();
            objEmployee.NameInitials = txtNameInitials.Text.Trim();
            objEmployee.HomeContactNo = txtHomeNo.Text.Trim();
            objEmployee.HomeContactNo2 = txtHomeNo2.Text.Trim();
            objEmployee.OfficeContactNo = txtOfficeNo.Text.Trim();
            objEmployee.MobileNo = txtMobileNo.Text.Trim();
            objEmployee.MobileNo2 = txtMobileNo2.Text.Trim();
            objEmployee.MobileNo3 = txtMobileNo3.Text.Trim();
            objEmployee.Address = txtAddress.Text.Trim();
            objEmployee.Address2 = txtAddress2.Text.Trim();
            objEmployee.City = txtcity.Text.Trim();
            objEmployee.EmergencyContactAddressLine1 = txtEmergencyAddress.Text.Trim();
            objEmployee.EmergencyContactAddressLine2 = txtEmergencyAddress2.Text.Trim();
            objEmployee.EmergencyContactCity = txtEmergancycity.Text.Trim();
            objEmployee.TemporaryAddressLine1 = txttempAddressline1.Text.Trim();
            objEmployee.TemporaryAddressLine2 = txttempAddressline2.Text.Trim();
            objEmployee.TemporaryCity = txttempAddresscity.Text.Trim();
            objEmployee.Email = txtEmail.Text.Trim();
            objEmployee.NIC = txtNIC.Text.Trim();
            objEmployee.Gender = rblGender.SelectedValue.ToString();
            objEmployee.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.SelectedDate.Value);
            //objEmployee.RecruitementDate = Convert.ToDateTime(txtRecruitementDate.Date);
            objEmployee.ConfirmationDate = Convert.ToDateTime(txtConfirmationDate.SelectedDate.Value);
            objEmployee.RetirementDate = Convert.ToDateTime(txtRetirementDate.SelectedDate.Value);
            objEmployee.EpfNoDate = Convert.ToDateTime(txtEPFNoDate.SelectedDate.Value);
            objEmployee.Status = Convert.ToInt32(rblStatus.SelectedValue);
            objEmployee.Image = fuEmployeeImage.FileName;
            objEmployee.JobCategoryID = Convert.ToInt32(ddlJobCatogory.SelectedValue);
            objEmployee.OrgStructureID = Convert.ToInt32(hfOrganizationStructure.Value);
            objEmployee.DesignationID = Convert.ToInt32(cmbDesignation.Value);
            objEmployee.JobGradeID = Convert.ToInt32(ddlJobGrade.SelectedValue);
            objEmployee.EmploymentTypeID = Convert.ToInt32(ddlEmployementType.SelectedValue);
            objEmployee.EmergencyContactPerson = txtEmergencycontactPerson.Text.Trim();
            objEmployee.EmergencyContactNo = txtEmergencyContactNo.Text.Trim();
            objEmployee.EmergencyContactNoHome = txtEmergencyContactNoHome.Text.Trim();
            objEmployee.EmergencyContactPersonRelationship = txtRelationship.Text.Trim();
            //objEmployee.EmergencyContactAddress = txtEmergencyAddress.Text.Trim();
            objEmployee.OccupationGrade = txtOccupationGrade.Text.Trim();
            objEmployee.EmploymentGradeID = Convert.ToInt32(ddlEmploymentGrade.SelectedValue);
            objEmployee.PostalCode = txtPostalCode.Text.Trim();
            objEmployee.InactiveStatus = 1;
            objEmployee.Remaks = txtRemark.Text.Trim();
            objEmployee.PassPortNo = txtPassport.Text.Trim();
            objEmployee.PayrollAct = radCmbPayrollAct.SelectedValue;
            objEmployee.DirectIndirect = radCmbDirectIndirect.SelectedValue;
            objEmployee.CostCenter = Convert.ToInt32(radCmbCostCenter.SelectedValue);
            //objEmployee.OcupationCategory = radTxtOcuGrade.Text;
            if (txtPassport.Text == string.Empty)
            {
                objEmployee.PasportExpiryDate = null;
            }
            else
            {
                objEmployee.PasportExpiryDate = Convert.ToDateTime(raddpPasportExpiyDate.SelectedDate.Value);
            }

            objEmployee.VisaNo = txtVisa.Text.Trim();

            if (objEmployee.IsExpatriate == true)
            {
                objEmployee.VisaExpiryDate = Convert.ToDateTime(raddpVisaExpiyDate.SelectedDate);
            }
            else
            {
                objEmployee.VisaExpiryDate = null;
            }

            //objEmployee.RecruitementDate = Convert.ToDateTime(txtRecruitementDate.Date);
            objEmployee.CreatedUser = Session["UserName"].ToString();
            objEmployee.UpdateUser = Session["UserName"].ToString();
            Filename = GetEmployeeImagepath(txtFirstName.Text.Trim());
            objEmployee.Image = Filename;

            DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(ddlEmployementType.SelectedValue));
            if (dtEmpTypes.Rows.Count > 0)
            {
                string empTypeCode = dtEmpTypes.Rows[0]["EmploymentTypeCode"].ToString();
                if (empTypeCode == "Probation")
                {
                    objEmployee.ProbationStartDate = Convert.ToDateTime(dxEmpTypeFromDate.SelectedDate);
                    objEmployee.ProbationEndDate = Convert.ToDateTime(dxEmpTypeEndDate.SelectedDate);
                } else if(empTypeCode == "FixedTermContarct")
                {
                    objEmployee.FixedTermContarctStartDate = Convert.ToDateTime(dxEmpTypeFromDate.SelectedDate);
                    objEmployee.FixedTermContarctEndDate = Convert.ToDateTime(dxEmpTypeEndDate.SelectedDate);
                }
                else if(empTypeCode == "Consultancy")
                {
                    objEmployee.ConsultancyStartDate = Convert.ToDateTime(dxEmpTypeFromDate.SelectedDate);
                    objEmployee.ConsultancyEndDate = Convert.ToDateTime(dxEmpTypeEndDate.SelectedDate);
                }
            }
            
        }
        catch(Exception ex)
        { }
    }

    public void SaveFile(string Filename)
    {
        if (fuEmployeeImage.HasFile)
        {
            ImageFilepath = Server.MapPath("~/HR/EmployeeImages") + "/" + Filename;
            fuEmployeeImage.SaveAs(ImageFilepath);
            //Image1.ImageUrl = ImageFilepath;
        }
        else
        {
            //Image1.ImageUrl = Server.MapPath("../../App_Themes/Common/DefaultEmployee.jpg");
        }
    }

    public string GetEmployeeImagepath(string UserName)
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
            return "DefaultEmployee.jpg";
    }

    private bool ValidateUser()
    {
        if (lblOrganization.Text == string.Empty)
        {
            SetNotification("delete", "Error!", "delete", "Select Department!");
            RadNotification1.Show();
            lblAddEmployee.Text = "Select Department!";
            return false;
        }
        //if (lblDesignation.Text == string.Empty)
        //{
        //    SetNotification("delete", "Error!", "delete", "Select Designation!");
        //    RadNotification1.Show();
        //    lblAddEmployee.Text = "Select Designation!";
        //    return false;
        //}

        //if (txtRecruitementDate.Date == null)
        //{
        //    SetNotification("delete", "Error!", "delete", "Select Recruitment Date!");
        //    RadNotification1.Show();
        //    lblAddEmployee.Text = "Select Recruitment Date!";
        //    return false;
        //}

        if (txtDateOfBirth.SelectedDate.Value == null)
        {
            SetNotification("delete", "Error!", "delete", "Select Date of Birth!");
            RadNotification1.Show();
            lblAddEmployee.Text = "Select Date of Birth!";
            return false;
        }

        if (txtPassport.Text != "" && raddpPasportExpiyDate.SelectedDate.Value == null)
        {
            SetNotification("delete", "Error!", "delete", "Select Passport Expiry Date!");
            RadNotification1.Show();
            lblAddEmployee.Text = "Select Passport Expiry Date!";
            return false;
        }

        if (ddlJobCatogory.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Job Category!");
            RadNotification1.Show();
            lblAddEmployee.Text = "Select Job Category!";
            return false;
        }
        //if (ddlJobGrade.SelectedIndex == 0)
        //{

        //    lblAddEmployee.Text = "Select Job Grade!";
        //    return false;
        //}
        if (ddlEmployementType.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Employment Type!");
            RadNotification1.Show();
            lblAddEmployee.Text = "Select Employment Type!";
            return false;
        }

        //objEmployee = new Employee();
        DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(ddlEmployementType.SelectedValue));
        if (dtEmpTypes.Rows.Count > 0)
        {
            string empTypeCode = dtEmpTypes.Rows[0]["EmploymentTypeCode"].ToString();
            if (empTypeCode == "Probation" || empTypeCode == "FixedTermContarct" || empTypeCode == "Consultancy")
            {
                //if (dxEmpTypeFromDate.Value == null || dxEmpTypeEndDate.Value == null)
                //{
                //    SetNotification("delete", "Error!", "delete", "If Employment Type is 'Probation' or 'FixedTermContarct' or 'Consultancy' then 'From' and 'To' dates are require.");
                //    RadNotification1.Show();
                //    //lblResult.Text = "Select Employment Grade!";
                //    return false;
                //}
            }
        }

        return true;
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

        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetDynamicContent(string contextKey)
    {
        return default(string);
    }

    protected void fuEmployeeImage_DataBinding(object sender, EventArgs e)
    {
        //Image1.ImageUrl = Server.MapPath(fuEmployeeImage.FileName);
    }


    protected void ddlJobCatogory_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("Select Job Category", "0");
        if (!ddlJobCatogory.Items.Contains(newItem))
        {
            ddlJobCatogory.Items.Insert(0, newItem);
        }
    }


    #region Drop Down
    protected void ddlJobGrade_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select Job Grade >>", "0");
        //if (!ddlEmployementType.Items.Contains(newItem))
        //{
        //    ddlEmployementType.Items.Insert(0, newItem);
        //}
        ddlJobGrade.SelectedIndex = 0;
    }

    protected void ddlEmployementType_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("Select Employee Type", "0");
        if (!ddlEmployementType.Items.Contains(newItem))
        {
            ddlEmployementType.Items.Insert(0, newItem);
        }
    }

    protected void ddlEmploymentGrade_DataBound(object sender, EventArgs e)
    {
        ddlEmploymentGrade.SelectedIndex = 0;
    }
    #endregion
    #endregion
    #region Add Employee Additional Details
    private void AddAdditionalDetails()
    {
        InitializationAdditionalDetails();
        objEmloyeeAditional.AddAdditionalInformation(EmployeeId);
        namesCounter = 0;
        if (!objEmloyeeAditional.IsError)
        {
            DetatilsSave = true;
        }
        else
        {
            DetatilsSave = false;
            //lblResult.Text = objEmloyeeAditional.ErrorMsg;
        }
    }
    private void InitializationAdditionalDetails()
    {
        try
        {
            objEmloyeeAditional = new EmployeeAdditional();
            objEmloyeeAditional.BloodGroup = ddlBloodGroup.SelectedValue;
            objEmloyeeAditional.RaceID = Convert.ToInt32(ddlRace.SelectedValue);
            objEmloyeeAditional.Province = txtProvince.Text;
            objEmloyeeAditional.District = txtDistrict.Text;
            objEmloyeeAditional.CollerSize = Convert.ToDouble(0);
            objEmloyeeAditional.NationalityID = Convert.ToInt32(ddlNationality.SelectedValue);
            objEmloyeeAditional.ElectoralDistrict = txtElectoralDistrict.Text;
            objEmloyeeAditional.DivisionalSecretariats = txtDivisionalSecretariats.Text;
            objEmloyeeAditional.GSDivision = txtGSDivision.Text;
            objEmloyeeAditional.TransportRoute = txtTransportRoute.Text;
            objEmloyeeAditional.DistanceforPermanentAddress = txtDistanceforPermanentAddress.Text;
            objEmloyeeAditional.CreatedUser = Session["UserName"].ToString();
        }
        catch
        {
        }
    }
    #endregion
    #region Add Employee Qulification

    private void TablePostback()
    {
        int i = 0;
        ViewState["QulificationNumber"] = 0;
        ViewState["QulificationTable"] = dtQulification;
    }

    private void CreateQulificationTable()
    {
        dtQulification.Columns.Add("EmployeeQulificationID", typeof(int));
        dtQulification.Columns.Add("InstituteID", typeof(int));
        dtQulification.Columns.Add("CourseID", typeof(int));
        dtQulification.Columns.Add("InstituteCode", typeof(string));
        dtQulification.Columns.Add("CourseName", typeof(string));
        dtQulification.Columns.Add("Year", typeof(string));
    }

    protected void btnQuliSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid1.Visible = true;
           // Session
            if (ViewState["QulificationTable"] != "")
                dtQulification = (DataTable)ViewState["QulificationTable"];
            namesCounter = Convert.ToInt32(ViewState["QulificationNumber"]) + 1;
            ViewState["QulificationNumber"] = namesCounter;
            Session["QuliId"] = namesCounter;
            DataRow dtrow = dtQulification.NewRow();
            dtrow[0] = namesCounter;
            dtrow["EmployeeQulificationID"] = namesCounter;
            dtrow["CourseID"] = ddlCourse.SelectedValue;
            dtrow["InstituteID"] = ddlInstitute.SelectedValue;
            dtrow["InstituteCode"] = ddlInstitute.Text;
            dtrow["CourseName"] = ddlCourse.Text;
            dtrow["Year"] = txtYear.Text;

            dtQulification.Rows.Add(dtrow);
            ViewState["QulificationTable"] = dtQulification;
            RadGrid1.DataSource = dtQulification;
            RadGrid1.DataBind();
            QulificationInitializeControls();
        }
        catch
        { }
    }

    private void AddQulificationDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objQulification.AddEmployeeQulifications(EmployeeId, tbl);

                if (!objQulification.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objQulification.ErrorMsg;
                }
            }
        }
        catch { }
    }

    protected void ddlCourse_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("Please Select Course", "0");
        if (!ddlCourse.Items.Contains(newItem))
        {
            ddlCourse.Items.Insert(0, newItem);
        }
    }

    protected void ddlInstitute_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Institute >>", "0");
        if (!ddlCourse.Items.Contains(newItem))
        {
            ddlCourse.Items.Insert(0, newItem);
        }
    }

    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            e.Item.Selected = true;
       


        }
    }

    private void QulificationInitializeControls()
    {
        txtYear.Text = "YYYY";
        ddlInstitute.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        lblMembership.ForeColor = Color.Red;
        lblMembership.Text = string.Empty;
    }

    #endregion
    #region Add Employee Certification
    private void TableCertificationPostback()
    {
        int i = 0;
        ViewState["CertificationNumber"] = 0;
        ViewState["CertificationTable"] = dtCertification;
    }
    private void CreateCertificationTable()
    {
        dtCertification.Columns.Add("EmployeeCertificationID", typeof(int));
        dtCertification.Columns.Add("InstituteID", typeof(int));
        dtCertification.Columns.Add("CertificationID", typeof(int));
        dtCertification.Columns.Add("InstituteCode", typeof(string));
        dtCertification.Columns.Add("CertificationName", typeof(string));
        dtCertification.Columns.Add("Date", typeof(DateTime));
        dtCertification.Columns.Add("Grade", typeof(string));
    }
    protected void btnCertiSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid2.Visible = true;
            //namesCounter =  Convert.ToInt32(hfCertificationID);
            if (ViewState["CertificationTable"] != "")
           dtCertification = (DataTable)ViewState["CertificationTable"];
            namesCounter = Convert.ToInt32(ViewState["CertificationNumber"]) + 1;
            ViewState["CertificationNumber"] = namesCounter;
            Session["CertId"] = namesCounter;
            DataRow dtrow = dtCertification.NewRow();
            dtrow[0] = namesCounter;
            dtrow["EmployeeCertificationID"] = namesCounter;
            dtrow["InstituteID"] = ddlCerInstitute.SelectedValue;
            dtrow["CertificationID"] = ddlCertification.SelectedValue;
            dtrow["InstituteCode"] = ddlCerInstitute.Text;
            dtrow["CertificationName"] = ddlCertification.Text;
            dtrow["Date"] = txtDate.SelectedDate.Value;
            dtrow["Grade"] = txtGrade.Text;

            dtCertification.Rows.Add(dtrow);
            ViewState["table"] = dtCertification;
            RadGrid2.DataSource = dtCertification;
            RadGrid2.DataBind();
            CertificationInitializeControls();
        }
        catch
        { }
    }
    protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
    {
 
    }

    private void CertificationInitializeControls()
    {
        ddlInstitute.SelectedIndex = -1;
        ddlCertification.SelectedIndex = -1;
        txtGrade.Text = string.Empty;
        txtDate.SelectedDate = null;
        lblCerticication.ForeColor = Color.Red;
        lblCerticication.Text = string.Empty;

    }

    private void AddCertificationDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objCertification.AddEmployeeCertification(EmployeeId, tbl);
                if (!objCertification.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objQulification.ErrorMsg;
                }
            }
        }
        catch { }
    }
    #endregion
    #region Add Employee Membership
    private void TableMembershipPostback()
    {
        int i = 0;
        ViewState["MembershipNumber"] = 0;
        ViewState["Membershiptable"] = dtMembership;
    }
    private void CreateMembershipTable()
    {
        dtMembership.Columns.Add("EmployeeMembershipID", typeof(int));
        dtMembership.Columns.Add("MembershipID", typeof(int));
        dtMembership.Columns.Add("Membership", typeof(string));
        dtMembership.Columns.Add("FromDate", typeof(DateTime));
        dtMembership.Columns.Add("ToDate", typeof(DateTime));
        dtMembership.Columns.Add("IsToDate", typeof(bool));
        dtMembership.Columns.Add("Grade", typeof(string));
        dtMembership.Columns.Add("Remark", typeof(string));
    }
    protected void btnMembeSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid3.Visible = true;
            if (ViewState["Membershiptable"] != "")
                dtMembership = (DataTable)ViewState["Membershiptable"];
            namesCounter = Convert.ToInt32(ViewState["MembershipNumber"]) + 1;
            ViewState["MembershipNumber"] = namesCounter;
            DataRow dtrow = dtMembership.NewRow();
            Session["MemId"] = namesCounter;
            dtrow[0] = namesCounter;
            dtrow["EmployeeMembershipID"] = namesCounter;
            dtrow["MembershipID"] = ddlMembership.SelectedValue;
            dtrow["Membership"] = ddlMembership.Text;
            dtrow["FromDate"] = txtFromDate.SelectedDate.Value;
            dtrow["ToDate"] = txtToDate.SelectedDate.Value;
            dtrow["IsToDate"] = cbIsToDate.Checked;
            dtrow["Grade"] = txtmemGrade.Text;
            dtrow["Remark"] = txtmemremark.Text;

            dtMembership.Rows.Add(dtrow);
            ViewState["Membershiptable"] = dtMembership;
            RadGrid3.DataSource = dtMembership;
            RadGrid3.DataBind();
            MembershipInitializeControls();
        }
        catch
        { }
    }
    protected void RadGrid3_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            e.Item.Selected = true;

              

        }
    }
    private void AddMembershipDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objMembership.AddEmployeeMemberships(Convert.ToInt32(EmployeeId), tbl);
                if (!objMembership.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = true;
                    //lblResult.Text = objMembership.ErrorMsg;
                }
            }
        }
        catch { }
    }

    private void MembershipInitializeControls()
    {
        ddlMembership.SelectedIndex = -1;
        txtmemGrade.Text = string.Empty;
        txtmemremark.Text = string.Empty;
        lblMembership.ForeColor = Color.Red;
        lblMembership.Text = string.Empty;
        txtFromDate.SelectedDate = null;
        txtToDate.SelectedDate = null;
        cbIsToDate.Checked = false;
    }


    protected void ddlMembership_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("Please Select Membership Type", "0");
        if (!ddlMembership.Items.Contains(newItem))
        {
            ddlMembership.Items.Insert(0, newItem);
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsToDate.Checked)
        {
            txtToDate.Enabled = true;
        }
        else
        {
            txtToDate.Enabled = true;
            txtToDate.SelectedDate = null;
        }
    }
    private void BindMembership()
    {
        ddlMembership.DataSource = objMembership.GetMembership(0);
        ddlMembership.DataValueField = "MembershipID";
        ddlMembership.DataTextField = "MembershipName";
        ddlMembership.DataBind();
    }
    #endregion
    #region Add Emloyee Fluency
    private void TableFluencyPostback()
    {
        int i = 0;
        ViewState["FluencyNumber"] = 0;
        ViewState["Fluencytable"] = dtFluency;
    }

    private void CreateFluencyTable()
    {
        dtFluency.Columns.Add("LanguId", typeof(int));
        dtFluency.Columns.Add("LanguageID", typeof(int));
        dtFluency.Columns.Add("LanguageName", typeof(string));
        dtFluency.Columns.Add("Reading", typeof(string));
        dtFluency.Columns.Add("Writing", typeof(string));
        dtFluency.Columns.Add("Speaking", typeof(string));
        dtFluency.Columns.Add("Listening", typeof(string));
    }

    protected void btnFluenSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid4.Visible = true;
            if (ViewState["Fluencytable"] != "")
                dtFluency = (DataTable)ViewState["Fluencytable"];
            namesCounter = Convert.ToInt32(ViewState["FluencyNumber"]) + 1;
            ViewState["FluencyNumber"] = namesCounter;
            Session["LangId"] = namesCounter;
            DataRow dtrow = dtFluency.NewRow();
            dtrow[0] = namesCounter;
            dtrow["LanguId"] = namesCounter;
            dtrow["LanguageID"] = ddlLanguge.SelectedValue;
            dtrow["LanguageName"] = ddlLanguge.Text;
            dtrow["Reading"] = ddlReading.Text;
            dtrow["Writing"] = ddlWriting.Text;
            dtrow["Speaking"] = ddlSpeaking.Text;
            dtrow["Listening"] = ddlListening.Text;

            dtFluency.Rows.Add(dtrow);
            ViewState["Fluencytable"] = dtFluency;
            RadGrid4.DataSource = dtFluency;
            RadGrid4.DataBind();
            FluencyInitializeControls();
        }

        catch { }
    }

    protected void RadGrid4_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            e.Item.Selected = true;
   
              


        }
    }

    private void FluencyInitializeControls()
    {
        ddlLanguge.SelectedIndex = -1;
        ddlReading.SelectedItem.Text = "Good";
        ddlSpeaking.SelectedItem.Text = "Good";
        ddlWriting.SelectedItem.Text = "Good";
        ddlListening.SelectedItem.Text = "Good";
        lblFluency.ForeColor = Color.Green;
        lblFluency.Text = string.Empty;

    }


    private void AddFluencyDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objFluency.AddEmployeeFluency(EmployeeId, tbl);
                if (!objFluency.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = false;
                    // lblResult.Text = objMembership.ErrorMsg;
                }
            }
        }
        catch { }
    }
    protected void ddlLanguge_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Languge >>", "0");
        if (!ddlLanguge.Items.Contains(newItem))
        {
            ddlLanguge.Items.Insert(0, newItem);
        }
    }

    protected void ddlSpeaking_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlSpeaking.Items.Contains(newItem))
        {
            ddlSpeaking.Items.Insert(0, newItem);
        }
    }
    protected void ddlListening_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlListening.Items.Contains(newItem))
        {
            ddlListening.Items.Insert(0, newItem);
        }
    }
    protected void ddlWriting_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlWriting.Items.Contains(newItem))
        {
            ddlWriting.Items.Insert(0, newItem);
        }
    }
    protected void ddlReading_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlReading.Items.Contains(newItem))
        {
            ddlReading.Items.Insert(0, newItem);
        }
    }

    #endregion
    #region Add Employee Spouse
    private void TableSpousePostback()
    {
        int i = 0;
        ViewState["SpouseNumber"] = 0;
        ViewState["Spousetable"] = dtSpouse;
    }

    private void CreateSpouseTable()
    {
        dtSpouse.Columns.Add("SpouseID", typeof(int));
        dtSpouse.Columns.Add("FirstName", typeof(string));
        dtSpouse.Columns.Add("FullName", typeof(string));
        dtSpouse.Columns.Add("LastName", typeof(string));
        dtSpouse.Columns.Add("Company", typeof(string));
        dtSpouse.Columns.Add("Designation", typeof(string));
        dtSpouse.Columns.Add("ContactNo", typeof(string));
        dtSpouse.Columns.Add("Email", typeof(string));
        dtSpouse.Columns.Add("Gender", typeof(string));
        dtSpouse.Columns.Add("DateOfBirth", typeof(DateTime));
    }

    protected void btnSpouseSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid5.Visible = true;

            if (ViewState["Spousetable"] != "")
                dtSpouse = (DataTable)ViewState["Spousetable"];
            namesCounter = Convert.ToInt32(ViewState["SpouseNumber"]) + 1;
            ViewState["SpouseNumber"] = namesCounter;
            Session["SpceId"] = namesCounter;
            DataRow dtrow = dtSpouse.NewRow();
            dtrow[0] = namesCounter;
            dtrow["SpouseID"] = namesCounter;
            dtrow["FirstName"] = txtSpousFirstName.Text;
            dtrow["FullName"] = txtSpousFullName.Text;
            dtrow["LastName"] = txtSpousLastName.Text;
            dtrow["Company"] = txtCompany.Text;
            dtrow["Designation"] = txtDesignation.Text;
            dtrow["ContactNo"] = txtContactNo.Text;
            dtrow["Email"] = txtSpouceEmail.Text;
            dtrow["Gender"] = rbGender.SelectedValue;
            dtrow["DateOfBirth"] = txtSpouseDateOfBirth.SelectedDate.Value;

            dtSpouse.Rows.Add(dtrow);
            ViewState["Spousetable"] = dtSpouse;
            RadGrid5.DataSource = dtSpouse;
            RadGrid5.DataBind();
            SpousInitializeControls();
        }

        catch { }
    }

    protected void RadGrid5_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            e.Item.Selected = true;
        

        }
    }

    private void SpousInitializeControls()
    {

        txtSpousFirstName.Text = string.Empty;
        txtSpousLastName.Text = string.Empty;
        txtSpousFullName.Text = string.Empty;
        txtSpouseDateOfBirth.SelectedDate = null;
        txtDesignation.Text = string.Empty;
        txtCompany.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtSpouceEmail.Text = string.Empty;
        lblSpouse.ForeColor = Color.Red;
        lblSpouse.Text = string.Empty;


    }

    private void AddSpouseDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objSpouce.AddSpouse(EmployeeId, tbl);
                if (!objSpouce.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objSpouce.ErrorMsg;
                }
            }
        }
        catch { }
    }
    #endregion
    #region Add Employee Child
    private void TableChildPostback()
    {
        int i = 0;
        ViewState["ChildNumber"] = 0;
        ViewState["Childtable"] = dtChild;
    }

    private void CreateChildTable()
    {
        dtChild.Columns.Add("ChildID", typeof(int));
        dtChild.Columns.Add("FirstName", typeof(string));
        dtChild.Columns.Add("FullName", typeof(string));
        dtChild.Columns.Add("LastName", typeof(string));
        dtChild.Columns.Add("Gender", typeof(string));
        dtChild.Columns.Add("DateOfBirth", typeof(DateTime));
    }

    protected void btnChildSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid6.Visible = true;
            if (ViewState["Childtable"] != "")
            dtChild = (DataTable)ViewState["Childtable"];
            namesCounter = Convert.ToInt32(ViewState["ChildNumber"]) + 1;
            ViewState["ChildNumber"] = namesCounter;
            Session["ChildId"] = namesCounter;
            DataRow dtrow = dtChild.NewRow();
            dtrow[0] = namesCounter;
            dtrow["ChildID"] = namesCounter;
            dtrow["FirstName"] = txtChildFName.Text;
            dtrow["FullName"] = txtChildFullName.Text;
            dtrow["LastName"] = txtChildLName.Text;
            dtrow["Gender"] = RBChildGender.SelectedValue;
            dtrow["DateOfBirth"] = txtChildDate.SelectedDate.Value;

            dtChild.Rows.Add(dtrow);
            ViewState["Childtable"] = dtChild;
            RadGrid6.DataSource = dtChild;
            RadGrid6.DataBind();
            ChildInitializeControls();
        }

        catch { }
    }

    protected void RadGrid6_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            e.Item.Selected = true;
            try
            {
                


            }
            catch
            {

            }


        }
    }

    private void ChildInitializeControls()
    {

        txtChildFName.Text = string.Empty;
        txtChildLName.Text = string.Empty;
        txtChildFullName.Text = string.Empty;
        txtChildDate.SelectedDate = null;
        lblChild.ForeColor = Color.Red;
        lblChild.Text = string.Empty;

    }

    private void AddChildDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objChild.AddChild(EmployeeId, tbl);
                if (!objChild.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objChild.ErrorMsg;
                }
            }
        }
        catch { }
    }
    #endregion
    #region Add Employee Skill
    private void TableSkillPostback()
    {
        int i = 0;
        ViewState["SkillNumber"] = 0;
        ViewState["Skilltable"] = dtSkill;
    }

    private void CreateSkillTable()
    {
        dtSkill.Columns.Add("EmployeeSkillID", typeof(int));
        dtSkill.Columns.Add("SkillID", typeof(int));
        dtSkill.Columns.Add("SkillGradeID", typeof(int));
        dtSkill.Columns.Add("SkillName", typeof(string));
        dtSkill.Columns.Add("SkillGradeName", typeof(string));
    }

    protected void btnSkillSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid7.Visible = true;
            if (ViewState["Skilltable"] != "")
            dtSkill = (DataTable)ViewState["Skilltable"];
            namesCounter = Convert.ToInt32(ViewState["SkillNumber"]) + 1;
            ViewState["SkillNumber"] = namesCounter;
            Session["SkId"] = namesCounter;
            DataRow dtrow = dtSkill.NewRow();
            dtrow[0] = namesCounter;
            dtrow["EmployeeSkillID"] = namesCounter;
            dtrow["SkillID"] = ddlSkill.SelectedValue;
            dtrow["SkillName"] = ddlSkill.Text;
            dtrow["SkillGradeID"] = ddlSkillGrade.SelectedValue;
            dtrow["SkillGradeName"] = ddlSkillGrade.Text;

            dtSkill.Rows.Add(dtrow);
            ViewState["Skilltable"] = dtSkill;
            RadGrid7.DataSource = dtSkill;
            RadGrid7.DataBind();
            SkillInitializeControls();
        }

        catch { }
    }

    protected void RadGrid7_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            e.Item.Selected = true;


        }
    }

    private void AddSkillDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objSkill.AddEmployeeSkill(EmployeeId, tbl);
                if (!objSkill.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objSkill.ErrorMsg;
                }
            }
        }
        catch { }
    }


    private void SkillInitializeControls()
    {
        ddlSkill.SelectedIndex = -1;
        ddlSkillGrade.SelectedIndex = -1;
        lblSkill.ForeColor = Color.Red;
        lblSkill.Text = string.Empty;

    }


    private void BindSkill()
    {
        ddlSkill.DataSource = objSkill.GetSkill(0);
        ddlSkill.DataValueField = "SkillID";
        ddlSkill.DataTextField = "SkillName";
        ddlSkill.DataBind();
    }
    private void BindSkillGrade()
    {
        ddlSkillGrade.DataSource = objSkill.GetSkillGrade(0);
        ddlSkillGrade.DataValueField = "SkillGradeID";
        ddlSkillGrade.DataTextField = "SkillGradeName";
        ddlSkillGrade.DataBind();
    }

    protected void ddlSkillGrade_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem1 = new RadComboBoxItem("Please Select Skill Grade>>", "0");
        if (!ddlSkillGrade.Items.Contains(newItem1))
        {
            ddlSkillGrade.Items.Insert(0, newItem1);
        }
    }
    protected void ddlSkill_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("Please Select Skill Type", "0");
        if (!ddlSkill.Items.Contains(newItem))
        {
            ddlSkill.Items.Insert(0, newItem);
        }
    }

    #endregion
    #region Add Employee Sport
    private void TableSportPostback()
    {
        int i = 0;
        ViewState["SportNumber"] = 0;
        ViewState["Sporttable"] = dtSport;
    }

    private void CreateSportTable()
    {
        dtSport.Columns.Add("EmployeeSportActivityID", typeof(int));
        dtSport.Columns.Add("SportID", typeof(int));
        dtSport.Columns.Add("SportName", typeof(string));
    }

    protected void btnSportSave_Click(object sender, EventArgs e)
    {
        try
        {
            RadGrid8.Visible = true;
            if (ViewState["Sporttable"] != "")
                dtSport = (DataTable)ViewState["Sporttable"];
            namesCounter = Convert.ToInt32(ViewState["SportNumber"]) + 1;
            ViewState["SportNumber"] = namesCounter;
            Session["SpId"] = namesCounter;
            DataRow dtrow = dtSport.NewRow();
            dtrow[0] = namesCounter;
            dtrow["EmployeeSportActivityID"] = namesCounter;
            dtrow["SportID"] = ddlSport.SelectedValue;
            dtrow["SportName"] = ddlSport.Text;

            dtSport.Rows.Add(dtrow);
            ViewState["Sporttable"] = dtSport;
            RadGrid8.DataSource = dtSport;
            RadGrid8.DataBind();
            SportInitializeControls();
        }

        catch { }
    }

    protected void RadGrid8_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            e.Item.Selected = true;
   

        }
    }

    private void SportInitializeControls()
    {
        ddlSport.SelectedIndex = -1;
        lblSport.ForeColor = Color.Red;
        lblSport.Text = string.Empty;

    }


    private void AddSportDetails(DataTable tbl)
    {
        try
        {
            if (tbl != null)
            {
                objSport.AddEmployeeSport(EmployeeId, tbl);
                if (!objSkill.IsError)
                {
                    DetatilsSave = true;
                }
                else
                {
                    DetatilsSave = false;
                    // lblResult.Text = objSport.ErrorMsg;
                }
            }
        }
        catch { }
    }


    protected void RadComboBox1_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("Please Select Sport", "0");
        if (!ddlSport.Items.Contains(newItem))
        {
            ddlSport.Items.Insert(0, newItem);
        }
    }

    #endregion
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        InitializeAddEmployee();
        if (ValidateUser())
        {
            DataTable CheckNic = objEmployee.CheckNicNo(txtNIC.Text.ToString());
            DataTable checkEmpCode = objEmployee.CheckEmployeeCode(txtEmployeeCode.Text, Convert.ToInt32(Session["CompanyId"]));
            DataTable checkEpfCode = objEmployee.CheckEpfCode(txtEPFNo.Text, Convert.ToInt32(Session["CompanyId"]));

            if (CheckNic.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete", "NIC No is Already Exsist ");
                RadNotification1.Show();
            }
            //else if (checkEmpCode.Rows.Count > 0)
            //{
            //    SetNotification("delete", "Error!", "delete ", "Employee code is Already Exsist ");
            //    RadNotification1.Show();
            //}
            else if (checkEpfCode.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete ", "EPF Number is Already Exsist ");
                RadNotification1.Show();
            }
            else
            {
                objEmployee.AddEmployee();
                EmployeeId = Convert.ToInt64(objEmployee.GetEmployeeID);
                Session["EmpCode"] = objEmployee.EmployeeCode;
                //objEmployee.SetProbationEmployeeLeaveEntitlment(EmployeeId);
                if (!objEmployee.IsError)
                {
                    DetatilsSave = true;
                    SaveFile(Filename);
                    //Image1.ImageUrl = "~/HR/EmployeeImages/" + Filename;
                    SetNotification("ok", "Success!", "ok", "Successfully Saved! Employee Code Is " + Session["EmpCode"].ToString());
                    RadNotification1.Show();
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objEmployee.ErrorMsg;
                }

            }
        }
        else
        {
            RadNotification1.Show();
        }
    }

    private void ClearBankAcounts()
    {
        RcmbBank.SelectedIndex = -1;
        RcmbBranches.SelectedIndex = -1;
        RdTxtAccountNo.Text = string.Empty;
    }

    protected void Unnamed_Click1(object sender, ImageClickEventArgs e)
    {
        GsDevisionPopup.ShowOnPageLoad = false;
        formContainer.Attributes.CssStyle.Add("height", "585px");

    }

    protected void Unnamed_Click2(object sender, ImageClickEventArgs e)
    {
        ElectPopup.ShowOnPageLoad = false;
        formContainer.Attributes.CssStyle.Add("height", "585px");

    }

    protected void Unnamed_Click3(object sender, ImageClickEventArgs e)
    {
        Divisianalpopup.ShowOnPageLoad = false;
        formContainer.Attributes.CssStyle.Add("height", "585px");

    }

    protected void Unnamed_Click4(object sender, ImageClickEventArgs e)
    {
        transportpopup.ShowOnPageLoad = false;
        formContainer.Attributes.CssStyle.Add("height", "585px");

    }

    protected void Unnamed_Click5(object sender, ImageClickEventArgs e)
    {
        vaccinepopup.ShowOnPageLoad = false;
        formContainer.Attributes.CssStyle.Add("height", "585px");

    }
    


    protected void txtPassport_TextChanged(object sender, EventArgs e)
    {
        raddpPasportExpiyDate.Enabled = true;
    }
    protected void RN_txtDailyWadge_TextChanged(object sender, EventArgs e)
    {

    }
    protected void RcmbCurencyCode_DataBound(object sender, EventArgs e)
    {

    }
    protected void RcmbPayPeriodCatogory_DataBound(object sender, EventArgs e)
    {

    }
    protected void cbStopPay_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void cbBankTranferRequired_CheckedChanged(object sender, EventArgs e)
    {
        if (cbBankTranferRequired.Checked)
        {
            RcmbBank.Enabled = true;
            RcmbBranches.Enabled = true;
            RdTxtAccountNo.Enabled = true;
            RdTxtNameAsPerBank.Enabled = true;
        }
        else
        {
            RcmbBank.Enabled = false;
            RcmbBranches.Enabled = false;
            RdTxtAccountNo.Enabled = false;
            RdTxtNameAsPerBank.Enabled = false;
            ClearBankAcounts();
        }
    }
    protected void RadButton2_Click(object sender, EventArgs e)
    {
        InitializeAddEmployee();
        if (ValidateUser() && ValidateFields())
        {
            DataTable CheckNic = objEmployee.CheckNicNo(txtNIC.Text.ToString());
            DataTable checkEmpCode = objEmployee.CheckEmployeeCode(txtEmployeeCode.Text, Convert.ToInt32(Session["CompanyId"]));
            DataTable checkEpfCode = objEmployee.CheckEpfCode(txtEPFNo.Text, Convert.ToInt32(Session["CompanyId"]));

            if (CheckNic.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete", "NIC No is Already Exsist ");
                RadNotification1.Show();
            }
            //else if (checkEmpCode.Rows.Count > 0)
            //{
            //    SetNotification("delete", "Error!", "delete ", "Employee code is Already Exsist ");
            //    RadNotification1.Show();
            //}
            else if (checkEpfCode.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete ", "EPF Number is Already Exsist ");
                RadNotification1.Show();
            }
            else
            {
                objEmployee.AddEmployee();
                EmployeeId = Convert.ToInt64(objEmployee.GetEmployeeID);
                
                if (!objEmployee.IsError)
                {
                    DetatilsSave = true;
                    SaveFile(Filename);
                    //Image1.ImageUrl = "~/HR/EmployeeImages/" + Filename;
                    SetNotification("ok", "Success!", "ok", "Successfully Saved! Employee Code Is " + Session["EmpCode"].ToString());
                    RadNotification1.Show();

                    //objEmployee.SetProbationEmployeeLeaveEntitlment(EmployeeId);
                    AddEmployeePay(EmployeeId);
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objEmployee.ErrorMsg;
                }
            }



        }
        else
        {
            RadNotification1.Show();
        }
    }

    private void AddEmployeePay(long EmployeeId)
    {
        string EncryptionKey = "007London";
        if (ValidateFields())
        {
            try
            {
                bool IsStopPay = false;
                bool IsPosted = false;
                bool IsBankTransfer = false;
                int BankId = 0;
                int BranchId = 0;
                if (cbStopPay.Checked)
                    IsStopPay = true;
                
                if (cbBankTranferRequired.Checked)
                {
                    IsBankTransfer = true;
                    BankId = Convert.ToInt32(RcmbBank.Value);
                    BranchId = Convert.ToInt32(RcmbBranches.Value);
                    ViewState["IsBankTransferd"] = true;
                }
                if (checkepf.Checked)
                {
                    int EPFRate1 = 8;
                    int EPFRate2 = 12;
                    int EPFRate3 = 3;
                    int EPFRate4 = 20;

                    if (EmployeeId > 0 && ValidateFields())
                    {
                        
                    }
                }
                else
                {
                    if (EmployeeId > 0 && ValidateFields())
                    {
                       
                    }
                }
           
            }
            catch
            { }
        }
    }

    private bool ValidateFields()
    {
        //if ((RN_txtBasicsalary.Value == 0) && (RN_txtDailyWadge.Value == 0))
        //{
        //    SetNotification("delete", "Error!", "delete", "Basic Salary or Daily Wage should be set! <br/>Both can not be '0'");
        //    RadNotification1.Show();
        //    return false;
        //}

        if (RcmbCurencyCode.SelectedIndex < 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Currency Code!");
            RadNotification1.Show();
            return false;
        }

        if (RcmbPayPeriodCatogory.SelectedIndex < 0)
        {
            SetNotification("delete", "Error!", "delete", "Select Pay Category!");
            RadNotification1.Show();
            return false;
        }

        if (cbBankTranferRequired.Checked)
        {
            if (RcmbBank.SelectedIndex < 0)
            {
                SetNotification("delete", "Error!", "delete", "Select Bank!");
                RadNotification1.Show();
                return false;
            }
            if (RcmbBranches.SelectedIndex < 0)
            {
                SetNotification("delete", "Error!", "delete", "Select Bank Branch!");
                RadNotification1.Show();
                return false;
            }
            if (RdTxtAccountNo.Text == string.Empty)
            {
                SetNotification("delete", "Error!", "delete", "Enter Account No!");
                RadNotification1.Show();
                return false;
            }
            if (RdTxtNameAsPerBank.Text == string.Empty)
            {
                SetNotification("delete", "Error!", "delete", "Enter Name As Per Bank!");
                RadNotification1.Show();
                return false;
            }
        }

        return true;
    }

    private bool TimeValidations()
    {
        if (radcboShiftCode.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Please Select a Shift Name");
            RadNotification1.Show();
            return false;
        }
        else if (radcboRosterGroup.SelectedIndex == 0)
        {
            SetNotification("delete", "Error!", "delete", "Please Select a Shift Group");
            RadNotification1.Show();
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void RcmbShiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
    {
        RadComboBoxItem newListItem = new RadComboBoxItem("<< Select Shift Name >>", "0");
        if (radcboShiftCode.Items.FindItemByValue("0") == null)
        {
            radcboShiftCode.Items.Insert(0, newListItem);
        }
    }

    protected void RcmbRosterGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
    {
        RadComboBoxItem newListItem = new RadComboBoxItem("<< Select Shift Group >>", "0");
        if (radcboRosterGroup.Items.FindItemByValue("0") == null)
        {
            radcboRosterGroup.Items.Insert(0, newListItem);
        }
    }


    protected void RadButton3_Click(object sender, EventArgs e)
    {
        InitializeAddEmployee();
        if (ValidateUser() && ValidateFields())
        {
            DataTable CheckNic = objEmployee.CheckNicNo(txtNIC.Text.ToString());
            DataTable checkEmpCode = objEmployee.CheckEmployeeCode(txtEmployeeCode.Text, Convert.ToInt32(Session["CompanyId"]));
            DataTable checkEpfCode = objEmployee.CheckEpfCode(txtEPFNo.Text, Convert.ToInt32(Session["CompanyId"]));

            if (CheckNic.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete", "NIC No is Already Exsist ");
                RadNotification1.Show();
            }
            //else if (checkEmpCode.Rows.Count > 0)
            //{
            //    SetNotification("delete", "Error!", "delete ", "Employee code is Already Exsist ");
            //    RadNotification1.Show();
            //}
            else if (checkEpfCode.Rows.Count > 0)
            {
                SetNotification("delete", "Error!", "delete ", "EPF Number is Already Exsist ");
                RadNotification1.Show();
            }
            else
            {
                objEmployee.AddEmployee();
                EmployeeId = Convert.ToInt64(objEmployee.GetEmployeeID);

                if (!objEmployee.IsError)
                {
                    DetatilsSave = true;
                    SaveFile(Filename);
                   // Image1.ImageUrl = "~/HR/EmployeeImages/" + Filename;
                    SetNotification("ok", "Success!", "ok", "Successfully Saved! Employee Code Is " + Session["EmpCode"].ToString());
                    RadNotification1.Show();

                    objEmployee.SetProbationEmployeeLeaveEntitlment(EmployeeId);
                    if (Convert.ToDecimal(RN_txtBasicsalary.Text) > 0)
                    {
                        AddEmployeePay(EmployeeId);
                    }
                    AddTimeEmployee(EmployeeId);
                }
                else
                {
                    DetatilsSave = false;
                    //lblResult.Text = objEmployee.ErrorMsg;
                }
            }



        }
        else
        {
            RadNotification1.Show();
        }
    }

    public void AddTimeEmployee(long EmployeeId) {

       
    }

    protected void ddlEmployementType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(ddlEmployementType.SelectedValue));
        if(dtEmpTypes.Rows.Count > 0)
        {
            string empTypeCode = dtEmpTypes.Rows[0]["EmploymentTypeCode"].ToString();
            if(empTypeCode == "Probation" || empTypeCode == "FixedTermContarct" || empTypeCode == "Consultancy")
            {
               // dxEmpTypeFromDate.Date = txtRecruitementDate.SelectedDate.Value();
                if (txtRecruitementDate.SelectedDate != null)
                    dxEmpTypeEndDate.SelectedDate = txtRecruitementDate.SelectedDate.Value.AddMonths(6);

                tddpFromDate.Visible = true;
                dxEmpTypeFromDate.Visible = true;
                tddpToDate.Visible = true;
                dxEmpTypeEndDate.Visible = true;
            }
            else
            {
                tddpFromDate.Visible = false;
                dxEmpTypeFromDate.Visible = false;
                tddpToDate.Visible = false;
                dxEmpTypeEndDate.Visible = false;
            }
        }
        else
        {
            tddpFromDate.Visible = false;
            dxEmpTypeFromDate.Visible = false;
            tddpToDate.Visible = false;
            dxEmpTypeEndDate.Visible = false;
        }
        
    }

    protected void txtDateOfBirth_DateChanged(object sender, EventArgs e)
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

    protected void lkSelect_Click(object sender, EventArgs e)
    {
       

           //GridDataItem dataItem = (GridDataItem)RadGrid2.sele[0];
           var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        int CertificationID = Convert.ToInt32(Session["CertId"]);
        for (int i = ((DataTable)ViewState["CertificationTable"]).Rows.Count - 1; i >= 0; i--)
        {
            string Id = CertificationID.ToString();
            DataRow dr = ((DataTable)ViewState["CertificationTable"]).Rows[i];


            if (Id != "")
            {
                if (dr["EmployeeCertificationID"].ToString() == CertificationID.ToString())
                    dr.Delete();

                ((DataTable)ViewState["CertificationTable"]).AcceptChanges();

                dtCertification = (DataTable)ViewState["CertificationTable"];


                ViewState["CertificationTable"] = dtCertification;
                RadGrid2.DataSource = dtCertification;
                RadGrid2.DataBind();
            }
        }
    }

    protected void lkSelect_Click1(object sender, EventArgs e)
    {
        try
        {


            int SelectedQulificationID = Convert.ToInt32(Session["QuliId"]);

            for (int i = ((DataTable)ViewState["QulificationTable"]).Rows.Count - 1; i >= 0; i--)
            {
                string Id = SelectedQulificationID.ToString();
                DataRow dr = ((DataTable)ViewState["QulificationTable"]).Rows[i];


                if (Id != "")
                {
                    if (dr["EmployeeQulificationID"].ToString() == SelectedQulificationID.ToString())
                        dr.Delete();

                    ((DataTable)ViewState["QulificationTable"]).AcceptChanges();

                    dtQulification = (DataTable)ViewState["QulificationTable"];


                    ViewState["QulificationTable"] = dtQulification;
                    RadGrid1.DataSource = dtQulification;
                    RadGrid1.DataBind();
                }
            }


        }
        catch
        {

        }
    }

    protected void lkSelect_Click2(object sender, EventArgs e)
    {
        try
        {

            int EmployeeMembershipID = Convert.ToInt32(ViewState["MembershipNumber"]);


            for (int i = ((DataTable)ViewState["Membershiptable"]).Rows.Count - 1; i >= 0; i--)
            {
                string Id = EmployeeMembershipID.ToString();
                DataRow dr = ((DataTable)ViewState["Membershiptable"]).Rows[i];


                if (Id != "")
                {
                    if (dr["EmployeeMembershipID"].ToString() == EmployeeMembershipID.ToString())
                        dr.Delete();

                    ((DataTable)ViewState["Membershiptable"]).AcceptChanges();

                    dtMembership = (DataTable)ViewState["Membershiptable"];


                    ViewState["Membershiptable"] = dtMembership;
                    RadGrid3.DataSource = dtMembership;
                    RadGrid3.DataBind();
                }
            }
        

        }
        catch
        {

        }
    }

    protected void lkSelect_Click3(object sender, EventArgs e)
    {
        try
        {

            int LanguId = Convert.ToInt32(Session["LangId"]);


            for (int i = ((DataTable)ViewState["Fluencytable"]).Rows.Count - 1; i >= 0; i--)
            {
                string Id = LanguId.ToString();
                DataRow dr = ((DataTable)ViewState["Fluencytable"]).Rows[i];


                if (Id != "")
                {
                    if (dr["LanguId"].ToString() == LanguId.ToString())
                        dr.Delete();

                    ((DataTable)ViewState["Fluencytable"]).AcceptChanges();

                    dtFluency = (DataTable)ViewState["Fluencytable"];


                    ViewState["Fluencytable"] = dtFluency;
                    RadGrid4.DataSource = dtFluency;
                    RadGrid4.DataBind();
                }
            }
           
        }
        catch
        {

        }
    }

    protected void lkSelect_Click4(object sender, EventArgs e)
    {
        try
        {
           
            int SpouseID = Convert.ToInt32(Session["SpceId"]);


            for (int i = ((DataTable)ViewState["Spousetable"]).Rows.Count - 1; i >= 0; i--)
            {
                string Id = SpouseID.ToString();
                DataRow dr = ((DataTable)ViewState["Spousetable"]).Rows[i];


                if (Id != "")
                {
                    if (dr["SpouseID"].ToString() == SpouseID.ToString())
                        dr.Delete();

                    ((DataTable)ViewState["Spousetable"]).AcceptChanges();

                    dtSpouse = (DataTable)ViewState["Spousetable"];


                    ViewState["Spousetable"] = dtSpouse;
                    RadGrid5.DataSource = dtSpouse;
                    RadGrid5.DataBind();
                }
            }
         

        }
        catch
        {

        }

    }

    protected void lkSelect_Click5(object sender, EventArgs e)
    {

        int ChildID = Convert.ToInt32(Session["ChildId"]);


        for (int i = ((DataTable)ViewState["Childtable"]).Rows.Count - 1; i >= 0; i--)
        {
            string Id = ChildID.ToString();
            DataRow dr = ((DataTable)ViewState["Childtable"]).Rows[i];


            if (Id != "")
            {
                if (dr["ChildID"].ToString() == ChildID.ToString())
                    dr.Delete();

                ((DataTable)ViewState["Childtable"]).AcceptChanges();

                dtChild = (DataTable)ViewState["Childtable"];


                ViewState["Childtable"] = dtChild;
                RadGrid6.DataSource = dtChild;
                RadGrid6.DataBind();
            }
        }
    }

    protected void lkSelect_Click6(object sender, EventArgs e)
    {
        try
        {

            int EmployeeSkillID = Convert.ToInt32(Session["SkId"]);

            for (int i = ((DataTable)ViewState["Skilltable"]).Rows.Count - 1; i >= 0; i--)
            {
                string Id = EmployeeSkillID.ToString();
                DataRow dr = ((DataTable)ViewState["Skilltable"]).Rows[i];


                if (Id != "")
                {
                    if (dr["EmployeeSkillID"].ToString() == EmployeeSkillID.ToString())
                        dr.Delete();

                    ((DataTable)ViewState["Skilltable"]).AcceptChanges();

                    dtSkill = (DataTable)ViewState["Skilltable"];


                    ViewState["Skilltable"] = dtSkill;
                    RadGrid7.DataSource = dtSkill;
                    RadGrid7.DataBind();
                }
            }


        }
        catch
        {

        }
    }

    protected void lkSelect_Click7(object sender, EventArgs e)
    {
        try
        {

            int EmployeeSportActivityID = Convert.ToInt32(Session["SpId"]);


            for (int i = ((DataTable)ViewState["Sporttable"]).Rows.Count - 1; i >= 0; i--)
            {
                string Id = EmployeeSportActivityID.ToString();
                DataRow dr = ((DataTable)ViewState["Sporttable"]).Rows[i];


                if (Id != "")
                {
                    if (dr["EmployeeSportActivityID"].ToString() == EmployeeSportActivityID.ToString())
                        dr.Delete();

                    ((DataTable)ViewState["Sporttable"]).AcceptChanges();

                    dtSport = (DataTable)ViewState["Sporttable"];


                    ViewState["Sporttable"] = dtSport;
                    RadGrid8.DataSource = dtSport;
                    RadGrid8.DataBind();
                }
            }


        }
        catch
        {

        }
    }
    protected void cmbDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
       // GetEmployeesInOrNotInTime("Check");
    }

    protected void btnOrganizationStucture0_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }

    protected void btngsAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpGsDevision(Convert.ToInt32(Session["CompanyId"].ToString()), txtgscode.Text, txtgsname.Text);
        GsDevisionPopup.ShowOnPageLoad = true;
        gvPopup.DataBind();
        txtGSDivision.DataBind();
        txtgscode.Text = "";
        txtgsname.Text = "";
        btngsupdate.Enabled = false;
        if (!objEmloyeeAditionals.IsError)
        {
          
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
            //lblResult.Visible = false;
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btngsupdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpGsDevision(Convert.ToInt32(hfgsId.Value), txtgscode.Text, txtgsname.Text);
        GsDevisionPopup.ShowOnPageLoad = true;
        txtGSDivision.DataBind();
        gvPopup.DataBind();
        txtgscode.Text = "";
        txtgsname.Text = "";
        btngsAdd.Enabled = false;

        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }

    }

    protected void btngscancel_Click(object sender, EventArgs e)
    {
        txtgscode.Text = "";
        txtgsname.Text = "";
        GsDevisionPopup.ShowOnPageLoad = false;
    }

    protected void btngsdelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpGsDevision(Convert.ToInt32(hfgsId.Value), txtgscode.Text, txtgsname.Text);
        GsDevisionPopup.ShowOnPageLoad = false;
        txtGSDivision.DataBind();
        txtgscode.Text = "";
        txtgsname.Text = "";
        gvPopup.DataBind();
      if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
        GsDevisionPopup.ShowOnPageLoad = true;

    }

    protected void btngsplus_Click(object sender, ImageClickEventArgs e)
    {
        GsDevisionPopup.ShowOnPageLoad = true;
       
    }

    protected void lkSelectPopup_Click(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfgsId.Value = gvPopup.GetRowValues(index, "GSId").ToString();
            txtgscode.Text = gvPopup.GetRowValues(index, "Code").ToString();
            txtgsname.Text = gvPopup.GetRowValues(index, "Name").ToString();
            btngsAdd.Enabled = false;
            btngsupdate.Enabled = true;
            btngsdelete.Enabled = true;
        }
        catch(Exception ex)
        {

        }

    }

    protected void btnelectplus_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ElectPopup.ShowOnPageLoad = true;
    }

    protected void btneladd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpElectoralDistrict(Convert.ToInt32(Session["CompanyId"].ToString()), txtelcode.Text, txteldistrict.Text);
        ElectPopup.ShowOnPageLoad = true;
        gvelectoral.DataBind();
        txtelcode.Text = "";
        txtgsname.Text = "";
     
        txtElectoralDistrict.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btnelupdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpElectoralDistrict(Convert.ToInt32(hfElectoral.Value), txtelcode.Text, txteldistrict.Text);
        ElectPopup.ShowOnPageLoad = true;
        gvelectoral.DataBind();
        txtelcode.Text = "";
        txteldistrict.Text = "";
        btngsAdd.Enabled = false;
        txtElectoralDistrict.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }

    }

    protected void btnelcancel_Click(object sender, EventArgs e)
    {
        txtelcode.Text = "";
        txteldistrict.Text = "";
        ElectPopup.ShowOnPageLoad = false;
    }

    protected void btneldelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpElectoralDistrict(Convert.ToInt32(hfElectoral.Value));
        ElectPopup.ShowOnPageLoad = true;
        txtelcode.Text = "";
        txteldistrict.Text = "";
        gvPopup.DataBind();
        txtElectoralDistrict.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
        
    }

    protected void lkSelectPopupel_Click(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfElectoral.Value = gvelectoral.GetRowValues(index, "ELId").ToString();
            txtelcode.Text = gvelectoral.GetRowValues(index, "Code").ToString();
            txteldistrict.Text = gvelectoral.GetRowValues(index, "Name").ToString();
            btneladd.Enabled = false;
            btnelupdate.Enabled = true;
            btneldelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btndivisionplus_Click(object sender, ImageClickEventArgs e)
    {
        Divisianalpopup.ShowOnPageLoad = true;

    }

    protected void lkSelectPopupel_Click1(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfDvId.Value = gvdivision.GetRowValues(index, "DvId").ToString();
            txtdvcode.Text = gvdivision.GetRowValues(index, "Code").ToString();
            txtdvname.Text = gvdivision.GetRowValues(index, "Name").ToString();
            btndvAdd.Enabled = false;
            btndvUpdate.Enabled = true;
            btndvDelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btndvAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpDivisional(Convert.ToInt32(Session["CompanyId"].ToString()), txtdvcode.Text, txtdvname.Text);
        Divisianalpopup.ShowOnPageLoad = true;
        gvdivision.DataBind();
        txtdvcode.Text = "";
        txtdvname.Text = "";
        btndvUpdate.Enabled = false;
        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btndvUpdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpDivisional(Convert.ToInt32(hfDvId.Value),txtdvcode.Text, txtdvname.Text);
        Divisianalpopup.ShowOnPageLoad = true;
        gvdivision.DataBind();
        txtdvcode.Text = "";
        txtdvname.Text = "";
        
        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btndvCancel_Click(object sender, EventArgs e)
    {
        txtdvcode.Text = "";
        txtdvname.Text = "";
        Divisianalpopup.ShowOnPageLoad = false;
    }

    protected void btndvDelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpDivisional(Convert.ToInt32(hfDvId.Value));
        Divisianalpopup.ShowOnPageLoad = true;
        gvdivision.DataBind();
        txtdvcode.Text = "";
        txtdvname.Text = "";

        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void transportplus_Click(object sender, ImageClickEventArgs e)
    {
        transportpopup.ShowOnPageLoad = true;
       
    }

    protected void btntrAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpTransportRout(Convert.ToInt32(Session["CompanyId"].ToString()),txtrtnumber.Text, txtrtname.Text);
        transportpopup.ShowOnPageLoad = true;
        transportgv.DataBind();
        txtrtnumber.Text = "";
        txtrtname.Text = "";
        btntrUpdate.Enabled = false;
        txtTransportRoute.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btntrUpdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpTransportRout(Convert.ToInt32(hfTrId.Value), txtrtnumber.Text, txtrtname.Text);
        transportpopup.ShowOnPageLoad = true;
        transportgv.DataBind();
        txtrtnumber.Text = "";
        txtrtname.Text = "";

        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btntrCancel_Click(object sender, EventArgs e)
    {
        txtrtnumber.Text = "";
        txtrtname.Text = "";
        transportpopup.ShowOnPageLoad = false;
    }

    protected void btntrDelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpTransport(Convert.ToInt32(hfTrId.Value));
        Divisianalpopup.ShowOnPageLoad = true;
        transportgv.DataBind();
        txtrtnumber.Text = "";
        txtrtname.Text = "";

        txtTransportRoute.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void lkSelectPopupel_Click2(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfTrId.Value = transportgv.GetRowValues(index, "TrId").ToString();
            txtrtnumber.Text = transportgv.GetRowValues(index, "Number").ToString();
            txtrtname.Text = transportgv.GetRowValues(index, "Name").ToString();
            btntrAdd.Enabled = false;
            btntrUpdate.Enabled = true;
            btntrDelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
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



    protected void btnvcplus_Click(object sender, ImageClickEventArgs e)
    {
        vaccinepopup.ShowOnPageLoad = true;
    }

    protected void btnvcAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpVacciene( Convert.ToInt32(Session["CompanyId"].ToString()), txtvccode.Text, txtvcname.Text);
        vaccinepopup.ShowOnPageLoad = true;
        
        txtvccode.Text = "";
        txtvcname.Text = "";
        btnvcUpdate.Enabled = false;
        cmbvaccine.DataBind();
        vaccienegv.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btnvcUpdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpVacciene( Convert.ToInt32(hfvcId.Value), txtvccode.Text, txtvcname.Text);
        vaccinepopup.ShowOnPageLoad = true;
        vaccienegv.DataBind();
        txtvccode.Text = "";
        txtvcname.Text = "";

        cmbvaccine.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btnvcCancel_Click(object sender, EventArgs e)
    {
        txtvccode.Text = "";
        txtvcname.Text = "";
        vaccinepopup.ShowOnPageLoad = false;
        RcmbBank.Enabled = false;
        RcmbBranches.Enabled = false;
        RdTxtAccountNo.Enabled = false;
        RdTxtNameAsPerBank.Enabled = false;
    }

    protected void btnvcDelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpVacciene(Convert.ToInt32(hfvcId.Value));
        vaccinepopup.ShowOnPageLoad = true;
        vaccienegv.DataBind();
        txtvccode.Text = "";
        txtvcname.Text = "";

        cmbvaccine.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void lkSelectPopupel_Click3(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfvcId.Value = vaccienegv.GetRowValues(index, "VcId").ToString();
            txtvccode.Text = vaccienegv.GetRowValues(index, "Code").ToString();
            txtvcname.Text = vaccienegv.GetRowValues(index, "Name").ToString();
            btnvcAdd.Enabled = false;
            btnvcUpdate.Enabled = true;
            btnvcDelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }
} 