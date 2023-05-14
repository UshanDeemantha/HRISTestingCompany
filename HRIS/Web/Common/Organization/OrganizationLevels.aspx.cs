using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using DevExpress.Web.Rendering;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class Organization_OrganizationLevels : System.Web.UI.Page
{
    DataTable dtTable = new DataTable();
    #region Security
    private const string ApplicationName = "Common";
    private const string PageName = "OrganizationLevels";

    MksSecurity objSecurity = new MksSecurity();
    #endregion

    Organization Companyfrdrop = new Organization();
    Organization objOrganization = new Organization();
    CompanyProfile Getcompany = new CompanyProfile();
    protected void Page_Load(object sender, EventArgs e)
   
    {
       // radgvDetails.DataBind();
        //Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (IsPostBack == false)
            {
                GEtDataIncompany(Convert.ToInt32(Session["CompanyId"]));
                Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));

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

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;
        
        if (!Page.IsPostBack)
        {
            //radgvDetails.DataBind();
            InitializeControls();            
            EnabledDisabled(true);
            if (Session["CompanyId"].ToString() != null && Session["CompanyId"].ToString() != "")
            {
                //   Session["CompanyId"].ToString();


               // radgvDetails.DataBind();
              
            }
            else
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }
        }
    }

    #region Methods
    private void InitializeControls()
    {
        //hfOrgLevelId.Value = "0";       
        radtxtOrganizationLevel.Text = string.Empty;      
        radcboIndex.SelectedIndex =-1;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }    

    private void EnabledDisabled(bool Enabled)
    {
        radcboIndex.Enabled = Enabled;
        btnSave.Enabled = Enabled;
        btnUpdate.Enabled = !Enabled;
        btnDelete.Enabled = !Enabled;
    }

    private void DataBindToControl(int OrganizationalLevelID)
    {
        DataTable dtTable = objOrganization.GetOrganizationLevel(Convert.ToInt32(radcboCompany.Value), OrganizationalLevelID);
        if (dtTable.Rows.Count > 0)
        {
            hfOrgLevelId.Value = dtTable.Rows[0]["OrganizationalLevelID"].ToString();
            radtxtOrganizationLevel.Text = dtTable.Rows[0]["OrganizationalLevel"].ToString();
            //radcboIndex.SelectedValue = dtTable.Rows[0]["OrganizationalIndex"].ToString();
        }
    }

    //private bool IsValidate()
    //{
    //    //if (radcboCompany.SelectedIndex <= 0)
    //    //{
    //    //    lblResult.Text = "Select Company!";
    //    //    return false;
    //    //}
    //    //return true;
    //}
    #endregion

    #region Grid View
    //protected void radgvDetails_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "Select")
    //        {
    //            GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
    //            DataBindToControl(Convert.ToInt32(dataItem.GetDataKeyValue("OrganizationalLevelID")));
    //        }

    //        EnabledDisabled(false);
    //    }
    //    catch
    //    {
    //        if (radgvDetails.SelectedItems.Count <= 0)
    //        {
    //            lblResult.ForeColor = Color.Red;
    //            lblResult.Text = "Select item for modify!";
    //            InitializeControls();
    //            EnabledDisabled(true);
    //        }
    //    }
    //}

    //protected void radgvDetails_ItemCreated(object sender, GridItemEventArgs e)
    //{
    //    #region Grid Permitions
    //    GridItem cmdItem = radgvDetails.MasterTableView.GetItems(GridItemType.CommandItem)[0];
    //    LinkButton lbtnEdit = cmdItem.FindControl("radgrdbtnEditSelected") as LinkButton;
    //    lbtnEdit.Visible = false;

    //    if (Convert.ToBoolean(ViewState["IsModify"]) == true)
    //    {
    //        lbtnEdit.Visible = true;
    //    }
    //    else
    //    {
    //        lbtnEdit.Visible = false;
    //    }

    //    #endregion
    //}
    #endregion

    #region Buttons
    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!IsValidate())
            //{
            //    return;
            //}
            objOrganization.AddOrganizationLevel(Convert.ToInt32(Session["CompanyId"]), radtxtOrganizationLevel.Text, Convert.ToInt32(radcboIndex.Value));
            if (!objOrganization.IsError)
            {
                if (objOrganization.IsSuccess)
                {
                    lblResult.ForeColor = Color.Green;
                    lblResult.Text = "Successfully Entered...";
                   
                    radgvDetails.DataBind();
                    Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));

                    InitializeControls();
                }
                else
                {
                    lblResult.Text = objOrganization.Result;
                }
                formContainer.Attributes.CssStyle.Add("height", "335px");
                radgvDetails.DataBind();
            }
            else
            {
                if (objOrganization.ErrorNo == 2601)
                {
                    lblResult.Text = "Cannot insert duplicate Department Level or Level Index";
                }
                else
                    lblResult.Text = objOrganization.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "335px");
            radgvDetails.DataBind();
        }
        catch(Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "Some Error Occured!";
        }
        radgvDetails.DataBind();
    }

    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!IsValidate())
            //{
            //    return;
            //}
            objOrganization.UpdateOrganizationLevel(Convert.ToInt32(hfOrgLevelId.Value),radtxtOrganizationLevel.Text,Convert.ToInt32(radcboIndex.Value));
            if (!objOrganization.IsError)
            {
                EnabledDisabled(true);
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
                
                
                Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));

                InitializeControls();

            }
            else
            {
                if (objOrganization.ErrorNo == 2601)
                {
                    lblResult.Text = "Cannot insert duplicate Department Level or Level Index";
                }
                else
                    lblResult.Text = objOrganization.ErrorMsg;
            }
            radgvDetails.DataBind();

            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "Some Error Occured!";
        }
        radgvDetails.DataBind();
    }

    protected void radbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objOrganization.DeleteOrganizationLevel(Convert.ToInt32(hfOrgLevelId.Value),Convert.ToInt32(radcboCompany.Value));
            if (!objOrganization.IsError)
            {
                if (objOrganization.IsSuccess)
                {
                    EnabledDisabled(true);
                    lblResult.ForeColor = Color.Green;
                    lblResult.Text = "Successfully Deleted...";
                   
                   
                    Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));

                    InitializeControls();

                }
                else
                {
                    lblResult.Text = objOrganization.Result;
                }
               
            }
            else
            {
                lblResult.Text = objOrganization.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "335px");
            radgvDetails.DataBind();
        }
        catch(Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "Some Error Occured!";
        }
        radgvDetails.DataBind();
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        EnabledDisabled(true);
        //formContainer.Attributes.CssStyle.Add("height", "335px");
    } 
    #endregion

    #region DropDown
    //protected void radcboCompany_DataBound(object sender, EventArgs e)
    //{
    //    radcboCompany.Items.Insert(0, new RadComboBoxItem("<< Select >>", "0"));
    //} 
    
    protected void radcboCompany_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        InitializeControls();
        radgvDetails.DataBind();
        EnabledDisabled(true);
    }
    #endregion

    protected void lkSelect_Click(object sender, EventArgs e)

    {
        try
        {
            EnabledDisabled(false);
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfOrgLevelId.Value = radgvDetails.GetRowValues(index, "OrganizationalLevelID").ToString();
            radtxtOrganizationLevel.Text = radgvDetails.GetRowValues(index, "OrganizationalLevel").ToString();
            radcboIndex.Value = radgvDetails.GetRowValues(index, "OrganizationalIndex").ToString();
            formContainer.Attributes.CssStyle.Add("height", "335px");
        }
        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "335px");
            lblResult.Text = "Cannot Updated!";
        }


    }

    protected void radcboCompany_SelectedIndexChanged1(object sender, EventArgs e)
    {
        InitializeControls();
        radgvDetails.DataBind();
        EnabledDisabled(true);
    }
   

    public void GEtDataIncompany(int CompanyId)
    {

       dtTable =  Getcompany.GetCompanyProfile(CompanyId);

        radcboCompany.Value = dtTable.Rows[0]["CompanyId"].ToString();


    }

    public void Companyfrdrops(int CompanyId)
    {
        if (hfOrgLevelId.Value == "")
        {
            hfOrgLevelId.Value = "0";

            DataTable dtTable = new DataTable();
            dtTable = Companyfrdrop.GetOrganizationLevel(CompanyId, Convert.ToInt32(hfOrgLevelId.Value));


            //  radgvDetails.Fill(dtTable);
            radgvDetails.DataSource = dtTable;
            radgvDetails.DataBind();
        }
        else
        {
            hfOrgLevelId.Value = "0";
            DataTable dtTable = new DataTable();
            dtTable = Companyfrdrop.GetOrganizationLevel(CompanyId, Convert.ToInt32(hfOrgLevelId.Value));


            //  radgvDetails.Fill(dtTable);
            radgvDetails.DataSource = dtTable;
            radgvDetails.DataBind();
        }




    }


    protected void RadButton1_Click(object sender, EventArgs e)
    {
        Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));
        radgvDetails.Columns[7].Visible = false;
        radgvDetails.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
        
    }
}