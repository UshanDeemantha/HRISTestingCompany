using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class Common_Settings_UserPermissionView : System.Web.UI.Page
{
    #region Security
    private const string ApplicationName = "Common";
    private const string PageName = "UserPermissionView";
    MksSecurity objSecurity = new MksSecurity();
    #endregion

    DataTable dtForms = new DataTable();
    DataTable dtGrid = new DataTable();

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (txtUserRole.Value == null)
        {
            string UserId = "0";
            string ApplicationId = "0";
            string MenuId = "0";
            FillGrid(UserId, ApplicationId, MenuId);
        }
        else
        {
            string UserId = txtUserRole.Value.ToString();
            string ApplicationId = RadComboBox2.SelectedValue.ToString();
            string MenuId = "0";
            FillGrid(UserId, ApplicationId, MenuId);
        }

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
                    btnAspx.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnAspx.Visible = true;
                    }
                    else
                    {
                        btnAspx.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnAspx.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                    }
                }
            }
        }


        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            //InitializeControls();
            //if (txtUserRole.Value == null)
            //{
            //    string UserId = "0";
            //    string ApplicationId = "0";
            //    string MenuId = "0";
            //    FillGrid(UserId, ApplicationId, MenuId);
            //}
            //else
            //{
            //    string UserId = txtUserRole.Value.ToString();
            //    string ApplicationId = RadComboBox2.SelectedValue.ToString();
            //    string MenuId = "0";
            //    FillGrid(UserId, ApplicationId, MenuId);
            //}
            GetPages();
            changeAttPopUp.ShowOnPageLoad = false;
        }
     
        //RadGrid1.DataSource = dtGrid;
        //RadGrid1.DataBind();
    }

    #endregion

    #region Form Methods



    //private void BindData()
    //{

    //    for (var i = 0; i < rdForms.Items.Count; i++)
    //    {
    //        if (chApplication.SelectedIndex >= 0)
    //        {
    //            HRM.AccessRights s = objSecurity.GetUserPermissions(txtUserRole.SelectedItem.Text, chApplication.SelectedItem.Text, rdForms.Items[i].Text);
    //            if (s.ToString() == RbListPermission.SelectedItem.Text)
    //            {
    //                rdForms.Items[i].Selected = true;
    //            }
    //            else
    //            {
    //                rdForms.Items[i].Selected = false;
    //            }
    //        }
    //    }



    //}

    #endregion

    private bool IsValidate()
    {
        /* if (txtUserRole.Value.ToString().Length <= 0)
         {
             lblResult.Text = "Select User To Set Permission!";
             return false;
         }

         if (radcboApplication.SelectedIndex <= 0)
         {
             lblResult.Text = "Select Application To Set Permission!";
             return false;
         }

         if (radcboMenu.SelectedIndex <= 0)
         {
             lblResult.Text = "Select Menu Name!";
             return false;
         }

         if (radcboPermissions.SelectedIndex <= 0)
         {
             lblResult.Text = "Select Permission Type";
             return false;
         }*/
        return true;
    }


    public void InitializeControls()
    {

        for (int i = 0; i < RadComboBox2.Items.Count; i++)
        {
            RadComboBox2.Items[i].Checked = false;
            // formContainer.Attributes.CssStyle.Add("height", "60px");
        }
    }
    #region Buttons
    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        //formContainer.Attributes.CssStyle.Add("height", "400px");
        try
        {
            string UserId = "0";
            string ApplicationId = "0";
            string MenuId = "0";
            if (txtUserRole.Value != null)
            {
                UserId = txtUserRole.Value.ToString();
            }
            if (RadComboBox2.SelectedItem != null)
            {
                ApplicationId = RadComboBox2.SelectedItem.Value.ToString();
            }


            FillGrid(UserId, ApplicationId, MenuId);

        }

        catch
        {
        }
    }


    #endregion

    #region ComboBox Data Binds

    private void GetPages()
    {
        //formContainer.Attributes.CssStyle.Add("height", "400px");
        try
        {
            DataTable dtLoginTypes = new DataTable();
            dtLoginTypes = objSecurity.GetApplication("0");
            RadComboBox2.DataSource = dtLoginTypes;

            RadComboBox2.DataBind();


            DataTable dtPermission = new DataTable();
            dtPermission = objSecurity.GetPermission("0");
            //RbListPermission.DataSource = dtPermission;
            //RbListPermission.TextField = "Permission";
            //RbListPermission.ValueField = "PermissionId";
            //RbListPermission.DataBind();

        }
        catch (Exception ex)
        {


        }

    }






    #endregion

    private void BindDataToCheckBox()
    {
        InitializeControls();
        RadComboBox2.DataBind();
        DataTable dtPermissions = objSecurity.GetUserApplication(txtUserRole.Value.ToString());
        for (int i = 0; i < RadComboBox2.Items.Count; i++)
        {
            for (int j = 0; j < dtPermissions.Rows.Count; j++)
            {
                if (RadComboBox2.Items[i].Value == dtPermissions.Rows[j]["ApplicationId"].ToString())
                {
                    RadComboBox2.Items[i].Checked = true;
                }
            }
        }
        RadGrid1.DataSource = dtPermissions;
        
        RadGrid1.DataBind();
    }
    protected void FillGrid(string UserId, string ApplicationId, string MenuId)

    {
        string menuID = "0";
        string appID = "0";



        for (var i = 0; i < RadComboBox2.Items.Count; i++)
        {
            if (appID.Equals("0"))
                appID = "";
            if (!Equals(RadComboBox2.Items[i].Checked, true)) continue;

            appID += RadComboBox2.Items[i].Value.ToString() + ",";
            //  user.Save_UserRoleList(id, int.Parse(chadminAdd.Items[i].Value.ToString()), true, true, true, 1);
        }
        if (appID.Length > 1)
            appID = appID.Remove(appID.Trim().Length - 1);
        else
            appID = "0";



        dtGrid = objSecurity.ViewUserRights(UserId, appID, MenuId);
        RadGrid1.DataSource = dtGrid;
        RadGrid1.DataBind();
    }
    protected void txtUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindDataToCheckBox();
        formContainer.Attributes.CssStyle.Add("height", "265px");
        //BindData();
    }
    protected void chApplication_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RbListPermission_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindData();
    }
    /* protected void chMenu_SelectedIndexChanged(object sender, EventArgs e)
     {
         try
         {

             for (var i = 0; i < chMenu.Items.Count; i++)
             {
                 if (!Equals(chMenu.Items[i].Selected, true)) continue;


                 //  user.Save_UserRoleList(id, int.Parse(chadminAdd.Items[i].Value.ToString()), true, true, true, 1);
             }


             DataTable dtForms = new DataTable();
             dtForms = objSecurity.GetForm(chMenu.Value.ToString(), "0");
             //rdForms.DataSource = dtForms;
             //rdForms.TextField = "FormName";
             //rdForms.ValueField = "FormId";
             //rdForms.DataBind();

         }
         catch (Exception ex)
         {


         }
         //BindData();
     }*/

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        txtUserRole.Value = string.Empty;
        //chApplication.Check = null;

        FillGrid("0", "0", "0");

    }


    protected void btnAspx_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        try
        {
            string UserId = txtUserRole.Value.ToString();
            string ApplicationId = RadComboBox2.SelectedValue.ToString();
            string MenuId = "0";
            if (txtUserRole.Value != null)
            {
                UserId = txtUserRole.Value.ToString();
            }
            foreach (RadComboBoxItem checkeditem in RadComboBox2.CheckedItems)
            {
                string _value = checkeditem.Value; //looping through each checked item and accessing its value.
            }

            IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.CheckedItems;
            foreach (Telerik.Web.UI.RadComboBoxItem item in itemList)
            {
                ApplicationId = item.Value.ToString();
            }
            //if (chApplication.SelectedItem.Checked != null)
            //{
            //    ApplicationId = chApplication.SelectedValue.ToString();
            //}


            FillGrid(UserId, ApplicationId, MenuId);

        }
        catch (Exception ex)
        {
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        txtUserRole.Value = string.Empty;
        //chApplication.SelectedItem = null;

        cbNone.Checked = false;
        cbView.Checked = false;
        cbAdd.Checked = false;
        cbEdit.Checked = false;
        cbDelete.Checked = false;
        cbFull.Checked = false;

        FillGrid("0", "0", "0");
    }
    protected void cbNone_CheckedChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbNone")).Checked = cbNone.Checked;

        if (cbNone.Checked)
        {
            cbView.Checked = false;
            cbAdd.Checked = false;
            cbEdit.Checked = false;
            cbDelete.Checked = false;
            cbFull.Checked = false;

            for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            {
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbAView")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbAdd")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbEdit")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbDelete")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbFull")).Checked = false;
            }
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }
    protected void cbView_CheckedChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");

        for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbAView")).Checked = cbView.Checked;

        if (cbView.Checked)
        {
            cbNone.Checked = false;
            cbFull.Checked = false;

            for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            {
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbFull")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbNone")).Checked = false;
            }
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");

    }
    protected void cbAdd_CheckedChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbAdd")).Checked = cbAdd.Checked;

        if (cbAdd.Checked)
        {
            cbNone.Checked = false;
            cbFull.Checked = false;

            for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            {
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbFull")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbNone")).Checked = false;
            }
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }
    protected void cbEdit_CheckedChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbEdit")).Checked = cbEdit.Checked;

        if (cbEdit.Checked)
        {
            cbNone.Checked = false;
            cbFull.Checked = false;

            for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            {
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbFull")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbNone")).Checked = false;
            }
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }
    protected void cbDelete_CheckedChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbDelete")).Checked = cbDelete.Checked;

        if (cbDelete.Checked)
        {
            cbNone.Checked = false;
            cbFull.Checked = false;

            for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            {
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbFull")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbNone")).Checked = false;
            }
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }
    protected void cbFull_CheckedChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbFull")).Checked = cbFull.Checked;

        if (cbFull.Checked)
        {
            cbView.Checked = false;
            cbAdd.Checked = false;
            cbEdit.Checked = false;
            cbDelete.Checked = false;
            cbNone.Checked = false;

            for (var i = 0; i < RadGrid1.VisibleRowCount; i++)
            {
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbAView")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbAdd")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbEdit")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbDelete")).Checked = false;
                ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(i, null, "cbNone")).Checked = false;
            }
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool Active = true;
        try
        {

            for (var a = 0; a < RadGrid1.VisibleRowCount; a++)
            {
                var view = ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(a, (GridViewDataColumn)RadGrid1.Columns[7], "cbAView"));
                var add = ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(a, (GridViewDataColumn)RadGrid1.Columns[8], "cbAdd"));
                var edit = ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(a, (GridViewDataColumn)RadGrid1.Columns[9], "cbEdit"));
                var delete = ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(a, (GridViewDataColumn)RadGrid1.Columns[10], "cbDelete"));
                var full = ((ASPxCheckBox)RadGrid1.FindRowCellTemplateControl(a, (GridViewDataColumn)RadGrid1.Columns[11], "cbFull"));

                string from = Convert.ToString(RadGrid1.GetRowValues(a, "FormId").ToString());

                if (Convert.ToBoolean(view.Value))
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "c4a103d8-989e-47ab-a10a-63214a321471",
                        from, Active
                    );
                else
                {
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "c4a103d8-989e-47ab-a10a-63214a321471",
                        from, false
                    );
                }
                if (Convert.ToBoolean(add.Value))
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "5b6ed384-7aaa-4c5c-a32a-da81abe59269",
                        from, Active
                    );
                else
                {
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "5b6ed384-7aaa-4c5c-a32a-da81abe59269",
                        from, false
                    );
                }
                if (Convert.ToBoolean(edit.Value))
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "d4ae40b0-dc81-42ee-ae1e-fe372bbad7a2",
                        from, Active
                    );
                else
                {
                    objSecurity.AddUserPermission(
                       Convert.ToString(txtUserRole.Value.ToString()),
                       "d4ae40b0-dc81-42ee-ae1e-fe372bbad7a2",
                       from, false
                   );
                }
                if (Convert.ToBoolean(delete.Value))
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "11e4ecaf-4f64-4942-9d19-e6b43450b315",
                       from, Active
                    );
                else
                {
                    objSecurity.AddUserPermission(
                       Convert.ToString(txtUserRole.Value.ToString()),
                       "11e4ecaf-4f64-4942-9d19-e6b43450b315",
                      from, false
                   );
                }
                if (Convert.ToBoolean(full.Value))
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "72c909fc-214b-4221-a70f-e7cb737d2c8f",
                        from, Active
                    );
                else
                {
                    objSecurity.AddUserPermission(
                        Convert.ToString(txtUserRole.Value.ToString()),
                        "72c909fc-214b-4221-a70f-e7cb737d2c8f",
                        from, false
                    );
                }
            }
            if (!objSecurity.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved";
                //InitializeControls2();

            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = objSecurity.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "265px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "265px");
            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Some Error Occured!";
        }

    }
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        changeAttPopUp.ShowOnPageLoad = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "265px");
        changeAttPopUp.ShowOnPageLoad = false;
        cbFromUser.Text = "";
        cbToUser.Text = "";
        Label1.Text = "";
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            objSecurity.copyUserPermission(cbFromUser.Value.ToString(), cbToUser.Value.ToString());
            Label1.ForeColor = Color.Green;
            Label1.Text = "Successfully copied!";
        }
        catch
        {
            Label1.ForeColor = Color.Red;
            Label1.Text = "Some Error Occured!";
        }
        formContainer.Attributes.CssStyle.Add("height", "265px");
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
        RadGrid1.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
       // RadToolTip1.Visible = !RadToolTip1.Visible;
    }
}