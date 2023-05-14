using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRM.Leave.DAL;
using HRM.Common.BLL;
using System.ComponentModel;
using System.Data;

namespace HRM.Leave.BLL
{
    [DataObject]
    public class Leave
    {
        #region Fields
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _errorNo = int.MinValue;
        private int _shiftId = 0;
        private DataTable _LeaveType;
        private int _leavetypeId;
        private int _dayTypeId = 0;
        private string _dayType = string.Empty;
        private string _shift = string.Empty;
        private decimal _appliedLeaveQty = 0;
        HRM.Leave.DAL.LeaveDAL objLeave;
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

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        public int ShiftId
        {
            get { return _shiftId; }
        }


        public int LeaveTypeId
        {
            get { return _leavetypeId; }
            set { _leavetypeId = value; }
        }


        public int DayTypeId
        {
            get { return _dayTypeId; }
        }

        public string DayType
        {
            get { return _dayType; }
        }

        public string Shift
        {
            get { return _shift; }
        }

        public decimal AppliedLeaveQty
        {
            get { return _appliedLeaveQty; }
        }

        #endregion

        #region Constructor
        public Leave()
        {
            objLeave = new LeaveDAL();
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
                _isError = objLeave.IsError;
                _errorMsg = objLeave.ErrorMsg;
                _errorNo = objLeave.ErrorNo;
                _shiftId = objLeave.ShiftId;
                _dayTypeId = objLeave.DayTypeId;
                _dayType = objLeave.DayType;
                _shift = objLeave.Shift;
                _appliedLeaveQty = objLeave.AppliedLeaveQty;
            }
        }
        #endregion

