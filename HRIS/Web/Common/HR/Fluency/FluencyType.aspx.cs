

using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.HR.BLL;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Drawing;

public partial class Fluency_FluencyType : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "FluencyTypes";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Fluency objFluency = new Fluency();
    DataTable dtFluencyType = new DataTable();

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
                    Response.Redirect("~/Common/NoPermissions.aspx");
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
            ViewState["dtFluencyType"] = dtFluencyType;
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfFluencyTypeId.Value = string.Empty;
        txtFluencyCode.Text = string.Empty;
        txtFluencyName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;

    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        
        if (dtTable.Rows.Count > 0)
        {
            hfFluencyTypeId.Value = dtTable.Rows[0]["FluencyTypeID"].ToString();
            txtFluencyCode.Text = dtTable.Rows[0]["FluencyTypeCode"].ToString();
            txtFluencyName.Text = dtTable.Rows[0]["FluencyTypeName"].ToString();
        }
    }

    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objFluency.AddFluencyType(txtFluencyCode.Text, txtFluencyName.Text);
            
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
            objFluency.UpdateFluencyType(Convert.ToInt32(hfFluencyTypeId.Value), txtFluencyCode.Text, txtFluencyName.Text);

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
        objFluency.DeleteFluencyType(Convert.ToInt32(hfFluencyTypeId.Value));
        
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

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable dtFluencyType = objFluency.GetFluencyType(Convert.ToInt32(e.CommandArgument));
            BindData(dtFluencyType);
            
            btnSave.Enabled = false;
            btnUpdate.Enabled= true;
            btnDelete.Enabled = true;
        }
    }   

    #endregion   
    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
                DataTable dtEvent = objFluency.GetFluencyType(Convert.ToInt32(dataItem.GetDataKeyValue("FluencyTypeID")));
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
