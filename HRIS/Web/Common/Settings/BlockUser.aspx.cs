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

public partial class Settings_BlockUser : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "BlockUser";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    #region Page Load

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
                   Response.Redirect("~/Payroll/NoPermissions.aspx");
                   return;
               }
               else
               {
                    radbtnUpdate.Visible = false;
                   if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                   {
                        radbtnUpdate.Visible = true;
                   }
                   else
                   {
                        radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                   }
               }
           }
       }

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;
        if (!IsPostBack)
        {
            cbActive.Checked = true;
            InitializeControls();
            if (Session["UserName"] != null)
            {              
                hfCurrentUser.Value = Session["UserName"].ToString().Trim();
                hfCurrentUserType.Value = Session["UserTypeId"].ToString();

            }
        }
    }
    
    #endregion

    #region Form Methods
    
    public void InitializeControls()
    {
        txtUserName.Text = "";
        cbActive.Checked = false;
        formContainer.Attributes.CssStyle.Add("height", "54px");

    }

    #endregion

    #region Methods

    //protected void radbtnSave_Click(object sender, EventArgs e)
    //{
    //        try
    //        {
    //            bool blockStatus = cbIsBlock.Checked;
    //            objSecurity.BlockUser(txtUserName.Text.ToString(), blockStatus);
    //            if (!objSecurity.IsError)
    //            {
    //                lblResult.ForeColor = Color.Green;
    //                InitializeControls();
    //                if (blockStatus == true)
    //                {
    //                    lblResult.Text = "User Blocked Successfully...";
    //                }
    //                else
    //                { lblResult.Text = "User Unblocked Successfully..."; }
    //            }
    //            else
    //            {                   
    //                lblResult.Text = objSecurity.ErrorMsg;
    //            }
    //            formContainer.Attributes.CssStyle.Add("height", "300px");

    //        }
    //        catch
    //        {
    //            formContainer.Attributes.CssStyle.Add("height", "300px");

    //            lblResult.Text = "Some Error Occured!";
    //        }
    //}

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "265px");

    }

    #endregion

    #region CheckBox Data Bind

    protected void radcbUserId_DataBound(object sender, EventArgs e)
    {
        //RadComboBoxItem newItem = new RadComboBoxItem("<< Select >>", "0");
        //radcbUserId.Items.Insert(0, newItem);
    }
 
    #endregion
    protected void cbIsUnblock_CheckedChanged(object sender, EventArgs e)
    {
        //if (cbIsUnblock.Checked)
        //{
        //    cbIsBlock.Checked = false;
        //}
        formContainer.Attributes.CssStyle.Add("height", "265px");

    }
    protected void cbIsBlock_CheckedChanged(object sender, EventArgs e)
    {
        //if (cbIsBlock.Checked)
        //{
        //    cbIsUnblock.Checked = false;
        //}
        formContainer.Attributes.CssStyle.Add("height", "265px");

    }


    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.BlockUser(txtUserName.Text.ToString(), cbActive.Checked);
            if (!objSecurity.IsError)
            {
                InitializeControls();
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
                formContainer.Attributes.CssStyle.Add("height", "265px");
            }
            else
            {
                lblResult.Text = objSecurity.ErrorMsg;
            }
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "265px");
            lblResult.Text = "Unable to Save";
        }
        radgvDetails.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            InitializeControls();
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            var Active = gridView.GetRowValues(index, "IsBlock").ToString();
            txtUserName.Text = radgvDetails.GetRowValues(index, "UserName").ToString();
            cbActive.Checked = Convert.ToBoolean(Active);



            radbtnUpdate.Enabled = true;
            radbtnCancel.Enabled = true;

            formContainer.Attributes.CssStyle.Add("height", "265px");
        }


        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "265px");
            lblResult.Text = "";
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }

    protected void RadButton1_Click(object sender, EventArgs e)
    {
        radgvDetails.Columns[0].Visible = false;
        radgvDetails.DataBind();
        radgvDetails.Columns[0].Visible = false;
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Century Gothic";
        GridExporter.Styles.Default.Font.Size = 10;
    }
}