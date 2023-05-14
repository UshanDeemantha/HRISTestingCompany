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
/// Created Timestamp   : 7/20/2011
/// Class Description   : AccpacIntergation
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
    /// Accpac Intergration Class
    /// </summary>
    [DataObject]
    public class AccpacIntergrate
    {
        #region Fields  
        private AccpacIntergrateDAL objAccPac;
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
        public AccpacIntergrate()
        {
            objAccPac = new AccpacIntergrateDAL();
        }     
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objAccPac.IsError;
            _errorNo = objAccPac.ErrorNo;
            _errorMsg = objAccPac.ErrorMsg;
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
        #region Accpac Categories        
        public DataTable GetEmployeeList(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objAccPac.GetEmployeeList(PayPeriodCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddCategory(string AccPacCategory, string EPFAccount, string DynamicAllowanceAccount, string OTAccount, string StaticAllowanceAccount, string AirfareDeductionAccount, string FestivalAdvanceAccount, string C3DeductionAccount, string GrativityAccount, string AirfareProvisionAccount,
                  string AirfareDeductionCredit, string FestivalAdvanceCredit, string C3DeductionCredit, string GrativityProvisionCredit, string AirfareProvisionCredit)
        {
            try
            {
                objAccPac.AddCategory(AccPacCategory, EPFAccount, DynamicAllowanceAccount, OTAccount, StaticAllowanceAccount, AirfareDeductionAccount, FestivalAdvanceAccount, C3DeductionAccount, GrativityAccount, AirfareProvisionAccount, AirfareDeductionCredit, FestivalAdvanceCredit, C3DeductionCredit, GrativityProvisionCredit, AirfareProvisionCredit);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateCategory(int AccPacCategoryId, string AccPacCategory, string EPFAccount, string DynamicAllowanceAccount, string OTAccount, string StaticAllowanceAccount, string AirfareDeductionAccount, string FestivalAdvanceAccount, string C3DeductionAccount, string GrativityAccount, string AirfareProvisionAccount,
                  string AirfareDeductionCredit, string FestivalAdvanceCredit, string C3DeductionCredit, string GrativityProvisionCredit, string AirfareProvisionCredit)
        {
            try
            {
                objAccPac.UpdateCategory(AccPacCategoryId, AccPacCategory, EPFAccount, DynamicAllowanceAccount, OTAccount, StaticAllowanceAccount, AirfareDeductionAccount, FestivalAdvanceAccount, C3DeductionAccount, GrativityAccount, AirfareProvisionAccount,AirfareDeductionCredit, FestivalAdvanceCredit, C3DeductionCredit, GrativityProvisionCredit, AirfareProvisionCredit);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteCategory(int AccPacCategoryId)
        {
            try
            {
                objAccPac.DeleteCategory(AccPacCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetCategory(int AccPacCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objAccPac.GetCategory(AccPacCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        } 
        #endregion

        #region Employee Accpac Code asign
        public void AsignEmployeeCategory(long EmployeeId, int AccPacCategoryId)
        {
            try
            {
                objAccPac.AsignEmployeeCategory(EmployeeId, AccPacCategoryId);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }

        public void RemoveEmployeeCategory(long EmployeeId, int AccPacCategoryId)
        {
            try
            {
                objAccPac.RemoveEmployeeCategory(EmployeeId, AccPacCategoryId);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        [DataObjectMethod(DataObjectMethodType.Select,true)]
        public DataTable GetEmployeeAccpacAccounts(long EmployeeId, int AccPacCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objAccPac.GetEmployeeAccpacAccounts(EmployeeId, AccPacCategoryId);
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
        public DataTable GetEmployeeAccpacAccounts(long EmployeeId, int AccPacCategoryId, string OrganizationFilter, string DesignationFilter)
        {
            if (OrganizationFilter == null)
            { OrganizationFilter = string.Empty; }
            if (DesignationFilter == null)
            { DesignationFilter = string.Empty; }
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objAccPac.GetEmployeeAccpacAccounts(EmployeeId, AccPacCategoryId, OrganizationFilter, DesignationFilter);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public int GetEmployeeAccpacCategory(long EmployeeId)
        {
            int accpacCategoryId = 0;
            try
            {
                accpacCategoryId = objAccPac.GetEmployeeAccpacCategory(EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
            return accpacCategoryId;
        }
       
        #endregion
        #endregion
        #endregion

        #region Destructor
        ~AccpacIntergrate()
        {           
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
