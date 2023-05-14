using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.Common.BLL;


public partial class HR_Employee_ModifiyEmployeeAditionalInfo : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    private long EmployeeId;
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
                    Response.Redirect("~/HR/NoPermissions.aspx");
                    return;
                }

              
            }
        }




        if (!Page.IsPostBack)
        {
            if (Request.QueryString["EmployeeID"] == null)
            {
                EmployeeAditionalInfoWizard.Enabled = false;
               // DataList1.Visible = false;
            }

            try
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    EmployeeId = Convert.ToInt64(Request.QueryString["EmployeeID"]);

                    if (EmployeeId > 0 && EmployeeId != null)
                    {
                        
                        EmployeeAditionalInfoWizard.ActiveStepIndex = 0;
                      
                        //EmployeeAditionalInfoContol1.EmployeeId = Convert.ToInt32(hfEmloyeeId.Value);
                      
                        //EmployeeSearchNew.EmployeeIdContol = hfEmloyeeId;
                        //EmployeeSearchNew.EmployeeAditonalWizad = EmployeeAditionalInfoWizard;
                     
                    }
                    else
                    {
                        EmployeeAditionalInfoWizard.ActiveStepIndex = 0;
                    }

                    ViewState["Status"] = 0;
                }
                else
                {
                    EmployeeAditionalInfoWizard.ActiveStepIndex = 0;
                }
            }
            catch
            { }
        }
        else
        {
           // DataList1.Visible = true;
        }
    }

    protected void tbSearchSupplier_TextChanged(object sender, EventArgs e)
    {
        //if (tbSearchSupplier.Text == string.Empty)
        //{
        //    hfEmloyeeId.Value = "-99";
        //    DataList1.DataBind();
        //    EmployeeAditionalInfoWizard.Enabled = false;
        //    EmployeeAditionalInfoWizard.ActiveStepIndex = 0;
        //}
        //else
        //{
        //    try
        //    {
        //        if (hfEmloyeeId.Value != null && hfEmloyeeId.Value != string.Empty)
        //        {

        //            EmployeeAditionalInfoContol1.EmployeeId = Convert.ToInt32(hfEmloyeeId.Value);
        //        }
        //    }
        //    catch
        //    { }
        //}
    }

    protected void EmployeeAditionalInfoWizard_ActiveStepChanged(object sender, EventArgs e)
    {
        int status = Convert.ToInt32(ViewState["Status"]);

        try
        {
            if (CmbCustomers.Value != null || CmbCustomers.Value != string.Empty)
            {
                switch (EmployeeAditionalInfoWizard.ActiveStep.Title)
                {
                    case "Employee's Details":
                        {
                            NewEmployeeAddContol1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Employee's Contact Details":
                        {
                            EmployeeContactDetailsContol1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Additional Information":
                        {
                            EmployeeAditionalInfoContol1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Qualification Details":
                        {
                            EmployeeQulificationControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Certification Details":
                        {
                            EmployeeCertificationControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Membership Details":
                        {
                            EmoployeeMembershipControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Language Ability Details":
                        {
                            EmployeeFluencyrControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Spouce's Details":
                        {
                            if (status == 0)
                            {
                                EmployeeAditionalInfoWizard.ActiveStepIndex = EmployeeAditionalInfoWizard.ActiveStepIndex;
                                EmployeeSpouseControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            else
                            {
                                EmployeeSpouseControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            break;
                        }
                    //case "Child":
                    case "Children's Details":
                        {
                            if (status == 0)
                            {
                                EmployeeAditionalInfoWizard.ActiveStepIndex = EmployeeAditionalInfoWizard.ActiveStepIndex;
                                EmployeeChildControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            else
                            {
                                EmployeeChildControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            break;
                        }
                    case "Extra Skills Details":
                        {
                            EmployeeSkillsControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Sports Skill Details":
                        {
                            EmoloyeeSportsControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                }
            }
        }
        catch(Exception ex)
        { }
    }

    protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    {
        //tbSearchSupplier.Text = string.Empty;
        //hfEmloyeeId.Value = "-99";
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            if (CmbCustomers.Value != null || CmbCustomers.Value.ToString() != string.Empty)
            {
                EmployeeAditionalInfoContol1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                HiddenField hfStatus = (HiddenField)e.Item.FindControl("hfStatus");
                ViewState["Status"] = hfStatus.Value;
            }
        }
        catch
        { }
    }

    protected void SideBarButton_Click(object sender, EventArgs e)
    {

    }

    protected void SideBarList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        WizardStep dataItem = (WizardStep)e.Item.DataItem;
        if (dataItem.Title == "Children" || dataItem.Title == "Spouse")
        {
            LinkButton lnkBtn = (LinkButton)e.Item.FindControl("SideBarButton");
            int status = Convert.ToInt32(ViewState["Status"]);

            if (status == 0)
            {
                lnkBtn.Visible = false;
            }
            else
            {
                lnkBtn.Visible = true;
            }
        }
    }

    protected void EmployeeAditionalInfoWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        EmployeeAditionalInfoWizard.ActiveStepIndex = 0;

        try
        {
            Label lblStatus = (Label)EmployeeAditionalInfoWizard.ActiveStep.FindControl("lblSelectEmp");
            lblStatus.Text = "Please Select Another Employee";
        }
        catch
        { }
    }

    protected void CmbCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        int status = Convert.ToInt32(ViewState["Status"]);

        try
        {
            if (CmbCustomers.Value != null || CmbCustomers.Value != string.Empty)
            {
                switch (EmployeeAditionalInfoWizard.ActiveStep.Title)
                {
                    case "Employee's Details":
                        {
                            NewEmployeeAddContol1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Employee's Contact Details":
                        {
                            EmployeeContactDetailsContol1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Additional Information":
                        {
                            EmployeeAditionalInfoContol1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Qualification Details":
                        {
                            EmployeeQulificationControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Certification Details":
                        {
                            EmployeeCertificationControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Membership Details":
                        {
                            EmoployeeMembershipControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Language Ability Details":
                        {
                            EmployeeFluencyrControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Spouce's Details":
                        {
                            if (status == 0)
                            {
                                EmployeeAditionalInfoWizard.ActiveStepIndex = EmployeeAditionalInfoWizard.ActiveStepIndex;
                                EmployeeSpouseControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            else
                            {
                                EmployeeSpouseControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            break;
                        }
                    //case "Child":
                    case "Children's Details":
                        {
                            if (status == 0)
                            {
                                EmployeeAditionalInfoWizard.ActiveStepIndex = EmployeeAditionalInfoWizard.ActiveStepIndex;
                                EmployeeChildControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            else
                            {
                                EmployeeChildControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            }
                            break;
                        }
                    case "Extra Skills Details":
                        {
                            EmployeeSkillsControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                    case "Sports Skill Details":
                        {
                            EmoloyeeSportsControl1.EmployeeId = Convert.ToInt32(CmbCustomers.Value);
                            break;
                        }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void CmbCustomers_TextChanged(object sender, EventArgs e)
    {

    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {

    }
}