/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Payroll.BLL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 8/30/2011
/// Class Description   : Text File Generator
/// Task Code List      : HRMV2P000020, 
/// </summary>
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using HRM.Payroll.DAL;
using System.IO;
using HRM.Common.BLL;

namespace HRM.Payroll.BLL
{
    public class TextFileGenerator
    {
        #region Fields
        private string _fileName = string.Empty;
        private string _pathName;
        private string _fileExtension = string.Empty;
        private string _companyUniqueId = string.Empty;
        private string _bankCode = string.Empty;
        private int _payPeriodId = 0;
        private DateTime _payPeriodStartDate = DateTime.MinValue;
        private DateTime _payPeriodEndDate = DateTime.MinValue;
        private int _days = 0;
        private decimal _TotalSalary = 0M;
        private int _employeeCount = 0;
        private bool _isError;
        private string _errorMsg;
        #endregion

        #region Properties
        /// <summary>
        /// Get File Name
        /// </summary>
        /// <value>FileName As string</value>
        public string FileName
        {
            get { return _fileName; }
        }

        /// <summary>
        /// Get File Extension
        /// </summary>
        /// <value>FileExtension As string</value>
        public string FileExtension
        {
            get { return _fileExtension; }
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <value>TPathName As string</value>
        public string PathName
        {
            get { return _pathName; }
        }

        /// <summary>
        /// Get Bank Name
        /// </summary>
        /// <value>BankCode As string</value>
        public string BankCode
        {
            get { return _bankCode; }
        }

        /// <summary>
        /// Get Pay Period Start Date
        /// </summary>
        /// <value>PayPeriodStartDate As DateTime</value>
        public DateTime PayPeriodStartDate
        {
            get { return _payPeriodStartDate; }
        }

        /// <summary>
        /// Get Pay Period End Date
        /// </summary>
        /// <value>PayPeriodEndDate As DateTime</value>
        public DateTime PayPeriodEndDate
        {
            get { return _payPeriodEndDate; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>. </value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        #endregion

        #region Constructors
        public TextFileGenerator(int CompanyId, int PayCategoryId, int PayPeriodId, string FilePath)
        {
            _payPeriodId = PayPeriodId;
            _pathName = FilePath;

            GetCompanyUniqueId(CompanyId);
            GetPayPeriodInfo(PayCategoryId, PayPeriodId);
        }
        #endregion

        #region Methods
        public DataTable GetCompanyEmployeeRecords(int Year, int Month, int CompanyId, string EncryptionKey, string catList)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.GetCompanyEmployeeRecordsDatail(Year, Month, CompanyId, EncryptionKey, catList);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }

        public DataTable GetCompanyEmployeeETFRecords(int Year, int Month, int CompanyId, string EncryptionKey, string catList)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.GetCompanyEmployeeETFRecords(Year, Month, CompanyId, EncryptionKey, catList);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }

        public DataTable GetEPFETFCompanyDetails(int companyId)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.GetEPFETFCompanyDetails(companyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }
        public DataTable GetCompanyBankDetails(int companyId, int bankID)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.GetCompanyBankDetails(companyId, bankID);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }
        public DataTable CheckForStandingOrder(Int64 empID)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.CheckForStandingOrder(empID);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }

