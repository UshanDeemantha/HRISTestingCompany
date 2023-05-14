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
/// Created Timestamp   : 7/18/2011
/// Class Description   : Bonus Related Data
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
    /// <summary>
    /// Bonus Class(Manage Bonus Related Data)
    /// </summary>
    [DataObject]
    public class Bonus
    {
        #region Fields
        private BonusDAL objBonusDAL; 
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;       
        #endregion
        
        #region Properties  
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
        #endregion

        #region Constructor
        public Bonus()
        {
            objBonusDAL = new BonusDAL();
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objBonusDAL.IsError;
            _errorNo = objBonusDAL.ErrorNo;
            _errorMsg = objBonusDAL.ErrorMsg;          
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
        public DataTable GetEmployeeBonus(int PayCategoryId, long EmployeeId, int Year, int Month)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objBonusDAL.GetEmployeeBonus(PayCategoryId, EmployeeId, Year, Month);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
            return dtTable;
        }

        public void AddEmployeeBonus(int CompanyId, int PayCategoryId, long EmployeeId, string CurrencyCode, DateTime BonusDate, decimal BonusAmount, decimal NoPayDays, decimal TaxDeduction, decimal BonusFinalAmount)
        {
            try
            {
                objBonusDAL.AddEmployeeBonus(CompanyId, PayCategoryId, EmployeeId, CurrencyCode, BonusDate, BonusAmount, NoPayDays, TaxDeduction, BonusFinalAmount);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void UpdateEmployeeBonus(long BonusId, long EmployeeId, string CurrencyCode, DateTime BonusDate, decimal BonusAmount, decimal NoPayDays, decimal TaxDeduction, decimal BonusFinalAmount)
        {
            try
            {
                objBonusDAL.UpdateEmployeeBonus(BonusId, EmployeeId, CurrencyCode,  BonusDate, BonusAmount, NoPayDays, TaxDeduction, BonusFinalAmount);
                SetValues();
            }          
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void DeleteEmployeeBonus(long BonusId)
        {
            try
            {
                objBonusDAL.DeleteEmployeeBonus(BonusId);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public DataTable GetEmployeeList(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objBonusDAL.GetEmployeeList(PayCategoryId);
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

        #region Destructor
        ~Bonus()
        {
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
