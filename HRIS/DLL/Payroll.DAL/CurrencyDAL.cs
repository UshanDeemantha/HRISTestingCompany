/// <summary>
/// Copyright (c) 2000-2011 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
///
/// Solution Name: HRM.Payroll.DAL
/// Cording Standard: MasterKey Cording Standards
/// Author: Chiran Chamara
/// Author: Asanka C. Amarasinghe
/// Created Timestamp: 7/18/2011
/// Class Description: HRM.Payroll.DAL.CurrencyDAL
/// Task Code: HRMV2P000016
/// Task Code: HRMV2P000008
/// </summary>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.Payroll.DAL
{
  
    public class CurrencyDAL
    {
        #region Fields

        private SqlConnection dbConnection;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private long _rateId;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        }
        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        /// <value>The employee ID.</value>
        public long RateId
        {
            get
            {
                return _rateId;
            }
            set
            {
                _rateId = value;
            }
        }

        #endregion

        #region Constructor

        public CurrencyDAL()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        #endregion

        #region Methods

        #region Internal
        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
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

        #region Currency Rate (Task Code :HRMV2P000016)

        /// <summary>
        /// Adds the currency rate.
        /// </summary>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="CurrencyCode">The currency code.</param>
        /// <param name="Date">The date.</param>
        /// <param name="Rate">The rate.</param>
        /// <param name="UserName">Name of the user.</param>
        public void AddCurrencyRate(int PayPeriodId, string CurrencyCode, DateTime Date, decimal Rate, string UserName)
        {
            InitializeFields();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddCurrencyRate", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@Rate", Rate);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorMsg = sqlExp.Message;
                _errorNo = sqlExp.Number;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

        }


        /// <summary>
        /// Updates the currency rate.
        /// </summary>
        /// <param name="RateId">The rate id.</param>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="CurrencyCode">The currency code.</param>
        /// <param name="Date">The date.</param>
        /// <param name="Rate">The rate.</param>
        /// <param name="UserName">Name of the user.</param>
        public void UpdateCurrencyRate(long RateId, int PayPeriodId, string CurrencyCode, DateTime Date, decimal Rate, string UserName)
        {
            try
            {
                InitializeFields();
                using (SqlCommand command = new SqlCommand("PAY_UpdateCurrencyRate", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@RateId", RateId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@Rate", Rate);
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
                dbConnection.Close();
            }
        }

        /// <summary>
        /// Deletes the currency rate.
        /// </summary>
        /// <param name="RateId">The rate id.</param>
        public void DeleteCurrencyRate(long RateId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_DeleteCurrencyRate", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@RateId", RateId);
                    command.ExecuteNonQuery();
                }

            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorNo = sqlExp.Number;
                _errorMsg = sqlExp.Message;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// Gets the additonal information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID . As Long</param>
        /// <returns></returns>
        public DataTable GetCurrencyRate(long RateId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetCurrencyRateByRateId", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@RateId", RateId);
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
                dbConnection.Close();

            }
            return dtTable;
        }

        public DataTable GetCurrencyRateByPayPeriod(int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetCurrencyRateByPayPeriodId", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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
                dbConnection.Close();

            }
            return dtTable;
        }
        #endregion

        #region Currency Types (Task Code :HRMV2P000008)

        public int AddCurrencyType(string currencyCode, string currencyName, string country)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ResultID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddCurrencyType", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    command.Parameters.AddWithValue("@CurrencyName", currencyName);
                    command.Parameters.AddWithValue("@Country", country);

                    ResultID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return ResultID;
        }

        public void DeleteCurrencyType(string currencyCode)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteCurrencyType", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorNo = sqlExp.Number;
                _errorMsg = sqlExp.Message;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int EditCurrencyType(string currencyCode, string currencyName, string country)
        {
            _isError = false;
            _errorMsg = string.Empty;

            // return status -1 = if method returned an error
            // return status  0 = if tax id specified cannot be found
            // return status  1 = if records updated
            // return status  2 = if type specified already exists
            int ReturnID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_EditCurrencyTypes", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    command.Parameters.AddWithValue("@CurrencyName", currencyName);
                    command.Parameters.AddWithValue("@Country", country);

                    ReturnID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return ReturnID;
        }

        public DataTable GetCurrencyTypes(string currencyCode)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetCurrencyTypes", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
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
                dbConnection.Close();
            }

            return dtTable;
        }

        public DataTable GetCurrencyTypesByPayPeriod(int PayPeriodId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetCurrencyTypesByPayPeriodId", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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
                dbConnection.Close();
            }

            return dtTable;
        }


        public DataTable GetConvertionCurrencyTypes(string CurrencyCode)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetConvertionCurrencyTypes", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
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
                dbConnection.Close();
            }

            return dtTable;
        }

        #endregion

        #region Currency Notes 

        public int AddCurrencyNote(string currencyCode, string currencyNote, int value)
        {
            int ResultID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddCurrencyNotes", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    command.Parameters.AddWithValue("@CurrencyNote", currencyNote);
                    command.Parameters.AddWithValue("@Value", value);

                    command.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlex)
            {
                _isError = true;
                ResultID = sqlex.Number;
               
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return ResultID;
        }

        public void DeleteCurrencyNote(string currencyCode, string currencyNote)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteCurrencyNotes", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    command.Parameters.AddWithValue("@CurrencyNote", currencyNote);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorNo = sqlExp.Number;
                _errorMsg = sqlExp.Message;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void EditCurrencyNote(string currencyCode, string currencyNote, int value)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateCurrencyNotes", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    command.Parameters.AddWithValue("@CurrencyNote", currencyNote);
                    command.Parameters.AddWithValue("@Value", value);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorMsg = sqlex.Message;
                _errorNo = sqlex.Number;
            }    
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

        }

        public DataTable GetCurrencyNotes(string currencyCode, string currencyNote)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetCurrencyNotes", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    command.Parameters.AddWithValue("@CurrencyNote", currencyNote);
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
                dbConnection.Close();
            }

            return dtTable;
        }

    
        #endregion

        #endregion

        #endregion

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
        /// </summary>
        ~CurrencyDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
