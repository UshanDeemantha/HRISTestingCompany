using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Membership_EmployeeMembership : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeMemberships";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Membership objMembership = new Membership();
    Employee objEmployee = new Employee();
    DataTable dtEmployeeMembership = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;

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
                    Response.Redirect("~/HR/NoPermissions.aspx");
                    return;
                }
                else
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSave.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                    }
                }
            }
        }

        if (!IsPostBack)
        {
            InitializeControls();
            ViewState["dtEmployeeMembership"] = dtEmployeeMembership;
        }
    }

    #region Methods

    private void InitializeControls()
    {
        hfEmployeeMembershipId.Value = string.Empty;
        ddlEmployee.SelectedIndex = -1;
        ddlMembership.SelectedIndex = -1;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        gvDetails.SelectedIndex = -1;
        gvDetails.EditIndex = -1;
        BindEmplyees();
        BindMembership();
        txtGrade.Text = string.Empty;
        txtRemark.Text = string.Empty;
    }

    private void BindEmplyees()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "HR");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "FullName";
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
        if (ddlEmployee.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Employee!";
            return false;
        }
        if (ddlMembership.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Membership!";
            return false;
        }
        if (Convert.ToDateTime(txtFromDate.SelectedDate) >= Convert.ToDateTime(txtToDate.SelectedDate))
        {
            lblResult.Text = "From Date cannot be higher than current date!";
            return false;
        }

        if (Convert.ToDateTime(txtToDate.SelectedDate) > DateTime.Today)
        {
            lblResult.Text = "To Date Cannot be higher than current date!";
            return false;
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
            DateTime fromDate = Convert.ToDateTime( dtTable.Rows[0]["FromDate"].ToString());
            DateTime toDate = Convert.ToDateTime(dtTable.Rows[0]["ToDate"].ToString());
            txtFromDate.SelectedDate = fromDate.Date;
            txtToDate.SelectedDate = toDate.Date;
            txtGrade.Text = dtTable.Rows[0]["Grade"].ToString();
            txtRemark.Text = dtTable.Rows[0]["Remark"].ToString();
        }
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
            DateTime fromDate = Convert.ToDateTime(txtFromDate.SelectedDate); // + " " + txtFromHH.Text + ":" + txtFromMM.Text + ":" + txtFromSec.Text + " " + ddlFromDateAmPM.SelectedItem.Value);
            DateTime toDate = Convert.ToDateTime(txtToDate.SelectedDate); // + " " + txtToHH.Text + ":" + txtToMM.Text + ":" + txtToSec.Text + " " + ddlToDateAmPM.SelectedItem.Value);
            
            objMembership.AddEmployeeMemberships(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlMembership.SelectedValue), fromDate, toDate, txtGrade.Text, txtRemark.Text);

            if (!objMembership.IsError)
            {
                btnSave.Enabled = false;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objMembership.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }

        gvDetails.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }

        try
        {
            DateTime fromDate = Convert.ToDateTime(txtFromDate.SelectedDate); // + " " + txtFromHH.Text + ":" + txtFromMM.Text + ":" + txtFromSec.Text + " " + ddlFromDateAmPM.SelectedItem.Value);
            DateTime toDate = Convert.ToDateTime(txtToDate.SelectedDate); // + " " + txtToHH.Text + ":" + txtToMM.Text + ":" + txtToSec.Text + " " + ddlToDateAmPM.SelectedItem.Value);
            
            objMembership.UpdateEmployeeMemberships(Convert.ToInt32(hfEmployeeMembershipId.Value), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlMembership.SelectedValue), fromDate, toDate, txtGrade.Text, txtRemark.Text);
            
            if (!objMembership.IsError)
            {
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

        gvDetails.DataBind();
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objMembership.DeleteEmployeeMemberships(Convert.ToInt32(hfEmployeeMembershipId.Value));

        if (objMembership.IsError)
        {
            lblResult.Text = objMembership.ErrorMsg;
        }
        else
        {
            lblResult.Text = "Successfully Deleted.";
        }

        gvDetails.DataBind();
        InitializeControls();
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
            btnUpdate.Enabled  = true;
            btnDelete.Enabled = true;
        }
    } 

    #endregion   

    protected void ddlEmployee_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Employee >>", "0");
        if (!ddlEmployee.Items.Contains(newItem))
        {
            ddlEmployee.Items.Insert(0, newItem);
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
    protected void RadGrid1_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

    }
}
