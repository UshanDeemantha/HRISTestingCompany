using System;
using HRM.Common.BLL;

public partial class ReportCommon : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.PathAndQuery);
        }
        else
        {
            string reportname = Request.QueryString["ReportName"].ToString();
            //Report_CompanyProfile _repot = new Report_CompanyProfile();
            //Report_Designation _repot = new Report_Designation();
            //Report_Organization _repot = new Report_Organization();
            //Report_JobSpecification _repot = new Report_JobSpecification();

            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, reportname, HRM.AccessRights.ViewOnly) == false)
            {
                Response.Redirect("~/Common/NoPermissions.aspx");
                return;
            }
            else
            {
                if (IsPostBack == false)
                {
                    ReportViewer1.ShowPrintButton = false;
                    ReportViewer1.ShowExportGroup = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, reportname, HRM.AccessRights.FullControl) == true)
                    {
                        ReportViewer1.ShowPrintButton = true;
                        ReportViewer1.ShowExportGroup = true;
                    }
                    else
                    {
                        ReportViewer1.ShowPrintButton = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, reportname, HRM.AccessRights.AddOnly);
                        ReportViewer1.ShowPrintButton = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, reportname, HRM.AccessRights.EditOnly);
                        ReportViewer1.ShowPrintButton = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, reportname, HRM.AccessRights.DeleteOnly);

                        ReportViewer1.ShowExportGroup = ReportViewer1.ShowPrintButton;
                    }
                }

                switch (reportname)
                {
                    //case "Report_EmployeeType":
                    //    {
                    //        Report_EmployeeType _repot = new Report_EmployeeType();
                    //        ReportViewer1.Report = _repot;
                    //        ReportViewer1.ShowPrintButton = false;
                    //        ReportViewer1.ShowExportGroup = ReportViewer1.ShowPrintButton;

                    //        break;
                    //    }
                    default:
                        break;
                }

            }
        }
    }
}