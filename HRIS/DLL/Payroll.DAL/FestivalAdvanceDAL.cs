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
/// Class Description : HRM.Payroll.DAL.FestivalAdvanceDAL
/// Task Code: HRMV2P000018

namespace HRM.Payroll.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;


    public class FestivalAdvanceDAL
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
        public FestivalAdvanceDAL()
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

        /// <summary>
        /// Add new festival advance
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="takenDate">Taken Date</param>
        /// <param name="festivalAdvanceAmount">Festival Advance Amount</param>
        /// <param name="startDate">Festival Advance Reduction Start Date</param>
        /// <param name="endDate">Festival Advance Reduction End Date</param>
        /// <param name="startPayPeriodId">Festival Advance Reduction Start Pay Period</param>
        /// <param name="endPayPeriodId">Festival Advance Reduction End Pay Period</param>
        /// <param name="noOfPremiums">Festival Advance Reduction, Number of Premiums</param>
        /// <param name="premium">Single Premium Amount</param>
        /// <returns>Created Festival Advance ID</returns>
        public Int64 AddFestivalAdvance(Int64 employeeId, int AdvanceTypeId, DateTime takenDate, decimal festivalAdvanceAmount, DateTime startDate, DateTime endDate, int noOfPremiums, decimal premium, bool IsWorkFolw)
        {
            _isError = false;
            _errorMsg = string.Empty;

            Int64 CreatedIndexID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddFestivalAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@AdvanceTypeId", AdvanceTypeId);
                    command.Parameters.AddWithValue("@TakenDate", takenDate);
                    command.Parameters.AddWithValue("@FestivalAdvanceAmount", festivalAdvanceAmount);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@NoOfPremiums", noOfPremiums);
                    command.Parameters.AddWithValue("@Premium", premium);
                    command.Parameters.AddWithValue("@IsWorkFolw", IsWorkFolw);

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

        public int UpdateFestivalAdvance(Int64 festivalAdvanceId, Int32 AdvanceTypeId, DateTime takenDate, decimal festivalAdvanceAmount, DateTime startDate, DateTime endDate, int noOfPremiums,int dueNoofPremiums,int NewDueNoFoPrimiums, decimal premium)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ReturnValue = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateFestivalAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    
                    command.Parameters.AddWithValue("@FestivalAdvanceId", festivalAdvanceId);
                    command.Parameters.AddWithValue("@AdvanceTypeId", AdvanceTypeId);
                    command.Parameters.AddWithValue("@TakenDate", takenDate);
                    command.Parameters.AddWithValue("@FestivalAdvanceAmount", festivalAdvanceAmount);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@NoOfPremiums", noOfPremiums);
                    command.Parameters.AddWithValue("@DueNoofPremiums", dueNoofPremiums);
                    command.Parameters.AddWithValue("@NewDueNoofPremiums", NewDueNoFoPrimiums);
                    command.Parameters.AddWithValue("@Premium", premium);

                    ReturnValue = Convert.ToInt32(command.ExecuteScalar());
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

            return ReturnValue;
        }

        public void SettleFestivalAdvance(long EmployeeId, int PayPeriodId, string PaySetupCode, bool Posted)
        {
            try
            {

                using (SqlCommand command = new SqlCommand("Pay_SettleEmployeeFestivalAdvance", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

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

        public int DeleteFestivalAdvance(Int64 festivalAdvanceId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ReturnValue = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteFestivalAdvance", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@FestivalAdvanceId", festivalAdvanceId);

                    ReturnValue = Convert.ToInt32(command.ExecuteScalar());
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

            return ReturnValue;
        }

        /// <summary>
        /// Get pay periods for combo, for display
        /// </summary>
        /// <returns></returns>
        public DataTable GetPayPeriodsForCombo()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayPeriodsForCombo", dbConnection))
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

        /// <summary>
        /// Get saved festival advances of specified employee...
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns></returns>
        public DataTable GetFestivalAdvancesForEmployee(Int64 employeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetFestivalAdvancesForEmployee", dbConnection))
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

        public void CheckFestivalAdvanceFinalize(long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_CheckFestivalAdvanceFinalize", dbConnection))
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

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FestivalAdvanceDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~FestivalAdvanceDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
