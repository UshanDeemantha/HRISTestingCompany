using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Common_Settings_AdminMenu : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        YearLabel.Text = DateTime.Now.Year.ToString();
        if (Session["CompanyName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else if (Session["CompanyId"].ToString() == "1")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company.png");
        }
        else if (Session["CompanyId"].ToString() == "2")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company2.png");
        }
        else if (Session["CompanyId"].ToString() == "3")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company3.png");
        }
        else if (Session["CompanyId"].ToString() == "4")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company4.png");
        }
        else if (Session["CompanyId"].ToString() == "5")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company5.png");
        }
        else if (Session["CompanyId"].ToString() == "6")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company6.png");
        }
        else if (Session["CompanyId"].ToString() == "7")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company7.png");
        }
        else if (Session["CompanyId"].ToString() == "8")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company8.png");
        }
        else if (Session["CompanyId"].ToString() == "9")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company9.png");
        }
        else if (Session["CompanyId"].ToString() == "10")
        {
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\CommonResources\\images\\company10.png");
        }

        // set Company Name
        lblCompanyName.Text = Session["CompanyName"].ToString();

        // set User Name
        lblUserName.InnerText = "Welcome, " + Session["UserName"].ToString();

        if (Session["UserName"].ToString() == "Ushan")
        {
            // btnAdminPanel.Visible = true;
        }
        else
        {
            // btnAdminPanel.Visible = false;
            // AdminPanel.Visible = false;
        }
        if (Session["UserTypeId"].ToString() == "2" || Session["UserTypeId"].ToString() == "3")
        {
            // ScreenPermission.Visible = true;
        }
        else
        {
            //  ScreenPermission.Visible = false;
            //  UserPermission.Visible = false;
        }

    }

    protected void RadPanelBar1_ItemClick(object sender, RadPanelBarEventArgs e)
    {
        string Menus = e.Item.Text;
        switch (Menus.ToLowerInvariant())
        {
            case "home":
                {
                    Response.Redirect("~/Home.html");
                    break;
                }
            case "system application":
                {
                    Response.Redirect("~/Common/Settings/NewAdminForms/SystemApplication.aspx");

                    break;
                }
            case "system menu":
                {
                    Response.Redirect("~/Common/Settings/NewAdminForms/SystemMenu.aspx");

                    break;
                }
            case "system forms":
                {
                    Response.Redirect("~/Common/Settings/NewAdminForms/SystemForms.aspx");

                    break;
                }
        }
    }
}