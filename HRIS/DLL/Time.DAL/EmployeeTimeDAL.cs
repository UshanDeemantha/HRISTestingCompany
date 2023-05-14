using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using HRM.Common.DAL;
using System.Configuration;

namespace HRM.Time.DAL
{
    public class EmployeeTimeDAL:EmployeeDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private long _employeeId;       
        private string _employeeName;
        private long _immediateSuperviserId;

        private int _defaultShiftId;
        private int _rosterGroupId;
        private int _jobCategoryId;

        private bool _isPayCutCalculation;
        private bool _isEarlyDepartureCalculation;
        private bool _isEarlyOTEntitlement;
        private bool _isOTEntitlement;
        private bool _isLievLeaveEntitlement;
        private bool _isLateAttendanceCalculation;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
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

        public long EmployeeId
        {
            get { return _employeeId; }
        }
              
        public string EmployeeName
        {
            get { return _employeeName; }
        }

        public long ImmediateSuperviserId
        {
            get { return _immediateSuperviserId; }
        }

        public int DefaultShiftId
        {
            get { return _defaultShiftId; }
        }

        public int RosterGroupId
        {
            get { return _rosterGroupId; }
        }

        public int JobCategoryId
        {
            get { return _jobCategoryId; }
        }

        public bool IsOTEntitlement
        {
            get { return _isOTEntitlement; }
        }

        public bool IsEarlyOTEntitlement
        {
            get { return _isEarlyOTEntitlement; }
        }

        public bool IsLievLeaveEntitlement
        {
            get { return _isLievLeaveEntitlement; }
        }

        public bool IsLateAttendanceCalculation
        {
            get { return _isLateAttendanceCalculation; }
        }

        public bool IsEarlyDepartureCalculation
        {
            get { return _isEarlyDepartureCalculation; }
        }

        public bool IsPayCutCalculation
        {
            get { return _isPayCutCalculation; }
        }   
        #endregion

        //public EmployeeTimeDAL()
        //{
        //    _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        //}
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
        #region Constructors
        public EmployeeTimeDAL(long EmployeeId, int CompanyId)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetEmployeeTimeSetups", _dbConnection))
                {

                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.CommandType = CommandType.StoredProcedure;
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
            if (dtTable.Rows.Count >= 0)
            {
                _employeeId = Convert.ToInt64(dtTable.Rows[0]["EmployeeId"]);       
                //  _employeeName;
                _immediateSuperviserId = Convert.ToInt64(dtTable.Rows[0]["ImmediateSuperviser"]);

                _defaultShiftId = Convert.ToInt32(dtTable.Rows[0]["DefaultShiftId"]);
                _rosterGroupId = Convert.ToInt32(dtTable.Rows[0]["RosterGroupId"]);
              //  _jobCategoryId = Convert.ToInt32(dtTable.Rows[0]["EmployeeId"]);

                _isPayCutCalculation = Convert.ToBoolean(dtTable.Rows[0]["PayCutCalculation"]);
                _isEarlyDepartureCalculation = Convert.ToBoolean(dtTable.Rows[0]["EarlyDepartureCalculation"]);
                _isEarlyOTEntitlement = Convert.ToBoolean(dtTable.Rows[0]["EarlyOTEntitlement"]);
                _isOTEntitlement = Convert.ToBoolean(dtTable.Rows[0]["OTEntitlement"]);
                _isLievLeaveEntitlement = Convert.ToBoolean(dtTable.Rows[0]["LievLeaveEntitlement"]);
                _isLateAttendanceCalculation = Convert.ToBoolean(dtTable.Rows[0]["LateAttendanceCalculation"]);
               // base.Active = Convert.ToBoolean(dtTable.Rows[0]["Active"]);
            }
            dtTable.Dispose();
        } 
        #endregion
    }
}
