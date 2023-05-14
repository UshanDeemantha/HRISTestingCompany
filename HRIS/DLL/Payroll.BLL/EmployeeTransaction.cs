/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
///
/// Solution Name : =HRM.Payroll.BLL
/// Cording Standard : MasterKey Cording Standards
/// Author : Chiran Chamara
/// Created Timestamp :7/22/2011
/// Class Description : HRM.Payroll.BLL.EmployeeTransaction
/// Task Code : HRMV2P000023
/// </summary> 

using System;
using System.ComponentModel;
using System.Data;
using HRM.Payroll.DAL;

namespace HRM.Payroll.BLL
{
    [DataObject]
    public class EmployeeTransaction
    {
        #region Fields
        private EmployeeTransactionDAL _objEmployeeTransaction;
        private string _result;        
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        #endregion

        #region Constructor
        public EmployeeTransaction()
        {
            _objEmployeeTransaction = new EmployeeTransactionDAL();
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
            _isError = _objEmployeeTransaction.IsError;
            _errorMsg = _objEmployeeTransaction.ErrorMsg;
            _errorNo = _objEmployeeTransaction.ErrorNo;
        }
        #endregion

        #region External
        /// <summary>
        /// Adds the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="AttendenceAllowans">The attendence allowans.</param>
        /// <param name="NoPayDays">The no pay days.</param>
        /// <param name="OTRate1">The OT rate1.</param>
        /// <param name="OTHRS1">The OTHR s1.</param>
        /// <param name="OTRate2">The OT rate2.</param>
        /// <param name="OTHrs2">The OT HRS2.</param>
        /// <param name="OTRate3">The OT rate3.</param>
        /// <param name="OTHrs3">The OT HRS3.</param>
        /// <param name="EPF">The EPF.</param>
        /// <param name="PayeTax">The paye tax.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        public void AddEmployeeTxn(long EmployeeId, int PayPeriodId, decimal AttendenceAllowans, decimal NoPayDays, decimal OTRate1, decimal OTHRS1, decimal OTRate2, decimal OTHrs2, decimal OTRate3, decimal OTHrs3, bool IsPosted)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeTransaction.AddEmployeeTxn( EmployeeId,  PayPeriodId,  AttendenceAllowans,  NoPayDays,  OTRate1,  OTHRS1,  OTRate2,  OTHrs2,  OTRate3,  OTHrs3, IsPosted);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetEmployeePayTransactions(int OrgStuctureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = _objEmployeeTransaction.GetEmployeePayTransactions(OrgStuctureID);
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
        /// Adds the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="AttendenceAllowans">The attendence allowans.</param>
        /// <param name="NoPayDays">The no pay days.</param>
        /// <param name="LateHrs">The late hrs</param>
        /// <param name="OTRate1">The OT rate1.</param>
        /// <param name="OTHRS1">The OTHR s1.</param>
        /// <param name="OTRate2">The OT rate2.</param>
        /// <param name="OTHrs2">The OT HRS2.</param>
        /// <param name="OTRate3">The OT rate3.</param>
        /// <param name="OTHrs3">The OT HRS3.</param>
        /// <param name="EPF">The EPF.</param>
        /// <param name="PayeTax">The paye tax.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        public void AddEmployeeTxn(long EmployeeId, int PayPeriodId, decimal AttendenceAllowans, decimal NoPayDays,decimal LateHrs, decimal OTRate1, decimal OTHRS1,
                                         decimal OTRate2, decimal OTHrs2, decimal OTRate3, decimal OTHrs3, bool IsPosted)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeTransaction.AddEmployeeTxn(EmployeeId, PayPeriodId, AttendenceAllowans, NoPayDays,LateHrs, OTRate1, OTHRS1, OTRate2, OTHrs2, OTRate3, OTHrs3, IsPosted);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="AttendenceAllowans">The attendence allowans.</param>
        /// <param name="NoPayDays">The no pay days.</param>
        /// <param name="OTRate1">The OT rate1.</param>
        /// <param name="OTHRS1">The OTHR s1.</param>
        /// <param name="OTRate2">The OT rate2.</param>
        /// <param name="OTHrs2">The OT HRS2.</param>
        /// <param name="OTRate3">The OT rate3.</param>
        /// <param name="OTHrs3">The OT HRS3.</param>
        /// <param name="EPF">The EPF.</param>
        /// <param name="PayeTax">The paye tax.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        public void UpdateEmployeeTxn(long EmployeeId, int PayPeriodId, decimal AttendenceAllowans, decimal NoPayDays,decimal LateHrs, decimal OTRate1, decimal OTHRS1, decimal OTRate2, decimal OTHrs2, decimal OTRate3, decimal OTHrs3, bool IsPosted)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeTransaction.UpdateEmployeeTxn( EmployeeId,  PayPeriodId,  AttendenceAllowans,  NoPayDays,LateHrs,  OTRate1,  OTHRS1,  OTRate2,  OTHrs2,  OTRate3,  OTHrs3,  IsPosted);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        
        /// <summary>
        /// Deletes the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        public void DeleteEmployeeTxn(long EmployeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeTransaction.DeleteEmployeeTxn(EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        
        /// <summary>
        /// Gets the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetEmployeeTxn(int PayPeriodId, long EmployeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeeTxn = new DataTable();
            try
            {
                dtEmployeeTxn = _objEmployeeTransaction.GetEmployeeTxn(PayPeriodId, EmployeeId);
                SetValues();
            }
            catch
            { }

            return dtEmployeeTxn;

        }


        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetEmployeeTxn(int PayCategoryId, string FieldName, string organizationFilterQuery, string designationFilterQuery, int CompanyId, int OrgStructureId, int PayPeriodId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtEmployeeTxn = new DataTable();
            try
            {
                dtEmployeeTxn = _objEmployeeTransaction.GetEmployeeTxn(PayCategoryId, FieldName, organizationFilterQuery, designationFilterQuery, CompanyId, OrgStructureId, PayPeriodId);
                SetValues();
            }
            catch
            { }

            return dtEmployeeTxn;

        }


        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetEmployeePayrollDatail(int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable=_objEmployeeTransaction.GetEmployeePayrollDatail(PayPeriodId, EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetEmployeePayrollDatail(DataTable DtDetail ,int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                if (DtDetail != null && DtDetail.Rows.Count > 0)
                {
                    dtTable = DtDetail;
                }
                else
                {
                    dtTable = _objEmployeeTransaction.GetEmployeePayrollDatail(PayPeriodId, EmployeeId);

                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddEmployeeTxn(long EmployeeId, int PayPeriodId, string FieldName, decimal Value, string EncryptionKey)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objEmployeeTransaction.AddEmployeeTxn(EmployeeId, PayPeriodId, FieldName, Value, EncryptionKey);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetPayrollSummery(int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = _objEmployeeTransaction.GetPayrollSummery(PayPeriodId, EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        #endregion
        #endregion
    }
}