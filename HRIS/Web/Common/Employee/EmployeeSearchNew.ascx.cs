using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using SiteUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Common_Employee_EmployeeSearchNew : System.Web.UI.UserControl
{
    private long _employeeId;
    RadTextBox txteemployeeCode;
    HiddenField hfeemployeeId;
    DataTable dtEmployees;
    Employee objEmployee;
    DataList dlEmployees;
    CollectionPager cpEmployee;
    RadListView rdLwEmlpoyees;


    public RadTextBox EmployeecodeContolId
    {
        set
        {
            txteemployeeCode = (RadTextBox)value;
        }
    }

    public CollectionPager EmployeeCollectopnPager
    {
        set
        {
            cpEmployee = (CollectionPager)value;
        }
    }

    public DataList EmployeeDataList
    {
        set
        {
            dlEmployees = (DataList)value;
        }
    }

    public RadListView EmployeeDataList_Rad
    {
        set
        {
            rdLwEmlpoyees = (RadListView)value;
        }
    }

    public HiddenField EmployeeIdContol
    {
        set
        {
            hfeemployeeId = (HiddenField)value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            for (int Col = 2; Col < gvSearch.Columns.Count; Col++)
            {
               // ddlSearchCollom.Items.Add(new RadComboBoxItem(gvSearch.Columns[Col].Name));
            }
            hfSupplierId.Value = "0";
            hfSupplireCode.Value = "";

            CommenDataSet d = new CommenDataSet();
            DataRow[] dd = d.GetEmployeesForSearch.Select();
        }
    }

    protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    {
        gvSearch.DataBind();
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {

        try
        {

            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfSupplierId.Value = gridView.GetRowValues(index, "EmployeeID").ToString();

            _employeeId = Convert.ToInt64(hfSupplierId.Value);
            hfeemployeeId.Value = hfSupplierId.Value;

           // Response.Redirect("EditEmployee.aspx?EmployeeId=" + hfSupplierId.Value + "");

        }
        catch
        {
        }

    }


}