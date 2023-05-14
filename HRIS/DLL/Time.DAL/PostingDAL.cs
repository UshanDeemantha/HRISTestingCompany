/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Time.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 11/20/2011
/// Class Description   : Posting [Main Processing/Unprocessing]
/// Task Code List      : 
/// </summary>
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace HRM.Time.DAL
{
    public class PostingDAL
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
        public PostingDAL()
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

        private void SetError(SqlException Ex)
        {           
            _isError = true;
            _errorMsg = Ex.Message;            
            _errorNo = Ex.Number;
            switch (Ex.Number)
            {
                case 2601: _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 2627: _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 547: _errorMsg = "Can not Delete. Alredy Assign!";
                    break;
                default: break;
            }
        }
        #endregion

        public DataTable GetDevicesList()
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetDevicesList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
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
        public DataTable GetDevicesIP(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetIPList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public void GetTimeAllTimeLogs(long EmployeeId, string CardDate, string time, DateTime CardDateTime, int AttendancesType, string Location)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetDevicesAttLogs", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CardDate", CardDate);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@CardDateTime", CardDateTime);
                    command.Parameters.AddWithValue("@AttendancesType", AttendancesType);
                    command.Parameters.AddWithValue("@Location", Location);
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
        public DataTable GetEmployeeList(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetEmployeeList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public DataTable GetRawDataList(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetRawDataList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public long GetEmployeeId(string CardNo)
        {
            long employeeId = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetEmployeeId", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CardNo", CardNo);

                    SqlParameter outputPara = new SqlParameter("@EmployeeId", SqlDbType.BigInt);
                    outputPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputPara);

                    command.ExecuteNonQuery();
                    employeeId = Convert.ToInt64(outputPara.Value);
                    
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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
            return employeeId;
        }
        
        public bool IsSetCalender(int CompanyId, DateTime CalendarDate)
        {
            bool isSet = false;
            try
            {
                using (SqlCommand command = new SqlCommand("Time_IsSetCalender", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@CalendarDate", CalendarDate);

                    SqlParameter outputPara = new SqlParameter("@IsSet", SqlDbType.Bit);
                    outputPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputPara);

                    command.ExecuteNonQuery();
                 
                    isSet = Convert.ToBoolean(outputPara.Value);
                }
            }
            catch(SqlException sqlex)
            {
                SetError(sqlex);
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
            return isSet;
        }

        public int GetDayTypeId(int CompanyId, DateTime Date)
        {
            int dayTypeId = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetDayTypeId", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Date", Date);
                    SqlParameter outputPara = new SqlParameter("@DayTypeId", SqlDbType.Int);
                    outputPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputPara);

                    command.ExecuteNonQuery();
                    dayTypeId = Convert.ToInt32(outputPara.Value);
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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
            return dayTypeId;
        }

        public int GetShiftTypeId(int CompanyId,long EmployeeId, DateTime Date)
        {
            int shiftId = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetShiftTypeId", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);                    
                    command.Parameters.AddWithValue("@Date", Date);
                   
                    SqlParameter outputPara = new SqlParameter("@ShiftId", SqlDbType.Int);
                    outputPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputPara);

                    command.ExecuteNonQuery();
                    shiftId = Convert.ToInt32(outputPara.Value);
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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
            return shiftId;
        }
    
        public DataSet GetShiftSetup(int ShiftTypeId, int DayTypeId)
        {
            DataSet dsShiftSetup = new DataSet();
            DataTable dtShiftSetup = new DataTable("ShiftSetup");
            DataTable dtShiftSetupByOTType = new DataTable("ShiftSetupByOTType");

            using (SqlCommand command = new SqlCommand("Time_GetShiftSetupByShiftDayType", _dbConnection))
            {
                OpenDb();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ShiftTypeId", ShiftTypeId);
                command.Parameters.AddWithValue("@DayTypeId", DayTypeId); 
                using (SqlDataAdapter daAdpater = new SqlDataAdapter(command))
                {
                    daAdpater.Fill(dtShiftSetup);
                }
            }
            // get ot setup
            int ShiftSetupId = -1;
            if (dtShiftSetup.Rows.Count > 0)
            {
                ShiftSetupId = Convert.ToInt32(dtShiftSetup.Rows[0]["ShiftSetupId"]);
            }
            using (SqlCommand command = new SqlCommand("Time_GetShiftSetupByOTTypes", _dbConnection))
            {
                OpenDb();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ShiftSetupId", ShiftSetupId);  
                using (SqlDataAdapter daAdpater = new SqlDataAdapter(command))
                {
                    daAdpater.Fill(dtShiftSetupByOTType);
                }
            }
            dsShiftSetup.Tables.Add(dtShiftSetup);
            dsShiftSetup.Tables.Add(dtShiftSetupByOTType);
            return dsShiftSetup;
        }

        public DataTable GetFirstPayPeriodId(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("GetFirstPayPeriodId", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public DataTable GetPostTimeData(int PayCategoryId, bool IsPosted)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("GET_TimeMonthEndProcessData", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@IsPosted", IsPosted);
                    command.Parameters.AddWithValue("@EncryptionKey", "007London");
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

        public DataTable GetEmployeePay(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("GetEmployeePay", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public DataTable CheckIsRowExist(long EmployeeId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("CheckIsRowExist", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public void MonthEndProcess(long EmployeeId, int PayPeriodId, DateTime FromDate, DateTime ToDate, string EncryptionKey , int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_MonthEndProcess", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@fromDate", FromDate);
                    command.Parameters.AddWithValue("@toDate", ToDate);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void UnpostTime(long EmployeeId, DateTime FromDate, DateTime ToDate, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UnpostTime", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void UnPostEmployeeTxn(long EmployeeId, int PayPeriodId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("UnPostEmployeeTxn", _dbConnection))
                {
                    OpenDb();
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
                _dbConnection.Close();
            }

        }


        public DataTable GetAttendanceData(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetAttendanceData", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public DataTable GetAttendanceData(int CompanyId, DateTime FromDate, DateTime ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetAttendanceDataByDatePeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void AddUnknownCard(string CardNo, DateTime PunchDate, DateTime PunchTime, long RowDataId, string FunctionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddUnknownCard", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CardNo", CardNo);
                    command.Parameters.AddWithValue("@PunchDate", PunchDate);
                    command.Parameters.AddWithValue("@PunchTime", PunchTime);
                    command.Parameters.AddWithValue("@FunctionKey", FunctionKey);
                    command.Parameters.AddWithValue("@RowDataId", RowDataId);
                    command.ExecuteNonQuery();                   
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void AddEmployeeAttendance(long EmployeeId, long RowDataId, DateTime PunchDate, string PunchTime, string AttendanceType, string UserName)
        {
            try
            {

                using (SqlCommand command = new SqlCommand("Time_AddEmployeeAttendanceNew", _dbConnection))
                {
                    OpenDb();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@RowDataId", RowDataId);
                    command.Parameters.AddWithValue("@PunchDate", PunchDate);
                    command.Parameters.AddWithValue("@PunchTime", PunchTime);
                    command.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void AddEmployeeAttendance(long EmployeeId, int ShiftId, int DayTypeId, DateTime PunchDate, string PunchTime, string FunctionKey, bool InOutOnly, string UserName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddEmployeeAttendanceFunctionKey", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@PunchDate", PunchDate);
                    command.Parameters.AddWithValue("@PunchTime", PunchTime);
                    command.Parameters.AddWithValue("@FunctionKey", FunctionKey);
                    command.Parameters.AddWithValue("@InOutOnly", InOutOnly);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void AddEmployeeAttendanceHistory(long EmployeeId, string CardNo, string PunchDate, string PunchTime, string FunctionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddEmployeeAttendanceHistory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CardNo", CardNo);
                    command.Parameters.AddWithValue("@PunchDate", PunchDate);
                    command.Parameters.AddWithValue("@PunchTime", PunchTime);
                    command.Parameters.AddWithValue("@FunctionKey", FunctionKey);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

          
        #region functions     
        public void LateCalculation(long EmployeeId, string ShiftIn, string EmployeeIn, int LateGracePeriod, string LateRoundMode, int LateRounding, string punchDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_LateCalculation", _dbConnection))
                {

                  
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftIn", ShiftIn);
                    command.Parameters.AddWithValue("@EmployeeIn", EmployeeIn);
                    command.Parameters.AddWithValue("@GracePeriod", LateGracePeriod);
                    command.Parameters.AddWithValue("@RoundMode", LateRoundMode);
                    command.Parameters.AddWithValue("@Rounding", LateRounding);
                    command.Parameters.AddWithValue("@PunchDate", punchDate);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void EarlyDepartureCalculation(long EmployeeId, string ShiftOut, string EmployeeOut, int EarlyDepartureGracePeriod, string EarlyDepartureRoundMode, int EarlyDepartureRounding, string punchDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_EarlyDepartureCalculation", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftOut", ShiftOut);
                    command.Parameters.AddWithValue("@EmployeeOut", (EmployeeOut));
                    command.Parameters.AddWithValue("@GracePeriod", EarlyDepartureGracePeriod);
                    command.Parameters.AddWithValue("@RoundMode", EarlyDepartureRoundMode);
                    command.Parameters.AddWithValue("@Rounding", EarlyDepartureRounding);
                    command.Parameters.AddWithValue("@PunchDate", punchDate);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void PayCutCalculation(long EmployeeId, string ShiftIn, string ShiftOut, string EmployeeIn, string EmployeeOut, int PayCutGracePeriod, string PayCutRoundMode, int PayCutRounding, int LeaveApplyDelayMints, decimal LeaveQtyWhenDelay, string punchDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_PayCutCalculation", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftIn", ShiftIn);
                    command.Parameters.AddWithValue("@EmployeeIn", Convert.ToDateTime(EmployeeIn));
                    command.Parameters.AddWithValue("@ShiftOut", ShiftOut);
                    command.Parameters.AddWithValue("@EmployeeOut", Convert.ToDateTime(EmployeeOut));
                    command.Parameters.AddWithValue("@GracePeriod", PayCutGracePeriod);
                    command.Parameters.AddWithValue("@RoundMode", PayCutRoundMode);
                    command.Parameters.AddWithValue("@Rounding", PayCutRounding);
                    command.Parameters.AddWithValue("@LeaveApplyDelayMints", LeaveApplyDelayMints);
                    command.Parameters.AddWithValue("@LeaveQtyWhenDelay", LeaveQtyWhenDelay);
                    command.Parameters.AddWithValue("@PunchDate", Convert.ToDateTime(punchDate));
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void EarlyOTCalculation(long EmployeeId, string ShiftIn, string EmployeeIn, int OTTypeId, int EarlyOTGracePeriod, string EarlyOTRoundMode, int EarlyOTRounding, string punchDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_EarlyOTCalculation", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftIn", ShiftIn);
                    command.Parameters.AddWithValue("@EmployeeIn",  Convert.ToDateTime(EmployeeIn));
                    command.Parameters.AddWithValue("@OTTypeId", OTTypeId);
                    command.Parameters.AddWithValue("@GracePeriod", EarlyOTGracePeriod);
                    command.Parameters.AddWithValue("@RoundMode", EarlyOTRoundMode);
                    command.Parameters.AddWithValue("@Rounding", EarlyOTRounding);
                    command.Parameters.AddWithValue("@PunchDate", Convert.ToDateTime(punchDate));
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void PostOTCalculation(long EmployeeId, string ShiftOut, string EmployeeOut, int OTTypeId, int OTGracePeriod, string OTRoundMode, int OTRounding, string punchDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_PostOTCalculation", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftOut", ShiftOut);
                    command.Parameters.AddWithValue("@EmployeeOut", Convert.ToDateTime(EmployeeOut));
                    command.Parameters.AddWithValue("@OTTypeId", OTTypeId);
                    command.Parameters.AddWithValue("@GracePeriod", OTGracePeriod);
                    command.Parameters.AddWithValue("@RoundMode", OTRoundMode);
                    command.Parameters.AddWithValue("@Rounding", OTRounding);
                    command.Parameters.AddWithValue("@PunchDate", Convert.ToDateTime(punchDate));
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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
        public void PostWorkingHrs(long EmployeeId, string Date, string TimeIn, string TimeOut, string LunchOut, string LunchtIn, int ShiftId, string punchDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_PostWorkingHrs", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date));
                    command.Parameters.AddWithValue("@TimeIn", Convert.ToDateTime(TimeIn));
                    command.Parameters.AddWithValue("@TimeOut", Convert.ToDateTime(TimeOut));
                    // var dd=(LunchtIn != null) ? Convert.ToDateTime(LunchtIn):"";

                    if (!string.IsNullOrEmpty(LunchtIn))
                    {
                        command.Parameters.AddWithValue("@LunchIn", Convert.ToDateTime(LunchtIn));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@LunchIn", LunchtIn);
                    }

                    if (!string.IsNullOrEmpty(LunchOut))
                    {
                        command.Parameters.AddWithValue("@LunchOut", Convert.ToDateTime(LunchOut));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@LunchOut", LunchOut);
                    }
                    command.Parameters.AddWithValue("@ShiftId", (ShiftId));
                    command.Parameters.AddWithValue("@PunchDate", Convert.ToDateTime(punchDate));

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void LeaveCalculation(long EmployeeId, string ShiftIn, string ShiftOut, string EmployeeIn, string EmployeeOut, decimal ShiftHours, decimal MinShiftHours, decimal AppliedLeaveQty, int LeaveApplyDelayMints, string punchDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_LeaveCalculation", _dbConnection)) //Need to impliment the SP 
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftIn", ShiftIn);
                    command.Parameters.AddWithValue("@EmployeeIn", EmployeeIn);
                    command.Parameters.AddWithValue("@ShiftOut", ShiftOut);
                    command.Parameters.AddWithValue("@EmployeeOut", EmployeeOut);
                    //command.Parameters.AddWithValue("@ShiftHours", ShiftHours);
                    //command.Parameters.AddWithValue("@MinShiftHours", MinShiftHours);
                    command.Parameters.AddWithValue("@AppliedLeaveQty", AppliedLeaveQty);
                    command.Parameters.AddWithValue("@LeaveApplyDelayMints", LeaveApplyDelayMints);
                    command.Parameters.AddWithValue("@PunchDate", Convert.ToDateTime(punchDate));
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void OTCalculation(long EmployeeId, decimal ShiftHours, decimal MinShiftHours, int oTType, string punchDate)
        {
            try
            {
                decimal ShiftHourshrs = 0;
                decimal ShiftHoursmin = 0;
                decimal MinShiftHourshrs = 0;
                decimal MinShiftHoursmin = 0;

                if (ShiftHours.ToString().Split('.').Length == 0)
                    ShiftHourshrs = ShiftHours;

                if (MinShiftHours.ToString().Split('.').Length == 0)
                    MinShiftHourshrs = MinShiftHours;

                if (ShiftHours.ToString().Split('.').Length == 2)
                {
                    ShiftHourshrs = Convert.ToInt32(ShiftHours.ToString().Split('.')[0]);
                    ShiftHoursmin = Convert.ToInt32(ShiftHours.ToString().Split('.')[1]);
                }
                if (MinShiftHours.ToString().Split('.').Length == 2)
                {
                    MinShiftHourshrs = Convert.ToInt32(MinShiftHours.ToString().Split('.')[0]);
                    MinShiftHoursmin = Convert.ToInt32(MinShiftHours.ToString().Split('.')[1]);
                }
             



                using (SqlCommand command = new SqlCommand("Time_OTCalculation", _dbConnection)) //Need to impliment the SP 
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ShiftHours", (ShiftHourshrs * 60) + ShiftHoursmin);
                    command.Parameters.AddWithValue("@MinShiftHours", (MinShiftHourshrs * 60) + MinShiftHoursmin);
                    command.Parameters.AddWithValue("@OTType", oTType);
                    command.Parameters.AddWithValue("@PunchDate", Convert.ToDateTime(punchDate));
                 
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        #endregion

        #region Destructor
        ~PostingDAL()
        {
            //if (_dbConnection.State!=ConnectionState.Closed)
            //{
            //    _dbConnection.Close();
            //    _dbConnection.Dispose();
            //}
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
