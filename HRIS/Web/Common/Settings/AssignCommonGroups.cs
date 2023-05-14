using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using HRM.Time.BLL;
using System.Drawing;
using DevExpress.Web;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HRM.Leave.BLL;
using DevExpress.Web.Rendering;

public partial class Common_Settings_AssignCommonGroups : System.Web.UI.Page
{

    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "GroupAssign";

    MksSecurity objSecurity = new MksSecurity();
    EmployeeTimeSetup objCommonGroups = new EmployeeTimeSetup();
    Employee objEmployee = new Employee();
    Leave Leave = new Leave();

    #endregion

    CompanyProfile objTimeSetup = new CompanyProfile();
    protected void Page_Load(object sender, EventArgs e)
    {


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
                    Response.Redirect("~/Common/NoPermissions.aspx");
                    return;
                }
                else
                {
                    radbtnSave.Visible = false;
                    radbtnUpdate.Visible = false;
                    radbtnCancel.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                        radbtnUpdate.Visible = true;
                        radbtnCancel.Visible = true;

                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        //radbtnCancel.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);

                        if (radbtnCancel.Visible == true || radbtnUpdate.Visible == true)
                        {
                            ViewState["IsModify"] = true;
                        }
                    }
                }
            }

            //ListBox1Fill();
        }

    }
   
    protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    {
        //tbSearchSupplier.Text = string.Empty;
        //hfEmloyeeId.Value = "-99";
    }
    //protected void list1Allcheckd_Change(object sender, EventArgs e)
    //{
    //    list1Allcheckd.Checked = false;
    //    //hfEmployeeTypeCode.Value = ddlEmployeeTypeId.SelectedItem.Value;
    //}


    private void EnabledDisabled(bool Enabled)
    {
        radbtnSave.Enabled = Enabled;
        //radbtnUpdate.Enabled = !Enabled;
        //radbtnDelete.Enabled = !Enabled;
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {

            {

            }

        }
        catch
        { }
    }

    private void EnableButtons(bool Flag)
    {
        radbtnSave.Enabled = Flag;
        radbtnUpdate.Enabled = Flag;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<ListEditItem> RemoveListitem = new List<ListEditItem>();
            IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.CheckedItems;
            foreach (Telerik.Web.UI.RadComboBoxItem item in itemList)
            {
                
                    Leave.AddCommonGroupsData(radcbApprovalType.SelectedItem.Value.ToString(),ddlLeaveGroups.SelectedItem.Value.ToString(), Session["UserName"].ToString(), Convert.ToInt32(item.Value));
                  
                
            }

            if (!Leave.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Entered...";
                grEmployeeLeaveSetup.DataBind();
                InitilizedControl();
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = Leave.ErrorMsg;
                grEmployeeLeaveSetup.DataBind();
            }
        }
        catch
        {
            lblResult.Text = "Some Error Occured!";
        }
    }
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void radbtnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
       try
        {
            List<ListEditItem> RemoveListitem = new List<ListEditItem>();
            IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.CheckedItems;
            foreach (Telerik.Web.UI.RadComboBoxItem item in itemList)
            {
                
                
                    Leave.UpdateCommonGroupsData(Convert.ToInt64(Session["id"].ToString()),ddlLeaveGroups.SelectedItem.Value.ToString(),  Session["UserName"].ToString(), Convert.ToInt32(item.Value));
                   
                
            }

            if (!Leave.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
                grEmployeeLeaveSetup.DataBind();
                InitilizedControl();
                radcbApprovalType.Enabled = true;
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = Leave.ErrorMsg;
                grEmployeeLeaveSetup.DataBind();
            }
        }
        catch
        {
            lblResult.Text = "Some Error Occured!";
        }
    }
  

    private void InitilizedControl()
    {
        hfOrganizationStructure.Value = "0";
        lblOrganization.Text = string.Empty;
        radcbApprovalType.SelectedIndex = -1;
        ddlLeaveGroups.SelectedIndex = -1;
        //list1Allcheckd.Checked = false;
        //cblEmployees.Items.Clear();
       
        EnableButtons(true);
        radbtnUpdate.Enabled = false;
       
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitilizedControl();
        lblResult.Text = string.Empty;
    }
    protected void ddlEmployee_DataBound(object sender, EventArgs e)
    {

    }

    //public void ListBox1Fill()
    //{
        
    //    var ff = hfOrganizationStructure.Value;
    //    if (ff != "" && radcbApprovalType.Value != null && ddlLeaveGroups.Value != null)
    //    {
    //        DataTable dtEmployee = Leave.GetOrganizationStructureEmployee(Convert.ToInt32(Session["CompanyId"]), Convert.ToInt32(ff), Convert.ToInt32(radcbApprovalType.Value), Convert.ToInt32(ddlLeaveGroups.Value));

    //        cblEmployees.DataSource = dtEmployee;
    //        cblEmployees.DataValueField = "EmployeeId";
    //        cblEmployees.DataTextField = "FirstName";
    //        cblEmployees.DataBind();
    //    }
    //    //else if(ff == "")
    //    //{
    //    //    lblResult.ForeColor = Color.Red;
    //    //    lblResult.Text = "Please Select Organization Level!";
    //    //}
        

    //}


    private void DataBindToControl(long EmployeeId)
    {
        IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.Items;
        foreach (Telerik.Web.UI.RadComboBoxItem items in itemList)
        {
            if (items.Value == EmployeeId.ToString())
            {
                items.Checked = true;
            }
        }
    }


  
    protected void lkSelect_Click(object sender, EventArgs e)
    {

        try
        {
            
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            var EmployeeName = gridView.GetRowValues(index, "NameWithInitials").ToString();
            var EmployeeId = gridView.GetRowValues(index, "EmployeeId").ToString();
            var ApprovalTypeID = gridView.GetRowValues(index, "ApprovalTypeID").ToString();
            var ApprovalGroupName = gridView.GetRowValues(index, "ApprovalWorkFlowId").ToString();
            long EmpTxn = Convert.ToInt64(gridView.GetRowValues(index, "EmployeeId").ToString());
            DataBindToControl(EmpTxn);
            bool Active = Convert.ToBoolean(gridView.GetRowValues(index, "IsActive"));
     
            hfAassignId.Value = gridView.GetRowValues(index, "CommonAssignDataId").ToString();

            Session["id"] = hfAassignId.Value;

            
             ListItem addItems = new ListItem();
           
            addItems.Text =EmployeeName.ToString();
            addItems.Value =  EmployeeId.ToString();
            addItems.Selected = true;
           // cblEmployees.Items.Add(addItems);


           
           
            radcbApprovalType.Value = ApprovalTypeID;
            DataTable groups = Leave.GetApprovalGroups(Convert.ToInt32(radcbApprovalType.Value));
            ddlLeaveGroups.DataSource = groups;
            ddlLeaveGroups.DataBind();
            
            //RadComboBox2.SelectedValue = EmployeeId;
            radcbApprovalType.Enabled = false;
            radbtnUpdate.Enabled = true;
            radbtnSave.Enabled = false;
            ddlLeaveGroups.Value = ApprovalGroupName;
            
        }
        catch
        {
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

    protected void ddlLeaveGroups_TextChanged(object sender, EventArgs e)
    {
        if (radbtnSave.Enabled != false)
        {
           // ListBox1Fill();
        }
    }
    protected void ddlLeaveGroups_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Comman Group >>", "0");
        //if (!ddlLeaveGroups.Items.Contains(newItem))
        //{
        //    ddlLeaveGroups.Items.Insert(0, newItem);
        //}
    }
    //protected void list1Allcheckd_CheckedChanged1(object sender, EventArgs e)
    //{
    //    foreach (ListItem item in cblEmployees.Items)
    //    {
    //        if (list1Allcheckd.Checked == true)
    //        {
    //            item.Selected = true;
    //        }
    //        else
    //        {
    //            item.Selected = false;
    //        }
    //    }
    //}
    protected void ddlLeaveGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radbtnSave.Enabled != false)
        {
          //  ListBox1Fill();
        }
    }
    protected void radcbApprovalType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable groups = Leave.GetApprovalGroups(Convert.ToInt32(radcbApprovalType.Value));
        ddlLeaveGroups.DataSource = groups;
        ddlLeaveGroups.DataBind();
    }
    
}