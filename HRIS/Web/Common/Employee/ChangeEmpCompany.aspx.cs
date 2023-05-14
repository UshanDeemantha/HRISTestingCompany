using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using System.Drawing;
using System.Data;

public partial class Common_Employee_ChangeEmpCompany : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "CompanyProfile";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    bool Validation = true;
    Employee objEmployee = new Employee();
    DataTable dtPayCatagery;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            lblResult.Text = string.Empty;

            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }
            else
            {
                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/Common/NoPermissions.aspx");
                    return;
                }
                else
                {
                    radbtnSave.Visible = false;
                   

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                     
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                      
                    }
                }
            }

            if(radcbCompany.Value != null)
            {
                if(hfPreviousSelectedCompany.Value != radcbCompany.Value.ToString())
                {
                    Session["StartingRecreatingOrg"] = true;
                    Session["StartingRecreatingDesig"] = true;
                    Session["NewCompanyId"] = radcbCompany.Value;
                    hfPreviousSelectedCompany.Value = radcbCompany.Value.ToString();
                }
            }

            if (!Page.IsPostBack)
            {
                InitilizedControl();
            }

            
        }
    }

    private void InitilizedControl()
    {
        hfOrganizationStructure.Value = string.Empty;
        hfDesignationId.Value = string.Empty;
        lblOrganization.Text = string.Empty;
       // lblDesignation.Text = string.Empty;
        ddlEmployee.SelectedIndex = -1;
        ddlEmployee.Text = string.Empty;
        radcbCompany.SelectedIndex = -1;
        radcbCompany.Text = string.Empty;
        raddpFromDate.SelectedDate =  DateTime.Today;
        Session["NewCompanyId"] = null;
        ViewState["IsCreated"] = null;
        Session["IsOk"] = null;
        btnOrganizationSelect.Enabled = false;
        cmbDesignation.Enabled = false;
        ddlCategory.SelectedIndex = -1;
        Label5.Visible = false;
        ddlCategory.Visible = false;
        txtEpfNo.Text = "";
        ddlEmployee.DataBind();
        dxcmbJobCategory.SelectedIndex = -1;
        dxcmbJobCategory.Text = string.Empty;
    }

  
    protected void radcbEmployee_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
       // Session["NewCompanyId"] = 2;
    }
    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            ValidationOne();
            if (Validation)
            {
                int paycatageryId = 0;

                if (ddlCategory.Value != null)
                {
                    paycatageryId = Convert.ToInt32(ddlCategory.Value);
                }

                objEmployee.ChangeEmployeeCompany(Convert.ToInt64(ddlEmployee.Value), Convert.ToInt32(Session["CompanyId"]), Convert.ToInt32(radcbCompany.Value), Convert.ToInt32(hfOrganizationStructure.Value), 
                    Convert.ToInt32(hfDesignationId.Value), Convert.ToDateTime(raddpFromDate.SelectedDate.Value), Convert.ToInt32(dxcmbJobCategory.Value), txtEpfNo.Text, Session["UserName"].ToString());

                if (!objEmployee.IsError)
                {
                    lblResult.ForeColor = Color.Green;
                    lblResult.Text = "Successfully Change..!";
                    InitilizedControl();
                }
                else
                {
                    lblResult.Text = objEmployee.ErrorMsg;
                }
            }
           
           
        }
        catch (Exception ex)
        {
            lblResult.Text = ex.Message;
            lblResult.ForeColor = Color.Red;
        }
    }
    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitilizedControl();
        lblResult.Text = string.Empty;
    }
    protected void btnOrganizationSelect_Click(object sender, EventArgs e)
    {
      
    }

    public void ValidationOne()
    {
        
        if (ddlEmployee.Value == null)
        {
            lblResult.Text = "Please Select Employee";
            lblResult.ForeColor = Color.Red;
            Validation = false;
        }
        if (radcbCompany.Value == null)
        {
            lblResult.Text = "Please Select Company";
            lblResult.ForeColor = Color.Red;
            Validation = false;
        }
        if (hfOrganizationStructure.Value == string.Empty)
        {
            lblResult.Text = "Please Select Department";
            lblResult.ForeColor = Color.Red;
            Validation = false;
        }
        if (hfDesignationId.Value == string.Empty)
        {
            lblResult.Text = "Please Select Designation";
            lblResult.ForeColor = Color.Red;
            Validation = false;
        }
        if ((ddlCategory.Value == string.Empty || ddlCategory.Value == null)  && ddlCategory.Visible==true)
        {
            lblResult.Text = "Please Select New Pay Category";
            lblResult.ForeColor = Color.Red;
            Validation = false;
        }
        if(txtEpfNo.Text != "" && radcbCompany.Value != null)
        {
            DataTable checkEpfCode = objEmployee.CheckEpfCode(txtEpfNo.Text, Convert.ToInt32(radcbCompany.Value));
            if (checkEpfCode.Rows.Count > 0)
            {
                lblResult.Text = "EPF No Already exists";
                lblResult.ForeColor = Color.Red;
                Validation = false;
            }
        }

    }

    protected void radcbCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfOrganizationStructure.Value = string.Empty;
        hfDesignationId.Value = string.Empty;
        lblOrganization.Text = string.Empty;
        //lblDesignation.Text = string.Empty;
        ddlEmployee.SelectedIndex = -1;
        ddlEmployee.Text = string.Empty;
        raddpFromDate.SelectedDate = DateTime.Today;
        //Session["NewCompanyId"] = radcbCompany.Value;
        ViewState["IsCreated"] = false;
        Session["IsOk"] = null;
        btnOrganizationSelect.Enabled = true;
        cmbDesignation.Enabled = true;
        //Session["StartingRecreatingOrg"] = true;
        //Session["StartingRecreatingDesig"] = true;
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IsOk"] = "true";
        //btnOrganizationSelect.Enabled = true;
        //btnDesignationStucture.Enabled = true;
        //DataTable payCatogory = new DataTable();

        //dtPayCatagery = objEmployee.GetCompanyPayCatagery(Convert.ToInt32(radcbCompany.Value));
       
        //if (dtPayCatagery.Rows.Count > 0)
        //{
        //    Label5.Visible = true;
        //    ddlCategory.Visible = true;
        //    ddlCategory.DataSource = dtPayCatagery;
        //    ddlCategory.DataBind();

        //}
       
    }

    protected void cmbDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}