/// <summary>
/// Copyright (c) 2010-2011 MasterKey Solutions .
/// MasterKey Solutions, 51, Flower Road, Colombo 7.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Payroll.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 8/05/2011
/// Class Description   : Archive [MonthEnd Process]
/// Task Code List      : 
/// </summary>
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace HRM.Payroll.DAL
{
    public class ArchiveDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private string _result;
        private bool _isSuccess;
        #endregion
        
        #region Properties
        public bool IsError
        {
            get { return _isError; }
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
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
        public ArchiveDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        } 
        #endregion

        #region Methods
        #region Internal
        private void OpenDb()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            InitializeFields();
        }

        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = int.MinValue;
            _isSuccess=true;
            _result=string.Empty;
        } 
        #endregion

        public DataTable GetPaySetup(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPaySetup", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
            return dtTable;
        }

        public DataTable GetEmployeeList(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
            return dtTable;
        }
       
        public void ArchiveRecord(int PayPeriodId, long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_ArchiveRecord", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);                 
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void SetEmployeeResign(int PayPeriodId, long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_SetEmployeeResign", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

      
        public void ClearTransaction(int PayPeriodId)
        {           
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_CleanPayroll", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.ExecuteNonQuery();                                   
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {             
                _dbConnection.Close();
            }         
        }

        public void EndPayPeriod(int PayPeriodId, string UserName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_MonthEndPayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
        }     
        #endregion

        #region Destructor
        ~ArchiveDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
