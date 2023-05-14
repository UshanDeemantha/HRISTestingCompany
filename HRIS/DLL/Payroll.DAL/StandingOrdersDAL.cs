
  /// <summary>
  /// Copyright (c) 2000-2008 MasterKey Solutions .
  /// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
  /// All right received.
  /// This software is the confidential and proprietary information of
  /// MasterKey Solutions (Confidential Information). You shall not disclose such
  /// Confidential Information and shall use it only in accordance with the
  /// terms of the license agreement you entered into with MasterKey Solutions.
  ///
  /// Solution Name : =HRM.Payroll.DAL
  /// Cording Standard : MasterKey Cording Standards
  /// Author : Chiran Chamara
  /// Created Timestamp :7/21/2011
  /// Class Description : HRM.Payroll.DAL.StandingOrdersDAL
 //// Task Code :HRMV2P000022
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
    public class StandingOrdersDAL
    {
        #region Fields

        private SqlConnection dbConnection;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private long _standingOrderId;

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
        public long StandingOrderId
        {
            get
            {
                return _standingOrderId;
            }
            set
            {
                _standingOrderId = value;
            }
        }

        #endregion

        #region Constructor

        public StandingOrdersDAL()
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
        /// <summary>
        /// Adds the standing order.
        /// </summary>
        /// <param name="StandingOrderNo">The standing order no.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="TotalAmount">The total amount.</param>
        /// <param name="StandingOrderAmount">The standing order amount.</param>
        /// <param name="Premium">The premium.</param>
        /// <param name="NoOfPremiums">The no of premiums.</param>
        /// <param name="DueNoofPremiums">The due noof premiums.</param>
        /// <param name="SettleAmount">The settle amount.</param>
        /// <param name="FromPayPeriodId">From pay period id.</param>
        /// <param name="ToPayPeriodId">To pay period id.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="BankId">The bank id.</param>
        /// <param name="BranchId">The branch id.</param>
        /// <param name="Account">The account.</param>
        /// <param name="CurrentPayment">The current payment.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        /// <param name="IsCompleted">if set to <c>true</c> [is completed].</param>
        public void AddStandingOrder(long EmployeeId, decimal StandingOrderAmount, int BankId, int BranchId, string Account, bool Active, string user)
        {            
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddStandingOrders", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;    
                   
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@StandingOrderAmount", StandingOrderAmount);
                   command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@Account", Account);
                    command.Parameters.AddWithValue("@Active", Active);
                    SqlParameter standingOrderId = new SqlParameter("@StandingOrderId", SqlDbType.BigInt, 16);
                    standingOrderId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(standingOrderId);
                    command.ExecuteNonQuery();
                    _standingOrderId = Convert.ToInt64(standingOrderId.Value);
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
        /// Updates the standing order.
        /// </summary>
        /// <param name="StandingOrderId">The standing order id.</param>
        /// <param name="StandingOrderNo">The standing order no.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="TotalAmount">The total amount.</param>
        /// <param name="StandingOrderAmount">The standing order amount.</param>
        /// <param name="Premium">The premium.</param>
        /// <param name="NoOfPremiums">The no of premiums.</param>
        /// <param name="DueNoofPremiums">The due noof premiums.</param>
        /// <param name="SettleAmount">The settle amount.</param>
        /// <param name="FromPayPeriodId">From pay period id.</param>
        /// <param name="ToPayPeriodId">To pay period id.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="BankId">The bank id.</param>
        /// <param name="BranchId">The branch id.</param>
        /// <param name="Account">The account.</param>
        /// <param name="CurrentPayment">The current payment.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        /// <param name="IsCompleted">if set to <c>true</c> [is completed].</param>
        public void UpdateStandingOrder(Int64 standingOrderId, Int64 EmployeeId, decimal StandingOrderAmount, int BankId, int BranchId, string Account, bool Active, bool IsPosted,
                                      string modifieduser)
        {
            try
            {               
                using (SqlCommand command = new SqlCommand("PAY_UpdateStandingOrders", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;   
                    command.Parameters.AddWithValue("@StandingOrderId", StandingOrderId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@StandingOrderAmount", StandingOrderAmount);
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@Account", Account);
                    command.Parameters.AddWithValue("@IsPosted", Active);
                    command.Parameters.AddWithValue("@IsCompleted", Active);
                    command.Parameters.AddWithValue("@ModifyUser", modifieduser);
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

        public void SettleStandingOrders(long EmployeeId, int PayPeriodId, string PaySetupCode, bool Posted)
        {
            try
            {                
                using (SqlCommand command = new SqlCommand("Pay_SettleEmployeeStandingOrder", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@Posted", Posted);                    
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
        /// Deletes the standing order.
        /// </summary>
        /// <param name="StandingOrderId">The standing order id.</param>
        public void DeleteStandingOrder(long StandingOrderId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_DeleteStandingOrder", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;      
                    command.Parameters.AddWithValue("@StandingOrderId", StandingOrderId);
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
        /// Gets the standing order by employee.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <returns></returns>
        public DataTable GetStandingOrderByEmployee(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetStandingOrderByEmployeeId", dbConnection))
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

        public void CheckStandingOrderFinalize(long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_CheckStandingOrderFinalize", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
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
                dbConnection.Close();
            }
        }
        #endregion

        #endregion

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
        /// </summary>
        ~StandingOrdersDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
