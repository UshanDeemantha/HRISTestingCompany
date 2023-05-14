using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommenUserControls_HRM_TimeAttendence : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void imgEmployeeTimeShift_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 1;
        Response.Redirect("~/TimeAttendance/Master/EmployeeTimeSetup.aspx");  
       
    }

    protected void imgLeaveTypes_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 2;
        Response.Redirect("~/TimeAttendance/Leave/LeaveTypes.aspx"); 
    }

    protected void imgShifts_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 3;
        Response.Redirect("~/TimeAttendance/Shifts/EmployeeShiftSheduler.aspx");
    }

    protected void imgTimeCard_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 4;
        Response.Redirect("~/TimeAttendance/Transaction/TimeCard.aspx"); 
    }
}