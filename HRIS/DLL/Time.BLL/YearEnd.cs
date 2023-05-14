/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, #51, Flower Road, Colombo 7.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Payroll.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 11/20/2011
/// Class Description   : Year End/MonthEnd Process
/// Task Code List      : 
/// </summary>
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HRM.Time.DAL;
using HRM.Common.BLL;

namespace HRM.Time.BLL
{
    /// <summary>
    /// Year End/Month End Class
    /// </summary> 
    public class YearEnd
    {
        #region Fields
        private YearEndDAL objYearEnd;
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;       
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <remarks>true is an Error, Otherwise false</remarks>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the Error No.
        /// </summary>
        /// <remarks>ErrorNo As int</remarks>
        public int ErrorNo
        {
            get { return _errorNo; }
        }

        /// <summary>
        /// Gets the Error Message.
        /// </summary>
        /// <remarks>ErrorMsg As string</remarks>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }      
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="YearEnd"/> class.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <remarks></remarks>
        public YearEnd(int CompanyId)
        {
            objYearEnd = new YearEndDAL(); 
        }
        #endregion

        #region Methods
        #region Internal     
        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = int.MinValue;           
        }

        private void SetValues()
        {
            if (!IsError)
            {
                _isError = objYearEnd.IsError;
                _errorMsg = objYearEnd.ErrorMsg;
                _errorNo = objYearEnd.ErrorNo;               
            }
        }     
        #endregion

        #region External
        /// <summary>
        /// Get Employee List
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>       
        public DataTable GetEmployeeList(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objYearEnd.GetEmployeeList(CompanyId);
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
        /// Year End specified company.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <param name="EmployeeId">EmployeeId As long</param> 
        public void YearEndProcess(int CompanyId, long EmployeeId, string UserName)
        {
            try
            {
                objYearEnd.YearEndProcess(CompanyId, EmployeeId, UserName);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }          
        }

        /// <summary>
        /// Month End specified company.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <param name="EmployeeId">EmployeeId As long</param>
        public void MonthEndProcess(int CompanyId, long EmployeeId, string UserName)
        {
            try
            {
                objYearEnd.MonthEndProcess(CompanyId, EmployeeId, UserName);
                SetValues();
            }         
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        /// <summary>
        /// Archives the Records in specified company.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <param name="EmployeeId">EmployeeId As long</param>
        /// <param name="FromDate">FromDate As DateTime</param>       
        public void Archive(int CompanyId, long EmployeeId, DateTime FromDate, string UserName)
        {
            try
            {
                objYearEnd.Archive(CompanyId, EmployeeId, FromDate, UserName);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void AddToTimeLog(Type TypeName, string UserName)
        {
            try
            {
                objYearEnd.AddToTimeLog(TypeName.ToString(), UserName);
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
        ~YearEnd()
        {
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}