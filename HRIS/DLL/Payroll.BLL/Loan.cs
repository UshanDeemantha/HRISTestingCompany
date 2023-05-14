/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 51, Flower Road, Colombo 7.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Payroll.BLL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 7/18/2011
/// Class Description   : Loan Related Data
/// Task Code List      : HRMV2P000020, 
/// </summary>
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using HRM.Payroll.DAL;
using System.ComponentModel;

namespace HRM.Payroll.BLL
{
    /// <summary>
    /// Loan Class(Manage Loan Related Data)
    /// </summary>
    [DataObject]
    public class Loan
    {
        #region Fields
        private LoanDAL objLoanDAL; 
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
        public Loan()
        {
            objLoanDAL = new LoanDAL();
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objLoanDAL.IsError;
            _errorNo = objLoanDAL.ErrorNo;
            _errorMsg = objLoanDAL.ErrorMsg;
            _result = objLoanDAL.Result;
            _isSuccess = objLoanDAL.IsSuccess;
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

        #region External
        #region Loan Types
        [DataObjectMethod(DataObjectMethodType.Select,true)]
        public DataTable GetLoanTypes(int LoanTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable=objLoanDAL.GetLoanTypes(LoanTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddLoanType(string LoanTypeCode, string LoanTypeName, decimal InterestRate, decimal MinLoanAmount, decimal MaxLoanAmount, string Remarks)
        {
            try
            {
                objLoanDAL.AddLoanType(LoanTypeCode, LoanTypeName,InterestRate, MinLoanAmount, MaxLoanAmount, Remarks);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateLoanType(int LoanTypeId, string LoanTypeCode, string LoanTypeName, decimal InterestRate, decimal MinLoanAmount, decimal MaxLoanAmount, string Remarks)
        {
            try
            {
                objLoanDAL.UpdateLoanType(LoanTypeId, LoanTypeCode, LoanTypeName,InterestRate, MinLoanAmount,MaxLoanAmount, Remarks);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteLoanType(int LoanTypeId)
        {
            try
            {
                objLoanDAL.DeleteLoanType(LoanTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion 

        #region Loan
        public DataTable GetEmployeeLoans(long EmployeeId, long LoanId, int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLoanDAL.GetEmployeeLoans(EmployeeId, LoanId, PayCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable ViewEmployeeLoans(long LoanId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLoanDAL.ViewEmployeeLoans(LoanId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeeLoansForSettlement(long EmployeeId, long LoanId, int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLoanDAL.GetEmployeeLoansForSettlement(EmployeeId, LoanId, PayCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetLoanEmployees(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLoanDAL.GetLoanEmployees(PayCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddEmployeeLoan(long EmployeeId, int payPeriod, int LoanTypeId, DateTime LoanTakenDate, DateTime TakenDate, DateTime EndDate, int NoOfPremiums, decimal LoanAmount,
          decimal Premium, decimal InterestRate, decimal SetleAmount, int DueNoofPremiums, decimal InterestAmount, bool isHold,
            bool isCompleted, bool IsNoOfPremiumCalculated, bool IsPremiumCalculated, string CreateUser, int ActualNoOfPremiums, decimal ActualPremium)
        {
            try
            {
                objLoanDAL.AddEmployeeLoan(EmployeeId, payPeriod, LoanTypeId, LoanTakenDate, TakenDate, EndDate, NoOfPremiums, LoanAmount,
            Premium, InterestRate, SetleAmount, DueNoofPremiums, InterestAmount, isHold, isCompleted, IsNoOfPremiumCalculated, IsPremiumCalculated, CreateUser, ActualNoOfPremiums, ActualPremium);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void ProcessLoan(long EmployeeId, int PayPeriod, string CreatedUser)
        {
            try
            {
                objLoanDAL.ProcessLoan(EmployeeId, PayPeriod, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateEmployeeLoan(long LoanId, DateTime EndDate, int NoOfPremiums, int ExistingPremiums, decimal Premium, decimal InterestRate, int PayPeriod, bool isHold, bool completed, bool minusHold, string UpdateUser)
        {
            try
            {
                objLoanDAL.UpdateEmployeeLoan(LoanId, EndDate, NoOfPremiums, ExistingPremiums, Premium, InterestRate, PayPeriod, isHold, completed, minusHold, UpdateUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteEmployeeLoan(long LoanId)
        {
            try
            {
                objLoanDAL.DeleteEmployeeLoan(LoanId);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
           
        }

        public void ManualLoanSettlement(long LoanId, decimal SettleAmount, decimal Premium, int DueNoofPremiums, decimal BalanceAmount, string Reference, string ApprovedBy,
            int? BankId, int? BankBranchId, string Remarks, DateTime SettleDate, int PayPeriodId, string CreatedUser)
        {
            try
            {
                objLoanDAL.ManualLoanSettlement(LoanId, SettleAmount, Premium, DueNoofPremiums, BalanceAmount, Reference, ApprovedBy,
            BankId, BankBranchId, Remarks, SettleDate, PayPeriodId, CreatedUser);
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
        #endregion

        #region Destructor
        ~Loan()
        {           
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
