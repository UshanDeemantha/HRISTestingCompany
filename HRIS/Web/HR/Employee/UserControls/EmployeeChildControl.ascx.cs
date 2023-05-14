using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Employee_UserControls_EmployeeChildControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Child objChild = new Child();
    Employee objEmployee = new Employee();
    DataTable dtChild = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.ForeColor = Color.Red;
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            BindEmployee();
            ViewState["dtChild"] = dtChild;

            btnSave.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            RadGrid1.Columns[0].Visible = false;

            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
            {
                btnSave.Visible = true;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                RadGrid1.Columns[0].Visible = true;
            }
            else
            {
                btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                if (btnDelete.Visible == true || btnUpdate.Visible == true)
                {
                    RadGrid1.Columns[0].Visible = true;
                }
            }
        }
    }

    public long EmployeeId
    {
        set
        {
            ViewState["EmployeeId"] = value;

            if (ViewState["EmployeeId"] != null)
            {
                GetEmployeeChidrens(Convert.ToInt64(ViewState["EmployeeId"]));

                hfEmployeeId.Value = ViewState["EmployeeId"].ToString();
            }
        }
    }

    #region Methods


    private void GetEmployeeChidrens(long EmployeeId)
    {
        objChild = new Child();
        dtChild = objChild.GetEmployeeChildByEmployeeId(EmployeeId);

        if (dtChild.Rows.Count > 0)
        {
           BindData(dtChild.Rows[0]);
        }
        else
        {
            InitializeControls();
            ddlEmployee.SelectedValue = EmployeeId.ToString();
        }
    }    

    private void InitializeControls()
    {
        hfChildId.Value = string.Empty;
        txtFirstName.Text = string.Empty;
        txtFullName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtDateOfBirth.SelectedDate = null;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
  
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfChildId.Value = dtTable.Rows[0]["ChildID"].ToString();
            txtFirstName.Text = dtTable.Rows[0]["FirstName"].ToString();
            txtFullName.Text = dtTable.Rows[0]["FullName"].ToString();
            txtLastName.Text = dtTable.Rows[0]["LastName"].ToString();
            txtDateOfBirth.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["DateOfBirth"]);
            rbGender.SelectedValue = dtTable.Rows[0]["Gender"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
        }
    }

    private void BindData(DataRow drRow)
    {
        InitializeControls();
        hfChildId.Value = drRow["ChildID"].ToString();

        ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
    }

    private void BindEmployee()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "Name";
        ddlEmployee.DataBind();

    }

    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objChild.AddChild2(Convert.ToInt64(ddlEmployee.SelectedValue), txtFirstName.Text, txtFullName.Text, txtLastName.Text, Convert.ToDateTime(txtDateOfBirth.SelectedDate.Value), rbGender.SelectedValue);
         
            if (!objChild.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Unable to Save";
            }

        }
        catch
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objChild.UpdateChild(Convert.ToInt32(hfChildId.Value), Convert.ToInt64(ddlEmployee.SelectedValue), txtFirstName.Text, txtFullName.Text, txtLastName.Text, Convert.ToDateTime(txtDateOfBirth.SelectedDate.Value), rbGender.SelectedValue);
           
            if (!objChild.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Unable to Save";
            }

        }
        catch
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objChild.DeleteChild(Convert.ToInt32(hfChildId.Value));

        if (objChild.IsError)
        {
            lblResult.Text = objChild.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }


    #endregion

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable
            dtChild = objChild.GetChild(Convert.ToInt32(e.CommandArgument));
            BindData(dtChild);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion
    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    e.Item.Selected = true;
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objChild.GetChild(Convert.ToInt32(dataItem.GetDataKeyValue("ChildID")));
        //        BindData(dtEvent);
        //        btnSave.Enabled = false;
        //        btnUpdate.Enabled = true;
        //        btnDelete.Enabled = true;
        //    }
        //    catch
        //    {
        //        if (RadGrid1.SelectedItems.Count <= 0)
        //        {
        //            lblResult.ForeColor = Color.Red;
        //            lblResult.Text = "Select item for modify!";
        //        }
        //    }
        //}
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        hfChildId.Value = RadGrid1.GetRowValues(index, "ChildID").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeID").ToString();
        // ddlMembership.Value = RadGrid1.GetRowValues(index, "SportID").ToString();
        txtFirstName.Text = RadGrid1.GetRowValues(index, "FirstName").ToString();
        txtLastName.Text = RadGrid1.GetRowValues(index, "LastName").ToString();
        txtFullName.Text = RadGrid1.GetRowValues(index, "Expr1").ToString();
        rbGender.Text = RadGrid1.GetRowValues(index, "Gender").ToString();
        txtDateOfBirth.SelectedDate = Convert.ToDateTime(RadGrid1.GetRowValues(index, "DateOfBirth").ToString());
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
    }
}