using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using HRM.Common.BLL;
using System.Data;
public partial class Common_MenuMasterPage : System.Web.UI.MasterPage
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
        if (Session["UserTypeId"].ToString()== "2" || Session["UserTypeId"].ToString() == "3")
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
            case "employee management":
                {
                    Response.Redirect("~/HR/Employee/EmployeeList.aspx");

                    break;
                }
            case "company profile":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyProfile.aspx");
                    break;
                }
            case "company branches":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyBranch.aspx");
                    break;
                }
            case "employee types":
                {
                    Response.Redirect("~/Common/Employee/EmploymentType.aspx");
                    break;
                }
            case "employee grades":
                {
                    Response.Redirect("~/Common/Employee/EmployeeGrade.aspx");
                    break;
                }
            case "job categories":
                {
                    Response.Redirect("~/Common/Reference/JobCategory.aspx");
                    break;
                }
            case "job grades":
                {
                    Response.Redirect("~/Common/Reference/JobGrade.aspx");
                    break;
                }
            case "job specifications":
                {
                    Response.Redirect("~/Common/Reference/JobSpecification.aspx");
                    break;
                }
            case "department levels":
                {
                    Response.Redirect("~/Common/Organization/OrganizationLevels.aspx");
                    break;
                }
            case "department types":
                {
                    Response.Redirect("~/Common/Organization/OrganizationType.aspx");
                    break;
                }
            case "department structure":
                {
                    Response.Redirect("~/Common/Organization/OrganizationStructure.aspx");
                    break;
                }
            case "designation types":
                {
                    Response.Redirect("~/Common/Designation/Designation.aspx");
                    break;
                }
            case "designation structure":
                {
                    Response.Redirect("~/Common/Designation/DesignationStructure.aspx");
                    break;
                }
            case "institutes":
                {
                    Response.Redirect("~/Common/HR/Institutes/Institutes.aspx");
                    break;
                }
            case "institute types":
                {
                    Response.Redirect("~/Common/HR/Institutes/InstituteType.aspx");
                    break;
                }
            case "courses":
                {
                    Response.Redirect("~/Common/HR/Course/Course.aspx");
                    break;
                }
            case "course types":
                {
                    Response.Redirect("~/Common/HR/Course/CourseType.aspx");
                    break;
                }
            case "certifications":
                {
                    Response.Redirect("~/Common/HR/Certification/Certification.aspx");
                    break;
                }
            case "memberships":
                {
                    Response.Redirect("~/Common/HR/Membership/Membership.aspx");
                    break;
                }
            case "membership types":
                {
                    Response.Redirect("~/Common/HR/Membership/MembershipType.aspx");
                    break;
                }
            case "skills":
                {
                    Response.Redirect("~/Common/HR/Skills/Skill.aspx");
                    break;
                }
            case "skill grades":
                {
                    Response.Redirect("~/Common/HR/Skills/SkillGrade.aspx");
                    break;
                }
            case "fluency":
                {
                    Response.Redirect("~/Common/HR/Fluency/Fluency.aspx");
                    break;
                }
            case "fluency type":
                {
                    Response.Redirect("~/Common/HR/Fluency/FluencyType.aspx");
                    break;
                }
            case "language":
                {
                    Response.Redirect("~/Common/HR/Languages/Language.aspx");
                    break;
                }
            case "sports":
                {
                    Response.Redirect("~/Common/HR/Sport/Sport.aspx");
                    break;
                }
            case "approval workflows":
                {
                    Response.Redirect("~/Common/Settings/CommonWorkFlow.aspx");
                    break;
                }
            case "workflow assigning":
                {
                    Response.Redirect("~/Common/Settings/AssignCommonGroups.aspx");
                    break;
                }
            case "create users":
                {
                    Response.Redirect("~/Common/Settings/SystemUsers.aspx");
                    break;
                }
            case "block users":
                {
                    Response.Redirect("~/Common/Settings/BlockUser.aspx");
                    break;
                }
            case "user permissions":
                {
                    Response.Redirect("~/Common/Settings/UserPermissionView.aspx");
                    break;
                }
            case "user companies":
                {
                    Response.Redirect("~/Common/Settings/UserCompany.aspx");
                    break;
                }
            case "user payroll":
                {
                    Response.Redirect("~/Common/Settings/UserPayrollPermission.aspx");
                    break;
                }
            case "admin panel":
                {
                    Response.Redirect("~/Common/Settings/AdminPanel.aspx");
                    break;
                }
            case "company details":
                {
                    Response.Redirect("~/Common/Reports/CompanyDetails.aspx");

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
            case "employee management":
                {
                    Response.Redirect("~/HR/Employee/EmployeeList.aspx");

                    break;
                }
            case "company profile":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyProfile.aspx");
                    break;
                }
            case "company branches":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyBranch.aspx");
                    break;
                }
            case "employee types":
                {
                    Response.Redirect("~/Common/Employee/EmploymentType.aspx");
                    break;
                }
            case "employee grades":
                {
                    Response.Redirect("~/Common/Employee/EmployeeGrade.aspx");
                    break;
                }
            case "job categories":
                {
                    Response.Redirect("~/Common/Reference/JobCategory.aspx");
                    break;
                }
            case "job grades":
                {
                    Response.Redirect("~/Common/Reference/JobGrade.aspx");
                    break;
                }
            case "job specifications":
                {
                    Response.Redirect("~/Common/Reference/JobSpecification.aspx");
                    break;
                }
            case "department levels":
                {
                    Response.Redirect("~/Common/Organization/OrganizationLevels.aspx");
                    break;
                }
            case "department types":
                {
                    Response.Redirect("~/Common/Organization/OrganizationType.aspx");
                    break;
                }
            case "department structure":
                {
                    Response.Redirect("~/Common/Organization/OrganizationStructure.aspx");
                    break;
                }
            case "designation types":
                {
                    Response.Redirect("~/Common/Designation/Designation.aspx");
                    break;
                }
            case "designation structure":
                {
                    Response.Redirect("~/Common/Designation/DesignationStructure.aspx");
                    break;
                }
            case "institutes":
                {
                    Response.Redirect("~/Common/HR/Institutes/Institutes.aspx");
                    break;
                }
            case "institute types":
                {
                    Response.Redirect("~/Common/HR/Institutes/InstituteType.aspx");
                    break;
                }
            case "courses":
                {
                    Response.Redirect("~/Common/HR/Course/Course.aspx");
                    break;
                }
            case "course types":
                {
                    Response.Redirect("~/Common/HR/Course/CourseType.aspx");
                    break;
                }
            case "certifications":
                {
                    Response.Redirect("~/Common/HR/Certification/Certification.aspx");
                    break;
                }
            case "memberships":
                {
                    Response.Redirect("~/Common/HR/Membership/Membership.aspx");
                    break;
                }
            case "membership types":
                {
                    Response.Redirect("~/Common/HR/Membership/MembershipType.aspx");
                    break;
                }
            case "skills":
                {
                    Response.Redirect("~/Common/HR/Skills/Skill.aspx");
                    break;
                }
            case "skill grades":
                {
                    Response.Redirect("~/Common/HR/Skills/SkillGrade.aspx");
                    break;
                }
            case "fluency":
                {
                    Response.Redirect("~/Common/HR/Fluency/Fluency.aspx");
                    break;
                }
            case "fluency type":
                {
                    Response.Redirect("~/Common/HR/Fluency/FluencyType.aspx");
                    break;
                }
            case "language":
                {
                    Response.Redirect("~/Common/HR/Languages/Language.aspx");
                    break;
                }
            case "sports":
                {
                    Response.Redirect("~/Common/HR/Sport/Sport.aspx");
                    break;
                }
            case "approval workflows":
                {
                    Response.Redirect("~/Common/Settings/CommonWorkFlow.aspx");
                    break;
                }
            case "workflow assigning":
                {
                    Response.Redirect("~/Common/Settings/AssignCommonGroups.aspx");
                    break;
                }
            case "create users":
                {
                    Response.Redirect("~/Common/Settings/SystemUsers.aspx");
                    break;
                }
            case "block users":
                {
                    Response.Redirect("~/Common/Settings/BlockUser.aspx");
                    break;
                }
            case "user permissions":
                {
                    Response.Redirect("~/Common/Settings/UserPermissionView.aspx");
                    break;
                }
            case "user companies":
                {
                    Response.Redirect("~/Common/Settings/UserCompany.aspx");
                    break;
                }
            case "user payroll":
                {
                    Response.Redirect("~/Common/Settings/UserPayrollPermission.aspx");
                    break;
                }
            case "admin panel":
                {
                    Response.Redirect("~/Common/Settings/NewAdminForms/SystemApplication.aspx");
                    break;
                }
            case "company details":
                {
                    Response.Redirect("~/Common/Reports/CompanyDetails.aspx");

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
            case "employee management":
                {
                    Response.Redirect("~/HR/Employee/EmployeeList.aspx");

                    break;
                }
            case "company profile":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyProfile.aspx");
                    break;
                }
            case "company branches":
                {
                    Response.Redirect("~/Common/CompanyProfile/CompanyBranch.aspx");
                    break;
                }
            case "employee types":
                {
                    Response.Redirect("~/Common/Employee/EmploymentType.aspx");
                    break;
                }
            case "employee grades":
                {
                    Response.Redirect("~/Common/Employee/EmployeeGrade.aspx");
                    break;
                }
            case "job categories":
                {
                    Response.Redirect("~/Common/Reference/JobCategory.aspx");
                    break;
                }
            case "job grades":
                {
                    Response.Redirect("~/Common/Reference/JobGrade.aspx");
                    break;
                }
            case "job specifications":
                {
                    Response.Redirect("~/Common/Reference/JobSpecification.aspx");
                    break;
                }
            case "department levels":
                {
                    Response.Redirect("~/Common/Organization/OrganizationLevels.aspx");
                    break;
                }
            case "department types":
                {
                    Response.Redirect("~/Common/Organization/OrganizationType.aspx");
                    break;
                }
            case "department structure":
                {
                    Response.Redirect("~/Common/Organization/OrganizationStructure.aspx");
                    break;
                }
            case "designation types":
                {
                    Response.Redirect("~/Common/Designation/Designation.aspx");
                    break;
                }
            case "designation structure":
                {
                    Response.Redirect("~/Common/Designation/DesignationStructure.aspx");
                    break;
                }
            case "institutes":
                {
                    Response.Redirect("~/Common/HR/Institutes/Institutes.aspx");
                    break;
                }
            case "institute types":
                {
                    Response.Redirect("~/Common/HR/Institutes/InstituteType.aspx");
                    break;
                }
            case "courses":
                {
                    Response.Redirect("~/Common/HR/Course/Course.aspx");
                    break;
                }
            case "course types":
                {
                    Response.Redirect("~/Common/HR/Course/CourseType.aspx");
                    break;
                }
            case "certifications":
                {
                    Response.Redirect("~/Common/HR/Certification/Certification.aspx");
                    break;
                }
            case "memberships":
                {
                    Response.Redirect("~/Common/HR/Membership/Membership.aspx");
                    break;
                }
            case "membership types":
                {
                    Response.Redirect("~/Common/HR/Membership/MembershipType.aspx");
                    break;
                }
            case "skills":
                {
                    Response.Redirect("~/Common/HR/Skills/Skill.aspx");
                    break;
                }
            case "skill grades":
                {
                    Response.Redirect("~/Common/HR/Skills/SkillGrade.aspx");
                    break;
                }
            case "fluency":
                {
                    Response.Redirect("~/Common/HR/Fluency/Fluency.aspx");
                    break;
                }
            case "fluency type":
                {
                    Response.Redirect("~/Common/HR/Fluency/FluencyType.aspx");
                    break;
                }
            case "language":
                {
                    Response.Redirect("~/Common/HR/Languages/Language.aspx");
                    break;
                }
            case "sports":
                {
                    Response.Redirect("~/Common/HR/Sport/Sport.aspx");
                    break;
                }
            case "approval workflows":
                {
                    Response.Redirect("~/Common/Settings/CommonWorkFlow.aspx");
                    break;
                }
            case "workflow assigning":
                {
                    Response.Redirect("~/Common/Settings/AssignCommonGroups.aspx");
                    break;
                }
            case "create users":
                {
                    Response.Redirect("~/Common/Settings/SystemUsers.aspx");
                    break;
                }
            case "block users":
                {
                    Response.Redirect("~/Common/Settings/BlockUser.aspx");
                    break;
                }
            case "user permissions":
                {
                    Response.Redirect("~/Common/Settings/UserPermissionView.aspx");
                    break;
                }
            case "user companies":
                {
                    Response.Redirect("~/Common/Settings/UserCompany.aspx");
                    break;
                }
            case "user payroll":
                {
                    Response.Redirect("~/Common/Settings/UserPayrollPermission.aspx");
                    break;
                }
            case "admin panel":
                {
                    Response.Redirect("~/Common/Settings/AdminPanel.aspx");
                    break;
                }
            case "company details":
                {
                    Response.Redirect("~/Common/Reports/CompanyDetails.aspx");

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

