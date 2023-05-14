using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web.Rendering;

public partial class Employee_UserControls_EmployeeCertificationControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Certification objCertification = new Certification();
    Employee objEmployee = new Employee();
    DataTable dtEmployeeCertification = new DataTable();

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
            BindEmplyees();
            ViewState["dtEmployeeCertification"] = dtEmployeeCertification;

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
                //gvDetails.DataBind();
            }
        }
    }

    private void GetEmployeeQulifications(long EmployeeId)
    {
        objCertification = new Certification();
        dtEmployeeCertification= objCertification.GetEmployeeCertificationByEmployeeID(EmployeeId);
        if (dtEmployeeCertification.Rows.Count > 0)
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            BindData(dtEmployeeCertification.Rows[0]);
        }
        else
        {
            InitializeControls();
            ddlEmployee.SelectedValue = ViewState["EmployeeId"].ToString(); 
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfemployeecertificationid.Value = string.Empty;
      //  ddlEmployee.SelectedIndex = -1;
        ddlInstitute.SelectedIndex = -1;
        ddlCertification.SelectedIndex = -1;
        txtGrade.Text = string.Empty;
        txtDate.SelectedDate = null;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
     
        //BindCertification();

    }


    private void BindEmplyees()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0,Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]),"Common");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "Name";
        ddlEmployee.DataBind();
    }
    //private void BindCertification()
    //{
    //    ddlCertification.DataSource = objCertification.GetCertification(0);
    //    ddlCertification.DataValueField = "CertificationID";
    //    ddlCertification.DataTextField = "CertificationName";
    //    ddlCertification.DataBind();
    //}


    //private bool IsValidate()
    //{
       
    //    if (ddlCertification.SelectedIndex <= 0)
    //    {
    //        lblResult.Text = "Select Certification!";
    //        return false;
    //    }

    //    return true;
    //}

    private void BindData(DataRow drRow)
    {
        InitializeControls();
        
           //hfEmployeeCertificationId.Value = drRow["EmployeeCertificationID"].ToString();
            ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
           // ddlCertification.SelectedValue = drRow["CertificationID"].ToString();
           // txtDate.Text = Convert.ToDateTime(drRow["Date"]).ToShortDateString();
           // txtGrade.Text = drRow["Grade"].ToString();

    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfemployeecertificationid.Value = dtTable.Rows[0]["EmployeeCertificationID"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
            ddlInstitute.SelectedValue = dtTable.Rows[0]["InstituteID"].ToString();
            ddlCertification.SelectedValue = dtTable.Rows[0]["CertificationID"].ToString();
            txtDate.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["Date"]);
            txtGrade.Text = dtTable.Rows[0]["Grade"].ToString();
           
        }
    }

    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (!IsValidate())
        //{
        //    return;
        //}
        try
        {
            objCertification.AddEmployeeCertification2(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlCertification.SelectedValue), Convert.ToDateTime(txtDate.SelectedDate.Value), txtGrade.Text);
            if (!objCertification.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objCertification.ErrorMsg;
            }


        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }
        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //if (!IsValidate())
        //{
        //    return;
        //}
        try
        {
            objCertification.UpdateEmployeeCertification(Convert.ToInt32(hfemployeecertificationid.Value), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlCertification.SelectedValue), Convert.ToDateTime(txtDate.SelectedDate.Value), txtGrade.Text);
            if (!objCertification.IsError)
            {
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
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objCertification.DeleteEmployeeCertification(Convert.ToInt32(hfemployeecertificationid.Value));
        if (objCertification.IsError)
        {
            lblResult.Text = objCertification.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted.";
        }
        RadGrid1.DataBind();
        InitializeControls();
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
            dtEmployeeCertification = objCertification.GetEmployeeCertification(Convert.ToInt32(e.CommandArgument));
            BindData(dtEmployeeCertification);
            btnSave.Enabled = false;
           btnUpdate.Enabled = true;
           btnDelete.Enabled = true;
        }
    }
    #endregion

    protected void ddlCertification_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select Certification >>", "0");
        //if (!ddlCertification.Items.Contains(newItem))
        //{
        //    ddlCertification.Items.Insert(0, newItem);
        //}
    }

    protected void ddlInstitute_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select Institute >>", "0");
        //if (!ddlInstitute.Items.Contains(newItem))
        //{
        //    ddlInstitute.Items.Insert(0, newItem);
        //}
    }
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    e.Item.Selected = true;
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objCertification.GetEmployeeCertification(Convert.ToInt32(dataItem.GetDataKeyValue("EmployeeCertificationID")));
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
    protected void ddlEmployee_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
    protected void ddlEmployee_DataBound(object sender, EventArgs e)
    {

    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        hfemployeecertificationid.Value = RadGrid1.GetRowValues(index, "EmployeeCertificationID").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeID").ToString();
        ddlInstitute.SelectedValue = RadGrid1.GetRowValues(index, "InstituteCode").ToString();
        ddlCertification.SelectedValue = RadGrid1.GetRowValues(index, "CertificationName").ToString();
        txtDate.SelectedDate = Convert.ToDateTime(gridView.GetRowValues(index, "Date"));
        // txtDate.tex = RadGrid1.GetRowValues(index, "Date").ToString();
        txtGrade.Text = RadGrid1.GetRowValues(index, "Grade").ToString();
        //.Value = RadGrid1.GetRowValues(index, "CourseID").ToString();
        //txtYear.Text = RadGrid1.GetRowValues(index, "Year").ToString();
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
    }
}