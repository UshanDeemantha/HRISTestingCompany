using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Reports_CompanyDetails : System.Web.UI.Page
{

    BranchDetail BRANCH = new BranchDetail();
    CompanyDetails COMPANY = new CompanyDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

    }

    protected void ReportViewer1_Load1(object sender, EventArgs e)
    {
        if (ASPxComboBox1.Value == "1")
        {

            COMPANY.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            COMPANY.Parameters["UserName"].Value =(Session["UserName"].ToString());
            ReportViewer1.Report = COMPANY;
            ReportViewer1.DataBind();

        }

        if (ASPxComboBox1.Value == "2")
        {
            BRANCH.Parameters["Company"].Value = Convert.ToInt32(Session["CompanyId"].ToString());
            BRANCH.Parameters["UserName"].Value = (Session["UserName"].ToString());
            ReportViewer1.Report = BRANCH;
            ReportViewer1.DataBind();
        }
    }
    protected void ASPxComboBox1_TextChanged(object sender, EventArgs e)
    {
        btnClear.Enabled = true;
        btnReset.Enabled = true;
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ASPxComboBox1.Value = null;
        var now = DateTime.Now;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        btnClear.Enabled = false;
        btnReset.Enabled = false;
        btnView.Enabled = false;
    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
    }
}