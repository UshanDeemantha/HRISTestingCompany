using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;

public partial class Employee_EmployeeSummaryList : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "EmployeeList";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Employee objEmployee = new Employee();
    DataTable dtEmployees;
    Wizard wizEmployeeAditional;

    public Wizard EmployeeAditionalWizard
    {
        set
        {
            wizEmployeeAditional = (Wizard)value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton lbtnMod = new LinkButton();

        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (IsPostBack == false)
            {
                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/Common/NoPermissions.aspx");
                    return;
                }
                else
                {
                    try
                    {
                       
                    }
                    catch { }   
                }
            }
        }

        EmployeeSearch1.EmployeecodeContolId = tbSearchSupplier;
       EmployeeSearch1.EmployeeDataList_Rad = RadListView1;
       EmployeeSearch1.EmployeeIdContol = hfEmloyeeId;
    }

    protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    {
      
    }

    protected void tbSearchSupplier_TextChanged(object sender, EventArgs e)
    {
        if (tbSearchSupplier.Text == string.Empty)
        {
            hfEmloyeeId.Value = "0";
            try
            {
                //dtEmployees = objEmployee.GetEmployee(Convert.ToInt64(0));
                //CollectionPager1.DataSource = dtEmployees.DefaultView;

                //CollectionPager1.BindToControl = DataList1;
                //DataList1.DataSource = CollectionPager1.DataSourcePaged;

                //DataList1.DataBind();
            }
            catch
            { }
        }
    }

    protected void hfEmloyeeId_ValueChanged(object sender, EventArgs e)
    {

    }

    protected void lbtnModify_Click(object sender, EventArgs e)
    {

    }
    protected void RadListView1_ItemCreated(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
       LinkButton lbtnMod  = e.Item.FindControl("lbtnModify") as LinkButton;
        lbtnMod.Visible = false;



        if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly) == true ||
            objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly) == true)
        {
            lbtnMod.Visible = true;
        }
        else
        {
            lbtnMod.Visible = false;
        }
    }
}