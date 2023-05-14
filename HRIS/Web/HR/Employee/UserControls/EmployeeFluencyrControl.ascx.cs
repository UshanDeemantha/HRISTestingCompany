using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Employee_UserControls_EmployeeFluencyrControl : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Fluency objFluency = new Fluency();
    Employee objEmployee = new Employee();
    DataTable dtEmployeeFluency = new DataTable();

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
            ViewState["dtEmployeeFluency"] = dtEmployeeFluency;

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
                GetEmployeeFluency(Convert.ToInt64(ViewState["EmployeeId"]));
                hfEmployeeId.Value = ViewState["EmployeeId"].ToString();

                //gvDetails.DataBind();
            }
        }
    }

    public void GetEmployeeFluency(long EmployeeId)
    {
        objFluency = new Fluency();
        dtEmployeeFluency = objFluency.GetEmployeeFluencyByEmployeeId(EmployeeId);
        if (dtEmployeeFluency.Rows.Count > 0)
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            BindData(dtEmployeeFluency.Rows[0]);
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
        hfLanguId.Value = string.Empty;

        ddlLanguge.SelectedIndex = -1;
        ddlReading.SelectedItem.Text = "Good";
        ddlSpeaking.SelectedItem.Text = "Good";
        ddlWriting.SelectedItem.Text = "Good";
        ddlListening.SelectedItem.Text = "Good";
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;

        //BindSkill();
        lblResult.ForeColor = Color.Green;
        lblResult.Text = string.Empty;

    }


    private void BindEmplyees()
    {
        ddlEmployee.DataSource = objEmployee.GetEmployee(0, Session["UserName"].ToString(), Convert.ToInt32(Session["CompanyId"]), "Common");
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataTextField = "Name";
        ddlEmployee.DataBind();
    }
    //private void BindSkill()
    //{
    //    ddlFluency.DataSource = objFluency.GetFluency(0);
    //    ddlFluency.DataValueField = "FluencyID";
    //    ddlFluency.DataTextField = "FluencyName";
    //    ddlFluency.DataBind();
    //}


    private bool IsValidate()
    {

        if (ddlLanguge.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Fluency!";
            return false;
        }

        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfLanguId.Value = dtTable.Rows[0]["LanguId"].ToString();
            ddlEmployee.SelectedValue = dtTable.Rows[0]["EmployeeID"].ToString();
            ddlLanguge.SelectedValue = dtTable.Rows[0]["LanguageID"].ToString();
            ddlReading.SelectedItem.Text = dtTable.Rows[0]["Reading"].ToString();
            ddlWriting.SelectedItem.Text= dtTable.Rows[0]["Writing"].ToString();
            ddlSpeaking.SelectedItem.Text = dtTable.Rows[0]["Speaking"].ToString();
            ddlListening.SelectedItem.Text = dtTable.Rows[0]["Listening"].ToString();

        }
    }

    private void BindData(DataRow drRow)
    {
        InitializeControls();

        hfLanguId.Value = drRow["LanguId"].ToString();
        ddlEmployee.SelectedValue = drRow["EmployeeID"].ToString();
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
            objFluency.AddEmployeeFluency2(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlLanguge.SelectedValue), ddlReading.Text.Trim(), ddlWriting.Text.Trim(),
                    ddlSpeaking.Text.Trim(), ddlListening.Text.Trim(), Convert.ToInt32(ddlEmployee.SelectedValue));
            
            if (!objFluency.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objFluency.ErrorMsg;
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
            objFluency.UpdateEmployeeFluency(Convert.ToInt32(hfLanguId.Value), Convert.ToInt32(ddlLanguge.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue), ddlReading.Text.Trim(), ddlWriting.Text.Trim(),
                    ddlSpeaking.Text.Trim(), ddlListening.Text.Trim(), Convert.ToInt32(ddlEmployee.SelectedValue));
            if (!objFluency.IsError)
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
        objFluency.DeleteEmployeeFluency(Convert.ToInt32(hfLanguId.Value));
        if (objFluency.IsError)
        {
            lblResult.Text = objFluency.ErrorMsg;
        }
        else
        {
            InitializeControls();
            lblResult.ForeColor = Color.Green;
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
            DataTable dtEmployeeFluency = objFluency.GetEmployeeFluency(Convert.ToInt32(e.CommandArgument));
            BindData(dtEmployeeFluency);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
    #endregion

    protected void ddlFluency_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Employee >>", "0");
        if (!ddlEmployee.Items.Contains(newItem))
        {
            ddlEmployee.Items.Insert(0, newItem);
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
        //        DataTable dtEvent = objFluency.GetEmployeeFluency(Convert.ToInt32(dataItem.GetDataKeyValue("LanguId")));
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
    protected void ddlLanguge_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Languge >>", "0");
        if (!ddlLanguge.Items.Contains(newItem))
        {
            ddlLanguge.Items.Insert(0, newItem);
        }
    }
    protected void ddlSpeaking_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlSpeaking.Items.Contains(newItem))
        {
            ddlSpeaking.Items.Insert(0, newItem);
        }
    }
    protected void ddlListening_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlListening.Items.Contains(newItem))
        {
            ddlListening.Items.Insert(0, newItem);
        }
    }
    protected void ddlWriting_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlWriting.Items.Contains(newItem))
        {
            ddlWriting.Items.Insert(0, newItem);
        }
    }
    protected void ddlReading_DataBound1(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Proficiency >>", "0");
        if (!ddlReading.Items.Contains(newItem))
        {
            ddlReading.Items.Insert(0, newItem);
        }
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        hfLanguId.Value = RadGrid1.GetRowValues(index, "LanguId").ToString();
        ddlEmployee.SelectedValue = RadGrid1.GetRowValues(index, "EmployeeId").ToString();
        ddlLanguge.SelectedValue = RadGrid1.GetRowValues(index, "LanguageID").ToString();
        ddlReading.Text = RadGrid1.GetRowValues(index, "Reading").ToString();
        ddlWriting.Text = RadGrid1.GetRowValues(index, "Writing").ToString();
        ddlSpeaking.Text = RadGrid1.GetRowValues(index, "Speaking").ToString();
        ddlListening.Text = RadGrid1.GetRowValues(index, "Listening").ToString();
        //txtYear.Text = RadGrid1.GetRowValues(index, "Year").ToString();
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
    }
}