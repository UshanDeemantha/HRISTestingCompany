using System;
using System.Drawing;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Settings_SystemUsers : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "SystemUsers";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

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

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                        radbtnUpdate.Visible = true;

                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);

                        ViewState["IsModify"] = true;
                    }
                }
            }
        }

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            if (Session["UserTypeId"] != null)
            {
                hfCurrentUserType.Value = Session["UserTypeId"].ToString();

            }
        }
    }    

    #region Form Methods
    
    public void InitializeControls()
    {
        radtxtUserName.Text = string.Empty;
        radtxtFirstName.Text = string.Empty;
        radtxtLastName.Text = string.Empty;
        radtxtEmail.Text = string.Empty;
        radtxtPassword.Text = string.Empty;
        radcbUserType.Text = string.Empty;
        radbtnUpdate.Enabled = false;
        radbtnSave.Enabled = true;
        radtxtUserName.Enabled = true;
        radtxtUserName.ReadOnly = false;
        radcbUserType.Enabled = true;
        radtxtFirstName.Enabled = true;
        radtxtLastName.Enabled = true;
        radtxtEmail.Enabled = true;
        radcbUserType.SelectedIndex = 1;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    public void EnableDisable(bool IsEnable)
    {
        radbtnSave.Enabled = IsEnable;
        radbtnCancel.Enabled = true;
        radbtnUpdate.Enabled = true;
    }

    #endregion

    #region  Button Methods

    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }

            objSecurity.AddSystemUser(radtxtUserName.Text, radtxtPassword.Text, radtxtEmail.Text, Convert.ToInt32(radcbUserType.Value), radtxtFirstName.Text, radtxtLastName.Text, Session["UserName"].ToString());

            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = objSecurity.Result;
                InitializeControls();
            }
            else
            {
                lblResult.Text = objSecurity.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "595px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "595px");
            lblResult.Text = "Some Error Occured!";
        }
        radgvDetails.DataBind();
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }

            objSecurity.UpdateSystemUser(radtxtUserName.Text.ToString(), radtxtPassword.Text, radtxtEmail.Text, radtxtFirstName.Text, radtxtLastName.Text, Convert.ToInt32(radcbUserType.Value), false, Session["UserName"].ToString());

            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = objSecurity.Result;
                InitializeControls();
            }
            else
            {
                lblResult.Text = objSecurity.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "595px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "595px");
            lblResult.Text = "Some Error Occured!";
        }

        radgvDetails.DataBind();
    }


    protected void radcbUserType_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select >>", "0");
        //radcbUserType.Items.Insert(0, newItem);ccc
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
       // formContainer.Attributes.CssStyle.Add("height", "595px");

    }


    private void BindData(DataTable dtUser)
    {
        radtxtUserName.Text = dtUser.Rows[0]["UserName"].ToString();
        //radtxtPassword.Text = dtUser.Rows[0]["CompanyName"].ToString();
        radcbUserType.Value = dtUser.Rows[0]["UserTypeId"].ToString();
        radtxtEmail.Text = dtUser.Rows[0]["Email"].ToString();
        radtxtFirstName.Text = dtUser.Rows[0]["FirstName"].ToString();
        radtxtLastName.Text = dtUser.Rows[0]["LastName"].ToString();

        radtxtUserName.Enabled = false;
        radcbUserType.Enabled = false;
        radtxtFirstName.Enabled = false;
        radtxtLastName.Enabled = false;
        radtxtEmail.Enabled = false;
        formContainer.Attributes.CssStyle.Add("height", "595px");
    }





   
    #endregion

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            EnableDisable(true);
            InitializeControls();
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            radtxtUserName.Text = radgvDetails.GetRowValues(index, "UserName").ToString();
            radcbUserType.Value = radgvDetails.GetRowValues(index, "UserTypeId").ToString();
            radtxtEmail.Text = radgvDetails.GetRowValues(index, "Email").ToString();
            radtxtFirstName.Text = radgvDetails.GetRowValues(index, "FirstName").ToString();
            radtxtLastName.Text = radgvDetails.GetRowValues(index, "LastName").ToString();
            radtxtUserName.ReadOnly = true;
            radtxtUserName.Enabled = false;
            //radcbUserType.Enabled = false;
            //radtxtFirstName.Enabled = false;
            //radtxtLastName.Enabled = false;
            //radtxtEmail.Enabled = false;
            radbtnSave.Enabled = false;
            radbtnUpdate.Enabled = true;
            formContainer.Attributes.CssStyle.Add("height", "595px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "595px");
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
        RadToolTip1.Visible = !RadToolTip1.Visible;

    }
}