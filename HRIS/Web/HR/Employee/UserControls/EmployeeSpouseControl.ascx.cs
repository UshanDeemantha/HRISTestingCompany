using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web.Rendering;

public partial class Employee_EmployeeSpouseControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Spouse objSpouce = new Spouse();
    Employee objEmployee = new Employee();
    DataTable dtSpouce = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;

        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            BindEmployee();
            ViewState["dtSpouce"] = dtSpouce;
            ViewState["SpouseID"] = "0";
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
                GetEmployeeSpouse(Convert.ToInt64(ViewState["EmployeeId"]));
                hfEmployeeId.Value = ViewState["EmployeeId"].ToString();
            }
        }
    }

    private void GetEmployeeSpouse(long EmployeeId)
    {
        objSpouce = new Spouse();
        dtSpouce = objSpouce.GetSpouseByEmployeeId(EmployeeId);
        if (dtSpouce.Rows.Count > 0)
        {
            BindData(dtSpouce.Rows[0]);
        }
        else
        {
            InitializeControls();
            ddlEmployee.SelectedValue = EmployeeId.ToString();
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        ViewState["SpouseID"] = "0";
        //hfSpouceId.Value = string.Empty;
        txtFirstName.Text = string.Empty;
        txtFullName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtDateOfBirth.SelectedDate = null;
        txtDesignation.Text = string.Empty;
        txtCompany.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtSpouceEmail.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
       
       
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfSpouceId.Value = dtTable.Rows[0]["SpouseID"].ToString();
            ViewState["SpouseID"] = hfSpouceId.Value;
            txtFirstName.Text = dtTable.Rows[0]["FirstName"].ToString();
            txtFullName.Text = dtTable.Rows[0]["FullName"].ToString();
            txtLastName.Text = dtTable.Rows[0]["LastName"].ToString();
            txtCompany.Text = dtTable.Rows[0]["Company"].ToString();
            txtContactNo.Text = dtTable.Rows[0]["ContactNo"].ToString();
            txtSpouceEmail.Text = dtTable.Rows[0]["SpouseEmail"].ToString();
            txtDateOfBirth.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["DateOfBirth"]);
            rbGender.SelectedValue = dtTable.Rows[0]["Gender"].ToString();
            txtDesignation.Text = dtTable.Rows[0]["Designation"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
        }
    }

    private void BindData(DataRow drRow)
    {
        InitializeControls();

        hfSpouceId.Value = drRow["SpouseID"].ToString();
        ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
    }

    private void BindEmployee()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0,Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]),"HR");
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
            objSpouce.AddSpouse2(Convert.ToInt64(ddlEmployee.SelectedValue), txtFirstName.Text, txtFullName.Text, txtLastName.Text, txtCompany.Text, txtContactNo.Text, txtSpouceEmail.Text, rbGender.SelectedValue, Convert.ToDateTime(txtDateOfBirth.SelectedDate.Value), txtDesignation.Text, true);
            
            if (!objSpouce.IsError)
            {
                InitializeControls();
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = "Unable to Save";
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
       
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objSpouce.UpdateSpouse(Convert.ToInt32(ViewState["SpouseID"]), Convert.ToInt64(ddlEmployee.SelectedValue), txtFirstName.Text, txtFullName.Text, txtLastName.Text, txtCompany.Text, txtContactNo.Text, txtSpouceEmail.Text, rbGender.SelectedValue, Convert.ToDateTime(txtDateOfBirth.SelectedDate.Value), txtDesignation.Text, true);
            
            if (!objSpouce.IsError)
            {
                InitializeControls();
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = "Unable to Save";
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objSpouce.DeleteSpouse(Convert.ToInt32(ViewState["SpouseID"]));

        if (objSpouce.IsError)
        {
            lblResult.Text = objSpouce.ErrorMsg;
        }
        else
        {
            InitializeControls();
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
       
    }

    #endregion

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable
            dtSpouce = objSpouce.GetSpouse(Convert.ToInt32(e.CommandArgument));
            BindData(dtSpouce);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion
    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }
    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    e.Item.Selected = true;
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objSpouce.GetSpouse(Convert.ToInt32(dataItem.GetDataKeyValue("SpouseID")));
        //        BindData(dtEvent);
        //        btnSave.Enabled = false;
        //        btnUpdate.Enabled = true;
        //        btnDelete.Enabled = true;
        //        hfSpouceId.Value = dataItem.GetDataKeyValue("SpouseID").ToString();
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

        hfSpouceId.Value = RadGrid1.GetRowValues(index, "SpouseID").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeID").ToString();
        // ddlMembership.Value = RadGrid1.GetRowValues(index, "SportID").ToString();
        txtFirstName.Text = RadGrid1.GetRowValues(index, "FirstName").ToString();
        txtLastName.Text = RadGrid1.GetRowValues(index, "LastName").ToString();
        txtFullName.Text = RadGrid1.GetRowValues(index, "FullName").ToString();
        rbGender.Text = RadGrid1.GetRowValues(index, "Gender").ToString();
        txtCompany.Text = RadGrid1.GetRowValues(index, "Company").ToString();
        txtDesignation.Text = RadGrid1.GetRowValues(index, "Designation").ToString();
        txtContactNo.Text = RadGrid1.GetRowValues(index, "ContactNo").ToString();
        txtSpouceEmail.Text = RadGrid1.GetRowValues(index, "SpouseEmail").ToString();
        string txtDateOfBirth = RadGrid1.GetRowValues(index, "DateOfBirth").ToString();
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
    }
}