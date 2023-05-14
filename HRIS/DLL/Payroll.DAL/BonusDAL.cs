/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Payroll.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 7/18/2011
/// Class Description   : Bonus Related Data
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
    public class BonusDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
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
        public BonusDAL()
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
        }
        #endregion

        #region External
        public DataTable GetEmployeeBonus(int PayCategoryId, long EmployeeId, int Year, int Month)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeBonus", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
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

        public void AddEmployeeBonus(int CompanyId, int PayCategoryId, long EmployeeId, string CurrencyCode, DateTime BonusDate, decimal BonusAmount, decimal NoPayDays, decimal TaxDeduction, decimal BonusFinalAmount)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddEmployeeBonus", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@BonusDate", BonusDate);
                    command.Parameters.AddWithValue("@BonusAmount", BonusAmount);
                    command.Parameters.AddWithValue("@NoPayDays", NoPayDays);
                    command.Parameters.AddWithValue("@TaxDeduction", TaxDeduction);
                    command.Parameters.AddWithValue("@BonusFinalAmount", BonusFinalAmount);                   
                    command.ExecuteNonQuery();                   
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void UpdateEmployeeBonus(long BonusId, long EmployeeId, string CurrencyCode, DateTime BonusDate, decimal BonusAmount, decimal NoPayDays, decimal TaxDeduction, decimal BonusFinalAmount)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateEmployeeBonus", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BonusId", BonusId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@BonusDate", BonusDate);
                    command.Parameters.AddWithValue("@BonusAmount", BonusAmount);
                    command.Parameters.AddWithValue("@NoPayDays", NoPayDays);
                    command.Parameters.AddWithValue("@TaxDeduction", TaxDeduction);
                    command.Parameters.AddWithValue("@BonusFinalAmount", BonusFinalAmount);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void DeleteEmployeeBonus(long BonusId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteEmployeeBonus", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BonusId", BonusId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public DataTable GetEmployeeList(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayCategoryId);                   
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

        #endregion
        #endregion

        #region Destructor
        ~BonusDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}