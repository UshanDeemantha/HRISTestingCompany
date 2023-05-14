
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Reference_JobGrade : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "JobGrade";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Reference objReference = new Reference();
    DataTable dtReference = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
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
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfJobGradeId.Value = "0";
        txtJobGradeCode.Text = string.Empty;
        txtJobGrade.Text = string.Empty;
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
            hfJobGradeId.Value = dtTable.Rows[0]["JobGradeID"].ToString();
            txtJobGradeCode.Text = dtTable.Rows[0]["JobGradeCode"].ToString();
            txtJobGrade.Text = dtTable.Rows[0]["JobGrade"].ToString();            
        }
    }

    #endregion
        
    //#region Grid View
    
    //protected void radgvDetails_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "Select")
    //        {
    //            GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
    //            BindData(objReference.GetJobGrade(Convert.ToInt32(dataItem.GetDataKeyValue("JobGradeID"))));
    //            EnabledDisabled(false);
    //        }
    //    }
    //    catch
    //    {
    //        if (radgvDetails.SelectedItems.Count <= 0)
    //        {
    //            lblResult.ForeColor = Color.Red;
    //            lblResult.Text = "Select item for modify!";
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

    #region Buttons
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objReference.AddJobGrade(txtJobGradeCode.Text, txtJobGrade.Text);
            if (!objReference.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
                radgvDetails.DataBind();
            }
            else
            {
                lblResult.Text = objReference.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = ex.Message;
        }

        radgvDetails.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objReference.UpdateJobGrade(Convert.ToInt32(hfJobGradeId.Value), txtJobGradeCode.Text, txtJobGrade.Text);
            if (!objReference.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
                radgvDetails.DataBind();
            }
            else
            {
                lblResult.Text = objReference.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "Unable to Save";
        }
        radgvDetails.DataBind();       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "335px");
        objReference.DeleteJobGrade(Convert.ToInt32(hfJobGradeId.Value));
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
        formContainer.Attributes.CssStyle.Add("height", "335px");
        radgvDetails.DataBind();  
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
        InitializeControls();
        EnabledDisabled(true);
        radgvDetails.DataBind();
        //formContainer.Attributes.CssStyle.Add("height", "335px");
    }
    #endregion  

    protected void lkSelect_Click1(object sender, EventArgs e)
    {

        try
        {
            EnabledDisabled(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;




            hfJobGradeId.Value = radgvDetails.GetRowValues(index, "JobGradeID").ToString();
            txtJobGradeCode.Text = radgvDetails.GetRowValues(index, "JobGradeCode").ToString();
            txtJobGrade.Text = radgvDetails.GetRowValues(index, "JobGrade").ToString();

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
        radgvDetails.Columns[3].Visible = false;
        radgvDetails.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;

    }
}