
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HRM.Time.DAL;
using HRM.Common.BLL;

namespace HRM.Time.BLL
{
    /// <summary>
    ///  Posting [Main Processing/Unprocessing]
    /// </summary> 
    public class Posting
    {
        #region Fields
        private PostingDAL objPosting;
        private TimeOptionDAL objTimeOption;
        private EmployeeTimeDAL objEmployee;
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private int _dayTypeId = 0;
        private int _shiftId = 0;
        private long _employeeId = 0;
        string AttType;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <remarks>true is an Error, Otherwise false</remarks>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the Error No.
        /// </summary>
        /// <remarks>ErrorNo As int</remarks>
        public int ErrorNo
        {
            get { return _errorNo; }
        }

        /// <summary>
        /// Gets the Error Message.
        /// </summary>
        /// <remarks>ErrorMsg As string</remarks>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Posting"/> class.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <remarks></remarks>
        public Posting(int CompanyId)
        {
            objPosting = new PostingDAL();
            objTimeOption = new TimeOptionDAL();
            DataTable dtInOut = objTimeOption.GetTimeOption(CompanyId);

            if (dtInOut.Rows.Count > 0)
                objTimeOption.IsMonitorTimeInOutOnly =Convert.ToBoolean(dtInOut.Rows[0]["MonitorTimeInOutOnly"]);
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
                _isError = objPosting.IsError;
                _errorMsg = objPosting.ErrorMsg;
                _errorNo = objPosting.ErrorNo;
            }
        }

