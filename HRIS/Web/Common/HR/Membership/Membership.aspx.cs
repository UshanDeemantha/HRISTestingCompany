using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web.Rendering;

public partial class Membership_Membership : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "Memberships";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Membership objMembership = new Membership();
    DataTable dtMembership = new DataTable();

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
        hfMembershipId.Value = string.Empty;
        txtMembershipCode.Text = string.Empty;
        txtMembershipName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled= false;
        formContainer.Attributes.CssStyle.Add("height", "54px");
        BindMembershipTypes();
        ddlMembershipType.SelectedIndex = -1;
    }

    private void BindMembershipTypes()
    {
        //ddlMembershipType.DataSource = objMembership.GetMembershipType(0);
        //ddlMembershipType.DataValueField = "MembershipTypeID";
        //ddlMembershipType.DataTextField = "MembershipTypeName";
        //ddlMembershipType.DataBind();
    }

    private bool IsValidate()
    {
        if (ddlMembershipType.SelectedIndex < 0)
        {
            lblResult.Text = "Select Membership Type!";
            return false;
        }
        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfMembershipId.Value = dtTable.Rows[0]["MembershipID"].ToString();
            txtMembershipCode.Text = dtTable.Rows[0]["MembershipCode"].ToString();
            txtMembershipName.Text = dtTable.Rows[0]["MembershipName"].ToString();
            ddlMembershipType.Value = dtTable.Rows[0]["MembershipTypeID"].ToString();
        }
    }
    #endregion
    
    #region Grid View
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable dtMembership = objMembership.GetMembership(Convert.ToInt32(e.CommandArgument));
            BindData(dtMembership);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled= true;
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
            objMembership.AddMembership(txtMembershipCode.Text, Convert.ToInt32(ddlMembershipType.Value), txtMembershipName.Text);

            if (!objMembership.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objMembership.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "335px");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }

        try
        {
            objMembership.UpdateMembership(Convert.ToInt32(hfMembershipId.Value), txtMembershipCode.Text, Convert.ToInt32(ddlMembershipType.Value), txtMembershipName.Text);
            
            if (!objMembership.IsError)
            {
                  lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objMembership.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "335px");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objMembership.DeleteMembership(Convert.ToInt32(hfMembershipId.Value));

        if (objMembership.IsError)
        {
            lblResult.Text = objMembership.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
            InitializeControls();
        }
        formContainer.Attributes.CssStyle.Add("height", "335px");

        RadGrid1.DataBind();
        formContainer.Attributes.CssStyle.Add("height", "335px");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       InitializeControls();
      //formContainer.Attributes.CssStyle.Add("height", "335px");

    }

    #endregion

    protected void ddlMembershipType_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem NewItem = new RadComboBoxItem("<< Select Membership Type >>", "0");
        //if (!ddlMembershipType.Items.Contains(NewItem))
        //{
        //    ddlMembershipType.Items.Insert(0, NewItem);
        //}
    }


    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            InitializeControls();
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfMembershipId.Value = RadGrid1.GetRowValues(index, "MembershipID").ToString();
            txtMembershipCode.Text = RadGrid1.GetRowValues(index, "MembershipCode").ToString();
            txtMembershipName.Text = RadGrid1.GetRowValues(index, "MembershipName").ToString();
            ddlMembershipType.Value = RadGrid1.GetRowValues(index, "MembershipTypeID").ToString();
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "";
        }

    }

    protected void ddlMembershipType_SelectedIndexChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "335px");
    }

    protected void RadButton1_Click(object sender, EventArgs e)
    {
        RadGrid1.Columns[6].Visible = false;
        RadGrid1.DataBind();
        
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Century Gothic";
        GridExporter.Styles.Default.Font.Size = 10;
      
    }
}
