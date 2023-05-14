/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions.
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
/// Created Timestamp   : 8/05/2011
/// Class Description   : Archive [Month End Process]
/// Task Code List      : 
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
    ///<summary>
    ///Archive (Month End Process)
    /// </summary>   
    public class Archive
    {
        #region Fields
        private ArchiveDAL objArchive; 
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private string _result;
        private bool _isSuccess;
        private int _payPeriodId;      
        #endregion
        
        #region Properties
        public int PayPeriodId
        {
            get { return _payPeriodId; }
            set { _payPeriodId = value; }
        }

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
        public Archive()
        {
            objArchive = new ArchiveDAL();
        }

        public Archive(int PayPeriodId)
        {
            objArchive = new ArchiveDAL();
            _payPeriodId = PayPeriodId;
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objArchive.IsError;
            _errorNo = objArchive.ErrorNo;
            _errorMsg = objArchive.ErrorMsg;
            _result = objArchive.Result;
            _isSuccess = objArchive.IsSuccess;
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
        public DataTable GetEmployeeList(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objArchive.GetEmployeeList(PayPeriodCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _isSuccess = false;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void ArchivePay(int PayPeriodCategoryId, int PayPeriodId, long EmployeeId)
        {
            try
            {
                LoanDAL objLoanNew = new LoanDAL();
                DataTable dtPaySetup = objArchive.GetPaySetup(PayPeriodCategoryId);
                if (dtPaySetup.Rows.Count <= 0)
                {
                    _isError = true;
                    _errorMsg = "Pay Setup Not Define!";
                    return;
                }

                for (int x = 0; x < dtPaySetup.Rows.Count; x++)
                {
                    // if calculate from Function
                    #region Function
                    string funcName = dtPaySetup.Rows[x]["FunctionName"].ToString().ToUpper();
                    switch (funcName)
                    {
                        case "LOAN": LoanDAL objLoan = new LoanDAL();
                            try
                            {
                                //objLoan.CheckLoanFinalize(EmployeeId);
                            }
                            catch (Exception ex)
                            {
                                _isError = true;
                                _errorMsg = ex.Message;
                            }
                            finally
                            {
                                if (objLoan != null)
                                {
                                    GC.SuppressFinalize(objLoan);
                                }
                            }
                            break;

                        case "SALADV()": SalaryAdvanceDAL objSalAdv = new SalaryAdvanceDAL();
                            try
                            {
                                objSalAdv.FinalizeSalaryAdvance(EmployeeId, PayPeriodId);
                            }
                            catch (Exception ex)
                            {
                                _isError = true;
                                _errorMsg = ex.Message;
                            }
                            finally
                            {
                                if (objSalAdv != null)
                                {
                                    GC.SuppressFinalize(objSalAdv);
                                }
                            }
                            break;

                        case "FESADV": FestivalAdvanceDAL objFesAdv = new FestivalAdvanceDAL();
                            try
                            {
                                objFesAdv.CheckFestivalAdvanceFinalize(EmployeeId);
                            }
                            catch (Exception ex)
                            {
                                _isError = true;
                                _errorMsg = ex.Message;
                            }
                            finally
                            {
                                if (objFesAdv != null)
                                {
                                    GC.SuppressFinalize(objFesAdv);
                                }
                            }
                            break;

                        case "STDORD": StandingOrdersDAL objStdOrd = new StandingOrdersDAL();
                            try
                            {
                                objStdOrd.CheckStandingOrderFinalize(EmployeeId);
                            }
                            catch (Exception ex)
                            {
                                _isError = true;
                                _errorMsg = ex.Message;
                            }
                            finally
                            {
                                if (objStdOrd != null)
                                {
                                    GC.SuppressFinalize(objStdOrd);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                objLoanNew.CheckLoanFinalize(EmployeeId, PayPeriodId);
                objArchive.ArchiveRecord(PayPeriodId, EmployeeId);
                objArchive.SetEmployeeResign(PayPeriodId, EmployeeId);
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _isError = true;
                _errorMsg = ex.Message;
            } 
        }

        public void ClearTransaction(int PayPeriodId)
        {
            try
            {
                objArchive.ClearTransaction( PayPeriodId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void EndPayPeriod(int PayPeriodId, string UserName)
        {
            try
            {
                objArchive.EndPayPeriod(PayPeriodId, UserName);
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
        ~Archive()
        {           
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
