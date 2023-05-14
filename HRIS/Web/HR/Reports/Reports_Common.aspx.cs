using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;

public partial class HR_Reports_Reports_Common : System.Web.UI.Page
{
    EmployeeList EmployeeList = new EmployeeList();
    NewComers NewEmployee = new NewComers();
    ActiveEmployeeDetails Active = new ActiveEmployeeDetails();
    ResignEmployeeList Resigned = new ResignEmployeeList();
    InactiveEmployeeDetails InInactiveEmployeeDetails = new InactiveEmployeeDetails();
    TerminatedEmployeeDetails TerminatedEmployeeDetails = new TerminatedEmployeeDetails();    

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
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
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
            lblActiveStatus.Enabled = true;
            ddlActiveStatus.Enabled = true;
        }

        if (ASPxComboBox1.Value != null)
        {
            btnClear.Enabled = true;
            btnReset.Enabled = true;

        }
        
        //if (hfOrganizationStructure.Value != "")
        //{
        //    if(hfPreviousOrgId.Value != hfOrganizationStructure.Value)
        //    {
        //        ddlEmployee.SelectedIndex = -1;
        //        hfPreviousOrgId.Value = hfOrganizationStructure.Value;
        //    }
        //}
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (hfOrganizationStructure.Value != "")
        {
            if (hfPreviousOrgId.Value != hfOrganizationStructure.Value)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('show');", true);
                //upModal.Update();
                ddlEmployee.SelectedIndex = -1;
                hfPreviousOrgId.Value = hfOrganizationStructure.Value;
            }
        }
     

    }

    protected void ReportViewer1_Load1(object sender, EventArgs e)
    {
        if (ASPxComboBox1.Value == "1")
        {
            EmployeeList.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            EmployeeList.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            EmployeeList.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            EmployeeList.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            EmployeeList.Parameters["ActiveStatus"].Value = Convert.ToInt32(ddlActiveStatus.Value);
            EmployeeList.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            EmployeeList.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = EmployeeList;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = false;
            raddpToDate.Enabled = false;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
            lblActiveStatus.Enabled = true;
            ddlActiveStatus.Enabled = true;
        }

        if (ASPxComboBox1.Value == "2")
        {
            NewEmployee.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            NewEmployee.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            NewEmployee.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            NewEmployee.Parameters["FromDate"].Value = Convert.ToDateTime(raddpFromDate.SelectedDate);
            NewEmployee.Parameters["ToDate"].Value = Convert.ToDateTime(raddpToDate.SelectedDate);
            NewEmployee.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            NewEmployee.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            NewEmployee.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = NewEmployee;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = true;
            raddpToDate.Enabled = true;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
            lblActiveStatus.Enabled = false;
            ddlActiveStatus.Enabled = false;
        }

        if (ASPxComboBox1.Value == "3")
        {
            Active.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            Active.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            Active.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            Active.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            Active.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            Active.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = Active;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = true;
            raddpToDate.Enabled = true;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
            lblActiveStatus.Enabled = false;
            ddlActiveStatus.Enabled = false;
        }

        if (ASPxComboBox1.Value == "4")
        {
            Resigned.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            Resigned.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            Resigned.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            Resigned.Parameters["FromDate"].Value = Convert.ToDateTime(raddpFromDate.SelectedDate);
            Resigned.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            Resigned.Parameters["ToDate"].Value = Convert.ToDateTime(raddpToDate.SelectedDate);
            Resigned.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            Resigned.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = Resigned;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = true;
            raddpToDate.Enabled = true;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
            lblActiveStatus.Enabled = false;
            ddlActiveStatus.Enabled = false;
        }

        if (ASPxComboBox1.Value == "5")
        {
            InInactiveEmployeeDetails.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            InInactiveEmployeeDetails.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            InInactiveEmployeeDetails.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            InInactiveEmployeeDetails.Parameters["FromDate"].Value = Convert.ToDateTime(raddpFromDate.SelectedDate);
            InInactiveEmployeeDetails.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            InInactiveEmployeeDetails.Parameters["ToDate"].Value = Convert.ToDateTime(raddpToDate.SelectedDate);
            InInactiveEmployeeDetails.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            InInactiveEmployeeDetails.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = InInactiveEmployeeDetails;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = true;
            raddpToDate.Enabled = true;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
            lblActiveStatus.Enabled = false;
            ddlActiveStatus.Enabled = false;
        }

        if (ASPxComboBox1.Value == "6")
        {
            TerminatedEmployeeDetails.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            TerminatedEmployeeDetails.Parameters["OrgStructureID"].Value = Convert.ToInt32(hfOrganizationStructure.Value);
            TerminatedEmployeeDetails.Parameters["JobCategoryID"].Value = Convert.ToInt32(radcbJobCategory.Value);
            TerminatedEmployeeDetails.Parameters["FromDate"].Value = Convert.ToDateTime(raddpFromDate.SelectedDate);
            TerminatedEmployeeDetails.Parameters["EmployeeId"].Value = Convert.ToInt32(ddlEmployee.Value);
            TerminatedEmployeeDetails.Parameters["ToDate"].Value = Convert.ToDateTime(raddpToDate.SelectedDate);
            TerminatedEmployeeDetails.Parameters["ReportValue"].Value = Convert.ToInt32(ASPxComboBox1.Value);
            TerminatedEmployeeDetails.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = TerminatedEmployeeDetails;
            ReportViewer1.DataBind();
            raddpFromDate.Enabled = true;
            raddpToDate.Enabled = true;
            Label3.Visible = true;
            ddlEmployee.Visible = true;
            lblActiveStatus.Enabled = false;
            ddlActiveStatus.Enabled = false;
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
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        raddpFromDate.Enabled = true;
        raddpToDate.Enabled = true;
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
        var now = DateTime.Now;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        raddpFromDate.SelectedDate = (DateTime)startOfMonth;
        raddpToDate.SelectedDate = DateTime.Today;
        raddpFromDate.Enabled = false;
        raddpToDate.Enabled = false;
        ddlEmployee.Visible = false;
        Label3.Visible = false;
        //lblOrganization.Text = "";
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
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        //upModal.Update();
        ddlEmployee.SelectedIndex = -1;
        
    }

    protected void btnADDCOM_Click(object sender, ImageClickEventArgs e)
    {
      //  ASPxPopupControl1.ShowOnPageLoad = true;
    }

    protected void Unnamed_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnADDCOMs_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //lblfdate.Text = raddpFromDate.SelectedDate.ToString();
        //lbltdate.Text = raddpToDate.SelectedDate.ToString();
        //lbljcategory.Text = radcbJobCategory.Text;
        //lblstatus.Text = ddlActiveStatus.Text;
        //lblemp.Text = ddlEmployee.Text;
    }

    protected void ddlActiveStatus_TextChanged1(object sender, EventArgs e)
    {
        
    }


    protected void CmbCity_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        
    }
}