using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRM.Leave.DAL;
using HRM.Common.BLL;
using System.Data;


namespace HRM.Leave.BLL
{
    public class MksLeave
    {
        #region Fields
        private string _result;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private long _EntitlementId = 0;
        LeaveDAL objLeaveDAL = new LeaveDAL();
        DataTable dtLeaveType;
        DataTable dtEmp;
        private int _shiftId = 0;
        private int _dayTypeId = 0;
        private string _dayType = string.Empty;
        private decimal _appliedLeaveQty = 0;
        private long _leaveId = 0;
        private string _lastNo = string.Empty;
        private decimal _casualDays = 0;
        private decimal _annualDays = 0;
        private decimal _medicalDays = 0;
        private DateTime? _returnDate = null;

        #endregion

        #region Properties

        public string Result
        {
            get { return _result; }
        }

        public bool IsError
        {
            get { return _isError; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        public long EntitlementId
        {
            get { return _EntitlementId; }
        }

        public decimal CasualDays
        {
            get { return _casualDays; }
        }

        public decimal AnnualDays
        {
            get { return _annualDays; }
        }

        public decimal MedicalDays
        {
            get { return _medicalDays; }
        }

        public int ShiftId
        {
            get { return _shiftId; }
        }

        public int DayTypeId
        {
            get { return _dayTypeId; }
        }

        public string DayType
        {
            get { return _dayType; }
        }

        public decimal AppliedLeaveQty
        {
            get { return _appliedLeaveQty; }
        }
        public long LeaveId
        {
            get { return _leaveId; }
        }
        public string LastNo
        {
            get { return _lastNo; }
        }
        public DateTime? ReturnToDate
        {
            get { return _returnDate; }
        }
        #endregion

        #region Constructor

        public MksLeave()
        {
            objLeaveDAL = new LeaveDAL();
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
                _result = objLeaveDAL.Result;
                _isError = objLeaveDAL.IsError;
                _errorMsg = objLeaveDAL.ErrorMsg;
                _shiftId = objLeaveDAL.ShiftId;
                _dayTypeId = objLeaveDAL.DayTypeId;
                _dayType = objLeaveDAL.DayType;
                _appliedLeaveQty = objLeaveDAL.AppliedLeaveQty;
                _leaveId = objLeaveDAL.LeaveId;
                _casualDays = objLeaveDAL.CasualDays;
                _annualDays = objLeaveDAL.AnnualDays;
                _medicalDays = objLeaveDAL.MedicalDays;
                //_lastNo = objLeaveDAL.LeaveNo;
            }
        }

        #endregion

         #region LeaveType

        public void AddLeaveType(string LeaveCode,string LeaveDescription,bool AnnualEntitlement,bool MonthlyEntitlement,bool EarnType,bool NoPayType,bool BroughtFarwardType,bool ShortLeaveType)
        {
            try
            {
                objLeaveDAL.AddLeaveType(LeaveCode,LeaveDescription,AnnualEntitlement,MonthlyEntitlement,EarnType,NoPayType,BroughtFarwardType,ShortLeaveType);
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable getLeaveTypeId(string LeaveType)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.getLeaveTypeId(LeaveType);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable GetMaternityLeaveToDate(DateTime FromDate, long EmployeeId, int MaternityLeaveTypeId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.GetMaternityLeaveToDate(FromDate, EmployeeId, MaternityLeaveTypeId);
                _returnDate = objLeaveDAL.ReturnToDate;
            }
            catch (Exception ex)
            {
                _errorMsg = string.Empty;
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();
            }
            return dt;
        }

        public DataTable SetMaternityLeaveQty(DateTime FromDate, DateTime ToDate, long EmployeeId, int MaternityLeaveTypeId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.SetMaternityLeaveQty(FromDate, ToDate, EmployeeId, MaternityLeaveTypeId);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable getEmployeeJobCategoryId(long EmployeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.getEmployeeJobCategoryId(EmployeeId);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable CheckLeaveLeave(long EmployeeId, DateTime FromDate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.CheckLeaveLeave(EmployeeId, FromDate);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public void UpdateLeaveType(int LeaveTypeId,string LeaveDescription,bool AnnualEntitlement,bool MonthlyEntitlement,bool EarnType,bool NoPayType,bool BroughtFarwardType,bool ShortLeaveType)
        {
            try
            {
                objLeaveDAL.UpdateLeaveType(LeaveTypeId,LeaveDescription,AnnualEntitlement,MonthlyEntitlement,EarnType,NoPayType,BroughtFarwardType,ShortLeaveType);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetLeaveType(int LeaveTypeId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.GetLeaveType(LeaveTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public DataTable CheckLeaveTypeByCode (string LeaveTypeCode)
        {
            try
            {
                dtLeaveType = objLeaveDAL.CheckLeaveTypeByCode(LeaveTypeCode);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public void DeleteLeaveType(int LeaveTypeId)
        {
            try
            {
                objLeaveDAL.DeleteLeaveType(LeaveTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetLeaveTypes(int LeaveTypeId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.GetLeaveTypes(LeaveTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        #endregion

        #region LeaveEntitlement

        public void AddLeaveEntitlement(int EmployeeId, int LeaveTypeId, decimal Entitlement, decimal Utilized, int Month, int Year)
        {
            try
            {
                objLeaveDAL.AddLeaveEntitlement(EmployeeId, LeaveTypeId, Entitlement,Utilized, Month, Year);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddLeaveEntitlement(int EmployeeId, int LeaveTypeId, decimal Entitlement, decimal Utilized, bool isLeaveCombined)
        {
            try
            {
                objLeaveDAL.AddLeaveEntitlement(EmployeeId, LeaveTypeId, Entitlement, Utilized, isLeaveCombined);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateLeaveEntitlement(int EmployeeId, int LeaveTypeId, decimal Entitlement, int Month, int Year, decimal Utilized)
        {
            try
            {
                objLeaveDAL.UpdateLeaveEntitlement( EmployeeId,  LeaveTypeId,  Entitlement,  Month,  Year,  Utilized);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateLeaveEntitlement(int EmployeeId, int LeaveTypeId, decimal Entitlement, decimal Utilized, bool isLeaveCombined)
        {
            try
            {
                objLeaveDAL.UpdateLeaveEntitlement(EmployeeId, LeaveTypeId, Entitlement, Utilized, isLeaveCombined);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void CalculateLeaveEntitlement(int EmployeeId)
        {
            try
            {
                objLeaveDAL.CalculateLeaveEntitlement(EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddLeaveWorkflow(int EmployeeId, System.Nullable<int> FirstApprovePerson, System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson)
        {
            try
            {
                objLeaveDAL.AddLeaveWorkflow(EmployeeId, FirstApprovePerson, SecondApprovePerson, ThirdApprovePerson);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void UpdateLeaveWorkflow(int EmployeeId, System.Nullable<int> FirstApprovePerson, System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson)
        {
            try
            {
                objLeaveDAL.UpdateLeaveWorkflow(EmployeeId, FirstApprovePerson, SecondApprovePerson, ThirdApprovePerson);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public void AddCommonWorkflow(string GroupCode, string GroupName, string UserName, int ApprovalTypeId, System.Nullable<int> FirstApprovePerson, System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson, int CompanyId, bool Active)
        {
            try
            {
                objLeaveDAL.AddCommonWorkflow(GroupCode, GroupName, UserName, ApprovalTypeId, FirstApprovePerson, SecondApprovePerson, ThirdApprovePerson, CompanyId, Active);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateCommonWorkflow(long CommonWorkflowId, string GroupCode, string GroupName, string UserName, int ApprovalTypeId, System.Nullable<int> FirstApprovePerson, System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson, bool Active)
        {
            try
            {
                objLeaveDAL.UpdateCommonWorkflow(CommonWorkflowId,GroupCode, GroupName, UserName, ApprovalTypeId, FirstApprovePerson, SecondApprovePerson, ThirdApprovePerson, Active);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddLeaveWorkflowGroup(int EmployeeId, System.Nullable<int> ApprovePerson, int ApproveLevel)
        {
            try
            {
                objLeaveDAL.AddLeaveWorkflowGroup(EmployeeId, ApprovePerson, ApproveLevel);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public DataTable GetLeaveEntitlement(int CompanyId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.GetLeaveEntitlement( CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public DataTable CheckLeaveEntitlement( int Year, int Month, long EmployeeId, int LeaveTypeId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.CheckLeaveEntitlement(Year,Month,EmployeeId,LeaveTypeId);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public DataTable CheckLeaveEntitlement(long EmployeeId, int LeaveTypeId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.CheckLeaveEntitlement(EmployeeId, LeaveTypeId);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public DataTable CheckAllLeaveEntitlement(long EmployeeId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.CheckAllLeaveEntitlement(EmployeeId);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public DataTable CheckLeaveEntitlementPreviousYear(long EmployeeId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.CheckLeaveEntitlementPreviousYear(EmployeeId);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public DataTable CheckLeaveWorkflow(int EmployeeId)
        {
            try
            {
                dtLeaveType = objLeaveDAL.CheckLeaveWorkflow(EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveType;
        }

        public DataTable GetEmployeeOrg(long EmployeeId)
        {
            try
            {
                dtEmp = objLeaveDAL.GetEmployeeOrg(EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtEmp;
        }

        #endregion

        #region LeaveSchedule

        public DataTable EmployeeWiseLeaveEntitlement(long EmployeeId, int Year, int Month)
        {
            try
            {
                dtEmp = objLeaveDAL.EmployeeWiseLeaveEntitlement(EmployeeId, Year, Month);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtEmp;
        }

        public DataTable EmployeeShiftSchedule(long EmployeeId, int Year, int Month)
        {
            try
            {
                dtEmp = objLeaveDAL.EmployeeWiseLeaveEntitlement(EmployeeId, Year, Month);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtEmp;
        }

        public DataTable AssignLeaveType()
        {
            try
            {
                dtEmp = objLeaveDAL.AssignLeaveType();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtEmp;
        }

        #endregion

        #region LeaveApplication
        public void GetShiftId(int CompnayId, long EmployeeId, DateTime Date)
        {
            try
            {
                objLeaveDAL.GetShiftId(CompnayId, EmployeeId, Date);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();
            }

        }


        public void GetLastNo(int CompanyId, string SetupName)
        {
            //using (DataTable dtTable = new DataTable())
            //{
            try
            {
                objLeaveDAL.GetLastNo(CompanyId, SetupName);
                this._lastNo = objLeaveDAL.LeaveNo;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();

            }
            // return dtTable;
        }

        public DataTable GetLevRequestDates(DateTime FromDate, DateTime ToDate, int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.GetLevRequestDates(FromDate, ToDate, CompanyId);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }


        public DataTable CheckShiftDate(DateTime Date, long EmployeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.CheckShiftDate(Date, EmployeeId);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable GetRemainingShortLeave(long EmployeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.GetRemainingShortLeave(EmployeeId);

                SetValues();

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable GetRemainingAnnualLeave(long EmployeeId, int Year, int Month , int LeaveTypeId, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.GetRemainingAnnualLeave(EmployeeId, Year, Month, LeaveTypeId, ToDate);

                SetValues();

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable checkLeaves(long EmployeeId, DateTime LeaveDate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.checkLeaves(EmployeeId, LeaveDate);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }


        public DataTable CheckDate(DateTime Date)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.CheckDate(Date);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable GetLeaveReason(int LeaveReasonId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.GetLeaveReason(LeaveReasonId);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public DataTable GetHalfDayShift(int ShiftId, int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objLeaveDAL.GetHalfDayShift(ShiftId, CompanyId);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dt;
        }

        public void AddLeaveHeaderNew(int CompanyId, long EmployeeId, int LeaveTypeId, DateTime FromDate, DateTime Todate, decimal TotalLeaveQty, long CoveringPerson, string CreatedUser, string ApplicationName, DataTable LeaveDetail, bool Active, string Session, int ReasonId)
        {
            try
            {
                objLeaveDAL.AddLeaveHeader(CompanyId, EmployeeId, LeaveTypeId, FromDate, Todate, TotalLeaveQty, CoveringPerson, CreatedUser, ApplicationName, LeaveDetail, Active, Session, ReasonId);
                _leaveId = objLeaveDAL.LeaveId;
            }
            catch (Exception ex)
            {
                _errorMsg = string.Empty;
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();
            }
        }

        public void UpdateLeaveCancelStatus(long EmployeeId, long LeaveId, string LeaveCancelStatus, string leaveType, string approvePerson)
        {
            try
            {
                objLeaveDAL.UpdateLeaveCancelStatus(EmployeeId, LeaveId, LeaveCancelStatus, leaveType, approvePerson);
            }
            catch (Exception ex)
            {
                _errorMsg = string.Empty;
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();
            }
        }

        public void AddLeaveHeader(long EmployeeId, string LeaveNo, DateTime FromDate, DateTime Todate, bool IsApproved, decimal TotalLeaveQty,
                                   string Status, long CoveringPerson, long ApprovedBy, decimal ApprovedTotalQty, string CreatedUser)
        {
            try
            {
               // objLeaveDAL.AddLeaveHeader(EmployeeId, LeaveNo, FromDate, Todate, IsApproved, TotalLeaveQty, Status, CoveringPerson, 
               //                             ApprovedBy, ApprovedTotalQty, CreatedUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();
            }
        }

        public void AddLeaveDetails(long LeaveId, int LeaveTypeId, DateTime LeaveDate, decimal LeaveQty, string session, int DayTypeId, int ShiftId, string DateName, string medicalNo, bool Aprroved, decimal ApprovedQty, long EmployeeId, int DayOffHalfShiftId, string CreatedUser)
        {
            try
            {
                objLeaveDAL.AddLeaveDetails(LeaveId, LeaveTypeId, LeaveDate, LeaveQty, session, DayTypeId, ShiftId, DateName, medicalNo, Aprroved, ApprovedQty, EmployeeId, DayOffHalfShiftId, CreatedUser);
                    _result = objLeaveDAL.Result;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();
            }
        }
        #endregion
        #endregion

        #region PendingLeaveCoveringApprove
        public long GetEmployeeLeaveCoveringPerson(long leaveId)
        {
            long empCoveringPerson=0;
            try
            {
                empCoveringPerson = objLeaveDAL.GetEmployeeLeaveCoveringPerson(leaveId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return empCoveringPerson;
        }


        public void UpdateLeaveCoveringPerApprove(string[] LeaveId)
        {
            try
            {
                objLeaveDAL.UpdateLeaveCoveringPerApprove(LeaveId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region PendingApprovePerson
        public void UpdateLeavePendingApprove(long LeaveId,string columnName, string levStatus)
        {
            try
            {
                objLeaveDAL.UpdateLeavePendingApprove(LeaveId, columnName, levStatus);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region PendingLeaveApprove
        public DataTable GetEmployeeLeaveApprovePerson(long leaveId)
        {
            DataTable dtLeaveStatus = new DataTable();
            try
            {
                dtLeaveStatus=objLeaveDAL.GetEmployeeLeaveApprovePerson(leaveId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveStatus;
        }

        public DataTable GetEmployeeLeaveApprovePersonAll()
        {
            DataTable dtLeaveStatus = new DataTable();
            try
            {
                dtLeaveStatus = objLeaveDAL.GetEmployeeLeaveApprovePersonAll();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveStatus;
        }

        public DataTable Lev_GetLeaveApprovedAll(long EmployeeId)
        {
            DataTable dtLeaveStatus = new DataTable();
            try
            {
                dtLeaveStatus = objLeaveDAL.Lev_GetLeaveApprovedAll(EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveStatus;
        }

        public DataTable Lev_GetLeaveApprovedAllForCurrentDate(long EmployeeId, DateTime currentDate)
        {
            DataTable dtLeaveStatus = new DataTable();
            try
            {
                dtLeaveStatus = objLeaveDAL.Lev_GetLeaveApprovedAllForCurrentDate(EmployeeId, currentDate);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveStatus;
        }

        public DataTable Lev_GetLeaveApprovedASCoveringPerson(long EmployeeId, DateTime currentDate)
        {
            DataTable dtLeaveStatus = new DataTable();
            try
            {
                dtLeaveStatus = objLeaveDAL.Lev_GetLeaveApprovedASCoveringPerson(EmployeeId, currentDate);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveStatus;
        }

        public DataTable Lev_GetLevAppAllForDateRange(long EmployeeId, DateTime currentDate)
        {
            DataTable dtLeaveStatus = new DataTable();
            try
            {
                dtLeaveStatus = objLeaveDAL.Lev_GetLevAppAllForDateRange(EmployeeId, currentDate);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveStatus;
        }

        public DataTable GetTobePermanentEmployee()
        {
            DataTable dtLeaveStatus = new DataTable();
            try
            {
                dtLeaveStatus = objLeaveDAL.GetTobePermanentEmployee();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtLeaveStatus;
        }
        
        #endregion
        #region Destructor

        ~MksLeave()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
