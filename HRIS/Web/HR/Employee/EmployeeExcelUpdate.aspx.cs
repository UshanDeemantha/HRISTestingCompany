using DevExpress.Web;
using HRM.Common.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_Employee_EmployeeExcelUpdate : System.Web.UI.Page
{
    public int namesCounter;
    DataTable dtSelectedSeats = new DataTable();
    Employee objEmployee = new Employee();
    DataTable dtLog;
    DataTable dtDepartment;
    DataTable dtDesignation;
    Reference objComReference = new Reference();
    CompanyProfile objCompanyProfile = new CompanyProfile();
    Organization objOrganization = new Organization();
    Designation objDesignation = new Designation();
    DataTable GetTimeSetup;
    MksSecurity objSecurity = new MksSecurity();
    private enum LogEntryLevel
    {
        Message = 1,
        Error = 2,
        Warning = 3,
        Success = 4
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {

        }

        }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {

        if (ViewState["IsUpdateSelected"] == null && ViewState["IsExecute"] == null)
        {
            GetEmployeesInOrNotInTime("Check");
        }
        ViewState["IsExecute"] = null;

    }
    private void GetEmployeesInOrNotInTime(string Type)
    {
        int jobCatID = 0;
        if (hfOrganizationStructure.Value == "")
            hfOrganizationStructure.Value = "0";


        DataTable dataTable = objEmployee.GetEmployeesInExcelUpdate(Convert.ToInt32(Session["CompanyId"]), Convert.ToInt32(hfOrganizationStructure.Value), Convert.ToInt32(cmbDesignation.Value));
        RadComboBox2.DataSource = dataTable;
        RadComboBox2.DataBind();
        RadComboBox2.Enabled = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow _drProcessedNewRow;
            string catList = "";
            string catList1 = "";
            List<ListEditItem> RemoveListitem = new List<ListEditItem>();
            IList<Telerik.Web.UI.RadComboBoxItem> itemList = RadComboBox2.CheckedItems;
            IList<Telerik.Web.UI.RadComboBoxItem> itemList1 = cmbempColumn.CheckedItems;

            foreach (Telerik.Web.UI.RadComboBoxItem item in cmbempColumn.Items)
            {
                if (item.Text == "EmployeeCode" || item.Text == "Company Name")
                {
                    item.Checked = true;
                }
            
            }

            foreach (Telerik.Web.UI.RadComboBoxItem item in RadComboBox2.CheckedItems)
            {
                    catList = catList + item.Value.ToString() + ",";

            }
            catList = catList.TrimEnd(',');
            foreach (Telerik.Web.UI.RadComboBoxItem item1 in cmbempColumn.CheckedItems)
            {
                 catList1 = catList1 + item1.Value.ToString() + ",";

            }
            catList1 = catList1.TrimEnd(',');
            DataTable dataTable = objEmployee.SearchEmployee(catList, catList1, "007London");

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {

                string employeeCode = string.Empty;
                string companyID = string.Empty;
                string orgstrucId = string.Empty;
                string orgstructypeId = string.Empty;
                string DesignationId = string.Empty;
                string JobcatId = string.Empty;
                string JobgradeId = string.Empty;
                string EmploymentTypeID = string.Empty;
                string EmploymentGradeID = string.Empty;
                _drProcessedNewRow = dataTable.Rows[k];
                foreach (Telerik.Web.UI.RadComboBoxItem item1 in cmbempColumn.CheckedItems)
                {
                    if (item1.Text == "Company Name")
                    {                     
                            companyID = _drProcessedNewRow["CompanyID"].ToString().Trim();
                            DataTable dtCompany = objCompanyProfile.GetEmployeeCompanyDetails(Convert.ToInt32(companyID.ToString()));
                            if (dataTable.Columns.Contains("CompanyName"))
                            {
                                _drProcessedNewRow["CompanyName"] = dtCompany.DefaultView[0]["CompanyName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("CompanyName", typeof(string));
                                _drProcessedNewRow["CompanyName"] = dtCompany.DefaultView[0]["CompanyName"].ToString();
                            }
                    }
                    if (item1.Text == "BranchCategory")
                    {
                     
                            orgstrucId = _drProcessedNewRow["OrgStructureID"].ToString().Trim();
                            DataTable dtParentDepartment = objOrganization.GetParentOrganizationTypesforsearch(Convert.ToInt32(dxPopcompany.Value), Convert.ToInt32(orgstrucId));
                            if (dataTable.Columns.Contains("BranchCategory"))
                            {
                                _drProcessedNewRow["OrgStructureID"] = Convert.ToInt32(dtParentDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                                _drProcessedNewRow["BranchCategory"] = dtParentDepartment.DefaultView[0]["OrganizationTypeName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("BranchCategory", typeof(string));
                                _drProcessedNewRow["OrgStructureID"] = Convert.ToInt32(dtParentDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                                _drProcessedNewRow["BranchCategory"] = dtParentDepartment.DefaultView[0]["OrganizationTypeName"].ToString();
                            }
                        
                    }
                    if (item1.Text == "Department")
                    {
                        
                            DataTable dtParentDepartment = objOrganization.GetParentOrganizationTypes(Convert.ToInt32(dxPopcompany.Value), 0);
                            _drProcessedNewRow["OrgStructureID"] = Convert.ToInt32(dtParentDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                            orgstructypeId = _drProcessedNewRow["OrgStructureID"].ToString().Trim();
                            dtDepartment = objOrganization.GetOrganizationTypesExcel(Convert.ToInt32(Session["CompanyId"]), Convert.ToInt32(orgstructypeId));


                            if (dataTable.Columns.Contains("Department"))
                            {
                                _drProcessedNewRow["OrgStructureID"] = Convert.ToInt32(dtDepartment.DefaultView[0]["OrgStructureID"].ToString());
                                _drProcessedNewRow["Department"] = dtDepartment.DefaultView[0]["OrganizationTypeName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("Department", typeof(string));
                                _drProcessedNewRow["OrgStructureID"] = Convert.ToInt32(dtDepartment.DefaultView[0]["OrgStructureID"].ToString());
                                _drProcessedNewRow["Department"] = dtDepartment.DefaultView[0]["OrganizationTypeName"].ToString();
                            }
                    }
                    if (item1.Text == "EmployeeDesignation")
                    {
                        
                            DesignationId = _drProcessedNewRow["DesignationID"].ToString().Trim();
                            dtDesignation = objDesignation.GetDesignationTypesExcel(Convert.ToInt32(DesignationId));

                            if (dataTable.Columns.Contains("EmployeeDesignation"))
                            {
                                _drProcessedNewRow["EmployeeDesignation"] = dtDesignation.DefaultView[0]["DesignationName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("EmployeeDesignation", typeof(string));
                                _drProcessedNewRow["EmployeeDesignation"] = dtDesignation.DefaultView[0]["DesignationName"].ToString();
                            }
                    }
                    if (item1.Text == "JobCategory")
                    {
                       
                            JobcatId = _drProcessedNewRow["JobCategoryID"].ToString().Trim();
                            DataTable dtJobcategory = objComReference.GetJobCategory(Convert.ToInt32(JobcatId));
                            if (dataTable.Columns.Contains("JobCategory"))
                            {
                                _drProcessedNewRow["JobCategory"] = dtJobcategory.DefaultView[0]["JobCategoryName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("JobCategory", typeof(string));
                                _drProcessedNewRow["JobCategory"] = dtJobcategory.DefaultView[0]["JobCategoryName"].ToString();
                            }
                    }
                    if (item1.Text == "JobGrade")
                    {
                         JobgradeId = _drProcessedNewRow["JobGradeID"].ToString().Trim();
                            DataTable dtJobGrade = objComReference.GetJobGrade(Convert.ToInt32(JobgradeId));
                            if (dataTable.Columns.Contains("JobGrade"))
                            {
                                _drProcessedNewRow["JobGrade"] = dtJobGrade.DefaultView[0]["JobGrade"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("JobGrade", typeof(string));
                                _drProcessedNewRow["JobGrade"] = dtJobGrade.DefaultView[0]["JobGrade"].ToString();
                            }
                    }
                    if (item1.Text == "EmploymentType")
                    {
                        
                            EmploymentTypeID = _drProcessedNewRow["EmploymentTypeID"].ToString().Trim();
                            DataTable dtEmpTypes = objEmployee.GetEmploymentType(Convert.ToInt32(EmploymentTypeID));
                            if (dataTable.Columns.Contains("EmploymentType"))
                            {
                                _drProcessedNewRow["EmploymentType"] = dtEmpTypes.DefaultView[0]["EmploymentTypeName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("EmploymentType", typeof(string));
                                _drProcessedNewRow["EmploymentType"] = dtEmpTypes.DefaultView[0]["EmploymentTypeName"].ToString();
                            }
                    }
                    if (item1.Text == "EmploymentGrade")
                    {
                        
                            EmploymentGradeID = _drProcessedNewRow["EmploymentGradeID"].ToString().Trim();
                            DataTable dtEmpGrade = objEmployee.GetEmploymentGrade(Convert.ToInt32(EmploymentGradeID));
                            if (dataTable.Columns.Contains("EmploymentGrade"))
                            {
                                _drProcessedNewRow["EmploymentGrade"] = dtEmpGrade.DefaultView[0]["EmploymentGradeName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("EmploymentGrade", typeof(string));
                                _drProcessedNewRow["EmploymentGrade"] = dtEmpGrade.DefaultView[0]["EmploymentGradeName"].ToString();
                            }
                    }
                    if (item1.Text == "CostCenter")
                    {
                       
                            companyID = _drProcessedNewRow["CompanyID"].ToString().Trim();
                            DataTable dtCompany = objCompanyProfile.GetEmployeeCompanyDetails(Convert.ToInt32(companyID.ToString()));
                            if (dataTable.Columns.Contains("CostCenterName"))
                            {
                                _drProcessedNewRow["CostCenterName"] = dtCompany.DefaultView[0]["CompanyName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("CostCenterName", typeof(string));
                                _drProcessedNewRow["CostCenterName"] = dtCompany.DefaultView[0]["CompanyName"].ToString();
                            }
                    }
                    if (item1.Text == "EPFCostCenter")
                    {
                        
                            companyID = _drProcessedNewRow["CompanyID"].ToString().Trim();
                            DataTable dtCompany = objCompanyProfile.GetEmployeeCompanyDetails(Convert.ToInt32(companyID.ToString()));
                            if (dataTable.Columns.Contains("EPFCostCenterName"))
                            {
                                _drProcessedNewRow["EPFCostCenterName"] = dtCompany.DefaultView[0]["CompanyName"].ToString();
                            }
                            else
                            {
                                dataTable.Columns.Add("EPFCostCenterName", typeof(string));
                                _drProcessedNewRow["EPFCostCenterName"] = dtCompany.DefaultView[0]["CompanyName"].ToString();
                            }
                    }
                }


            }
            if (dataTable.Columns.Contains("CompanyID"))
            {
                dataTable.Columns.Remove("CompanyID");
            }
            if (dataTable.Columns.Contains("OrgStructureID"))
            {
                dataTable.Columns.Remove("OrgStructureID");

            }
            if (dataTable.Columns.Contains("OrgStructureID1"))
            {
                dataTable.Columns.Remove("OrgStructureID1");

            }
            if (dataTable.Columns.Contains("DesignationID"))
            {
                dataTable.Columns.Remove("DesignationID");
            }
            if (dataTable.Columns.Contains("JobGradeID"))
            {
                dataTable.Columns.Remove("JobGradeID");
            }
            if (dataTable.Columns.Contains("JobCategoryID"))
            {
                dataTable.Columns.Remove("JobCategoryID");
            }
            if (dataTable.Columns.Contains("EmploymentTypeID"))
            {
                dataTable.Columns.Remove("EmploymentTypeID");
            }
            if (dataTable.Columns.Contains("EmploymentGradeID"))
            {
                dataTable.Columns.Remove("EmploymentGradeID");
            }
            if (dataTable.Columns.Contains("CostCenter"))
            {
                dataTable.Columns.Remove("CostCenter");
            }

            if (dataTable.Columns.Contains("CostCenterName"))
            {
                dataTable.Columns["CostCenterName"].ColumnName = "CostCenter";
            }
            if (dataTable.Columns.Contains("EPFCostCenter"))
            {
                dataTable.Columns.Remove("EPFCostCenter");
            }

            if (dataTable.Columns.Contains("EPFCostCenterName"))
            {
                dataTable.Columns["EPFCostCenterName"].ColumnName = "EPFCostCenter";
            }

            foreach (var column in EMPGRID.DataColumns)
            {
                column.Caption = column.FieldName;
                column.Name = column.FieldName;
            }

            if (dataTable.Rows.Count > 0)
            {
                Session["SearchEmp"] = dataTable;
                NullGrid();
                EMPGRID.DataSource = dataTable;
                EMPGRID.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }

    public void NullGrid()
    {
        EMPGRID.Columns.Clear();
        EMPGRID.AutoGenerateColumns = true;
        EMPGRID.DataSource = null;
        EMPGRID.DataBind();
    }
    //protected void lkSelect_Click(object sender, EventArgs e)
    //{

    //}



    protected void RadButton1_Click(object sender, EventArgs e)
    {

        EMPGRID.DataSource = (DataTable)Session["SearchEmp"];
        EMPGRID.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Arial";
        GridExporter.Styles.Default.Font.Size = 20;
    }

    protected void cmbDesignation_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void cmbDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void dxPopcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  formContainer.Attributes.CssStyle.Add("height", "325px");
    }

    protected void radUploadEmpExcel_FileExists(object sender, Telerik.Web.UI.Upload.UploadedFileEventArgs e)
    {

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }

    protected void btnPopupCancel_Click(object sender, EventArgs e)
    {

    }
    private void CreateLogEntry(LogEntryLevel logLevel, string MessageText)
    {
        try
        {
            string ImageURL = string.Empty;

            if (ViewState["ErrorLog"] != null)
            {
                if (dtLog.Rows.Count > 0)
                {
                    dtLog = (DataTable)ViewState["ErrorLog"];
                }
                else
                {
                    dtLog = null;
                }
            }

            if (dtLog == null)
            {
                dtLog = new DataTable();

                dtLog.Columns.Add("EventImageURL");
                dtLog.Columns.Add("EventType");
                dtLog.Columns.Add("EventTime");
                dtLog.Columns.Add("EventMessage");
            }

            switch (logLevel)
            {
                case LogEntryLevel.Message:
                    {
                        ImageURL = "~\\App_Themes\\CommonResources\\Log\\Log-Info.png";
                        break;
                    }
                case LogEntryLevel.Error:
                    {
                        ImageURL = "~\\App_Themes\\CommonResources\\Log\\Log-Error.png";
                        break;
                    }
                case LogEntryLevel.Warning:
                    {
                        ImageURL = "~\\App_Themes\\CommonResources\\Log\\Log-Warning.png";
                        break;
                    }
                case LogEntryLevel.Success:
                    {
                        ImageURL = "~\\App_Themes\\CommonResources\\Log\\Log-Success.png";
                        break;
                    }
                default:
                    {
                        ImageURL = "~\\App_Themes\\CommonResources\\Log\\Log-Info.png";
                        break;
                    }
            }

            DataRow drTemp = dtLog.NewRow();
            drTemp["EventImageURL"] = ImageURL;
            drTemp["EventType"] = logLevel.ToString();
            drTemp["EventTime"] = DateTime.Now.ToString();
            drTemp["EventMessage"] = MessageText;
            dtLog.Rows.Add(drTemp);

            ViewState["ErrorLog"] = dtLog;

            radGridEvents.DataSource = dtLog;
            radGridEvents.DataBind();
        }
        catch { }
    }

    private void LoadExcelFile(string targetFolder)
    {
        try
        {

            string fullPath = string.Empty;
            DataTable dtFileData = new DataTable();
            dtFileData = ReadExcelData(Directory.GetFiles(Server.MapPath(targetFolder))[0]);

            if (dtFileData != null)
            {
                ValidateLoadedData(dtFileData);
            }

            foreach (var filesInTempDir in Directory.GetFiles(Server.MapPath(targetFolder)))
            {
                try
                {
                    File.Delete(filesInTempDir);
                }
                catch
                {
                    CreateLogEntry(LogEntryLevel.Error, "Could not delete all files in temporary directory! [File deletion failed for: " + filesInTempDir + "]");
                }
            }
        }
        catch (Exception ex)
        {
            CreateLogEntry(LogEntryLevel.Error, ex.Message);
        }
    }


    private void ValidateLoadedData(DataTable dtExcelData)
    {
        //DataTable dtExcelData = new DataTable();
        DataRow drTemp;

        int SearchReplaceInstanceCount = 0;
        decimal tempDecimal = 0;

        List<string> _duplicateEmpCodes = new List<string>();
        List<string> _duplicateNICNumbers = new List<string>();
        List<string> _duplicateEPFNumbers = new List<string>();
        try
        {
            // variables to store temp data...
            string pattern = @"\b(\w|['-])+\b";
            string employeeCode = string.Empty;
            string LogHeading = string.Empty;

            DateTime tempDate = new DateTime();
            //decimal tempDecimal = 0;
            DataRow _drProcessedRow;

            #region Data for Verifying Uploaded Data


            DataTable dtCompany = objCompanyProfile.GetCompanyProfile(0);
            DataTable dtCompanyCostCenter = objCompanyProfile.GetCompanyCostCenters(0);
            DataTable dtepfCompanyCostCenter = objCompanyProfile.GetCompanyCostCenters(0);
            DataTable dtJobCategory = objComReference.GetJobCategory(0);
            DataTable dtJobGrade = objComReference.GetJobGrade(0);
            DataTable dtEmpTypes = objEmployee.GetEmploymentType(0);

            // DataTable dtBranch = objComReference.GetBranch(0, Convert.ToInt32(Session["CompanyId"]));
            DataTable dtEmpGrade = objEmployee.GetEmploymentGrade(0);
            DataTable dtReligion = objEmployee.GetReligion();
            DataTable dtNationality = objEmployee.GetNaionality();



            #endregion

            #region Find Duplicate 'Employee Numbers' & 'NIC's' within datatable

            foreach (DataRow _drItem in dtExcelData.Rows)
            {
                if (dtExcelData.Columns.Contains("EmployeeCode"))
                {
                    if (_duplicateEmpCodes.FindIndex(s => s == _drItem["EmployeeCode"].ToString().Trim()) < 0)
                    {

                        //string b = _drItem["F1"].ToString().Trim();
                        dtExcelData.DefaultView.RowFilter = null;
                        //string a = "EmployeeCode = '" + _drItem[0].ToString().Trim() + "'";
                        dtExcelData.DefaultView.RowFilter = "EmployeeCode = '" + _drItem["EmployeeCode"].ToString().Trim() + "'";
                        // dtExcelData.DefaultView.RowFilter = a;
                        if (dtExcelData.DefaultView.Count > 1)
                        {
                            _duplicateEmpCodes.Add(_drItem["EmployeeCode"].ToString().Trim());
                        }
                    }
                }




                if (dtExcelData.Columns.Contains("NIC"))
                {
                    if (dtExcelData.Columns.Contains("NIC"))
                    {
                        if (_duplicateNICNumbers.FindIndex(s => s == _drItem["NIC"].ToString().Trim()) < 0)
                        {
                            dtExcelData.DefaultView.RowFilter = null;
                            dtExcelData.DefaultView.RowFilter = "NIC = '" + _drItem["NIC"].ToString().Trim() + "'";
                            if (dtExcelData.DefaultView.Count > 1)
                            {
                                _duplicateNICNumbers.Add(_drItem["NIC"].ToString().Trim());
                            }
                        }
                    }
                }

                if (dtExcelData.Columns.Contains("EPFNo"))
                {
                    if (_duplicateEPFNumbers.FindIndex(s => s == _drItem["EPFNo"].ToString().Trim()) < 0)
                    {
                        dtExcelData.DefaultView.RowFilter = null;
                        //  string a = "EPFNo = '" + _drItem[31].ToString().Trim() + "'";
                        if (_drItem["EPFNo"].ToString().Trim().Length > 0)
                        {
                            dtExcelData.DefaultView.RowFilter = "EPFNo = '" + _drItem["EPFNo"].ToString().Trim() + "'";
                            if (dtExcelData.DefaultView.Count > 1)
                            {
                                _duplicateEPFNumbers.Add(_drItem["EPFNo"].ToString().Trim());
                            }

                        }

                    }
                }
            }



            #endregion


            bool isCurrentEmpSkipped = false;
            bool isUpload = true;

            for (int k = 0; k < dtExcelData.Rows.Count; k++)
            {
                string EmployeeId = string.Empty;
                bool isExistEmployee = false;
                isCurrentEmpSkipped = false;
                //isUpload = true;
                _drProcessedRow = dtExcelData.Rows[k];
                employeeCode = _drProcessedRow["EmployeeCode"].ToString().Trim();
                DataTable dtEmpId = objEmployee.GetEmploymeeIdforexcelupdate(Convert.ToInt32(employeeCode));
                if (dtExcelData.Columns.Contains("EmployeeId"))
                {

                    dtExcelData.DefaultView.RowFilter = null;
                    //EmployeeId = _drProcessedRow["OrgStructureID"].ToString().Trim();
                    _drProcessedRow["EmployeeId"] = dtEmpId.DefaultView[0]["EmployeeId"].ToString();
                }
                else
                {
                    dtExcelData.Columns.Add("EmployeeId", typeof(string));
                    dtExcelData.DefaultView.RowFilter = null;
                    //EmployeeId = _drProcessedRow["OrgStructureID"].ToString().Trim();
                    _drProcessedRow["EmployeeId"] = dtEmpId.DefaultView[0]["EmployeeId"].ToString();
                }


                if (employeeCode != string.Empty)
                {
                    LogHeading = "[Line " + Convert.ToString(k + 1) + " | Employee code: " + employeeCode + "]";

                    if (_duplicateEmpCodes.FindIndex(s => s == employeeCode) >= 0)
                    {
                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Employee code is duplicated within uploaded file & this is not allowed!, record is skipped from processing!");
                        //isCurrentEmpSkipped = true;
                        isUpload = false;
                    }

                    #region Date Verifications

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("DateOfBirth"))
                        {
                            if (_drProcessedRow["DateOfBirth"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'DateOfBirth' mandatory posting; record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                            else
                            {
                                if (DateTime.TryParse(_drProcessedRow["DateOfBirth"].ToString().Trim(), out tempDate) == false)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'DateOfBirth' mandatory posting is in invalid format; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("RecruitementDate"))
                        {
                            if (_drProcessedRow["RecruitementDate"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'RecruitementDate' mandatory posting; record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                            else
                            {
                                if (DateTime.TryParse(_drProcessedRow["RecruitementDate"].ToString().Trim(), out tempDate) == false)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'RecruitementDate' mandatory posting is in invalid format; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EPFEntitlementDate"))
                        {
                            if (_drProcessedRow["EPFEntitlementDate"].ToString().Trim() != string.Empty)
                            {
                                if (DateTime.TryParse(_drProcessedRow["EPFEntitlementDate"].ToString().Trim(), out tempDate) == false)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'EPF Entitle Date' mandatory posting is in invalid format; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("SecondRecruitmentDate"))
                        {
                            if (_drProcessedRow["SecondRecruitmentDate"].ToString().Trim() != string.Empty)
                            {
                                if (DateTime.TryParse(_drProcessedRow["SecondRecruitmentDate"].ToString().Trim(), out tempDate) == false)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'SecondRecruitmentDate' posting is in invalid format; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("ResignDate"))
                        {
                            if (_drProcessedRow.Table.Columns.Contains("ResignDate") && _drProcessedRow["ResignDate"].ToString().Trim() != string.Empty)
                            {
                                if (DateTime.TryParse(_drProcessedRow["ResignDate"].ToString().Trim(), out tempDate) == false)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'ResignDate' posting is in invalid format; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    #endregion

                    #region Database Checks

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("CompanyName"))
                        {
                            if (dtExcelData.Columns.Contains("CompanyName"))
                            {
                                if (radchkDefaultCompany.Checked == false)
                                {
                                    dtCompany.DefaultView.RowFilter = null;
                                    dtCompany.DefaultView.RowFilter = "CompanyName = '" + _drProcessedRow["CompanyName"].ToString().Trim() + "'";
                                    if (dtCompany.DefaultView.Count <= 0)
                                    {
                                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " CompanyID specified does not exist on database; record is skipped from processing!");
                                        //isCurrentEmpSkipped = true;
                                        isUpload = false;
                                    }
                                    else
                                    {
                                        _drProcessedRow.BeginEdit();
                                        _drProcessedRow["CompanyName"] = Convert.ToInt32(dtCompany.DefaultView[0]["CompanyID"].ToString());
                                        if (dtExcelData.Columns.Contains("CompanyCode"))
                                        {
                                            _drProcessedRow["CompanyCode"] = dtCompany.DefaultView[0]["CompanyCode"].ToString();
                                        }
                                        _drProcessedRow.EndEdit();
                                    }
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("CompanyName"))
                        {
                            dtCompany.DefaultView.RowFilter = null;
                            dtCompany.DefaultView.RowFilter = "CompanyName = '" + _drProcessedRow["CompanyName"].ToString().Trim() + "'";
                            if (dtCompany.DefaultView.Count > 0)
                            {
                                string prefix = dtCompany.DefaultView[0]["Prefix"].ToString();
                                bool isCustomEmployeeCode = Convert.ToBoolean(dtCompany.Rows[0]["CustomEmployeeCode"].ToString());
                                if (isCustomEmployeeCode)
                                {
                                    if (employeeCode.Length > 3)
                                    {
                                        string cuPrefix = employeeCode.Substring(0, 3);
                                        if (prefix != cuPrefix)
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Must have '" + prefix + "' prefix to EmployeeCode; record is skipped from processing!");
                                            isUpload = false;
                                        }
                                        else
                                        {
                                            string trueEmployeeCode = employeeCode.Substring(3);
                                            _drProcessedRow["TrueEmployeeCode"] = trueEmployeeCode;
                                        }

                                        //if (_drProcessedRow["CompanyCode"].ToString() == "UNIT1" || _drProcessedRow["CompanyCode"].ToString() == "UNIT2")
                                        //{
                                        //    dtUnitCompanies.DefaultView.RowFilter = null;
                                        //    dtUnitCompanies.DefaultView.RowFilter = "TrueEmployeeCode = '" + _drProcessedRow["TrueEmployeeCode"] + "' And CompanyID <> '" + _drProcessedRow["CompanyID"] + "'";
                                        //    if (dtUnitCompanies.DefaultView.Count > 0)
                                        //    {
                                        //        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Employee code already exists on another company; record is skipped from processing!");
                                        //        isUpload = false;
                                        //    }

                                        //    dtUnitCompanies.DefaultView.RowFilter = null;
                                        //    dtUnitCompanies.DefaultView.RowFilter = "EPFNo = '" + _drProcessedRow["EPFNo"].ToString().Trim() + "' And CompanyID <> '" + _drProcessedRow["CompanyID"] + "'";
                                        //    if (dtUnitCompanies.DefaultView.Count > 0)
                                        //    {
                                        //        CreateLogEntry(LogEntryLevel.Error, LogHeading + " EPFNo already exists on another company; record is skipped from processing!");
                                        //        isUpload = false;
                                        //    }
                                        //}
                                    }
                                    else
                                    {
                                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Must have '" + prefix + "' prefix to EmployeeCode; record is skipped from processing!");
                                        isUpload = false;
                                    }

                                }
                                else
                                {
                                    _drProcessedRow["TrueEmployeeCode"] = employeeCode;

                                    if (_drProcessedRow["CompanyCode"].ToString() == "UNIT1" || _drProcessedRow["CompanyCode"].ToString() == "UNIT2")
                                    {
                                        
                                    }
                                }
                            }
                        }
                    }

                    DataTable dtParentDepartment = objOrganization.GetParentOrganizationTypes(Convert.ToInt32(_drProcessedRow["CompanyName"]), 0);
                    DataTable dtParentDesignations = objDesignation.GetParentOrganizationTypes(0);
                    DataTable dtJobCatCompanyWise = objComReference.GetJobCategoryCompanyWise(Convert.ToInt32(_drProcessedRow["CompanyName"]));



                    if (isCurrentEmpSkipped == false)
                    {
                        
                    }

                    if (isExistEmployee)
                    {
                        if (isCurrentEmpSkipped == false)
                        {
                            if (dtExcelData.Columns.Contains("NIC"))
                            {
                               
                            }
                        }

                        if (isCurrentEmpSkipped == false)
                        {
                            if (dtExcelData.Columns.Contains("EPFNo"))
                            {
                                
                            }
                        }
                    }
                    else
                    {
                        if (isCurrentEmpSkipped == false)
                        {
                           
                        }

                        if (isCurrentEmpSkipped == false)
                        {
                            
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("JobCategory"))
                        {
                            if (radchkDefaultJobCategory.Checked == false)
                            {
                                dtJobCatCompanyWise.DefaultView.RowFilter = null;
                                dtJobCatCompanyWise.DefaultView.RowFilter = "JobCategoryName = '" + _drProcessedRow["JobCategory"].ToString().Trim() + "'";
                                if (dtJobCatCompanyWise.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Job Category specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["JobCategory"] = Convert.ToInt32(dtJobCatCompanyWise.DefaultView[0]["JobCategoryID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("BranchCategory"))
                        {
                            if (radckDefaultBranch.Checked == false)
                            {
                                dtParentDepartment.DefaultView.RowFilter = null;
                                dtParentDepartment.DefaultView.RowFilter = "OrganizationTypeName = '" + _drProcessedRow["BranchCategory"].ToString().Trim() + "'";
                                if (dtParentDepartment.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " BranchCategory specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["BranchCategory"] = Convert.ToInt32(dtParentDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("BranchCategory"))
                        {
                            if (radckDefaultBranch.Checked == false)
                            {
                                dtParentDepartment.DefaultView.RowFilter = null;
                                dtParentDepartment.DefaultView.RowFilter = "OrganizationTypeName = '" + _drProcessedRow["BranchCategory"].ToString().Trim() + "'";
                                if (dtParentDepartment.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + "BranchCategory specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["BranchCategory"] = Convert.ToInt32(dtParentDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                                    _drProcessedRow.EndEdit();
                                    int OrganizationTypeID = Convert.ToInt32(dtParentDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                                    GetOrgStructureID(OrganizationTypeID);
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("Department"))
                        {
                            if (radchkDefaultDepartment.Checked == false)
                            {
                                int OrganizationTypeID = Convert.ToInt32(dtParentDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                                GetOrgStructureID(OrganizationTypeID);
                                dtDepartment.DefaultView.RowFilter = null;
                                dtDepartment.DefaultView.RowFilter = "OrganizationTypeName = '" + _drProcessedRow["Department"].ToString().Trim() + "'";
                                if (dtDepartment.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + "OrganizationTypeName 2 specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["Department"] = Convert.ToInt32(dtDepartment.DefaultView[0]["OrgStructureID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EmploymentGradeName"))
                        {
                            if (radchkDefaultEmpGrade.Checked == false)
                            {
                                dtEmpGrade.DefaultView.RowFilter = null;
                                dtEmpGrade.DefaultView.RowFilter = "EmploymentGradeName = '" + _drProcessedRow["EmploymentGrade"].ToString().Trim() + "'";
                                if (dtEmpGrade.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + "Employment Grade specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["EmploymentGrade"] = Convert.ToInt32(dtEmpGrade.DefaultView[0]["EmploymentGradeID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }


                    //if (isCurrentEmpSkipped == false)
                    //{
                    //    if (radchkDefaultDesignation.Checked == false)
                    //    {
                    //        dtParentDesignations.DefaultView.RowFilter = null;
                    //        dtParentDesignations.DefaultView.RowFilter = "DesignationName = '" + _drProcessedRow["Designation1SecondLevel"].ToString().Trim() + "'";
                    //        if (dtParentDesignations.DefaultView.Count <= 0)
                    //        {
                    //            CreateLogEntry(LogEntryLevel.Error, LogHeading + "Designation1SecondLevel specified does not exist on database; record is skipped from processing!");
                    //            //isCurrentEmpSkipped = true;
                    //            isUpload = false;
                    //        }
                    //        else
                    //        {
                    //            _drProcessedRow.BeginEdit();
                    //            _drProcessedRow["DesignationID"] = Convert.ToInt32(dtParentDesignations.DefaultView[0]["DesignationID"].ToString());
                    //            _drProcessedRow.EndEdit();
                    //            int DesignationID = Convert.ToInt32(dtParentDesignations.DefaultView[0]["DesignationID"].ToString());
                    //            GetDesignationID(0);
                    //        }
                    //    }
                    //}

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EmployeeDesignation"))
                        {
                            if (radchkDefaultDepartment.Checked == false)
                            {
                                GetDesignationID(0);
                                dtDesignation.DefaultView.RowFilter = null;
                                dtDesignation.DefaultView.RowFilter = "DesignationName = '" + _drProcessedRow["EmployeeDesignation"].ToString().Trim() + "'";
                                if (dtDesignation.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + "'EmployeeDesignation' specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["EmployeeDesignation"] = Convert.ToInt32(dtDesignation.DefaultView[0]["DesignationID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }
                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("JobGrade"))
                        {
                            if (radchkDefaultJobGrade.Checked == false)
                            {
                                dtJobGrade.DefaultView.RowFilter = null;
                                dtJobGrade.DefaultView.RowFilter = "JobGrade = '" + _drProcessedRow["JobGrade"].ToString().Trim() + "'";
                                if (dtJobGrade.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Job Grade specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["JobGrade"] = Convert.ToInt32(dtJobGrade.DefaultView[0]["JobGradeID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EmploymentTypeName"))
                        {
                            if (radchkDefaultEmpType.Checked == false)
                            {
                                dtEmpTypes.DefaultView.RowFilter = null;
                                dtEmpTypes.DefaultView.RowFilter = "EmploymentTypeName = '" + _drProcessedRow["EmploymentType"].ToString().Trim() + "'";
                                if (dtEmpTypes.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Employment Type specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["EmploymentType"] = Convert.ToInt32(dtEmpTypes.DefaultView[0]["EmploymentTypeID"].ToString());
                                    _drProcessedRow["EmploymentTypeCode"] = dtEmpTypes.DefaultView[0]["EmploymentTypeCode"].ToString();
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }



                    //if (isCurrentEmpSkipped == false)
                    //{
                    //    if (_drProcessedRow["Religion"].ToString().Trim() != "")
                    //    {
                    //        dtReligion.DefaultView.RowFilter = null;
                    //        dtReligion.DefaultView.RowFilter = "RaceName = '" + _drProcessedRow["Religion"].ToString().Trim() + "'";
                    //        if (dtReligion.DefaultView.Count <= 0)
                    //        {
                    //            CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'Religion' specified does not exist on database; record is skipped from processing!");
                    //            //isCurrentEmpSkipped = true;
                    //            isUpload = false;
                    //        }
                    //        else
                    //        {
                    //            _drProcessedRow.BeginEdit();
                    //            _drProcessedRow["ReligionID"] = Convert.ToInt32(dtReligion.DefaultView[0]["RaceID"].ToString());
                    //            _drProcessedRow.EndEdit();
                    //        }
                    //    }
                    //}

                    //if (isCurrentEmpSkipped == false)
                    //{
                    //    if (_drProcessedRow["National"].ToString().Trim() != "")
                    //    {
                    //        dtNationality.DefaultView.RowFilter = null;
                    //        dtNationality.DefaultView.RowFilter = "NationalityName = '" + _drProcessedRow["National"].ToString().Trim() + "'";
                    //        if (dtNationality.DefaultView.Count <= 0)
                    //        {
                    //            CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'National' specified does not exist on database; record is skipped from processing!");
                    //            //isCurrentEmpSkipped = true;
                    //            isUpload = false;
                    //        }
                    //        else
                    //        {
                    //            _drProcessedRow.BeginEdit();
                    //            _drProcessedRow["NationalID"] = Convert.ToInt32(dtNationality.DefaultView[0]["NationalityID"].ToString());
                    //            _drProcessedRow.EndEdit();
                    //        }
                    //    }
                    //}

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("CompanyCostName"))
                        {
                            if (radchkDefaultCompany.Checked == false)
                            {
                                dtCompanyCostCenter.DefaultView.RowFilter = null;
                                dtCompanyCostCenter.DefaultView.RowFilter = "CompanyCostName = '" + _drProcessedRow["CostCenter"].ToString().Trim() + "'";
                                if (dtCompanyCostCenter.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Cost Center name specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["CostCenter"] = Convert.ToInt32(dtCompanyCostCenter.DefaultView[0]["CompanyCostID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }
                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("CompanyCostName"))
                        {
                            if (radchkDefaultCompany.Checked == false)
                            {
                                dtepfCompanyCostCenter.DefaultView.RowFilter = null;
                                dtepfCompanyCostCenter.DefaultView.RowFilter = "CompanyCostName = '" + _drProcessedRow["EPFCostCenter"].ToString().Trim() + "'";
                                if (dtCompanyCostCenter.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " EPF Cost Center name specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow.BeginEdit();
                                    _drProcessedRow["EPFCostCenter"] = Convert.ToInt32(dtCompanyCostCenter.DefaultView[0]["CompanyCostID"].ToString());
                                    _drProcessedRow.EndEdit();
                                }
                            }
                        }
                    }
                    #endregion

                    #region Check if Current Record is Skipped from Processing
                    if (dtExcelData.Columns.Contains("FirstName"))
                    {
                        if (_drProcessedRow["FirstName"].ToString().Trim() == string.Empty)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'FirstName' mandatory posting; record is skipped from processing!");
                            //isCurrentEmpSkipped = true;
                            isUpload = false;
                        }
                    }
                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("LastName"))
                        {
                            if (_drProcessedRow["LastName"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'LastName' mandatory posting; record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("PayrollAct"))
                        {
                            if (_drProcessedRow["PayrollAct"].ToString().Trim() != string.Empty)
                            {
                                if (_drProcessedRow["PayrollAct"].ToString().Trim() == "Shop & Office" || _drProcessedRow["PayrollAct"].ToString().Trim() == "Wages Board")
                                {

                                }
                                else
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'PayrollAct' column must be 'Shop & Office' or 'Wages Board'.; record is skipped from processing!");
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("CompanyName"))
                        {
                            if (_drProcessedRow["CompanyName"].ToString().Trim() == string.Empty)
                            {
                                if (radchkDefaultCompany.Checked == false)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'CompanyName' mandatory posting; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("Gender"))
                        {
                            if (radchkDefaultGender.Checked == false)
                            {
                                switch (_drProcessedRow["Gender"].ToString().Trim().ToLowerInvariant())
                                {
                                    case "male":
                                        {
                                            _drProcessedRow["Gender"] = "Male";

                                            break;
                                        }
                                    case "female":
                                        {
                                            _drProcessedRow["Gender"] = "Female";

                                            break;
                                        }
                                    default:
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data or contains invalid data for 'Gender' mandatory posting; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;

                                            break;
                                        }
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("BCardAvailability"))
                        {
                            if (radchDefultBCard.Checked == false)
                            {
                                switch (_drProcessedRow["BCardAvailability"].ToString().Trim().ToLowerInvariant())
                                {
                                    case "no":
                                        {
                                            _drProcessedRow["BCardAvailability"] = "No";

                                            break;
                                        }
                                    case "yes":
                                        {
                                            _drProcessedRow["BCardAvailability"] = "Yes";

                                            break;
                                        }
                                    default:
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data or contains invalid data for 'B Card Availability' mandatory posting; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;

                                            break;
                                        }
                                }
                            }
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("NIC"))
                        {
                            if (_drProcessedRow["NIC"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'NIC' mandatory posting; record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("NIC"))
                        {
                            if (_duplicateNICNumbers.FindIndex(s => s == _drProcessedRow["NIC"].ToString().Trim()) >= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " NIC: '" + _drProcessedRow["NIC"].ToString().Trim() + "' is duplicated within uploaded file & this is not allowed!, record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                        }
                    }
                    if (dtExcelData.Columns.Contains("EmploymentTypeCode"))
                    {
                        if (_drProcessedRow["EmploymentTypeCode"].ToString().Trim() == "Probation" || _drProcessedRow["EmploymentTypeCode"].ToString().Trim() == "FixedTermContarct" || _drProcessedRow["EmploymentTypeCode"].ToString().Trim() == "Consultancy")
                        {
                            if (_drProcessedRow["StartDate"].ToString().Trim() == "" || _drProcessedRow["EndDate"].ToString().Trim() == "")
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " If EmploymentType is 'Probation' or 'FixedTermContarct' or 'Consultancy' then 'StartDate' and 'EndDate' columns are mandatory ; record is skipped from processing!");
                                isUpload = false;
                            }
                        }
                    }
                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EPFNo"))
                        {

                            string a = _drProcessedRow["EPFNo"].ToString().Trim();

                            if (a != "")
                            {

                                if (_drProcessedRow["EPFNo"].ToString().Trim() == string.Empty)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'EPFNo' mandatory posting; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EPFNo"))
                        {
                            if (_duplicateEPFNumbers.FindIndex(s => s == _drProcessedRow["EPFNo"].ToString().Trim()) >= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " EPFNo: '" + _drProcessedRow["EPFNo"].ToString().Trim() + "' is duplicated within uploaded file & this is not allowed!, record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EPFNo"))
                        {
                            if (_drProcessedRow["EPFNo"].ToString().Trim() == employeeCode)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'EPFNo' & 'EmployeeCode' cannot be thes same & this is not allowed!, record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("JobCategory"))
                        {
                            if (radchkDefaultJobCategory.Checked == false)
                            {
                                if (_drProcessedRow["JobCategory"].ToString().Trim() == string.Empty)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'JobCategory' mandatory posting; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("BranchCategory"))
                        {
                            if (radchkDefaultDepartment.Checked == false)
                            {
                                if (_drProcessedRow["BranchCategory"].ToString().Trim() == string.Empty)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Department_Or_Section' mandatory posting; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("Department"))
                        {
                            if (radchkDefaultDepartment.Checked == false)
                            {
                                if (_drProcessedRow["Department"].ToString().Trim() == string.Empty)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Department_Or_Section' mandatory posting; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    //if (isCurrentEmpSkipped == false)
                    //{
                    //    if (radckDefaultBranch.Checked == false)
                    //    {
                    //        if (_drProcessedRow["Branch"].ToString().Trim() == string.Empty)
                    //        {
                    //            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Branch' mandatory posting; record is skipped from processing!");
                    //            isCurrentEmpSkipped = true;
                    //        }
                    //    }
                    //}

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EmploymentGrade"))
                        {
                            if (radchkDefaultEmpGrade.Checked == false)
                            {
                                if (_drProcessedRow["EmploymentGrade"].ToString().Trim() == string.Empty)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Employment Grade' mandatory posting; record is skipped from processing!");
                                    // isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EmployeeDesignation"))
                        {
                            if (radchkDefaultDesignation.Checked == false)
                            {
                                if (_drProcessedRow["EmployeeDesignation"].ToString().Trim() == string.Empty)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Designation' mandatory posting; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    //if (isCurrentEmpSkipped == false)
                    //{
                    //    if (radchkDefaultDesignation.Checked == false)
                    //    {
                    //        if (_drProcessedRow["Designation1SecondLevel"].ToString().Trim() == string.Empty)
                    //        {
                    //            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Designation1SecondLevel' mandatory posting; record is skipped from processing!");
                    //            //isCurrentEmpSkipped = true;
                    //            isUpload = false;
                    //        }
                    //    }
                    //}

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("RecruitementDate"))
                        {
                            if (_drProcessedRow["RecruitementDate"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'RecruitementDate' mandatory posting; record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                            else
                            {
                                try
                                {
                                    tempDate = Convert.ToDateTime(_drProcessedRow["RecruitementDate"].ToString().Trim());
                                }
                                catch
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'RecruitementDate' contains invalid data; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EPFEntitlementDate"))
                        {
                            if (_drProcessedRow["EPFEntitlementDate"].ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    tempDate = Convert.ToDateTime(_drProcessedRow["EPFEntitlementDate"].ToString().Trim());
                                }
                                catch
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'EPF Entitle Date' contains invalid data; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("ConfirmationDate"))
                        {
                            if (_drProcessedRow["ConfirmationDate"].ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    tempDate = Convert.ToDateTime(_drProcessedRow["ConfirmationDate"].ToString().Trim());

                                    if (tempDate < Convert.ToDateTime(_drProcessedRow["RecruitementDate"].ToString().Trim()))
                                    {
                                        _drProcessedRow["ConfirmationDate"] = string.Empty;
                                        CreateLogEntry(LogEntryLevel.Warning, LogHeading + " Row data 'ConfirmationDate' must be greater than or equal to 'RecruitementDate'; 'ConfirmationDate' was not applied to employee!");
                                    }
                                }
                                catch
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'ConfirmationDate' contains invalid data; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("SecondRecruitmentDate"))
                        {
                            if (_drProcessedRow["SecondRecruitmentDate"].ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    tempDate = Convert.ToDateTime(_drProcessedRow["SecondRecruitmentDate"].ToString().Trim());
                                }
                                catch
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'SecondRecruitmentDate' contains invalid data; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("ResignDate"))
                        {
                            if (_drProcessedRow["ResignDate"].ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    tempDate = Convert.ToDateTime(_drProcessedRow["ResignDate"].ToString().Trim());
                                }
                                catch
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'ResignDate' contains invalid data; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("JobCategory"))
                        {
                            switch (_drProcessedRow["Active"].ToString().Trim().ToLowerInvariant())
                            {
                                case "yes":
                                    {
                                        _drProcessedRow["Active"] = true;

                                        break;
                                    }
                                case "no":
                                    {
                                        _drProcessedRow["Active"] = false;
                                        if (_drProcessedRow["ResignDate"].ToString().Trim() == string.Empty)
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'Inactive Employees' must have an 'ResignDate'; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;
                                        }

                                        break;
                                    }
                                case "true":
                                    {
                                        _drProcessedRow["Active"] = true;

                                        break;
                                    }
                                case "false":
                                    {
                                        _drProcessedRow["Active"] = false;
                                        if (_drProcessedRow["ResignDate"].ToString().Trim() == string.Empty)
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'Inactive Employees' must have an 'ResignDate'; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;
                                        }

                                        break;
                                    }
                                default:
                                    {
                                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'Active' contains invalid data; record is skipped from processing!");
                                        //isCurrentEmpSkipped = true;
                                        isUpload = false;

                                        break;
                                    }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EmployeeSalary"))
                        {
                            if (_drProcessedRow["EmployeeSalary"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'EmployeeSalary' mandatory posting; record is skipped from processing!");
                                //isCurrentEmpSkipped = true;
                                isUpload = false;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (dtExcelData.Columns.Contains("EmployeeSalary"))
                        {
                            switch (_drProcessedRow["EmployeeSalary"].ToString().Trim().ToLowerInvariant())
                            {
                                case "yes":
                                    {
                                        _drProcessedRow["EmployeeSalary"] = true;
                                        break;
                                    }
                                case "no":
                                    {
                                        _drProcessedRow["EmployeeSalary"] = false;
                                        break;
                                    }
                                case "true":
                                    {
                                        _drProcessedRow["EmployeeSalary"] = true;
                                        break;
                                    }
                                case "false":
                                    {
                                        _drProcessedRow["EmployeeSalary"] = false;
                                        break;
                                    }

                                default:
                                    {
                                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'Active' contains invalid data; record is skipped from processing!");
                                        //isCurrentEmpSkipped = true;
                                        isUpload = false;
                                        break;
                                    }
                            }
                        }
                    }
                    if (dtExcelData.Columns.Contains("EmployeeSalary"))
                    {
                        if (Convert.ToBoolean(_drProcessedRow["EmployeeSalary"]))
                        {

                            if (isCurrentEmpSkipped == false)
                            {
                                if (_drProcessedRow["BasicSalary"].ToString().Trim() == string.Empty || _drProcessedRow["DailyWage"].ToString().Trim() == string.Empty)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Data is mandatory for either one of 'BasicSalary' or 'DailyWage' posting(s); record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    if (_drProcessedRow["BasicSalary"].ToString().Trim() != string.Empty)
                                    {
                                        try
                                        {
                                            tempDecimal = Convert.ToDecimal(_drProcessedRow["BasicSalary"].ToString().Trim());
                                        }
                                        catch
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'BasicSalary' contains invalid data; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;
                                        }
                                    }
                                    else
                                    {
                                        _drProcessedRow["BasicSalary"] = 0;
                                    }

                                    if (_drProcessedRow["DailyWage"].ToString().Trim() != string.Empty)
                                    {
                                        try
                                        {
                                            tempDecimal = Convert.ToDecimal(_drProcessedRow["DailyWage"].ToString().Trim());
                                        }
                                        catch
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'DailyWage' contains invalid data; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;
                                        }
                                    }
                                    else
                                    {
                                        _drProcessedRow["DailyWage"] = 0;
                                    }
                                }
                            }

                            if (isCurrentEmpSkipped == false)
                            {
                                if (_drProcessedRow["FixedAllowance"].ToString().Trim() != string.Empty)
                                {
                                    try
                                    {
                                        tempDecimal = Convert.ToDecimal(_drProcessedRow["FixedAllowance"].ToString().Trim());
                                    }
                                    catch
                                    {
                                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'FixedAllowance' contains invalid data; record is skipped from processing!");
                                        //isCurrentEmpSkipped = true;
                                        isUpload = false;
                                    }
                                }
                                else
                                {
                                    _drProcessedRow["FixedAllowance"] = 0;
                                }
                            }

                            if (isCurrentEmpSkipped == false)
                            {
                                switch (_drProcessedRow["StopPay"].ToString().Trim().ToLowerInvariant())
                                {
                                    case "yes":
                                        {
                                            _drProcessedRow["StopPay"] = true;

                                            break;
                                        }
                                    case "no":
                                        {
                                            _drProcessedRow["StopPay"] = false;

                                            break;
                                        }
                                    case "true":
                                        {
                                            _drProcessedRow["StopPay"] = true;

                                            break;
                                        }
                                    case "false":
                                        {
                                            _drProcessedRow["StopPay"] = false;

                                            break;
                                        }
                                    default:
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'StopPay' contains invalid data; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;

                                            break;
                                        }
                                }
                            }

                            if (isCurrentEmpSkipped == false)
                            {
                                if (_drProcessedRow["MaxAdvancePercentage"].ToString().Trim() != string.Empty)
                                {
                                    try
                                    {
                                        tempDecimal = Convert.ToDecimal(_drProcessedRow["MaxAdvancePercentage"].ToString().Trim());
                                        if (tempDecimal > 100)
                                        {
                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'MaxAdvancePercentage' cannot exceed 100%; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;
                                        }
                                    }
                                    catch
                                    {
                                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'MaxAdvancePercentage' contains invalid data; record is skipped from processing!");
                                        //isCurrentEmpSkipped = true;
                                        isUpload = false;
                                    }
                                }
                                else
                                {
                                    _drProcessedRow["MaxAdvancePercentage"] = 0;
                                    CreateLogEntry(LogEntryLevel.Warning, LogHeading + " Row data 'MaxAdvancePercentage' was empty and was applied 0 as a result!");
                                }
                            }

                            if (isCurrentEmpSkipped == false)
                            {
                                switch (_drProcessedRow["BankTranferRequired"].ToString().Trim().ToLowerInvariant())
                                {
                                    case "yes":
                                        {
                                            _drProcessedRow["BankTranferRequired"] = true;
                                            if (_drProcessedRow["BankCode"].ToString().Trim() == string.Empty || _drProcessedRow["BranchCode"].ToString().Trim() == string.Empty || _drProcessedRow["AccountNo"].ToString().Trim() == string.Empty || _drProcessedRow["EmployeeNameAsPerBank"].ToString().Trim() == string.Empty)
                                            {
                                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " If employee is required a Bank Transfer, 'BankCode', 'BranchCode', 'AccountNo' & 'EmployeeNameAsPerBank' must be specified; record is skipped from processing!");
                                                //isCurrentEmpSkipped = true;
                                                isUpload = false;
                                            }

                                            break;
                                        }
                                    case "no":
                                        {
                                            _drProcessedRow["BankTranferRequired"] = false;
                                            _drProcessedRow["BankCode"] = 0;
                                            _drProcessedRow["BranchCode"] = 0;
                                            _drProcessedRow["AccountNo"] = 0;

                                            break;
                                        }
                                    case "true":
                                        {
                                            _drProcessedRow["BankTranferRequired"] = true;
                                            if (_drProcessedRow["BankCode"].ToString().Trim() == string.Empty || _drProcessedRow["BranchCode"].ToString().Trim() == string.Empty || _drProcessedRow["AccountNo"].ToString().Trim() == string.Empty || _drProcessedRow["EmployeeNameAsPerBank"].ToString().Trim() == string.Empty)
                                            {
                                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " If employee is required a Bank Transfer, 'BankCode', 'BranchCode' & 'AccountNo' & 'EmployeeNameAsPerBank' must be specified; record is skipped from processing!");
                                                //isCurrentEmpSkipped = true;
                                                isUpload = false;
                                            }

                                            break;
                                        }
                                    case "false":
                                        {
                                            _drProcessedRow["BankTranferRequired"] = false;
                                            _drProcessedRow["BankCode"] = 0;
                                            _drProcessedRow["BranchCode"] = 0;
                                            _drProcessedRow["AccountNo"] = 0;

                                            break;
                                        }
                                    default:
                                        {

                                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'BankTranferRequired' contains invalid data; record is skipped from processing!");
                                            //isCurrentEmpSkipped = true;
                                            isUpload = false;

                                            break;
                                        }
                                }
                            }

                            if (_drProcessedRow["BankTranferRequired"].ToString().Trim().ToLowerInvariant() == "true")
                            {

                                if (isCurrentEmpSkipped == false)
                                {
                                   
                                }

                                if (isCurrentEmpSkipped == false)
                                {
                                   
                                }

                            }

                            if (isCurrentEmpSkipped == false)
                            {
                                DataTable dtPayCategory = objSecurity.GetPayCategory(Convert.ToInt32(_drProcessedRow["CompanyID"]));
                                dtPayCategory.DefaultView.RowFilter = null;
                                dtPayCategory.DefaultView.RowFilter = "PayPeriodCategory = '" + _drProcessedRow["PayCategoryName"].ToString().Trim() + "'";
                                if (dtPayCategory.DefaultView.Count <= 0)
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'Pay Period Category' specified does not exist on database; record is skipped from processing!");
                                    //isCurrentEmpSkipped = true;
                                    isUpload = false;
                                }
                                else
                                {
                                    _drProcessedRow["PayCategoryID"] = Convert.ToInt32(dtPayCategory.DefaultView[0]["PayPeriodCategoryId"].ToString().Trim());
                                }
                            }
                        }
                    }
                    #endregion

                    if (isCurrentEmpSkipped == false)
                    {
                        // start editing rows; if any changes to row
                        _drProcessedRow.BeginEdit();

                        if (radchkCombineToGetFullName.Checked == true)
                        {
                            _drProcessedRow["FullName"] = _drProcessedRow["FirstName"].ToString().Trim() + " " + _drProcessedRow["LastName"].ToString().Trim();
                        }

                        if (radchkConvertNameFieldDataToCapsEachWord.Checked == true)
                        {
                            _drProcessedRow["FirstName"] = System.Text.RegularExpressions.Regex.Replace(_drProcessedRow["FirstName"].ToString().Trim(), pattern, m => m.Value[0].ToString().ToUpper() + m.Value.Substring(1));
                            _drProcessedRow["LastName"] = System.Text.RegularExpressions.Regex.Replace(_drProcessedRow["LastName"].ToString().Trim(), pattern, m => m.Value[0].ToString().ToUpper() + m.Value.Substring(1));
                            _drProcessedRow["FullName"] = System.Text.RegularExpressions.Regex.Replace(_drProcessedRow["FullName"].ToString().Trim(), pattern, m => m.Value[0].ToString().ToUpper() + m.Value.Substring(1));
                        }

                        #region Defaults Check

                        if (radchkDefaultGender.Checked == true)
                        {
                            if (_drProcessedRow["Gender"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " 'Gender' column was empty & was replaced with default!");
                                _drProcessedRow["Gender"] = radcmbDefaultGender.Text;
                            }
                            else
                            {
                                //if (_drProcessedRow["Gender"].ToString().Trim().ToLowerInvariant() != "male" || _drProcessedRow["Gender"].ToString().Trim().ToLowerInvariant() != "female")
                                if (_drProcessedRow["Gender"].ToString().Trim().ToLowerInvariant() != "male" || _drProcessedRow["Gender"].ToString().Trim().ToLowerInvariant() != "female")
                                {
                                    CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid gender & was replaced with default!");
                                    _drProcessedRow["Gender"] = radcmbDefaultGender.Text;
                                }
                            }
                        }

                        if (radchDefultBCard.Checked == true)
                        {
                            if (_drProcessedRow["BCardAvailability"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " 'B Card Availability' column was empty & was replaced with default!");
                                _drProcessedRow["BCardAvailability"] = radcmbDefaultBCard.Text;
                            }
                            else
                            {

                                if (_drProcessedRow["BCardAvailability"].ToString().Trim().ToLowerInvariant() != "no" || _drProcessedRow["BCardAvailability"].ToString().Trim().ToLowerInvariant() != "yes")
                                {
                                    CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid no & was replaced with default!");
                                    _drProcessedRow["BCardAvailability"] = radcmbDefaultBCard.Text;
                                }
                            }
                        }

                        if (radchkDefaultCompany.Checked == true)
                        {
                            dtCompany.DefaultView.RowFilter = null;
                            dtCompany.DefaultView.RowFilter = "CompanyName = '" + _drProcessedRow["CompanyName"].ToString().Trim() + "'";
                            if (dtCompany.DefaultView.Count > 0)
                            {
                                _drProcessedRow["CompanyID"] = Convert.ToInt32(dtCompany.DefaultView[0]["CompanyId"].ToString().Trim());
                            }
                            else
                            {
                                _drProcessedRow["CompanyID"] = Convert.ToInt32(radcmbDefCompany.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid company & was replaced with default!");
                            }
                        }

                        if (radchkDefaultJobCategory.Checked == true)
                        {
                            dtJobCategory.DefaultView.RowFilter = null;
                            dtJobCategory.DefaultView.RowFilter = "JobCategoryName = '" + _drProcessedRow["JobCategory"].ToString().Trim() + "'";
                            if (dtJobCategory.DefaultView.Count > 0)
                            {
                                _drProcessedRow["JobCategoryID"] = Convert.ToInt32(dtJobCategory.DefaultView[0]["JobCategoryId"].ToString().Trim());
                            }
                            else
                            {
                                _drProcessedRow["JobCategoryID"] = Convert.ToInt32(radcmbDefJobCategory.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid job category & was replaced with default!");
                            }
                        }

                        if (radchkDefaultDepartment.Checked == true)
                        {
                            dtDepartment.DefaultView.RowFilter = null;
                            dtDepartment.DefaultView.RowFilter = "OrganizationTypeName = '" + _drProcessedRow["OrganizationTypeName"].ToString().Trim() + "'";
                            if (dtDepartment.DefaultView.Count > 0)
                            {
                                _drProcessedRow["OrganizationTypeID"] = Convert.ToInt32(dtDepartment.DefaultView[0]["OrganizationTypeId"].ToString().Trim());
                            }
                            else
                            {
                                _drProcessedRow["OrganizationTypeID"] = Convert.ToInt32(radcmbDefDepartment.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid department\\section & was replaced with default!");
                            }
                        }

                        //if (radckDefaultBranch.Checked == true)
                        //{
                        //    dtBranch.DefaultView.RowFilter = null;
                        //    dtBranch.DefaultView.RowFilter = "Location = '" + _drProcessedRow["Branch"].ToString().Trim() + "'";
                        //    if (dtBranch.DefaultView.Count > 0)
                        //    {
                        //        _drProcessedRow["BranchId"] = Convert.ToInt32(dtBranch.DefaultView[0]["BranchId"].ToString().Trim());
                        //    }
                        //    else
                        //    {
                        //        _drProcessedRow["BranchId"] = Convert.ToInt32(radcmbDefBranch.SelectedValue.Trim());
                        //        CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid Branch & was replaced with default!");
                        //    }
                        //}

                        if (radchkDefaultEmpGrade.Checked == true)
                        {
                            dtEmpGrade.DefaultView.RowFilter = null;
                            dtEmpGrade.DefaultView.RowFilter = "EmploymentGradeName = '" + _drProcessedRow["EmploymentGrade"].ToString().Trim() + "'";
                            if (dtEmpGrade.DefaultView.Count > 0)
                            {
                                _drProcessedRow["EmploymentGradeID"] = Convert.ToInt32(dtEmpGrade.DefaultView[0]["EmploymentGradeID"].ToString().Trim());
                            }
                            else
                            {
                                _drProcessedRow["EmploymentGradeID"] = Convert.ToInt32(radcmbDefEmpGrade.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid Branch & was replaced with default!");
                            }
                        }


                        if (radchkDefaultDesignation.Checked == true)
                        {
                            dtDesignation.DefaultView.RowFilter = null;
                            dtDesignation.DefaultView.RowFilter = "DesignationName = '" + _drProcessedRow["Designation"].ToString().Trim() + "'";
                            if (dtDesignation.DefaultView.Count > 0)
                            {
                                _drProcessedRow["DesignationID"] = Convert.ToInt32(dtDesignation.DefaultView[0]["DesignationId"].ToString().Trim());
                            }
                            else
                            {
                                _drProcessedRow["DesignationID"] = Convert.ToInt32(radcmbDefJobDesignation.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid designation & was replaced with default!");
                            }
                        }

                        if (radchkDefaultJobGrade.Checked == true)
                        {
                            dtJobGrade.DefaultView.RowFilter = null;
                            dtJobGrade.DefaultView.RowFilter = "JobGrade = '" + _drProcessedRow["JobGrade"].ToString().Trim() + "'";
                            if (dtJobGrade.DefaultView.Count > 0)
                            {
                                _drProcessedRow["JobGradeID"] = Convert.ToInt32(dtJobGrade.DefaultView[0]["JobGradeId"].ToString().Trim());
                            }
                            else
                            {
                                _drProcessedRow["JobGradeID"] = Convert.ToInt32(radcmbDefJobGrade.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid job grade & was replaced with default!");
                            }
                        }

                        if (radchkDefaultEmpType.Checked == true)
                        {
                            dtEmpTypes.DefaultView.RowFilter = null;
                            dtEmpTypes.DefaultView.RowFilter = "EmploymentTypeName = '" + _drProcessedRow["EmploymentType"].ToString().Trim() + "'";
                            if (dtEmpTypes.DefaultView.Count > 0)
                            {
                                _drProcessedRow["EmploymentTypeID"] = Convert.ToInt32(dtEmpTypes.DefaultView[0]["EmploymentTypeId"].ToString().Trim());
                            }
                            else
                            {
                                _drProcessedRow["EmploymentTypeID"] = Convert.ToInt32(radcmbDefEmpType.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid employee type & was replaced with default!");
                            }
                        }

                        #endregion

                        #region Find Structure ID's

                        //dtOrgStructure.DefaultView.RowFilter = null;
                        //dtOrgStructure.DefaultView.RowFilter = "OrganizationTypeID = " + Convert.ToInt32(_drProcessedRow["OrganizationTypeID"].ToString().Trim());
                        //if (dtOrgStructure.DefaultView.Count > 0)
                        //{
                        //    _drProcessedRow["OrganizationStructureID"] = Convert.ToInt32(dtOrgStructure.DefaultView[0]["OrgStructureID"].ToString().Trim());
                        //}
                        //else
                        //{
                        //    _drProcessedRow["OrganizationStructureID"] = 0;
                        //}

                        //dtDesigStructure.DefaultView.RowFilter = null;
                        //dtDesigStructure.DefaultView.RowFilter = "DesignationID = " + Convert.ToInt32(_drProcessedRow["DesignationID"].ToString().Trim());
                        //if (dtDesigStructure.DefaultView.Count > 0)
                        //{
                        //    _drProcessedRow["DesignationStructureID"] = Convert.ToInt32(dtDesigStructure.DefaultView[0]["DesignationStuctureID"].ToString().Trim());
                        //}
                        //else
                        //{
                        //    _drProcessedRow["DesignationStructureID"] = 0;
                        //}

                        #endregion
                        if (dtExcelData.Columns.Contains("IsExported"))
                        {
                            _drProcessedRow["IsExported"] = true;
                        }
                        _drProcessedRow.EndEdit();
                    }
                }

                else
                {
                    CreateLogEntry(LogEntryLevel.Warning, "[Line " + Convert.ToString(k + 1) + "] Excel file data is invalid & does not contain a employee code!");
                }
            }

            bool isPartialUpdateContinued = true;

            if (radchkEnablePartialUploads.Checked == false)
            {
                dtExcelData.DefaultView.RowFilter = null;
                if (dtExcelData.Columns.Contains("IsExported"))
                {
                    dtExcelData.DefaultView.RowFilter = "IsExported = " + false;
                }
                if (dtExcelData.DefaultView.Count > 0)
                {
                    isPartialUpdateContinued = false;
                    CreateLogEntry(LogEntryLevel.Message, "Some records of excel file contained errors & employee import was cancelled due to 'Partial Upload' is disabled. (To enable view 'Uploaded file error correction options' & check 'Upload partial list when errora occur in upload process')");
                }

                dtExcelData.DefaultView.RowFilter = null;
            }

            if (isPartialUpdateContinued == true && isUpload == true)
            {
                int SavedItemCount = 0;
                int NotExportedItemCount = 0;
                if (dtExcelData.Columns.Contains("IsExported"))
                {
                    dtExcelData.DefaultView.RowFilter = "IsExported = " + false;
                }
                dtExcelData.DefaultView.RowFilter = null;

                NotExportedItemCount = dtExcelData.DefaultView.Count;

                if (Session["UserName"] != null)
                {
                   

                    if (SavedItemCount > 0)
                    {
                        CreateLogEntry(LogEntryLevel.Message, SavedItemCount + " of " + dtExcelData.Rows.Count.ToString() + " record(s) imported to database. Please check event log for further information on non-imported records!");
                    }
                    else
                    {
                        CreateLogEntry(LogEntryLevel.Message, "No records imported to database. Please check event log for further information!");
                    }
                }
                else
                {
                    Response.Redirect("~/SessionTimeout.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            CreateLogEntry(LogEntryLevel.Error, ex.Message);
        }
    }

    private void GetOrgStructureID(int OrganizationTypeID)
    {
        dtDepartment = objOrganization.GetOrganizationTypesExcel(Convert.ToInt32(Session["CompanyId"]), OrganizationTypeID);
    }

    private void GetDesignationID(int OrganizationTypeID)
    {
        dtDesignation = objDesignation.GetDesignationTypesExcel(OrganizationTypeID);
    }


    private DataTable ReadExcelData(string filePath)
    {
        OleDbConnection con = new OleDbConnection();
        DataTable dtReturnData = new DataTable();

        try
        {
            switch ((new FileInfo(filePath)).Extension.ToLowerInvariant())
            {
                case ".xls":
                    {
                        con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'");
                        break;
                    }
                case ".xlsx":
                    {
                        con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1'");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [EmployeeMaster$]", con);
                da.Fill(dtReturnData);





            }
            catch (Exception ex)
            {
                dtReturnData = null;

                CreateLogEntry(LogEntryLevel.Error, "Error reading excel file [" + ex.Message + "]");
                CreateLogEntry(LogEntryLevel.Message, "Error generating data from excel file, Possible solution: Rename Excel work sheet name to 'EmployeeMaster'");
            }

            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogEntry(LogEntryLevel.Error, ex.Message);
        }
        finally
        {
            con.Dispose();
        }

        return dtReturnData;
    }
    protected void popOkbtn_Click(object sender, EventArgs e)
    {
        LoadExcelFile(radUploadEmpExcel.TargetFolder);
        confirmpopup.ShowOnPageLoad = false;
    }

    protected void popCancelbtn_Click(object sender, EventArgs e)
    {

    }

    protected void radbtnUploadFile_Click(object sender, EventArgs e)
    {
        confirmpopup.ShowOnPageLoad = true;
    }



    protected void radchkDefaultGender_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchDefultBCard_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkDefaultCompany_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkDefaultJobCategory_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkDefaultDepartment_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radckDefaultBranch_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkDefaultDesignation_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkDefaultJobGrade_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkDefaultEmpType_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkDefaultEmpGrade_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radchkReplaceTextWith_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radbtnClearLog_Click(object sender, EventArgs e)
    {

    }

    protected void radcmbFilterEvents_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        {
            dtLog = (DataTable)ViewState["ErrorLog"];

            if (radcmbFilterEvents.Text != "All")
            {
                dtLog.DefaultView.RowFilter = "EventType = '" + radcmbFilterEvents.Text + "'";
                radGridEvents.DataSource = dtLog.DefaultView;
            }
            else
            {
                dtLog.DefaultView.RowFilter = null;
                radGridEvents.DataSource = dtLog;
            }

            radGridEvents.DataBind();
        }
    }
}