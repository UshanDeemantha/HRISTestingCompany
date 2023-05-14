using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using System.Drawing;
using HRM.Common.BLL;

public partial class Common_Settings_AdminPanel : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "ChangePassword";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            //if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
            //{
            //    Response.Redirect("~/Common/NoPermissions.aspx");
            //    return;
            //}
        }
    }
    protected void backButton_Click(object sender, EventArgs e)
    {

    }
    protected void nextButton_Click(object sender, EventArgs e)
    {

    }
    protected void imgbtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        //string AuthCode = Request.QueryString["AuthCode"].ToString();

       
        TextBox fff = (TextBox)RadPanelBar1.FindItemByValue("AdminLogin").FindControl("TextBox1");
        RadTextBox txtUserName = (RadTextBox)RadPanelBar1.FindItemByValue("AdminLogin").FindControl("radtxtUsername");
        RadTextBox txtPassword = (RadTextBox)RadPanelBar1.FindItemByValue("AdminLogin").FindControl("radtxtPassword");
       // if (Encode.EncodeWithMD5ForAdminPanel(txtUserName.Text, txtPassword.Text) == AuthCode)
        //{
            GoToNextItem(); 
        //}
        
    }


    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void GoToNextItem()
    {
        int selectedIndex = RadPanelBar1.SelectedItem.Index;

        RadPanelBar1.Items[selectedIndex + 1].Selected = true;
        RadPanelBar1.Items[selectedIndex + 1].Expanded = true;
        RadPanelBar1.Items[selectedIndex + 1].Enabled = true;
        RadPanelBar1.Items[selectedIndex].Expanded = false;

    }

    private void GoToPerviousItem()
    {
        int selectedIndex = RadPanelBar1.SelectedItem.Index;

        RadPanelBar1.Items[selectedIndex - 1].Selected = true;
        RadPanelBar1.Items[selectedIndex - 1].Expanded = true;
        RadPanelBar1.Items[selectedIndex - 1].Enabled = true;
        RadPanelBar1.Items[selectedIndex].Expanded = false;

    }
    protected void btnBgrImg2_Click(object sender, EventArgs e)
    {
        GoToNextItem();
    }
    protected void btnBgrImg2_Click1(object sender, EventArgs e)
    {
        
    }
    protected void btnBgrImg2_Click2(object sender, EventArgs e)
    {
        GoToNextItem();
    }
    protected void RadButton2_Click(object sender, EventArgs e)
    {
        GoToPerviousItem();
    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }
 
}