using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Skills_SkillGrade : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "SkillGrade";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Skills objSkill = new Skills();
    DataTable dtSkillGrade = new DataTable();

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
            ViewState["dtSkillGrade"] = dtSkillGrade;
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfSkillGradeId.Value = string.Empty;
        txtSkillGradeCode.Text = string.Empty;
        txtSkillGradeName.Text = string.Empty;
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
            hfSkillGradeId.Value = dtTable.Rows[0]["SkillGradeID"].ToString();
            txtSkillGradeCode.Text = dtTable.Rows[0]["SkillGradeCode"].ToString();
            txtSkillGradeName.Text = dtTable.Rows[0]["SkillGradeName"].ToString();          
        }
    }

    #endregion
    
    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objSkill.AddSkillGrade(txtSkillGradeCode.Text, txtSkillGradeName.Text);

            if (!objSkill.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objSkill.ErrorMsg;
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
            objSkill.UpdateSkillGrade(Convert.ToInt32(hfSkillGradeId.Value), txtSkillGradeCode.Text, txtSkillGradeName.Text);
            
            if (!objSkill.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objSkill.ErrorMsg;
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
        objSkill.DeleteSkillGrade(Convert.ToInt32(hfSkillGradeId.Value));

        if (objSkill.IsError)
        {
            lblResult.Text = objSkill.ErrorMsg;
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

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {            
            dtSkillGrade = objSkill.GetSkillGrade(Convert.ToInt32(e.CommandArgument));

            BindData(dtSkillGrade);
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

            hfSkillGradeId.Value = RadGrid1.GetRowValues(index, "SkillGradeID").ToString();
            txtSkillGradeCode.Text = RadGrid1.GetRowValues(index, "SkillGradeCode").ToString();
            txtSkillGradeName.Text = RadGrid1.GetRowValues(index, "SkillGradeName").ToString();


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
