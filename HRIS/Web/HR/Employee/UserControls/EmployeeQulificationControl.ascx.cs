using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Employee_UserControls_EmployeeQulificationControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Qulification objQulification = new Qulification();
    Employee objEmployee = new Employee();
    DataTable dtEmployeeQulification = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        lblResult.Text = string.Empty;
     
        if (!IsPostBack)
        {
            InitializeControls();
            BindEmplyees();
            ViewState["dtEmployeeQulification"] = dtEmployeeQulification;

            btnSave.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            RadGrid1.Columns[0].Visible = false;

            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
            {
                btnSave.Visible = true;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                RadGrid1.Columns[0].Visible = false;
            }
            else
            {
                btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                if (btnDelete.Visible == true || btnUpdate.Visible == true)
                {
                    RadGrid1.Columns[0].Visible = false;
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
                GetEmployeeQulifications(Convert.ToInt64(ViewState["EmployeeId"]));
                hfEmployeeId.Value = ViewState["EmployeeId"].ToString();
            }
        }
    }


    private void GetEmployeeQulifications(long EmployeeId)
    {
        objQulification = new Qulification();
        dtEmployeeQulification = objQulification.GetEmployeeQulificationByEmployeeId(EmployeeId);
        if (dtEmployeeQulification.Rows.Count > 0)
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            BindData(dtEmployeeQulification.Rows[0]);
        }
        else
        {
            InitializeControls();
            ddlEmployee.SelectedValue = ViewState["EmployeeId"].ToString();
            //ddlEmployee.Items.FindItemByValue(ViewState["EmployeeId"].ToString()).Selected=true;
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfEmployeeQulificationId.Value = string.Empty;
        txtYear.Text = "";
        ddlInstitute.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;

        //BindCourse();
        lblResult.ForeColor = Color.Green;
        lblResult.Text = string.Empty;
    }


    private void BindEmplyees()
    {

        //ddlEmployee.DataSource = objEmployee.GetEmployee(0, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
        ddlEmployee.DataSource = objEmployee.GetEmployee(0, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "Name";
        ddlEmployee.DataBind();
    }


    private void BindCourse()
    {
        //ddlCourse.DataSource = objQulification.GetCourseDetails(0);
        //ddlCourse.DataValueField = "CourseID";
        //ddlCourse.DataTextField = "CourseName";
        //ddlCourse.DataBind();
    }

    private bool IsValidate()
    {
        //if (ddlEmployee.SelectedIndex <= 0)
        //{
        //    lblResult.Text = "Select Employee!";
        //    return false;
        //}


        if (ddlCourse.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Course!";
            return false;
        }

        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfEmployeeQulificationId.Value = dtTable.Rows[0]["EmployeeQulificationID"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
            ddlInstitute.SelectedValue = dtTable.Rows[0]["InstituteId"].ToString();
            ddlCourse.SelectedValue = dtTable.Rows[0]["CourseID"].ToString();
            txtYear.Text = dtTable.Rows[0]["Year"].ToString();
        }
    }

    private void BindData(DataRow drRow)
    {
        InitializeControls();

        hfEmployeeQulificationId.Value = drRow["EmployeeQulificationID"].ToString();
        ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
        //ddlCourse.SelectedValue = drRow["CourseID"].ToString();
        //txtYear.Text = drRow["Year"].ToString();
        
    }

    #endregion
    
    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {

            objQulification.AddEmployeeQulification2(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(txtYear.Text));
            if (!objQulification.IsError)
            {

                InitializeControls();
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objQulification.ErrorMsg;
            }


        }
        catch
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Unable to Save";
        }
        RadGrid1.DataBind();
      
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            objQulification.UpdateEmployeeQulifications(Convert.ToInt32(hfEmployeeQulificationId.Value), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(txtYear.Text));
            if (!objQulification.IsError)
            {
                InitializeControls();
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Unable to Save";
            }

        }
        catch(Exception ex)
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objQulification.DeleteEmployeeQulification(Convert.ToInt32(hfEmployeeQulificationId.Value));

        if (objQulification.IsError)
        {
            InitializeControls();
            lblResult.Text = objQulification.ErrorMsg;
        }
        else
        {
            InitializeControls();
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
     
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
    }

    #endregion

    #region GridView

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            dtEmployeeQulification = objQulification.GetEmployeeQulification(Convert.ToInt32(e.CommandArgument));
            BindData(dtEmployeeQulification);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion

    
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    e.Item.Selected = true;
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objQulification.GetEmployeeQulification(Convert.ToInt32(dataItem.GetDataKeyValue("EmployeeQulificationID")));
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
    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {

    }
    protected void ddlCourse_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Course >>", "0");
        if (!ddlCourse.Items.Contains(newItem))
        {
            ddlCourse.Items.Insert(0, newItem);
        }
    }

    protected void ddlInstitute_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Institute >>", "0");
        if (!ddlCourse.Items.Contains(newItem))
        {
            ddlCourse.Items.Insert(0, newItem);
        }
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        hfEmployeeQulificationId.Value = RadGrid1.GetRowValues(index, "EmployeeQulificationID").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeID").ToString();
       // ddlInstitute.SelectedValue = RadGrid1.GetRowValues(index, "InstituteCode").ToString();
        ddlCourse.SelectedValue = RadGrid1.GetRowValues(index, "CourseID").ToString();
        txtYear.Text = RadGrid1.GetRowValues(index, "Year").ToString();
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
    }
}