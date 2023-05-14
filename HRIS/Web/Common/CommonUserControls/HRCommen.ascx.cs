using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommenUserControls_HRCommen : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 1;
        Response.Redirect("~/Common/Employee/Employee.aspx");
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 2;
        Response.Redirect("~/Common/CompanyProfile/CompanyProfile.aspx");
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 3;
        Response.Redirect("~/Common/Organization/OrganizationalView.aspx");
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Session["SelectedTabIndex"] = 4;
        Response.Redirect("~/Common/Designation/DesignationView.aspx");
    }
}