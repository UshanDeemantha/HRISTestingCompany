using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM.Time.DAL
{
    public class TimeOptionDAL
    {
        #region Fields

        private SqlConnection _dbConnection;
        private bool _isError = false;
        private string _errorMsg = string.Empty;

        private bool _functionKeyUse;
        private bool _monitorTimeInOutOnly;
        private bool _rosterBased;
        private bool _oTApprovalProcess;
        private bool _leaveApprovalProcess;
        private bool _doubleShift;
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

        public bool IsFunctionKeyUse
        {
            get { return _functionKeyUse; }
        }

        public bool IsMonitorTimeInOutOnly
        {
            get { return _monitorTimeInOutOnly; }

            set { _monitorTimeInOutOnly = value; }
        }

        public bool IsRosterBased
        {
            get { return _rosterBased; }
        }

        public bool IsOTApprovalProcess
        {
            get { return _oTApprovalProcess; }
        }

        public bool IsLeaveApprovalProcess
        {
            get { return _leaveApprovalProcess; }
        }

        public bool IsDoubleShift
        {
            get { return _doubleShift; }
        }
        #endregion

        #region Constructor

        public TimeOptionDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        public TimeOptionDAL(int CompanyId)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            DataTable dtOptions = GetTimeOption(CompanyId);
            if (dtOptions.Rows.Count > 0)
            {
                _functionKeyUse = Convert.ToBoolean(dtOptions.Rows[0]["FunctionKeyUse"]);
                _monitorTimeInOutOnly = Convert.ToBoolean(dtOptions.Rows[0]["MonitorTimeInOutOnly"]);
                _rosterBased = Convert.ToBoolean(dtOptions.Rows[0]["RosterBased"]);
                _oTApprovalProcess = Convert.ToBoolean(dtOptions.Rows[0]["OTApprovalProcess"]);
                _leaveApprovalProcess = Convert.ToBoolean(dtOptions.Rows[0]["LeaveApprovalProcess"]);
                _doubleShift = Convert.ToBoolean(dtOptions.Rows[0]["DoubleShift"]);
            }
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


        #region TimeOption

        public void AddTimeOption(int CompanyId, bool FunctionKeyUse, bool MonitorTimeInOutOnly, bool RosterBased, bool OTApprovalProcess, bool LeaveApprovalProcess, bool DoubleShift)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddTimeOption", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@FunctionKeyUse", FunctionKeyUse);
                    command.Parameters.AddWithValue("@MonitorTimeInOutOnly", MonitorTimeInOutOnly);
                    command.Parameters.AddWithValue("@RosterBased", RosterBased);
                    command.Parameters.AddWithValue("@OTApprovalProcess", OTApprovalProcess);
                    command.Parameters.AddWithValue("@LeaveApprovalProcess", LeaveApprovalProcess);
                    command.Parameters.AddWithValue("@DoubleShift", DoubleShift);

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

        public void UpdateTimeOption(int CompanyId, bool FunctionKeyUse, bool MonitorTimeInOutOnly, bool RosterBased, bool OTApprovalProcess, bool LeaveApprovalProcess, bool DoubleShift)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateTimeOption", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@FunctionKeyUse", FunctionKeyUse);
                    command.Parameters.AddWithValue("@MonitorTimeInOutOnly", MonitorTimeInOutOnly);
                    command.Parameters.AddWithValue("@RosterBased", RosterBased);
                    command.Parameters.AddWithValue("@OTApprovalProcess", OTApprovalProcess);
                    command.Parameters.AddWithValue("@LeaveApprovalProcess", LeaveApprovalProcess);
                    command.Parameters.AddWithValue("@DoubleShift", DoubleShift);

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

        public DataTable GetTimeOption(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetTimeOption", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
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


        

        //public DataTable GetOTTypeById(int OTTypeId)
        //{
        //    using (DataTable dtTable = new DataTable())
        //    {
        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand("Time_GetOTTypeById", _dbConnection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                OpenDB();
        //                command.Parameters.AddWithValue("@OTTypeId", OTTypeId);
        //                using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
        //                {
        //                    daAdapter.Fill(dtTable);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _isError = true;
        //            _errorMsg = ex.Message;
        //        }
        //        finally
        //        {
        //            _dbConnection.Close();

        //        }
        //        return dtTable;
        //    }
        //}

        public DataTable CheckTimeOption(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetOTTypeByCode", _dbConnection))
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


        #endregion

        #region LateAttendanceOption

        public void AddLateAttendance(int CompanyId, string OptionType, string OptionDescription, string Formula, string LeaveType, bool IsActive, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddLateAttendanceOption", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OptionType", OptionType);
                    command.Parameters.AddWithValue("@OptionDescription", OptionDescription);
                    command.Parameters.AddWithValue("@Formula", Formula);
                    command.Parameters.AddWithValue("@LeaveType", LeaveType);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
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

        public void UpdateLateAttendance(int LateSetupId, string OptionDescription, string Formula, string LeaveType, bool IsActive, string UpdateUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateLateAttendanceOption", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LateSetupId", LateSetupId);
                    command.Parameters.AddWithValue("@OptionDescription", OptionDescription);
                    command.Parameters.AddWithValue("@Formula", Formula);
                    command.Parameters.AddWithValue("@LeaveType", LeaveType);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@UpdateUser", UpdateUser);

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

        public void UpdateLateAttendanceFalse(int LateSetupId,  bool IsActive, string UpdateUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateLateAttendanceOptionFalse", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LateSetupId", LateSetupId);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@UpdateUser", UpdateUser);

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


        public DataTable GetLateAttendanceOption(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetLateAttendanceOption", _dbConnection))
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

          #endregion
        #endregion

        #region Destructor

        ~TimeOptionDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
