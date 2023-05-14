using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Certification_Certification : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
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
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void BindInstitute()
    {
        ddlInstitue.DataSource = objCertification.GetInstituteDetails(0);
        ddlInstitue.ValueField = "InstituteID";
        ddlInstitue.TextField = "InstituteName";
        ddlInstitue.DataBind();
        formContainer.Attributes.CssStyle.Add("height", "333px");
    }

    private bool IsValidate()
    {
        formContainer.Attributes.CssStyle.Add("height", "333px");
        if (ddlInstitue.SelectedIndex < 0)
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
            ddlInstitue.Value = dtTable.Rows[0]["InstituteID"].ToString();
            formContainer.Attributes.CssStyle.Add("height", "333px");
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
            objCertification.AddCertification(txtCertificationCode.Text, Convert.ToInt32(ddlInstitue.Value), txtCertificationName.Text);
         
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
            formContainer.Attributes.CssStyle.Add("height", "333px");

        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "Unable to Save";
        }

        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "333px");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objCertification.UpdateCertification(Convert.ToInt32(hfCertificationId.Value), txtCertificationCode.Text, Convert.ToInt32(ddlInstitue.Value), txtCertificationName.Text);
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
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "Unable to Save";
        }

        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "333px");
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        //formContainer.Attributes.CssStyle.Add("height", "333px");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objCertification.DeleteCertification(Convert.ToInt32(hfCertificationId.Value));
        if (objCertification.IsError)
        {
            lblResult.Text = objCertification.ErrorMsg;
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
            RadGrid1.DataBind();
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        formContainer.Attributes.CssStyle.Add("height", "333px");
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "333px");

    }

    #endregion

    protected void ddlInstitue_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select Institute >>", "0");
        //if (!ddlInstitue.Items.Contains(newItem))
        //{
        //    ddlInstitue.Items.Insert(0, newItem);
        //}
    }
    protected void RadGrid1_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

    }


    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {

            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            //InitializeControls();
            var RadGrid1 = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfCertificationId.Value = RadGrid1.GetRowValues(index, "CertificationID").ToString();
            txtCertificationCode.Text = RadGrid1.GetRowValues(index, "CertificationCode").ToString();
            //ddlInstitue.SelectedValue = RadGrid1.GetRowValues(index, "InstitueID").ToString();
            txtCertificationName.Text = RadGrid1.GetRowValues(index, "CertificationName").ToString();
            ddlInstitue.Value = RadGrid1.GetRowValues(index, "InstituteID").ToString();
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "";
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String downloadExcelFromGrid()
    {


        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize("ok");
    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        RadGrid1.Columns[5].Visible = false;
        RadGrid1.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
        
    }
}