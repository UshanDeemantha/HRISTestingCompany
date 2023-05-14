using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace HRM.Time.DAL
{
    public class DayTypesDAL
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

        public DayTypesDAL()
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


        #region DayType

        public void AddDayTypes(string DayTypeCode, string DayType, string BackGroundColour, string FontColour, string CreatedUser, DateTime CreatedDate, bool IsDefault)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddDayType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@DayTypeCode", DayTypeCode);
                    command.Parameters.AddWithValue("@DayType", DayType);
                    command.Parameters.AddWithValue("@BackGroundColour", BackGroundColour);
                    command.Parameters.AddWithValue("@FontColour", FontColour);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    command.Parameters.AddWithValue("@IsDefault", IsDefault);

                    SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    DayTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    _dayTypeId = Convert.ToInt32(DayTypeId.Value);
                    if (_dayTypeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
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

        public void UpdateDayType(int DayTypeId, string DayType, string BackGroundColour, string FontColour, string ModifyUser, DateTime ModifyDate, bool Default)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateDayType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@DayType", DayType);
                    command.Parameters.AddWithValue("@BackGroundColour", BackGroundColour);
                    command.Parameters.AddWithValue("@FontColour", FontColour);
                    command.Parameters.AddWithValue("@ModifiedUser", ModifyUser);
                    command.Parameters.AddWithValue("@ModifiedDate", ModifyDate);
                    command.Parameters.AddWithValue("@IsDefault", Default);

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

        public DataTable GetDayType(int DayTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetDayType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
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
        public DataTable tbledaytypecheck(int DayTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetDaytypedelete", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
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
        public DataTable GetDayTypeCode(string DayTypeCode)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetDayTypeByCode", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@DayTypeCode", DayTypeCode);
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

        public DataTable GetDayTypeById(int DayTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetDayTypeById", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
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

        public DataTable CheckDefaultDayType(bool Default)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_CheckDefaultDayType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@IsDefault", Default);
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

        public void DeleteDayType(int DayTypeId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Time_DeleteDayType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _isError = true;
                if (ex.Number == 547)
                {
                    _errorMsg = "Already Assign This Code";
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


        #region Calender

        public DataTable GetCalender(int CompanyId, int Year)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetCalender", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Year", Year);
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


        public DataTable GetCalenderByYearMonthDay(int CompanyId, int Year, int Month, int Date)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetCalenderByYearMonthDay", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Year", Year);
                        command.Parameters.AddWithValue("@Month", Month);
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
        
             public void SetEmpLeaveLev(int CompanyId, DateTime CalenderDate, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_statutoryLeaveEntitlements", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", @CompanyId);
                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();

                }

                //UpdateTimeWhenCalenderChange(CompanyId, CalenderDate, CreatedUser);
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


        public void AddCalender(DateTime CalenderDate, int CompanyId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddCalender", _dbConnection))
                {
                    DateTime expiry = new DateTime(Convert.ToInt32(Year),
                               Convert.ToInt32(Month),
                               Day);
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CalenderDate", expiry);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@StartTime", expiry);
                    command.Parameters.AddWithValue("@EndTime", expiry);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    //SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    //DayTypeId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    //_dayTypeId = Convert.ToInt32(DayTypeId.Value);


                }

                using (SqlCommand command = new SqlCommand("Time_SetDefaultTime_EmployeeTxn", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();

                }


                //UpdateTimeWhenCalenderChange(CompanyId, CalenderDate, CreatedUser);
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

        public void CopyCalendar(int FromCompanyId, int ToCompanyId, string SelectedYear, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_CopyCompanyCalender", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@FromCompany", FromCompanyId);
                    command.Parameters.AddWithValue("@ToCompany", ToCompanyId);
                    command.Parameters.AddWithValue("@SelectedYear", SelectedYear);
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
                _dbConnection.Close();
            }
        }

        public void UpdateTimeWhenCalenderChange(int CompanyId, DateTime CalenderDate, string CreatedUser) {
            try
            {
                DataTable timeEmployees = new DataTable();

                using (SqlCommand command = new SqlCommand("Time_GetTimeEmployees", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(timeEmployees);
                    }
                }

                foreach (DataRow employee in timeEmployees.Rows) {

                    bool isAttendanceConsider = Convert.ToBoolean(employee["ConsiderAttendance"]);
                    string employeeCode = employee["EmployeeCode"].ToString();
                    DataTable prvsRawData = new DataTable();

                    using (SqlCommand command = new SqlCommand("Time_GetPrvsRawData", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@CalculateDate", CalenderDate);
                        command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(prvsRawData);
                        }
                    }

                    if (isAttendanceConsider)
                    {
                        using (SqlCommand command = new SqlCommand("TIME_DeletePunchTimesFromRawData", _dbConnection))
                        {
                            OpenDB();
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            command.Parameters.AddWithValue("@CalculateDate", CalenderDate);
                            command.ExecuteNonQuery();
                        }

                        foreach (DataRow rowData in prvsRawData.Rows)
                        {

                            string punchTime = rowData["CardTime"].ToString();
                            string attendanceType = rowData["AttendanceType"].ToString();
                            int year = Convert.ToDateTime(rowData["PunchDateTime"]).Year;
                            int month = Convert.ToDateTime(rowData["PunchDateTime"]).Month;
                            int day = Convert.ToDateTime(rowData["PunchDateTime"]).Day;

                            using (SqlCommand command = new SqlCommand("Time_ModifyAttendance", _dbConnection))
                            {
                                OpenDB();
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                                command.Parameters.AddWithValue("@Day", day);
                                command.Parameters.AddWithValue("@Month", month);
                                command.Parameters.AddWithValue("@Year", year);
                                command.Parameters.AddWithValue("@attType", attendanceType);
                                command.Parameters.AddWithValue("@punchTime", punchTime);
                                command.Parameters.AddWithValue("@ShiftId", "");
                                command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
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

        public void UpdateCalender(DateTime CalenderDate, int CompanyId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, bool Posted, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateCalender", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@StartTime", StartTime);
                    command.Parameters.AddWithValue("@EndTime", EndTime);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    //SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    //DayTypeId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    //_dayTypeId = Convert.ToInt32(DayTypeId.Value);


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


        public void DeleteCalender(DateTime CalenderDate, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("[Time_DELETECalender]", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    //SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    //DayTypeId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    //_dayTypeId = Convert.ToInt32(DayTypeId.Value);


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

        public void DeleteCalenderById(Int64 DayId, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DELETECalenderByDayaID", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@DayId", DayId);
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



        #endregion

        #endregion

        #region Destructor

        ~DayTypesDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
