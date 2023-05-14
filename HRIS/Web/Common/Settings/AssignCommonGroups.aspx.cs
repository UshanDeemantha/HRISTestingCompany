using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Common_Settings_AssignCommonGroups : System.Web.UI.Page
{

    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "GroupAssign";

    MksSecurity objSecurity = new MksSecurity();
    Employee objEmployee = new Employee();

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
                    btnSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSave.Visible = true;
                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;

                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        //radbtnCancel.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);

                        if (btnCancel.Visible == true || btnUpdate.Visible == true)
                        {
                            ViewState["IsModify"] = true;
                        }
                    }
                }
            }
            else
            {
                radcbApprovalType.Enabled = true;
                ddlLeaveGroups.Enabled = true;
                RadComboBox2.Enabled = true;
            }

            //ListBox1Fill();
        }

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (ViewState["IsUpdateSelected"] == null)
        {
            FillEmployeeDropDown("NotIn");
        }
        if (txtRootLevel.Text == "" & radcbApprovalType.Text == "")
        {
            formContainer.Attributes.CssStyle.Add("height", "54px");
        }
        else
        {
            formContainer.Attributes.CssStyle.Add("height", "445px");
        }
    }

    protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void EnabledDisabled(bool Enabled)
    {
        btnSave.Enabled = Enabled;
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

    protected void txtRootLevel_PreRender(object sender, EventArgs e)
    {

    }

    private void EnableButtons(bool Flag)
    {
        btnSave.Enabled = Flag;
        btnUpdate.Enabled = Flag;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      //  formContainer.Attributes.CssStyle.Add("height", "400px");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       // formContainer.Attributes.CssStyle.Add("height", "400px");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //formContainer.Attributes.CssStyle.Add("height", "400px");
    }

    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            try
            {
                List<ListEditItem> RemoveListitem = new List<ListEditItem>();
                IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.CheckedItems;
                
                formContainer.Attributes.CssStyle.Add("height", "445px");
            }

            catch
            {
                formContainer.Attributes.CssStyle.Add("height", "445px");
                lblResult.Text = "Some Error Occured!";
            }
        }
    }

    private bool Validation()
    {
        if (radcbApprovalType.Value == null)
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Please Select an Approval Type.";
            return false;
        }
        if (ddlLeaveGroups.Text == "")
        {
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Please Select a Group Name.";
            return false;
        }
        if (RadComboBox2.CheckedItems.Count<=0)
        { 
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Please Select Employee Name.";
            return false;
        }
        return true;
    }

    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void radbtnDelete_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "445px");
    }
    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            List<ListEditItem> RemoveListitem = new List<ListEditItem>();
            IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.CheckedItems;
            
            formContainer.Attributes.CssStyle.Add("height", "445px");
        } 
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "445px");
            lblResult.Text = "Some Error Occured!";
        }
    }


    private void InitilizedControl()
    {
        hfOrganizationStructure.Value = "0";
        txtRootLevel.Text = string.Empty;
        radcbApprovalType.SelectedIndex = -1;
        ddlLeaveGroups.SelectedIndex = -1;
        txtRootLevel.Enabled = true;
        ViewState["IsUpdateSelected"] = null;
        EnableButtons(true);
        btnUpdate.Enabled = false;

        radcbApprovalType.Enabled = false;
        ddlLeaveGroups.Enabled = false;
        RadComboBox2.Enabled = false;
        btnUpdate.Enabled = false;
        btnSave.Enabled = false;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitilizedControl();
        lblResult.Text = string.Empty;
        formContainer.Attributes.CssStyle.Add("height", "445px");
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
                formContainer.Attributes.CssStyle.Add("height", "445px");
            }
            formContainer.Attributes.CssStyle.Add("height", "445px");
        }
        formContainer.Attributes.CssStyle.Add("height", "445px");
    }



    protected void lkSelect_Click(object sender, EventArgs e)
    {

        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            if (RadComboBox2.CheckedItems.Count >= 1)
            {

                InitilizedControl();

                hfAassignId.Value = gridView.GetRowValues(index, "CommonAssignDataId").ToString();
                // var EmployeeName = gridView.GetRowValues(index, "Name").ToString();
                var EmployeeId = gridView.GetRowValues(index, "EmployeeId").ToString();
                var ApprovalTypeID = gridView.GetRowValues(index, "ApprovalTypeID").ToString();
                var ApprovalGroupName = gridView.GetRowValues(index, "ApprovalWorkFlowId").ToString();
                long EmpTxn = Convert.ToInt64(gridView.GetRowValues(index, "EmployeeId").ToString());
                DataBindToControl(EmpTxn);
                bool Active = Convert.ToBoolean(gridView.GetRowValues(index, "IsActive"));



                Session["id"] = hfAassignId.Value;


                ListItem addItems = new ListItem();

                //addItems.Text = EmployeeName.ToString();
                addItems.Value = EmployeeId.ToString();
                addItems.Selected = true;
                // cblEmployees.Items.Add(addItems);

                radcbApprovalType.Value = ApprovalTypeID;
               // DataTable groups = Leave.GetApprovalGroups(Convert.ToInt32(radcbApprovalType.Value));
               // ddlLeaveGroups.DataSource = groups;
                ddlLeaveGroups.DataBind();

                radcbApprovalType.Enabled = true;
                ddlLeaveGroups.Enabled = true;
                RadComboBox2.Enabled = false;
                txtRootLevel.Enabled = false;
                ddlLeaveGroups.Value = ApprovalGroupName;
                btnCancel.Enabled = true;
                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
                formContainer.Attributes.CssStyle.Add("height", "445px");
                if (ViewState["IsUpdateSelected"] == null)
                {
                    FillEmployeeDropDown("In");
                }
                ViewState["IsUpdateSelected"] = "True";



                IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.Items;
                foreach (Telerik.Web.UI.RadComboBoxItem items in itemList)
                {
                    formContainer.Attributes.CssStyle.Add("height", "445px");
                    if (items.Value == EmployeeId.ToString())
                    {
                        formContainer.Attributes.CssStyle.Add("height", "445px");
                        items.Checked = true;
                    }
                }
                formContainer.Attributes.CssStyle.Add("height", "445px");


            }
            else
            {
                hfAassignId.Value = gridView.GetRowValues(index, "CommonAssignDataId").ToString();
                // var EmployeeName = gridView.GetRowValues(index, "Name").ToString();
                var EmployeeId = gridView.GetRowValues(index, "EmployeeId").ToString();
                var ApprovalTypeID = gridView.GetRowValues(index, "ApprovalTypeID").ToString();
                var ApprovalGroupName = gridView.GetRowValues(index, "ApprovalWorkFlowId").ToString();
                long EmpTxn = Convert.ToInt64(gridView.GetRowValues(index, "EmployeeId").ToString());
                DataBindToControl(EmpTxn);
                bool Active = Convert.ToBoolean(gridView.GetRowValues(index, "IsActive"));



                Session["id"] = hfAassignId.Value;


                ListItem addItems = new ListItem();

                //addItems.Text = EmployeeName.ToString();
                addItems.Value = EmployeeId.ToString();
                addItems.Selected = true;
                // cblEmployees.Items.Add(addItems);

                radcbApprovalType.Value = ApprovalTypeID;
               // DataTable groups = Leave.GetApprovalGroups(Convert.ToInt32(radcbApprovalType.Value));
                //ddlLeaveGroups.DataSource = groups;
                ddlLeaveGroups.DataBind();

                radcbApprovalType.Enabled = true;
                ddlLeaveGroups.Enabled = true;
                RadComboBox2.Enabled = false;
                txtRootLevel.Enabled = false;
                ddlLeaveGroups.Value = ApprovalGroupName;
                btnCancel.Enabled = true;
                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
                formContainer.Attributes.CssStyle.Add("height", "445px");
                if (ViewState["IsUpdateSelected"] == null)
                {
                    FillEmployeeDropDown("In");
                }
                ViewState["IsUpdateSelected"] = "True";



                IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.Items;
                foreach (Telerik.Web.UI.RadComboBoxItem items in itemList)
                {
                    formContainer.Attributes.CssStyle.Add("height", "445px");
                    if (items.Value == EmployeeId.ToString())
                    {
                        formContainer.Attributes.CssStyle.Add("height", "445px");
                        items.Checked = true;
                    }
                }
                formContainer.Attributes.CssStyle.Add("height", "445px");



            }



        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "445px");
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
        if (btnSave.Enabled != false)
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
        if (btnSave.Enabled != false)
        {
            //  ListBox1Fill();
        }
        formContainer.Attributes.CssStyle.Add("height", "445px");
    }
    protected void radcbApprovalType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (btnUpdate.Enabled == true)
        {
            RadComboBox2.Enabled = false;
        }
        else
        {
            RadComboBox2.Enabled = true;
        }
        ddlLeaveGroups.Value = null;
        ddlLeaveGroups.DataSource = null;
        //DataTable groups = Leave.GetApprovalGroups(Convert.ToInt32(radcbApprovalType.Value));
        //ddlLeaveGroups.DataSource = groups;
        ddlLeaveGroups.DataBind();
        formContainer.Attributes.CssStyle.Add("height", "445px");
    }

    private void FillEmployeeDropDown(string status)
    {
        int orgStrucId = 0;
        if (hfOrganizationStructure.Value != null && hfOrganizationStructure.Value != "")
        {
            orgStrucId = Convert.ToInt32(hfOrganizationStructure.Value);
        }
        //DataTable dt = Leave.GetInOrNotInAssignEmployees(Convert.ToInt32(radcbApprovalType.Value), Convert.ToInt32(Session["CompanyId"]), orgStrucId, status);
        //RadComboBox2.DataSource = dt;
        RadComboBox2.DataBind();
       
    }


    protected void RadComboBox2_TextChanged(object sender, EventArgs e)
    {
        btnCancel.Enabled = true;
        btnUpdate.Enabled = false;
        btnSave.Enabled = true;
    }

    protected void btnOrganizationSelect_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "445px");
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
        grEmployeeLeaveSetup.Columns[9].Visible = false;
        grEmployeeLeaveSetup.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
        //RadToolTip1.Visible = !RadToolTip1.Visible;

    }
}