using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.Time.DAL;
using System.ComponentModel;

namespace HRM.Time.BLL
{
    public class TimeOption
    {
       #region Fields

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        //private int _dayTypeId = 0;
        TimeOptionDAL  objTimeOptionDAL = new TimeOptionDAL();
      
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

        
        #endregion

        #region Constructor

        public TimeOption()
        {
            objTimeOptionDAL = new TimeOptionDAL();
        }
        #endregion

        #region Methods

         #region TimeOption

        public void AddTimeOption(int CompanyId, bool FunctionKeyUse, bool MonitorTimeInOutOnly, bool RosterBased, bool OTApprovalProcess, bool LeaveApprovalProcess, bool DoubleShift)
        {
            try
            {
               objTimeOptionDAL.AddTimeOption(CompanyId,FunctionKeyUse,MonitorTimeInOutOnly,RosterBased,OTApprovalProcess,LeaveApprovalProcess,DoubleShift);
            }
            catch (Exception ex) 
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void UpdateTimeOption(int CompanyId, bool FunctionKeyUse, bool MonitorTimeInOutOnly, bool RosterBased, bool OTApprovalProcess, bool LeaveApprovalProcess, bool DoubleShift)
        {
            try
            {
                objTimeOptionDAL.UpdateTimeOption(CompanyId,FunctionKeyUse,MonitorTimeInOutOnly,RosterBased,OTApprovalProcess,LeaveApprovalProcess,DoubleShift);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetTimeOption(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                  
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
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
                    objTimeOptionDAL.CheckTimeOption(CompanyId);
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
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
                objTimeOptionDAL.AddLateAttendance(CompanyId, OptionType, OptionDescription, Formula, LeaveType, IsActive, CreatedUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void UpdateLateAttendance(int LateSetupId, string OptionDescription, string Formula, string LeaveType, bool IsActive, string UpdateUser)
        {
            try
            {
                objTimeOptionDAL.UpdateLateAttendance(LateSetupId,OptionDescription, Formula, LeaveType, IsActive, UpdateUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateLateAttendanceFalse(int LateSetupId, bool IsActive, string UpdateUser)
        {
            try
            {
                objTimeOptionDAL.UpdateLateAttendanceFalse(LateSetupId, IsActive, UpdateUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetLateAttendanceOption(int CompanyId)
        {
             DataTable dtTable = new DataTable();
            {
                try
                {
                    dtTable = objTimeOptionDAL.GetLateAttendanceOption(CompanyId);
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
                }

                return dtTable;
            }
        }


        #endregion

        #endregion


        #region Destructor

        ~TimeOption()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
