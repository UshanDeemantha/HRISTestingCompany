using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.HR.BLL;
using Telerik.Web.UI;
using HRM.Common.BLL;
using System.Drawing;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Institutes_Institutes : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
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
        formContainer.Attributes.CssStyle.Add("height", "54px");
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
            ddlInsitituteType.Text = dtTable.Rows[0]["InstituteTypeID"].ToString();
            txtTel.Text = dtTable.Rows[0]["Tel"].ToString();
            txtFax.Text = dtTable.Rows[0]["Fax"].ToString();
            txtAddress.Text = dtTable.Rows[0]["Address"].ToString();
        }
    }


    private void BindInstitutesTypes()
    {
        ddlInsitituteType.DataSource = objInstitute.GetInstituteTypeDetails(0);
        ddlInsitituteType.ValueField = "InstituteTypeID";
        ddlInsitituteType.TextField = "InstituteTypeName";
        ddlInsitituteType.DataBind();
    }

    private bool IsValidate()
    {
        formContainer.Attributes.CssStyle.Add("height", "416px");
        if (ddlInsitituteType.SelectedIndex < 0)
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
            objInstitute.AddInstitute(txtInstituteCode.Text, txtInstituteName.Text, Convert.ToInt32(ddlInsitituteType.Value),txtTel.Text,txtFax.Text,txtAddress.Text);
            
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
            formContainer.Attributes.CssStyle.Add("height", "416px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "416px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "416px");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //if (!IsValidate())
        //{
        //    return;
        //}
        formContainer.Attributes.CssStyle.Add("height", "416px");
        try
        {
            objInstitute.UpdateInstitute(Convert.ToInt32(hfInstitutesId.Value), txtInstituteCode.Text, txtInstituteName.Text,Convert.ToInt32(ddlInsitituteType.Value),txtTel.Text,txtFax.Text,txtAddress.Text);
            
            if (!objInstitute.IsError)
            {
              
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
                formContainer.Attributes.CssStyle.Add("height", "416px");
            }
            else
            {
                lblResult.Text = objInstitute.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "416px");

        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "416px");
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "416px");
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
            InitializeControls();
        }
        formContainer.Attributes.CssStyle.Add("height", "416px");

        RadGrid1.DataBind();
        formContainer.Attributes.CssStyle.Add("height", "416px");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        //formContainer.Attributes.CssStyle.Add("height", "416px");
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
        //if (!ddlInsitituteType.Items.Contains(newItem))
        //{
        //    ddlInsitituteType.Items.Insert(0, newItem);
        //}
    }


    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfInstitutesId.Value = RadGrid1.GetRowValues(index, "InstituteID").ToString();
            txtInstituteCode.Text = RadGrid1.GetRowValues(index, "InstituteCode").ToString();
            txtInstituteName.Text = RadGrid1.GetRowValues(index, "InstituteName").ToString();
            ddlInsitituteType.Text = RadGrid1.GetRowValues(index, "InstituteTypeID").ToString();
            txtTel.Text = RadGrid1.GetRowValues(index, "Tel").ToString();
            txtFax.Text = RadGrid1.GetRowValues(index, "Fax").ToString();
            txtAddress.Text = RadGrid1.GetRowValues(index, "Address").ToString();
            formContainer.Attributes.CssStyle.Add("height", "416px");
        }
        catch (Exception ex)
        {
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
        RadGrid1.Columns[8].Visible = false;
        RadGrid1.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
       
    }
}
