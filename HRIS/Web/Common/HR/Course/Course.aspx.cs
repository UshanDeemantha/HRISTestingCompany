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

public partial class Course_Course : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "Course";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Course objCourse = new Course();
    DataTable dtCourse = new DataTable();

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

    private void InitializeControls()
    {
        hfCourseId.Value = string.Empty;
        txtCourseCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        txtCourseStreem.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        BindCourseTypes();
        BindInstitute();
        ddlCourseType.SelectedIndex = -1;
        ddlInstitute.SelectedIndex=-1;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void BindCourseTypes()
    {
        formContainer.Attributes.CssStyle.Add("height", "400px");
        ddlCourseType.DataSource = objCourse.GetCourseTypeDetails(0);
        ddlCourseType.ValueField = "CourseTypeID";
        ddlCourseType.TextField = "CourseTypeName";
        ddlCourseType.DataBind();
        formContainer.Attributes.CssStyle.Add("height", "400px");
    }

    private void BindInstitute()
    {
        ddlInstitute.DataSource = objCourse.GetInstituteDetails(0);
        ddlInstitute.ValueField = "InstituteID";
        ddlInstitute.TextField = "InstituteName";
        ddlInstitute.DataBind();
    }

    private bool IsValidate()
    {
        formContainer.Attributes.CssStyle.Add("height", "400px");
        if (ddlCourseType.SelectedIndex < 0)
        {
            lblResult.Text = "Select Course Type!";
            return false;

        }
        formContainer.Attributes.CssStyle.Add("height", "400px");
        if (ddlInstitute.SelectedIndex < 0)
        {
            lblResult.Text = "Select Institute!";
            return false;
        }
        formContainer.Attributes.CssStyle.Add("height", "400px");
        return true;
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
       
        if (dtTable.Rows.Count > 0)
        {
            hfCourseId.Value = dtTable.Rows[0]["CourseID"].ToString();
            txtCourseCode.Text = dtTable.Rows[0]["CourseCode"].ToString();
            txtCourseName.Text = dtTable.Rows[0]["CourseName"].ToString();
            ddlCourseType.Value = dtTable.Rows[0]["CourseTypeID"].ToString();
            ddlInstitute.Value = dtTable.Rows[0]["InstituteID"].ToString();
            txtCourseStreem.Text = dtTable.Rows[0]["CourseStreem"].ToString();
            formContainer.Attributes.CssStyle.Add("height", "400px");
        }
    }

    #endregion

    #region Grid View

    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
             dtCourse = objCourse.GetCourseDetails(Convert.ToInt32(e.CommandArgument));
            BindData(dtCourse);
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
            objCourse.AddCourse(txtCourseCode.Text, txtCourseName.Text, Convert.ToInt32(ddlCourseType.Value), Convert.ToInt32(ddlInstitute.Value), txtCourseStreem.Text);

            if (!objCourse.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = "Unable to Save";
            }
            formContainer.Attributes.CssStyle.Add("height", "400px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "400px");
            lblResult.Text = "Unable to Save";
        }

        gvDetail.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "400px");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objCourse.UpdateCourse(Convert.ToInt32(hfCourseId.Value), txtCourseCode.Text, txtCourseName.Text, Convert.ToInt32(ddlCourseType.Value), Convert.ToInt32(ddlInstitute.Value), txtCourseStreem.Text);

            if (!objCourse.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
                formContainer.Attributes.CssStyle.Add("height", "400px");
            }
            else
            {
                formContainer.Attributes.CssStyle.Add("height", "400px");
                lblResult.Text = "Unable to Save";
            }
            formContainer.Attributes.CssStyle.Add("height", "400px");

        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "400px");
            lblResult.Text = "Unable to Save";
        }

        gvDetail.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "400px");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        //formContainer.Attributes.CssStyle.Add("height", "400px");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objCourse.DeleteCourse(Convert.ToInt32(hfCourseId.Value));

        if (objCourse.IsError)
        {
            lblResult.Text = objCourse.ErrorMsg;
            formContainer.Attributes.CssStyle.Add("height", "400px");
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
            InitializeControls();
            formContainer.Attributes.CssStyle.Add("height", "400px");
        }
        formContainer.Attributes.CssStyle.Add("height", "400px");

        gvDetail.DataBind();
        formContainer.Attributes.CssStyle.Add("height", "400px");

    }

    #endregion

    #region DropDown



    #endregion
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
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfCourseId.Value = gvDetail.GetRowValues(index, "CourseID").ToString();
            txtCourseCode.Text = gvDetail.GetRowValues(index, "CourseCode").ToString();
            txtCourseName.Text = gvDetail.GetRowValues(index, "CourseName").ToString();
            ddlCourseType.Value = gvDetail.GetRowValues(index, "CourseTypeID").ToString();
            ddlInstitute.Value = gvDetail.GetRowValues(index, "InstituteID").ToString();
            txtCourseStreem.Text = gvDetail.GetRowValues(index, "CourseStreem").ToString();
            formContainer.Attributes.CssStyle.Add("height", "400px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "400px");
            lblResult.Text = "";
        }
    }



    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "400px");
    }

    protected void ddlCourseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "400px");
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
        gvDetail.Columns[8].Visible = false;
        gvDetail.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
       
    }
}

