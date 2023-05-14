using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using SiteUtils;
using Telerik.Web.UI;

public partial class Employee_EmployeeSearch : System.Web.UI.UserControl
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
                ddlSearchCollom.Items.Add(new RadComboBoxItem(gvSearch.Columns[Col].HeaderText, gvSearch.Columns[Col].SortExpression));
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

    protected void gvSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            hfSupplierId.Value = e.CommandArgument.ToString();
            _employeeId = Convert.ToInt64(hfSupplierId.Value);
            hfeemployeeId.Value= hfSupplierId.Value;
        }
    }

    protected void gvSearch_DataBound(object sender, EventArgs e)
    {
       
    }

    protected void gvSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txteemployeeCode.Text = gvSearch.SelectedRow.Cells[2].Text;

            if (hfeemployeeId.Value != null)
            {
                try
                {
                    rdLwEmlpoyees.DataBind();
                }
                catch{}
            }

        }
        catch{}
    }
    protected void gvSearch_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}