
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using HRM.Payroll.DAL;

namespace HRM.Payroll.BLL
{
    [DataObject]
    public  class PaySetup
    {
        #region Fields
        private PaySetupDAL objPaySetup; 
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private string _result;
        private bool _isSuccess;
        #endregion
        
        #region Properties
        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public bool IsError
        {
            get { return _isError; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        } 

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public string Result
        {
            get { return _result; }
        } 
        #endregion

        #region Constructor
        public PaySetup()
        {
            objPaySetup = new PaySetupDAL();
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objPaySetup.IsError;
            _errorNo = objPaySetup.ErrorNo;
            _errorMsg = objPaySetup.ErrorMsg;
            _result = objPaySetup.Result;
            _isSuccess = objPaySetup.IsSuccess;
            if ((!_isSuccess) && (_result != string.Empty))
            {
                if (_errorMsg == string.Empty)
                {
                    _errorMsg = _result;
                }
            }
            if (_isError == true)
            {
                switch (_errorNo)
                {
                    case 2601: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    case 2627: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    default:
                        break;
                }
            }      
        }
        #endregion

        #region Pay Period Category
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetPayPeriodCategory(int CompanyId, int PayPeriodCategoryId, string UserName)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPayPeriodCategory(CompanyId, PayPeriodCategoryId, UserName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }          
            return dtTable;
        }

