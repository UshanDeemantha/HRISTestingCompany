using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;

public partial class Course_Course : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
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
    }

    private void BindCourseTypes()
    {
        ddlCourseType.DataSource = objCourse.GetCourseTypeDetails(0);
        ddlCourseType.DataValueField = "CourseTypeID";
        ddlCourseType.DataTextField = "CourseTypeName";
        ddlCourseType.DataBind();
    }

    private void BindInstitute()
    {
        ddlInstitute.DataSource = objCourse.GetInstituteDetails(0);
        ddlInstitute.DataValueField = "InstituteID";
        ddlInstitute.DataTextField = "InstituteName";
        ddlInstitute.DataBind();
    }

    private bool IsValidate()
    {
        if (ddlCourseType.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Course Type!";
            return false;
        }
        if (ddlInstitute.SelectedIndex <= 0)
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
            hfCourseId.Value = dtTable.Rows[0]["CourseID"].ToString();
            txtCourseCode.Text = dtTable.Rows[0]["CourseCode"].ToString();
            txtCourseName.Text = dtTable.Rows[0]["CourseName"].ToString();
            ddlCourseType.SelectedValue = dtTable.Rows[0]["CourseTypeID"].ToString();
            ddlInstitute.SelectedValue = dtTable.Rows[0]["InstituteID"].ToString();
            txtCourseStreem.Text = dtTable.Rows[0]["CourseStreem"].ToString();
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
            objCourse.AddCourse(txtCourseCode.Text, txtCourseName.Text, Convert.ToInt32(ddlCourseType.SelectedValue), Convert.ToInt32(ddlInstitute.SelectedValue), txtCourseStreem.Text);

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
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }

        gvDetail.DataBind();
        InitializeControls();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objCourse.UpdateCourse(Convert.ToInt32(hfCourseId.Value), txtCourseCode.Text, txtCourseName.Text, Convert.ToInt32(ddlCourseType.SelectedValue), Convert.ToInt32(ddlInstitute.SelectedValue), txtCourseStreem.Text);

            if (!objCourse.IsError)
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

        gvDetail.DataBind();
        InitializeControls();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objCourse.DeleteCourse(Convert.ToInt32(hfCourseId.Value));

        if (objCourse.IsError)
        {
            lblResult.Text = objCourse.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
        }

        gvDetail.DataBind();
        InitializeControls();

    }

    #endregion

    #region DropDown

 

    #endregion
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                dtCourse = objCourse.GetCourseDetails(Convert.ToInt32(gvDetail.SelectedItems[0].Cells[2].Text));
                BindData(dtCourse);
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch
            {
            }
        }
    }
    protected void RadGrid1_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

    }
    protected void ddlInstitute_DataBinding(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Institute >>", "0");
        if (!ddlInstitute.Items.Contains(newItem))
        {
            ddlInstitute.Items.Insert(0, newItem);
        }
    }
   
    protected void ddlCourseType_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Course Type >>", "0");
        if (ddlCourseType.Items.FindItemByValue("0") == null)
        {
            ddlCourseType.Items.Insert(0, newItem);
        }
    }
    protected void ddlInstitute_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Course Type >>", "0");
        if (ddlInstitute.Items.FindItemByValue("0") == null)
        {
            ddlInstitute.Items.Insert(0, newItem);
        }
    }
    protected void gvDetail_ItemCreated(object sender, GridItemEventArgs e)
    {
        #region Grid Permitions

        GridItem cmdItem = gvDetail.MasterTableView.GetItems(GridItemType.CommandItem)[0];
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
