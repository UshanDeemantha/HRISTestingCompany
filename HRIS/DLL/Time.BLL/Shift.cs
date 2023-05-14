using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using HRM.Time.DAL;
using System.Data.SqlClient;

namespace HRM.Time.BLL
{
    [DataObject]
    public class Shift
    {
        #region Fields

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _shiftId = 0;
        ShiftDAL objShiftDAL = new ShiftDAL();
        private DataTable dtTable;
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

        public Shift()
        {
            objShiftDAL = new ShiftDAL();
        }
        #endregion

        #region Methods

        #region Internal
        /// <summary>
        /// Sets the values.
        /// </summary>
        private void SetValues()
        {
            if (!IsError)
            {
                _isError = objShiftDAL.IsError;
                _errorMsg = objShiftDAL.ErrorMsg;
                _rosterGroupId = objShiftDAL.RosterGroupId;
                _uniqueNameResult = objShiftDAL.UniqueNameResult;
                _shiftSetupId = objShiftDAL.ShiftSetupId;
                _result = objShiftDAL.Result;
            }
        }
        #endregion

        #region ShiftType

        public void AddShiftType(string ShiftCode, string ShiftDescription, bool NightShift, bool OffShift, bool DoubleShift, bool GeneralShift, bool RosterShift, string CreatedUser, int CompanyId)
        {
            try
            {
                objShiftDAL.AddShiftType(ShiftCode,ShiftDescription,NightShift,OffShift,DoubleShift,GeneralShift,RosterShift,CreatedUser,CompanyId);
                _shiftId = objShiftDAL.ShiftId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateShiftType(int ShiftId, string ShiftDescription, bool NightShift, bool OffShift, bool DoubleShift, bool GeneralShift, bool RosterShift, string ModifyUser)
        {
            try
            {
                objShiftDAL.UpdateShiftType(ShiftId,ShiftDescription,NightShift,OffShift,DoubleShift,GeneralShift,RosterShift,ModifyUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetShiftType(int ShiftId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftType(ShiftId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetShiftTypeByCompany(int CompanyId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftTypeByCompany(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetShiftTypeByCode(string DayTypeCode, int CompanyId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftTypeByCode(DayTypeCode, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void DeleteShiftType(int ShiftId)
        {
            try
            {
                objShiftDAL.DeleteShiftType(ShiftId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetShiftTypeidforDelete(int ShiftId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftTypeidforDelete(ShiftId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        #endregion

        #region ShiftSetup

        public void InsertShiftSetup(string UserName)
        {
            try
            {
                objShiftDAL.InsertShiftSetup(UserName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        // Adding missing items
        public void AddShiftSetup(int ShiftId, int DayTypeId, string ShiftSetupName, string BackGroundColour, string fontcolor, int IsMidnightCross,
                            string ShiftInTime, string ShiftTimeOut, decimal ShiftHours, /*decimal MinShiftHours,*/ decimal AppliedLeaveQty,
                            int LateGracePeriod, int LateRounding, string LateRoundMode, int EarlyDepartureGracePeriod, int EarlyDepartureRounding, string EarlyDepartureRoundMode,
                            int PayCutGracePeriod, int PayCutRounding, string PayCutRoundMode, decimal LieuLeaveGrace, decimal LieuLeaveRounding, decimal LieuLeaveHrs,
                            int LeaveApplyDelayMints, decimal LeaveQtyWhenDelay, /*int OTCalStartTime,*/ bool cbEntitleEarlyOT,
                            /*bool OTSetup,*/ int EarlyOTGrace, string EarlyOTRounding, string EarlyOTRound, int PostOfGrace, int PostOfRounding, string PostOtRound, bool cbEntitleGrace)

        { 
        
            try
            {
                objShiftDAL.AddShiftSetup(ShiftId, DayTypeId, ShiftSetupName, BackGroundColour, fontcolor, IsMidnightCross,
                            ShiftInTime, ShiftTimeOut, ShiftHours, /*MinShiftHours,*/ AppliedLeaveQty,
                            LateGracePeriod, LateRounding, LateRoundMode, EarlyDepartureGracePeriod, EarlyDepartureRounding, EarlyDepartureRoundMode,
                            PayCutGracePeriod, PayCutRounding, PayCutRoundMode, LieuLeaveGrace, LieuLeaveRounding, LieuLeaveHrs,
                            LeaveApplyDelayMints, LeaveQtyWhenDelay, /*OTCalStartTime,*/ cbEntitleEarlyOT,
                            /*OTSetup,*/ EarlyOTGrace, EarlyOTRounding, EarlyOTRound, PostOfGrace, PostOfRounding, PostOtRound, cbEntitleGrace);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateShiftSetup(int setupId,int ShiftId, int DayTypeId, string ShiftSetupName, string BackGroundColour, string fontcolor, int IsMidnightCross,
                            string ShiftInTime, string ShiftTimeOut, decimal ShiftHours, /*decimal MinShiftHours,*/ decimal AppliedLeaveQty,
                            int LateGracePeriod, int LateRounding, string LateRoundMode, int EarlyDepartureGracePeriod, int EarlyDepartureRounding, string EarlyDepartureRoundMode,
                            int PayCutGracePeriod, int PayCutRounding, string PayCutRoundMode, decimal LieuLeaveGrace, decimal LieuLeaveRounding, decimal LieuLeaveHrs,
                            int LeaveApplyDelayMints, decimal LeaveQtyWhenDelay, /*int OTCalStartTime,*/ bool cbEntitleEarlyOT,
                            /*bool OTSetup,*/ int EarlyOTGrace, string EarlyOTRounding, string EarlyOTRound, int PostOfGrace, int PostOfRounding, string PostOtRound, bool cbEntitleGrace)
        {
            try
            {
                objShiftDAL.UpdateShiftSetup(setupId,ShiftId, DayTypeId, ShiftSetupName, BackGroundColour, fontcolor, IsMidnightCross,
                            ShiftInTime, ShiftTimeOut, ShiftHours, /*MinShiftHours,*/ AppliedLeaveQty,
                            LateGracePeriod, LateRounding, LateRoundMode, EarlyDepartureGracePeriod, EarlyDepartureRounding, EarlyDepartureRoundMode,
                            PayCutGracePeriod, PayCutRounding, PayCutRoundMode, LieuLeaveGrace, LieuLeaveRounding, LieuLeaveHrs,
                            LeaveApplyDelayMints, LeaveQtyWhenDelay, /*OTCalStartTime,*/ cbEntitleEarlyOT,
                            /*OTSetup,*/ EarlyOTGrace, EarlyOTRounding, EarlyOTRound, PostOfGrace, PostOfRounding, PostOtRound, cbEntitleGrace);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public void AddShiftSetupByOTType(int dtShiftType, /*int OTTypesId, bool IsEntitle,*/
                       int EarlyOTGracePeriod, int EralyOTRounding, string EarlyOTRoundMode, int OTGracePeriod, int OTRounding, string OTRoundMode/*, bool OTSetup*/)
        {
            try
            {
                objShiftDAL.AddShiftSetupByOTType(dtShiftType, /*OTTypesId, IsEntitle,*/
                       EarlyOTGracePeriod, EralyOTRounding, EarlyOTRoundMode, OTGracePeriod, OTRounding, OTRoundMode/*, OTSetup*/);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public DataTable checkShifttyype(int ShiftId /*,int OTTypeId*/)
        {
            try
            {
                dtTable = objShiftDAL.checkShifttyype(ShiftId /*, OTTypeId*/);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void UpdateShiftSetupByOTType(int dtShiftType, /*int OTTypesId, bool IsEntitle,*/
                       int EarlyOTGracePeriod, int EralyOTRounding, string EarlyOTRoundMode, int OTGracePeriod, int OTRounding, string OTRoundMode)
        {
            try
            {
                objShiftDAL.UpdateShiftSetupByOTType(dtShiftType, /*OTTypesId, IsEntitle,*/
                       EarlyOTGracePeriod, EralyOTRounding, EarlyOTRoundMode, OTGracePeriod, OTRounding, OTRoundMode);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void InsertShiftSetupByOTType(int dtShiftType, /*int OTTypesId, bool IsEntitle*/
                 int EarlyOTGracePeriod, int EralyOTRounding, string EarlyOTRoundMode, int OTGracePeriod, int OTRounding, string OTRoundMode)
        {
            try
            {
                objShiftDAL.InsertShiftSetupByOTType(dtShiftType, /*OTTypesId, IsEntitle,*/
                       EarlyOTGracePeriod, EralyOTRounding, EarlyOTRoundMode, OTGracePeriod, OTRounding, OTRoundMode);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public void DeleteShiftSetup(int ShiftSetupId)
        {
            try
            {
                objShiftDAL.DeleteShiftSetup(ShiftSetupId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public void DeleteShiftSetupByOTType(int ShiftSetupId)
        {
            try
            {
                objShiftDAL.DeleteShiftSetupByOTType(ShiftSetupId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public DataTable GetShiftSetupByOTType(int ShiftSetupId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftSetupByOTType(ShiftSetupId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetShiftSetup(int ShiftSetupId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftSetup(ShiftSetupId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetShiftSetupByCompanyId(int CompanyId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftSetupByCompanyId(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        public DataTable checkshiftId(int ShiftId)
        {
            try
            {
                dtTable = objShiftDAL.checkshiftId(ShiftId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetShiftSetupByShiftID(int ShiftId)
        {
            try
            {
                dtTable = objShiftDAL.GetShiftSetupByShiftID(ShiftId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public void DeleteEmployeeShiftByShedulerId(long shedulerID)
        {
            try
            {
                objShiftDAL.DeleteEmployeeShiftShedulerByID(shedulerID);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void AddEmployeeShiftSchedule(int CompanyId, long EmployeeId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, string DateName, DateTime ShiftDate, int ShiftId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                objShiftDAL.AddEmployeeShiftSchedule(CompanyId, EmployeeId, StartTime, EndTime, Day, Month, Year, DayTypeId, DateName, ShiftDate, ShiftId, Posted, CreatedUser, Subject);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetShiftSetupDetails(int CompanyId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objShiftDAL.GetShiftSetupDetails(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }


        public DataTable GetDayTypeByDate(int CompanyId, DateTime CalDate, int Month, int Year, int date)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objShiftDAL.GetDayTypeByDate(CompanyId, CalDate, Year, Month, date);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }


        #endregion

        #region RosterGroup

        public void AddRosterGroup(string RosterGroup, string Remarks, string CreatedUser, int CompanyId)
        {
            try
            {
                objShiftDAL.AddRosterGroup(RosterGroup, Remarks, CreatedUser, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateRosterGroup(int RosterGroupId, string RosterGroup, string Remarks, string ModifiedUser, int CompanyId)
        {
            try
            {
                objShiftDAL.UpdateRosterGroup(RosterGroupId, RosterGroup, Remarks, ModifiedUser, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteRosterGroup(int RosterGroupId)
        {
            try
            {
                objShiftDAL.DeleteRosterGroup(RosterGroupId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetRosterGroupByTypeId(int RosterGroupId)
        {
            try
            {
                dtTable = objShiftDAL.GetRosterGroupByTypeId(RosterGroupId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GeRosterGroup(int RosterGroupId)
        {
            try
            {
                dtTable = objShiftDAL.GeRosterGroup(RosterGroupId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GeRosterGroupForCompany(int CompanyId)
        {
            try
            {
                dtTable = objShiftDAL.GeRosterGroupForCompany(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        #endregion
        #endregion

        #region Destructor

        ~Shift()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