        public DataTable GetPayEmployeeRecordsDatail(int Year, int Month, int CompanyId, string EncryptionKey, string catList)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.GetPayEmployeeRecordsDatail(Year, Month, CompanyId, EncryptionKey, catList);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }

        public DataTable GetBankCode(int bankId)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.GetBankCode(bankId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }
        public void GetCompanyUniqueId(int CompanyId)
        {
            CompanyProfile objCompany = new CompanyProfile();
            try
            {
                DataTable dtTable = objCompany.GetCompanyProfile(CompanyId);
                if (dtTable.Rows.Count > 0)
                {
                    _companyUniqueId = dtTable.Rows[0]["CompanyRegistrationNo"].ToString().Trim();
                    _bankCode = dtTable.Rows[0]["BankCode"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objCompany != null)
                {
                    GC.SuppressFinalize(objCompany);
                }
            }
        }

        public DataTable GetEmployeeRecords(int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            EmployeeTransactionDAL objEmpTxn = new EmployeeTransactionDAL();
            try
            {
                dtTable = objEmpTxn.GetEmployeePayrollDatail(PayPeriodId, EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objEmpTxn != null)
                {
                    GC.SuppressFinalize(objEmpTxn);
                }
            }
            return dtTable;
        }

        public void GetPayPeriodInfo(int PayCategoryId, int PayPeriodId)
        {
            PaySetupDAL objPayPeriod = new PaySetupDAL();
            try
            {
                DataTable dtTable = objPayPeriod.GetPayPeriods(PayCategoryId, PayPeriodId);
                if (dtTable.Rows.Count > 0)
                {
                    _payPeriodStartDate = Convert.ToDateTime(dtTable.Rows[0]["BeginDate"]);
                    _payPeriodEndDate = Convert.ToDateTime(dtTable.Rows[0]["EndDate"]);

                    TimeSpan span = _payPeriodEndDate.Subtract(_payPeriodStartDate);
                    _days = span.Days;
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (objPayPeriod != null)
                {
                    GC.SuppressFinalize(objPayPeriod);
                }
            }
        }

        #region SIF
        public void CreateSIFFile()
        {
            _fileExtension = ConfigurationManager.AppSettings["SIFFileExtension"].ToString();
            _fileName = _companyUniqueId + DateTime.Now.ToString("yyMMddHHmmss") + _fileExtension;

            StreamWriter sw = new StreamWriter(_pathName + _fileName, true);
            //  sw.WriteLine("");
            sw.Flush();
            sw.Close();

            DataTable dtPayroll = GetEmployeeRecords(_payPeriodId, 0);

            GenerateSIF_EDRRecord(dtPayroll);
            GenerateSIF_ControlRecord();
        }

        private void GenerateSIF_EDRRecord(DataTable EmpRecord)
        {
            for (int i = 0; i < EmpRecord.Rows.Count; i++)
            {
                StringBuilder record = new StringBuilder();
                string EmployeeUniqueId = string.Empty;
                try
                {
                    if (EmpRecord.Rows[i]["EmployeeCode"].ToString().Trim().Length < 14)
                    {
                        EmployeeUniqueId = new StringBuilder().Insert(0, "0", (14 - EmpRecord.Rows[i]["EmployeeCode"].ToString().Trim().Length)).ToString() + EmpRecord.Rows[i]["EmployeeCode"].ToString().Trim();
                    }
                    EmpRecord.Rows[i]["EmployeeCode"].ToString().Trim();

                    string fromMonth = "00";
                    if (_payPeriodStartDate.Month < 10)
                    { fromMonth = "0" + _payPeriodStartDate.Month.ToString(); }
                    else { fromMonth = _payPeriodStartDate.Month.ToString(); }

                    string fromDay = "00";
                    if (_payPeriodStartDate.Day < 10)
                    { fromDay = "0" + _payPeriodStartDate.Day.ToString(); }
                    else { fromDay = _payPeriodStartDate.Day.ToString(); }

                    string toMonth = "00";
                    if (_payPeriodEndDate.Month < 10)
                    { toMonth = "0" + _payPeriodEndDate.Month.ToString(); }
                    else { toMonth = _payPeriodEndDate.Month.ToString(); }

                    string toDay = "00";
                    if (_payPeriodEndDate.Day < 10)
                    { toDay = "0" + _payPeriodEndDate.Day.ToString(); }
                    else { toDay = _payPeriodEndDate.Day.ToString(); }

                    record.Append("EDR ," + EmployeeUniqueId + "," + EmpRecord.Rows[i]["BankCode"].ToString().Trim() + ", " + EmpRecord.Rows[i]["AccountCode"].ToString() + " ," + _payPeriodStartDate.Year + "-" + fromMonth + "-" + fromDay + ",");
                    record.Append(_payPeriodEndDate.Year + "-" + toMonth + "-" + toDay + "," + _days + ",");
                    record.Append(EmpRecord.Rows[i]["FixedIncome"].ToString() + "," + EmpRecord.Rows[i]["VariableIncome"].ToString() + "," + EmpRecord.Rows[i]["NOPAYDAYS"].ToString());

                    _TotalSalary = Convert.ToDecimal(EmpRecord.Rows[i]["FixedIncome"]) + Convert.ToDecimal(EmpRecord.Rows[i]["VariableIncome"]);
                    _employeeCount += 1;

                    AppendRecord(record.ToString() + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
                }
            }
        }

        private void GenerateSIF_ControlRecord()
        {
            StringBuilder record = new StringBuilder();
            string uniqueId = string.Empty;
            try
            {
                if (_companyUniqueId.Length < 13)
                {
                    uniqueId = new StringBuilder().Insert(0, "0", (13 - _companyUniqueId.Length)).ToString() + _companyUniqueId;
                }

                record.Append("SCR ," + uniqueId + "," + _bankCode + ", " + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + ",");
                record.Append(DateTime.Now.Hour + "-" + DateTime.Now.Minute + "," + _payPeriodStartDate.Month + "," + _employeeCount.ToString() + ",");
                record.Append(_TotalSalary.ToString() + "," + "AED   ,");

                AppendRecord(record.ToString());
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region RFR
        public void CreateRFRFile(DataTable RefundData, string SIFFile)
        {
            _fileExtension = ConfigurationManager.AppSettings["RFRFileExtension"].ToString();
            _fileName = _companyUniqueId + DateTime.Now.ToString("yyMMddHHmmss") + _fileExtension;

            StreamWriter sw = new StreamWriter(_pathName + _fileName, true);
            sw.Flush();
            sw.Close();

            GenerateRFR_FDRRecord(RefundData, SIFFile);
            GenerateRFR_ControlRecord();
        }

        private void GenerateRFR_FDRRecord(DataTable EmpRecord, string SIFFile)
        {
            for (int i = 0; i < EmpRecord.Rows.Count; i++)
            {
                StringBuilder record = new StringBuilder();
                try
                {
                    if (Convert.ToDecimal(EmpRecord.Rows[i]["RefundAmount"]) > 0)
                    {
                        string EmployeeUniqueId = string.Empty;
                        if (EmpRecord.Rows[i]["EmployeeCode"].ToString().Trim().Length < 14)
                        {
                            EmployeeUniqueId = new StringBuilder().Insert(0, "0", (14 - EmpRecord.Rows[i]["EmployeeCode"].ToString().Trim().Length)).ToString() + EmpRecord.Rows[i]["EmployeeCode"].ToString().Trim();
                        }

                        record.Append("FDR ," + SIFFile + ", ," + EmpRecord.Rows[i]["BankCode"].ToString().Trim() + ", " + EmployeeUniqueId + " ,");
                        record.Append(EmpRecord.Rows[i]["RefundAmount"].ToString().Trim() + "," + EmpRecord.Rows[i]["ErrorCode"].ToString().Trim());
                        record.Append(EmpRecord.Rows[i]["AccountCode"].ToString().Trim() + ", , EWPMS");

                        _TotalSalary = Convert.ToDecimal(EmpRecord.Rows[0]["FixedIncome"]) + Convert.ToDecimal(EmpRecord.Rows[0]["VariableIncome"]);
                        _employeeCount += 1;

                        AppendRecord(record.ToString() + Environment.NewLine);
                    }
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
                }
            }
        }

        private void GenerateRFR_ControlRecord()
        {
            StringBuilder record = new StringBuilder();
            string uniqueId = string.Empty;
            try
            {
                if (_companyUniqueId.Length < 13)
                {
                    uniqueId = new StringBuilder().Insert(0, "0", (13 - _companyUniqueId.Length)).ToString() + _companyUniqueId;
                }

                record.Append("FCR ," + uniqueId + ", ," + "802420101, " + _bankCode + "," + _TotalSalary.ToString() + "," + _employeeCount.ToString() + ", , ,EWPMS");

                AppendRecord(record.ToString());
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region Journal
        public void CreateJournalFile(string FileName)
        {
            StreamWriter sw = new StreamWriter(FileName, true);
            sw.Flush();
            sw.Close();
        }

        public void GenerateJournalHeader(string FileName, string HeaderLine)
        {
            StringBuilder record = new StringBuilder();
            try
            {              
                //record.Append("RECTYPE,BATCHID,BTCHENTRY,ORIGCOMP,SRCELEDGER,SRCETYPE,FSCSYR,FSCSPERD,SWEDIT,SWREVERSE,JRNLDESC,JRNLDR,JRNLCR,JRNLQTY,DATEENTRY,DRILSRCTY,DRILLDWNLK,DRILAPP,BALANCE,REVYR,REVPERD,ERRBATCH,ERRENTRY,DETAILCNT,PROCESSCMD");
                //record.Append(Environment.NewLine);
                record.Append("RECTYPE,BATCHID,BTCHENTRY,SRCELEDGER,SRCETYPE,JRNLDESC");
                record.Append(Environment.NewLine);
                //record.Append("RECTYPE,BATCHNBR,JOURNALID,TRANSNBR,DESCOMP,ROUTE,ACCTID,COMPANYID,TRANSAMT,TRANSQTY,SCURNDEC,SCURNAMT,HCURNCODE,RATETYPE,SCURNCODE,RATEDATE,CONVRATE,RATESPREAD,DATEMTCHCD,RATEOPER,TRANSDESC,TRANSREF,TRANSDATE,SRCELDGR,SRCETYPE,COMMENT,VALUES,PROCESSCMD");
                //record.Append(Environment.NewLine);

                record.Append("RECTYPE,BATCHNBR,JOURNALID,TRANSNBR,ACCTID,TRANSAMT,TRANSDESC,TRANSREF");
                record.Append(Environment.NewLine);
              //  string record = "2,1,1" + rowno * 20 + "," + AccNo + "," + TransferAmount + ", Employee sdfd,Employee sdfd";

                record.Append("RECTYPE,BATCHNBR,JOURNALID,TRANSNBR,OPTFIELD,VALUE,TYPE,LENGTH,DECIMALS,ALLOWNULL,VALIDATE,SWSET,VALINDEX,VALIFTEXT,VALIFMONEY,VALIFNUM,VALIFLONG,VALIFBOOL,VALIFDATE,VALIFTIME,FDESC,VDESC");

                AppendRecord(record.ToString() + Environment.NewLine, FileName);
                AppendRecord(HeaderLine + Environment.NewLine, FileName);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void GenerateJournalDetail(string FileName, string DetailLine)
        {
            try
            {
                AppendRecord(DetailLine + Environment.NewLine, FileName);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        } 
        #endregion

        /// <summary>
        /// Append Data To Log File
        /// </summary>
        /// <param name="Content">Content As string</param>
        public void AppendRecord(string Content)
        {
            try
            {
                if (File.Exists(PathName + @"\" + FileName))
                {
                    File.AppendAllText(PathName + @"\" + FileName, Content);
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AppendRecord(string Content, string FileName)
        {
            try
            {
                if (File.Exists(FileName))
                {
                    File.AppendAllText(FileName, Content);
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Closes the log file
        /// </summary>
        public void CloseFile()
        {
            try
            {
                if (File.Exists(PathName + @"\" + FileName))
                {
                    File.AppendAllText(PathName + @"\" + FileName, ">>>>>>>>>>>>>>> File End >>>>>>>>>>>>>>>");
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion
    }
}
