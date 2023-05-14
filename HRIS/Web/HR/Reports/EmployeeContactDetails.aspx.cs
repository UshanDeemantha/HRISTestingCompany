using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;

public partial class HR_Reports_EmployeeContactDetails : System.Web.UI.Page
{
    EmployeeContactDetails EmployeeContact = new EmployeeContactDetails();
    EmployeeEmergencyContactDetails EmployeeEmergency = new EmployeeEmergencyContactDetails();

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
            ASPxComboBox1.Enabled = false;
        }
        else
        {
            ASPxComboBox1.Enabled = true;
            ddlEmployee.Enabled = true;
            radcbJobCategory.Enabled = true;
            btnView.Enabled = true;
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
        if (ASPxComboBox1.Value == "7")
        {
            EmployeeContact.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            EmployeeContact.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            EmployeeContact.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            EmployeeContact.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            EmployeeContact.Parameters["ActiveStatus"].Value = Convert.ToInt32(ddlActiveStatus.Value);
            EmployeeContact.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            EmployeeContact.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = EmployeeContact;
            ReportViewer1.DataBind();
            Label3.Visible = true;
            ddlEmployee.Visible = true;
        }

        if (ASPxComboBox1.Value == "8")
        {
            EmployeeEmergency.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            EmployeeEmergency.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            EmployeeEmergency.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            EmployeeEmergency.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            EmployeeEmergency.Parameters["ActiveStatus"].Value = Convert.ToInt32(ddlActiveStatus.Value);
            EmployeeEmergency.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            EmployeeEmergency.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = EmployeeEmergency;
            ReportViewer1.DataBind();
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
        ddlEmployee.Visible = false;
        Label3.Visible = false;
        radcbJobCategory.SelectedIndex = -1;
        btnClear.Enabled = false;
        btnReset.Enabled = false;
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
        ddlEmployee.Visible = false;
        Label3.Visible = false;
        lblOrganization.Text = "";
        ASPxComboBox1.Enabled = false;
        radcbJobCategory.SelectedIndex = -1;
        btnClear.Enabled = false;
        btnReset.Enabled = false;
        btnView.Enabled = false;
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
        ddlActiveStatus.SelectedIndex = 0;
        radcbJobCategory.SelectedIndex = -1;
    }

    protected void ddlActiveStatus_TextChanged(object sender, EventArgs e)
    {
        ddlEmployee.SelectedIndex = -1;
    }
}