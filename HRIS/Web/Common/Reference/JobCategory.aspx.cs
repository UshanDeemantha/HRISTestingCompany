
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using System.Data;
using System.Drawing;
using Telerik.Web.UI;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

public partial class Reference_JobCategory : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "JobCategory";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Reference objReference = new Reference();
    DataTable dtReference = new DataTable();

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
        lblResult.ForeColor = Color.Red;

        if (!IsPostBack)
        {
            InitializeControls();
            EnabledDisabled(true);
            ViewState["dtReference"] = dtReference;
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
        hfJobCategoryId.Value = "0";
        txtJobCategoryCode.Text = string.Empty;
        txtJobCategoryName.Text = string.Empty;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void EnabledDisabled(bool Enabled)
    {
        btnSave.Enabled = Enabled;
        btnUpdate.Enabled = !Enabled;
        btnDelete.Enabled = !Enabled;
    }      

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfJobCategoryId.Value = dtTable.Rows[0]["JobCategoryID"].ToString();
            txtJobCategoryCode.Text = dtTable.Rows[0]["JobCategoryCode"].ToString();
            txtJobCategoryName.Text = dtTable.Rows[0]["JobCategoryName"].ToString();
        }
    }

    #endregion
    
    #region Buttons
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objReference.AddJobCategory(txtJobCategoryCode.Text, txtJobCategoryName.Text,Convert.ToInt32(Session["CompanyId"]));
            if (!objReference.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved..";
            }
            else
            {
                lblResult.Text = objReference.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "Unable to Save";
        }
        radgvDetails.DataBind();        
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objReference.UpdateJobCategory(Convert.ToInt32(hfJobCategoryId.Value), txtJobCategoryCode.Text, txtJobCategoryName.Text, Convert.ToInt32(Session["CompanyId"]));
            if (!objReference.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objReference.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "Unable to Save";
        }
        radgvDetails.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objReference.DeleteJobCategory(Convert.ToInt32(hfJobCategoryId.Value));
        if (!objReference.IsError)
        {
            InitializeControls();
            EnabledDisabled(true);
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "successfully Deleted.";
        }
        else
        {
            lblResult.Text = objReference.ErrorMsg;
        }
        formContainer.Attributes.CssStyle.Add("height", "333px");
        radgvDetails.DataBind();       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        InitializeControls();
        EnabledDisabled(true);
        radgvDetails.DataBind();
        //formContainer.Attributes.CssStyle.Add("height", "333px");
    }

    #endregion

    //#region Grid View

    //protected void radgvDetails_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        try
    //        {
    //            GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
    //            BindData(objReference.GetJobCategory(Convert.ToInt32(dataItem.GetDataKeyValue("JobCategoryID"))));

    //            EnabledDisabled(false);
    //        }
    //        catch
    //        {
    //            if (radgvDetails.SelectedItems.Count <= 0)
    //            {
    //                lblResult.ForeColor = Color.Red;
    //                lblResult.Text = "Select item for modify!";
    //            }
    //        }
    //    }
    //}

    //protected void radgvDetails_ItemCreated(object sender, GridItemEventArgs e)
    //{
    //    #region Grid Permitions

    //    GridItem cmdItem = radgvDetails.MasterTableView.GetItems(GridItemType.CommandItem)[0];
    //    LinkButton lbtnEdit = cmdItem.FindControl("radgrdbtnEditSelected") as LinkButton;
    //    lbtnEdit.Visible = false;

    //    if (Convert.ToBoolean(ViewState["IsModify"]) == true)
    //    {
    //        lbtnEdit.Visible = true;
    //    }
    //    else
    //    {
    //        lbtnEdit.Visible = false;
    //    }

    //    #endregion
    //}

    //#endregion

    protected void lkSelect_Click(object sender, EventArgs e)
    {



        try
        {
            InitializeControls();
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfJobCategoryId.Value = radgvDetails.GetRowValues(index, "JobCategoryID").ToString();
            txtJobCategoryCode.Text = radgvDetails.GetRowValues(index, "JobCategoryCode").ToString();
            txtJobCategoryName.Text = radgvDetails.GetRowValues(index, "JobCategoryName").ToString();


            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            formContainer.Attributes.CssStyle.Add("height", "333px");
        }


        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "";
        }
        formContainer.Attributes.CssStyle.Add("height", "333px");
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
        radgvDetails.Columns[3].Visible = false;
        radgvDetails.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
       
    }
}
