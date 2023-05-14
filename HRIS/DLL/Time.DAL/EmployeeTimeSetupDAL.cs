using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace HRM.Time.DAL
{
    public class EmployeeTimeSetupDAL
    {
        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private long _employeeID = 0;
        int _errorNo = 0;

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

        public long EmployeeID
        {
            get { return _employeeID; }
        }
        
        #endregion

        #region Constructor

        public EmployeeTimeSetupDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion


        #region Methods

        #region Internal
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        } 
        #endregion



        #region External





        public DataTable GetEmployeeTime(long EmployeeId, int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetEmployeeTimeSetups", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        public DataTable GetEmployeeTime(int jobiD, long OrgID, int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetEmployeeTimeSetupsByOrgID", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@OrgID", OrgID);
                        command.Parameters.AddWithValue("@JobID", jobiD);
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


        public DataTable GetEmployeeTimeByOrgID(int jobiD, long OrgID, int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetEmployeeTimeSetupsByOrgID", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@OrgID", OrgID);
                        command.Parameters.AddWithValue("@JobID", jobiD);
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

        public DataTable GetEmployeesInOrNotInTime(int jobiD, long OrgID, int CompanyId, string Type)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetEmployeesInOrNotInTime", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@OrgStructureID", OrgID);
                        command.Parameters.AddWithValue("@JobCategoryID", jobiD);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Type", Type);
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
        public DataTable GetEmployeesInOrNotInTime2(int jobiD, long OrgID, int CompanyId, string Type)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetEmployeesInOrNotInTime", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@OrgStructureID", OrgID);
                        command.Parameters.AddWithValue("@JobCategoryID", jobiD);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Type", Type);
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

        public DataTable GetEmployeeLate(long EmployeeId, int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetEmployeeLateSetups", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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
       
             public void AddNewEmpLeaveLev(long hfEmloyeeId, string CreatedUser, DateTime efectiveDate , bool isChecked)
        {
            try
            {

                using (SqlCommand command = new SqlCommand("Lev_ModifyNewEmployeeStatutoryLeaveEntitlements", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", hfEmloyeeId);
                    command.Parameters.AddWithValue("@CreateUser", CreatedUser);
                    command.Parameters.AddWithValue("@EfectiveDte", efectiveDate);
                    command.Parameters.AddWithValue("@IsChecked", isChecked);
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
        public void  AddNewEmpLeaveLev(long hfEmloyeeId, string CreatedUser)
                  {
            try
            {

                using (SqlCommand command = new SqlCommand("Lev_NewEmployeeStatutoryLeaveEntitlements", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", hfEmloyeeId);
                    command.Parameters.AddWithValue("@CreateUser", CreatedUser);
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

        public void AddEmployeeTimeSetup(int CompanyId, long EmployeeId, string CardNo, long ImmediateSuperviser, int DefaultShiftId, int RosterGroupId, bool OTEntitlement, bool EarlyOTEntitlement,
                                   bool LievLeaveEntitlement, bool LateAttendanceCalculation, bool EarlyDepartureCalculation, bool PayCutCalculation, bool ConsiderAttendance,bool NightOtEntitle, int LateSetupId, string CreatedUser)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddEmployeeTimeSetup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CardNo", CardNo);
                    command.Parameters.AddWithValue("@ImmediateSuperviser", ImmediateSuperviser);
                    command.Parameters.AddWithValue("@DefaultShiftId", DefaultShiftId);
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    command.Parameters.AddWithValue("@OTEntitlement", OTEntitlement);
                    command.Parameters.AddWithValue("@EarlyOTEntitlement", EarlyOTEntitlement);
                    command.Parameters.AddWithValue("@LievLeaveEntitlement", LievLeaveEntitlement);
                    command.Parameters.AddWithValue("@LateAttendanceCalculation", LateAttendanceCalculation);
                    command.Parameters.AddWithValue("@EarlyDepartureCalculation", EarlyDepartureCalculation);
                    command.Parameters.AddWithValue("@PayCutCalculation", PayCutCalculation);
                    command.Parameters.AddWithValue("@ConsiderAttendance", ConsiderAttendance);
                    command.Parameters.AddWithValue("@NightOtEntitle", NightOtEntitle);
                    command.Parameters.AddWithValue("@LateSetupId", LateSetupId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();


                }


                using (SqlCommand command = new SqlCommand("Time_SetNewEmployeeDefaultTime_Txn", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@DefaultShiftId", DefaultShiftId);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();


                }

                using (SqlCommand command = new SqlCommand("Time_SetNewEmployeeShift", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        public void ChangeEmployeeAttendaceDetails(int CompanyId, long EmployeeId, string CreatedUser, bool ChangelateAttendace, bool ChangeEarlyDepartures, bool ChangeEarlyOT, bool ChangePostOT, bool ChangeOT20, string Reason, DateTime FromDate, DateTime Todate)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("Time_ChangeAttendanceDetails", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@ChangelateAttendace", ChangelateAttendace);
                    command.Parameters.AddWithValue("@ChangeEarlyDepartures", ChangeEarlyDepartures);
                    command.Parameters.AddWithValue("@ChangeEarlyOT", ChangeEarlyOT);
                    command.Parameters.AddWithValue("@ChangePostOT", ChangePostOT);
                    command.Parameters.AddWithValue("@ChangeOT20", ChangeOT20);
                    command.Parameters.AddWithValue("@Reason", Reason);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@Todate", Todate);
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


        public void UpdateEmployeeTimeSetup(long EmployeeId, string CardNo, long ImmediateSuperviser, int DefaultShiftId, int RosterGroupId, bool OTEntitlement, bool EarlyOTEntitlement,
                              bool LievLeaveEntitlement, bool LateAttendanceCalculation, bool EarlyDepartureCalculation, bool PayCutCalculation, bool ConsiderAttendance,bool NightOtEntitle, string CreatedUser, DateTime? EffectiveDate,
                              DateTime OTEntitlementEffectDate, DateTime EarlyOTEntitlementEffectDate, DateTime LievLeaveEntitlementEffectDate, DateTime LateAttendanceCalculationEffectDate, DateTime EarlyDepartureCalculationEffectDate,
                              DateTime? PayCutCalculationEffectDate, DateTime ConsiderAttendanceEffectDate, DateTime? NightOtEntitleEffectDate)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateEmployeeTimeSetup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CardNo", CardNo);
                    command.Parameters.AddWithValue("@ImmediateSuperviser", ImmediateSuperviser);
                    command.Parameters.AddWithValue("@DefaultShiftId", DefaultShiftId);
                    command.Parameters.AddWithValue("@RosterGroupId", RosterGroupId);
                    command.Parameters.AddWithValue("@OTEntitlement", OTEntitlement);
                    command.Parameters.AddWithValue("@EarlyOTEntitlement", EarlyOTEntitlement);
                    command.Parameters.AddWithValue("@LievLeaveEntitlement", LievLeaveEntitlement);
                    command.Parameters.AddWithValue("@LateAttendanceCalculation", LateAttendanceCalculation);
                    command.Parameters.AddWithValue("@EarlyDepartureCalculation", EarlyDepartureCalculation);
                    command.Parameters.AddWithValue("@PayCutCalculation", PayCutCalculation);
                    command.Parameters.AddWithValue("@ConsiderAttendance", ConsiderAttendance);
                    command.Parameters.AddWithValue("@NightOtEntitle", NightOtEntitle);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@OTEntitlementEffectDate", OTEntitlementEffectDate);
                    command.Parameters.AddWithValue("@EarlyOTEntitlementEffectDate", EarlyOTEntitlementEffectDate);
                    command.Parameters.AddWithValue("@LievLeaveEntitlementEffectDate", LievLeaveEntitlementEffectDate);
                    command.Parameters.AddWithValue("@LateAttendanceCalculationEffectDate", LateAttendanceCalculationEffectDate);
                    command.Parameters.AddWithValue("@EarlyDepartureCalculationEffectDate", EarlyDepartureCalculationEffectDate);
                    command.Parameters.AddWithValue("@PayCutCalculationEffectDate", PayCutCalculationEffectDate);
                    command.Parameters.AddWithValue("@ConsiderAttendanceEffectDate", ConsiderAttendanceEffectDate);
                    command.Parameters.AddWithValue("@NightOtEntitleEffectDate", NightOtEntitleEffectDate);
                    command.ExecuteNonQuery();


                }

                if (EffectiveDate != null)
                {
                    using (SqlCommand command = new SqlCommand("Time_SetNewEmployeeShiftUpdate", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                        command.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                        command.ExecuteNonQuery();
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

        public void AddLateAttendanceCalculation(long EmployeeId, int LateSetupId, string CreatedUser)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddEmployeeLateSetup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LateSetupId", LateSetupId);
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


        public void DeleteLateAttendanceCalculation(long EmployeeId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteEmployeeLateSetup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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



        public DataTable GetLateAttendanceSetup(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetLateAttendanceSetup", _dbConnection))
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

    }
}