        public bool IsSetCalender(int CompanyId, DateTime CalendarDate)
        {
            bool isSet = false;
            try
            {
                isSet = objPosting.IsSetCalender(CompanyId, CalendarDate);
                if (isSet == false)
                {
                    _isError = true;
                    _errorMsg = "Calender not define!";
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return isSet;
        }

        #endregion

        #region External
        public void GetTimeAllTimeLogs(long EmployeeId, string CardDate, string time, DateTime CardDateTime, int AttendancesType, string Location)
        {
            try
            {
                objPosting.GetTimeAllTimeLogs(EmployeeId, CardDate, time, CardDateTime, AttendancesType, Location);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public DataTable GetDevicesList()
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetDevicesList();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        public DataTable GetDevicesIP(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetDevicesIP(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        /// <summary>
        /// Gets the Employee List.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <returns>Returns DataTable</returns>       
        public DataTable GetEmployeeList(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetEmployeeList(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        /// <summary>
        /// Gets the raw data list.
        /// </summary>
        /// <returns>Returns DataTable</returns>          
        public DataTable GetRawDataList(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetRawDataList(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        /// <summary>
        /// Datas the gathering.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <param name="CardNo">CardNo As string</param>
        /// <param name="EntryDate">EntryDate As string</param>
        /// <param name="EntryTime">EntryTime As string</param>
        /// <param name="FunctionKey">FunctionKey As string</param>
        /// <param name="UserName">UserName As string</param>     
        public void DataGathering(int CompanyId, string RowDataId, string CardNo, string EntryDate, string EntryTime, string FunctionKey, string AttendanceType, string UserName)
        {
            if (!IsSetCalender(CompanyId, DateTime.Today))
            {
                return;     //no need to process Calender is not upto date 
            }

            _employeeId = objPosting.GetEmployeeId(CardNo.Trim());

            if (_employeeId <= 0)
            {
                objPosting.AddUnknownCard(CardNo, Convert.ToDateTime(EntryDate), Convert.ToDateTime(EntryTime),Convert.ToInt64(RowDataId), FunctionKey);
            }
            else
            {
                DateTime punchDate = Convert.ToDateTime(EntryDate);
                DateTime punchtime = Convert.ToDateTime(EntryTime);
                long RowId = Convert.ToInt64(RowDataId);
                string time = punchtime.ToString("hh:mm tt");   

                
                if (AttendanceType == "1")
                {
                    AttType = "Clock-In";
                }
                if (AttendanceType == "2")
                {
                    AttType = "Clock-Out";
                }
                try
                {
                   
                    if (AttendanceType == "1" || AttendanceType == "2")
                    {
                        objPosting.AddEmployeeAttendance(_employeeId, RowId, punchDate, time, AttType, UserName);
                    }
                }
                catch
                {
                    return;// or break;
                }
            }
            //objPosting.AddEmployeeAttendanceHistory(_employeeId, CardNo, EntryDate, EntryTime, FunctionKey);// this is common to all 
        }

        public int GetDayTypeId(int CompanyId, DateTime Date)
        {
            int dayTypeId = 0;
            try
            {
                dayTypeId = objPosting.GetDayTypeId(CompanyId, Date);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dayTypeId;
        }

        public int GetShiftTypeId(int CompanyId, long EmployeeId, DateTime Date)
        {
            int shiftId = 0;
            try
            {
                shiftId = objPosting.GetShiftTypeId(CompanyId, EmployeeId, Date);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return shiftId;
        }

        public decimal GetEmployeeWorkDays(int DayTypeId, int ShiftId)
        {
            decimal workingDay = 0M;
            try
            {
                DataSet dsData = objPosting.GetShiftSetup(ShiftId, DayTypeId);
                if (dsData.Tables["ShiftSetup"].Rows.Count > 0)
                {
                    workingDay = Convert.ToDecimal(dsData.Tables["ShiftSetup"].Rows[0]["LeaveQtyWhenLeave"]);
                }
            }
            catch
            {
                _isError = true;
                _errorMsg = "Not Found on Employee for this Date!";
            }
            return workingDay;
        }

        public DataTable GetFirstPayPeriodId(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetFirstPayPeriodId(PayCategoryId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetPostTimeData(int PayCategoryId, bool IsPosted)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetPostTimeData(PayCategoryId, IsPosted);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeePay(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetEmployeePay(PayCategoryId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable CheckIsRowExist(long EmployeeId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.CheckIsRowExist(EmployeeId, PayPeriodId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }


        public void MonthEndProcess(long EmployeeId, int PayPeriodId, DateTime FromDate, DateTime ToDate, string EncryptionKey, int CompanyId)
        {
            try
            {
                objPosting.MonthEndProcess(EmployeeId, PayPeriodId, FromDate, ToDate, EncryptionKey, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UnpostTime(long EmployeeId, DateTime FromDate, DateTime ToDate, string CreatedUser)
        {
            try
            {
                objPosting.UnpostTime(EmployeeId, FromDate, ToDate, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UnPostEmployeeTxn(long EmployeeId, int PayPeriodId)
        {

            try
            {
                objPosting.UnPostEmployeeTxn(EmployeeId, PayPeriodId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }


        #region Posting Methods
        /// <summary>
        /// Posts the specified company id.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <param name="EmployeeId">EmployeeId As long</param>    
        public void Post(int CompanyId, long EmployeeId)
        {
            objEmployee = new EmployeeTimeDAL(EmployeeId, CompanyId);
            DataTable UnPostRecords = objPosting.GetAttendanceData(Convert.ToInt32(EmployeeId));
            for (int i = 0; i < UnPostRecords.Rows.Count; i++)
            {
                _dayTypeId = Convert.ToInt32(UnPostRecords.Rows[i]["DayTypeId"]);
                _shiftId = objPosting.GetShiftTypeId(CompanyId, _employeeId, Convert.ToDateTime(UnPostRecords.Rows[i]["Date"]));
                if (_dayTypeId > 0 && _shiftId > 0)
                {

                    DataSet dsShiftSetup = objPosting.GetShiftSetup(_shiftId, _dayTypeId);
                    if (dsShiftSetup.Tables.Count > 0)
                    {
                        for (int j = 0; j < dsShiftSetup.Tables[0].Rows.Count; j++)
                        {
                            try
                            {
                                long employeeTxn =Convert.ToInt64( UnPostRecords.Rows[i]["EmpTxnId"].ToString());

                                if (objEmployee.IsLateAttendanceCalculation && UnPostRecords.Rows[i]["TimeIn"].ToString().Length>0)
                                {
                                    objPosting.LateCalculation(employeeTxn, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LateGracePeriod"]), dsShiftSetup.Tables[0].Rows[j]["LateRoundMode"].ToString().Trim(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LateRounding"]), UnPostRecords.Rows[i]["Date"].ToString());
                                }

                                if (objEmployee.IsEarlyDepartureCalculation && UnPostRecords.Rows[i]["TimeOut"].ToString().Length > 0)
                                {
                                    objPosting.EarlyDepartureCalculation(employeeTxn, dsShiftSetup.Tables[0].Rows[j]["ShiftTimeOut"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["EarlyDepatrureGracePeriod"]), dsShiftSetup.Tables[0].Rows[j]["EarlyDepartureRoundMod"].ToString().Trim(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["EarlyDepartureRounding"]), (UnPostRecords.Rows[i]["Date"].ToString()));
                                }

                                if (objEmployee.IsPayCutCalculation  && UnPostRecords.Rows[i]["TimeIn"].ToString().Length>0 )
                                {
                                   // objPosting.PayCutCalculation(employeeTxn, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), dsShiftSetup.Tables[0].Rows[j]["ShiftTimeOut"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["PayCutGracePeriod"]), dsShiftSetup.Tables[0].Rows[j]["PayCutRoundMode"].ToString().Trim(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["PayCutRounding"]), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LeaveApplyDelayMints"]), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["LeaveQtyWhenDelay"]), (UnPostRecords.Rows[i]["Date"].ToString()));
                                }

                                if( UnPostRecords.Rows[i]["TimeIn"].ToString().Length>0 &&  UnPostRecords.Rows[i]["TimeOut"].ToString().Length>0)
                                  //  objPosting.LeaveCalculation(employeeTxn, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), dsShiftSetup.Tables[0].Rows[j]["ShiftTimeOut"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["ShiftHours"]), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["MinShiftHours"]), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["AppliedLeaveQty"]), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LeaveApplyDelayMints"]), (UnPostRecords.Rows[i]["Date"].ToString()));


                                for (int c = 0; c < dsShiftSetup.Tables[1].Rows.Count; c++)
                                {
                                    if (objEmployee.IsOTEntitlement)
                                    {
                                        if (objEmployee.IsEarlyOTEntitlement && UnPostRecords.Rows[i]["TimeIn"].ToString().Length>0)
                                        {
                                            objPosting.EarlyOTCalculation(employeeTxn, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTTypeId"]), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTGracePeriod"]), dsShiftSetup.Tables[1].Rows[c]["OTRoundMode"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTRounding"]), (UnPostRecords.Rows[i]["Date"].ToString()));
                                        }

                                        if (UnPostRecords.Rows[i]["TimeOut"].ToString().Length > 0)
                                            objPosting.PostOTCalculation(employeeTxn, dsShiftSetup.Tables[0].Rows[j]["ShiftTimeOut"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTTypeId"]), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTGracePeriod"]), dsShiftSetup.Tables[1].Rows[c]["OTRoundMode"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTRounding"]), (UnPostRecords.Rows[i]["Date"].ToString()));
                                        if (UnPostRecords.Rows[i]["TimeIn"].ToString().Length > 0 && UnPostRecords.Rows[i]["TimeOut"].ToString().Length > 0)
                                            objPosting.OTCalculation(employeeTxn, Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["ShiftHours"]), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["MinShiftHours"]), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTTypeId"]), UnPostRecords.Rows[i]["Date"].ToString());
                     
                                    }
                                }

                                if ((UnPostRecords.Rows[i]["TimeOut"].ToString().Length > 0) && (UnPostRecords.Rows[i]["TimeIn"].ToString().Length > 0))
                                    objPosting.PostWorkingHrs(employeeTxn, UnPostRecords.Rows[i]["Date"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), UnPostRecords.Rows[i]["LunchOut"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), _shiftId, (UnPostRecords.Rows[i]["Date"].ToString()));
                            }
                            catch { }


                        }

                    }
                }
            }
        }

        /// <summary>
        /// Repost Specified Company.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <param name="EmployeeId">EmployeeId As long</param> 
        /// <param name="FromDate">FromDate As DateTime</param>
        /// <param name="ToDate">ToDate As DateTime</param>       
        public void RePost(int CompanyId, long EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            if (!IsSetCalender(CompanyId, DateTime.Today))
            {
                return;     //no need to process Calender is not upto date 
            }

            DataTable UnPostRecords = objPosting.GetAttendanceData(CompanyId, FromDate, ToDate);
            for (int i = 0; i < UnPostRecords.Rows.Count; i++)
            {
                _dayTypeId = objPosting.GetDayTypeId(CompanyId, Convert.ToDateTime(UnPostRecords.Rows[i]["Date"]));
                _shiftId = objPosting.GetShiftTypeId(CompanyId, _employeeId, Convert.ToDateTime(UnPostRecords.Rows[i]["Date"]));
                if (_dayTypeId > 0 && _shiftId > 0)
                {
                    DataSet dsShiftSetup = objPosting.GetShiftSetup(_shiftId, _dayTypeId);
                    if (dsShiftSetup.Tables.Count > 0)
                    {
                        for (int j = 0; j < dsShiftSetup.Tables[0].Rows.Count; j++)
                        {
                            try
                            {
                                if (objEmployee.IsLateAttendanceCalculation)
                                {
                                    objPosting.LateCalculation(EmployeeId, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LateGracePeriod"]), dsShiftSetup.Tables[0].Rows[j]["LateRoundMode"].ToString().Trim(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LateRounding"]), (UnPostRecords.Rows[i]["Date"].ToString()));
                                }

                                if (objEmployee.IsEarlyDepartureCalculation)
                                {
                                    objPosting.EarlyDepartureCalculation(EmployeeId, dsShiftSetup.Tables[0].Rows[j]["ShiftOutTime"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["EarlyDepartureGracePeriod"]), dsShiftSetup.Tables[0].Rows[j]["EarlyDepartureRoundMode"].ToString().Trim(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["EarlyDepartureRounding"]), UnPostRecords.Rows[i]["Date"].ToString());
                                }

                                if (objEmployee.IsPayCutCalculation)
                                {
                                    objPosting.PayCutCalculation(EmployeeId, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), dsShiftSetup.Tables[0].Rows[j]["ShiftOutTime"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["PayCutGracePeriod"]), dsShiftSetup.Tables[0].Rows[j]["PayCutRoundMode"].ToString().Trim(), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["PayCutRounding"]), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LeaveApplyDelayMints"]), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["LeaveQtyWhenDelay"]), UnPostRecords.Rows[i]["Date"].ToString());
                                }

                                objPosting.LeaveCalculation(EmployeeId, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), dsShiftSetup.Tables[0].Rows[j]["ShiftOutTime"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), UnPostRecords.Rows[i]["TimeOut"].ToString(), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["ShiftHours"]), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["MinShiftHours"]), Convert.ToDecimal(dsShiftSetup.Tables[0].Rows[j]["AppliedLeaveQty"]), Convert.ToInt32(dsShiftSetup.Tables[0].Rows[j]["LeaveApplyDelayMints"]), UnPostRecords.Rows[i]["Date"].ToString());

                                for (int c = 0; c < dsShiftSetup.Tables[1].Rows.Count; c++)
                                {
                                    if (objEmployee.IsEarlyOTEntitlement)
                                    {
                                        objPosting.EarlyOTCalculation(EmployeeId, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTTypeId"]), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["EarlyOTGracePeriod"]), dsShiftSetup.Tables[1].Rows[c]["EarlyOTRoundMode"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["EarlyOTRounding"]), UnPostRecords.Rows[i]["Date"].ToString());
                                    }

                                    if (objEmployee.IsOTEntitlement)
                                    {
                                        objPosting.PostOTCalculation(EmployeeId, dsShiftSetup.Tables[0].Rows[j]["ShiftInTime"].ToString(), UnPostRecords.Rows[i]["TimeIn"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTTypeId"]), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTGracePeriod"]), dsShiftSetup.Tables[1].Rows[c]["OTRoundMode"].ToString(), Convert.ToInt32(dsShiftSetup.Tables[1].Rows[c]["OTRounding"]), UnPostRecords.Rows[i]["Date"].ToString());
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
        }
        #endregion

        #endregion
        #endregion

        #region Destructor
        ~Posting()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
