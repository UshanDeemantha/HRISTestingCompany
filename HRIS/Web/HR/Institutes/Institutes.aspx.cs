using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.HR.BLL;
using Telerik.Web.UI;
using HRM.Common.BLL;
using System.Drawing;

public partial class Institutes_Institutes : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "Institutes";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Institute objInstitute = new Institute();
    DataTable dtInstitute = new DataTable();

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
            ViewState["dtInstitute"] = dtInstitute;
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfInstitutesId.Value = string.Empty;
        txtInstituteCode.Text = string.Empty;
        txtInstituteName.Text = string.Empty;
        txtTel.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtAddress.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        ddlInsitituteType.SelectedIndex = -1;
        BindInstitutesTypes();
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfInstitutesId.Value = dtTable.Rows[0]["InstituteID"].ToString();
            txtInstituteCode.Text = dtTable.Rows[0]["InstituteCode"].ToString();
            txtInstituteName.Text = dtTable.Rows[0]["InstituteName"].ToString();
            ddlInsitituteType.SelectedValue = dtTable.Rows[0]["InstituteTypeID"].ToString();
            txtTel.Text = dtTable.Rows[0]["Tel"].ToString();
            txtFax.Text = dtTable.Rows[0]["Fax"].ToString();
            txtAddress.Text = dtTable.Rows[0]["Address"].ToString();
        }
    }


    private void BindInstitutesTypes()
    {
        ddlInsitituteType.DataSource = objInstitute.GetInstituteTypeDetails(0);
        ddlInsitituteType.DataValueField = "InstituteTypeID";
        ddlInsitituteType.DataTextField = "InstituteTypeName";
        ddlInsitituteType.DataBind();
    }

    private bool IsValidate()
    {
        if (ddlInsitituteType.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Institute Type!";
            return false;
        }
        return true;
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
            objInstitute.AddInstitute(txtInstituteCode.Text, txtInstituteName.Text, Convert.ToInt32(ddlInsitituteType.SelectedValue),txtTel.Text,txtFax.Text,txtAddress.Text);
            
            if (!objInstitute.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objInstitute.ErrorMsg;
            }
        }
        catch
        {
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
            objInstitute.UpdateInstitute(Convert.ToInt32(hfInstitutesId.Value), txtInstituteCode.Text, txtInstituteName.Text,Convert.ToInt32(ddlInsitituteType.SelectedValue),txtTel.Text,txtFax.Text,txtAddress.Text);
            
            if (!objInstitute.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objInstitute.ErrorMsg;
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
        objInstitute.DeleteInstitute(Convert.ToInt32(hfInstitutesId.Value));
        
        if (objInstitute.IsError)
        {
            lblResult.Text = objInstitute.ErrorMsg;
        }
        else
        {
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

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            dtInstitute = objInstitute.GetInstituteDetails(Convert.ToInt32(e.CommandArgument));
            BindData(dtInstitute);
            
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion

    protected void ddlInsitituteType_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Institute Type >>", "0");
        if (!ddlInsitituteType.Items.Contains(newItem))
        {
            ddlInsitituteType.Items.Insert(0, newItem);
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
                DataTable dtEvent = objInstitute.GetInstituteDetails(Convert.ToInt32(dataItem.GetDataKeyValue("InstituteID")));
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
