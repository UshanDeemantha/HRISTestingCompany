using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using System.Drawing;
using Telerik.Web.UI;

public partial class Course_CourseType : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "CourseType";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Course objCourse = new Course();
    DataTable dtCourseType = new DataTable();
    
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
            ViewState["dtCourseType"] = dtCourseType;
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
        hfCourseTypeId.Value = string.Empty;
        txtCourseCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
       
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfCourseTypeId.Value = dtTable.Rows[0]["CourseTypeID"].ToString();
            txtCourseCode.Text = dtTable.Rows[0]["CourseTypeCode"].ToString();
            txtCourseName.Text = dtTable.Rows[0]["CourseTypeName"].ToString();
        }
    }
    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objCourse.AddCourseType(txtCourseCode.Text, txtCourseName.Text);
            
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
            objCourse.UpdateCourseType(Convert.ToInt32(hfCourseTypeId.Value), txtCourseCode.Text, txtCourseName.Text);
            
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
        objCourse.DeleteCourseType(Convert.ToInt32(hfCourseTypeId.Value));
        
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

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    #endregion
    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                dtCourseType = objCourse.GetCourseTypeDetails(Convert.ToInt32(gvDetail.SelectedItems[0].Cells[2].Text));
                BindData(dtCourseType);
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
               
            }
        }
        catch
        { 
        }
    }
    protected void RadGrid1_ItemDeleted(object sender, Telerik.Web.UI.GridDeletedEventArgs e)
    {

    }
    protected void gvDetail_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
