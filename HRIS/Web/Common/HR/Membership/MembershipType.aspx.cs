using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web.Rendering;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class Membership_MembershipType : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "MembershipTypes";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Membership objMembership = new Membership();
    DataTable dtMembershipType = new DataTable();

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
            ViewState["dtMembershipType"] = dtMembershipType;
        }
    }

    #region Methods
    #region Access Permissions
    
    #endregion
    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfMembershipTypeId.Value = dtTable.Rows[0]["MembershipTypeID"].ToString();
            txtMembershipTypeCode.Text = dtTable.Rows[0]["MembershipTypeCode"].ToString();
            txtMembershipTypeName.Text = dtTable.Rows[0]["MembershipTypeName"].ToString();
            
        }
    }


    private void InitializeControls()
    {
        hfMembershipTypeId.Value = string.Empty;
        txtMembershipTypeCode.Text = string.Empty;
        txtMembershipTypeName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        formContainer.Attributes.CssStyle.Add("height", "54px");

    }
    #endregion
    
    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objMembership.AddMembershipTypes(txtMembershipTypeCode.Text, txtMembershipTypeName.Text);

            if (!objMembership.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
                formContainer.Attributes.CssStyle.Add("height", "335px");
            }
            else
            {
                lblResult.Text = objMembership.ErrorMsg;
                formContainer.Attributes.CssStyle.Add("height", "335px");
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
            objMembership.MemberShipCode = txtMembershipTypeCode.Text;
            objMembership.MemberShipName = txtMembershipTypeName.Text;
            objMembership.UpdateMembershipTypes(Convert.ToInt32(hfMembershipTypeId.Value), true);

            if (!objMembership.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
                formContainer.Attributes.CssStyle.Add("height", "335px");
            }
            else
            {
                lblResult.Text = objMembership.ErrorMsg;
                formContainer.Attributes.CssStyle.Add("height", "335px");
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
        objMembership.DeleteMembershipType(Convert.ToInt32(hfMembershipTypeId.Value));

        if (objMembership.IsError)
        {
            lblResult.Text = objMembership.ErrorMsg;
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
            dtMembershipType= objMembership.GetMembershipType(Convert.ToInt32(e.CommandArgument));
            BindData(dtMembershipType);
            btnSave.Enabled= false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion  
    protected void lkSelect_Click1(object sender, EventArgs e)
    {

        try
        {
            InitializeControls();
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            hfMembershipTypeId.Value = RadGrid1.GetRowValues(index, "MembershipTypeID").ToString();
            txtMembershipTypeCode.Text = RadGrid1.GetRowValues(index, "MembershipTypeCode").ToString();
            txtMembershipTypeName.Text = RadGrid1.GetRowValues(index, "MembershipTypeName").ToString();

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
