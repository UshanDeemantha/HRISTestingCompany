
using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.HR.BLL;
using Telerik.Web.UI;
using HRM.Common.BLL;
using System.Drawing;

public partial class Fluency_Fluency : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "Fluency";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Fluency objFluency = new Fluency();
    DataTable dtFluency = new DataTable();

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
        hfFluencyId.Value = string.Empty;
        txtFluencyCode.Text = string.Empty;
        txtFluencyName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled= false;
        btnDelete.Enabled = false;
        BindFluencyTypes();
        ddlFluencyType.SelectedIndex = -1;
    }

    private void BindFluencyTypes()
    {
        ddlFluencyType.DataSource = objFluency.GetFluencyType(0);
        ddlFluencyType.DataValueField = "FluencyTypeID";
        ddlFluencyType.DataTextField = "FluencyTypeName";
        ddlFluencyType.DataBind();
    }

    private bool IsValidate()
    {
        if (ddlFluencyType.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Fluency Type!";
            return false;
        }
        return true;       
    }
    
    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfFluencyId.Value = dtTable.Rows[0]["FluencyID"].ToString();
            txtFluencyCode.Text = dtTable.Rows[0]["FluencyCode"].ToString();
            txtFluencyName.Text = dtTable.Rows[0]["FluencyName"].ToString();
            ddlFluencyType.SelectedValue = dtTable.Rows[0]["FluencyTypeID"].ToString();
        }
    }

    #endregion

    #region Grid View

    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable dtFluency = objFluency.GetFluency(Convert.ToInt32(e.CommandArgument));
            BindData(dtFluency);
            btnSave.Enabled= false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    } 

    #endregion

    #region Button

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }

        try
        {
            objFluency.AddFluency(txtFluencyCode.Text, Convert.ToInt32(ddlFluencyType.SelectedValue), txtFluencyName.Text);
            
            if (!objFluency.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
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

        RadGrid1.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objFluency.UpdateFluency(Convert.ToInt32(hfFluencyId.Value), txtFluencyCode.Text, Convert.ToInt32(ddlFluencyType.SelectedValue), txtFluencyName.Text);

            if (!objFluency.IsError)
            {
                lblResult.ForeColor = Color.Green;
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

        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objFluency.DeleteFluency(Convert.ToInt32(hfFluencyId.Value));

        if (objFluency.IsError)
        {
            lblResult.Text = objFluency.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }

    #endregion
    
    protected void ddlFluencyType_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Fluency Type >>", "0");
        if (!ddlFluencyType.Items.Contains(newItem))
        {
            ddlFluencyType.Items.Insert(0, newItem);
        }
    }
    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        #region Grid Permitions

        GridItem cmdItem = RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
        LinkButton lbtnEdit = cmdItem.FindControl("btnEditSelected") as LinkButton;
        lbtnEdit.Visible = false;
        if (Convert.ToBoolean(ViewState["IsModify"]) == true)
        {
            lbtnEdit.Visible = true;
        }
        else
        {
            lbtnEdit.Visible = false;
        }
        #endregion
    }
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
                DataTable dtEvent = objFluency.GetFluency(Convert.ToInt32(dataItem.GetDataKeyValue("FluencyID")));
                BindData(dtEvent);
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch
            {
                if (RadGrid1.SelectedItems.Count <= 0)
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = "Select item for modify!";
                }
            }
        }
    }
}
