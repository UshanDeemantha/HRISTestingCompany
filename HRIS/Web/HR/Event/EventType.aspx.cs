
using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web.Rendering;

public partial class Event_EventType : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EventTypes";

    HRM.Common.BLL.MksSecurity objSecurity = new HRM.Common.BLL.MksSecurity();

    #endregion

    Event objEvent = new Event();
    DataTable dtEventType = new DataTable();

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
            ViewState["dtEventType"] = dtEventType;
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfEventTypeId.Value = string.Empty;
        txtEventCode.Text = string.Empty;
        txtEventName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        formContainer.Attributes.CssStyle.Add("height", "54px");

    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
    
        if (dtTable.Rows.Count > 0)
        {
            hfEventTypeId.Value = dtTable.Rows[0]["EventTypeID"].ToString();
            txtEventCode.Text = dtTable.Rows[0]["EventTypeCode"].ToString();
            txtEventName.Text = dtTable.Rows[0]["EventTypeName"].ToString();
        }
    }
    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objEvent.AddEventType(txtEventCode.Text, txtEventName.Text);
            
            if (!objEvent.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
                formContainer.Attributes.CssStyle.Add("height", "305px");
            }
            else
            {
                formContainer.Attributes.CssStyle.Add("height", "305px");

                lblResult.Text = "Unable to Save";
            }

        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "305px");

            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "305px");

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objEvent.UpdateEventType(Convert.ToInt32(hfEventTypeId.Value), txtEventCode.Text, txtEventName.Text);
            
            if (!objEvent.IsError)
            {
                formContainer.Attributes.CssStyle.Add("height", "305px");

                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                formContainer.Attributes.CssStyle.Add("height", "305px");

                lblResult.Text = "Unable to Save";
            }

        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "305px");

            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "305px");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "305px");


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objEvent.DeleteEventType(Convert.ToInt32(hfEventTypeId.Value));
        
        if (objEvent.IsError)
        {
            formContainer.Attributes.CssStyle.Add("height", "305px");

            lblResult.Text = objEvent.ErrorMsg;
        }
        else
        {
            formContainer.Attributes.CssStyle.Add("height", "305px");

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "305px");

    }

    #endregion

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable
            dtEventType = objEvent.GetEventType(Convert.ToInt32(e.CommandArgument));
            BindData(dtEventType);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion


    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

        //if (e.CommandName == "Select")
        //{
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objEvent.GetEventType(Convert.ToInt32(dataItem.GetDataKeyValue("EventTypeID")));
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
    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        hfEventTypeId.Value = RadGrid1.GetRowValues(index, "EventTypeID").ToString();
        txtEventCode.Text = RadGrid1.GetRowValues(index, "EventTypeCode").ToString();
        txtEventName.Text = RadGrid1.GetRowValues(index, "EventTypeName").ToString();
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
        formContainer.Attributes.CssStyle.Add("height", "305px");

    }

    protected void RadButton1_Click(object sender, EventArgs e)
    {
        RadGrid1.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Century Gothic";
        GridExporter.Styles.Default.Font.Size = 10;
    }
}
