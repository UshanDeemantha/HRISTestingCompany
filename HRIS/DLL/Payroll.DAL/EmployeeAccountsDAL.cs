/// <summary>
/// /// <summary>
/// /// /// <summary>
/// /// /// Copyright (c) 2000-2008 MasterKey Solutions .
/// /// /// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// /// /// All right received.
/// /// /// This software is the confidential and proprietary information of
/// /// /// MasterKey Solutions (Confidential Information). You shall not disclose such
/// /// /// Confidential Information and shall use it only in accordance with the
/// /// /// terms of the license agreement you entered into with MasterKey Solutions.
/// /// ///
/// /// /// Solution Name : =HRM.Payroll.DAL
/// /// /// Cording Standard : MasterKey Cording Standards
/// /// /// Author : Chiran Chamara
/// /// /// Created Timestamp :7/15/2011
/// /// /// Class Description : HRM.Payroll.DAL.EmployeeAccounts
/// /// /// </summary>
/// /// </summary>
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
    public class EmployeeAccountsDAL
    { 
        #region Fields

        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        private long _employeeId;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
        /// </summary>
        public EmployeeAccountsDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        public EmployeeAccountsDAL(int EmployeeId)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            _employeeId = EmployeeId;
        }
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
        public long EmployeeID
        {
            get
            {
                return _employeeId;
            }
            set
            {
                _employeeId = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        /// <summary>
        /// Adds the additional information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID. AS Long</param>
        public void AddEmployeePay(long EmployeeID, int BankId, int BranchId, string AccountCode, string NameAsPerBank, decimal Percentage)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeAccounts", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@AccountCode", AccountCode);
                    command.Parameters.AddWithValue("@NameAsPerBank", NameAsPerBank);
                    command.Parameters.AddWithValue("@Percentage", Percentage);
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
                _dbConnection.Close();
            }

        }

        /// <summary>
        /// Updates the additional information.
        /// </summary>
        /// <param name="EmoloyeeID">The emoloyee ID. As Long</param>
        public void UpdateEmployeePay(long EmployeeID, int BankId, int BranchId, string AccountCode, decimal Percentage)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_UpdateEmployeeAccounts", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@AccountCode", AccountCode);
                    command.Parameters.AddWithValue("@Percentage", Percentage);
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

        public void DeleteEmployeeAccounts(long EmployeeID)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_DeleteEmployeeAccounts", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
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

        /// <summary>
        /// Gets the additonal information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID . As Long</param>
        /// <returns></returns>
        public DataTable GetEmpoyeeAccounts(long EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetEmployeeAccountsByEmployeeID", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
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

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
        /// </summary>
        ~EmployeeAccountsDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
