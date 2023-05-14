using System;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Windows.Forms;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Common_Settings_UserPayrollPermission : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "UserPayrollPermission";

    #endregion

    MksSecurity objMksSecurity = new MksSecurity();
    DataTable dtPayrolls = new DataTable();
    DataTable dtPayPermissions = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataBindToCheckBox1();
        #region User Permission

        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (IsPostBack == false)
            {
                if (objMksSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/Common/NoPermissions.aspx");
                    return;
                }
                else
                {
                    radbtnSave.Visible = false;
                    if (objMksSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
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

            InitializeControls();
            lblResult.Text = string.Empty;
            if (Session["UserTypeId"] != null)
            {
                hfCurrentUserType.Value = Session["UserTypeId"].ToString();
            }
        }
    }

    #region Form Methods

    public void DataBindToCheckBox1()
    {

        dtPayPermissions = objMksSecurity.GetUserPayRoll("0", 0);
        radgvDetails.DataSource = dtPayPermissions;
        radgvDetails.DataBind();
    }
    public void InitializeControls()
    {
        formContainer.Attributes.CssStyle.Add("height", "54px");
        for (int i = 0; i < cblPayCategory.Items.Count; i++)
        {
            cblPayCategory.Items[i].Selected = false;
        }
        radcbSelectCompany.Enabled = false;
        cblPayCategory.Enabled = false;
    }

    private void DataBindToCheckBox()
    {
        //cblPayCategory.Visible = true;
        InitializeControls();

        if (radcbSelectCompany.SelectedIndex >= 0)
        {
            dtPayrolls = objMksSecurity.GetPayCategory(Convert.ToInt32(radcbSelectCompany.Value));
            cblPayCategory.DataSource = dtPayrolls;
            cblPayCategory.DataBind();
            if (radcbSelectUser.SelectedIndex >= 0)
            {
                dtPayPermissions = objMksSecurity.GetActiveUserPayRoll(radcbSelectUser.Value.ToString(), Convert.ToInt32(radcbSelectCompany.Value));
                radgvDetails.DataSource = dtPayPermissions;
                radgvDetails.DataBind();

                for (int i = 0; i < cblPayCategory.Items.Count; i++)
                {
                    for (int j = 0; j < dtPayPermissions.Rows.Count; j++)
                    {
                        if (cblPayCategory.Items[i].Value == dtPayPermissions.Rows[j]["PayPeriodCategoryId"].ToString())
                        {
                            cblPayCategory.Items[i].Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            dtPayrolls.Rows.Clear();
            cblPayCategory.DataSource = dtPayrolls;
            cblPayCategory.DataBind();

            dtPayPermissions.Rows.Clear();
            radgvDetails.DataSource = dtPayPermissions;
            radgvDetails.DataBind();
        }
        formContainer.Attributes.CssStyle.Add("height", "395px");
    }

    #endregion

    #region Buttons
    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        if (radcbSelectUser.Text != null)
        {
            for (int i = 0; i < cblPayCategory.Items.Count; i++)
            {
                try
                {
                    objMksSecurity.AddUserPayRoll(radcbSelectUser.Value.ToString(), Convert.ToInt32(radcbSelectCompany.Value), Convert.ToInt32(cblPayCategory.Items[i].Value), cblPayCategory.Items[i].Checked);
                    if (objMksSecurity.IsError)
                    {
                        lblResult.Text = "Error No: " + objMksSecurity.ErrorNo +
                            "; Error Msg: " + objMksSecurity.ErrorMsg;
                    }
                    else
                    {
                        lblResult.ForeColor = Color.Green;
                        lblResult.Text = "Record saved successfully.";
                        DataTable dtPayPermissions = objMksSecurity.GetActiveUserPayRoll(radcbSelectUser.Value.ToString(), Convert.ToInt32(radcbSelectCompany.Value));
                        radgvDetails.DataSource = dtPayPermissions;
                        radgvDetails.DataBind();
                    }
                    formContainer.Attributes.CssStyle.Add("height", "365px");
                }
                catch (Exception ex)
                {
                    formContainer.Attributes.CssStyle.Add("height", "365px");
                    lblResult.Text = "Some error occured! Msg: " + ex.Message;
                }
                formContainer.Attributes.CssStyle.Add("height", "365px");
            }
            formContainer.Attributes.CssStyle.Add("height", "365px");
        }
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        cblPayCategory.Items.Clear();
        radcbSelectUser.SelectedIndex = -1;
        radcbSelectCompany.SelectedIndex = -1; 
    }

    protected void radbtnSelectAll_Click1(object sender, EventArgs e)
    {
        for (int i = 0; i < cblPayCategory.Items.Count; i++)
        {
            cblPayCategory.Items[i].Selected = true;
        }
        formContainer.Attributes.CssStyle.Add("height", "395px");
    }

    protected void radbtnClearAll_Click1(object sender, EventArgs e)
    {
        //formContainer.Attributes.CssStyle.Add("height", "365px");
    }
    #endregion

    #region Drop Down
    protected void radcbSelectUser_DataBound(object sender, EventArgs e)
    {
        //radcbSelectUser.Items.Insert(0, new RadComboBoxItem("<< Select >>", "0"));       
    }

    protected void radcbSelectCompany_DataBound(object sender, EventArgs e)
    {
        //radcbSelectCompany.Items.Insert(0, new RadComboBoxItem("<< Select >>", "0"));    
    }

    protected void radcbSelectUser_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataBindToCheckBox();
    }

    protected void radcbSelectCompany_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataBindToCheckBox();
    }
    #endregion   

    protected void cblPayCategory_DataBound(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "365px");
        if (cblPayCategory.Items.Count > 0)
        {
            //radbtnSelectAll.Visible = true;
            //radbtnClearAll.Visible = true;
            lblPaycategory.Visible = true;
        }
        else
        {
            //radbtnSelectAll.Visible = false;
            //radbtnClearAll.Visible = false;
            lblPaycategory.Visible = true;
        }
    }

    protected void radcbSelectUser_SelectedIndexChanged1(object sender, EventArgs e)
    {

        HiddenField1.Value = radcbSelectUser.Value.ToString();
        DataTable DtTable = objMksSecurity.GetUserCompany(HiddenField1.Value);

        radcbSelectCompany.DataSource = DtTable;
        radcbSelectCompany.SelectedIndex = 0;
        radcbSelectCompany.DataBind();
        DataBindToCheckBox();
        radcbSelectCompany.Enabled = true;
        cblPayCategory.Enabled = true;
        formContainer.Attributes.CssStyle.Add("height", "365px");
    }

    protected void radcbSelectCompany_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataBindToCheckBox();
        radcbSelectCompany.Enabled = true;
        cblPayCategory.Enabled = true;
        formContainer.Attributes.CssStyle.Add("height", "365px");
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
       // RadToolTip1.Visible = !RadToolTip1.Visible;
    }
}