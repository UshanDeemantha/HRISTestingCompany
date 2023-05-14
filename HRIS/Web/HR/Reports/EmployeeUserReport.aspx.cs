using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using OfficeExcel = Microsoft.Office.Interop.Excel;
using DevExpress.Web;
using System.Configuration;
using HRM.Common.BLL;

public partial class HR_Reports_EmployeeUserReport : System.Web.UI.Page
{
    DataTable Data;

    long EmployeeID = 0;
    string EncryptionKey = "007London";

    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeUserReport";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
            {
              // Response.Redirect("~/HR/NoPermissions.aspx");
               // return;
            }
        }

        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        if (!Page.IsPostBack)
        {
            if (Session["dt"] != null)
            {
                Button1.Enabled = true;
                Button2.Enabled = true;
                Button3.Enabled = true;
            }
        }
        

        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dt"] != null)
            {
                UserReport.DataSource = Session["dt"];
                UserReport.DataBind();
                GridExporter.WriteXlsToResponse();
                GridExporter.Styles.Default.Font.Name = "Arial";
                GridExporter.Styles.Default.Font.Size = 20;
            }
            else
            {
                lblResult.Text = "Please Select Data Field(s)";
                lblResult.ForeColor = Color.Red;
            }


        }
        catch (Exception ex)
        {

        }

    }


    private void InitializeFields()
    {
        hfOrganizationStructure.Value = "0";
        Session["dt"] = null;
        //ddlPayPeriod.SelectedValue = null;
        checkboxlist1.UnselectAll();
        CBEmpActive.Checked = true;
        lblResult.Text = string.Empty;
        lblResult.ForeColor = Color.Green;
        UserReport.DataSource = null;
        UserReport.DataBind();
        lblOrganization.Text = string.Empty;
        UserReport.Visible = false;

        List<ListEditItem> RemoveListitem = new List<ListEditItem>();
        foreach (ListEditItem item in checkboxlist1.Items)
        {
            string ColumnCode = item.Value.ToString();
            UserReport.Columns[ColumnCode].Visible = item.Selected;

        }


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        InitializeFields();
    }
    protected void Grid_DataBound(object sender, EventArgs e)
    {


        //Grid.DataSource = Session["dt"];
        //Grid.DataBind();

        List<ListEditItem> RemoveListitem = new List<ListEditItem>();
        foreach (ListEditItem item in checkboxlist1.Items)
        {
            if (item.Selected)
            {
                string ColumnCode = item.Value.ToString();
                UserReport.Columns[ColumnCode].Visible = true;
            }

        }


    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            UserReport.Visible = true;

            Session["dt"] = null;

            if (hfOrganizationStructure.Value != "")
            {

               
                List<ListEditItem> RemoveListitem = new List<ListEditItem>();
                foreach (ListEditItem item in checkboxlist1.Items)
                {
                    if (item.Selected)
                    {

                    }
                    else
                    {
                        string ColumnCode = item.Value.ToString();
                        Data.Columns.Remove(ColumnCode);

                        Session["dt"] = Data;
                        UserReport.DataSource = Session["dt"];
                        UserReport.DataBind();

                    }
                }
            }
            else
            {
                lblResult.Text = "Please Select Organization";
                lblResult.ForeColor = Color.Red;
            }


        }
        catch (Exception ex)
        {
            lblResult.Text = ex.Message;
            lblResult.ForeColor = Color.Red;
        }
    }
    protected void btnOrganizationSelect_Click(object sender, EventArgs e)
    {
        Button1.Enabled = true;
        Button2.Enabled = true;
        Button3.Enabled = true;
    }

}