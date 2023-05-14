/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, #51, Flower Road, Colombo 7.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Time.BLL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 02/dec/2011
/// Class Description   : Employee Time Card
/// Task Code List      : 
/// </summary>
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.ComponentModel;
using HRM.Time.DAL;
using HRM.Common.BLL;
using System.Globalization;

namespace HRM.Time.BLL
{
    [DataObject]
    public class TimeCard
    {
        #region Fields
        private TimeCardDAL objTimeCard;
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private DateTime _ShiftDate;
        private DateTime _ShiftTime;
        #endregion

        #region Properties
        public bool IsError
        {
            get { return _isError; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public DateTime ShiftDate
        {
            get { return _ShiftDate; }
        }

        public DateTime ShiftTime
        {
            get { return _ShiftTime; }
        }

        #endregion

        #region Constructor
        public TimeCard(int CompanyId)
        {
            objTimeCard = new TimeCardDAL();
        }

    
        #endregion

        #region Methods
        #region Internal     
        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = int.MinValue;           
        }

        private void SetValues()
        {
            if (!IsError)
            {
                _isError = objTimeCard.IsError;
                _errorMsg = objTimeCard.ErrorMsg;
                _errorNo = objTimeCard.ErrorNo;               
            }
        }
        #endregion

        #region External

        public void AttendancesReCalculation(int CompanyId, long EmployeeId, string EmployeeCode, DateTime FromDate, DateTime ToDate, string CreatedUser)
        {
            try
            {
                objTimeCard.AttendancesReCalculation(CompanyId, EmployeeId, EmployeeCode, FromDate, ToDate, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }
        public DataTable GetTimeCard(int CompanyId, DateTime FromDate, DateTime ToDate, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objTimeCard.GetTimeCard(CompanyId, FromDate, ToDate, EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }          
            return dtTable;
        }

        

        public DataTable GetAttendence(int CompanyId, DateTime Date, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objTimeCard.GetAttendence(CompanyId, Date, EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPendingAttendence(int CompanyId, DateTime Date, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objTimeCard.GetPendingAttendence(CompanyId, Date, EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

         public DataTable GetAttendenceApprovedData(int CompanyId, string Date, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objTimeCard.GetAttendenceApprovelData(CompanyId, Date, EmployeeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

         public DataTable GetAttendencesRequestApprovedStatus(int attnId)
         {
             DataTable dtTable = new DataTable();
             try
             {
                 dtTable = objTimeCard.GetAttendencesRequestApprovedStatus(attnId);
                 SetValues();
             }
             catch (Exception ex)
             {
                 _isError = true;
                 _errorMsg = ex.Message;
             }
             return dtTable;
         }

        public void InsertAttendenceForApprove(long EmployeeId, DateTime Date, DateTime? Intime1, DateTime? OutTime1, DateTime? Intime2, DateTime? OutTime2)
        {
            DataTable dtTable = new DataTable();
            try
            {
                objTimeCard.InsertAttendenceForApprove(EmployeeId,Date, Intime1, OutTime1, Intime2, OutTime2);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
           
        }

        public void GetShiftInShiftOutDetail(long EmployeeId, DateTime Date, string ShiftDetail)
        {
            try
            {
                objTimeCard.GetShiftInShiftOutDetail(EmployeeId, Date, ShiftDetail);
                _ShiftDate = objTimeCard.ShiftDate;
                _ShiftTime = objTimeCard.ShiftTime;
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



        public DataTable GetTimewithComments(int CompanyId, int OrgStuctureID, DateTime FromDate, DateTime ToDate, long EmployeeId, int AttendancesTypeId, int ShiftTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objTimeCard.GetTimewithComments(CompanyId, OrgStuctureID, FromDate, ToDate, EmployeeId, AttendancesTypeId, ShiftTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeesForTimeRepostingGrid(int CompanyId, int OrgStuctureID, long EmployeeId, int RosterGroupId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objTimeCard.GetEmployeesForTimeRepostingGrid(CompanyId, OrgStuctureID, EmployeeId, RosterGroupId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPunchTimesFromRawData(long EmployeeId, DateTime CalculateDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objTimeCard.GetPunchTimesFromRawData(EmployeeId, CalculateDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetTimeCard(int CompanyId, int OrgStructureId, DateTime FromDate, DateTime ToDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable=objTimeCard.GetTimeCard(CompanyId, OrgStructureId, FromDate, ToDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        
        public void UpdateTimeCard(DataTable TimeCard)
        {
            try
            {
                objTimeCard.UpdateTimeCard(TimeCard);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public DataTable GetChangedIndex(long EmpTxnId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objTimeCard.GetChangedIndex(EmpTxnId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }


        public void ModifyEmployeeAttendance(Decimal ApproveOT15, Decimal ApproveOT20, Decimal ApproveOT30, Decimal ApproveNightOT, Decimal ApproveLateHrs, Decimal ApproveEarlyHrs, Decimal ApproveEarlyOT, Decimal ApprovePostOT,
            bool IsApproveOT15Changed, bool IsApproveOT20Changed, bool IsApproveOT30Changed, bool IsApproveNightOTChanged, bool IsApproveLateHrsChanged, bool IsApproveEarlyHrsChanged,bool IsApproveEarlyOTPostOTChanged,
            long EmpTxnId, string UpdatedUser, string ChangedIndex)
        {
            
            try
            {

                objTimeCard.ModifyEmployeeAttendance(ApproveOT15, ApproveOT20, ApproveOT30, ApproveNightOT, ApproveLateHrs, ApproveEarlyHrs, ApproveEarlyOT, ApprovePostOT, IsApproveOT15Changed,
                IsApproveOT20Changed, IsApproveOT30Changed, IsApproveNightOTChanged, IsApproveLateHrsChanged, IsApproveEarlyHrsChanged,IsApproveEarlyOTPostOTChanged, 
                EmpTxnId, UpdatedUser, ChangedIndex);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void ModifyAttendance(string EmployeeCode, int Day, int Month, int Year, string attType, string punchTime, string shiftId, int modifiedDay, int modifiedMonth, int modifiedYear, string CreatedUser,int CompanyId)
        {

            try
            {

                objTimeCard.ModifyAttendance(EmployeeCode, Day, Month, Year, attType, punchTime, shiftId, modifiedDay, modifiedMonth, modifiedYear, CreatedUser,CompanyId);
                SetValues();
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

                objTimeCard.DeletePunchTimesFromRawData(EmployeeCode, CalculateDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void UpdateChangedIndex(string EmployeeCode, DateTime CalculateDate, DateTime? prvsIntime, DateTime? prvsOuttime)
        {

            try
            {

                objTimeCard.UpdateChangedIndex(EmployeeCode, CalculateDate, prvsIntime, prvsOuttime);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void TimeReposting(int CompanyId, long EmployeeId, string EmployeeCode, DateTime FromDate, DateTime ToDate, string CreatedUser)
        {
            try
            {
                objTimeCard.TimeReposting(CompanyId, EmployeeId, EmployeeCode, FromDate, ToDate, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public DataTable GetPrvsIntimeAndOuttime(string EmployeeCode, DateTime CalculateDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                return objTimeCard.GetPrvsIntimeAndOuttime(EmployeeCode, CalculateDate);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public DataTable GetApproveFlow(int  emp)
        {
            DataTable dtTable = new DataTable();
            try
            {
               return objTimeCard.getWorkFlow(emp);
               // SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public void UpdateAttendenceApproveStatus(int empID,string date,int loation,bool isLast, string Status)
        {
            try
            {
                objTimeCard.UpdateAttendencesAprovelStatus(empID, date, loation, isLast,Status);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public Int32 UpdateEmployeeTime(int _empId, DateTime? _date, DateTime? _inTime1,
     DateTime? _inTime2, DateTime? _outTime1, DateTime? _outTime2,
     int _modifyUser)
        {
            Int32 _employeeId = -1;
            try
            {
                double timevalue = 0.00;
                double lateHrs = 0.00;
                double earlyHrs = 0.00;
                double extraHrs = 0.00;
                double lessyHrs = 0.00;
                TimeSpan? totalHrs2=null;

                        if (_outTime2 == null && _inTime1 == null)
                        {

                            timevalue = 0.00;
                        }
                        else if (_outTime2 != null && _inTime1 != null)
                        {
                            DateTime? timeIN = _inTime1;

                            if (_inTime1 < DateTime.Parse("7:30"))
                            {
                                timeIN = DateTime.Parse("7:30");
                            }
                            TimeSpan totalHrs = (TimeSpan)(_outTime2 - timeIN);
                            totalHrs2 = totalHrs;
                            var mint = totalHrs.ToString();
                            var domain = mint.Split(':')[1];        
                            timevalue = Convert.ToDouble(totalHrs.Hours + "." + domain);

                            if ((_outTime1 != null && _inTime2 != null))
                            {
                                TimeSpan outTime = (TimeSpan)(_inTime2 - _outTime1);

                                var mintTotal = outTime.ToString();
                                var domainTotal = mintTotal.Split(':')[1];
                                TimeSpan workingHrs = (TimeSpan)(totalHrs - outTime);
                                totalHrs2 = workingHrs;

                                double WorkingHours = Convert.ToDouble(workingHrs.Hours + "." + workingHrs.Minutes);

                                timevalue = WorkingHours;
                               
                            }
                            
                        }

                        string timein = "";
                        string outTime2 = "";

                        if (_inTime1 != null)
                            timein = _inTime1.Value.ToShortTimeString();

                        if (_outTime2 != null)
                            outTime2 = _outTime2.Value.ToShortTimeString();


                        DateTime dt1 = DateTime.Parse("8:30");
                      
                       lateHrs = 0.00;

                        if (timein != "" && (dt1 < DateTime.Parse(timein)))
                        {
                            TimeSpan result = DateTime.Parse(timein) - dt1;
                            var mint = result.ToString();
                            var domain = mint.Split(':')[1];
                            lateHrs = Convert.ToDouble(result.Hours + "." + domain);

                        }

                        DateTime dt3 = DateTime.Parse("5:00 PM");
                      

                        earlyHrs = 0.00;

                        if (outTime2 != "" && (DateTime.Parse(outTime2) < dt3))
                        {
                            TimeSpan result = dt3 - DateTime.Parse(outTime2);
                            var mint = result.ToString();
                            var domain = mint.Split(':')[1];
                            earlyHrs = Convert.ToDouble(result.Hours + "." + domain);

                        }

                        if (timevalue > 8.30)
                        {
                            TimeSpan? result = TimeSpan.Parse("8:30");
                            result = totalHrs2 - TimeSpan.Parse("8:30");
                            var mint = result.ToString().Split(':')[1];
                            var hrs = result.ToString().Split(':')[0];
                            extraHrs = Convert.ToDouble(hrs + "." + mint);


                        }
                        else
                        {
                            lessyHrs = (8.30 - timevalue);
                            TimeSpan? result = TimeSpan.Parse("8:30");
                            if (timevalue != 0)
                            {
                                result = TimeSpan.Parse("8:30") - totalHrs2;
                            }

                            var mint = result.ToString().Split(':')[1];
                            var hrs = result.ToString().Split(':')[0];
                            lessyHrs = Convert.ToDouble(hrs + "." + mint);
                        }

                        objTimeCard.UpdateEmployeeTime(_empId, _date, _inTime1, _inTime2, _outTime1, _outTime2, lateHrs, earlyHrs, timevalue, extraHrs, lessyHrs, _modifyUser);
                        SetValues();


            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
           
            return _employeeId;
        }

        public void DeleteTimeCard(DataTable TimeCard)
        {
            try
            {
                objTimeCard.DeleteTimeCard(TimeCard);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }

        public void PostTimeCard(DataTable TimeCard, int CompanyId, int EmployeeId)
        {
            Posting objPosting = new Posting(CompanyId);
            try
            {
                for (int i = 0; i < TimeCard.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(TimeCard.Rows[i]["Posted"]) == false)
                    {
                        objPosting.Post(Convert.ToInt32(TimeCard.Rows[i]["CompanyId"]), Convert.ToInt64(TimeCard.Rows[i]["EmployeeId"]));
                        if (objPosting.IsError)
                        {
                            _isError = true;
                            _errorMsg = objPosting.ErrorMsg;
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
                if (objPosting != null)
                {
                    GC.SuppressFinalize(objPosting);
                }
            }
        }

        public void ShiftChange(DataTable TimeCard)
        {
            try
            {
                objTimeCard.ShiftChange(TimeCard);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void DayChange(DataTable TimeCard)
        {
            try
            {
                objTimeCard.DayChange(TimeCard);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void InOutChange(DataTable TimeCard)
        {
            try
            {
                objTimeCard.InOutChange(TimeCard);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }



        public void OTApproval(DataTable TimeCard)
        {
            try
            {
                objTimeCard.OTApproval(TimeCard);
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

        #region Destructor
        ~TimeCard()
        {
            GC.SuppressFinalize(this);            
        } 
        #endregion
    }
}
