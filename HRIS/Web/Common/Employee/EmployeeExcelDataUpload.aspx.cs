using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Common_Employee_EmployeeExcelDataUpload : System.Web.UI.Page
{ 
    DataTable dtLog;

    Reference objComReference = new Reference();
    CompanyProfile objCompanyProfile = new CompanyProfile();
    Organization objOrganization = new Organization();
    Designation objDesignation = new Designation();
    Employee objEmployee = new Employee();


    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "EmployeExcelUpload";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    private enum LogEntryLevel
    {
        Message = 1,
        Error = 2,
        Warning = 3,
        Success = 4
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        if (IsPostBack == true)
        {
            if (ViewState["ErrorLog"] != null)
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
        else
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }
            else
            {
                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/Payroll/NoPermissions.aspx");
                    return;
                }
                else
                {
                    radbtnUploadFile.Visible = false;
                    radbtnUploadOptions.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnUploadFile.Visible = true;
                        radbtnUploadOptions.Visible = true;
                    }
                    else
                    {
                        radbtnUploadOptions.Visible = radbtnUploadFile.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);

                    }
                }
            }
        }
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
        DataTable dtProcessedData = new DataTable();
        DataRow drTemp;

        int SearchReplaceInstanceCount = 0;

        List<string> _duplicateEmpCodes = new List<string>();
        List<string> _duplicateNICNumbers = new List<string>();
        List<string> _duplicateEPFNumbers = new List<string>();
        try
        {
            dtProcessedData = dtExcelData.Clone();

            dtProcessedData.Columns.Add(new DataColumn("IsExported", typeof(bool)));
            dtProcessedData.Columns.Add(new DataColumn("CompanyID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("JobCategoryID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("OrganizationTypeID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("DesignationID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("JobGradeID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("EmploymentTypeID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("OrganizationStructureID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("DesignationStructureID", typeof(int)));
           // dtProcessedData.Columns.Add(new DataColumn("BranchId", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("EmploymentGradeID", typeof(int)));

            if (radchkReplaceTextWith.Checked == true)
            {
                if (radtxtSerchFor.Text != string.Empty)
                {
                    foreach (DataRow drData in dtExcelData.Rows)
                    {
                        drTemp = dtProcessedData.NewRow();

                        for (int i = 0; i < drData.Table.Columns.Count; i++)
                        {
                            if (radcmbRepaceMethod.Text == "any occurrence")
                            {
                                if (drData[i].ToString().Contains(radtxtSerchFor.Text) == true)
                                {
                                    SearchReplaceInstanceCount++;
                                    drTemp[i] = drData[i].ToString().Replace(radtxtSerchFor.Text, radtxtReplaceWith.Text);
                                }
                                else
                                {
                                    drTemp[i] = drData[i];
                                }
                            }
                            else
                            {
                                if (drData[i].ToString() == radtxtSerchFor.Text)
                                {
                                    SearchReplaceInstanceCount++;
                                    drTemp[i] = radtxtReplaceWith.Text;
                                }
                                else
                                {
                                    drTemp[i] = drData[i];
                                }
                            }
                        }

                        drTemp["CompanyID"] = 0;
                        drTemp["JobCategoryID"] = 0;
                        drTemp["OrganizationTypeID"] = 0;
                        drTemp["DesignationID"] = 0;
                        drTemp["JobGradeID"] = 0;
                        drTemp["EmploymentTypeID"] = 0;
                        drTemp["OrganizationStructureID"] = 0;
                        drTemp["DesignationStructureID"] = 0;
                       // drTemp["BranchId"] = 0;
                        drTemp["EmploymentGradeID"] =0;
                        drTemp["IsExported"] = false;
                        dtProcessedData.Rows.Add(drTemp);
                    }

                    CreateLogEntry(LogEntryLevel.Success, "Search & Replace successfully completed! [" + SearchReplaceInstanceCount.ToString() + " occurrence(s) found with search string]");
                }
            }
            else
            {
                foreach (DataRow drData in dtExcelData.Rows)
                {
                    drTemp = dtProcessedData.NewRow();

                    for (int i = 0; i < drData.Table.Columns.Count; i++)
                    {
                        drTemp[i] = drData[i];
                    }

                    drTemp["CompanyID"] = 0;
                    drTemp["JobCategoryID"] = 0;
                    drTemp["OrganizationTypeID"] = 0;
                    drTemp["DesignationID"] = 0;
                    drTemp["JobGradeID"] = 0;
                    drTemp["EmploymentTypeID"] = 0;
                    drTemp["OrganizationStructureID"] = 0;
                    drTemp["DesignationStructureID"] = 0;
                    //drTemp["BranchId"] = 0;
                    drTemp["EmploymentGradeID"] =0;
                    drTemp["IsExported"] = false;
                    dtProcessedData.Rows.Add(drTemp);
                }
            }

            // variables to store temp data...
            string pattern = @"\b(\w|['-])+\b";
            string employeeCode = string.Empty;
            string LogHeading = string.Empty;

            DateTime tempDate = new DateTime();
            //decimal tempDecimal = 0;
            DataRow _drProcessedRow;
            
            #region Data for Verifying Uploaded Data

            DataTable dtCompany = objCompanyProfile.GetCompanyProfile(0);
            DataTable dtJobCategory = objComReference.GetJobCategory(0);
            DataTable dtDepartment = objOrganization.GetOrganizationTypes(Convert.ToInt32(Session["CompanyId"]), 0);
            DataTable dtDesignations = objDesignation.GetDesignation(0);
            DataTable dtJobGrade = objComReference.GetJobGrade(0);
            DataTable dtEmpTypes = objEmployee.GetEmploymentType(0);

           // DataTable dtBranch = objComReference.GetBranch(0, Convert.ToInt32(Session["CompanyId"]));
            DataTable dtEmpGrade = objEmployee.GetEmploymentGrade(0);

            #endregion

            #region Find Duplicate 'Employee Numbers' & 'NIC's' within datatable

            foreach (DataRow _drItem in dtExcelData.Rows)
            {
                if (_duplicateEmpCodes.FindIndex(s => s == _drItem["EmployeeCode"].ToString().Trim()) < 0)
                {

                    //string b = _drItem["F1"].ToString().Trim();
                    dtProcessedData.DefaultView.RowFilter = null;
                    //string a = "EmployeeCode = '" + _drItem[0].ToString().Trim() + "'";
                    dtProcessedData.DefaultView.RowFilter = "EmployeeCode = '" + _drItem["EmployeeCode"].ToString().Trim() + "'";
                   // dtProcessedData.DefaultView.RowFilter = a;
                    if (dtProcessedData.DefaultView.Count > 1)
                    {
                        _duplicateEmpCodes.Add(_drItem["EmployeeCode"].ToString().Trim()); 
                    }
                }

                if (_duplicateNICNumbers.FindIndex(s => s == _drItem["NIC"].ToString().Trim()) < 0)
                {
                    dtProcessedData.DefaultView.RowFilter = null;
                    dtProcessedData.DefaultView.RowFilter = "NIC = '" + _drItem["NIC"].ToString().Trim() + "'";
                    if (dtProcessedData.DefaultView.Count > 1)
                    {
                        _duplicateNICNumbers.Add(_drItem["NIC"].ToString().Trim());
                    }
                }
                if (_duplicateEPFNumbers.FindIndex(s => s == _drItem["EPFNo"].ToString().Trim()) < 0)
                {
                    dtProcessedData.DefaultView.RowFilter = null;
                    string a = "EPFNo = '" + _drItem[31].ToString().Trim() + "'";
                    if (a != "EPFNo = ''")
                    {
                        dtProcessedData.DefaultView.RowFilter = "EPFNo = '" + _drItem["EPFNo"].ToString().Trim() + "'";
                        if (dtProcessedData.DefaultView.Count > 1)
                        {
                            _duplicateEPFNumbers.Add(_drItem["EPFNo"].ToString().Trim());
                        }

                    }
                    
                }
            }

            #endregion

            bool isCurrentEmpSkipped = false;

            for (int k = 0; k < dtProcessedData.Rows.Count; k++)
            {
                isCurrentEmpSkipped = false;
                _drProcessedRow = dtProcessedData.Rows[k];
                employeeCode = _drProcessedRow["EmployeeCode"].ToString().Trim();

                if (employeeCode != string.Empty)
                {
                    LogHeading = "[Line " + Convert.ToString(k + 1) + " | Employee code: " + employeeCode + "]";

                    if (_duplicateEmpCodes.FindIndex(s => s == employeeCode) >= 0)
                    {
                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Employee code is duplicated within uploaded file & this is not allowed!, record is skipped from processing!");
                        isCurrentEmpSkipped = true;
                    }

                    #region Date Verifications

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["DateOfBirth"].ToString().Trim() == string.Empty)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'DateOfBirth' mandatory posting; record is skipped from processing!");
                            isCurrentEmpSkipped = true;
                        }
                        else
                        {
                            if (DateTime.TryParse(_drProcessedRow["DateOfBirth"].ToString().Trim(), out tempDate) == false)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'DateOfBirth' mandatory posting is in invalid format; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["RecruitementDate"].ToString().Trim() == string.Empty)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'RecruitementDate' mandatory posting; record is skipped from processing!");
                            isCurrentEmpSkipped = true;
                        }
                        else
                        {
                            if (DateTime.TryParse(_drProcessedRow["RecruitementDate"].ToString().Trim(), out tempDate) == false)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'RecruitementDate' mandatory posting is in invalid format; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["EPFEntitleDate"].ToString().Trim() != string.Empty)
                        {
                            if (DateTime.TryParse(_drProcessedRow["EPFEntitleDate"].ToString().Trim(), out tempDate) == false)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'EPF Entitle Date' mandatory posting is in invalid format; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["SecondReqruitmentDate"].ToString().Trim() != string.Empty)
                        {
                            if (DateTime.TryParse(_drProcessedRow["SecondReqruitmentDate"].ToString().Trim(), out tempDate) == false)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'SecondReqruitmentDate' posting is in invalid format; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow.Table.Columns.Contains("ResignDate") && _drProcessedRow["ResignDate"].ToString().Trim() != string.Empty)
                        {
                            if (DateTime.TryParse(_drProcessedRow["ResignDate"].ToString().Trim(), out tempDate) == false)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data for 'ResignDate' posting is in invalid format; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    #endregion

                    #region Database Checks

                    

                   

                    if (isCurrentEmpSkipped == false)
                    {
                        
                        string a = "EPFNo = '" + _drProcessedRow["EPFNo"].ToString().Trim() + "'";
                        if (a != "EPFNo = ''")
                        {
                            
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultCompany.Checked == false)
                        {
                            dtCompany.DefaultView.RowFilter = null;
                            dtCompany.DefaultView.RowFilter = "CompanyName = '" + _drProcessedRow["CompanyName"].ToString().Trim() + "'";
                            if (dtCompany.DefaultView.Count <= 0)
                            {
                               // CreateLogEntry(LogEntryLevel.Error, LogHeading + " Company name specified does not exist on database; record is skipped from processing!");
                               // isCurrentEmpSkipped = true;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["CompanyID"] = Convert.ToInt32(dtCompany.DefaultView[0]["CompanyID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultJobCategory.Checked == false)
                        {
                            dtJobCategory.DefaultView.RowFilter = null;
                            dtJobCategory.DefaultView.RowFilter = "JobCategoryCode = '" + _drProcessedRow["JobCategory"].ToString().Trim() + "'";
                            if (dtJobCategory.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Job Category specified does not exist on database; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["JobCategoryID"] = Convert.ToInt32(dtJobCategory.DefaultView[0]["JobCategoryID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultDepartment.Checked == false)
                        {
                            dtDepartment.DefaultView.RowFilter = null;
                            dtDepartment.DefaultView.RowFilter = "OrganizationTypeName = '" + _drProcessedRow["OrganizationTypeName"].ToString().Trim() + "'";
                            if (dtDepartment.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Department or Section specified does not exist on database; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["OrganizationTypeID"] = Convert.ToInt32(dtDepartment.DefaultView[0]["OrganizationTypeID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    //if (isCurrentEmpSkipped == false)
                    //{
                    //    if (radckDefaultBranch.Checked == false)
                    //    {
                    //        dtBranch.DefaultView.RowFilter = null;
                    //        dtBranch.DefaultView.RowFilter = "Location = '" + _drProcessedRow["Branch"].ToString().Trim() + "'";
                    //        if (dtBranch.DefaultView.Count <= 0)
                    //        {
                    //            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Branch specified does not exist on database; record is skipped from processing!");
                    //            isCurrentEmpSkipped = true;
                    //        }
                    //        else
                    //        {
                    //            _drProcessedRow.BeginEdit();
                    //            _drProcessedRow["BranchId"] = Convert.ToInt32(dtBranch.DefaultView[0]["BranchId"].ToString());
                    //            _drProcessedRow.EndEdit();
                    //        }
                    //    }
                    //}

                    
                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultEmpGrade.Checked == false)
                        {
                            dtEmpGrade.DefaultView.RowFilter = null;
                            dtEmpGrade.DefaultView.RowFilter = "EmploymentGradeName = '" + _drProcessedRow["EmploymentGrade"].ToString().Trim() + "'";
                            if (dtEmpGrade.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Employment Grade specified does not exist on database; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["EmploymentGradeID"] = Convert.ToInt32(dtEmpGrade.DefaultView[0]["EmploymentGradeID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultDesignation.Checked == false)
                        {
                            dtDesignations.DefaultView.RowFilter = null;
                            dtDesignations.DefaultView.RowFilter = "DesignationName = '" + _drProcessedRow["Designation"].ToString().Trim() + "'";
                            if (dtDesignations.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Designation specified does not exist on database; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["DesignationID"] = Convert.ToInt32(dtDesignations.DefaultView[0]["DesignationID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultJobGrade.Checked == false)
                        {
                            dtJobGrade.DefaultView.RowFilter = null;
                            dtJobGrade.DefaultView.RowFilter = "JobGrade = '" + _drProcessedRow["JobGrade"].ToString().Trim() + "'";
                            if (dtJobGrade.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Job Grade specified does not exist on database; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["JobGradeID"] = Convert.ToInt32(dtJobGrade.DefaultView[0]["JobGradeID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultEmpType.Checked == false)
                        {
                            dtEmpTypes.DefaultView.RowFilter = null;
                            dtEmpTypes.DefaultView.RowFilter = "EmploymentTypeName = '" + _drProcessedRow["EmploymentType"].ToString().Trim() + "'";
                            if (dtEmpTypes.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Employment Type specified does not exist on database; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["EmploymentTypeID"] = Convert.ToInt32(dtEmpTypes.DefaultView[0]["EmploymentTypeID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    #endregion

                    #region Check if Current Record is Skipped from Processing

                    if (_drProcessedRow["FirstName"].ToString().Trim() == string.Empty)
                    {
                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'FirstName' mandatory posting; record is skipped from processing!");
                        isCurrentEmpSkipped = true;
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["LastName"].ToString().Trim() == string.Empty)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'LastName' mandatory posting; record is skipped from processing!");
                            isCurrentEmpSkipped = true;
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["CompanyName"].ToString().Trim() == string.Empty)
                        {
                            if (radchkDefaultCompany.Checked == false)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'CompanyName' mandatory posting; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
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
                                        isCurrentEmpSkipped = true;

                                        break;
                                    }
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
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
                                        isCurrentEmpSkipped = true;

                                        break;
                                    }
                            }
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["NIC"].ToString().Trim() == string.Empty)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'NIC' mandatory posting; record is skipped from processing!");
                            isCurrentEmpSkipped = true;
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_duplicateNICNumbers.FindIndex(s => s == _drProcessedRow["NIC"].ToString().Trim()) >= 0)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " NIC: '" + _drProcessedRow["NIC"].ToString().Trim() + "' is duplicated within uploaded file & this is not allowed!, record is skipped from processing!");
                            isCurrentEmpSkipped = true;
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {

                       string a =_drProcessedRow["EPFNo"].ToString().Trim();

                       if (a != "")
                       {

                           if (_drProcessedRow["EPFNo"].ToString().Trim() == string.Empty)
                           {
                               CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'EPFNo' mandatory posting; record is skipped from processing!");
                               isCurrentEmpSkipped = true;
                           }
                       }
                     
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_duplicateEPFNumbers.FindIndex(s => s == _drProcessedRow["EPFNo"].ToString().Trim()) >= 0)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " EPFNo: '" + _drProcessedRow["EPFNo"].ToString().Trim() + "' is duplicated within uploaded file & this is not allowed!, record is skipped from processing!");
                            isCurrentEmpSkipped = true;
                        }
                    }


                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultJobCategory.Checked == false)
                        {
                            if (_drProcessedRow["JobCategory"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'JobCategory' mandatory posting; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultDepartment.Checked == false)
                        {
                            if (_drProcessedRow["OrganizationTypeName"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Department_Or_Section' mandatory posting; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radckDefaultBranch.Checked == false)
                        {
                            if (_drProcessedRow["Branch"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Branch' mandatory posting; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                        if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultEmpGrade.Checked == false)
                        {
                            if (_drProcessedRow["EmploymentGrade"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Employment Grade' mandatory posting; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (radchkDefaultDesignation.Checked == false)
                        {
                            if (_drProcessedRow["Designation"].ToString().Trim() == string.Empty)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'Designation' mandatory posting; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["RecruitementDate"].ToString().Trim() == string.Empty)
                        {
                            CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row does not contain data for 'RecruitementDate' mandatory posting; record is skipped from processing!");
                            isCurrentEmpSkipped = true;
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
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["EPFEntitleDate"].ToString().Trim() != string.Empty)
                        {
                            try
                            {
                                tempDate = Convert.ToDateTime(_drProcessedRow["EPFEntitleDate"].ToString().Trim());
                            }
                            catch
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'EPF Entitle Date' contains invalid data; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
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
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["SecondReqruitmentDate"].ToString().Trim() != string.Empty)
                        {
                            try
                            {
                                tempDate = Convert.ToDateTime(_drProcessedRow["SecondReqruitmentDate"].ToString().Trim());
                            }
                            catch
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'SecondReqruitmentDate' contains invalid data; record is skipped from processing!");
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
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
                                isCurrentEmpSkipped = true;
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
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
                                        isCurrentEmpSkipped = true;
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
                                        isCurrentEmpSkipped = true;
                                    }

                                    break;
                                }
                            default:
                                {
                                    CreateLogEntry(LogEntryLevel.Error, LogHeading + " Row data 'Active' contains invalid data; record is skipped from processing!");
                                    isCurrentEmpSkipped = true;

                                    break;
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
                                _drProcessedRow["BranchId"] = Convert.ToInt32(radcmbDefEmpGrade.SelectedValue.Trim());
                                CreateLogEntry(LogEntryLevel.Message, LogHeading + " Contained an invalid Branch & was replaced with default!");
                            }
                        }


                        if (radchkDefaultDesignation.Checked == true)
                        {
                            dtDesignations.DefaultView.RowFilter = null;
                            dtDesignations.DefaultView.RowFilter = "DesignationName = '" + _drProcessedRow["Designation"].ToString().Trim() + "'";
                            if (dtDesignations.DefaultView.Count > 0)
                            {
                                _drProcessedRow["DesignationID"] = Convert.ToInt32(dtDesignations.DefaultView[0]["DesignationId"].ToString().Trim());
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


                        #endregion

                        _drProcessedRow["IsExported"] = true;
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
                dtProcessedData.DefaultView.RowFilter = null;
                dtProcessedData.DefaultView.RowFilter = "IsExported = " + false;
                if (dtProcessedData.DefaultView.Count > 0)
                {
                    isPartialUpdateContinued = false;
                    CreateLogEntry(LogEntryLevel.Message, "Some records of excel file contained errors & employee import was cancelled due to 'Partial Upload' is disabled. (To enable view 'Uploaded file error correction options' & check 'Upload partial list when errora occur in upload process')");
                }

                dtProcessedData.DefaultView.RowFilter = null;
            }

            if (isPartialUpdateContinued == true)
            {
                int SavedItemCount = 0;
                int NotExportedItemCount = 0;

                dtProcessedData.DefaultView.RowFilter = "IsExported = " + false;
                dtProcessedData.DefaultView.RowFilter = null;

                NotExportedItemCount = dtProcessedData.DefaultView.Count;

                if (Session["UserName"] != null)
                {
                    

                    if (SavedItemCount > 0)
                    {
                        CreateLogEntry(LogEntryLevel.Message, SavedItemCount + " of " + dtProcessedData.Rows.Count.ToString() + " record(s) imported to database. Please check event log for further information on non-imported records!");
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



                //for (int i = 0; i < dtReturnData.Columns.Count; i++)
                //{
                //    dtReturnData.Columns[i].ColumnName = dtReturnData.Rows[0][i].ToString();

                //}
                //dtReturnData.Rows.RemoveAt(0);
                //dtReturnData.AcceptChanges();




//For i = 0 To DtSet.Tables(0).Columns.Count - 1
//myDataRow(i) = DtSet.Tables(0).Columns(i).Caption
//DtSet.Tables(0).Columns(i).ColumnName = CStr(i)
//Next
//DtSet.Tables(0).Rows.InsertAt(myDataRow, 0) 


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
    protected void radchkDefaultEmpType_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefEmpType.Enabled = ((RadButton)sender).Checked;
    }

    protected void radchkDefaultCompany_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefCompany.Enabled = ((RadButton)sender).Checked;
    }

    protected void radchkDefaultJobGrade_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefJobGrade.Enabled = ((RadButton)sender).Checked;
    }

    protected void radchkDefaultDesignation_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefJobDesignation.Enabled = ((RadButton)sender).Checked;
    }

    protected void radchkDefaultDepartment_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefDepartment.Enabled = ((RadButton)sender).Checked;
    }

    protected void radckDefaultBranch_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefBranch.Enabled = ((RadButton)sender).Checked;
    }


    protected void radchkDefaultEmpGrade_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefEmpGrade.Enabled = ((RadButton)sender).Checked;
    }


    protected void radchkDefaultJobCategory_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefJobCategory.Enabled = ((RadButton)sender).Checked;
    }

    protected void radchkDefaultGender_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefaultGender.Enabled = ((RadButton)sender).Checked;
    }

    protected void radchDefultBCard_CheckedChanged(object sender, EventArgs e)
    {
        radcmbDefaultBCard.Enabled = ((RadButton)sender).Checked;
    }

    protected void radchkReplaceTextWith_CheckedChanged(object sender, EventArgs e)
    {
        pnlSearchAndReplace.Enabled = ((RadButton)sender).Checked;
        radcmbRepaceMethod.Enabled = ((RadButton)sender).Checked;

        radtxtSerchFor.Text = "";
        radtxtReplaceWith.Text = "";
    }

    protected void radUploadEmpExcel_FileExists(object sender, Telerik.Web.UI.Upload.UploadedFileEventArgs e)
    {
        try
        {
            string targetFileName = Path.Combine(Server.MapPath(radUploadEmpExcel.TargetFolder), e.UploadedFile.GetNameWithoutExtension() + e.UploadedFile.GetExtension());

            if (File.Exists(targetFileName) == true)
            {
                File.Delete(targetFileName);
            }

            e.UploadedFile.SaveAs(targetFileName);
        }
        catch (Exception ex)
        {
            CreateLogEntry(LogEntryLevel.Error, ex.Message);
        }
    }

    protected void radbtnUploadFile_Click(object sender, EventArgs e)
    {
        confirmpopup.ShowOnPageLoad = true;
    }

    protected void radbtnClearLog_Click(object sender, EventArgs e)
    {
        dtLog = new DataTable();
        ViewState["ErrorLog"] = dtLog;

        CreateLogEntry(LogEntryLevel.Message, "Log cleared!");

        radGridEvents.DataSource = dtLog;
        radGridEvents.DataBind();
    }

    protected void radcmbFilterEvents_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (ViewState["ErrorLog"] != null)
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
    protected void radbtnUploadOptions_Click(object sender, EventArgs e)
    {

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        LoadExcelFile(radUploadEmpExcel.TargetFolder);

        confirmpopup.ShowOnPageLoad = false;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        confirmpopup.ShowOnPageLoad = false;
    }
}