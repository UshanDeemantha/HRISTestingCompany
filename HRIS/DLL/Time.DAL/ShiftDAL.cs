using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM.Time.DAL
{
    public class ShiftDAL
    {
        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _shiftId = 0;
        private int _rosterGroupId = 0;
        private bool _uniqueNameResult = false;
        private int _shiftSetupId = 0;
        private string _result = string.Empty;

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
        public int ShiftId
        {
            get { return _shiftId; }
        }
        public int RosterGroupId
        {
            get { return _rosterGroupId; }
        }

        public bool UniqueNameResult
        {
            get { return _uniqueNameResult; }
        }

        public int ShiftSetupId
        {
            get { return _shiftSetupId; }
        }

        public string Result
        {
            get { return _result; }
        }
        #endregion

        #region Constructor

        public ShiftDAL()
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

        #region ShiftType
        public void AddShiftType(string ShiftCode, string ShiftDescription, bool NightShift, bool OffShift, bool DoubleShift, bool GeneralShift, bool RosterShift, string CreatedUser, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddShiftType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ShiftCode", ShiftCode);//ShiftCode
                    command.Parameters.AddWithValue("@ShiftDescription", ShiftDescription);
                    command.Parameters.AddWithValue("@NightShift", NightShift);
                    command.Parameters.AddWithValue("@OffShift", OffShift);
                    command.Parameters.AddWithValue("@DoubleShift", DoubleShift);
                    command.Parameters.AddWithValue("@GeneralShift", GeneralShift);
                    command.Parameters.AddWithValue("@RousterShift", RosterShift);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    SqlParameter shiftTypeId = new SqlParameter("@ShiftId", SqlDbType.Int, 8);
                    shiftTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(shiftTypeId);
                    command.ExecuteNonQuery();
                    _shiftId = Convert.ToInt32(shiftTypeId.Value);
                    if (_shiftId < 0)
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

        public void UpdateShiftType(int ShiftId, string ShiftDescription, bool NightShift, bool OffShift, bool DoubleShift, bool GeneralShift, bool RosterShift, string ModifyUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdteShiftType", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@ShiftDescription", ShiftDescription);
                    command.Parameters.AddWithValue("@NightShift", NightShift);
                    command.Parameters.AddWithValue("@OffShift", OffShift);
                    command.Parameters.AddWithValue("@DoubleShift", DoubleShift);
                    command.Parameters.AddWithValue("@GeneralShift", GeneralShift);
                    command.Parameters.AddWithValue("@RosterShift", RosterShift);
                    command.Parameters.AddWithValue("@ModifiedUser", ModifyUser);                  
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

        public DataTable GetShiftType(int ShiftId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ShiftId", ShiftId);
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

        public DataTable GetShiftTypeByCompany(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftTypeByCompany", _dbConnection))
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

        public DataTable GetShiftTypeByCode(string DayTypeCode, int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftTypeByCode", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ShiftTypeCode", DayTypeCode);
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

        public void DeleteShiftType(int ShiftId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Time_DeleteShift", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
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

        public DataTable GetShiftTypeidforDelete(int ShiftId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftTypeIdforDelete", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ShiftId", ShiftId);
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

        #endregion

        #region ShiftSetup


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

        public void AddShiftSetup(int ShiftId, int DayTypeId, string ShiftSetupName, string BackGroundColour,string fontcolor, int IsMidnightCross,
                            string ShiftInTime, string ShiftTimeOut, decimal ShiftHours, /*decimal MinShiftHours,*/ decimal AppliedLeaveQty,
                            int LateGracePeriod, int LateRounding, string LateRoundMode, int EarlyDepartureGracePeriod, int EarlyDepartureRounding, string EarlyDepartureRoundMode,
                            int PayCutGracePeriod, int PayCutRounding, string PayCutRoundMode, decimal LieuLeaveGrace, decimal LieuLeaveRounding, decimal LieuLeaveHrs,
                            int LeaveApplyDelayMints, decimal LeaveQtyWhenDelay, /*int OTCalStartTime,*/bool cbEntitleEarlyOT, 
                          /*  bool OTSetup,*/ int EarlyOTGrace, string EarlyOTRounding, string EarlyOTRound, int PostOfGrace, int PostOfRounding, string PostOtRound, bool cbEntitleGrace)
        {
            try 
            {
                using (SqlCommand command = new SqlCommand("Time_AddShiftSetup", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@ShiftSetupName", ShiftSetupName);
                    command.Parameters.AddWithValue("@FontColour", BackGroundColour);
                    command.Parameters.AddWithValue("@Fncolor", fontcolor);
                    command.Parameters.AddWithValue("@IsMidnightCross", IsMidnightCross);
                    command.Parameters.AddWithValue("@ShiftInTime", ShiftInTime);
                    command.Parameters.AddWithValue("@ShiftTimeOut", ShiftTimeOut);
                    command.Parameters.AddWithValue("@ShiftHours", ShiftHours);
                   // command.Parameters.AddWithValue("@MinShiftHours", MinShiftHours);
                    command.Parameters.AddWithValue("@AppliedLeaveQty", AppliedLeaveQty);
                    command.Parameters.AddWithValue("@LateGracePeriod", LateGracePeriod);
                    command.Parameters.AddWithValue("@LateRounding", LateRounding);
                    command.Parameters.AddWithValue("@LateRoundMode", LateRoundMode);
                    command.Parameters.AddWithValue("@EarlyDepartureGracePeriod", EarlyDepartureGracePeriod);
                    command.Parameters.AddWithValue("@EarlyDepartureRounding", EarlyDepartureRounding);
                    command.Parameters.AddWithValue("@EarlyDepartureRoundMode", EarlyDepartureRoundMode);
                    command.Parameters.AddWithValue("@PayCutGracePeriod", PayCutGracePeriod);
                    command.Parameters.AddWithValue("@PayCutRounding", PayCutRounding);
                    command.Parameters.AddWithValue("@PayCutRoundMode", PayCutRoundMode);
                    command.Parameters.AddWithValue("@LieuLeaveGrace", LieuLeaveGrace);
                    command.Parameters.AddWithValue("@LieuLeaveRounding", LieuLeaveRounding);
                    command.Parameters.AddWithValue("@LieuLeaveHrs", LieuLeaveHrs);
                    command.Parameters.AddWithValue("@LeaveApplyDelayMints", LeaveApplyDelayMints);
                    command.Parameters.AddWithValue("@LeaveQtyWhenDelay", LeaveQtyWhenDelay);
                  //  command.Parameters.AddWithValue("@OTCalStartTime", OTCalStartTime);
                    command.Parameters.AddWithValue("@IsEarlyOTEntitle", cbEntitleEarlyOT);
                    // missing ones
                    //command.Parameters.AddWithValue("@OTType", OTType);
                   // command.Parameters.AddWithValue("@OTSetup", OTSetup);
                    command.Parameters.AddWithValue("@EarlyOTGrace", EarlyOTGrace);
                    command.Parameters.AddWithValue("@EarlyOTRounding", EarlyOTRounding);
                    command.Parameters.AddWithValue("@EarlyOTRound", EarlyOTRound);
                    command.Parameters.AddWithValue("@PostOfGrace", PostOfGrace);
                    command.Parameters.AddWithValue("@PostOfRounding", PostOfRounding);
                    command.Parameters.AddWithValue("@PostOtRound", PostOtRound);
                    command.Parameters.AddWithValue("@EntitleGrace", cbEntitleGrace);



                    SqlParameter ShiftSetupId = new SqlParameter("@ShiftSetupId", SqlDbType.Int, 8);
                    ShiftSetupId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ShiftSetupId);

                    SqlParameter Result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    Result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(Result);


                    command.ExecuteNonQuery();
                    _shiftSetupId = Convert.ToInt32(ShiftSetupId.Value.ToString());
                    _result = Result.Value.ToString();
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

        public void UpdateShiftSetup(int setupId, int ShiftId, int DayTypeId, string ShiftSetupName, string BackGroundColour, string fontcolor, int IsMidnightCross,
                            string ShiftInTime, string ShiftTimeOut, decimal ShiftHours, /*decimal MinShiftHours,*/ decimal AppliedLeaveQty,
                            int LateGracePeriod, int LateRounding, string LateRoundMode, int EarlyDepartureGracePeriod, int EarlyDepartureRounding, string EarlyDepartureRoundMode,
                            int PayCutGracePeriod, int PayCutRounding, string PayCutRoundMode, decimal LieuLeaveGrace, decimal LieuLeaveRounding, decimal LieuLeaveHrs,
                            int LeaveApplyDelayMints, decimal LeaveQtyWhenDelay, /*int OTCalStartTime,*/bool cbEntitleEarlyOT,
                          /*  bool OTSetup,*/ int EarlyOTGrace, string EarlyOTRounding, string EarlyOTRound, int PostOfGrace, int PostOfRounding, string PostOtRound, bool cbEntitleGrace)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateShiftSetup", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ShiftSetupId", setupId);
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@ShiftSetupName", ShiftSetupName);
                    command.Parameters.AddWithValue("@FontColour", BackGroundColour);
                    command.Parameters.AddWithValue("@Fncolor", fontcolor);
                    command.Parameters.AddWithValue("@IsMidnightCross", IsMidnightCross);
                    command.Parameters.AddWithValue("@ShiftInTime", ShiftInTime);
                    command.Parameters.AddWithValue("@ShiftTimeOut", ShiftTimeOut);
                    command.Parameters.AddWithValue("@ShiftHours", ShiftHours);
                    // command.Parameters.AddWithValue("@MinShiftHours", MinShiftHours);
                    command.Parameters.AddWithValue("@AppliedLeaveQty", AppliedLeaveQty);
                    command.Parameters.AddWithValue("@LateGracePeriod", LateGracePeriod);
                    command.Parameters.AddWithValue("@LateRounding", LateRounding);
                    command.Parameters.AddWithValue("@LateRoundMode", LateRoundMode);
                    command.Parameters.AddWithValue("@EarlyDepartureGracePeriod", EarlyDepartureGracePeriod);
                    command.Parameters.AddWithValue("@EarlyDepartureRounding", EarlyDepartureRounding);
                    command.Parameters.AddWithValue("@EarlyDepartureRoundMode", EarlyDepartureRoundMode);
                    command.Parameters.AddWithValue("@PayCutGracePeriod", PayCutGracePeriod);
                    command.Parameters.AddWithValue("@PayCutRounding", PayCutRounding);
                    command.Parameters.AddWithValue("@PayCutRoundMode", PayCutRoundMode);
                    command.Parameters.AddWithValue("@LieuLeaveGrace", LieuLeaveGrace);
                    command.Parameters.AddWithValue("@LieuLeaveRounding", LieuLeaveRounding);
                    command.Parameters.AddWithValue("@LieuLeaveHrs", LieuLeaveHrs);
                    command.Parameters.AddWithValue("@LeaveApplyDelayMints", LeaveApplyDelayMints);
                    command.Parameters.AddWithValue("@LeaveQtyWhenDelay", LeaveQtyWhenDelay);
                    //  command.Parameters.AddWithValue("@OTCalStartTime", OTCalStartTime);
                    command.Parameters.AddWithValue("@IsEarlyOTEntitle", cbEntitleEarlyOT);
                    // missing ones
                    //command.Parameters.AddWithValue("@OTType", OTType);
                    // command.Parameters.AddWithValue("@OTSetup", OTSetup);
                    command.Parameters.AddWithValue("@EarlyOTGrace", EarlyOTGrace);
                    command.Parameters.AddWithValue("@EarlyOTRounding", EarlyOTRounding);
                    command.Parameters.AddWithValue("@EarlyOTRound", EarlyOTRound);
                    command.Parameters.AddWithValue("@PostOfGrace", PostOfGrace);
                    command.Parameters.AddWithValue("@PostOfRounding", PostOfRounding);
                    command.Parameters.AddWithValue("@PostOtRound", PostOtRound);
                    command.Parameters.AddWithValue("@EntitleGrace", cbEntitleGrace);

                    
                      
                    //SqlParameter ShiftSetupId = new SqlParameter("@ShiftSetupId", SqlDbType.Int, 8);
                    //ShiftSetupId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(ShiftSetupId);

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

        public void DeleteShiftSetup(int ShiftSetupId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Time_DeleteShiftSetup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ShiftSetupId", ShiftSetupId);
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

        public void DeleteEmployeeShiftShedulerByID(long Id)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteindividualEmployeeShiftByID", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SchedulerId", Id);
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


        public void AddEmployeeShiftSchedule(int CompanyId, long EmployeeId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, string DateName, DateTime ShiftDate, int ShiftId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddEmployeeShiftSchedule", _dbConnection))
                {

                    DateTime expiry = new DateTime(Convert.ToInt32(Year),
                              Convert.ToInt32(Month),
                              Day);
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@StartTime", expiry);
                    command.Parameters.AddWithValue("@EndTime", expiry);
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
        public DataTable checkShifttyype(int ShiftId /*, int OTTypeId*/)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_checkOttype", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ShiftSetupId", ShiftId);
                       // command.Parameters.AddWithValue("@OTTypeId", OTTypeId);
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


        public void AddShiftSetupByOTType(int dtShiftType, /*int OTTypesId, bool IsEntitle,*/
                       int EarlyOTGracePeriod, int EralyOTRounding, string EarlyOTRoundMode, int OTGracePeriod, int OTRounding, string OTRoundMode/*, bool OTSetup*/)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddShiftSetupByOTType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ShiftSetupId", dtShiftType);
                    //command.Parameters.AddWithValue("@OTTypeId", OTTypesId);
                    //command.Parameters.AddWithValue("@IsEntitle", IsEntitle);
                    command.Parameters.AddWithValue("@EarlyOTGracePeriod", EarlyOTGracePeriod);
                    command.Parameters.AddWithValue("@EarlyOTRounding", EralyOTRounding);
                    command.Parameters.AddWithValue("@EarlyOTRoundMode", EarlyOTRoundMode);
                    command.Parameters.AddWithValue("@OTGracePeriod", OTGracePeriod);
                    command.Parameters.AddWithValue("@OTRounding", OTRounding);
                    command.Parameters.AddWithValue("@OTRoundMode", OTRoundMode);
                    // new one add
                  //  command.Parameters.AddWithValue("@OTSetup", OTSetup);

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

        public void UpdateShiftSetupByOTType(int dtShiftType, /*int OTTypesId, bool IsEntitle,*/
                       int EarlyOTGracePeriod, int EralyOTRounding, string EarlyOTRoundMode, int OTGracePeriod, int OTRounding, string OTRoundMode)
        {
            try
            {
                {
                    using (SqlCommand command = new SqlCommand("Time_UpdateShiftSetupByOTType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        OpenDB();

                        command.Parameters.AddWithValue("@ShiftSetupId", dtShiftType);
                       // command.Parameters.AddWithValue("@OTTypeId", OTTypesId);
                       // command.Parameters.AddWithValue("@IsEntitle", IsEntitle);
                        command.Parameters.AddWithValue("@EarlyOTGracePeriod", EarlyOTGracePeriod);
                        command.Parameters.AddWithValue("@EarlyOTRounding", EralyOTRounding);
                        command.Parameters.AddWithValue("@EarlyOTRoundMode", EarlyOTRoundMode);
                        command.Parameters.AddWithValue("@OTGracePeriod", OTGracePeriod);
                        command.Parameters.AddWithValue("@OTRounding", OTRounding);
                        command.Parameters.AddWithValue("@OTRoundMode", OTRoundMode);

                        command.ExecuteNonQuery();
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

        public void InsertShiftSetupByOTType(int dtShiftType, /*int OTTypesId, bool IsEntitle,*/
               int EarlyOTGracePeriod, int EralyOTRounding, string EarlyOTRoundMode, int OTGracePeriod, int OTRounding, string OTRoundMode)
        {
            try
            {
                {
                    using (SqlCommand command = new SqlCommand("Time_InsertShiftSetupByOTType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        OpenDB();

                        command.Parameters.AddWithValue("@ShiftSetupId", dtShiftType);
                      //  command.Parameters.AddWithValue("@OTTypeId", OTTypesId);
                       // command.Parameters.AddWithValue("@IsEntitle", IsEntitle);
                        command.Parameters.AddWithValue("@EarlyOTGracePeriod", EarlyOTGracePeriod);
                        command.Parameters.AddWithValue("@EarlyOTRounding", EralyOTRounding);
                        command.Parameters.AddWithValue("@EarlyOTRoundMode", EarlyOTRoundMode);
                        command.Parameters.AddWithValue("@OTGracePeriod", OTGracePeriod);
                        command.Parameters.AddWithValue("@OTRounding", OTRounding);
                        command.Parameters.AddWithValue("@OTRoundMode", OTRoundMode);

                        command.ExecuteNonQuery();
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

        public void DeleteShiftSetupByOTType(int ShiftSetupId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteShiftSetupByOTType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ShiftSetupId", ShiftSetupId);
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

        public DataTable GetShiftSetupByOTType(int ShiftSetupId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftSetupByOTType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ShiftSetupId", ShiftSetupId);
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

        public DataTable GetShiftSetup(int ShiftSetupId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftSetup", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ShiftSetupId", ShiftSetupId);
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

        public DataTable GetShiftSetupByCompanyId(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftSetupByCompanyId", _dbConnection))
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

        public DataTable checkshiftId(int ShiftId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_CheckShiftIdDelte", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ShiftId", ShiftId);
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

        public DataTable GetShiftSetupByShiftID(int ShiftId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetShiftSetupByShiftID", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ShiftId", ShiftId);
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

        #endregion

        #region RosterGroup
        public void AddRosterGroup(string RosterGroup, string Remarks, string CreatedUser, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddRousterGroup", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@RosterGroup", RosterGroup);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    SqlParameter RosterGroupId = new SqlParameter("@RosterGroupId", SqlDbType.Int, 8);
                    RosterGroupId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(RosterGroupId);

                    SqlParameter UniqueNameResult = new SqlParameter("@UniqueNameResult", SqlDbType.Int, 8);
                    UniqueNameResult.Direction = ParameterDirection.Output;
                    command.Parameters.Add(UniqueNameResult);

                    command.ExecuteNonQuery();
                    _rosterGroupId = Convert.ToInt32(RosterGroupId.Value);
                    _uniqueNameResult = Convert.ToBoolean(UniqueNameResult.Value);
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

        public void UpdateRosterGroup(int RosterGroupId, string RosterGroup, string Remarks, string ModifiedUser, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateRousterGroup", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    command.Parameters.AddWithValue("@RosterGroup", RosterGroup);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
                    command.Parameters.AddWithValue("@ModifiedUser", ModifiedUser);
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

        public void DeleteRosterGroup(int RosterGroupId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteRousterGroup", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    SqlParameter UniqueNameResult = new SqlParameter("@UniqueNameResult", SqlDbType.Int, 8);
                    UniqueNameResult.Direction = ParameterDirection.Output;
                    command.Parameters.Add(UniqueNameResult);
                    command.ExecuteNonQuery();
                    _uniqueNameResult = Convert.ToBoolean(UniqueNameResult.Value);

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

        public DataTable GetRosterGroupByTypeId(int RosterGroupId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("HR_GetRosterGroupByTypeId", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.Add("@RosterGroupId", RosterGroupId);
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

        public DataTable GeRosterGroup(int RosterGroupId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetRosterGroup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
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

        public DataTable GeRosterGroupForCompany(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetRosterGroupForCompany", _dbConnection))
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

        #endregion
        #endregion

        #region Destructor

        ~ShiftDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}