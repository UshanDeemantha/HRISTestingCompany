using System;
using System.ComponentModel;
using System.Data;
using HRM.Time.DAL;

namespace HRM.Time.BLL
{
    [DataObject]
    public class Roster
    {
        #region Fields

        private DataTable dtTable;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _dayTypeId = 0;
        RosterDAL objRoster;

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

        public Roster()
        {
            objRoster = new RosterDAL();
        }
        #endregion

        #region Methods

        #region Roster

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetRosterDetails(int companyId, int rosterGroupId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetRosterDetails(companyId, rosterGroupId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public DataTable GetCalenderByYear(int CompanyId, int Year, int Month)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetCalenderByYear(CompanyId, Year, Month);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetShiftsCreated()
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetShiftsCreated();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetRosterGroupsList(int CompanyId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetRosterGroupsList(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public DataTable GetDayTypeByDate(int CompanyId, DateTime Date)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetDayTypeByDate(CompanyId, Date);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetDefaultDayType()
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetDefaultDayType();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddRoster(int CompanyId, int RosterGroupId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, string DateName, DateTime RosterDate, int ShiftId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                objRoster.AddRoster(CompanyId, RosterGroupId, StartTime, EndTime, Day, Month, Year, DayTypeId, DateName, RosterDate, ShiftId, Posted, CreatedUser, Subject);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void DeleteRoster(DateTime RosterDate, int CompanyId)
        {
            try
            {
                objRoster.DeleteRoster(RosterDate, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable DeleteTimeRoster(int companyId, int rosterGroupId, string CreatedUser)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.DeleteTimeRoster(companyId, rosterGroupId, CreatedUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetShiftSetupDetails(int CompanyId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetShiftSetupDetails(CompanyId);
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
                dtTable = objRoster.GetDayTypeByDate(CompanyId, CalDate, Year, Month, date);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void InsertShiftSetup(string UserName)
        {
            try
            {
                objRoster.InsertShiftSetup(UserName);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region EmployeeShiftSchedule

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeShiftDetails(int CompanyId, Int64 EmployeeId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objRoster.GetEmployeeShiftDetails(CompanyId, EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddEmployeeShiftSchedule(int CompanyId, int EmployeeId, int OrgStructureID, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, string DateName, DateTime ShiftDate, int ShiftId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                objRoster.AddEmployeeShiftSchedule(CompanyId, EmployeeId, OrgStructureID, StartTime, EndTime, Day, Month, Year, DayTypeId, DateName, ShiftDate, ShiftId, Posted, CreatedUser, Subject);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void DeleteEmployeeShift(DateTime ShiftDate, int CompanyId)
        {
            try
            {
                objRoster.DeleteEmployeeShift(ShiftDate, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }
        public void DeleteEmployeeShift(DateTime ShiftDate, int CompanyId,long EmployeeId)
        {
            try
            {
                objRoster.DeleteEmployeeShift(ShiftDate, CompanyId,EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }
        #endregion

        #endregion

        #region Destructor

        ~Roster()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