        #region External Methods
        #region LeaveType
        public void AddLeaveType(string LeaveCode, string LeaveDescription, bool AnnualEntitlement, bool MonthlyEntitlement, bool EarnType, bool NoPayType, bool BroughtFarwardType, bool ShortLeaveType)
        {
            try
            {
                objLeave.AddLeaveType(LeaveCode, LeaveDescription, AnnualEntitlement, MonthlyEntitlement, EarnType, NoPayType, BroughtFarwardType, ShortLeaveType);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateLeaveType(int LeaveTypeId, string LeaveDescription, bool AnnualEntitlement, bool MonthlyEntitlement, bool EarnType, bool NoPayType, bool BroughtFarwardType, bool ShortLeaveType)
        {
            try
            {
                objLeave.UpdateLeaveType(LeaveTypeId, LeaveDescription, AnnualEntitlement, MonthlyEntitlement, EarnType, NoPayType, BroughtFarwardType, ShortLeaveType);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetLeaveType(int LeaveTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetLeaveType(LeaveTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }


        public DataTable CheckLeaveTypeByCode(string LeaveTypeCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.CheckLeaveTypeByCode(LeaveTypeCode);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void DeleteLeaveType(int LeaveTypeId)
        {
            try
            {
                objLeave.DeleteLeaveType(LeaveTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region LeaveEntitlement
        public void AddLeaveEntitlement(int EmployeeId, int LeaveTypeId, decimal Entitlement, decimal Utilized, int Month, int Year)
        {
            try
            {
                objLeave.AddLeaveEntitlement(EmployeeId, LeaveTypeId, Entitlement, Utilized, Month, Year);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateLeaveEntitlement(int EntitlementId, int LeaveTypeId, decimal Entitlement, int Month, int Year, decimal Utilized)
        {
            try
            {
                objLeave.UpdateLeaveEntitlement(EntitlementId, LeaveTypeId, Entitlement, Month, Year, Utilized);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetLeaveEntitlement(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetLeaveEntitlement(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetOrganizationStructureEmployee(int CompanyId, int OrganizationId, int ApprovalTypeID, int ApprovalWorkFlowId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetOrganizationStructureEmployee(CompanyId, OrganizationId, ApprovalTypeID, ApprovalWorkFlowId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetApprovalGroups(int ApprovalTypeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetApprovalGroups(ApprovalTypeID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetInOrNotInAssignEmployees(int approvalTypeID, int companyId, int orgStrucId, string status)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetInOrNotInAssignEmployees(approvalTypeID, companyId, orgStrucId, status);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddCommonGroupsData(string ApprovalTypeId, string commonGroupName, string userName, long hfEmployeeId)
        {
            try
            {
                objLeave.AddCommonGroupsData(ApprovalTypeId, commonGroupName, userName, hfEmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateCommonGroupsData(long approvalTypeId, string iD, string commonGroupName, string userName, long hfEmloyeeId)
        {
            try
            {
                objLeave.UpdateCommonGroupsData(approvalTypeId, iD, commonGroupName, userName, hfEmloyeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetYears( int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetYears(CompanyId);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeeLeaveEntitlement(long EmployeeId, int OrgStructureID, int CompanyId, int Year, int JobCategoryID, int ActiveStatus)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetEmployeeLeaveEntitlement(EmployeeId, OrgStructureID, CompanyId, Year, JobCategoryID, ActiveStatus);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable CheckLeaveEntitlement(int Year, int Month, long EmployeeId, int LeaveTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.CheckLeaveEntitlement(Year, Month, EmployeeId, LeaveTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void DeleteLeaveEntitlement(int LeaveTypeId)
        {
            try
            {
                objLeave.DeleteLeaveEntitlement(LeaveTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetEmployeeOrg(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetEmployeeOrg(EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        public void GetShiftId(int CompnayId, long EmployeeId, DateTime Date,int org)
        {
            try
            {
                objLeave.GetShiftId(CompnayId, EmployeeId, Date, org);
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

        #region LeaveApplication
        public bool CheckCalender(int CompanyId, DateTime Date)
        {
            HRM.Time.BLL.Posting objPosting = new HRM.Time.BLL.Posting(CompanyId);
            try
            {
                if (!objPosting.IsSetCalender(CompanyId, DateTime.Today))
                {
                    return false;     //no need to process Calender is not upto date 
                }
            }
            catch
            {
                _isError = true;
                _errorMsg = "Company Calender Not Define!";
            }
            finally
            {
                if (objPosting != null)
                {
                    GC.SuppressFinalize(objPosting);
                }
            }
            return true;
        }

        private void GetShiftId(int CompanyId, long EmployeeId, DateTime Date)
        {
            HRM.Time.BLL.Posting objPosting = new HRM.Time.BLL.Posting(CompanyId);
            try
            {
                _shiftId = objPosting.GetShiftTypeId(CompanyId, EmployeeId, Date);
            }
            catch
            {
                _isError = true;
                _errorMsg = "Shift Not Found!";
            }
            finally
            {
                if (objPosting != null)
                {
                    GC.SuppressFinalize(objPosting);
                }
            }
        }



        public void GetDayTypeId(int CompanyId, DateTime Date)
        {
            HRM.Time.BLL.Posting objPosting = new HRM.Time.BLL.Posting(CompanyId);
            try
            {
                _dayTypeId = objPosting.GetDayTypeId(CompanyId, Date);
            }
            catch
            {
                _isError = true;
                _errorMsg = "Day Type Not Found!";
            }
            finally
            {
                if (objPosting != null)
                {
                    GC.SuppressFinalize(objPosting);
                }
            }
        }

        public decimal GetEmployeeWorkDays(int CompanyId, long EmployeeId, DateTime Date)
        {
            decimal workingDay = 0M;
            HRM.Time.BLL.Posting objPosting = new HRM.Time.BLL.Posting(CompanyId);
            try
            {
                GetDayTypeId(CompanyId, Date);
                GetShiftId(CompanyId, EmployeeId, Date);
                workingDay = objPosting.GetEmployeeWorkDays(_dayTypeId, ShiftId);
            }
            catch
            {
                _isError = true;
                _errorMsg = "Not Found on Employee for this Date!";
            }
            finally
            {
                if (objPosting != null)
                {
                    GC.SuppressFinalize(objPosting);
                }
            }
            return workingDay;
        }

        public bool GetEmployeeNormalWorkDays(int CompanyId, DateTime fromDate)
        {
            bool intNormalWorkDays = true;

            try
            {
                intNormalWorkDays = objLeave.GetEmployeeNormalWorkDays(CompanyId, fromDate);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return intNormalWorkDays;
        }
        public void GetLastNo(int CompanyId, string SetupName)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddLeaveHeader(long EmployeeId, string LeaveNo, DateTime FromDate, DateTime Todate, bool IsApproved, decimal TotalLeaveQty,
                                    string Status, long CoveringPerson, long ApprovedBy, decimal ApprovedTotalQty, string CreatedUser)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddLeaveHeaderNew(int CompanyId, long EmployeeId, int LeaveTypeId, DateTime FromDate, DateTime Todate, decimal TotalLeaveQty, long CoveringPerson, string CreatedUser, string ApplicationName, DataTable LeaveDetail, bool Active)
        {
            try
            {
                objLeave.AddLeaveHeader(CompanyId, EmployeeId, LeaveTypeId, FromDate, Todate, TotalLeaveQty, CoveringPerson, CreatedUser, ApplicationName, LeaveDetail, Active);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddLeaveDetails(DataTable dtLeaveDetails)
        {
            try
            {
                //objLeave.AddLeaveDetails(dtLeaveDetails);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddApprovedLeaveHeader(long LeaveId, long EmployeeId, decimal TotalApprovedQty, string ApproveStatus, string ReasonforReject, string ApprovedPerson)
        {
            try
            {
                objLeave.AddApprovedLeaveHeader(LeaveId, EmployeeId, TotalApprovedQty, ApproveStatus, ReasonforReject, ApprovedPerson);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateEmployeeLeaveEntitlement(long EmployeeId, int LeaveTypeId, DateTime fromDate, DateTime toDate, decimal Utilized, long LeaveNo)
        {
            try
            {
                objLeave.UpdateEmployeeLeaveEntitlement(EmployeeId, LeaveTypeId, fromDate, toDate, Utilized, LeaveNo);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateCancelledEmployeeEntitlements(long EmployeeId, int LeaveTypeId, DateTime fromDate, DateTime toDate, decimal Utilized, long leaveNo)
        {
            try
            {
                objLeave.UpdateCancelledEmployeeEntitlements(EmployeeId, LeaveTypeId, fromDate, toDate, Utilized, leaveNo);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddApprovedLeaveDetails(DataTable dtTableDetails)
        {
            try
            {
                objLeave.AddApprovedLeaveDetails(dtTableDetails);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion


        public void CancelLeave(int LeaveDetailId, int LeaveId, long EmployeeId,string CreateUser)
        {
            try
            {
                objLeave.CancelLeave(LeaveDetailId, LeaveId, EmployeeId,CreateUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeletePunchTimesFromRawData(string EmployeeCode, DateTime CalculateDate)
        {

            try
            {

                objLeave.DeletePunchTimesFromRawData(EmployeeCode, CalculateDate);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void ModifyAttendance(string EmployeeCode, int Day, int Month, int Year, string attType, string punchTime, string shiftId, string CreatedUser)
        {

            try
            {

                objLeave.ModifyAttendance(EmployeeCode, Day, Month, Year, attType, punchTime, shiftId, CreatedUser);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public DataTable GetEmployeeLeave(int CompanyId, long EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetEmployeeLeave(CompanyId, EmployeeId, FromDate, ToDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }


        public DataTable GetEmployeeLeaveByApproved(int CompanyId, long EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetEmployeeLeaveByApproved(CompanyId, EmployeeId, FromDate, ToDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }


        public DataTable GetEmployeeLeaveAll(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetEmployeeLeaveAll(EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeeLeaveAllForLevStatRep(int OrgStructureID, int CompanyId, int JobCategoryId, long EmployeeId, DateTime? FromDate, DateTime? ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetEmployeeLeaveAllForLevStatRep(OrgStructureID, CompanyId, JobCategoryId, EmployeeId, FromDate, ToDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPunchTimes(string EmployeeCode, DateTime CalculateDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetPunchTimes(EmployeeCode,CalculateDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPendingLeaves(int OrgStructureID, int CompanyId, int JobCategoryId, long EmployeeId, DateTime? FromDate, DateTime? ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetPendingLeaves(OrgStructureID, CompanyId, JobCategoryId, EmployeeId, FromDate, ToDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPostPayPeriod(long EmployeeId, DateTime LeaveDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetPostPayPeriod(EmployeeId, LeaveDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeeLeaveDetail(int CompanyId, long EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetEmployeeLeaveDetail(CompanyId, EmployeeId, FromDate, ToDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public DataTable GetLeaveApprovedHeader(long LeaveId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objLeave.GetLeaveApprovedHeader(LeaveId);
                //SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public void ApproveLeaveByHR(long LeaveId, string CreatedUser, int LeaveTypeId)
        {

            try
            {

                objLeave.ApproveLeaveByHR(LeaveId, CreatedUser, LeaveTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void RejectLeaveByHR(long LeaveId, string CreatedUser)
        {

            try
            {

                objLeave.RejectLeaveByHR(LeaveId, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        #endregion

        #endregion
    }
}
