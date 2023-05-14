/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name : HRM.Payroll.DAL
/// Cording Standard : MasterKey Cording Standards
/// Author : Asanka C. Amarasinghe
/// Created Timestamp : 20-July-2011
/// Class Description : HRM.Payroll.DAL.SalaryAdvanceDAL
/// Task Code: HRMV2P000019


namespace HRM.Payroll.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;


    public class SalaryAdvanceDAL
    {
        #region Fields

        private SqlConnection dbConnection;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxTypesDAL"/> class.
        /// </summary>
        public SalaryAdvanceDAL()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>Occurred Error Message As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        /// <summary>
        /// Gets the Error Number.
        /// </summary>
        /// <value>Occurred Error as a Numeric Representation</value>
        public int ErrorNo
        {
            get { return _errorNo; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }

        public Int64 AddSalaryAdvance(Int64 employeeId, DateTime takenDate, int payPeriodId, decimal amount, bool IsWorkflow)
        {
            _isError = false;
            _errorMsg = string.Empty;

            Int64 CreatedIndexID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddSalaryAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@TakenDate", takenDate);
                    command.Parameters.AddWithValue("@PayPeriodId", payPeriodId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@IsWorkflow", IsWorkflow);

                    CreatedIndexID = Convert.ToInt64(command.ExecuteScalar());
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

            return CreatedIndexID;
        }
      
        public Int64 UpdateSalaryAdvance(Int64 SalaryAdvanceId, Int64 employeeId, DateTime takenDate, int payPeriodId, decimal amount)
        {
            _isError = false;
            _errorMsg = string.Empty;
            Int64 CreatedIndexID = -1;
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_UpdateSalaryAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@SalaryAdvanceId", SalaryAdvanceId);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@TakenDate", takenDate);
                    command.Parameters.AddWithValue("@PayPeriodId", payPeriodId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    CreatedIndexID = Convert.ToInt64(command.ExecuteScalar());
                   
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

            return CreatedIndexID;
        }

        public void SettleSlaryAdvance(long EmployeeId, int PayPeriodId, string PaySetupCode, bool Posted)
        {
            try
            {

                using (SqlCommand command = new SqlCommand("Pay_SettleEmployeeSalaryAdvance", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    //command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@EncryptionKey", "007London");
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

        public void DeleteSalaryAdvance(Int64 SalaryAdvanceId)
        {
            _isError = false;
            _errorMsg = string.Empty;
           
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_DeleteSalaryAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@SalaryAdvanceId", SalaryAdvanceId);
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

        public DataTable GetLastPostedPayperiod(int PayCategoryId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetLastPostedPayperiod", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
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

        public void AddAdvance(long EmployeeId, string AdvanceType, decimal Amount, DateTime StartDate, DateTime EndDate, int NoOfInstallments, bool IsCompleted, bool IsHold, string EncryptionKey, string CreatedUser)
        {
            _isError = false;
            _errorMsg = string.Empty;
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@AdvanceType", AdvanceType);
                    command.Parameters.AddWithValue("@Amount", Amount);
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@NoOfInstallments", NoOfInstallments);
                    command.Parameters.AddWithValue("@IsCompleted", IsCompleted);
                    command.Parameters.AddWithValue("@IsHold", IsHold);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);


                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 100);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    SqlParameter isError = new SqlParameter("@IsError", SqlDbType.Bit);
                    isError.Direction = ParameterDirection.Output;
                    command.Parameters.Add(isError);

                    command.ExecuteNonQuery();
                    _isError = Convert.ToBoolean(isError.Value);
                    _errorMsg = result.Value.ToString();

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

        public void UpdateAdvance(int Id, bool IsCompleted, bool IsHold, string CreatedUser)
        {
            _isError = false;
            _errorMsg = string.Empty;
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@IsCompleted", IsCompleted);
                    command.Parameters.AddWithValue("@IsHold", IsHold);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);

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

        public DataTable GetUserField()
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetDeductionUserFields", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
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

        public DataTable GetAdvance(int Id, int PayCategoryId, string EncryptionKey, int CompanyId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
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

        public DataTable GetMaxAdvanceAmount(long EmployeeId, string EncryptionKey)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetMaxAdvanceAmount", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
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

        public DataTable GetNotCompletedAdvances(long EmployeeId, string AdvanceType)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetNotCompletedAdvances", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@AdvanceType", AdvanceType);
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

        public DataTable CheckSalaryPost(long EmployeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_CheckSalaryPost", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        public void DeleteAdvance(int Id)
        {
            _isError = false;
            _errorMsg = string.Empty;
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@Id", Id);
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
        /// Get saved salary advances of specified employee...
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable GetSalaryAdvancesForEmployee(Int64 employeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetSalaryAdvancesForEmployee", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
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

        /// <summary>
        /// Get pay periods for combo, for display
        /// </summary>
        /// <returns></returns>
        public DataTable GetPayPeriodsForSalaryAdvanceCombo(Int64 employeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayPeriodsForSalaryAdvanceCombo", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
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
   
        public void FinalizeSalaryAdvance(long EmployeeId, int PayPeriodId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_ClearSalaryAdvance", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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
                dbConnection.Close();
            }
        }
        #endregion

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FestivalAdvanceDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~SalaryAdvanceDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
