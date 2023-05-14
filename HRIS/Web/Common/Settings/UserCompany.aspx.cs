using System;
using System.Collections.Generic;
using System.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Common;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using HRM.Common.BLL;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using DevExpress.Web.Rendering;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Common_Settings_UserCompany : System.Web.UI.Page
{
    #region Security
    private const string ApplicationName = "Common";
    private const string PageName = "UserCompanies";
    #endregion
    MksSecurity obj_MksSecurity = new MksSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        ssss.Value = "0";
        #region User Permission
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (IsPostBack == false)
            {
                if (obj_MksSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/Common/NoPermissions.aspx");
                    return;
                }
                else
                {
                    radbtnSave.Visible = false;
                    if (obj_MksSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                    }
                }
            }
        }
        #endregion

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;

        if (!Page.IsPostBack)
        {
            // BindDataToCheckBox();
            InitializeControls();
            EnableDisable(true);

            if (Session["UserTypeId"] != null)
            {
                hfCurrentUserType.Value = Session["UserTypeId"].ToString();
            }
        }
    }

    #region Form Methods
    public void InitializeControls()
    {
        //radcbUser.Text = string.Empty;
        cblCompany.Enabled = true;
        for (int i = 0; i < cblCompany.Items.Count; i++)
        {
            cblCompany.Items[i].Selected = true;
            // formContainer.Attributes.CssStyle.Add("height", "60px");
        }
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    public void EnableDisable(bool IsEnable)
    {
        radbtnSave.Enabled = IsEnable;
    }

    private void BindDataToCheckBox()
    {
        //formContainer.Attributes.CssStyle.Add("height", "247px");

        //if (radcbUser.Value == null)
        //{
        //    radcbUser.Value = 0;
        //    InitializeControls();
        //    DataTable dtPermissions = obj_MksSecurity.GetUserCompany(radcbUser.Value.ToString());
        //    radgvUserCompanies.DataSource = dtPermissions;
        //    radgvUserCompanies.DataBind();
        //}

        cblCompany.Items.Clear();
        cblCompany.DataBind();
        DataTable dtPermissions = obj_MksSecurity.GetUserCompany(radcbUser.Value.ToString());
        for (int i = 0; i < cblCompany.Items.Count; i++)
        {
            for (int j = 0; j < dtPermissions.Rows.Count; j++)
            {
                if (cblCompany.Items[i].Value == dtPermissions.Rows[j]["CompanyId"].ToString())
                {
                    cblCompany.Items[i].Checked = true;
                }
            }
        }

    }
    #endregion

    #region Buttons

    protected void radbtnSave_Click(object sender, EventArgs e)
    {
            for (int i = 0; i < cblCompany.Items.Count; i++)
            {

                try
                {
                    obj_MksSecurity.AddUserCompany(radcbUser.Value.ToString(),
                        Convert.ToInt32(cblCompany.Items[i].Value), cblCompany.Items[i].Checked);
                    if (obj_MksSecurity.IsError)
                    {
                        lblResult.Text = "Error occured! Error No: " + obj_MksSecurity.ErrorNo +
                            "; Error Msg: " + obj_MksSecurity.ErrorMsg;
                    }
                    else
                    {
                        lblResult.ForeColor = Color.Green;
                        lblResult.Text = "Record(s) saved successfully.";
                        EnableDisable(true);
                    }
                    formContainer.Attributes.CssStyle.Add("height", "300px");
                }
                catch (Exception ex)
                {
                    formContainer.Attributes.CssStyle.Add("height", "300px");
                    lblResult.Text = "Some error occured! Error Msg: " + ex.Message;
                }
            }
        radgvUserCompanies.DataBind();
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        cblCompany.Items.Clear();
        cblCompany.DataBind();
        hfUserId.Value = "-99";
        InitializeControls();
        radcbUser.SelectedIndex = -1;
        cblCompany.SelectedIndex = -1;
        EnableDisable(true);
        //formContainer.Attributes.CssStyle.Add("height", "300px");

    }

    protected void radbtnSelectAll_Click1(object sender, EventArgs e)
    {
        for (int j = 0; j < cblCompany.Items.Count; j++)
        {
            cblCompany.Items[j].Selected = true;
        }
        formContainer.Attributes.CssStyle.Add("height", "300px");
    }

    protected void radbtnClearAll_Click1(object sender, EventArgs e)
    {
        for (int k = 0; k < cblCompany.Items.Count; k++)
        {
            cblCompany.Items[k].Selected = false;
        }
        formContainer.Attributes.CssStyle.Add("height", "300px");
    }

    #endregion

    #region Data Binding

    //protected void radcbUser_DataBound(object sender, EventArgs e)
    //{
    //    //RadComboBoxItem item = new RadComboBoxItem("<< Select >>", "0");
    //    //radcbUser.Items.Insert(0, item);
    //}


    #endregion

    protected void radcbUser_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindDataToCheckBox();
    }

    protected void cblCompany_DataBound(object sender, EventArgs e)
    {

        // formContainer.Attributes.CssStyle.Add("height", "400px");
    }
    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            radcbUser.Text = radgvUserCompanies.GetRowValues(index, "UserName").ToString();
            hfUserId.Value = radgvUserCompanies.GetRowValues(index, "UserId").ToString();

            cblCompany.Enabled = true;

            InitializeControls();
            BindDataToCheckBox();
            DataTable dtPermissions = obj_MksSecurity.GetUserCompany(hfUserId.Value.ToString());

            for (int i = 0; i < cblCompany.Items.Count; i++)
            {
                for (int j = 0; j < dtPermissions.Rows.Count; j++)
                { 
                    if (cblCompany.Items[i].Value == dtPermissions.Rows[j]["CompanyId"].ToString())
                    {
                        cblCompany.Items[i].Checked = true;
                        
                    }
                }
            }
            formContainer.Attributes.CssStyle.Add("height", "300px");

        }

        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "300px");
        }

    }

    protected void radcbUser_SelectedIndexChanged1(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "300px");
        BindDataToCheckBox();
        cblCompany.Enabled = true;
    }

    protected void cblCompany_DataBound1(object sender, EventArgs e)
    {
        //if (cblCompany.Items.Count > 0)
        //{
        //    //radbtnAdd.Visible = true;
        //    //radbtnDelete.Visible = true;
        //}
        //else
        //{
        //    //radbtnAdd.Visible = false;
        //    //radbtnDelete.Visible = false;
        //}
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
        radgvUserCompanies.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
        //RadToolTip1.Visible = !RadToolTip1.Visible;

    }

}