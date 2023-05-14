using System;

public partial class NoPermissions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnHomeNow_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.html");
    }
}