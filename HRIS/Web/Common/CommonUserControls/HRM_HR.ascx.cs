using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommenUserControls_HRM_HR : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void imgbtnEmpAdditionalInfo_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 1;
        Response.Redirect("~/HR/Employee/EmployeeAditionalInfo.aspx");
    }

    protected void imgbtnEvents_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 4;
        Response.Redirect("~/HR/Event/Event.aspx");
    }

    protected void imgbtnNoticeBoard_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 3;
        Response.Redirect("~/HR/NoticeBoard/NoticeBoard.aspx");
    }
}