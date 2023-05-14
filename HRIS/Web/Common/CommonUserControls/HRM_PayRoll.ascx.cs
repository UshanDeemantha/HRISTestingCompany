using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommenUserControls_HRM_PayRoll : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void imgbtnEmpPaySetup_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 1;
        Response.Redirect("~/PayRoll/Employee/EmployeePaySetup.aspx");
    }

    protected void imgbtnPaySetup_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 2;
        Response.Redirect("~/PayRoll/PaySetup/PayrollSetup.aspx");
    }

    protected void imgbtnLoan_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 4;
        Response.Redirect("~/PayRoll/Loan/Loan.aspx");
    }

    protected void imgbtnTools_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 6;
        Response.Redirect("~/PayRoll/Default.aspx");
    }
}