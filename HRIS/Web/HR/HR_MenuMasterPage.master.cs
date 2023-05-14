using System;
using System.Configuration;
using Telerik.Web.UI;

public partial class HR_MenuMasterPage : System.Web.UI.MasterPage
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
            Logo.Attributes["src"] = ResolveUrl("~\\App_Themes\\NewTheme\\img\\menu\\logo.png");
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
    private void NavigateToPage(string navigationText)
    {
        switch (navigationText.ToLowerInvariant())
        {
            case "home":
                {
                    Response.Redirect("~/Home.html");
                    break;
                }
            case "common information":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyProfile.aspx");

                    break;
                }
            case "employee list":
                {
                    Response.Redirect("~/HR/Employee/EmployeeList.aspx");
                    break;
                }
            case "new employee":
                {
                    Response.Redirect("~/HR/Employee/EmployeeAditionalInfo.aspx");
                    break;
                }
            case "bulk employee upload":
                {
                    Response.Redirect("~/HR/Employee/EmployeeExcelDataUpload.aspx");
                    break;
                }
            case "bulk employee additional upload":
                {
                    Response.Redirect("~/HR/Employee/EmployeeAdditionalDataUpload.aspx");
                    break;
                }
            case "export employee excel":
                {
                    Response.Redirect("~/HR/Employee/EmployeeExcelUpdate.aspx");
                    break;
                }
            case "change company":
                {
                    Response.Redirect("~/Common/Employee/ChangeEmpCompany.aspx");
                    break;
                }
            case "employee files":
                {
                    Response.Redirect("~/HR/FileUpload/Upload.aspx");
                    break;
                }
            case "notice board":
                {
                    Response.Redirect("~/HR/NoticeBoard/NoticeBoard.aspx");
                    break;
                }
            case "event types":
                {
                    Response.Redirect("~/HR/Event/EventType.aspx");
                    break;
                }
            case "event submission":
                {
                    Response.Redirect("~/HR/Event/Event.aspx");
                    break;
                }
            case "employee details":
                {
                    Response.Redirect("~/HR/Reports/Reports_Common.aspx");
                    break;
                }
            case "employee contact details":
                {
                    Response.Redirect("~/HR/Reports/EmployeeContactDetails.aspx");
                    break;
                }
            case "employement status details":
                {
                    Response.Redirect("~/HR/Reports/EmployementStatusDetails.aspx");
                    break;
                }
            case "logout user":
                {
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                    break;
                }
            default:
                break;
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
            case "common information":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyProfile.aspx");

                    break;
                }
            case "employee list":
                {
                    Response.Redirect("~/HR/Employee/EmployeeList.aspx");
                    break;
                }
            case "new employee":
                {
                    Response.Redirect("~/HR/Employee/EmployeeAditionalInfo.aspx");
                    break;
                }
            case "bulk employee upload":
                {
                    Response.Redirect("~/HR/Employee/EmployeeExcelDataUpload.aspx");
                    break;
                }
            case "bulk employee additional upload":
                {
                    Response.Redirect("~/HR/Employee/EmployeeAdditionalDataUpload.aspx");
                    break;
                }
            case "change company":
                {
                    Response.Redirect("~/Common/Employee/ChangeEmpCompany.aspx");
                    break;
                }
            case "employee files":
                {
                    Response.Redirect("~/HR/FileUpload/Upload.aspx");
                    break;
                }
            case "notice board":
                {
                    Response.Redirect("~/HR/NoticeBoard/NoticeBoard.aspx");
                    break;
                }
            case "event types":
                {
                    Response.Redirect("~/HR/Event/EventType.aspx");
                    break;
                }
            case "event submission":
                {
                    Response.Redirect("~/HR/Event/Event.aspx");
                    break;
                }
            case "employee details":
                {
                    Response.Redirect("~/HR/Reports/Reports_Common.aspx");
                    break;
                }
            case "employee contact details":
                {
                    Response.Redirect("~/HR/Reports/EmployeeContactDetails.aspx");
                    break;
                }
            case "employement status details":
                {
                    Response.Redirect("~/HR/Reports/EmployementStatusDetails.aspx");
                    break;
                }
            case "export employee excel":
                {
                    Response.Redirect("~/HR/Employee/EmployeeExcelUpdate.aspx");
                    break;
                }
            case "logout user":
                {
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                    break;
                }
            default:
                break;
        }
    }

    protected void RadPanelBar3_ItemClick(object sender, RadPanelBarEventArgs e)
    {
        string Menus = e.Item.Text;
        switch (Menus.ToLowerInvariant())
        {
            case "home":
                {
                    Response.Redirect("~/Home.html");
                    break;
                }
            case "common information":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyProfile.aspx");

                    break;
                }
            case "employee list":
                {
                    Response.Redirect("~/HR/Employee/EmployeeList.aspx");
                    break;
                }
            case "new employee":
                {
                    Response.Redirect("~/HR/Employee/EmployeeAditionalInfo.aspx");
                    break;
                }
            case "bulk employee upload":
                {
                    Response.Redirect("~/HR/Employee/EmployeeExcelDataUpload.aspx");
                    break;
                }
            case "bulk employee additional upload":
                {
                    Response.Redirect("~/HR/Employee/EmployeeAdditionalDataUpload.aspx");
                    break;
                }
            case "change company":
                {
                    Response.Redirect("~/Common/Employee/ChangeEmpCompany.aspx");
                    break;
                }
            case "employee files":
                {
                    Response.Redirect("~/HR/FileUpload/Upload.aspx");
                    break;
                }
            case "notice board":
                {
                    Response.Redirect("~/HR/NoticeBoard/NoticeBoard.aspx");
                    break;
                }
            case "event types":
                {
                    Response.Redirect("~/HR/Event/EventType.aspx");
                    break;
                }
            case "event submission":
                {
                    Response.Redirect("~/HR/Event/Event.aspx");
                    break;
                }
            case "employee details":
                {
                    Response.Redirect("~/HR/Reports/Reports_Common.aspx");
                    break;
                }
            case "employee contact details":
                {
                    Response.Redirect("~/HR/Reports/EmployeeContactDetails.aspx");
                    break;
                }
            case "employement status details":
                {
                    Response.Redirect("~/HR/Reports/EmployementStatusDetails.aspx");
                    break;
                }
            case "export employee excel":
                {
                    Response.Redirect("~/HR/Employee/EmployeeExcelUpdate.aspx");
                    break;
                }
            case "logout user":
                {
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                    break;
                }
            default:
                break;
        }
    }
}

