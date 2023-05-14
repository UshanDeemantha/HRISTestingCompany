using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class HR_Employee_EmployeeAdditionalDataUpload : System.Web.UI.Page
{
    DataTable dtLog;
    Employee objEmployee = new Employee();



    #region Security

    private const string ApplicationName = "HR";
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
                    Response.Redirect("~/HR/NoPermissions.aspx");
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
        decimal tempDecimal = 0;

        List<string> _duplicateEmpCodes = new List<string>();
        
        try
        {
            dtProcessedData = dtExcelData.Clone();

            dtProcessedData.Columns.Add(new DataColumn("IsExported", typeof(bool)));
            dtProcessedData.Columns.Add(new DataColumn("EmployeeID", typeof(long)));
            dtProcessedData.Columns.Add(new DataColumn("ReligionID", typeof(int)));
            dtProcessedData.Columns.Add(new DataColumn("NationalityID", typeof(int)));
            

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

                        drTemp["IsExported"] = false;
                        drTemp["EmployeeID"] = 0;
                        drTemp["ReligionID"] = 0;
                        drTemp["NationalityID"] = 0;
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

                    drTemp["IsExported"] = false;
                    drTemp["EmployeeID"] = 0;
                    drTemp["ReligionID"] = 0;
                    drTemp["NationalityID"] = 0;
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
            DataTable dtReligion = objEmployee.GetReligion();
            DataTable dtNationality = objEmployee.GetNaionality();
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
            }

            #endregion

            bool isCurrentEmpSkipped = false;
            bool isUpload = true;

            for (int k = 0; k < dtProcessedData.Rows.Count; k++)
            {
                isCurrentEmpSkipped = false;
                isUpload = true;
                _drProcessedRow = dtProcessedData.Rows[k];
                employeeCode = _drProcessedRow["EmployeeCode"].ToString().Trim();


                if (employeeCode != string.Empty)
                {
                    LogHeading = "[Line " + Convert.ToString(k + 1) + " | Employee code: " + employeeCode + "]";

                    if (_duplicateEmpCodes.FindIndex(s => s == employeeCode) >= 0)
                    {
                        CreateLogEntry(LogEntryLevel.Error, LogHeading + " Employee code is duplicated within uploaded file & this is not allowed!, record is skipped from processing!");
                        //isCurrentEmpSkipped = true;
                        isUpload = false;
                    }



                    #region Database Checks

                    if (isCurrentEmpSkipped == false)
                    {
                        
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["Religion"].ToString().Trim() != "")
                        {
                            dtReligion.DefaultView.RowFilter = null;
                            dtReligion.DefaultView.RowFilter = "RaceName = '" + _drProcessedRow["Religion"].ToString().Trim() + "'";
                            if (dtReligion.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'Religion' specified does not exist on database; record is skipped from processing!");
                                isUpload = false;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["ReligionID"] = Convert.ToInt32(dtReligion.DefaultView[0]["RaceID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    if (isCurrentEmpSkipped == false)
                    {
                        if (_drProcessedRow["Ethnicity"].ToString().Trim() != "")
                        {
                            dtNationality.DefaultView.RowFilter = null;
                            dtNationality.DefaultView.RowFilter = "NationalityName = '" + _drProcessedRow["Ethnicity"].ToString().Trim() + "'";
                            if (dtNationality.DefaultView.Count <= 0)
                            {
                                CreateLogEntry(LogEntryLevel.Error, LogHeading + " 'Ethnicity' specified does not exist on database; record is skipped from processing!");
                                isUpload = false;
                            }
                            else
                            {
                                _drProcessedRow.BeginEdit();
                                _drProcessedRow["NationalityID"] = Convert.ToInt32(dtNationality.DefaultView[0]["NationalityID"].ToString());
                                _drProcessedRow.EndEdit();
                            }
                        }
                    }

                    
                    #endregion

                    

                    if (isCurrentEmpSkipped == false)
                    {
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

            if (isPartialUpdateContinued == true && isUpload == true)
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
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [EmployeeAdditionalDetails$]", con);
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

    protected void btnplus_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

    }
    protected void btnDownloadTemplates_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/Common/Settings/UploadFormats.aspx?TempateName=EmpAdditionalDetails&PagePath=HR/Employee/EmployeeAdditionalDataUpload.aspx");
    }
}