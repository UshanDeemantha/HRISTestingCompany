using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Rendering;
using HRM.Common.BLL;
using HRM.HR.BLL;

public partial class Employee_EmployeeAditionalInfoContol : System.Web.UI.UserControl
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "EmployeeAdditionalInfo";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    EmployeeAdditional objEmloyeeAditional;
    DataTable dtEmployeeAditional;
    EmployeeAdditional objEmloyeeAditionals = new EmployeeAdditional();
    protected void Page_Load(object sender, EventArgs e)
    {
        Convert.ToInt32(Session["CompanyId"].ToString());
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        if (IsPostBack == false)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = false;

            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
            {
                btnSave.Visible = true;
                btnUpdate.Visible = true;
            }
            else
            {
                btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
            }
        }
    }

    public long EmployeeId
    {
        set
        {
            ViewState["EmployeeId"] = value;
            if (ViewState["EmployeeId"] != null)
                GetEmployeeAditional(Convert.ToInt64(ViewState["EmployeeId"]));
        }
    }
  

    private void Initialization()
    {
        try
        {
            objEmloyeeAditional = new EmployeeAdditional();
            objEmloyeeAditional.BloodGroup = ddlBloodGroup.SelectedValue;
            objEmloyeeAditional.RaceID = Convert.ToInt32(ddlRace.SelectedValue);
            objEmloyeeAditional.Province = txtProvince.Text;
            objEmloyeeAditional.District = txtDistrict.Text;
            objEmloyeeAditional.CollerSize = Convert.ToDouble(0);
            objEmloyeeAditional.NationalityID = Convert.ToInt32(ddlNationality.SelectedValue);
            objEmloyeeAditional.ElectoralDistrict = txtElectoralDistrict.Text;
            objEmloyeeAditional.DivisionalSecretariats = txtDivisionalSecretariats.Text;
            objEmloyeeAditional.GSDivision = txtGSDivision.Text;
            objEmloyeeAditional.TransportRoute = txtTransportRoute.Text;
            objEmloyeeAditional.DistanceforPermanentAddress = txtDistanceforPermanentAddress.Text;
            objEmloyeeAditional.CreatedUser = Session["UserName"].ToString();
        }
        catch
        { 
        }
    }

    private void LoadAditianalDetails(DataRow AditionalDetailsRow)
    {
        try
        {
            ddlBloodGroup.SelectedValue = AditionalDetailsRow["BloodGroup"].ToString();
            ddlNationality.SelectedValue = AditionalDetailsRow["NationalityID"].ToString();
            ddlRace.SelectedValue = AditionalDetailsRow["RaceID"].ToString();
            txtProvince.Text = AditionalDetailsRow["Province"].ToString();
            txtDistrict.Text = AditionalDetailsRow["District"].ToString();
            txtElectoralDistrict.Text = AditionalDetailsRow["ElectoralDistrict"].ToString();
            txtDivisionalSecretariats.Text = AditionalDetailsRow["DivisionalSecretariats"].ToString();
            txtGSDivision.Text = AditionalDetailsRow["GSDivision"].ToString();
            txtTransportRoute.Text = AditionalDetailsRow["TransportRoute"].ToString();
            txtDistanceforPermanentAddress.Text = AditionalDetailsRow["DistanceforPermanentAddress"].ToString();
            txtxCollorSize.Text = AditionalDetailsRow["CollerSize"].ToString();
        }
        catch
        {
 
        }
    }

    public void Clear()
    {
        ddlBloodGroup.SelectedIndex =0;
        ddlRace.SelectedIndex = 0;
        //ddlRace.SelectedIndex = 0;
        //txtxpolingDivetion.Text = string.Empty;
        //txtxNearestPoliceStation.Text = string.Empty;
        txtxCollorSize.Text = string.Empty;
        lblResult.Text = string.Empty;
        lblResult.ForeColor = Color.Red;
    }

    private void GetEmployeeAditional(long EmployeeId)
    {
       
        objEmloyeeAditional = new EmployeeAdditional();
        dtEmployeeAditional = objEmloyeeAditional.GetAdditonalInformation(EmployeeId);
        if (dtEmployeeAditional.Rows.Count > 0)
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            LoadAditianalDetails(dtEmployeeAditional.Rows[0]);
        }
        else
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
 
    }


    protected void transportplus_Click(object sender, ImageClickEventArgs e)
    {
        transportpopup.ShowOnPageLoad = true;

    }

    private void AddAditionalInformation(long EmployeeId)
    {
        Initialization();
        objEmloyeeAditional.AddAdditionalInformation(EmployeeId);
        if (!objEmloyeeAditional.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Saved.";
            
        }
        else
        {
            lblResult.Text = objEmloyeeAditional.ErrorMsg;
        }
    }

    private void UpdateAditionalInformation(long EmployeeId)
    {
        Initialization();
        objEmloyeeAditional.UpdateAdditionalInformation(EmployeeId);
        if (!objEmloyeeAditional.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated.";
            
        }
        else
        {
            lblResult.Text = objEmloyeeAditional.ErrorMsg;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddAditionalInformation(Convert.ToInt64(ViewState["EmployeeId"]));
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateAditionalInformation(Convert.ToInt64(ViewState["EmployeeId"]));
    }
    protected void btngsplus_Click(object sender, ImageClickEventArgs e)
    {
        GsDevisionPopup.ShowOnPageLoad = true;
        //lblResult.Enabled = false;
    }
    protected void btngsAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpGsDevision(Convert.ToInt32(Session["CompanyId"].ToString()), txtgscode.Text, txtgsname.Text);
        GsDevisionPopup.ShowOnPageLoad = true;
        gvPopup.DataBind();
        txtGSDivision.DataBind();
        txtgscode.Text = "";
        txtgsname.Text = "";
        btngsupdate.Enabled = false;
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
            //lblResult.Visible = false;
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btngsupdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpGsDevision(Convert.ToInt32(hfgsId.Value), txtgscode.Text, txtgsname.Text);
        GsDevisionPopup.ShowOnPageLoad = true;
        txtGSDivision.DataBind();
        gvPopup.DataBind();
        txtgscode.Text = "";
        txtgsname.Text = "";
        btngsAdd.Enabled = false;

        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }

    }

    protected void btngscancel_Click(object sender, EventArgs e)
    {
        txtgscode.Text = "";
        txtgsname.Text = "";
        GsDevisionPopup.ShowOnPageLoad = false;
    }

    protected void btngsdelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpGsDevision(Convert.ToInt32(hfgsId.Value), txtgscode.Text, txtgsname.Text);
        GsDevisionPopup.ShowOnPageLoad = false;
        txtGSDivision.DataBind();
        txtgscode.Text = "";
        txtgsname.Text = "";
        gvPopup.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
        GsDevisionPopup.ShowOnPageLoad = true;

    }
    protected void lkSelectPopup_Click(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((DevExpress.Web.Rendering.GridViewTableDataRow)(((System.Web.UI.WebControls.LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((DevExpress.Web.Rendering.GridViewTableDataRow)(((System.Web.UI.WebControls.LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfgsId.Value = gvPopup.GetRowValues(index, "GSId").ToString();
            txtgscode.Text = gvPopup.GetRowValues(index, "Code").ToString();
            txtgsname.Text = gvPopup.GetRowValues(index, "Name").ToString();
            btngsAdd.Enabled = false;
            btngsupdate.Enabled = true;
            btngsdelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnelectplus_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ElectPopup.ShowOnPageLoad = true;
    }
    protected void btneladd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpElectoralDistrict(Convert.ToInt32(Session["CompanyId"].ToString()), txtelcode.Text, txteldistrict.Text);
        ElectPopup.ShowOnPageLoad = true;
        gvelectoral.DataBind();
        txtelcode.Text = "";
        txtgsname.Text = "";

        txtElectoralDistrict.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btnelupdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpElectoralDistrict(Convert.ToInt32(hfElectoral.Value), txtelcode.Text, txteldistrict.Text);
        ElectPopup.ShowOnPageLoad = true;
        gvelectoral.DataBind();
        txtelcode.Text = "";
        txteldistrict.Text = "";
        btngsAdd.Enabled = false;
        txtElectoralDistrict.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }

    }

    protected void btnelcancel_Click(object sender, EventArgs e)
    {
        txtelcode.Text = "";
        txteldistrict.Text = "";
        ElectPopup.ShowOnPageLoad = false;
    }

    protected void btneldelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpElectoralDistrict(Convert.ToInt32(hfElectoral.Value));
        ElectPopup.ShowOnPageLoad = true;
        txtelcode.Text = "";
        txteldistrict.Text = "";
        gvPopup.DataBind();
        txtElectoralDistrict.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }

    }

    protected void lkSelectPopupel_Click(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfElectoral.Value = gvelectoral.GetRowValues(index, "ELId").ToString();
            txtelcode.Text = gvelectoral.GetRowValues(index, "Code").ToString();
            txteldistrict.Text = gvelectoral.GetRowValues(index, "Name").ToString();
            btneladd.Enabled = false;
            btnelupdate.Enabled = true;
            btneldelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }
    protected void btndivisionplus_Click(object sender, ImageClickEventArgs e)
    {
        Divisianalpopup.ShowOnPageLoad = true;

    }
    protected void btndvAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpDivisional(Convert.ToInt32(Session["CompanyId"].ToString()), txtdvcode.Text, txtdvname.Text);
        Divisianalpopup.ShowOnPageLoad = true;
        gvdivision.DataBind();
        txtdvcode.Text = "";
        txtdvname.Text = "";
        btndvUpdate.Enabled = false;
        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }
    protected void btndvUpdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpDivisional(Convert.ToInt32(hfDvId.Value), txtdvcode.Text, txtdvname.Text);
        Divisianalpopup.ShowOnPageLoad = true;
        gvdivision.DataBind();
        txtdvcode.Text = "";
        txtdvname.Text = "";

        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }
    protected void btndvCancel_Click(object sender, EventArgs e)
    {
        txtdvcode.Text = "";
        txtdvname.Text = "";
        Divisianalpopup.ShowOnPageLoad = false;
    }
    protected void btndvDelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpDivisional(Convert.ToInt32(hfDvId.Value));
        Divisianalpopup.ShowOnPageLoad = true;
        gvdivision.DataBind();
        txtdvcode.Text = "";
        txtdvname.Text = "";

        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }
    protected void lkSelectPopupel_Click1(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfDvId.Value = gvdivision.GetRowValues(index, "DvId").ToString();
            txtdvcode.Text = gvdivision.GetRowValues(index, "Code").ToString();
            txtdvname.Text = gvdivision.GetRowValues(index, "Name").ToString();
            btndvAdd.Enabled = false;
            btndvUpdate.Enabled = true;
            btndvDelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnvcplus_Click(object sender, ImageClickEventArgs e)
    {
        vaccinepopup.ShowOnPageLoad = true;
    }

    protected void btnvcAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpVacciene(Convert.ToInt32(Session["CompanyId"].ToString()), txtvccode.Text, txtvcname.Text);
        vaccinepopup.ShowOnPageLoad = true;
      
        txtvccode.Text = "";
        txtvcname.Text = "";
        btnvcUpdate.Enabled = false;
        cmbvaccine.DataBind();
        vaccienegv.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btnvcUpdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpVacciene(Convert.ToInt32(hfvcId.Value), txtvccode.Text, txtvcname.Text);
        vaccinepopup.ShowOnPageLoad = true;
        vaccienegv.DataBind();
        txtvccode.Text = "";
        txtvcname.Text = "";

        cmbvaccine.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btnvcCancel_Click(object sender, EventArgs e)
    {
        txtvccode.Text = "";
        txtvcname.Text = "";
        vaccinepopup.ShowOnPageLoad = false;
    }

    protected void btnvcDelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpVacciene(Convert.ToInt32(hfvcId.Value));
        vaccinepopup.ShowOnPageLoad = true;
        vaccienegv.DataBind();
        txtvccode.Text = "";
        txtvcname.Text = "";

        cmbvaccine.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void lkSelectPopupel_Click3(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfvcId.Value = vaccienegv.GetRowValues(index, "VcId").ToString();
            txtvccode.Text = vaccienegv.GetRowValues(index, "Code").ToString();
            txtvcname.Text = vaccienegv.GetRowValues(index, "Name").ToString();
            btnvcAdd.Enabled = false;
            btnvcUpdate.Enabled = true;
            btnvcDelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btntrAdd_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.AddEmpTransportRout(Convert.ToInt32(Session["CompanyId"].ToString()), txtrtnumber.Text, txtrtname.Text);
        transportpopup.ShowOnPageLoad = true;
        transportgv.DataBind();
        txtrtnumber.Text = "";
        txtrtname.Text = "";
        btntrUpdate.Enabled = false;
        txtTransportRoute.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Entered...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btntrUpdate_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.UpdateEmpTransportRout(Convert.ToInt32(hfTrId.Value), txtrtnumber.Text, txtrtname.Text);
        transportpopup.ShowOnPageLoad = true;
        transportgv.DataBind();
        txtrtnumber.Text = "";
        txtrtname.Text = "";

        txtDivisionalSecretariats.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void btntrCancel_Click(object sender, EventArgs e)
    {
        txtrtnumber.Text = "";
        txtrtname.Text = "";
        transportpopup.ShowOnPageLoad = false;
    }

    protected void btntrDelete_Click(object sender, EventArgs e)
    {
        objEmloyeeAditionals.DeleteEmpTransport(Convert.ToInt32(hfTrId.Value));
        Divisianalpopup.ShowOnPageLoad = true;
        transportgv.DataBind();
        txtrtnumber.Text = "";
        txtrtname.Text = "";

        txtTransportRoute.DataBind();
        if (!objEmloyeeAditionals.IsError)
        {

            lblResult.ForeColor = Color.Red;
            lblResult.Text = "Successfully Deleted...";
        }
        else
        {
            lblResult.Text = objEmloyeeAditionals.ErrorMsg;
        }
    }

    protected void lkSelectPopupel_Click2(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
            hfTrId.Value = transportgv.GetRowValues(index, "TrId").ToString();
            txtrtnumber.Text = transportgv.GetRowValues(index, "Number").ToString();
            txtrtname.Text = transportgv.GetRowValues(index, "Name").ToString();
            btntrAdd.Enabled = false;
            btntrUpdate.Enabled = true;
            btntrDelete.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }

}