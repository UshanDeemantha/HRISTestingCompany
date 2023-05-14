using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;

public partial class HR_Reports_EmployementStatusDetails : System.Web.UI.Page
{
    EmploymentStatusReport EmploymentStatusReport = new EmploymentStatusReport();
    EmployeeRetirementDetails EmployeeRetirementDetails = new EmployeeRetirementDetails();

    EmployeePersonalFilesDetails EmployeePersonal = new EmployeePersonalFilesDetails();

    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "Reports_Common";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

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
                   
                }
            }
        }


        if (!IsPostBack)
        {
            var now = DateTime.Now;
            int year = DateTime.Now.Year;
            var startOfMonth = new DateTime(year, 1, 1);
            raddpFromDate.SelectedDate = (DateTime)startOfMonth;
            raddpToDate.SelectedDate = DateTime.Today;
            ASPxComboBox1.Enabled = false;
        }
        else
        {
            ASPxComboBox1.Enabled = true;
            raddpFromDate.Enabled = true;
            ddlEmployee.Enabled = true;
            radcbJobCategory.Enabled = true;
            raddpToDate.Enabled = true;
            btnView.Enabled = true;
            ddlEmplymentStatus.Enabled = true;
            lblEmplymentStatus.Enabled = true;
            lblActiveStatus.Enabled = true;
            ddlActiveStatus.Enabled = true;
        }

        if (ASPxComboBox1.Value != null)
        {
            btnClear.Enabled = true;
            btnReset.Enabled = true;

        }
        if (hfOrganizationStructure.Value != null)
        {

        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (hfOrganizationStructure.Value != "")
        {
            if (hfPreviousOrgId.Value != hfOrganizationStructure.Value)
            {
                ddlEmployee.SelectedIndex = -1;
                hfPreviousOrgId.Value = hfOrganizationStructure.Value;
            }
        }

    }

    protected void ReportViewer1_Load1(object sender, EventArgs e)
    {
        if (ASPxComboBox1.Value == "1")
        {
            EmploymentStatusReport.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            EmploymentStatusReport.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            EmploymentStatusReport.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            EmploymentStatusReport.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            EmploymentStatusReport.Parameters["FromDate"].Value = Convert.ToDateTime(raddpFromDate.SelectedDate);
            EmploymentStatusReport.Parameters["ToDate"].Value = Convert.ToDateTime(raddpToDate.SelectedDate);
            EmploymentStatusReport.Parameters["EmploymentTypeID"].Value = Convert.ToInt32(ddlEmplymentStatus.Value);
            EmploymentStatusReport.Parameters["ActiveStatus"].Value = Convert.ToInt32(ddlActiveStatus.Value);
            EmploymentStatusReport.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            EmploymentStatusReport.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = EmploymentStatusReport;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = true;
            raddpToDate.Enabled = true;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
        }

        if (ASPxComboBox1.Value == "2")
        {
            EmployeeRetirementDetails.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            EmployeeRetirementDetails.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            EmployeeRetirementDetails.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            EmployeeRetirementDetails.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            EmployeeRetirementDetails.Parameters["FromDate"].Value = Convert.ToDateTime(raddpFromDate.SelectedDate);
            EmployeeRetirementDetails.Parameters["ToDate"].Value = Convert.ToDateTime(raddpToDate.SelectedDate);
            EmployeeRetirementDetails.Parameters["EmploymentTypeID"].Value = Convert.ToInt32(ddlEmplymentStatus.Value);
            EmployeeRetirementDetails.Parameters["ActiveStatus"].Value = Convert.ToInt32(ddlActiveStatus.Value);
            EmployeeRetirementDetails.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            EmployeeRetirementDetails.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = EmployeeRetirementDetails;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = true;
            raddpToDate.Enabled = true;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
        }

        if (ASPxComboBox1.Value == "9")
        {
            EmployeePersonal.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            EmployeePersonal.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            EmployeePersonal.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            EmployeePersonal.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            ReportViewer1.Report = EmployeePersonal;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = false;
            raddpToDate.Enabled = false;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
        }
    }

    protected void btnOrganizationSelect_Click(object sender, EventArgs e)
    {
        ASPxComboBox1.Enabled = true;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlEmployee.Value = null;
        ASPxComboBox1.Value = null;
        var now = DateTime.Now;
        int year = DateTime.Now.Year;
        var startOfMonth = new DateTime(year, 1, 1);
        raddpFromDate.Enabled = true;
        raddpToDate.Enabled = true;
        ddlEmployee.Visible = true;
        Label3.Visible = true;
        ddlEmployee.Enabled = false;
        Label3.Enabled = false;
        radcbJobCategory.SelectedIndex = -1;
        btnClear.Enabled = false;
        btnReset.Enabled = false;
        ddlEmplymentStatus.Enabled = false;
        lblEmplymentStatus.Enabled = false;
        ddlEmplymentStatus.SelectedIndex = 1;
        lblActiveStatus.Enabled = false;
        ddlActiveStatus.Enabled = false;
        ddlActiveStatus.SelectedIndex = 0;
    }
    protected void ASPxComboBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlEmployee.Value = null;
        ASPxComboBox1.Value = null;
        var now = DateTime.Now;
        int year = DateTime.Now.Year;
        var startOfMonth = new DateTime(year, 1, 1);
        raddpFromDate.SelectedDate = (DateTime)startOfMonth;
        raddpToDate.SelectedDate = DateTime.Today;
        raddpFromDate.Enabled = false;
        raddpToDate.Enabled = false;
        ddlEmployee.Visible = true;
        Label3.Visible = true;
        lblOrganization.Text = "";
        ASPxComboBox1.Enabled = false;
        radcbJobCategory.SelectedIndex = -1;
        btnClear.Enabled = false;
        btnReset.Enabled = false;
        btnView.Enabled = false;
        ddlEmplymentStatus.Enabled = false;
        lblEmplymentStatus.Enabled = false;
        ddlEmplymentStatus.SelectedIndex = 1;
        ddlEmployee.Enabled = false;
        Label3.Enabled = false;
        lblActiveStatus.Enabled = false;
        ddlActiveStatus.Enabled = false;
        ddlActiveStatus.SelectedIndex = 0;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {

    }
    protected void CBActive_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void radcbJobCategory_TextChanged(object sender, EventArgs e)
    {
        ddlEmployee.SelectedIndex = -1;
    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        ddlEmployee.SelectedIndex = -1;
        ddlEmplymentStatus.SelectedIndex = 1;
        radcbJobCategory.SelectedIndex = -1;
        ddlActiveStatus.SelectedIndex = 0;
    }
    protected void ddlActiveStatus_TextChanged(object sender, EventArgs e)
    {
        ddlEmployee.SelectedIndex = -1;
    }
}