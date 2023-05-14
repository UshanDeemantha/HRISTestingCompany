using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;

public partial class Certification_Certification : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "Certification";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Certification objCertification = new Certification();
        DataTable dtCertification = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
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

        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
        }
    }

    #region Methods
    //#region Access Permissions
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
    //#endregion

    private void InitializeControls()
    {
        hfCertificationId.Value = string.Empty;
        txtCertificationCode.Text = string.Empty;
        txtCertificationName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        BindInstitute();
        ddlInstitue.SelectedIndex = -1;
    }

    private void BindInstitute()
    {
        ddlInstitue.DataSource = objCertification.GetInstituteDetails(0);
        ddlInstitue.DataValueField = "InstituteID";
        ddlInstitue.DataTextField = "InstituteName";
        ddlInstitue.DataBind();
    }

    private bool IsValidate()
    {
        if (ddlInstitue.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Institute!";
            return false;
        }
        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfCertificationId.Value = dtTable.Rows[0]["CertificationID"].ToString();
            txtCertificationCode.Text = dtTable.Rows[0]["CertificationCode"].ToString();
            txtCertificationName.Text = dtTable.Rows[0]["CertificationName"].ToString();
            ddlInstitue.SelectedValue = dtTable.Rows[0]["InstituteID"].ToString();
        }
    }
    #endregion

    #region Grid View
    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
           dtCertification = objCertification.GetCertification(Convert.ToInt32(e.CommandArgument));
            BindData(dtCertification);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                dtCertification = objCertification.GetCertification(Convert.ToInt32(RadGrid1.SelectedItems[0].Cells[2].Text));
                BindData(dtCertification);
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch
            {
            }
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
            objCertification.AddCertification(txtCertificationCode.Text, Convert.ToInt32(ddlInstitue.SelectedValue), txtCertificationName.Text);
         
            if (!objCertification.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
                RadGrid1.DataBind();
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

        InitializeControls();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objCertification.UpdateCertification(Convert.ToInt32(hfCertificationId.Value), txtCertificationCode.Text, Convert.ToInt32(ddlInstitue.SelectedValue), txtCertificationName.Text);
            if (!objCertification.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
                RadGrid1.DataBind();
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

        InitializeControls();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objCertification.DeleteCertification(Convert.ToInt32(hfCertificationId.Value));
        if (objCertification.IsError)
        {
            lblResult.Text = objCertification.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
            RadGrid1.DataBind();
        }
        InitializeControls();

    }

    #endregion

    protected void ddlInstitue_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Institute >>", "0");
        if (!ddlInstitue.Items.Contains(newItem))
        {
            ddlInstitue.Items.Insert(0, newItem);
        }
    }
    protected void RadGrid1_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

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
}
