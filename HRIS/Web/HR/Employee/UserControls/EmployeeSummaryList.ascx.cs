using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
public partial class Employee_EmployeeSummaryList : System.Web.UI.UserControl
{
    Employee objEmployee = new Employee();
    DataTable dtEmployees;
    //#region Security

    //private const string ApplicationName = "HR";
    //private const string PageName = "EmployeeList";

    //MksSecurity objSecurity = new MksSecurity();

    //#endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //EmployeeSearch1.EmployeecodeContolId = tbSearchSupplier;
        //EmployeeSearch1.EmployeeDataList_Rad = RadListView1;
        //EmployeeSearch1.EmployeeIdContol = hfEmloyeeId;

        if (IsPostBack == false)
        {
            
            //DataTable dataTable = new DataTable();
            //dataTable = objEmployee.GetEmployee(Convert.ToInt64(0), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
            //for (int k = 0; k < dataTable.Rows.Count; k++)
            //{
            //    DataRow _drProcessedNewRow;
            //    _drProcessedNewRow = dataTable.Rows[k];
            //    //var empid = _drProcessedNewRow["EmployeeID"].ToString().Trim();

            //    dtEmployees = objEmployee.GetEmployee(Convert.ToInt64(0), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
            //    CollectionPager1.DataSource = dtEmployees.DefaultView;
            //    RadListView1.DataSource = dtEmployees;

            //    RadListView1.DataBind();
            //    CollectionPager1.BindToControl = RadListView1;


            //}
            // if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
            {
              //  Response.Redirect("~/HR/NoPermissions.aspx");
               // return;
            }
        }

        if (!IsPostBack)
        {
            //DataTable dataTable = new DataTable();
            //dataTable = objEmployee.GetEmployee(Convert.ToInt64(0), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
            //for (int k = 0; k < dataTable.Rows.Count; k++)
            //{
            //    DataRow _drProcessedNewRow;
            //    _drProcessedNewRow = dataTable.Rows[k];
            //    //var empid = _drProcessedNewRow["EmployeeID"].ToString().Trim();

            //    dtEmployees = objEmployee.GetEmployee(Convert.ToInt64(0), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
            // // CollectionPager1. = dtEmployees.DefaultView;
            //    RadListView1.DataSource = dtEmployees;

            //    RadListView1.DataBind();
            //   // CollectionPager1.ControlCssClass = 'pagecount';

            //   // CollectionPager1.BindToControl = RadListView1;
            //}
        }
        else
        {
            GetEmployeesInOrNotInTime("NotIn");
            //RadComboBox2.Enabled = true;
        }
    }
  private void GetEmployeesInOrNotInTime(string Type)
    {
        int jobCatID = 0;
        if (hfOrganizationStructure.Value == "")
            hfOrganizationStructure.Value = "0";


        //DataTable dataTable = objTimeSetup.GetEmployeesInOrNotInTime(jobCatID, Convert.ToInt64(hfOrganizationStructure.Value), Convert.ToInt32(Session["CompanyId"]), Type);
        //RadComboBox2.DataSource = dataTable;
        //RadComboBox2.DataBind();
    }
    protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void tbSearchSupplier_TextChanged(object sender, EventArgs e)
    {
        if (tbSearchSupplier.Text == string.Empty)
        {
           // hfEmloyeeId.Value = "0";
            //hfEmloyeeId.Value = "-99";
            try
            {
                //dtEmployees = objEmployee.GetEmployee(Convert.ToInt64(0), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
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

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlEmployeeStatus.SelectedIndex = 0;
        hfOrganizationStructure.Value = "0";
        // lblOrganization.Text = "";
        CmbCustomers.Text = "";
            
    }

    protected void RadButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeExcelUpdate.aspx");
        // ASPxPopupControl1.ShowOnPageLoad = true;
    }



    protected void CmbCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var empid = CmbCustomers.Value.ToString();
            hfEmloyeeId.Value = (empid);
            //DataTable dataTable = new DataTable();
            //dataTable = objEmployee.GetEmployee(Convert.ToInt64(empid), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
            //for (int k = 0; k < dataTable.Rows.Count; k++)
            //{
            //    DataRow _drProcessedNewRow;
            //    _drProcessedNewRow = dataTable.Rows[k];
            //    //var empid = _drProcessedNewRow["EmployeeID"].ToString().Trim();

            //    dtEmployees = objEmployee.GetEmployee(Convert.ToInt64(empid), Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
            //    CollectionPager1.DataSource = dtEmployees.DefaultView;
            //    RadListView1.DataSource = dtEmployees;

            //    RadListView1.DataBind();
            //    CollectionPager1.BindToControl = RadListView1;


            //}
        }
        catch (Exception ex)
        {

        }
        }

    protected void lkSelect_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeExcelDataUpload.aspx");
    }
    
    protected void AddEmpbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeAditionalInfo.aspx");
    }
}