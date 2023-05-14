using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Employee_UserControls_EmoloyeeSportsControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Sport objSport = new Sport();
    Employee objEmployee = new Employee();
    DataTable dtEmployeeSport = new DataTable();

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

        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            BindEmplyees();
            ViewState["dtEmployeeSport"] = dtEmployeeSport;
        }
    }

    public long EmployeeId
    {
        set
        {
            ViewState["EmployeeId"] = value;
            if (ViewState["EmployeeId"] != null)
            {
                GetEmployeeSports(Convert.ToInt64(ViewState["EmployeeId"]));
                hfEmployeeId.Value = ViewState["EmployeeId"].ToString();
                //gvDetails.DataBind();
            }
        }
    }

    private void GetEmployeeSports(long EmployeeId)
    {
        objSport = new Sport();
        dtEmployeeSport = objSport.GetEmployeeSport(EmployeeId);
        if (dtEmployeeSport.Rows.Count > 0)
        {
            BindData(dtEmployeeSport.Rows[0]);
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
        hfEmployeeSportId.Value = string.Empty;
       // ddlEmployee.SelectedIndex = -1;
        ddlSport.SelectedIndex = -1;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;
        //BindEmplyees();
        BindSkill();
      

    }


    private void BindEmplyees()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0,Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]),"Common");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "Name";
        ddlEmployee.DataBind();
    }
    private void BindSkill()
    {
        //ddlSport.DataSource = objSport.GetSport(0);
        //ddlSport.DataValueField = "SportID";
        //ddlSport.DataTextField = "SportName";
        //ddlSport.DataBind();
    }


    private bool IsValidate()
    {
        //if (ddlEmployee.SelectedIndex <= 0)
        //{
        //    lblResult.Text = "Select Employee!";
        //    return false;
        //}
        if (ddlSport.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Sport!";
            return false;
        }

        return true;
    }



    private void BindData(DataRow drRow)
    {
        InitializeControls();
        hfEmployeeSportId.Value = drRow["EmployeeSportActivityID"].ToString();
        ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
          //  ddlSport.SelectedValue = dtTable.Rows[0]["SportID"].ToString();

    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfEmployeeSportId.Value = dtTable.Rows[0]["EmployeeSportActivityID"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
            ddlSport.SelectedValue = dtTable.Rows[0]["SportID"].ToString();

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

            objSport.AddEmployeeSport2(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlSport.SelectedValue));
            if (!objSport.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objSport.ErrorMsg;
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
            objSport.UpdateEmployeeSport(Convert.ToInt32(hfEmployeeSportId.Value), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlSport.SelectedValue));
            if (!objSport.IsError)
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
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objSport.DeleteEmployeeSport(Convert.ToInt32(hfEmployeeSportId.Value));
        if (objSport.IsError)
        {
            lblResult.Text = objSport.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
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
            DataTable dtEmployeeSport = objSport.GetEmployeeSport(Convert.ToInt32(e.CommandArgument));
            BindData(dtEmployeeSport);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
    #endregion

    protected void ddlSport_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select Sport >>", "0");
        //if (!ddlSport.Items.Contains(newItem))
        //{
        //    ddlSport.Items.Insert(0, newItem);
        //}
    }
    protected void ddlSport_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
    protected void RadComboBox1_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Sport >>", "0");
        if (!ddlSport.Items.Contains(newItem))
        {
            ddlSport.Items.Insert(0, newItem);
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
        //        DataTable dtEvent = objSport.GetEmployeeSport(Convert.ToInt32(dataItem.GetDataKeyValue("EmployeeSportActivityID")));
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

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        hfEmployeeSportId.Value = RadGrid1.GetRowValues(index, "EmployeeSportActivityID").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeID").ToString();
        ddlSport.SelectedValue = RadGrid1.GetRowValues(index, "SportID").ToString();
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
    }
}