using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM.Time.DAL
{
    public class RosterDAL
    {
        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _dayTypeId = 0;

        #endregion

        #region Properties

        public bool IsError
        {
            get { return _isError; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        public int DayTypeId
        {
            get { return _dayTypeId; }
        }

        #endregion

        #region Constructor

        public RosterDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods

        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        #region Roster

        public DataTable GetRosterDetails(int companyId, int rosterGroupId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetRosterDetails", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@CompanyId", companyId);
                        command.Parameters.AddWithValue("@RosterGroupId", rosterGroupId);

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
        }

        public DataTable GetShiftsCreated()
        {
            DataTable dtShiftList = new DataTable();

            try
            {
                //string sqlQuery = "SELECT ShiftId, '['+ ShiftCode + ']' + ' - ' + ShiftDescription AS Shift FROM Time_Shift";
                string sqlQuery = "SELECT ShiftId, '['+ ShiftCode + ']' + ' - ' + ShiftDescription AS Shift FROM Time_ShiftTypes";

                using (SqlCommand command = new SqlCommand(sqlQuery, _dbConnection))
                {
                    command.CommandType = CommandType.Text;
                    OpenDB();

                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtShiftList);
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

            return dtShiftList;
        }

        public DataTable GetRosterGroupsList(int CompanyId)
        {
            DataTable dtRosterGroupList = new DataTable();

            try
            {
                string sqlQuery = "SELECT RosterGroupId, RosterGroup,Remarks FROM Time_RosterGroup Where CompanyId=" + CompanyId;

                using (SqlCommand command = new SqlCommand(sqlQuery, _dbConnection))
                {
                    command.CommandType = CommandType.Text;
                    OpenDB();

                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtRosterGroupList);
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

            return dtRosterGroupList;
        }

        public DataTable GetCalenderByYear(int CompanyId, int Year, int Month)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetCalenderByYear", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
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
        }

        public DataTable GetDayTypeByDate(int CompanyId, DateTime Date)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetCalenderByYear", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Year", 2017);
                        command.Parameters.AddWithValue("@Month", 12);
                        
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
        }

        public DataTable GetDefaultDayType()
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetDefaultDayType", _dbConnection))
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
                    _dbConnection.Close();
                }

                return dtTable;
            }
        }

        public void AddRoster(int CompanyId, int RosterGroupId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, string DateName, DateTime RosterDate, int ShiftId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                DataTable dtEmployees = new DataTable();

                using (SqlCommand command = new SqlCommand("Time_AddRoster", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    command.Parameters.AddWithValue("@StartTime", StartTime);
                    command.Parameters.AddWithValue("@EndTime", EndTime);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@DateName", DateName);
                    command.Parameters.AddWithValue("@RosterDate", RosterDate);
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@Subject", Subject);

                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand("Time_SetEmployeeDefaultCalender", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    command.Parameters.AddWithValue("@RosterDate", RosterDate);
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand("Time_SetEmployeeShift", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    command.Parameters.AddWithValue("@RosterDate", RosterDate);
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    command.ExecuteNonQuery();


                }

                using (SqlCommand command = new SqlCommand("TIME_GetRosterGroupEmployees", _dbConnection))
                {
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtEmployees);
                    }
                }

                
                foreach (DataRow emp in dtEmployees.Rows)
                {
                    long employeeId = Convert.ToInt64(emp["EmployeeId"].ToString());
                    bool isAttendanceConsider = Convert.ToBoolean(emp["ConsiderAttendance"].ToString());
                    string employeeCode = emp["EmployeeCode"].ToString();
                    DataTable dtDays = new DataTable();

                    if (isAttendanceConsider)
                    {
                        using (SqlCommand command = new SqlCommand("TIME_GetTimeRawDataDetails", _dbConnection))
                        {
                            command.Parameters.AddWithValue("@EmployeeId", employeeId);
                            command.Parameters.AddWithValue("@RosterDate", RosterDate);
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                            {
                                daAdapter.Fill(dtDays);
                            }
                        }

                        using (SqlCommand command = new SqlCommand("TIME_DeletePunchTimesFromRawData", _dbConnection))
                        {
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            command.Parameters.AddWithValue("@CalculateDate", RosterDate);
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                        }

                        foreach (DataRow times in dtDays.Rows)
                        {
                            using (SqlCommand command = new SqlCommand("Time_ModifyAttendance", _dbConnection))
                            {
                                command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                                command.Parameters.AddWithValue("@Day", Convert.ToDateTime(times["PunchDateTime"].ToString()).Day);
                                command.Parameters.AddWithValue("@Month", Convert.ToDateTime(times["PunchDateTime"].ToString()).Month);
                                command.Parameters.AddWithValue("@Year", Convert.ToDateTime(times["PunchDateTime"].ToString()).Year);
                                command.Parameters.AddWithValue("@attType", times["AttendanceType"].ToString());
                                command.Parameters.AddWithValue("@punchTime", times["CardTime"].ToString());
                                command.Parameters.AddWithValue("@ShiftId", ShiftId);
                                command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                        }
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

        public void DeleteRoster(DateTime RosterDate, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteRoster", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@RosterDate", RosterDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

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

        public DataTable DeleteTimeRoster(int companyId, int rosterId, string CreatedUser)
        {
            DataTable dtEmployees = new DataTable();
            DataTable dtRosterDetail = new DataTable();

            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("[Time_GetRosterDateDetail]", _dbConnection))
                    {
                        command.Parameters.AddWithValue("@RosterId", rosterId);
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtRosterDetail);
                        }
                    }

                    using (SqlCommand command = new SqlCommand("Time_DeleteTimeRoster", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@CompanyId", companyId);
                        command.Parameters.AddWithValue("@RosterGroupId", rosterId);

                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                    }

                    

                    if (dtRosterDetail.Rows.Count > 0) {

                        int rosterGroupId = Convert.ToInt32(dtRosterDetail.Rows[0]["RosterGroupId"]);
                        DateTime rosterDate = Convert.ToDateTime(dtRosterDetail.Rows[0]["RosterDate"]);

                        using (SqlCommand command = new SqlCommand("TIME_GetRosterGroupEmployees", _dbConnection))
                        {
                            command.Parameters.AddWithValue("@RosterGroupId", rosterGroupId);
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                            {
                                daAdapter.Fill(dtEmployees);
                            }
                        }

                        foreach (DataRow emp in dtEmployees.Rows)
                        {
                            long employeeId = Convert.ToInt64(emp["EmployeeId"].ToString());
                            bool isAttendanceConsider = Convert.ToBoolean(emp["ConsiderAttendance"].ToString());
                            string employeeCode = emp["EmployeeCode"].ToString();
                            DataTable dtDays = new DataTable();

                            if (isAttendanceConsider)
                            {
                                using (SqlCommand command = new SqlCommand("TIME_GetTimeRawDataDetails", _dbConnection))
                                {
                                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                                    command.Parameters.AddWithValue("@RosterDate", rosterDate);
                                    command.CommandType = CommandType.StoredProcedure;
                                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                                    {
                                        daAdapter.Fill(dtDays);
                                    }
                                }

                                using (SqlCommand command = new SqlCommand("TIME_DeletePunchTimesFromRawData", _dbConnection))
                                {
                                    command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                                    command.Parameters.AddWithValue("@CalculateDate", rosterDate);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.ExecuteNonQuery();
                                }

                                foreach (DataRow times in dtDays.Rows)
                                {
                                    using (SqlCommand command = new SqlCommand("Time_ModifyAttendance", _dbConnection))
                                    {
                                        command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                                        command.Parameters.AddWithValue("@Day", Convert.ToDateTime(times["PunchDateTime"].ToString()).Day);
                                        command.Parameters.AddWithValue("@Month", Convert.ToDateTime(times["PunchDateTime"].ToString()).Month);
                                        command.Parameters.AddWithValue("@Year", Convert.ToDateTime(times["PunchDateTime"].ToString()).Year);
                                        command.Parameters.AddWithValue("@attType", times["AttendanceType"].ToString());
                                        command.Parameters.AddWithValue("@punchTime", times["CardTime"].ToString());
                                        command.Parameters.AddWithValue("@ShiftId", "");
                                        command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
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
        }

        public DataTable GetShiftSetupDetails(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftTypesDetails", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

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
        }

        public DataTable GetDayTypeByDate(int CompanyId, DateTime Date, int Year, int Month, int Day)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetDayTypeByDate", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Date", Date);

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
        }

        public void InsertShiftSetup(string UserName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_SetEmployeeDefaultTimeCard", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CreatedUser", UserName);
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
                
        #region Employee Shift Schedule

        public DataTable GetEmployeeShiftDetails(int CompanyId, Int64 EmployeeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetEmployeeShiftDetails", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
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
        }

        public void AddEmployeeShiftSchedule(int CompanyId, int EmployeeId, int OrgStructureID, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, string DateName, DateTime ShiftDate, int ShiftId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddEmployeeShiftSchedule", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                   
                    command.Parameters.AddWithValue("@StartTime", StartTime);
                    command.Parameters.AddWithValue("@EndTime", EndTime);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@DateName", DateName);
                    command.Parameters.AddWithValue("@ShiftDate", ShiftDate);
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@Subject", Subject);

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

        public void DeleteEmployeeShift(DateTime ShiftDate, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteEmployeeShift", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ShiftDate", ShiftDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

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
        public void DeleteEmployeeShift(DateTime ShiftDate, int CompanyId,long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteindividualEmployeeShift", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ShiftDate", ShiftDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
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
        #endregion

        #endregion

        #region Destructor

        ~RosterDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
