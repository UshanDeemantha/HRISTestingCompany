
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using DevExpress.Web.Rendering;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

public partial class Reference_JobSpecification : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "JobSpecification";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Reference objReference = new Reference();
    DataTable dtReference = new DataTable();
    Designation objDesignation = new Designation();

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
                    radbtnSave.Visible = false;
                    radbtnUpdate.Visible = false;
                    radbtnDelete.Visible = false;

                    ViewState["IsModify"] = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                        radbtnUpdate.Visible = true;
                        radbtnDelete.Visible = true;

                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        radbtnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);

                        if (radbtnDelete.Visible == true || radbtnUpdate.Visible == true)
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

    private void InitializeControls()
    {
        hfJobSpecificationId.Value = "0";
        txtResponsibilities.Text = string.Empty;
        txtBenifites.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlDesignation.SelectedIndex = -1;
        BindDesignation();
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void EnabledDisabled(bool Enabled)
    {
        radbtnSave.Enabled = Enabled;
        radbtnUpdate.Enabled = !Enabled;
        radbtnDelete.Enabled = !Enabled;
    }

    private void BindDesignation()
    {
        ddlDesignation.DataSource = objDesignation.GetDesignation(0);
        ddlDesignation.TextField = "DesignationName";
        ddlDesignation.ValueField = "DesignationId";
        ddlDesignation.DataBind();
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfJobSpecificationId.Value = dtTable.Rows[0]["JobSpecificationID"].ToString();
            txtResponsibilities.Text = dtTable.Rows[0]["Responsibilities"].ToString();
            txtBenifites.Text = dtTable.Rows[0]["Benifits"].ToString();
            txtRemarks.Text = dtTable.Rows[0]["Remarks"].ToString();
            ddlDesignation.Value = dtTable.Rows[0]["DesignationID"].ToString();
        }
    }

    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objReference.AddJobSpecifiction(Convert.ToInt32(ddlDesignation.Value), txtResponsibilities.Text, txtBenifites.Text, txtRemarks.Text);
            if (!objReference.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objReference.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "555px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "555px");
            lblResult.Text = "Unable to Save";
        }
        radgvDetails.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objReference.UpdateJobSpecifiction(Convert.ToInt32(hfJobSpecificationId.Value), Convert.ToInt32(ddlDesignation.Value), txtResponsibilities.Text, txtBenifites.Text, txtRemarks.Text);
            if (!objReference.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = "Unable to Save";
            }
            formContainer.Attributes.CssStyle.Add("height", "555px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "555px");
            lblResult.Text = "Unable to Save";
        }
        radgvDetails.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objReference.DeleteJobSpecifiction(Convert.ToInt32(hfJobSpecificationId.Value));
        if (!objReference.IsError)
        {
            InitializeControls();
            EnabledDisabled(true);
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objReference.ErrorMsg;
        }
        formContainer.Attributes.CssStyle.Add("height", "555px");
        radgvDetails.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnabledDisabled(true);
        radgvDetails.DataBind();
        //formContainer.Attributes.CssStyle.Add("height", "555px");
    }

    #endregion

    //#region Grid View

    //protected void radgvDetails_ItemCommand(object sender, GridCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        try
    //        {
    //            GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
    //            BindData(objReference.GetJobSpecifiction(Convert.ToInt32(dataItem.GetDataKeyValue("JobSpecificationId"))));
                
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

    //protected void ddlDesignation_PreRender(object sender, EventArgs e)
    //{
    //    //RadComboBoxItem newItem = new RadComboBoxItem("<< Select Designation >>", "0");
    //    //if (!ddlDesignation.Items.Contains(newItem))
    //    //{
    //    //    ddlDesignation.Items.Insert(0, newItem);
    //    //}
    //}
    protected void lkSelect_Click(object sender, EventArgs e)
    {


        try
        {
            EnabledDisabled(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;




            hfJobSpecificationId.Value = radgvDetails.GetRowValues(index, "JobSpecificationId").ToString();

            txtResponsibilities.Text = radgvDetails.GetRowValues(index, "Responsibilities").ToString();
            txtBenifites.Text = radgvDetails.GetRowValues(index, "Benifits").ToString();
            txtRemarks.Text = radgvDetails.GetRowValues(index, "Remarks").ToString();
            //hfDesignationId.Value = radgvDetails.GetRowValues(index, "DesignationId").ToString();
            ddlDesignation.Value = radgvDetails.GetRowValues(index, "DesignationId").ToString();
            ddlDesignation.Text = radgvDetails.GetRowValues(index, "DesignationName").ToString();
            formContainer.Attributes.CssStyle.Add("height", "555px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "555px");
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
        radgvDetails.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;

      //  RadToolTip1.Visible = !RadToolTip1.Visible;
    }

    protected void RadButton1_Click1(object sender, EventArgs e)
    {
        radgvDetails.Columns[6].Visible = false;
        radgvDetails.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
       
    }
}

