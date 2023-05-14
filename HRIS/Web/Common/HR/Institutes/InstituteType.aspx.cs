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

public partial class Institutes_InstituteType : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "InstituteType";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Institute objInstitute = new Institute();
    DataTable dtInstituteType = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;

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
            ViewState["dtInstituteType"] = dtInstituteType;
        }
    }

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {        
            objInstitute.AddInstituteType(txtInstituteTypeCode.Text, txtInstituteTypeName.Text);

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
        try
        {
            objInstitute.UpdateInstituteType(Convert.ToInt32(hfInstituteTypeId.Value), txtInstituteTypeCode.Text, txtInstituteTypeName.Text);
            
            if (!objInstitute.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = "Unable to Save";
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
       DataTable tblInstite= objInstitute.GetInstituteByTypeId(Convert.ToInt32(hfInstituteTypeId.Value));
        if (tblInstite.Rows.Count > 0)
        {
            lblResult.Text = "Can not be Delete..! Already assigned to Insititue!";       }
        else
        {
            objInstitute.DeleteInstituteType(Convert.ToInt32(hfInstituteTypeId.Value));

            if (objInstitute.IsError)
            {
                lblResult.Text = objInstitute.ErrorMsg;
            }
            else
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Deleted.";
            }

            formContainer.Attributes.CssStyle.Add("height", "335px");
            RadGrid1.DataBind();
            InitializeControls();
            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        //formContainer.Attributes.CssStyle.Add("height", "335px");
    }

    #endregion

    #region Methods
    
    private void InitializeControls()
    {
        hfInstituteTypeId.Value = string.Empty;
        txtInstituteTypeCode.Text = string.Empty;
        txtInstituteTypeName.Text = string.Empty;
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
            hfInstituteTypeId.Value = dtTable.Rows[0]["InstituteTypeID"].ToString();
            txtInstituteTypeCode.Text = dtTable.Rows[0]["InstituteTypeCode"].ToString();
            txtInstituteTypeName.Text = dtTable.Rows[0]["InstituteTypeName"].ToString();
        }
    }

    #endregion

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            dtInstituteType = objInstitute.GetInstituteTypeDetails(Convert.ToInt32(e.CommandArgument));

            BindData(dtInstituteType);

            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion
    protected void btnEditSelected_Click(object sender, EventArgs e)

    {
        try
        {
            InitializeControls();
            var RadGrid1 = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfInstituteTypeId.Value = RadGrid1.GetRowValues(index, "InstituteTypeID").ToString();
            txtInstituteTypeCode.Text = RadGrid1.GetRowValues(index, "InstituteTypeCode").ToString();
            txtInstituteTypeName.Text = RadGrid1.GetRowValues(index, "InstituteTypeName").ToString();
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String downloadExcelFromGrid()
    {


        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize("ok");
    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        RadGrid1.Columns[3].Visible = false;
        RadGrid1.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
    
    }
}
