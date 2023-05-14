using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM.Leave.DAL
{ 
    public class LeaveDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _errorNo = int.MinValue;
        private long _EntitlementId = 0;
        private int _shiftId = 0;
        private int _dayTypeId = 0;
        private string _dayType = string.Empty;
        private string _shift = string.Empty;
        private decimal _appliedLeaveQty = 0;
        private string _result = string.Empty;
        private long _leaveId = 0;
        private long _leaveApprovedId = 0;
        private string _leaveNo = string.Empty;
        private decimal _casualDays=0;
        private decimal _annualDays = 0;
        private decimal _medicalDays = 0;
        private DateTime? _returnDate = null;
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

        public long LeaveTypeId
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

        public string Shift
        {
            get { return _shift; }
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

        public string Result
        {
            get { return _result; }
        }

        public long LeaveId
        {
            get { return _leaveId; }
        }

        public string LeaveNo
        {
            get { return _leaveNo; }
        }

        public long LeaveApprovedId
        {
            get { return _leaveApprovedId; }
        }

        public DateTime? ReturnToDate
        {
            get { return _returnDate; }
        }
        #endregion

        #region Constructor

        public LeaveDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods
        #region Internal Methods
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            InitializeFields();
        }

        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = int.MinValue;
        }

        private void SetError(SqlException Ex)
        {           
            _isError = true;
            _errorMsg = Ex.Message;            
            _errorNo = Ex.Number;
            switch (Ex.Number)
            {
                case 2601: _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 2627: _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 547: _errorMsg = "Can not Delete. Alredy Assign!";
                    break;
                default: break;
            }
        }
        #endregion
        
        #region External Methods
        #region LeaveType

        public void AddLeaveType(string LeaveCode, string LeaveDescription, bool AnnualEntitlement, bool MonthlyEntitlement, bool EarnType, bool NoPayType, bool BroughtFarwardType, bool ShortLeaveType)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LeaveCode", LeaveCode);
                    command.Parameters.AddWithValue("@LeaveDescription", LeaveDescription);
                    command.Parameters.AddWithValue("@AnnualEntitlement", AnnualEntitlement);
                    command.Parameters.AddWithValue("@MonthlyEntitlement", MonthlyEntitlement);
                    command.Parameters.AddWithValue("@EarnType", EarnType);
                    command.Parameters.AddWithValue("@NoPayType", NoPayType);
                    command.Parameters.AddWithValue("@BroughtFarwardType", BroughtFarwardType);
                    command.Parameters.AddWithValue("@ShortLeaveType", ShortLeaveType);

                    SqlParameter DayTypeId = new SqlParameter("@LeaveTypeId", SqlDbType.Int, 8);
                    DayTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    _EntitlementId = Convert.ToInt32(DayTypeId.Value);
                    if (_EntitlementId < 0)
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

        public DataTable getLeaveTypeId(string LeaveType)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("getLeaveTypeId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LeaveType", LeaveType);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public DataTable GetMaternityLeaveToDate(DateTime FromDate, long EmployeeId, int MaternityLeaveTypeId)
        {
            DataTable dt = new DataTable();
            OpenDB();

            try
            {
                using (SqlCommand command = new SqlCommand("Leave_GetMaternityDayCount", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@MaternityLeaveTypeId", MaternityLeaveTypeId);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }


            }
            catch (SqlException ex)
            {

                SetError(ex);
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
            return dt;
        }

        public DataTable SetMaternityLeaveQty(DateTime FromDate, DateTime ToDate, long EmployeeId, int MaternityLeaveTypeId)
        {
            DataTable dt = new DataTable();
            OpenDB();
            try
            {
                using (SqlCommand command = new SqlCommand("Leave_SetMaternityLeaveQty", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@SelectedToDate", ToDate);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@MaternityLeaveTypeId", MaternityLeaveTypeId);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
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
            return dt;
        }

        public DataTable getEmployeeJobCategoryId(long EmployeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("getEmployeeJobCategoryId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public DataTable CheckLeaveLeave(long EmployeeId, DateTime FromDate)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_CheckLeaveLeave", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public void AddLeaveHeader(int CompanyId, long EmployeeId, int LeaveTypeId, DateTime FromDate, DateTime Todate, decimal TotalLeaveQty, long CoveringPerson, string CreatedUser, string ApplicationName, DataTable LeaveDetail, bool Active, string Session, int ReasonId)
        {
            //SqlTransaction transaction;
            OpenDB();
            //transaction = _dbConnection.BeginTransaction();
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveHeader", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@Todate", Todate);
                    command.Parameters.AddWithValue("@TotalLeaveQty", TotalLeaveQty);
                    command.Parameters.AddWithValue("@CoveringPerson", CoveringPerson);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                    //command.Parameters.AddWithValue("@IsCoveringApproved", 'false');
                    command.Parameters.AddWithValue("@Active", Active);
                    command.Parameters.AddWithValue("@Session", Session);
                    command.Parameters.AddWithValue("@ReasonId", ReasonId);

                    SqlParameter leaveId = new SqlParameter("@LeaveId", SqlDbType.BigInt);
                    leaveId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(leaveId);

                    SqlParameter leaveNo = new SqlParameter("@LeaveNo", SqlDbType.NVarChar, 50);
                    leaveNo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(leaveNo);

                    command.ExecuteNonQuery();
                    _leaveId = Convert.ToInt64(leaveId.Value);
                    _leaveNo = leaveNo.Value.ToString();
                }

                //for (int i = 0; i < LeaveDetail.Rows.Count; i++)
                //{
                //    LeaveDetail.Rows[i]["LeaveId"] = _leaveId;
                //}

                //_dbConnection.Close();
                //AddLeaveDetails(LeaveDetail);
                //transaction.Commit();
            }
            catch (SqlException ex)
            {
                //transaction.Rollback();
                SetError(ex);
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void UpdateLeaveType(int LeaveTypeId, string LeaveDescription, bool AnnualEntitlement, bool MonthlyEntitlement, bool EarnType, bool NoPayType, bool BroughtFarwardType, bool ShortLeaveType)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateLeaveType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@LeaveDescription", LeaveDescription);
                    command.Parameters.AddWithValue("@AnnualEntitlement", AnnualEntitlement);
                    command.Parameters.AddWithValue("@MonthlyEntitlement", MonthlyEntitlement);
                    command.Parameters.AddWithValue("@EarnType", EarnType);
                    command.Parameters.AddWithValue("@NoPayType", NoPayType);
                    command.Parameters.AddWithValue("@BroughtFarwardType", BroughtFarwardType);
                    command.Parameters.AddWithValue("@ShortLeaveType", ShortLeaveType);

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
               
        public DataTable GetLeaveType(int LeaveTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetLeaveType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
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



        public DataTable CheckLeaveTypeByCode(string LeaveTypeCode)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_CheckLeaveType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@LeaveTypeCode", LeaveTypeCode);
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

        public void DeleteLeaveType(int LeaveTypeId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_DeleteLeaveType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
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

        public DataTable GetLeaveTypes(int LeaveTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetLeaveTypes", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
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

        #region LeaveEntitlement

        public void AddLeaveEntitlement(int EmployeeId, int LeaveTypeId, decimal Entitlement, decimal Utilized, int Month, int Year)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveEntitlement", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@Entitlement", Entitlement);
                    command.Parameters.AddWithValue("@Utilized", Utilized);
                    //command.Parameters.AddWithValue("@BroughtFarward", MonthlyEntitlement);
                    //command.Parameters.AddWithValue("@CarryFarward", EarnType);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);


                    SqlParameter EmtitlementId = new SqlParameter("@EntitlementId", SqlDbType.Int, 16);
                    EmtitlementId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(EmtitlementId);
                    command.ExecuteNonQuery();
                    _EntitlementId = Convert.ToInt64(EmtitlementId.Value);
                    if (_EntitlementId < 0)
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

        public void AddLeaveEntitlement(int EmployeeId, int LeaveTypeId, decimal Entitlement, decimal Utilized,bool isLeaveCombined)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveEntitlement", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@Entitlement", Entitlement);
                    command.Parameters.AddWithValue("@Utilized", Utilized);
                    command.Parameters.AddWithValue("@IsLeaveCombined", isLeaveCombined);
                    //command.Parameters.AddWithValue("@BroughtFarward", MonthlyEntitlement);
                    //command.Parameters.AddWithValue("@CarryFarward", EarnType);
                    command.Parameters.AddWithValue("@Month", System.DateTime.Now.Month);
                    command.Parameters.AddWithValue("@Year", System.DateTime.Now.Year);


                    SqlParameter EmtitlementId = new SqlParameter("@EntitlementId", SqlDbType.Int, 16);
                    EmtitlementId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(EmtitlementId);
                    command.ExecuteNonQuery();
                    _EntitlementId = Convert.ToInt64(EmtitlementId.Value);
                    if (_EntitlementId < 0)
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

        public void UpdateLeaveEntitlement(int EntitlementId, int LeaveTypeId, decimal Entitlement, int Month, int Year, decimal Utilized)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateLeaveEntitlement", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EntitlementId", EntitlementId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@Entitlement", Entitlement);
                    command.Parameters.AddWithValue("@Utilized", Utilized);
                    //command.Parameters.AddWithValue("@BroughtFarward", MonthlyEntitlement);
                    //command.Parameters.AddWithValue("@CarryFarward", EarnType);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);

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

        public void UpdateLeaveEntitlement(int EntitlementId, int LeaveTypeId, decimal Entitlement, decimal Utilized, bool isLeaveCombined)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateLeaveEntitlement", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EntitlementId", EntitlementId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@Entitlement", Entitlement);
                    command.Parameters.AddWithValue("@Utilized", Utilized);
                    command.Parameters.AddWithValue("@IsLeaveCombined", isLeaveCombined);
                    //command.Parameters.AddWithValue("@BroughtFarward", MonthlyEntitlement);
                    //command.Parameters.AddWithValue("@CarryFarward", EarnType);
                    //command.Parameters.AddWithValue("@Month", Month);
                    //command.Parameters.AddWithValue("@Year", Year);

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

        public void CalculateLeaveEntitlement(int EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_CalculateLeaveEntitlementsTemp", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    SqlParameter CasualDays = new SqlParameter("@casualDays", SqlDbType.Int, 16);
                    CasualDays.Direction = ParameterDirection.Output;
                    command.Parameters.Add(CasualDays);

                    SqlParameter AnnualDays = new SqlParameter("@annualDays", SqlDbType.Int, 16);
                    AnnualDays.Direction = ParameterDirection.Output;
                    command.Parameters.Add(AnnualDays);

                    SqlParameter MedicalDays = new SqlParameter("@medicalDays", SqlDbType.Int, 16);
                    MedicalDays.Direction = ParameterDirection.Output;
                    command.Parameters.Add(MedicalDays);
                    command.ExecuteNonQuery();
                    _casualDays = Convert.ToInt64(CasualDays.Value);
                    _annualDays = Convert.ToInt64(AnnualDays.Value);
                    _medicalDays = Convert.ToInt64(MedicalDays.Value);

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
        public void AddLeaveWorkflow(int EmployeeId, System.Nullable<int> FirstApprovePerson, System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveWorkflow", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FirstApprovePerson", FirstApprovePerson);
                    command.Parameters.AddWithValue("@SecondApprovePerson", SecondApprovePerson);
                    command.Parameters.AddWithValue("@ThirdApprovePerson", ThirdApprovePerson);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void UpdateLeaveWorkflow(int EmployeeId, System.Nullable<int> FirstApprovePerson, System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateLeaveWorkflow", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FirstApprovePerson", FirstApprovePerson);
                    command.Parameters.AddWithValue("@SecondApprovePerson", SecondApprovePerson);
                    command.Parameters.AddWithValue("@ThirdApprovePerson", ThirdApprovePerson);
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

        public void UpdateEmployeeLeaveEntitlement(long EmployeeId, int LeaveTypeId, DateTime fromDate, DateTime toDate, decimal Utilized, long LeaveNo)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateEmployeeEntitlements", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@Todate", toDate);
                    //command.Parameters.AddWithValue("@BroughtFarward", MonthlyEntitlement);
                    //command.Parameters.AddWithValue("@CarryFarward", EarnType);
                    command.Parameters.AddWithValue("@TotalLeaveQty", Utilized);
                    command.Parameters.AddWithValue("@LeaveId", LeaveNo);

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

        public void UpdateCancelledEmployeeEntitlements(long EmployeeId, int LeaveTypeId, DateTime fromDate, DateTime toDate, decimal Utilized, long leaveNo)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateCancelledEmployeeEntitlements", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@Todate", toDate);
                    //command.Parameters.AddWithValue("@BroughtFarward", MonthlyEntitlement);
                    //command.Parameters.AddWithValue("@CarryFarward", EarnType);
                    command.Parameters.AddWithValue("@TotalLeaveQty", Utilized);
                    command.Parameters.AddWithValue("@LeaveNo", leaveNo);

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

        public void AddCommonWorkflow(string GroupCode, string GroupName, string UserName, int ApprovalTypeId, System.Nullable<int> FirstApprovePerson,
            System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson, int CompanyId, bool Active)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddCommonWorkflow", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@GroupCode", GroupCode);
                    command.Parameters.AddWithValue("@GroupName", GroupName);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@ApprovalTypeID", ApprovalTypeId);

                    command.Parameters.AddWithValue("@FirstApprovePerson", FirstApprovePerson);
                    command.Parameters.AddWithValue("@SecondApprovePerson", SecondApprovePerson);
                    command.Parameters.AddWithValue("@ThirdApprovePerson", ThirdApprovePerson);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Active", Active);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void UpdateCommonWorkflow(long CommonWorkflowId, string GroupCode, string GroupName, string UserName, int ApprovalTypeId, System.Nullable<int> FirstApprovePerson, System.Nullable<int> SecondApprovePerson, System.Nullable<int> ThirdApprovePerson, bool Active)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateCommonWorkflow", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@Id", CommonWorkflowId);
                    command.Parameters.AddWithValue("@GroupCode", GroupCode);
                    command.Parameters.AddWithValue("@GroupName", GroupName);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@ApprovalTypeID", ApprovalTypeId);

                    command.Parameters.AddWithValue("@FirstApprovePerson", FirstApprovePerson);
                    command.Parameters.AddWithValue("@SecondApprovePerson", SecondApprovePerson);
                    command.Parameters.AddWithValue("@ThirdApprovePerson", ThirdApprovePerson);
                    command.Parameters.AddWithValue("@Active", Active);
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


        public void AddLeaveWorkflowGroup(int EmployeeId, System.Nullable<int> ApprovePerson, int ApproveLevel)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveWorkflowGroup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ApprovePerson", ApprovePerson);
                    command.Parameters.AddWithValue("@ApproveLevel", ApproveLevel);

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


        
        public DataTable GetLeaveEntitlement(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetLeaveEntitlements", _dbConnection))
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

        public DataTable GetEmployeeLeaveApprovePersonAll()
        {
            DataTable LeaveApprovedPerson = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveApprovePersonAll", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@LeaveId", leaveId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(LeaveApprovedPerson);
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
            return LeaveApprovedPerson;
        }

        public DataTable Lev_GetLeaveApprovedAll(long EmployeeId)
        {
            DataTable LeaveApprovedPerson = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveApprovedAll", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(LeaveApprovedPerson);
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
            return LeaveApprovedPerson;
        }

        public DataTable Lev_GetLeaveApprovedAllForCurrentDate(long EmployeeId,DateTime currentDate)
        {
            DataTable LeaveApprovedPerson = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveApprovedAllForCurrentDate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CurrentDate", currentDate);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(LeaveApprovedPerson);
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
            return LeaveApprovedPerson;
        }

        public DataTable Lev_GetLeaveApprovedASCoveringPerson(long EmployeeId, DateTime currentDate)
        {
            DataTable LeaveApprovedASCoveringPerson = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveAppliedASCoveringPerson", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CurrentDate", currentDate);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(LeaveApprovedASCoveringPerson);
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
            return LeaveApprovedASCoveringPerson;
        }

        public DataTable Lev_GetLevAppAllForDateRange(long EmployeeId, DateTime currentDate)
        {
            DataTable LeaveApprovedPerson = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLevAppAllForDateRange", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@currentDate", currentDate);
                    //command.Parameters.AddWithValue("@ToDate", toDate);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(LeaveApprovedPerson);
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
            return LeaveApprovedPerson;
        }

        public DataTable GetTobePermanentEmployee()
        {
            DataTable LeaveApprovedPerson = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetTobePermanentEmployee", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@LeaveId", leaveId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(LeaveApprovedPerson);
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
            return LeaveApprovedPerson;
        }


        public DataTable GetEmployeeLeaveApprovePerson(long leaveId)
        {
            DataTable LeaveApprovedPerson = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveApprovePerson", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LeaveId", leaveId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(LeaveApprovedPerson);
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
            return LeaveApprovedPerson;
        }


        public DataTable GetOrganizationStructureEmployee(int CompanyId, int OrganizationId, int ApprovalTypeID, int ApprovalWorkFlowId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_GetOrganizationStructureEmployee", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@OrganizationId", OrganizationId);
                        command.Parameters.AddWithValue("@ApprovalTypeID", ApprovalTypeID);
                        command.Parameters.AddWithValue("@ApprovalWorkFlowId", ApprovalWorkFlowId);
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

        public DataTable GetApprovalGroups(int ApprovalTypeID)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_GetApprovalGroups", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ApprovalTypeID", ApprovalTypeID);
                        
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

        public DataTable GetInOrNotInAssignEmployees(int approvalTypeID, int companyId, int orgStrucId, string status)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_GetInOrNotInAssignEmployees", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@ApprovalTypeID", approvalTypeID);
                        command.Parameters.AddWithValue("@CompanyId", companyId);
                        command.Parameters.AddWithValue("@OrgStrucId", orgStrucId);
                        command.Parameters.AddWithValue("@Status", status);
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

        public void AddCommonGroupsData(string ApprovalTypeId, string commonGroupName, string userName, long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Com_AddCommonGroupsData", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ApprovalTypeId", ApprovalTypeId);
                    command.Parameters.AddWithValue("@ApprovalWorkFlowId", commonGroupName);
                    //command.Parameters.AddWithValue("@EmployeeId", employeeCode);
                    command.Parameters.AddWithValue("@userName", userName);


                    SqlParameter EmtitlementId = new SqlParameter("@EntitlementId", SqlDbType.Int, 16);
                    EmtitlementId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(EmtitlementId);
                    command.ExecuteNonQuery();
                    _EntitlementId = Convert.ToInt64(EmtitlementId.Value);
                    if (_EntitlementId <= 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Update or Insert Error !";
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

        public DataTable GetYears(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Com_Years", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.ExecuteNonQuery();
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


        public void UpdateCommonGroupsData(long CommenAssignId, string radcbApprovalType, string commonGroupName, string ModifiedName, long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Com_UpdateCommonGroupsData", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CommenAssignId", CommenAssignId);
                    command.Parameters.AddWithValue("@ApprovalWorkFlowId", commonGroupName);
                    command.Parameters.AddWithValue("@ApprovalType", radcbApprovalType);
                    command.Parameters.AddWithValue("@ModifiedUser", ModifiedName);
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


        public DataTable GetEmployeeLeaveEntitlement(long EmployeeId,int OrgStructureID, int CompanyId, int Year, int JobCategoryID, int ActiveStatus)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetEmployeeLeaveEntitlement", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Year", Year);
                        command.Parameters.AddWithValue("@JobCategoryID", JobCategoryID);
                        command.Parameters.AddWithValue("@ActiveStatus", ActiveStatus);
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

        public DataTable CheckLeaveEntitlement(int Year, int Month, long EmployeeId, int LeaveTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_CheckLeaveEntitlements", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@Year", Year);
                        command.Parameters.AddWithValue("@Month", Month);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);

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

        public DataTable CheckLeaveEntitlement(long EmployeeId, int LeaveTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_CheckLeaveEntitlements", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        //command.Parameters.AddWithValue("@Year", Year);
                        //command.Parameters.AddWithValue("@Month", Month);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);

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

        public DataTable CheckAllLeaveEntitlement(long EmployeeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_CheckAllLeaveEntitlements", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        //command.Parameters.AddWithValue("@Year", Year);
                        //command.Parameters.AddWithValue("@Month", Month);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        //command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);

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

        public DataTable CheckLeaveEntitlementPreviousYear(long EmployeeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_CheckPreviousYearLeaveEntitlements", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        //command.Parameters.AddWithValue("@Year", Year);
                        //command.Parameters.AddWithValue("@Month", Month);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        //command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);

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

        public DataTable CheckLeaveWorkflow(int EmployeeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_CheckLeaveWorkflow", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);

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


        public void DeleteLeaveEntitlement(int LeaveTypeId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_DeleteLeaveType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
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


        public DataTable GetEmployeeOrg(long EmployeeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetEmployeeOrg", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        #region LeaveApplication
        public void GetShiftId(int CompanyId, long EmployeeId, DateTime Date)
        {
            //using (DataTable dtTable = new DataTable())
            //{
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_GetShiftId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Date", Date);

                    SqlParameter ShiftId = new SqlParameter("@ShiftId", SqlDbType.Int, 8);
                    ShiftId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ShiftId);

                    SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    DayTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(DayTypeId);

                    SqlParameter DayType = new SqlParameter("@DayType", SqlDbType.NVarChar, 50);
                    DayType.Direction = ParameterDirection.Output;
                    command.Parameters.Add(DayType);

                    SqlParameter shift = new SqlParameter("@Shift", SqlDbType.NVarChar, 50);
                    shift.Direction = ParameterDirection.Output;
                    command.Parameters.Add(shift);

                    SqlParameter AppliedLeaveQty = new SqlParameter("@AppliedLeaveQty", SqlDbType.Decimal, 18);
                    AppliedLeaveQty.Direction = ParameterDirection.Output;
                    command.Parameters.Add(AppliedLeaveQty);

                    command.ExecuteNonQuery();

                    _shiftId = Convert.ToInt32(ShiftId.Value);
                    _shift = shift.Value.ToString();
                    if (!String.IsNullOrEmpty(DayTypeId.Value.ToString()))
                    {
                        _dayTypeId = Convert.ToInt32(DayTypeId.Value);
                    }
                    else
                    {
                        _dayTypeId = 0;
                    }
                    _dayType = Convert.ToString(DayType.Value);
                    if (!String.IsNullOrEmpty(AppliedLeaveQty.Value.ToString()))
                    {
                        _appliedLeaveQty = Convert.ToDecimal(AppliedLeaveQty.Value);
                    }
                    else
                    {
                        _appliedLeaveQty = 0;
                    }

                    //using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    //{
                    //    daAdapter.Fill(dtTable);
                    //}
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
            // return dtTable;
        }
        public void GetShiftId(int CompanyId, long EmployeeId, DateTime Date,int org)
        {
            //using (DataTable dtTable = new DataTable())
            //{
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_GetShiftId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Date", Date);

                    SqlParameter ShiftId = new SqlParameter("@ShiftId", SqlDbType.Int, 8);
                    ShiftId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ShiftId);

                    SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    DayTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(DayTypeId);

                    SqlParameter DayType = new SqlParameter("@DayType", SqlDbType.NVarChar, 50);
                    DayType.Direction = ParameterDirection.Output;
                    command.Parameters.Add(DayType);

                    SqlParameter shift = new SqlParameter("@Shift", SqlDbType.NVarChar, 50);
                    shift.Direction = ParameterDirection.Output;
                    command.Parameters.Add(shift);

                    SqlParameter AppliedLeaveQty = new SqlParameter("@AppliedLeaveQty", SqlDbType.Decimal, 18);
                    AppliedLeaveQty.Direction = ParameterDirection.Output;
                    command.Parameters.Add(AppliedLeaveQty);

                    command.ExecuteNonQuery();

                    _shiftId = Convert.ToInt32(ShiftId.Value);
                    _shift = shift.Value.ToString();
                    if (!String.IsNullOrEmpty(DayTypeId.Value.ToString()))
                    {
                        _dayTypeId = Convert.ToInt32(DayTypeId.Value);
                    }
                    else
                    {
                        _dayTypeId = 0;
                    }
                    _dayType = Convert.ToString(DayType.Value);
                    if (!String.IsNullOrEmpty(AppliedLeaveQty.Value.ToString()))
                    {
                        _appliedLeaveQty = Convert.ToDecimal(AppliedLeaveQty.Value);
                    }
                    else
                    {
                        _appliedLeaveQty = 0;
                    }

                    //using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    //{
                    //    daAdapter.Fill(dtTable);
                    //}
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
            // return dtTable;
        }
        
        public void GetLastNo(int CompanyId, string SetupName)
        {
            //using (DataTable dtTable = new DataTable())
            //{
            try
            {
                using (SqlCommand command = new SqlCommand("GetLastNo", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@SetupName", @SetupName);


                    SqlParameter LastNo = new SqlParameter("@LastNo", SqlDbType.NVarChar, 50);
                    LastNo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(LastNo);

                    command.ExecuteNonQuery();

                    _leaveNo = Convert.ToString(LastNo.Value);

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
            // return dtTable;
        }

        public bool GetEmployeeNormalWorkDays(int CompanyId, DateTime fromDate)
        {
            bool _isNormalDay = true;
            OpenDB();
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_GetEmpNormalWorkDays", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@CurrentDate", fromDate);
                    //command.Parameters.AddWithValue("@Todate", toDate);

                    SqlParameter isNormalDay = new SqlParameter("@IsNormalDay", SqlDbType.Bit);
                    isNormalDay.Direction = ParameterDirection.Output;
                    command.Parameters.Add(isNormalDay);

                    command.ExecuteNonQuery();
                    _isNormalDay = (bool)isNormalDay.Value;

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
            return _isNormalDay;
        }

        public DataTable GetLevRequestDates(DateTime FromDate, DateTime ToDate, int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveRequestDates_", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }


        public DataTable CheckShiftDate(DateTime Date, long EmployeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_CheckShiftDate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }


        public DataTable GetRemainingShortLeave(long EmployeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetRemainingShortLeave", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public DataTable GetRemainingAnnualLeave(long EmployeeId,int Year,int Month, int LeaveTypeId, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetRemainingAnnualLeave", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public DataTable checkLeaves(long EmployeeId, DateTime LeaveDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_checkLeaves", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@LeaveDate", LeaveDate);
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


        public DataTable CheckDate(DateTime Date)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_CheckDate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Date", Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public DataTable GetLeaveReason(int LeaveReasonId)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveReason", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LeaveReasonId", LeaveReasonId);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public DataTable GetHalfDayShift(int ShiftId, int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Lev_GetHalfDayShift", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
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
            return dt;
        }

        public void AddLeaveHeader(int CompanyId, long EmployeeId, int LeaveTypeId, DateTime FromDate, DateTime Todate, decimal TotalLeaveQty, long CoveringPerson, string CreatedUser, string ApplicationName, DataTable LeaveDetail, bool Active)
        {
            //SqlTransaction transaction;
            OpenDB();
            //transaction = _dbConnection.BeginTransaction();
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveHeader", _dbConnection))
                {                   
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@Todate", Todate);
                    command.Parameters.AddWithValue("@TotalLeaveQty", TotalLeaveQty);
                    command.Parameters.AddWithValue("@CoveringPerson", CoveringPerson);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                    //command.Parameters.AddWithValue("@IsCoveringApproved", 'false');
                    command.Parameters.AddWithValue("@Active", Active);

                    SqlParameter leaveId = new SqlParameter("@LeaveId", SqlDbType.BigInt);
                    leaveId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(leaveId);

                    SqlParameter leaveNo = new SqlParameter("@LeaveNo", SqlDbType.NVarChar, 50);
                    leaveNo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(leaveNo);

                    command.ExecuteNonQuery();
                    _leaveId = Convert.ToInt64(leaveId.Value);
                    _leaveNo = leaveNo.Value.ToString();
                }

                //for (int i = 0; i < LeaveDetail.Rows.Count; i++)
                //{
                //    LeaveDetail.Rows[i]["LeaveId"] = _leaveId;
                //}

                //_dbConnection.Close();
                //AddLeaveDetails(LeaveDetail);
                //transaction.Commit();
            }
            catch (SqlException ex)
            {
                //transaction.Rollback();
                SetError(ex);
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void UpdateLeaveCancelStatus(long EmployeeId, long LeaveId, string LeaveCancelStatus, string leaveType, string approvePerson)
        {
            //SqlTransaction transaction;
            OpenDB();
            //transaction = _dbConnection.BeginTransaction();
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateLeaveCancelStatus", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LeaveNo", LeaveId);
                    command.Parameters.AddWithValue("@Status", LeaveCancelStatus);
                    command.Parameters.AddWithValue("@ApproveType",leaveType);
                    command.Parameters.AddWithValue("@ApprovePerson", approvePerson);
                    
                    command.ExecuteNonQuery();
                }

                //for (int i = 0; i < LeaveDetail.Rows.Count; i++)
                //{
                //    LeaveDetail.Rows[i]["LeaveId"] = _leaveId;
                //}

                //_dbConnection.Close();
                //AddLeaveDetails(LeaveDetail);
                //transaction.Commit();
            }
            catch (SqlException ex)
            {
                //transaction.Rollback();
                SetError(ex);
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void AddLeaveDetails(long LeaveId, int LeaveTypeId, DateTime LeaveDate, decimal LeaveQty, string session, int DayTypeId, int ShiftId, string DateName, string medicalNo, 
            bool Aprroved, decimal ApprovedQty, long EmployeeId, int DayOffHalfShiftId, string CreatedUser)
        {
            OpenDB();
           string employeeCode = "";
            string leaveTypeCode = "";
            DataTable dtDays = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddLeaveDetails", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@LeaveId", LeaveId);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                    command.Parameters.AddWithValue("@LeaveDate", LeaveDate);
                    command.Parameters.AddWithValue("@LeaveQty", LeaveQty);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@ShiftId", ShiftId);
                    command.Parameters.AddWithValue("@DateName", DateName);
                    command.Parameters.AddWithValue("@MedicalNo", medicalNo);
                    command.Parameters.AddWithValue("@Session", session);
                    command.Parameters.AddWithValue("@Approved", Aprroved);
                    command.Parameters.AddWithValue("@ApprovedQty", ApprovedQty);
                    command.Parameters.AddWithValue("@DayOffHalfShiftId", DayOffHalfShiftId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    SqlParameter Result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    Result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(Result);

                    SqlParameter EmployeeCode = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar, 50);
                    EmployeeCode.Direction = ParameterDirection.Output;
                    command.Parameters.Add(EmployeeCode);

                    SqlParameter LeaveTypeCode = new SqlParameter("@LeaveTypeCode", SqlDbType.NVarChar, 50);
                    LeaveTypeCode.Direction = ParameterDirection.Output;
                    command.Parameters.Add(LeaveTypeCode);

                    command.ExecuteNonQuery();
                        
                    _result = Convert.ToString(Result.Value);
                    employeeCode = Convert.ToString(EmployeeCode.Value);
                    leaveTypeCode = Convert.ToString(LeaveTypeCode.Value);
                }

                if(leaveTypeCode == "DAYOFFFULL" || leaveTypeCode == "DAYOFFHALF")
                {
                    using (SqlCommand command = new SqlCommand("TIME_GetTimeRawDataDetails", _dbConnection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@RosterDate", LeaveDate);
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtDays);
                        }
                    }

                    using (SqlCommand command = new SqlCommand("TIME_DeletePunchTimesFromRawData", _dbConnection))
                    {
                        command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                        command.Parameters.AddWithValue("@CalculateDate", LeaveDate);
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                    }

                    foreach (DataRow times in dtDays.Rows)
                    {
                        using (SqlCommand command = new SqlCommand("Time_ModifyAttendance", _dbConnection))
                        {
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            command.Parameters.AddWithValue("@Day", Convert.ToDateTime(times["PunchDateTime"].ToString()).Day);
                            command.Parameters.AddWithValue("@Month", Convert.ToDateTime(times["PunchDateTime"].ToString()).Month);
                            command.Parameters.AddWithValue("@Year", Convert.ToDateTime(times["PunchDateTime"].ToString()).Year);
                            command.Parameters.AddWithValue("@punchTime", times["CardTime"].ToString());
                            command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                            command.Parameters.AddWithValue("@attType", "");
                            command.Parameters.AddWithValue("@ShiftId", "");
                            command.Parameters.AddWithValue("@RelevantDay", LeaveDate.Day);
                            command.Parameters.AddWithValue("@RelevantMonth", LeaveDate.Month);
                            command.Parameters.AddWithValue("@RelevantYear", LeaveDate.Year);
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
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
                _dbConnection.Close();
            }
        }
        #endregion

        #region LeaveSchedule

        public DataTable EmployeeWiseLeaveEntitlement(long EmployeeId, int Year, int Month)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_EmployeeEntitlement", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@Year", Year);
                        command.Parameters.AddWithValue("@Month", Month);
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

        public DataTable EmployeeShiftSchedule(long EmployeeId, int Year, int Month)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_EmployeeShiftSchedule", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@Year", Year);
                        command.Parameters.AddWithValue("@Month", Month);
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

        public DataTable AssignLeaveType()
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_AssignLeaveType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
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

        #endregion 

        public void CancelLeave(int LeaveDetailId, int LeaveId, long EmployeeId,string CreateUser)
        {

            OpenDB();

            try
            {
                using (SqlCommand command = new SqlCommand("Lev_CancelLeave", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LeaveDetailId", LeaveDetailId);
                    command.Parameters.AddWithValue("@LeaveId", LeaveId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CreateUser", CreateUser);
                    command.Parameters.AddWithValue("@CancelledByHr", true);

                    command.ExecuteNonQuery();
                }


            }
            catch (SqlException ex)
            {

                SetError(ex);
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

        public void DeletePunchTimesFromRawData(string EmployeeCode, DateTime CalculateDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("TIME_DeletePunchTimesFromRawData", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@CalculateDate", CalculateDate);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public void ModifyAttendance(string EmployeeCode, int Day, int Month, int Year, string attType, string punchTime, string shiftId, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_ModifyAttendance", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@attType", attType);
                    command.Parameters.AddWithValue("@punchTime", punchTime);
                    command.Parameters.AddWithValue("@ShiftId", shiftId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
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

        public DataTable GetEmployeeLeave(int CompanyId, long EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetEmployeeLeave", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@FromDate", FromDate);
                        command.Parameters.AddWithValue("@ToDate", ToDate);
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


        public DataTable GetEmployeeLeaveByApproved(int CompanyId, long EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetEmployeeLeaveByApproved", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@FromDate", FromDate);
                        command.Parameters.AddWithValue("@ToDate", ToDate);
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

        public DataTable GetEmployeeLeaveAll(long EmployeeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetEmployeeLeaveALL", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        public DataTable GetEmployeeLeaveAllForLevStatRep(int OrgStructureID, int CompanyId, int JobCategoryId, long EmployeeId, DateTime? FromDate, DateTime? ToDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetEmployeeLeaveHistoryALL", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@JobCategoryId", JobCategoryId);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@FromDate", FromDate);
                        command.Parameters.AddWithValue("@ToDate", ToDate);
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

        public DataTable GetPunchTimes(string EmployeeCode,DateTime CalculateDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetPunchTimes", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                        command.Parameters.AddWithValue("@CalculateDate", CalculateDate);
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

        public DataTable GetPendingLeaves(int OrgStructureID, int CompanyId, int JobCategoryId, long EmployeeId, DateTime? FromDate, DateTime? ToDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetPendingLeaves", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@JobCategoryId", JobCategoryId);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@FromDate", FromDate);
                        command.Parameters.AddWithValue("@ToDate", ToDate);
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

        public DataTable GetPostPayPeriod(long EmployeeId, DateTime LeaveDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetPostPayPeriod", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@LeaveDate", LeaveDate);
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


        public DataTable GetEmployeeLeaveDetail(int CompanyId, long EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetEmployeeLeaveDetail", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@FromDate", FromDate);
                        command.Parameters.AddWithValue("@ToDate", ToDate);
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

        public DataTable GetAllEmployeeLeaveByApproval(int CompanyId, long EmployeeId, bool IsValid)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetAllEmployeeLeaveByApproval", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@IsApproved", IsValid);
                        
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

        public DataTable GetAllEmployeeLeaveByApprovalDetails(long LeaveId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetAllEmployeeLeaveByApprovalDetails", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LeaveId", LeaveId);

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

        public DataTable GetLeaveApprovedHeader(long LeaveId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetLeaveApprovedHeader", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LeaveId", LeaveId);

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

        public DataTable GetAllEmployeeLeaveByApprovalDetailsDatailId(long LeaveDetailId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Lev_GetAllEmployeeLeaveByApprovalDetailsDetailId", _dbConnection))
                    {
                        OpenDB();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LeaveDetailId", @LeaveDetailId);

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

        public void AddApprovedLeaveHeader(long LeaveId, long EmployeeId, decimal TotalApprovedQty, string ApproveStatus, string ReasonforReject, string ApprovedPerson)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddApprovedHeader", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LeaveId", LeaveId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@TotalApprovedQty", TotalApprovedQty);
                    command.Parameters.AddWithValue("@ApproveStatus", ApproveStatus);
                    command.Parameters.AddWithValue("@ReasonforReject", ReasonforReject);
                    command.Parameters.AddWithValue("@ApprovedPerson", ApprovedPerson);              
                
                    SqlParameter LeaveApprovedId = new SqlParameter("@LeaveApprovedId", SqlDbType.BigInt, 16);
                    LeaveApprovedId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(LeaveApprovedId);
                    command.ExecuteNonQuery();
                    _leaveApprovedId = Convert.ToInt32(LeaveApprovedId.Value);
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

        public void AddApprovedLeaveDetails(DataTable dtTableDetails)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_AddApprovedDetails", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    DataRow detailRow = dtTableDetails.Rows[0];
                    command.Parameters.AddWithValue("@LeaveApprovedId", detailRow["LeaveApprovedId"]);
                    command.Parameters.AddWithValue("@LeaveDetailId", detailRow["LeaveDetailId"]);
                    command.Parameters.AddWithValue("@ApplyQty", detailRow["ApplyQty"]);
                    command.Parameters.AddWithValue("@ApprovedQty", detailRow["ApprovedQty"]);
                    

                    //command.Parameters.Add("@LeaveApprovedId", SqlDbType.BigInt, 16, "LeaveApprovedId");
                    //command.Parameters.Add("@LeaveDetailId", SqlDbType.BigInt, 16, "LeaveDetailId");
                    //command.Parameters.Add("@ApplyQty", SqlDbType.Decimal, 18, "LeaveQty");
                    //command.Parameters.Add("@ApprovedQty", SqlDbType.Decimal, 18, "ApprovedQty");                
        
                       
                    command.ExecuteNonQuery();
                        //daAdpter.UpdateCommand = command;
                        //daAdpter.Update(dtTableDetails);
                    
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
        #endregion

        #endregion

        #region Destructor

        ~LeaveDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        public long GetEmployeeLeaveCoveringPerson(long LeaveId)
        {
            long _empCoveringPerson = 0;
            OpenDB();
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_GetLeaveCoveringPerson", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LeaveId", LeaveId);

                    SqlParameter empCoveringPerson = new SqlParameter("@CoveringPerson", SqlDbType.BigInt);
                    empCoveringPerson.Direction = ParameterDirection.Output;
                    command.Parameters.Add(empCoveringPerson);

                    command.ExecuteNonQuery();
                    _empCoveringPerson = (long)empCoveringPerson.Value;

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
            return _empCoveringPerson;
        }

        public void UpdateLeaveCoveringPerApprove(string[] LeaveId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateLeaveCoveringStatus", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    foreach (string leaveItem in LeaveId)
                    {
                        long leaveNo = Convert.ToInt64(leaveItem.ToString().Split(',')[0].ToString());
                        string appStatus = leaveItem.ToString().Split(',')[1].ToString();
                        command.Parameters.AddWithValue("@LeaveId", leaveNo);
                        command.Parameters.AddWithValue("@Status", appStatus);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
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

        public void UpdateLeavePendingApprove(long LeaveId,string columnName, string levStatus)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_UpdateLeaveApprovePerson", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LeaveId", LeaveId);
                    command.Parameters.AddWithValue("@columnName", columnName);
                    command.Parameters.AddWithValue("@LeaveStatus", levStatus);
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

        public void ApproveLeaveByHR(long LeaveId, string CreatedUser, int LeaveTypeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_ApproveLeaveByHR", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LeaveId", LeaveId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
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

        public void RejectLeaveByHR(long LeaveId, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_RejectLeaveByHR", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LeaveId", LeaveId);
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
    }
}
