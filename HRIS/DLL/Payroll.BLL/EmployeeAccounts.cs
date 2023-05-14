/// <summary>
/// /// <summary>
/// /// /// <summary>
/// /// /// Copyright (c) 2000-2008 MasterKey Solutions .
/// /// /// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// /// /// All right received.
/// /// /// This software is the confidential and proprietary information of
/// /// /// MasterKey Solutions (Confidential Information). You shall not disclose such
/// /// /// Confidential Information and shall use it only in accordance with the
/// /// /// terms of the license agreement you entered into with MasterKey Solutions.
/// /// ///
/// /// /// Solution Name : =HRM.Payroll.BLL
/// /// /// Cording Standard : MasterKey Cording Standards
/// /// /// Author : Chiran Chamara
/// /// /// Created Timestamp :7/15/2011
/// /// /// Class Description : HRM.Payroll.BLL.EmployeeAccounts
/// /// /// </summary>
/// /// </summary>
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
   public  class EmployeeAccounts
    {
        #region Fields

        private EmployeeAccountsDAL _objEmployeeAccounts;

        private string _result;        
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public EmployeeAccounts()
        {
            _objEmployeeAccounts = new EmployeeAccountsDAL();
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
            _isError = _objEmployeeAccounts.IsError;
            _errorMsg = _objEmployeeAccounts.ErrorMsg;
            _errorNo = _objEmployeeAccounts.ErrorNo; 
        }


        #endregion

        #region External

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
        public void AddEmployeeAccounts(long EmployeeID, int BankId, int BranchId, string AccountCode, string NameAsPerBank, decimal Percentage)
        {
            _isError =false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeAccounts.AddEmployeePay( EmployeeID,  BankId,  BranchId,  AccountCode, NameAsPerBank,  Percentage);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateEmployeeAccounts(long EmployeeID, int BankId, int BranchId, string AccountCode, decimal Percentage)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeAccounts. UpdateEmployeePay( EmployeeID,  BankId,  BranchId,  AccountCode,  Percentage);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void DeleteEmployeeAccounts(long EmployeeID)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeAccounts.DeleteEmployeeAccounts(EmployeeID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmpoyeeAccounts(long EmployeeID)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            DataTable dtEmployeePay = new DataTable();
            try
            {
                dtEmployeePay = _objEmployeeAccounts.GetEmpoyeeAccounts(EmployeeID);
                SetValues();
            }
            catch
            { }

            return dtEmployeePay;
            
        }



        #endregion

        #endregion
    }
}
