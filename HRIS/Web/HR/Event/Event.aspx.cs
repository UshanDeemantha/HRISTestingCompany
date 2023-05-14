
using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.HR.BLL;
using Telerik.Web.UI;
using HRM.Common.BLL;
using System.Drawing;
using DevExpress.Web.Rendering;

public partial class Event_Event : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EventSubmissions";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    HRM.HR.BLL.Event objEvent = new HRM.HR.BLL.Event();
    DataTable dtEvent = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;
        lblResult.ForeColor = Color.Red;
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
                    ViewState["IsModify"] = false;
                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSave.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                        if (btnDelete.Visible == true || btnUpdate.Visible == true)
                        {
                            ViewState["IsModify"] = true;
                        }
                    }
                }
            }
        }

        if (!IsPostBack)
        {
            InitializeControls();
        }
    }

    #region Methods

    private void InitializeControls()
    {
        hfEventId.Value = string.Empty;
        txtEventCode.Text = string.Empty;
        txtName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtFromDate.SelectedDate = null;
        txtToDate.SelectedDate = null;
        txtEventDate.SelectedDate = null;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;

        BindEventTypes();
        ddlEventType.SelectedIndex = -1;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void BindEventTypes()
    {
        ddlEventType.DataSource = objEvent.GetEventType(0);
        ddlEventType.DataValueField = "EventTypeID";
        ddlEventType.DataTextField = "EventTypeName";
        ddlEventType.DataBind();
    }

    private bool IsValidate()
    {
        if (ddlEventType.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Event Type!";
            return false;
        }

        if (Convert.ToDateTime(txtFromDate.SelectedDate) >= Convert.ToDateTime(txtToDate.SelectedDate))
        {
            lblResult.Text = "From Date cannot be higher than or equal to 'To Date'!";
            return false;
        }

        if (Convert.ToDateTime(txtFromDate.SelectedDate) < DateTime.Today)
        {
            lblResult.Text = "'To Date' cannot be less than current day!";
            return false;
        }
        if (Convert.ToDateTime(txtEventDate.SelectedDate) < DateTime.Today)
        {
            lblResult.Text = "'Event Date' cannot be less than current day!";
            return false;
        }

        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfEventId.Value = dtTable.Rows[0]["EventID"].ToString();
            txtEventCode.Text = dtTable.Rows[0]["EventCode"].ToString();
            txtName.Text = dtTable.Rows[0]["Name"].ToString();
            ddlEventType.SelectedValue = dtTable.Rows[0]["EventTypeID"].ToString();
            txtDescription.Text= dtTable.Rows[0]["Description"].ToString();
            txtFromDate.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["FromDate"]);
            txtToDate.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["ToDate"]);
            txtEventDate.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["EventDate"]);
            cbActive.Checked = Convert.ToBoolean(dtTable.Rows[0]["Active"]);
        }
    }

    #endregion

    #region Grid View

    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable dtEvent = objEvent.GetEvent(Convert.ToInt32(e.CommandArgument));
            BindData(dtEvent);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion

    #region Button

    protected void fuEmployeeImage_DataBinding(object sender, EventArgs e)
    {
       // Image1.ImageUrl = Server.MapPath(fuEmployeeImage.FileName);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            objEvent.AddEvent(Convert.ToInt32(ddlEventType.SelectedValue), txtEventCode.Text, txtName.Text, txtDescription.Text, Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate), Convert.ToDateTime(txtEventDate.SelectedDate));
         
            if (!objEvent.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
                formContainer.Attributes.CssStyle.Add("height", "440px");
            }
            else
            {
                formContainer.Attributes.CssStyle.Add("height", "440px");
                lblResult.Text = "Unable to Save";
            }
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "440px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objEvent.UpdateEvent(Convert.ToInt32(hfEventId.Value), Convert.ToInt32(ddlEventType.SelectedValue), txtEventCode.Text, txtName.Text, txtDescription.Text, Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate), Convert.ToDateTime(txtEventDate.SelectedDate), cbActive.Checked);
            
            if (!objEvent.IsError)
            {
                formContainer.Attributes.CssStyle.Add("height", "440px");
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                formContainer.Attributes.CssStyle.Add("height", "440px");
                lblResult.Text = "Unable to Save";
            }
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "440px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "440px");
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objEvent.DeleteEvent(Convert.ToInt32(hfEventId.Value));
    
        if (objEvent.IsError)
        {
            formContainer.Attributes.CssStyle.Add("height", "440px");
            lblResult.Text = objEvent.ErrorMsg;
        }
        else
        {
            formContainer.Attributes.CssStyle.Add("height", "440px");
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }

    #endregion

    protected void ddlEventType_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Event Type >>", "0");
        if (!ddlEventType.Items.Contains(newItem))
        {
            formContainer.Attributes.CssStyle.Add("height", "440px");
            ddlEventType.Items.Insert(0, newItem);
        }
    }
    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        #region Grid Permitions

        //GridItem cmdItem = RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
        //LinkButton lbtnEdit = cmdItem.FindControl("btnEditSelected") as LinkButton;
        //lbtnEdit.Visible = false;
        //if (Convert.ToBoolean(ViewState["IsModify"]) == true)
        //{
        //    lbtnEdit.Visible = true;
        //}
        //else
        //{
        //    lbtnEdit.Visible = false;
        //}
        #endregion
    }
    protected void RadGrid1_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

    }
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objEvent.GetEvent(Convert.ToInt32(dataItem.GetDataKeyValue("EventID")));
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

    protected void RadButton1_Click(object sender, EventArgs e)
    {

    }

    protected void lkSelectPopup_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

        hfEventId.Value = RadGrid1.GetRowValues(index, "EventID").ToString();
        // hfCompanyBranchId.Value = radgvDetails.GetRowValues(index, "CompanyId").ToString();
        txtEventCode.Text = RadGrid1.GetRowValues(index, "EventCode").ToString();
        ddlEventType.Text = RadGrid1.GetRowValues(index, "EventTypeID").ToString();
        txtName.Text = RadGrid1.GetRowValues(index, "Name").ToString();
        txtDescription.Text = RadGrid1.GetRowValues(index, "Description").ToString();
        var txtFromDate = RadGrid1.GetRowValues(index, "FromDate").ToString();
        var txtToDate = RadGrid1.GetRowValues(index, "ToDate").ToString();
        var txtEventDate = RadGrid1.GetRowValues(index, "EventDate").ToString();
        cbActive.Text = RadGrid1.GetRowValues(index, "Active").ToString();

        formContainer.Attributes.CssStyle.Add("height", "440px");
    }

    protected void ddlEventType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "440px");

    }
}

