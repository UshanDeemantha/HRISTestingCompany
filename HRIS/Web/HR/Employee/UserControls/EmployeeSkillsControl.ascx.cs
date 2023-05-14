using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Employee_UserControls_EmployeeSkillsControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Skills objSkill = new Skills();
    Employee objEmployee = new Employee();
    DataTable dtSkillEmployee = new DataTable();
    
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
            ViewState["dtSkillEmployee"] = dtSkillEmployee;

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
                GetEmployeeSkills(Convert.ToInt64(ViewState["EmployeeId"]));
                hfEmployeeId.Value = ViewState["EmployeeId"].ToString();
            }
        }
    }

    private void GetEmployeeSkills(long EmployeeId)
    {
        objSkill = new Skills();
        dtSkillEmployee = objSkill.GetEmployeeSkillByEmployeeId(EmployeeId);
        if (dtSkillEmployee.Rows.Count > 0)
        {
            BindData(dtSkillEmployee.Rows[0]);
        }
        else
        {
            InitializeControls();
            ddlEmployee.SelectedValue = EmployeeId.ToString();
        }
    }

    #region Methods
    #region Access Permissions
    //private void GetUserPermission()
    //{
    //    MKSLogin objLogin = new MKSLogin();
    //    ViewState["FullControl"] = false;
    //    ViewState["Add"] = false;
    //    ViewState["Edit"] = false;
    //    ViewState["Delete"] = false;
    //    ViewState["ViewOnly"] = false;
    //    ViewState["None"] = false;
    //    DataTable dtAccessRights = objLogin.AccessRights(Session["UserName"].ToString(), Convert.ToInt32(Session["LocationId"]), "Language");

    //    if (dtAccessRights.Rows.Count <= 0)
    //    {
    //        ViewState["None"] = true;
    //        Response.Redirect("~/AccessDenied.aspx");
    //        return;
    //    }
    //    else
    //    {
    //        for (int i = 0; i < dtAccessRights.Rows.Count; i++)
    //        {
    //            string permission = dtAccessRights.Rows[i]["Permission"].ToString();
    //            switch (permission)
    //            {
    //                case "FullControl": ViewState["FullControl"] = true;
    //                    ViewState["Add"] = true;
    //                    ViewState["Edit"] = true;
    //                    ViewState["Delete"] = true;
    //                    break;
    //                case "Add": ViewState["Add"] = true;
    //                    break;
    //                case "Edit": ViewState["Edit"] = true;
    //                    break;
    //                case "Delete": ViewState["Delete"] = true;
    //                    break;
    //                case "ViewOnly": ViewState["ViewOnly"] = true;
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //    }
    //    UserAccessPermissions();
    //}

    //private void UserAccessPermissions()
    //{
    //    if (Convert.ToBoolean(ViewState["ViewOnly"]) == true)
    //    {
    //        btnSave.Visible = false;
    //        btnUpdate.Visible = false;
    //        btnDelete.Visible = false;
    //        gvDetails.Columns[0].Visible = false;
    //        return;
    //    }

    //    if (Convert.ToBoolean(ViewState["FullControl"]) == true)
    //    {
    //        btnSave.Visible = true;
    //        btnUpdate.Visible = true;
    //        btnDelete.Visible = true;
    //        gvDetails.Columns[0].Visible = true;
    //        return;
    //    }

    //    if (Convert.ToBoolean(ViewState["Add"]) == true)
    //    {
    //        btnSave.Visible = true;
    //    }
    //    else
    //    {
    //        btnSave.Visible = false;
    //    }

    //    if (Convert.ToBoolean(ViewState["Edit"]) == true)
    //    {
    //        btnUpdate.Visible = true;
    //        gvDetails.Columns[0].Visible = true;
    //    }
    //    else
    //    {
    //        btnUpdate.Visible = false;
    //        gvDetails.Columns[0].Visible = false;
    //    }

    //    if (Convert.ToBoolean(ViewState["Delete"]) == true)
    //    {
    //        btnDelete.Visible = true;
    //    }
    //    else
    //    {
    //        btnDelete.Visible = false;
    //    }
    //} 
    #endregion
    private void InitializeControls()
    {
        hfEmployeeSkillId.Value = string.Empty;
        //ddlEmployee.SelectedIndex = -1;
        ddlSkill.SelectedIndex = -1;
        ddlSkillGrade.SelectedIndex = -1;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        
        BindEmplyees();
        BindSkill();
        BindSkillGrade();
    }


    private void BindEmplyees()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "Name";
        ddlEmployee.DataBind();
    }
    private void BindSkill()
    {
        ddlSkill.DataSource = objSkill.GetSkill(0);
        ddlSkill.DataValueField = "SkillID";
        ddlSkill.DataTextField = "SkillName";
        ddlSkill.DataBind();
    }
    private void BindSkillGrade()
    {
        ddlSkillGrade.DataSource = objSkill.GetSkillGrade(0);
        ddlSkillGrade.DataValueField = "SkillGradeID";
        ddlSkillGrade.DataTextField = "SkillGradeName";
        ddlSkillGrade.DataBind();
    }


    private bool IsValidate()
    {
        //if (ddlEmployee.SelectedIndex <= 0)
        //{
        //    lblResult.Text = "Select Employee!";
        //    return false;
        //}
        if (ddlSkill.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Skill!";
            return false;
        }
        if (ddlSkillGrade.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Skill Grade !";
            return false;
        }
        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfEmployeeSkillId.Value = dtTable.Rows[0]["EmployeeSkillID"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
            ddlSkill.SelectedValue = dtTable.Rows[0]["SkillID"].ToString();
            ddlSkillGrade.SelectedValue = dtTable.Rows[0]["SkillGradeID"].ToString();
        }
    }

    private void BindData(DataRow drRow)
    {
        InitializeControls();

        hfEmployeeSkillId.Value = drRow["EmployeeSkillID"].ToString();
        ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
            //ddlSkill.SelectedValue = dtTable.Rows[0]["SkillID"].ToString();
            //ddlSkillGrade.SelectedValue = dtTable.Rows[0]["SkillGradeID"].ToString();
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

            objSkill.AddEmployeeSkill2(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlSkill.SelectedValue), Convert.ToInt32(ddlSkillGrade.SelectedValue));
            if (!objSkill.IsError)
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
        if (!IsValidate())
        {
            return;
        }
        try
        {
            
            objSkill.UpdateEmployeeSkill(Convert.ToInt32(hfEmployeeSkillId.Value), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlSkill.SelectedValue), Convert.ToInt32(ddlSkillGrade.SelectedValue));
            if (!objSkill.IsError)
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



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objSkill.DeleteEmployeeSkill(Convert.ToInt32(hfEmployeeSkillId.Value));

        if (objSkill.IsError)
        {
            lblResult.Text = objSkill.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "successfully Deleted.";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();

    }

    #endregion

    #region DropDowns

    protected void ddlSkill_PreRender(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select Skill >>", "0");
        //if (!ddlSkill.Items.Contains(newItem))
        //{
        //    ddlSkill.Items.Insert(0, newItem);
        //}
    }
    protected void ddlSkillGrade_PreRender(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem1 = new RadComboBoxItem("<< Select Skill Grade >>", "0");
        //if (!ddlSkillGrade.Items.Contains(newItem1))
        //{
        //    ddlSkillGrade.Items.Insert(0, newItem1);
        //}
    }
    #endregion

    #region Grid View
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable dtEmployeeSkill = objSkill.GetEmployeeSkill(Convert.ToInt32(e.CommandArgument));
            BindData(dtEmployeeSkill);
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
        //        DataTable dtEvent = objSkill.GetEmployeeSkill(Convert.ToInt32(dataItem.GetDataKeyValue("EmployeeSkillID")));
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
    protected void ddlSkillGrade_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem1 = new RadComboBoxItem("<< Select Skill Grade >>", "0");
        if (!ddlSkillGrade.Items.Contains(newItem1))
        {
            ddlSkillGrade.Items.Insert(0, newItem1);
        }
    }
    protected void ddlSkill_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Skill >>", "0");
        if (!ddlSkill.Items.Contains(newItem))
        {
            ddlSkill.Items.Insert(0, newItem);
        }
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        hfEmployeeSkillId.Value = RadGrid1.GetRowValues(index, "EmployeeSkillID").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeID").ToString();
        ddlSkill.SelectedValue = RadGrid1.GetRowValues(index, "SkillID").ToString();
        ddlSkillGrade.SelectedValue = RadGrid1.GetRowValues(index, "SkillGradeID").ToString();

        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
    }
}