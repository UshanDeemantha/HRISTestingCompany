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
/// Created Timestamp   : 012/DEC/2011
/// Class Description   : Time Card
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
    public class TimeCardDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private DateTime _ShiftDate ;
        private DateTime _ShiftTime ;
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

        public DateTime ShiftDate
        {
            get { return _ShiftDate; }
        }

        public DateTime ShiftTime
        {
            get { return _ShiftTime; }
        }

        #endregion

        #region Constructor
        public TimeCardDAL()
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
        public static bool IsNullOrEmpty<T>(IEnumerable<T> source)
        {
            if (source != null)
            {
                foreach (T obj in source)
                {
                    return false;
                }
            }
            return true;
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

        #region External
        public void AttendancesReCalculation(int CompanyId, long EmployeeId, string EmployeeCode, DateTime FromDate, DateTime ToDate, string CreatedUser)
        {
            try
            {
                OpenDb();

                DataTable dtCalendarDates = new DataTable();

                using (SqlCommand command = new SqlCommand("TIME_GetRawDataRecordsFromAttHistory", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtCalendarDates);
                    }
                }

                using (SqlCommand command1 = new SqlCommand("TIME_DeleteRawDataRecordsWhenMissingDataProcess", _dbConnection))
                {
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command1.Parameters.AddWithValue("@FromDate", FromDate);
                    command1.Parameters.AddWithValue("@ToDate", ToDate);
                    command1.Parameters.AddWithValue("@CompanyId", CompanyId);

                    command1.ExecuteNonQuery();
                    command1.Parameters.Clear();
                }

                for (DateTime date = FromDate; date <= ToDate; date = date.AddDays(1.0))
                {
                    DateTime calendarDate = date;
                    DataTable dtDays = new DataTable();
                    IEnumerable<DataRow> orderedRows = dtCalendarDates.AsEnumerable().Where(x => x.Field<DateTime>("CardDateTime") == calendarDate).OrderBy(r => r.Field<DateTime>("PunchDateTime"));
                    if (!IsNullOrEmpty(orderedRows))
                    {
                        dtDays = orderedRows.CopyToDataTable();
                    }

                    foreach (DataRow row in dtDays.Rows)
                    {
                        using (SqlCommand command = new SqlCommand("COM_RaDataTimeFromExcel", _dbConnection))
                        {
                            try
                            {
                                string time = row["CardTime"].ToString();

                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                                command.Parameters.AddWithValue("@Day", Convert.ToDateTime(row["PunchDateTime"].ToString()).Day);
                                command.Parameters.AddWithValue("@Month", Convert.ToDateTime(row["PunchDateTime"].ToString()).Month);
                                command.Parameters.AddWithValue("@Year", Convert.ToDateTime(row["PunchDateTime"].ToString()).Year);
                                command.Parameters.AddWithValue("@CompanyId", CompanyId);
                                command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                                command.Parameters.AddWithValue("@punchTime", row["CardTime"].ToString());

                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                        //using (SqlCommand command = new SqlCommand("Time_ModifyAttendanceWhenMissingAttendanceProcess", _dbConnection))
                        //{
                        //    try
                        //    {
                        //        command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                        //        command.Parameters.AddWithValue("@Day", Convert.ToDateTime(row["PunchDateTime"].ToString()).Day);
                        //        command.Parameters.AddWithValue("@Month", Convert.ToDateTime(row["PunchDateTime"].ToString()).Month);
                        //        command.Parameters.AddWithValue("@Year", Convert.ToDateTime(row["PunchDateTime"].ToString()).Year);
                        //        command.Parameters.AddWithValue("@attType", row["AttendanceType"].ToString());
                        //        command.Parameters.AddWithValue("@punchTime", row["CardTime"].ToString());
                        //        command.Parameters.AddWithValue("@ShiftId", "");
                        //        command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                        //        command.Parameters.AddWithValue("@RelevantDay", calendarDate.Day);
                        //        command.Parameters.AddWithValue("@RelevantMonth", calendarDate.Month);
                        //        command.Parameters.AddWithValue("@RelevantYear", calendarDate.Year);
                        //        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        //        command.CommandType = CommandType.StoredProcedure;
                        //        command.ExecuteNonQuery();
                        //    }
                        //    catch (SqlException sqlex)
                        //    {

                        //    }

                        //}
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
        }

        public DataTable GetTimeCard(int CompanyId, DateTime FromDate, DateTime ToDate, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetTimeCard", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
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
                _dbConnection.Close();
            }
            return dtTable;
        }


        public Int32
           UpdateEmployeeTime(int _empId, DateTime? _date, DateTime? _inTime1,
    DateTime? _inTime2, DateTime? _outTime1, DateTime? _outTime2, double lateHrs, double earlyHrs, double timevalue, double extraHrs, double lessyHrs,
    int _updatedUser)
        {
            Int32 _employeeId = -1;

            try
            {


                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_UpdateEmployeeAttendences", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmpID", _empId);
                    command.Parameters.AddWithValue("@Date", _date);
                    command.Parameters.AddWithValue("@TimeIn", _inTime1);
                    command.Parameters.AddWithValue("@LunchOut", _outTime1);
                    command.Parameters.AddWithValue("@LunchIn", _inTime2);
                    command.Parameters.AddWithValue("@TimeOut", _outTime2);
                    command.Parameters.AddWithValue("@ModifiedUser", _updatedUser);
                    command.Parameters.AddWithValue("@LateHrs", lateHrs);
                    command.Parameters.AddWithValue("@EarlyHrs", earlyHrs);
                    command.Parameters.AddWithValue("@WorkingHrs", timevalue);
                    command.Parameters.AddWithValue("@ExtraHrs", extraHrs);
                    command.Parameters.AddWithValue("@LessHrs", lessyHrs);
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

            return _employeeId;
        }

        public DataTable GetAttendence(int CompanyId, DateTime Date, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetAttendence", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Date", Date);
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public DataTable GetPendingAttendence(int CompanyId, DateTime Date, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetPendingAttendence", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Date", Date);
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public DataTable GetAttendenceApprovelData(int CompanyId, string Date, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_Get_ChangeAttendence", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Date", Date);
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
                _dbConnection.Close();
            }
            return dtTable;
        }

        public DataTable GetAttendencesRequestApprovedStatus(int attnId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetAttendencesRequestApprovedStatus", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@AttId", attnId);
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

        public void GetShiftInShiftOutDetail(long EmployeeId, DateTime Date, string ShiftDetail)
        {
           
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("TIME_GetShiftInShiftOutDetail", _dbConnection))
                {
                   
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        command.Parameters.AddWithValue("@EmployeeNo", EmployeeId);
                        command.Parameters.AddWithValue("@Date", Date);
                        command.Parameters.AddWithValue("@ShiftDetail", ShiftDetail);

                        SqlParameter ShiftDate = new SqlParameter("@ShiftDate", SqlDbType.DateTime);
                        ShiftDate.Direction = ParameterDirection.Output;
                        command.Parameters.Add(ShiftDate);

                        SqlParameter ShiftTime = new SqlParameter("@ShiftTime", SqlDbType.DateTime);
                        ShiftTime.Direction = ParameterDirection.Output;
                        command.Parameters.Add(ShiftTime);
                        command.ExecuteNonQuery();

                        _ShiftDate = Convert.ToDateTime(ShiftDate.Value);
                        _ShiftTime = Convert.ToDateTime(ShiftTime.Value);
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
            
        }


        public DataTable GetTimewithComments(int CompanyId, int OrgStuctureID, DateTime FromDate, DateTime ToDate, long EmployeeId, int AttendancesTypeId, int ShiftTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {

                using (SqlCommand command = new SqlCommand("TIME_GetTime_EmployeeAttendences", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStuctureID", OrgStuctureID);
                    command.Parameters.AddWithValue("@Date", FromDate);
                    command.Parameters.AddWithValue("@Date2", ToDate);
                    command.Parameters.AddWithValue("@EmployeeNo", EmployeeId);
                    command.Parameters.AddWithValue("@AttendancesTypeId", AttendancesTypeId);
                    command.Parameters.AddWithValue("@ShiftTypeId", ShiftTypeId);

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

        public DataTable GetEmployeesForTimeRepostingGrid(int CompanyId, int OrgStuctureID, long EmployeeId, int RosterGroupId)
        {
            DataTable dtTable = new DataTable();
            try
            {

                using (SqlCommand command = new SqlCommand("TIME_GetEmployeesForTimeRepostingGrid", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStuctureID", OrgStuctureID);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);

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

        public DataTable GetPunchTimesFromRawData(long EmployeeId, DateTime CalculateDate)
        {
            DataTable dtTable = new DataTable();
            try
            {

                using (SqlCommand command = new SqlCommand("TIME_GetPunchTimesFromRawData", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CalculateDate", CalculateDate);
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

        public DataTable GetAttendanceDetailsFromTxnId(long TxnId)
        {
            DataTable dtTable = new DataTable();
            try
            {

                using (SqlCommand command = new SqlCommand("TIME_GetAttendanceDetailsFromTxnId", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TxnId", TxnId);
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

        public DataTable getWorkFlow(int empID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetEmployeeWorkFlow", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", empID);

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

        public DataTable GetTimeCard(int CompanyId, int OrgStructureId, DateTime FromDate, DateTime ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetTimeCardbyOrganizationalUnit", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureId);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
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


        public DataTable GetChangedIndex(long EmpTxnId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_GetChangedIndex", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpTxnId", EmpTxnId);


                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
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

        public DataTable GetPrvsIntimeAndOuttime(string EmployeeCode, DateTime CalculateDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("TIME_GetPrvsIntimeAndOuttime", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@CalculateDate", CalculateDate);
                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
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


        public void ModifyEmployeeAttendance(Decimal ApproveOT15, Decimal ApproveOT20, Decimal ApproveOT30, Decimal ApproveNightOT, Decimal ApproveLateHrs, Decimal ApproveEarlyHrs, Decimal ApproveEarlyOT, Decimal ApprovePostOT,
            bool IsApproveOT15Changed, bool IsApproveOT20Changed, bool IsApproveOT30Changed, bool IsApproveNightOTChanged, bool IsApproveLateHrsChanged, bool IsApproveEarlyHrsChanged, bool IsApproveEarlyOTPostOTChanged,
            long EmpTxnId, string UpdatedUser, string ChangedIndex)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_ModifyEmployeeAttendance", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApproveOT15", ApproveOT15);
                    command.Parameters.AddWithValue("@ApproveOT20", ApproveOT20);
                    command.Parameters.AddWithValue("@ApproveOT30", ApproveOT30);
                    command.Parameters.AddWithValue("@ApproveNightOT", ApproveNightOT);
                    command.Parameters.AddWithValue("@ApproveLateHrs", ApproveLateHrs);
                    command.Parameters.AddWithValue("@ApproveEarlyHrs", ApproveEarlyHrs);
                    command.Parameters.AddWithValue("@ApproveEarlyOT", ApproveEarlyOT);
                    command.Parameters.AddWithValue("@ApprovePostOT", ApprovePostOT);
                    command.Parameters.AddWithValue("@IsApproveOT15Changed", IsApproveOT15Changed);
                    command.Parameters.AddWithValue("@IsApproveOT20Changed", IsApproveOT20Changed);
                    command.Parameters.AddWithValue("@IsApproveOT30Changed", IsApproveOT30Changed);
                    command.Parameters.AddWithValue("@IsApproveNightOTChanged", IsApproveNightOTChanged);
                    command.Parameters.AddWithValue("@IsApproveEarlyHrsChanged", IsApproveEarlyHrsChanged);
                    command.Parameters.AddWithValue("@IsApproveLateHrsChanged", IsApproveLateHrsChanged);
                    command.Parameters.AddWithValue("@IsApproveEarlyOTPostOTChanged", IsApproveEarlyOTPostOTChanged);
                    command.Parameters.AddWithValue("@EmpTxnId", EmpTxnId);
                    command.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);
                    command.Parameters.AddWithValue("@ChangedIndex", ChangedIndex);
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

        public void ModifyAttendance(string EmployeeCode, int Day, int Month, int Year, string attType, string punchTime, string shiftId, int modifiedDay, int modifiedMonth, int modifiedYear, string CreatedUser, int CompanyId )
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_ModifyAttendance", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@attType", attType);
                    command.Parameters.AddWithValue("@punchTime", punchTime);
                    command.Parameters.AddWithValue("@ShiftId", shiftId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@RelevantDay", modifiedDay);
                    command.Parameters.AddWithValue("@RelevantMonth", modifiedMonth);
                    command.Parameters.AddWithValue("@RelevantYear", modifiedYear);
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

        public void DeletePunchTimesFromRawData(string EmployeeCode, DateTime CalculateDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("TIME_DeletePunchTimesFromRawData", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@CalculateDate", CalculateDate);
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

        public void UpdateChangedIndex(string EmployeeCode, DateTime CalculateDate, DateTime? prvsIntime, DateTime? prvsOuttime)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("TIME_UpdateChangedIndex", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@CalculateDate", CalculateDate);
                    command.Parameters.AddWithValue("@prvsIntime1", prvsIntime);
                    command.Parameters.AddWithValue("@prvsOuttime1", prvsOuttime);
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

        public void TimeReposting(int CompanyId, long EmployeeId, string EmployeeCode, DateTime FromDate, DateTime ToDate, string CreatedUser)
        {
            try
            {
                OpenDb();

                DataTable dtCalendarDates = new DataTable();

                using (SqlCommand command = new SqlCommand("TIME_GetTimeRawDataDetailsAndInsertToRawDataDuplicate", _dbConnection))
                {
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }

                for (DateTime date = FromDate; date <= ToDate; date = date.AddDays(1.0))
                {
                    DateTime calendarDate = date;
                    DataTable dtDays = new DataTable();

                    using (SqlCommand command = new SqlCommand("TIME_GetTimeRawDataDuplicateDetails", _dbConnection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@RosterDate", calendarDate);
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtDays);
                        }

                    }

                    foreach (DataRow times in dtDays.Rows)
                    {
                        using (SqlCommand command = new SqlCommand("Time_ModifyAttendanceWhenBackDateProcess", _dbConnection))
                        {
                            try
                            {
                                command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                                command.Parameters.AddWithValue("@Day", Convert.ToDateTime(times["PunchDateTime"].ToString()).Day);
                                command.Parameters.AddWithValue("@Month", Convert.ToDateTime(times["PunchDateTime"].ToString()).Month);
                                command.Parameters.AddWithValue("@Year", Convert.ToDateTime(times["PunchDateTime"].ToString()).Year);
                                command.Parameters.AddWithValue("@attType", times["AttendanceType"].ToString());
                                command.Parameters.AddWithValue("@punchTime", times["CardTime"].ToString());
                                command.Parameters.AddWithValue("@ShiftId", "");
                                command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                                command.Parameters.AddWithValue("@RelevantDay", calendarDate.Day);
                                command.Parameters.AddWithValue("@RelevantMonth", calendarDate.Month);
                                command.Parameters.AddWithValue("@RelevantYear", calendarDate.Year);
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                            catch (SqlException sqlex)
                            {

                            }

                        }
                    }
                }

                using (SqlCommand command = new SqlCommand("TIME_DeletePunchTimesFromRawDataWhenBackDateProcessing", _dbConnection))
                {
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    command.CommandType = CommandType.StoredProcedure;
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

        public void UpdateTimeCard(DataTable TimeCard)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_UpdateTimeCard", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EmpTxnId", SqlDbType.BigInt, 16, "EmpTxnId");
                    command.Parameters.Add("@EmployeeId", SqlDbType.BigInt, 16, "EmployeeId");
                    command.Parameters.Add("@Date", SqlDbType.DateTime, 8, "Date");
                    command.Parameters.Add("@DayTypeId", SqlDbType.Int, 8, "DayTypeId");
                    command.Parameters.Add("@ShiftId", SqlDbType.Int, 8, "ShiftId");
                    command.Parameters.Add("@TimeIn", SqlDbType.NVarChar, 5, "TimeIn");
                    command.Parameters.Add("@TimeOut", SqlDbType.NVarChar, 5, "TimeOut");
                    command.Parameters.Add("@LunchOut", SqlDbType.NVarChar, 5, "LunchOut");
                    command.Parameters.Add("@LunchIn", SqlDbType.NVarChar, 5, "LunchIn");
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.UpdateCommand = command;
                        daAdpter.Update(TimeCard);
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
        }

        //   public void UpdateEmployeeTime(int _empId, string _employeeCode, DateTime? _date,  DateTime? _inTime1,
        //DateTime? _inTime2, DateTime? _outTime1, DateTime? _outTime2,
        //string _createdUser)
        //   {
        //       try
        //       {
        //           OpenDb();
        //           using (SqlCommand command = new SqlCommand("Time_UpdateTimeCard", _dbConnection))
        //           {
        //               command.Parameters.AddWithValue("@EmpID", _empId);
        //               command.Parameters.AddWithValue("@EmployeeCode", _employeeCode);
        //               command.Parameters.AddWithValue("@Date", _date);
        //               command.Parameters.AddWithValue("@TimeIn", _inTime1);
        //               command.Parameters.AddWithValue("@LunchOut", _outTime1);
        //               command.Parameters.AddWithValue("@LunchIn", _inTime2);
        //               command.Parameters.AddWithValue("@TimeOut", _outTime2);
        //               command.Parameters.AddWithValue("@CreatedUser", _createdUser);
        //               command.Parameters.AddWithValue("@LateHrs", lateHrs);
        //               command.Parameters.AddWithValue("@EarlyHrs", earlyHrs);
        //               command.Parameters.AddWithValue("@WorkingHrs", timevalue);
        //               command.Parameters.AddWithValue("@ExtraHrs", extraHrs);
        //               command.Parameters.AddWithValue("@LessHrs", lessyHrs);

        //               using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
        //               {
        //                   daAdpter.UpdateCommand = command;
        //                   daAdpter.Update(TimeCard);
        //               }
        //           }
        //       }
        //       catch (SqlException sqlex)
        //       {
        //           SetError(sqlex);
        //       }
        //       catch (Exception ex)
        //       {
        //           _isError = true;
        //           _errorMsg = ex.Message;
        //       }
        //       finally
        //       {
        //           _dbConnection.Close();
        //       }
        //   }


        public void UpdateAttendencesAprovelStatus(int empID, string date, int location, bool isLast, string Status)
        {

            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_UpdateAttendencesRequest", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeId", empID);
                    command.Parameters.AddWithValue("@Date", date);

                    if (location == 1)
                    {

                        if (Status == "true")
                        {
                            if (!isLast)
                            {
                                command.Parameters.AddWithValue("@Coverring_Person1", "Approved");
                                command.Parameters.AddWithValue("@Coverring_Person2", "Pending");
                                command.Parameters.AddWithValue("@Coverring_Person3", "");
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Coverring_Person1", "Approved");
                                command.Parameters.AddWithValue("@Coverring_Person2", "");
                                command.Parameters.AddWithValue("@Coverring_Person3", "");
                            }
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Coverring_Person1", "Rejected");
                            command.Parameters.AddWithValue("@Coverring_Person2", "");
                            command.Parameters.AddWithValue("@Coverring_Person3", "");
                        }
                    }
                    if (location == 2)
                    {

                        if (Status == "true")
                        {
                            if (!isLast)
                            {
                                command.Parameters.AddWithValue("@Coverring_Person2", "Approved");
                                command.Parameters.AddWithValue("@Coverring_Person3", "Pending");
                                command.Parameters.AddWithValue("@Coverring_Person1", "Approved");
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Coverring_Person2", "Approved");
                                command.Parameters.AddWithValue("@Coverring_Person3", "");
                                command.Parameters.AddWithValue("@Coverring_Person1", "Approved");

                            }
                        }
                        else
                        {

                            command.Parameters.AddWithValue("@Coverring_Person1", "Approved");
                            command.Parameters.AddWithValue("@Coverring_Person2", "Rejected");
                            command.Parameters.AddWithValue("@Coverring_Person3", "");

                        }
                    }
                    if (location == 3)
                    {
                        if (Status == "true")
                        {
                            command.Parameters.AddWithValue("@Coverring_Person3", "Approved");
                            command.Parameters.AddWithValue("@Coverring_Person2", "Approved");
                            command.Parameters.AddWithValue("@Coverring_Person1", "Approved");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Coverring_Person3", "Rejected");
                            command.Parameters.AddWithValue("@Coverring_Person2", "Approved");
                            command.Parameters.AddWithValue("@Coverring_Person1", "Approved");
                        }

                    }

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



        public Int64 InsertAttendenceForApprove(long EmployeeId, DateTime Date, DateTime? Intime1, DateTime? OutTime1, DateTime? Intime2, DateTime? OutTime2)
        {
            Int64 _employeeId = -1;
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_InsertAttendecForApprovel", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@TimeIn", Intime1);
                    command.Parameters.AddWithValue("@LunchOut", OutTime1);
                    command.Parameters.AddWithValue("@LunchIn", Intime2);
                    command.Parameters.AddWithValue("@TimeOut", OutTime2);
                    command.Parameters.AddWithValue("@Coverring_Person1", "Pending");

                    //SqlParameter imployeeId = new SqlParameter("@EmployeeID", SqlDbType.BigInt, 16);
                    //imployeeId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(imployeeId);

                    command.ExecuteNonQuery();

                    // _employeeId = Convert.ToInt64(imployeeId.Value);
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
            return _employeeId;
        }


        public void DeleteTimeCard(DataTable TimeCard)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_DeleteTimeCard", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EmpTxnId", SqlDbType.BigInt, 16, "EmpTxnId");
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.UpdateCommand = command;
                        daAdpter.Update(TimeCard);
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
        }

        public void ShiftChange(DataTable TimeCard)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_ShiftChange", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EmpTxnId", SqlDbType.BigInt, 16, "EmpTxnId");
                    command.Parameters.Add("@EmployeeId", SqlDbType.BigInt, 16, "EmployeeId");
                    command.Parameters.Add("@Date", SqlDbType.DateTime, 8, "Date");
                    command.Parameters.Add("@ShiftId", SqlDbType.Int, 8, "ShiftId");
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.UpdateCommand = command;
                        daAdpter.Update(TimeCard);
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
        }

        public void DayChange(DataTable TimeCard)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_DayChange", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EmpTxnId", SqlDbType.BigInt, 16, "EmpTxnId");
                    command.Parameters.Add("@EmployeeId", SqlDbType.BigInt, 16, "EmployeeId");
                    command.Parameters.Add("@Date", SqlDbType.DateTime, 8, "Date");
                    command.Parameters.Add("@DayTypeId", SqlDbType.Int, 8, "DayTypeId");
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.UpdateCommand = command;
                        daAdpter.Update(TimeCard);
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
        }

        public void InOutChange(DataTable TimeCard)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_InOutChange", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EmpTxnId", SqlDbType.BigInt, 16, "EmpTxnId");
                    command.Parameters.Add("@EmployeeId", SqlDbType.BigInt, 16, "EmployeeId");
                    command.Parameters.Add("@Date", SqlDbType.DateTime, 8, "Date");
                    command.Parameters.Add("@ChangeType", SqlDbType.NVarChar, 50, "ChangeType");
                    command.Parameters.Add("@TimeIn", SqlDbType.NVarChar, 5, "TimeIn");
                    command.Parameters.Add("@TimeOut", SqlDbType.NVarChar, 5, "TimeOut");
                    command.Parameters.Add("@LunchOut", SqlDbType.NVarChar, 5, "LunchOut");
                    command.Parameters.Add("@LunchIn", SqlDbType.NVarChar, 5, "LunchIn");
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.UpdateCommand = command;
                        daAdpter.Update(TimeCard);
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
        }

        public void OTApproval(DataTable TimeCard)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("Time_InOutChange", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EmpTxnId", SqlDbType.BigInt, 16, "EmpTxnId");
                    command.Parameters.Add("@EmployeeId", SqlDbType.BigInt, 16, "EmployeeId");
                    command.Parameters.Add("@Date", SqlDbType.DateTime, 8, "Date");
                    command.Parameters.Add("@TimeIn", SqlDbType.NVarChar, 5, "TimeIn");
                    command.Parameters.Add("@TimeOut", SqlDbType.NVarChar, 5, "TimeOut");
                    command.Parameters.Add("@LunchOut", SqlDbType.NVarChar, 5, "LunchOut");
                    command.Parameters.Add("@LunchIn", SqlDbType.NVarChar, 5, "LunchIn");
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");
                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.UpdateCommand = command;
                        daAdpter.Update(TimeCard);
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
        }
        #endregion
        #endregion

        #region Destructor
        ~TimeCardDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
