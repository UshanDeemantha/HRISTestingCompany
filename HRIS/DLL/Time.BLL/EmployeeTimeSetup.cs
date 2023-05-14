using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HRM.Time.DAL;
using System.ComponentModel;

namespace HRM.Time.BLL
{
    [DataObject]
    public class EmployeeTimeSetup
    {
        #region Fields

        private DataTable dtTable;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private long _employeeID = 0;
        EmployeeTimeSetupDAL objEmpTimeSetup;

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
        public long EmployeeID
        {
            get { return _employeeID; }
        }
        
        #endregion

       #region Constructor

        public EmployeeTimeSetup()
        {
            objEmpTimeSetup = new EmployeeTimeSetupDAL();
        }
        #endregion


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeTime(long EmployeeId, int CompanyId)
        {
            try
            {
                dtTable = objEmpTimeSetup.GetEmployeeTime(EmployeeId, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeTime(int jobID, long OrgID, int CompanyId)
        {
            try
            {
                dtTable = objEmpTimeSetup.GetEmployeeTime(jobID, OrgID, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeTimeByOrgID(int jobiD, long OrgID, int CompanyId)
        {
            try
            {
                dtTable = objEmpTimeSetup.GetEmployeeTimeByOrgID(jobiD, OrgID, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeesInOrNotInTime(int jobiD, long OrgID, int CompanyId, string Type)
        {
            try
            {
                dtTable = objEmpTimeSetup.GetEmployeesInOrNotInTime(jobiD, OrgID, CompanyId, Type);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeesInOrNotInTime2(int jobiD, long OrgID, int CompanyId, string Type)
        {
            try
            {
                dtTable = objEmpTimeSetup.GetEmployeesInOrNotInTime(jobiD, OrgID, CompanyId, Type);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeLate(long EmployeeId, int CompanyId)
        {
            try
            {
                dtTable = objEmpTimeSetup.GetEmployeeLate(EmployeeId, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public void AddNewEmpLeaveLev(long hfEmloyeeId, string CreatedUser,DateTime efectiveDate,bool isChecked)
        {
            try
            {
                objEmpTimeSetup.AddNewEmpLeaveLev(hfEmloyeeId, CreatedUser, efectiveDate, isChecked);
                // _shiftId = objShiftDAL.ShiftId;
                // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void AddNewEmpLeaveLev( long hfEmloyeeId, string CreatedUser)
        {
            try
            {
                objEmpTimeSetup.AddNewEmpLeaveLev( hfEmloyeeId,CreatedUser);
                // _shiftId = objShiftDAL.ShiftId;
                // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddTimeSetup(int CompanyId, long hfEmloyeeId, string CardNo, long ImmediateSuperviser, int DefaultShiftId, int RosterGroupId, bool OTEntitlement, bool EarlyOTEntitlement,
                                  bool LievLeaveEntitlement, bool LateAttendanceCalculation, bool EarlyDepartureCalculation, bool PayCutCalculation, bool ConsiderAttendance,bool NIghtOtEntitle, int LateSetupId, string CreatedUser)
        {
            try
            {
                objEmpTimeSetup.AddEmployeeTimeSetup(CompanyId, hfEmloyeeId, CardNo, ImmediateSuperviser, DefaultShiftId, RosterGroupId, OTEntitlement, EarlyOTEntitlement,
                                LievLeaveEntitlement, LateAttendanceCalculation, EarlyDepartureCalculation, PayCutCalculation, ConsiderAttendance, NIghtOtEntitle, LateSetupId, CreatedUser);
                // _shiftId = objShiftDAL.ShiftId;
                // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void ChangeEmployeeAttendaceDetails(int CompanyId, long EmployeeId, string CreatedUser, bool ChangelateAttendace, bool ChangeEarlyDepartures, bool EarlyOT, bool PostOT, bool ChangeOT20, string Reason, DateTime FromDate, DateTime Todate)
        {
            try
            {
                objEmpTimeSetup.ChangeEmployeeAttendaceDetails(CompanyId, EmployeeId, CreatedUser, ChangelateAttendace, ChangeEarlyDepartures, EarlyOT, PostOT, ChangeOT20, Reason, FromDate, Todate);
                // _shiftId = objShiftDAL.ShiftId;
                // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateTimeSetup(long hfEmloyeeId, string CardNo, long ImmediateSuperviser, int DefaultShiftId, int RosterGroupId, bool OTEntitlement, bool EarlyOTEntitlement,
                             bool LievLeaveEntitlement, bool LateAttendanceCalculation, bool EarlyDepartureCalculation, bool PayCutCalculation, bool ConsiderAttendance,bool NightOtEntitle, string CreatedUser, DateTime? EffectiveDate,
                             DateTime OTEntitlementEffectDate, DateTime EarlyOTEntitlementEffectDate, DateTime LievLeaveEntitlementEffectDate, DateTime LateAttendanceCalculationEffectDate, DateTime EarlyDepartureCalculationEffectDate,
                              DateTime? PayCutCalculationEffectDate, DateTime ConsiderAttendanceEffectDate, DateTime? NightOtEntitleEffectDate)
        {
            try
            {
                objEmpTimeSetup.UpdateEmployeeTimeSetup(hfEmloyeeId, CardNo, ImmediateSuperviser, DefaultShiftId, RosterGroupId, OTEntitlement, EarlyOTEntitlement,
                                LievLeaveEntitlement, LateAttendanceCalculation, EarlyDepartureCalculation, PayCutCalculation, ConsiderAttendance,NightOtEntitle, CreatedUser, EffectiveDate,
                                OTEntitlementEffectDate, EarlyOTEntitlementEffectDate, LievLeaveEntitlementEffectDate, LateAttendanceCalculationEffectDate, EarlyDepartureCalculationEffectDate,
                              PayCutCalculationEffectDate, ConsiderAttendanceEffectDate, NightOtEntitleEffectDate);
                // _shiftId = objShiftDAL.ShiftId;
                // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddLateAttendanceCalculation(long EmloyeeId, int LateSetupId, string CreatedUser)
        {
            try
            {
                objEmpTimeSetup.AddLateAttendanceCalculation(EmloyeeId, LateSetupId, CreatedUser);
                // _shiftId = objShiftDAL.ShiftId;
                // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteLateAttendanceCalculation(long EmloyeeId)
        {
            try
            {
                objEmpTimeSetup.DeleteLateAttendanceCalculation(EmloyeeId);
                // _shiftId = objShiftDAL.ShiftId;
                // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetLateAttendanceSetup(int CompanyId)
        {
            try
            {
                dtTable = objEmpTimeSetup.GetLateAttendanceSetup(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

    }
}