        public DataTable GetPayPeriodCategory(int CompanyId, int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPayPeriodCategory(CompanyId, PayPeriodCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddPayPeriodCategory(int CompanyId, string PayPeriodCategory, string Remarks)
        {
            try
            {
                objPaySetup.AddPayPeriodCategory(CompanyId, PayPeriodCategory, Remarks);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void UpdatePayPeriodCategory(int PayPeriodCategoryId, int CompanyId, string PayPeriodCategory, string Remarks)
        {
            try
            {
                objPaySetup.UpdatePayPeriodCategory(PayPeriodCategoryId, CompanyId, PayPeriodCategory, Remarks);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
           
        }

        public void DeletePayPeriodCategory(int PayPeriodCategoryId)
        {
            try
            {
                objPaySetup.DeletePayPeriodCategory(PayPeriodCategoryId);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }
        #endregion

        #region Pay Periods

        public DataTable GetPayPeriod(int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPayPeriod(PayPeriodId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }


        public DataTable GetPayPeriods(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPayPeriods(PayPeriodCategoryId, PayPeriodId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
            return dtTable;
        }

        public DataTable GetPreviousPayPeriods(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPreviousPayPeriods(PayPeriodCategoryId, PayPeriodId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
            return dtTable;
        }
        public int GetPayPeriodIdByCode(string PayPeriodCode)
        {
            int PayPeriodId = 0;
            try
            {
                PayPeriodId = objPaySetup.GetPayPeriodsByPayPeriodCode(PayPeriodCode);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return PayPeriodId;
        }


        public DataTable GetUnCompletedPayPeriods(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetUnCompletedPayPeriods(PayPeriodCategoryId, PayPeriodId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetHistoryPayPeriod(int PayPeriodCategoryId, int PayPeriodId, int Year, int Month)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetHistoryPayPeriod(PayPeriodCategoryId, PayPeriodId, Year, Month);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
    
        public void AddPayPeriod(int PayPeriodCategoryId, string PayPeriodName, DateTime BeginDate, DateTime EndDate, int Month, int Year, string CurrencyCode, int PreviousPayPeriodId)
        {
            try
            {
                objPaySetup.AddPayPeriod(PayPeriodCategoryId, PayPeriodName, BeginDate, EndDate, Month, Year, CurrencyCode, PreviousPayPeriodId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void UpdatePayPeriod(int PayPeriodId, int PayPeriodCategoryId, string PayPeriodName, DateTime BeginDate, DateTime EndDate, int Month, int Year, string CurrencyCode, int PreviousPayPeriodId)
        {
            try
            {
                objPaySetup.UpdatePayPeriod(PayPeriodId, PayPeriodCategoryId, PayPeriodName, BeginDate, EndDate, Month, Year, CurrencyCode, PreviousPayPeriodId);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void DeletePayPeriod(int PayPeriodId, int PreviousPayPeriodId)
        {
            try
            {
                objPaySetup.DeletePayPeriod(PayPeriodId, PreviousPayPeriodId);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void PostPayPeriod(int PayPeriodId, bool Posted, string UserName)
        {
            try
            {
                objPaySetup.PostPayPeriod(PayPeriodId, Posted, UserName);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }

        public void MonthEndPayPeriod(int PayPeriodId, string UserName)
        {
            try
            {
                objPaySetup.MonthEndPayPeriod(PayPeriodId, UserName);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }
        #endregion 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetPaySetupCodes(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPaySetupCodes(PayPeriodCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable Pay_GetUserAssignedPayCategory(int CompanyId, int PayPeriodCategoryId, string UserName)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.Pay_GetUserAssignedPayCategory(CompanyId, PayPeriodCategoryId, UserName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable getSalControl(int OrgStructureID, int CompanyId, string EncryptionKey, int Month, int Year, string CatID, int OrgLevel, string UserName)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getSalControl(OrgStructureID, CompanyId, EncryptionKey, Month, Year, CatID, OrgLevel, UserName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getSignatureList(int OrgStructureID, int CompanyId, string EncryptionKey, int Month, int Year, string CatID, string UserName, int OrgLevel, int SalaryType)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getSignatureList(OrgStructureID, CompanyId, EncryptionKey, Month, Year, CatID, UserName, OrgLevel, SalaryType);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getSalBank(int OrgStructureID, int CompanyId, string EncryptionKey, int Month, int Year, string CatID,string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getSalBank(OrgStructureID, CompanyId, EncryptionKey, Month, Year, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getMaleFemale(int OrgStructureID, int CompanyId, string CatID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getMaleFemale(OrgStructureID, CompanyId, CatID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getMaleFemaleSummary(int OrgStructureID, int CompanyId, string CatID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getMaleFemaleSummary(OrgStructureID, CompanyId, CatID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getWCISummary(int OrgStructureID, int CompanyId, string EncryptionKey, string CatID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getWCISummary(OrgStructureID, CompanyId, EncryptionKey, CatID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getEPFCform(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getEPFCform(OrgStructureID, CompanyId, EncryptionKey, Year, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getEmployeePaySheet(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel, int EmployeeId, DateTime PaidDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getEmployeePaySheet(OrgStructureID, CompanyId, EncryptionKey, Year, Month, CatID, UserName, OrgLevel, EmployeeId, PaidDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable getEmployeePaySlip(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel, int EmployeeId, DateTime PaidDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getEmployeePaySlip(OrgStructureID, CompanyId, EncryptionKey, Year, Month, CatID, UserName, OrgLevel, EmployeeId, PaidDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getETFCform(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getETFCform(OrgStructureID, CompanyId, EncryptionKey, Year, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable getEmployeeContributionA4Report(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, string FieldStatus, int Semester, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getEmployeeContributionA4Report(OrgStructureID, CompanyId, EncryptionKey, Year, FieldStatus, Semester, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getEPFReport(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getETFCform(OrgStructureID, CompanyId, EncryptionKey, Year, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getETFReport(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getETFCform(OrgStructureID, CompanyId, EncryptionKey, Year, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getETFETFStatement(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getETFCform(OrgStructureID, CompanyId, EncryptionKey, Year, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getStampDuty(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getStampDuty(OrgStructureID, CompanyId, EncryptionKey, Year, EmployeeId, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable getStampDutyMonthly(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getStampDutyMonthly(OrgStructureID, CompanyId, EncryptionKey, Year, EmployeeId, Month, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getStampDutySemester(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, int Semester, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getStampDutySemester(OrgStructureID, CompanyId, EncryptionKey, Year, EmployeeId, Month, Semester, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable getStampDutyQuarter(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, int Quarter, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.getStampDutyQuarter(OrgStructureID, CompanyId, EncryptionKey, Year, EmployeeId, Month, Quarter, CatID, UserName, OrgLevel);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        #region UserFields
        public DataTable GetUserFields(int FieldId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetUserFields(FieldId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
            return dtTable;
        }

      
        public DataTable GetUserFields(string FieldType, int FieldId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetUserFields(FieldType, FieldId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddUserField(string FieldCode, string FieldDescription, string FieldType, string DataType, int Length, int DecimalPoints, bool IsNull, bool IsDeduction, bool IsLoanType, string DefaultValue, string CheckConstraints)
        {
            try
            {
                objPaySetup.AddUserField(FieldCode, FieldDescription, FieldType, DataType, Length, DecimalPoints, IsNull, IsDeduction, IsLoanType, DefaultValue, CheckConstraints);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateUserField(int FieldId, string FieldCode, string FieldDescription, string FieldType, string DataType, int Length, int DecimalPoints, bool IsNull, bool IsDeduction, bool IsLoanType, string DefaultValue, string CheckConstraints)
        {
            try
            {
                objPaySetup.UpdateUserField(FieldId, FieldCode, FieldDescription, FieldType, DataType, Length, DecimalPoints, IsNull, IsDeduction, IsLoanType, DefaultValue, CheckConstraints);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteUserField(int FieldId,string FieldCode, string FieldType, string DataType)
        {
            try
            {
                objPaySetup.DeleteUserField(FieldId, FieldCode, FieldType, DataType);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }
        #endregion

        #region  Payroll Field
        public DataTable GetPayrollFields(int PayCategoryId, int SequenceNo)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPayrollFields(PayCategoryId, SequenceNo);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPayrollFields(int IndexId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPayrollFields(IndexId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPayrollFunctions()
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetPayrollFunctions();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
            return dtTable;
        }


        public void PaySetupRowMove(long IndexId, string Status)
        {
            try
            {
                objPaySetup.PaySetupRowMove(IndexId, Status);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public void AddPayrollField(int PayCategoryId, string PaySetupCode, string PaySetupDescription, string Formula, int SeqNo, int DependOn, bool IsDeduct, string FunctionName, string ParameterList)
        {
            try
            {
                objPaySetup.AddPayrollField(PayCategoryId,PaySetupCode,PaySetupDescription,Formula,SeqNo,DependOn,IsDeduct,FunctionName,ParameterList);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdatePayrollField(int IndexId, string OLDPayCode, int PayCategoryId, string PaySetupCode, string PaySetupDescription, string Formula, int SeqNo, int DependOn, bool IsDeduct, string FunctionName, string ParameterList)
        {
            try
            {
                objPaySetup.UpdatePayrollField(IndexId, OLDPayCode,PayCategoryId, PaySetupCode, PaySetupDescription, Formula, SeqNo, DependOn, IsDeduct, FunctionName, ParameterList);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeletePayrollField(int IndexId, int PayCategoryId)
        {
            try
            {
                objPaySetup.DeletePayrollField(IndexId,PayCategoryId );
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region Application Session
        public DataTable GetApplicationSession(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPaySetup.GetApplicationSession(PayPeriodCategoryId, PayPeriodId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void SetApplicationSession(int PayPeriodCategoryId, int PayPeriodId, string CreatedUser)
        {
            try
            {
                objPaySetup.SetApplicationSession(PayPeriodCategoryId, PayPeriodId, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void SetApplicationSession(int PayPeriodCategoryId, int PayPeriodId, bool IsPosted, bool IsArchive, bool IsHistory)
        {
            try
            {
                objPaySetup.SetApplicationSession(PayPeriodCategoryId, PayPeriodId, IsPosted, IsArchive, IsHistory);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        #endregion  

        #endregion

        #region Destructor
        ~PaySetup()
        {           
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
