
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

public partial class Languages_Language : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "Languages";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Languages objLanguages = new Languages();
    DataTable dtLanguages = new DataTable();

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
                        if (btnUpdate.Visible == true || btnDelete.Visible == true)
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
            ViewState["dtLanguages"] = dtLanguages;
        }
    }

    #region Methods
        
    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfLanguageId.Value = dtTable.Rows[0]["LanguageID"].ToString();
            txtLanguageName.Text = dtTable.Rows[0]["LanguageName"].ToString();          
        }
    }
    
    private void InitializeControls()
    {
        hfLanguageId.Value = string.Empty;
        txtLanguageName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }
   
    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objLanguages.AddLanguage(txtLanguageName.Text);

            if (!objLanguages.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objLanguages.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "235px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "235px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "235px");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objLanguages.UpdateLanguage(Convert.ToInt32(hfLanguageId.Value), txtLanguageName.Text);

            if (!objLanguages.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objLanguages.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "235px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "235px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "235px");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objLanguages.DeleteLanguage(Convert.ToInt32(hfLanguageId.Value));
        
        if (objLanguages.IsError)
        {
            lblResult.Text = objLanguages.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
        }
        formContainer.Attributes.CssStyle.Add("height", "235px");

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "235px");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        //formContainer.Attributes.CssStyle.Add("height", "235px");
    }

    #endregion

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            dtLanguages = objLanguages.GetLanguage(Convert.ToInt32(e.CommandArgument));
            BindData(dtLanguages);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            InitializeControls();
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfLanguageId.Value = RadGrid1.GetRowValues(index, "LanguageID").ToString();
            txtLanguageName.Text = RadGrid1.GetRowValues(index, "LanguageName").ToString();


            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            formContainer.Attributes.CssStyle.Add("height", "235px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "235px");
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
        RadGrid1.Columns[2].Visible = false;
        RadGrid1.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
        //RadToolTip1.Visible = !RadToolTip1.Visible;
    }
}
