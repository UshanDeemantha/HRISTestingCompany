/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name : =HRM.Payroll.BLL
/// Cording Standard : MasterKey Cording Standards
/// Author : Chiran Chamara
/// Created Timestamp :7/15/2011
/// Class Description : HRM.Payroll.BLL.EmployeePay
/// Task Code :HRMV2P000011
/// </summary>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using HRM.Payroll.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.Payroll.BLL
{
    [DataObject]
    public class EmployeePay
    {
        #region Fields

        private EmployeePayDAL _objEmployeePay;

        private string _result;        
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private string _createdUser;

        #endregion

        #region Constructor

        public EmployeePay()
        {
            _objEmployeePay = new EmployeePayDAL();
        }
         #endregion

        #region Propeties
        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError
        {
            get { return _isError; }
        }

        public string CreatedUser
        {
            get
            {
                return _createdUser;
            }
            set
            {
                _createdUser = value;
            }
        }

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public int ErrorNo
        {
            get
            {
                return _errorNo;
            }
        }

        public string Result
        {
            get { return _result; }
        }
        #endregion
        
        #region Methods
        #region Internal

        private void SetValues()
        {
            _isError = _objEmployeePay.IsError;
            _errorMsg = _objEmployeePay.ErrorMsg;
            _errorNo = _objEmployeePay.ErrorNo;
           
            if (_isError == true)
            {
                switch (_errorNo)
                {
                    //case 2601: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                    //    break;
                    //case 2627: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                    //    break;
                    //default:
                    //    break;
                }
            }           
        }


        #endregion

        #region External

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmpDetail(int EmpId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = _objEmployeePay.GetEmpDetail(EmpId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        /// <summary>
        /// Adds the employee pay sheet.

        public void AddEmployeePaySheet(long EmployeeID, int PayPeriodCategoryId, decimal BasicSalary, decimal BRA01, decimal BRA02, decimal INCREMENT2018, decimal PERFORMANCEINCENTIVE2018, decimal TRAVELLINGINCENTIVE, decimal TELEPHONEINCENTIVE, decimal SALESINCENTIVE,
                                        decimal OTHERINCENTIVE, decimal LEAVEENCASHMENT, decimal PREVIOUSMONTH, decimal GROSSSALARY, decimal LOAN, decimal SALAADVANCE, decimal MEALS,decimal DATA, bool Posted, decimal NOPAY, decimal STAFFWELFAIR, decimal CONTRIBUTION )
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeePay.AddEmployeePaySheet(EmployeeID, PayPeriodCategoryId, BasicSalary, BRA01, BRA02, INCREMENT2018, PERFORMANCEINCENTIVE2018, TRAVELLINGINCENTIVE, TELEPHONEINCENTIVE, SALESINCENTIVE, OTHERINCENTIVE, LEAVEENCASHMENT, PREVIOUSMONTH,
                    GROSSSALARY, LOAN, SALAADVANCE, MEALS, DATA, Posted, NOPAY, STAFFWELFAIR, CONTRIBUTION);
                SetValues();
                //if (!_objEmployeePay.IsError)
                //{
                //    if (BankTranferRequired)
                //    {
                //        EmployeeAccounts objEmloyeeAcounts = new EmployeeAccounts();
                //        objEmloyeeAcounts.AddEmployeeAccounts(EmployeeID, BankId, BranchId, AccountNo, 100);
                //        if (objEmloyeeAcounts.IsError)
                //        {
                //            DeleteEmployeePay(EmployeeID);
                //        }
                //        SetValues();
                //    }
                //}


            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddEmployeeBankDetails(long EmployeeID, int BankId, int BranchId, string AccountNo, string NameAsPerBank)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            try
            {

                SetValues();
                EmployeeAccounts objEmloyeeAcounts = new EmployeeAccounts();
                objEmloyeeAcounts.AddEmployeeAccounts(EmployeeID, BankId, BranchId, AccountNo, NameAsPerBank, 100);


            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

       


        /// <summary>
        /// Adds the employee pay.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        /// <param name="BasicSalary">The basic salary.</param>
        /// <param name="FixedAllowance">The fixed allowance.</param>
        /// <param name="DailyWage">The daily wage.</param>
        /// <param name="StopPay">if set to <c>true</c> [stop pay].</param>
        /// <param name="PayPeriodCategoryId">The pay period category id.</param>
        /// <param name="Posted">if set to <c>true</c> [posted].</param>
        /// <param name="CurrencyCode">The currency code.</param>
        /// <param name="MaxAdvancePer">The max advance per.</param>
        /// <param name="BankTranferRequired">if set to <c>true</c> [bank tranfer required].</param>
        public void AddEmployeePay(long EmployeeID, decimal BasicSalary, decimal FixedAllowance, decimal DailyWage, bool StopPay, int PayPeriodCategoryId, bool Posted, string CurrencyCode, decimal MaxAdvancePer, bool BankTranferRequired, int BankId, int BranchId, string AccountNo, string NameAsPerBank, bool Hold, string EncryptionKey,int EPFRate1,int EPFRate2,int EPFRate3,int EPFRate4,bool Isepf)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            
            try
            {
                _objEmployeePay.CreatedUser = _createdUser;
                _objEmployeePay.AddEmployeePay(EmployeeID, BasicSalary, FixedAllowance, DailyWage, StopPay, PayPeriodCategoryId, Posted, CurrencyCode, MaxAdvancePer, BankTranferRequired,Hold, EncryptionKey, EPFRate1, EPFRate2, EPFRate3, EPFRate4, Isepf);
                SetValues();
                if (!_objEmployeePay.IsError)
                {
                    if (BankTranferRequired)
                    {
                        EmployeeAccounts objEmloyeeAcounts = new EmployeeAccounts();
                        objEmloyeeAcounts.AddEmployeeAccounts(EmployeeID, BankId, BranchId, AccountNo, NameAsPerBank, 100);
                        if (objEmloyeeAcounts.IsError)
                        {
                            DeleteEmployeePay(EmployeeID);
                        }
                        SetValues();
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddEmployeePayWhenAddEmployee(long EmployeeID, decimal BasicSalary, decimal FixedAllowance, decimal DailyWage, bool StopPay, int PayPeriodCategoryId, bool Posted, string CurrencyCode, decimal MaxAdvancePer, bool BankTranferRequired, int BankId, int BranchId, string AccountNo, string NameAsPerBank, bool Hold, string EncryptionKey, int EPFRate1, int EPFRate2, int EPFRate3, int EPFRate4)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            try
            {
                _objEmployeePay.CreatedUser = _createdUser;
                _objEmployeePay.AddEmployeePayWhenAddEmployee(EmployeeID, BasicSalary, FixedAllowance, DailyWage, StopPay, PayPeriodCategoryId, Posted, CurrencyCode, MaxAdvancePer, BankTranferRequired, Hold, EncryptionKey, EPFRate1, EPFRate2, EPFRate3, EPFRate4);
                SetValues();
                if (!_objEmployeePay.IsError)
                {
                    if (BankTranferRequired)
                    {
                        EmployeeAccounts objEmloyeeAcounts = new EmployeeAccounts();
                        objEmloyeeAcounts.AddEmployeeAccounts(EmployeeID, BankId, BranchId, AccountNo, NameAsPerBank, 100);
                        if (objEmloyeeAcounts.IsError)
                        {
                            DeleteEmployeePay(EmployeeID);
                        }
                        SetValues();
                    }
                }


            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateEmployeePay(long EmployeeID, decimal BasicSalary, decimal FixedAllowance, decimal DailyWage, bool StopPay, int PayPeriodCategoryId, bool Posted, string CurrencyCode, decimal MaxAdvancePer, bool BankTranferRequired, int BankId, int BranchId, string AccountNo, string NameAsPerBank, bool Hold, string EncryptionKey, int PayPeriodId,bool Isepf,int CostCenter)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            
            try
            {
                _objEmployeePay.CreatedUser = _createdUser;
                _objEmployeePay.UpdateEmployeePay(EmployeeID, BasicSalary, FixedAllowance, DailyWage, StopPay, PayPeriodCategoryId, Posted, CurrencyCode, MaxAdvancePer, BankTranferRequired, Hold, EncryptionKey, PayPeriodId, Isepf, CostCenter);
                SetValues();
                if (!_objEmployeePay.IsError)
                {
                    if (BankTranferRequired)
                    {
                        EmployeeAccounts objEmloyeeAcounts = new EmployeeAccounts();
                        objEmloyeeAcounts.AddEmployeeAccounts(EmployeeID, BankId, BranchId, AccountNo, NameAsPerBank, 100);
                        if (objEmloyeeAcounts.IsError)
                        {
                            if (objEmloyeeAcounts.ErrorNo == 2627)
                            {
                                objEmloyeeAcounts.UpdateEmployeeAccounts(EmployeeID, BankId, BranchId, AccountNo, 100);
                            }
                           
                        }
                        SetValues();
                    }
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
     
        public void DeleteEmployeePay(long EmployeeID)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeePay.DeleteEmployeePay(EmployeeID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeePayByEmployeeId(long EmployeeID)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeePay = new DataTable();
            try
            {
                dtEmployeePay=_objEmployeePay.GetEmpoyeePay(EmployeeID);
                SetValues();
            }
            catch
            { }

            return dtEmployeePay;
            
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetPayrollUnassignedEmployee(int CompanyId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeePay = new DataTable();
            try
            {
                dtEmployeePay = _objEmployeePay.GetPayrollUnassignedEmployee(CompanyId);
                SetValues();
            }
            catch
            { }

            return dtEmployeePay;

        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUserDefinedReport(int OrgStructureID, int CompanyId, int PayPeriodId, bool Active, string EncryptionKey)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeePay = new DataTable();

            try
            {
                dtEmployeePay = _objEmployeePay.GetUserDefinedReport(OrgStructureID, CompanyId, PayPeriodId,Active, EncryptionKey);
                SetValues();

            }
            catch
            { }

            return dtEmployeePay;

        }

        public DataTable GetEmployeeUserDefinedReport(int OrgStructureID, int CompanyId, bool Active)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeePay = new DataTable();

            try
            {
                dtEmployeePay = _objEmployeePay.GetEmployeeUserDefinedReport(OrgStructureID, CompanyId, Active);
                SetValues();

            }
            catch
            { }

            return dtEmployeePay;

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetPermissionViseEmpoyeePay(long EmployeeID, int CompanyID, int PayPeriodCatageryId, string UserName, string ApplicationCode, string EncryptionKey, int PayPeriodId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeePay = new DataTable();

            try
            {
                dtEmployeePay = _objEmployeePay.GetPermissionViseEmpoyeePay(EmployeeID, CompanyID, PayPeriodCatageryId, UserName, ApplicationCode, EncryptionKey, PayPeriodId);
                SetValues();

            }
            catch
            { }

            return dtEmployeePay;

        }
        public DataTable GetEmpoyeePayByPayPeriod(int PayPeriodCategoryId, string CurrencyCode)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeePay = new DataTable();
            try
            {
                dtEmployeePay = _objEmployeePay.GetEmpoyeePayByPayPeriod(PayPeriodCategoryId, CurrencyCode);
                SetValues();
            }
            catch
            { }

            return dtEmployeePay;

        }

        public void AddEmployeeTransactions(int PayPeriodId, long EmployeeId, string FieldType, decimal Value)
        {
            try
            {
                _objEmployeePay.AddEmployeeTransactions(PayPeriodId, EmployeeId, FieldType, Value);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddChangePayFieldValue(long EmployeeID, int PayPeriodId, string FieldName, string Percentage, string Amount, bool isPercentage, bool isFixfield,string EncryptKey, string CreatedUser)
        {
            try
            {
                _objEmployeePay.AddChangePayFieldValue(EmployeeID, PayPeriodId,FieldName, Percentage, Amount, isPercentage, isFixfield,EncryptKey, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateChangePayFieldValue(int Id, string Percentage, string Amount, bool isPercentage, string EncryptKey, string CreatedUser)
        {
            try
            {
                _objEmployeePay.UpdateChangePayFieldValue(Id, Percentage, Amount, isPercentage, EncryptKey, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteChangePayFieldValue(int Id)
        {
            try
            {
                _objEmployeePay.DeleteChangePayFieldValue(Id);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeesByPayroll(int PayCategoryId, long EmployeeId, string FieldName, string OrganizationList, string DesignationList, int CompanyId, int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = _objEmployeePay.GetEmployeesByPayroll(PayCategoryId, EmployeeId, FieldName, OrganizationList, DesignationList, CompanyId, OrgStructureID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }



        public DataTable GetChangePayFieldValue(int PayCategoryId, int PayPeriodId, string EncryptKey)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = _objEmployeePay.GetChangePayFieldValue(PayCategoryId, PayPeriodId, EncryptKey);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetFixFields()
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = _objEmployeePay.GetFixFields();
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetVariableFields()
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = _objEmployeePay.GetVariableFields();
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddEmployeeStaticData(long EmployeeId, string FieldName, decimal Value, string EncryptionKey, string CreatedUser)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeePay.AddEmployeeStaticData(EmployeeId, FieldName, Value, EncryptionKey, CreatedUser);
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
    }
}