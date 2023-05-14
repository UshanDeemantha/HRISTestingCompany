using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Common_Settings_CommonWorkFlow : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "CommonWorkFlow";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Employee objEmp = new Employee();
    DataTable dtWorkflow = new DataTable();
    private System.Nullable<int> firstApprovePerson;
    private System.Nullable<int> secondApprovePerson;
    private System.Nullable<int> thirdApprovePerson;

    string userName;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;

        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            userName = Session["UserName"].ToString();
            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
            {
                Response.Redirect("~/TimeAttendance/NoPermissions.aspx");
                return;
            }
            else
            {
                radbtnSave.Visible = false;
                radbtnUpdate.Visible = false;

                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                {
                    radbtnSave.Visible = true;
                    radbtnUpdate.Visible = true;
                }
                else
                {
                    radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                    radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                }
            }
        }

        hfCompanyId.Value = Session["CompanyId"].ToString();

        if (!Page.IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["EmployeeId"]))
            {

                var empId = Request.QueryString["EmployeeId"];
                var fullName = Request.QueryString["FullName"];
                var employeeCode = Request.QueryString["EmployeeCode"];
                var orgStructureID = Request.QueryString["OrgStructureID"];

                ////radcbEmployee.Items.Clear();
                //RadComboBoxItem newItem = new RadComboBoxItem(fullName, empId);
                //radcbEmployee.Items.Insert(0, newItem);
                //radcbEmployee.SelectedValue = empId;

                hfEmployeeId.Value = empId;

                EmployeeEventChange();

            }
            else
            {
                InitilizedControl();
            }
        }
    }
    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {

    }

    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "505px");
        try
        {
            if (Validation())
            {
                //dtWorkflow = objLeave.CheckLeaveWorkflow(Convert.ToInt32(radcbEmployee.SelectedValue));

                //if (dtLeaveWorkflow.Rows.Count > 0)
                //{
                //    lblResult.Text = "Leave Already Assigned";
                //    radbtnSave.Enabled = false;
                //    return;
                //}

                if (radcbFirstApprovePerson.Value != null)
                {
                    firstApprovePerson = (System.Nullable<int>)(Int32.Parse(radcbFirstApprovePerson.Value.ToString()));

                    if (radcbSecondApprovePerson.Value == null)
                    {
                        secondApprovePerson = null;
                    }
                    else
                    {
                        secondApprovePerson = (System.Nullable<int>)(Int32.Parse(radcbSecondApprovePerson.Value.ToString()));
                    }
                    if (radcbThirdApprovePerson.Value == null)
                    {
                        thirdApprovePerson = null;
                    }
                    else
                    {
                        thirdApprovePerson = (System.Nullable<int>)(Int32.Parse(radcbThirdApprovePerson.Value.ToString()));
                    }

                  
                }
                else
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = "First approve person can't be empty.";
                }
            }
            formContainer.Attributes.CssStyle.Add("height", "505px");

        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "505px");
            lblResult.Text = "Error Occured";
            //InitializedControl();
            //EnableButton(true);
            //btnSave.Enabled = false;
        }

    }

    private bool Validation()
    {
        if(radcbApprovalType.Value == null)
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Please Select an Approval Type.";
            return false;
        }
        if (txtGroupCode.Text == "")
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Please Enter Group Code.";
            return false;
        }
        if (txtGroupName.Text == "")
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Please Enter Group Name.";
            return false;
        }
        return true;
    }

    private void ResetControl()
    {
        
        radcbFirstApprovePerson.SelectedIndex = -1;
        radcbSecondApprovePerson.SelectedIndex = -1;
        radcbThirdApprovePerson.SelectedIndex = -1;
        radcbFirstApprovePerson.Enabled = false;
        radcbSecondApprovePerson.Enabled = false;
        radcbThirdApprovePerson.Enabled = false;
    }

    private void ClearControl()
    {
        radcbApprovalType.SelectedIndex = -1;
        txtGroupCode.Text = string.Empty;
        txtGroupName.Text = string.Empty;
        txtGroupCode.ReadOnly = false;
        txtGroupName.ReadOnly = false;
        txtGroupCode.Enabled = true;
        txtGroupName.Enabled = true;
        radcbFirstApprovePerson.SelectedIndex = -1;
        radcbSecondApprovePerson.SelectedIndex = -1;
        radcbThirdApprovePerson.SelectedIndex = -1;
        radcbApprovalType.Enabled = true;
        radbtnSave.Enabled = true;
    }


    private void InitilizedControl()
    {
        radcbFirstApprovePerson.Enabled = true;
        radcbSecondApprovePerson.Enabled = false;
        radcbThirdApprovePerson.Enabled = false;
        formContainer.Attributes.CssStyle.Add("height", "54px");
        ClearControl();
    }

    private void EnableButtons(bool Flag)
    {
        radbtnSave.Enabled = Flag;
        radbtnUpdate.Enabled = Flag;
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
       
        InitilizedControl();
        lblResult.Text = string.Empty;
        EnableButtons(true);
        radbtnUpdate.Enabled = false;
        //formContainer.Attributes.CssStyle.Add("height", "505px");

    }

    protected void radcbMonth_DataBound(object sender, EventArgs e)
    {

    }
    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "505px");
        try
        {
            if (radcbFirstApprovePerson.Value != null)
            {
                firstApprovePerson = (System.Nullable<int>)(Int32.Parse(radcbFirstApprovePerson.Value.ToString()));

                if (radcbSecondApprovePerson.Value == null)
                {
                    secondApprovePerson = null;
                }
                else
                {
                    secondApprovePerson = (System.Nullable<int>)(Int32.Parse(radcbSecondApprovePerson.Value.ToString()));
                }
                formContainer.Attributes.CssStyle.Add("height", "505px");
                if (radcbThirdApprovePerson.Value == null)
                {
                    thirdApprovePerson = null;
                }
                else
                {
                    thirdApprovePerson = (System.Nullable<int>)(Int32.Parse(radcbThirdApprovePerson.Value.ToString()));
                }
                formContainer.Attributes.CssStyle.Add("height", "505px");

                // objLeave.UpdateLeaveWorkflow(Convert.ToInt32(radcbEmployee.SelectedValue), firstApprovePerson, secondApprovePerson, thirdApprovePerson);
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "First approve person can't be empty.";
                formContainer.Attributes.CssStyle.Add("height", "505px");
            }


            formContainer.Attributes.CssStyle.Add("height", "505px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "505px");
            lblResult.Text = "Error Occured";
        }
    }
    protected void radgdEntitlement_SelectedIndexChanged(object sender, EventArgs e)
    {

        //   GridDataItem item = radgdEntitlement.SelectedItems[0] as GridDataItem;

        //   radcbEmployee.SelectedValue = item["EmployeeId"].Text.ToString();
        //   radcbLeaveType.SelectedValue = item["LeaveTypeId"].Text.ToString();

        //   radntxtEntitlement.Text = item["Entitlement"].Text.ToString();
        //   radntxtUtilized.Text = item["Utilized"].Text.ToString();
        //   radntxtYear.Text = item["Year"].Text.ToString();
        //   radcbMonth.SelectedValue = item["Month"].Text.ToString();

        //   radntxtUtilized.Enabled = true;
        //   radbtnUpdate.Enabled = true;
        //   radbtnSave.Enabled = false;
        //   btnOrganizationSelect.Enabled = false;
        //   radcbEmployee.Enabled = false;

        //   DataTable dtEmp = objLeave.GetEmployeeOrg(Convert.ToInt64(item["EmployeeId"].Text));

        //   //txtLeaveCode.Enabled = false;
        //   hfEntitlementId.Value = item["EntitlementId"].Text;
        //   hfOrganizationStructure.Value = dtEmp.Rows[0]["OrgStructureID"].ToString();
        //  //lblOrganization.Text = dtEmp.Rows[0]["OrgStructureID"].ToString();
    }
    protected void btnOrganizationSelect_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "505px");
    }
    protected void radcbEmployee_DataBound(object sender, EventArgs e)
    {
        //ListItem newItem = new ListItem("<< Select Employee >>", "0");
        //radcbEmployee.Items.Insert(0, newItem);
    }
    protected void radcbLeaveType_DataBound(object sender, EventArgs e)
    {
        //ListItem newItem = new ListItem("<< Select Leave Type >>", "0");
        //radcbEmployee.Items.Insert(0, newItem);
    }
    //protected void radcbEmployee_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    //{
    //    hfEmployeeId.Value = radcbEmployee.SelectedValue.Trim();
    //    EmployeeEventChange();

    //}


    private void EmployeeEventChange()
    {

        if (Convert.ToInt32(hfEmployeeId.Value) > 0)
        {
            radcbFirstApprovePerson.Enabled = true;
        }

        

        if (dtWorkflow.Rows.Count > 0)
        {
            radbtnSave.Enabled = false;

            DataRow rowItem = dtWorkflow.Rows[0];
            if (rowItem.ItemArray[1] != DBNull.Value)
            {
                radcbFirstApprovePerson.Text = objEmp.GetEmployee(Convert.ToInt64(rowItem[1].ToString()), Session["UserName"].ToString(), Convert.ToInt32(hfCompanyId.Value), "Common").Rows[0][4].ToString();
                hfFirstApprovePerson.Value = rowItem[1].ToString();
                //radcbFirstApprovePerson.SelectedValue = rowItem[1].ToString();
            }
            else
            {
                radcbFirstApprovePerson.DataBind();
                radcbFirstApprovePerson.SelectedIndex = -1;
                radcbFirstApprovePerson.Text = string.Empty;
            }

            if (rowItem.ItemArray[2] != DBNull.Value)
            {
                radcbSecondApprovePerson.Text = objEmp.GetEmployee(Convert.ToInt64(rowItem[2].ToString()), Session["UserName"].ToString(), Convert.ToInt32(hfCompanyId.Value), "Common").Rows[0][4].ToString();
                radcbSecondApprovePerson.Enabled = true;
                hfSecondApprovePerson.Value = rowItem[2].ToString();
                //radcbSecondApprovePerson.SelectedValue = rowItem[2].ToString();
            }
            else
            {
                radcbSecondApprovePerson.DataBind();
                radcbSecondApprovePerson.SelectedIndex = -1;
                radcbSecondApprovePerson.Text = string.Empty;
                if (rowItem.ItemArray[1] != DBNull.Value || (!(String.IsNullOrEmpty(radcbFirstApprovePerson.Text))))
                {
                    radcbSecondApprovePerson.Enabled = true;
                }
                else
                {
                    radcbSecondApprovePerson.Enabled = false;
                }
            }

            if (rowItem.ItemArray[3] != DBNull.Value)
            {
                radcbThirdApprovePerson.Text = objEmp.GetEmployee(Convert.ToInt64(rowItem[3].ToString()), Session["UserName"].ToString(), Convert.ToInt32(hfCompanyId.Value), "Common").Rows[0][4].ToString();
                radcbThirdApprovePerson.Enabled = true;
                hfThirdApprovePerson.Value = rowItem[3].ToString();
                //radcbThirdApprovePerson.SelectedValue = rowItem[3].ToString();
            }
            else
            {
                radcbThirdApprovePerson.DataBind();
                radcbThirdApprovePerson.SelectedIndex = -1;
                radcbThirdApprovePerson.Text = string.Empty;
                if (rowItem.ItemArray[2] != DBNull.Value || !(String.IsNullOrEmpty(radcbSecondApprovePerson.Text)))
                {
                    radcbThirdApprovePerson.Enabled = true;
                }
                else
                {
                    radcbThirdApprovePerson.Enabled = false;
                }
            }

        }

    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "505px");
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            var ApprovalType = gridView.GetRowValues(index, "ApprovalTypeID").ToString();
            var ApprovalGroupName = gridView.GetRowValues(index, "ApprovalGroupName").ToString();
            var ApprovalGroupId = gridView.GetRowValues(index, "ApprovalGroupCode").ToString();
            var ApprovePerson1Code = gridView.GetRowValues(index, "ApprovePerson1Code").ToString();
            var ApprovePerson2Name = gridView.GetRowValues(index, "ApprovePerson2Code").ToString();
            var ApprovePerson3Name = gridView.GetRowValues(index, "ApprovePerson3Code").ToString(); 
            var cbActive = gridView.GetRowValues(index, "IsActive").ToString();
            formContainer.Attributes.CssStyle.Add("height", "505px");
            hfApproveId.Value = gridView.GetRowValues(index, "ApprovalWorkFlowId").ToString();

            Session["id"] = hfApproveId.Value;


            radcbApprovalType.Value = ApprovalType;

            txtGroupCode.Text = ApprovalGroupId;
            txtGroupName.Text = ApprovalGroupName;
            
            hfSecondApprovePerson.Value = ApprovePerson2Name;
            hfFirstApprovePerson.DataBind();
            hfFirstApprovePerson.Value = ApprovePerson1Code;
            hfThirdApprovePerson.Value = ApprovePerson3Name;

            radcbFirstApprovePerson.DataBind();
            radcbFirstApprovePerson.Text = ApprovePerson1Code;
            radcbSecondApprovePerson.DataBind();
            radcbSecondApprovePerson.Value = ApprovePerson2Name;
            radcbThirdApprovePerson.DataBind();
            radcbThirdApprovePerson.Value = ApprovePerson3Name;
            radbtnUpdate.Enabled = true;
            radbtnSave.Enabled = false;
            radbtnCancel.Enabled = true;
            radcbApprovalType.Enabled = false;
            txtGroupCode.Enabled = false;
            txtGroupName.Enabled = false;
            radbtnDelete.Enabled = true;
            radcbSecondApprovePerson.Enabled = true;
            radcbThirdApprovePerson.Enabled = true;
            // Response.Redirect("LeaveWorkflow.aspx?EmployeeId=" + EmpTxn + "&EmployeeCode=" + employeeCode + "&FullName=" + fullName + "&OrgStructureID=" + orgStructureID, true);
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "505px");
        }
    }

    protected void radcbFirstApprovePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtGroupName.Enabled == false)
        {
            radbtnUpdate.Enabled = true;
            radbtnSave.Enabled = false;
            radbtnCancel.Enabled = true;
            radbtnDelete.Enabled = true;
        }
        else
        {
            radbtnUpdate.Enabled = false;
            radbtnSave.Enabled = true;
            radbtnCancel.Enabled = true;
            radbtnDelete.Enabled = true;
        }
        formContainer.Attributes.CssStyle.Add("height", "505px");
        radcbFirstApprovePerson.Enabled = true;
        hfFirstApprovePerson.Value = radcbFirstApprovePerson.Value.ToString();
        
        if (radbtnSave.Enabled == false)
        {
            radbtnUpdate.Enabled = true;
        }
        formContainer.Attributes.CssStyle.Add("height", "505px");
    }
    protected void radcbSecondApprovePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtGroupName.Enabled == false)
        {
            radbtnUpdate.Enabled = true;
            radbtnSave.Enabled = false;
            radbtnCancel.Enabled = true;
            radbtnDelete.Enabled = true;
        }
        else
        {
            radbtnUpdate.Enabled = false;
            radbtnSave.Enabled = true;
            radbtnCancel.Enabled = true;
            radbtnDelete.Enabled = true;
        }
        formContainer.Attributes.CssStyle.Add("height", "505px");
        radcbFirstApprovePerson.Enabled = true;
        hfSecondApprovePerson.Value = radcbFirstApprovePerson.Value.ToString();
        
        if (radbtnSave.Enabled == false)
        {
            radbtnUpdate.Enabled = true;
        }
        formContainer.Attributes.CssStyle.Add("height", "505px");
    }
    protected void radcbThirdApprovePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtGroupName.Enabled == false)
        {
            radbtnUpdate.Enabled = true;
            radbtnSave.Enabled = false;
            radbtnCancel.Enabled = true;
            radbtnDelete.Enabled = true;
        }
        else
        {
            radbtnUpdate.Enabled = false;
            radbtnSave.Enabled = true;
            radbtnCancel.Enabled = true;
            radbtnDelete.Enabled = true;
        }
        formContainer.Attributes.CssStyle.Add("height", "505px");
        radcbFirstApprovePerson.Enabled = true;
        hfThirdApprovePerson.Value = radcbFirstApprovePerson.Value.ToString();
        

        if (radbtnSave.Enabled == false)
        {
            radbtnUpdate.Enabled = true;
        }
    }
    protected void radcbApprovalType_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Approval Type >>", "0");
        //if (!radcbApprovalType.Items.Contains(newItem))
        //{
        //    radcbApprovalType.Items.Insert(0, newItem);
        //}
   
    }

    protected void radbtnReset_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "505px");
        lblResult.Text = string.Empty;
        EnableButtons(true);
        radbtnUpdate.Enabled = true;
        radcbFirstApprovePerson.SelectedIndex = -1;
        radcbSecondApprovePerson.SelectedIndex = -1;
        radcbThirdApprovePerson.SelectedIndex = -1;
        formContainer.Attributes.CssStyle.Add("height", "505px");
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String downloadExcelFromGrid()
    {


        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize("ok");
    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        grEmployeeLeaveSetup.Columns[12].Visible = false;
        grEmployeeLeaveSetup.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
     
    }
}