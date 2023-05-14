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

public partial class HR_Employee_UserControls_EmployeeContactDetails : System.Web.UI.UserControl
{

    DataTable dtEmployee;
    Employee objEmployee;
    bool isResign = false;
    string FileName = string.Empty;
    string ImageFilepath = string.Empty;
    string isPersonalChangeRequest = string.Empty;
    User objEmail = new User();
    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";
    MksSecurity objSecurity = new MksSecurity();

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

               // DataTable dtPersonalChangeInfo = ((DataTable)objEmployee.GetEmployeeOfPersonalInfoChange(Convert.ToInt64(Request.QueryString["EmployeeId"]), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common"));
                //if (dtPersonalChangeInfo.Rows.Count > 0)
               // {
                   // btnUpdate.Visible = true;
               // }
               // else
               // {
                   // btnUpdate.Visible = false;
              //  }
                    //btnSave.Visible = false;
                   // btnUpdate.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                       // btnSave.Visible = true;
                        btnUpdate.Visible = true;
                    }
                    else
                    {
                       // btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
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
               
            }
        }
    }


    public void GetEmployee(long EmployeeId)
    {
        objEmployee = new Employee();
        dtEmployee = objEmployee.GetEmployee(EmployeeId, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
    }

    public void GetEmployeeOfPersonalInfoChange(long EmployeeId)
    {
        objEmployee = new Employee();
        dtEmployee = objEmployee.GetEmployeeOfPersonalInfoChange(EmployeeId, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");

    }

    public void SetValues()
    {
       
          if (dtEmployee.Rows.Count > 0)
        {
            txtRemark.Text = dtEmployee.Rows[0]["Remark"].ToString();
            //txtAddress.Text = dtEmployee.Rows[0]["AddressLine1"].ToString();
            txtAddress2.Text = dtEmployee.Rows[0]["AddressLine2"].ToString();
            //txtAddresscity.Text = dtEmployee.Rows[0]["City"].ToString();
            txtPostalCode.Text = dtEmployee.Rows[0]["PostalCode"].ToString();
            txtHomeNo.Text = dtEmployee.Rows[0]["HomeContactNo"].ToString();
            txtHomeNo2.Text = dtEmployee.Rows[0]["HomeContactNo2"].ToString();
            txtOfficeNo.Text = dtEmployee.Rows[0]["OfficeContactNo"].ToString();
            txtMobileNo.Text = dtEmployee.Rows[0]["MobileNo"].ToString();
            txtMobileNo2.Text = dtEmployee.Rows[0]["MobileNo2"].ToString();
            txtMobileNo3.Text = dtEmployee.Rows[0]["MobileNo3"].ToString();
            txtEmail.Text = dtEmployee.Rows[0]["Email"].ToString();
            txtNIC.Text = dtEmployee.Rows[0]["NIC"].ToString();
            txtEmergencycontactPerson.Text = dtEmployee.Rows[0]["EmergencyContactPerson"].ToString();
            txtEmergencyContactNo.Text = dtEmployee.Rows[0]["EmergencyContactNo"].ToString();
            txtEmergencyContactNoHome.Text = dtEmployee.Rows[0]["EmergencyContactHomeNo"].ToString();
            txtRelationship.Text = dtEmployee.Rows[0]["RelationshipOfContactPerson"].ToString();
            txtPassport.Text = dtEmployee.Rows[0]["PassportNo"].ToString();
            txtVisa.Text = dtEmployee.Rows[0]["VisaNo"].ToString();
            //txtEmergencyAddressline1.Text = dtEmployee.Rows[0]["EmergencyContactAddressLine1"].ToString();
            //txtEmergencyAddressline2.Text = dtEmployee.Rows[0]["EmergencyContactAddressLine2"].ToString();
            //txtEmergencyAddressCity.Text = dtEmployee.Rows[0]["EmergencyContactCity"].ToString();
            //txtemporary1.Text = dtEmployee.Rows[0]["TemporaryAddressLine1"].ToString();
            //txtemporary2.Text = dtEmployee.Rows[0]["TemporaryAddressLine2"].ToString();
            //txtemporarycity.Text = dtEmployee.Rows[0]["TemporaryCity"].ToString();
            if (dtEmployee.Rows[0]["PassPortExpiryDate"] != DBNull.Value)
                raddpPasportExpiyDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["PassPortExpiryDate"].ToString());
            if (dtEmployee.Rows[0]["VisaExpiryDate"] != DBNull.Value)
                raddpVisaExpiyDate.SelectedDate = Convert.ToDateTime(dtEmployee.Rows[0]["VisaExpiryDate"].ToString());
            
       
        }

    }

    protected void txtPassport_TextChanged(object sender, EventArgs e)
    {
        raddpPasportExpiyDate.Enabled = true;
    }

    private void InitializeEmployee()
    {
        try
        {
            objEmployee = new Employee();
            objEmployee.HomeContactNo = txtHomeNo.Text.Trim();
            objEmployee.HomeContactNo2 = txtHomeNo2.Text.Trim();
            objEmployee.OfficeContactNo = txtOfficeNo.Text.Trim();
            objEmployee.MobileNo = txtMobileNo.Text.Trim();
            objEmployee.MobileNo2 = txtMobileNo2.Text.Trim();
            objEmployee.MobileNo3 = txtMobileNo3.Text.Trim();
            objEmployee.Address = txtAddress.Text.Trim();
            objEmployee.Address2 = txtAddress2.Text.Trim();
            objEmployee.City = txtAddresscity.Text.Trim();
            objEmployee.Email = txtEmail.Text.Trim();
            objEmployee.NIC = txtNIC.Text.Trim();
            objEmployee.EmergencyContactPerson = txtEmergencycontactPerson.Text.Trim();
            objEmployee.EmergencyContactNo = txtEmergencyContactNo.Text.Trim();
            objEmployee.EmergencyContactNoHome = txtEmergencyContactNoHome.Text.Trim();
            objEmployee.PostalCode = txtPostalCode.Text.Trim();
            objEmployee.EmergencyContactPersonRelationship = txtRelationship.Text.Trim();
            objEmployee.Remaks = txtRemark.Text.Trim();
            objEmployee.UpdateUser = Session["UserName"].ToString();
            objEmployee.PassPortNo = txtPassport.Text.Trim();
            objEmployee.PasportExpiryDate = Convert.ToDateTime(raddpPasportExpiyDate.SelectedDate);
            objEmployee.VisaNo = txtVisa.Text.Trim();
            objEmployee.EmergencyContactAddressLine1 = txtEmergencyAddressline1.Text.Trim();
            objEmployee.EmergencyContactAddressLine2 = txtEmergencyAddressline2.Text.Trim();
            objEmployee.EmergencyContactCity = txtEmergencyAddressCity.Text.Trim();
            objEmployee.TemporaryAddressLine1 = txtemporary1.Text.Trim();
            objEmployee.TemporaryAddressLine2 = txtemporary2.Text.Trim();
            objEmployee.TemporaryCity = txtemporarycity.Text.Trim();
            if (txtVisa.Text != string.Empty)
                objEmployee.VisaExpiryDate = Convert.ToDateTime(raddpVisaExpiyDate.SelectedDate);
         
        }
        catch
        { }
    }


    public void Clear()
    {
        txtHomeNo.Text = string.Empty;
        txtOfficeNo.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtNIC.Text = string.Empty;
        txtPostalCode.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRelationship.Text = string.Empty;
        txtEmergencycontactPerson.Text = string.Empty;
        txtEmergencyContactNo.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtPassport.Text = string.Empty;
        txtVisa.Text = string.Empty;
        raddpPasportExpiyDate.SelectedDate = null;
        raddpVisaExpiyDate.SelectedDate = null;
        txtAddress.Text = string.Empty;
        txtAddress2.Text = string.Empty;
        txtAddresscity.Text = string.Empty;
        txtemporary1.Text = string.Empty;
        txtemporary2.Text = string.Empty;
        txtemporarycity.Text = string.Empty;
        txtEmergencyAddressline1.Text = string.Empty;
        txtEmergencyAddressline2.Text = string.Empty;
        txtEmergencyAddressCity.Text = string.Empty;

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
        InitializeEmployee();
        DataTable CheckNic = objEmployee.CheckNicNo(EmoloyeeId, txtNIC.Text.ToString());
        if (CheckNic.Rows.Count > 0)
        {
            SetNotification("ok", "Error!", "ok", "NIC No is Already Exsist ");
            RadNotification1.Show();
        }
        else
        {
            objEmployee.UpdateEmployeeContact(EmoloyeeId, true);
            if (!objEmployee.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objEmployee.ErrorMsg;
            }
        }
    }

    private void UpdateApprovedPersonalInfo(long EmoloyeeId, string Status)
    {
        InitializeEmployee();
        objEmployee.UpdateEmployee(EmoloyeeId, true);
        if (!objEmployee.IsError)
        {
            lblResult.ForeColor = Color.Green;
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
        else
        {
            InitializeEmployee();
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
        Clear();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        
            UpdateApprovedPersonalInfo(Convert.ToInt64(ViewState["EmployeeId"]), "Approved");
            btnApprove.Enabled = false;
            btnReject.Enabled = false;
            sendEmailToEmployee("Approved");
        
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        
            InitializeEmployee();
            objEmployee.UpdatePersonalChangeInfoStat(Convert.ToInt64(ViewState["EmployeeId"]), "Rejected");
            btnApprove.Enabled = false;
            btnReject.Enabled = false;
            if (!objEmployee.IsError)
            {
                lblResult.Text = "Successfully Updated Personal Info Change Request Status.";
                sendEmailToEmployee("Rejected");
            }
        
    }

    private void sendEmailToEmployee(string status)
    {
        string from = WebConfigurationManager.AppSettings["HREmail"];

        string to = objEmployee.GetEmployeeEmailByEmployeeId(Convert.ToInt64(ViewState["EmployeeId"])).ToString();
        string empName = string.Empty;

        string lastApprovePerson = string.Empty;
        

        DataTable employeeData = objEmployee.GetEmployee(Convert.ToInt32(ViewState["EmployeeId"]), Session["UserName"].ToString(), Convert.ToInt32(hfCompanyId.Value), "Common");
        if (employeeData.Rows.Count > 0)
        {
            empName = employeeData.Rows[0][4].ToString();
        }

        //Session["CoveringPerson"] = radcboCoveringPerson.SelectedItem.Value.ToString();
        StringWriter writer = new StringWriter();
        HtmlTextWriter html = new HtmlTextWriter(writer);

        Style style = new Style();
        style.Font.Name = "Times Roman";
        style.Font.Size = 12;
        style.Font.Bold = false;
        string htmlString = string.Empty;
        string subject = string.Empty;

        if (status == "Approved")
        {
            html.EnterStyle(style);
            html.RenderBeginTag(HtmlTextWriterTag.P);
            html.WriteEncodedText(string.Format("Dear {0},", empName));
            html.WriteBreak();
            html.RenderBeginTag(HtmlTextWriterTag.P);

            html.WriteEncodedText("Your personal information change request is approved.");
            html.WriteEncodedText(" Please log in to the Employee Self Service Portal to see the status.");
            html.WriteBreak();

            html.RenderBeginTag(HtmlTextWriterTag.P);
            html.WriteEncodedText("Thank you");
            html.WriteBreak();
            Style style1 = new Style();
            style1.Font.Name = "Times Roman";
            style1.Font.Size = 7;
            style1.Font.Bold = false;
            html.EnterStyle(style1);
            html.RenderBeginTag(HtmlTextWriterTag.P);
            html.WriteEncodedText("Please note that this email is automatically generated from the system, therefore, do not need to reply.");
            html.WriteBreak();
            html.Flush();
            htmlString = writer.ToString();

            subject = "Employee Personal Information Change Request Accepted ";
        }
        else if (status == "Rejected")
        {
            html.EnterStyle(style);
            html.RenderBeginTag(HtmlTextWriterTag.P);
            html.WriteEncodedText(string.Format("Dear {0},", empName));
            html.WriteBreak();
            html.RenderBeginTag(HtmlTextWriterTag.P);

            html.WriteEncodedText(string.Format("Your personal information change request has been rejected by {0}.", lastApprovePerson));
            html.WriteEncodedText(" Please log in to the Employee Self Service Portal to see the status.");
            html.WriteBreak();

            html.RenderBeginTag(HtmlTextWriterTag.P);
            html.WriteEncodedText("Thank you");
            html.WriteBreak();
            Style style1 = new Style();
            style1.Font.Name = "Times Roman";
            style1.Font.Size = 7;
            style1.Font.Bold = false;
            html.EnterStyle(style1);
            html.RenderBeginTag(HtmlTextWriterTag.P);
            html.WriteEncodedText("Please note that this email is automatically generated from the system, therefore, do not need to reply.");
            html.WriteBreak();
            html.Flush();
            htmlString = writer.ToString();

            subject = "Employee Personal Information Change Request Rejected ";
        }
        try
        {
            //Send email through exchange server
            objEmail.sendemailInExchangeServer(from, to, subject, htmlString);

            //lblResult.Text = " Leave applied successfully";
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
            lblResult.Text = errorMessage;
            HttpContext.Current.Response.Write(errorMessage);
        }
    }

    private void disableControls()
    {
        txtRemark.Enabled = false;
        txtRelationship.Enabled = false;
        txtAddress.Enabled = false;
        txtPostalCode.Enabled = false;
        txtHomeNo.Enabled = false;
        txtMobileNo.Enabled = false;
        txtEmail.Enabled = false;
        txtNIC.Enabled = false;
        txtEmergencycontactPerson.Enabled = false;
        txtEmergencyContactNo.Enabled = false;
        raddpPasportExpiyDate.Enabled = false;
        txtPassport.Enabled = false;
        txtVisa.Enabled = false;
        raddpPasportExpiyDate.Enabled = false;
        raddpVisaExpiyDate.Enabled = false;
    }

    #region Drop Down
   

    protected void btnUpdatePersonalInfoEditRequest_Click(object sender, EventArgs e)
    {
        GetEmployeeOfPersonalInfoChange(Convert.ToInt64(ViewState["EmployeeId"]));
        SetValues();
       
        disableControls();
        //btnUpdatePersonalInfoEditRequest.Visible = false;
        lbtnModify.Visible = false;
        btnPanel.Visible = true;
        btnPanelUpdate.Visible = false;
    }


    #endregion

}
    #endregion