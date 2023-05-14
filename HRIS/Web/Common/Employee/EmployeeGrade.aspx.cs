/// <summary>
/// Solution Name        : HRM
/// Project Name          : Common.Web
/// Author                     : ushan deemantha
/// Class Description    : Employment Grade
/// </summary>

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Employee_EmpolyeeGrade : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "EmploymentGrade";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    DataTable dtEmployment = new DataTable();
    Employee objEmployee = new Employee();
    
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
            ViewState["dtEmployment"] = dtEmployment;
        }
    }

    #region Methods

    private void InitializeControls()
    {
        hfEmploymentGradeId.Value = string.Empty;     
        txtEmploymentGradeCode.Text = string.Empty;
        txtEmploymentGradeName.Text = string.Empty;
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
            hfEmploymentGradeId.Value = dtTable.Rows[0]["EmploymentGradeID"].ToString();

            txtEmploymentGradeCode.Text = dtTable.Rows[0]["EmploymentGradeCode"].ToString();
            txtEmploymentGradeName.Text = dtTable.Rows[0]["EmploymentGradeName"].ToString();
        }
    }

    #endregion
    
    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objEmployee.AddEmploymentGrade(txtEmploymentGradeCode.Text, txtEmploymentGradeName.Text);
            if (!objEmployee.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved...";
            }
            else
            {
                lblResult.Text = objEmployee.ErrorMsg;
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objEmployee.UpdateEmploymentGrade(Convert.ToInt32(hfEmploymentGradeId.Value), txtEmploymentGradeCode.Text, txtEmploymentGradeName.Text);
            if (!objEmployee.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
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
        radgvDetails.DataBind();       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objEmployee.DeleteEmploymentGrade(Convert.ToInt32(hfEmploymentGradeId.Value));
        if (!objEmployee.IsError)
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted..";
            InitializeControls();
            EnabledDisabled(true);
        }
        else
        {
             lblResult.Text = objEmployee.ErrorMsg;
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

    #region Grid view

    //protected void radgvDetails_ItemCommand(object sender, GridCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        try
    //    {
    //         GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
    //         BindData(objEmployee.GetEmploymentGrade(Convert.ToInt32(dataItem.GetDataKeyValue("EmploymentGradeID"))));
    //         EnabledDisabled(false);
           
                            
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

    #endregion

    protected void lkSelect_Click(object sender, EventArgs e)
    {



        try
        {
            EnabledDisabled(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfEmploymentGradeId.Value = radgvDetails.GetRowValues(index, "EmploymentGradeID").ToString();
            txtEmploymentGradeCode.Text = radgvDetails.GetRowValues(index, "EmploymentGradeCode").ToString();
            txtEmploymentGradeName.Text = radgvDetails.GetRowValues(index, "EmploymentGradeName").ToString();
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
        // this.RadButton1..ToolTipText = "This is a RadButton";
       
    }
}
