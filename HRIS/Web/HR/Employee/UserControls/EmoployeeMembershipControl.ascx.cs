using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Employee_UserControls_EmoployeeMembershipControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion
	
    Membership objMembership = new Membership();
    Employee objEmployee = new Employee();
    DataTable dtEmployeeMembership = new DataTable();

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
            ViewState["dtEmployeeMembership"] = dtEmployeeMembership;
            cbIsToDate.Checked = false;
            txtToDate.Enabled = false;

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
                GetEmployeeMemberships(Convert.ToInt64(ViewState["EmployeeId"]));
                hfEmployeeId.Value = ViewState["EmployeeId"].ToString();
            }
        }
    }

    private void GetEmployeeMemberships(long EmployeeId)
    {

        objMembership = new Membership();
        dtEmployeeMembership = objMembership.GetEmployeeMembershipByEmployeeId(EmployeeId);
        if (dtEmployeeMembership.Rows.Count > 0)
        {
            BindData(dtEmployeeMembership.Rows[0]);
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
        hfEmployeeMembershipId.Value = string.Empty;
        //ddlEmployee.SelectedIndex = -1;
        ddlMembership.SelectedIndex = -1;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
   
        BindMembership();
        txtGrade.Text = string.Empty;
        txtRemark.Text = string.Empty;
        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;
        txtFromDate.SelectedDate = null;
        txtToDate.SelectedDate = null;
    }


    private void BindEmplyees()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "Name";
        ddlEmployee.DataBind();
    }
    private void BindMembership()
    {
        ddlMembership.DataSource = objMembership.GetMembership(0);
        ddlMembership.DataValueField = "MembershipID";
        ddlMembership.DataTextField = "MembershipName";
        ddlMembership.DataBind();
    }



    private bool IsValidate()
    {
        //if (ddlEmployee.SelectedIndex <= 0)
        //{
        //    lblResult.Text = "Select Employee!";
        //    return false;
        //}
        if (ddlMembership.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Membership!";
            return false;
        }
       

        if (cbIsToDate.Checked)
        {
            if (Convert.ToDateTime(txtFromDate.SelectedDate.Value) >= Convert.ToDateTime(txtToDate.SelectedDate.Value))
            {
                lblResult.Text = "From Date Can not be higher than or equelant to To Date!";
                return false;
            }
                   //if (Convert.ToDateTime(txtToDate.Text) > DateTime.Today)
                  //{
                  //    lblResult.Text = "To Date  Can not higher than Today!";
                   //    return false;
                  //}
        }
        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfEmployeeMembershipId.Value = dtTable.Rows[0]["EmployeeMembershipID"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
            ddlMembership.SelectedValue = dtTable.Rows[0]["MembershipID"].ToString();
            DateTime fromDate = Convert.ToDateTime(dtTable.Rows[0]["FromDate"].ToString());
            txtFromDate.SelectedDate = fromDate.Date;
            if (dtTable.Rows[0]["ToDate"].ToString() != null && dtTable.Rows[0]["ToDate"].ToString() != string.Empty)
            {
                cbIsToDate.Checked = true;
                DateTime toDate = Convert.ToDateTime(dtTable.Rows[0]["ToDate"].ToString());
                txtToDate.SelectedDate = toDate.Date;
            }
            else
            {
                cbIsToDate.Checked = false;
                txtToDate.Enabled = false;
                txtToDate.SelectedDate = null;
            }
            txtGrade.Text = dtTable.Rows[0]["Grade"].ToString();
            txtRemark.Text = dtTable.Rows[0]["Remark"].ToString();
        }
    }

    private void BindData(DataRow drRow)
    {
        InitializeControls();
           hfEmployeeMembershipId.Value = drRow["EmployeeMembershipID"].ToString();
            ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
            //ddlMembership.SelectedValue = drRow["MembershipID"].ToString();
            //DateTime fromDate = Convert.ToDateTime(drRow["FromDate"].ToString());
            //DateTime toDate = Convert.ToDateTime(drRow["ToDate"].ToString());
            //txtFromDate.Text = fromDate.Date.ToShortDateString();
            //txtToDate.Text = toDate.Date.ToShortDateString();
            //txtGrade.Text = drRow["Grade"].ToString();
            //txtRemark.Text = drRow["Remark"].ToString();
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
            DateTime fromDate = Convert.ToDateTime(txtFromDate.SelectedDate.Value);// + " " + txtFromHH.Text + ":" + txtFromMM.Text + ":" + txtFromSec.Text + " " + ddlFromDateAmPM.SelectedItem.Value);

            DateTime toDate  = DateTime.Today;
            if (cbIsToDate.Checked == true)
            {
                toDate = Convert.ToDateTime(txtToDate.SelectedDate.Value); // + " " + txtToHH.Text + ":" + txtToMM.Text + ":" + txtToSec.Text + " " + ddlToDateAmPM.SelectedItem.Value);
            }

            objMembership.AddEmployeeMemberships2(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlMembership.SelectedValue), fromDate, toDate , cbIsToDate.Checked, txtGrade.Text, txtRemark.Text);
            if (!objMembership.IsError)
            {
                InitializeControls();
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objMembership.ErrorMsg;
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
            DateTime fromDate = Convert.ToDateTime(txtFromDate.SelectedDate.Value); // + " " + txtFromHH.Text + ":" + txtFromMM.Text + ":" + txtFromSec.Text + " " + ddlFromDateAmPM.SelectedItem.Value);
            DateTime toDate;

            if (cbIsToDate.Checked)
            {
                toDate = Convert.ToDateTime(txtToDate.SelectedDate.Value); // + " " + txtToHH.Text + ":" + txtToMM.Text + ":" + txtToSec.Text + " " + ddlToDateAmPM.SelectedItem.Value);
            }
            else
            {
                toDate = DateTime.Today;
            }

            objMembership.UpdateEmployeeMemberships(Convert.ToInt32(hfEmployeeMembershipId.Value), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlMembership.SelectedValue), fromDate, toDate,cbIsToDate.Checked, txtGrade.Text, txtRemark.Text);
            
            if (!objMembership.IsError)
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
        catch
        {
           
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objMembership.DeleteEmployeeMemberships(Convert.ToInt32(hfEmployeeMembershipId.Value));
        if (objMembership.IsError)
        {
            InitializeControls();
            lblResult.Text = objMembership.ErrorMsg;
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

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable dtEmployeeMembership = objMembership.GetEmployeeMembership(Convert.ToInt32(e.CommandArgument));
            BindData(dtEmployeeMembership);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion

    protected void gvDetails_DataBinding(object sender, EventArgs e)
    {
        string ff = hfEmployeeId.Value;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsToDate.Checked)
        {
            txtToDate.Enabled = true;
        }
        else
        {
            txtToDate.Enabled = false;
            txtToDate.SelectedDate = null;
        }
    }

    protected void ddlMembership_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Membership >>", "0");
        if (!ddlMembership.Items.Contains(newItem))
        {
            ddlMembership.Items.Insert(0, newItem);
        }
    }
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    e.Item.Selected = true;
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objMembership.GetEmployeeMembership(Convert.ToInt32(dataItem.GetDataKeyValue("EmployeeMembershipID")));
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

        hfEmployeeMembershipId.Value = RadGrid1.GetRowValues(index, "EmployeeMembershipID").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeID").ToString();
        ddlMembership.SelectedValue = RadGrid1.GetRowValues(index, "MembershipID").ToString();
         txtFromDate.SelectedDate = Convert.ToDateTime(RadGrid1.GetRowValues(index, "FromDate").ToString());
         txtToDate.SelectedDate = Convert.ToDateTime(RadGrid1.GetRowValues(index, "ToDate").ToString());
        txtGrade.Text = RadGrid1.GetRowValues(index, "Grade").ToString();
        txtRemark.Text = RadGrid1.GetRowValues(index, "Remark").ToString();
        //txtYear.Text = RadGrid1.GetRowValues(index, "Year").ToString();
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
       
    }
}