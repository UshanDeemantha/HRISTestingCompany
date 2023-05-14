using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using DevExpress.Web.Rendering;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

public partial class Designation_Designation : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "DesignationMaster";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Designation objDesignation = new Designation();
    DataTable dtDesignation = new DataTable();

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

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSave.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
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
            ViewState["dtDesignation"] = dtDesignation;
        }
    }

    #region Methods        
    private void InitializeControls()
    {
        hfDesignationId.Value = string.Empty;
        txtDesignationCode.Text = string.Empty;
        txtDesignationName.Text = string.Empty;
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
            hfDesignationId.Value = dtTable.Rows[0]["DesignationID"].ToString();
            txtDesignationCode.Text = dtTable.Rows[0]["DesignationCode"].ToString();
            txtDesignationName.Text = dtTable.Rows[0]["DesignationName"].ToString();
        }
    }

    private void EnabledDisabled(bool Enabled)
    {
        btnSave.Enabled = Enabled;
        btnUpdate.Enabled = !Enabled;
        btnDelete.Enabled = !Enabled;
    }
    #endregion
        
    #region Buttons
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx");
            }
            objDesignation.AddDesignation(txtDesignationCode.Text, txtDesignationName.Text);
            if (!objDesignation.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved...";
            }
            else
            {
                lblResult.Text = objDesignation.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "Unable to save due to unrecoverable error!";
        }
        RadGrid1.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx");
            }

            objDesignation.UpdateDesignation(Convert.ToInt32(hfDesignationId.Value), txtDesignationCode.Text, txtDesignationName.Text);
            if (!objDesignation.IsError)
            {
                InitializeControls();
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
                formContainer.Attributes.CssStyle.Add("height", "333px");
            }
            else
            {
                formContainer.Attributes.CssStyle.Add("height", "333px");
                lblResult.Text = objDesignation.ErrorMsg;
            }
        }

        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "Unable to save due to unrecoverable error!";
        }
       
        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "333px");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx");
        }

        objDesignation.DeleteDesignation(Convert.ToInt32(hfDesignationId.Value));
        if (!objDesignation.IsError)
        {
            InitializeControls();
            EnabledDisabled(true);
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted...";
            formContainer.Attributes.CssStyle.Add("height", "333px");
        }
        else
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = objDesignation.ErrorMsg;
        }
        RadGrid1.DataBind();
        formContainer.Attributes.CssStyle.Add("height", "333px");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnabledDisabled(true);
        //formContainer.Attributes.CssStyle.Add("height", "333px");
    }
    #endregion

    //#region Grid View
    //protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        try
    //        {
    //            dtDesignation = objDesignation.GetDesignation(Convert.ToInt32(RadGrid1.SelectedItems[0].Cells[2].Text));
    //            BindData(dtDesignation);
    //            EnabledDisabled(false);
    //        }
    //        catch
    //        {
    //            if (RadGrid1.SelectedItems.Count <= 0)
    //            {
    //                lblResult.ForeColor = Color.Red;
    //                lblResult.Text = "Select item for modify!";
    //                InitializeControls();
    //                EnabledDisabled(true);
    //            }
    //        }       
    //    }
    //}
    //#endregion

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            EnabledDisabled(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfDesignationId.Value = RadGrid1.GetRowValues(index, "DesignationID").ToString();
            txtDesignationCode.Text = RadGrid1.GetRowValues(index, "DesignationCode").ToString();
            txtDesignationName.Text = RadGrid1.GetRowValues(index, "DesignationName").ToString();

            formContainer.Attributes.CssStyle.Add("height", "333px");

        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "333px");
            lblResult.Text = "Cannot Updated!";
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
