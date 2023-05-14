/// <summary>
/// /// <summary>
/// /// Solution Name : HR Mannagement System
/// /// Author : ushan deemantha
/// /// Class Description : Membership.DAL
/// /// </summary>
/// </summary>



using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.HR.DAL
{
   
   public  class MemberShipDAL
    {
        #region Fields

        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;

        private int _memberShipId;
        private string _membershipCode;
        private string _membershipName;

        private int _membershipTypeId;
        private string _membershipTypeCode;
        private string _membershipTypeName;

        private int _employeeMembershipId;
        private bool _active;
      
        #endregion

        #region Properties
          /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        /// <summary>
        /// Gets the member ship ID.
        /// </summary>
        /// <value>The member ship ID.</value>
        public int MemberShipID
        {
            get
            {
                return _memberShipId;
            }
        }

        /// <summary>
        /// Gets or sets the member ship code.
        /// </summary>
        /// <value>The member ship code.</value>
        public string MemberShipCode
        {
            get
            {
                return _membershipCode;
            }
            set
            {
                _membershipCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the member ship.
        /// </summary>
        /// <value>The name of the member ship.</value>
        public string MemberShipName
        {
            get
            {
                return _membershipName;
            }
            set
            {
                _membershipName = value;
            }
        }

        /// <summary>
        /// Gets the employee membership ID.
        /// </summary>
        /// <value>The employee membership ID.</value>
        public int EmployeeMembershipID
        {
            get
            {
                return _employeeMembershipId;
            }
        }

        /// <summary>
        /// Gets the membership tyor ID.
        /// </summary>
        /// <value>The membership tyor ID.</value>
        public int MembershipTypeID
        {
            get
            {
                return _membershipTypeId;
            }

        }
        /// <summary>
        /// Gets or sets the membership type code.
        /// </summary>
        /// <value>The membership type code.</value>
        public string MembershipTypeCode
        {
            get
            {
                return _membershipTypeCode;
            }
            set
            {
                _membershipTypeCode = value;
            }
        }
        /// <summary>
        /// Gets or sets the membership type code.
        /// </summary>
        /// <value>The membership type code.</value>
        public string MembershipTypeName
        {
            get
            {
                return _membershipTypeName;
            }
            set
            {
                _membershipTypeName = value;
            }
        }

        #endregion

        #region Methords

        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        /// <summary>
        /// Adds the member ship.
        /// </summary>
        public void AddMembership(string MembershipCode,int MembershipTypeId,string MembershipName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddMemberShips", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@MembershipCode", MembershipCode);
                    command.Parameters.AddWithValue("@MembershipTypeID", MembershipTypeId);
                    command.Parameters.AddWithValue("@MembershipName", MembershipName);

                    SqlParameter membsipId= new SqlParameter("@MembershipID", SqlDbType.Int, 8);
                    membsipId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(membsipId);
                    command.ExecuteNonQuery();

                    _memberShipId = Convert.ToInt32(membsipId.Value);
                    if (_memberShipId < 0) 
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
                    
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

        /// <summary>
        /// Adds the type of the membersip.
        /// </summary>
        public void AddMembersipType(string MembershipTypeCode, string MemebrshipTypeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddMemberShipTypes", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@MembershipTypeCode", MembershipTypeCode);
                    command.Parameters.AddWithValue("@MembershipTypeName", MemebrshipTypeName);

                    SqlParameter membsipTypeId = new SqlParameter("@MembershipTypeID", SqlDbType.Int, 16);
                    membsipTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(membsipTypeId);
                    command.ExecuteNonQuery();

                    _membershipTypeId = Convert.ToInt32(membsipTypeId.Value);
                    if (_membershipTypeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already exists";
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

        /// <summary>
        /// Adds the employee memberships.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        /// <param name="MembershipID">The membership ID.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="Todate">The todate.</param>
        public void AddEmployeeMemberships(int EmployeeID, int MembershipID,DateTime FromDate, DateTime Todate, string Grade, string Remark)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeMemberShips", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@MembershipID", MembershipID);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", Todate);
                    command.Parameters.AddWithValue("@Grade", Grade);
                    command.Parameters.AddWithValue("@Remark", Remark);

                    SqlParameter employeeMembsipId = new SqlParameter("@EmployeeMembershipID", SqlDbType.Int, 8);
                    employeeMembsipId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeMembsipId);
                    command.ExecuteNonQuery();

                     _employeeMembershipId= Convert.ToInt32(employeeMembsipId.Value);
                     if (_employeeMembershipId < 0)
                     {
                         _isError = true;
                         _errorMsg = "Already exists";
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

        public void AddEmployeeMemberships(int EmployeeID, int MembershipID, DateTime FromDate, DateTime Todate, bool IsToDate ,string Grade, string Remark)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeMemberShips", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@MembershipID", MembershipID);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    if(IsToDate)
                    command.Parameters.AddWithValue("@ToDate", Todate);
                    else
                        command.Parameters.AddWithValue("@ToDate",DBNull.Value);
                    command.Parameters.AddWithValue("@Grade", Grade);
                    command.Parameters.AddWithValue("@Remark", Remark);

                    SqlParameter employeeMembsipId = new SqlParameter("@EmployeeMembershipID", SqlDbType.Int, 8);
                    employeeMembsipId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeMembsipId);
                    command.ExecuteNonQuery();

                    _employeeMembershipId = Convert.ToInt32(employeeMembsipId.Value);
                    if (_employeeMembershipId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already exists";
                    }
                }
            }
            catch(SqlException sqlex)
            {

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

        /// <summary>
        /// Updates the membership.
        /// </summary>
        /// <param name="MembershipID">The membership ID.</param>
        public void UpdateMembership(int MembershipID, string MembershipCode,int MembershipTypeId,string MembershipName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateMembership", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@MembershipID", MembershipID);
                    command.Parameters.AddWithValue("@MembershipCode", MembershipCode);
                    command.Parameters.AddWithValue("@MembershipTypeID", MembershipTypeId);
                    command.Parameters.AddWithValue("@MembershipName", MembershipName);
                    
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

        /// <summary>
        /// Updates the type of the membership.
        /// </summary>
        /// <param name="MembershipTypeID">The membership type ID.</param>
        public void UpdateMembershipType(int MembershipTypeID,bool IsUpdate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateMembershipType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@MembershipTypeID", MembershipTypeID);
                    command.Parameters.AddWithValue("@MembershipTypeCode", MemberShipCode);
                    command.Parameters.AddWithValue("@MembershipTypeName",MemberShipName);
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

        /// <summary>
        /// Updates the employee memberships.
        /// </summary>
        /// <param name="EmployeeMembershipID">The employee membership ID.</param>
        public void UpdateEmployeeMemberships(int EmployeeMembershipID, int EmployeeID, int MembershipID, DateTime FromDate, DateTime Todate, string Grade, string Remark)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeMembership", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeMembershipID", EmployeeMembershipID);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@MembershipID", MembershipID);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@Todate", Todate);
                    command.Parameters.AddWithValue("@Grade", Grade);
                    command.Parameters.AddWithValue("@Remark", Remark);
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

        public void UpdateEmployeeMemberships(int EmployeeMembershipID, int EmployeeID, int MembershipID, DateTime FromDate, DateTime Todate,bool IsToDate, string Grade, string Remark)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeMembership", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeMembershipID", EmployeeMembershipID);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@MembershipID", MembershipID);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    if (IsToDate)
                    command.Parameters.AddWithValue("@Todate", Todate);
                    else
                        command.Parameters.AddWithValue("@Todate", DBNull.Value);
                    command.Parameters.AddWithValue("@Grade", Grade);
                    command.Parameters.AddWithValue("@Remark", Remark);
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

        /// <summary>
        /// Deletes the employee membership.
        /// </summary>
        /// <param name="EmployeeMemberhsipID">The employee memberhsip ID.</param>
        public void DeleteEmployeeMembership(int EmployeeMemberhsipID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeMembership", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeMembershipID", EmployeeMemberhsipID);
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


        /// <summary>
        /// Deletes the membership.
        /// </summary>
        /// <param name="MemberhsipId">The memberhsip id as int.</param>
        public void DeleteMembership(int MemberhsipId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteMembership", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@MembershipId", MemberhsipId);
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


        /// <summary>
        /// Deletes the type of the membership.
        /// </summary>
        /// <param name="MembershipTypeId">The membership type id as int.</param>
        public void DeleteMembershipType(int MembershipTypeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteMembershipType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@MembershipTypeId", MembershipTypeId);
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



        /// <summary>
        /// Gets the memberships.
        /// </summary>
        /// <param name="MembershipID">The membership ID </param>
        /// <param name="Active">if set to <c>true</c> [active].</param>
        /// <returns>Data Tabele</returns>
        public DataTable GetMemberships(int MembershipID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetMemberships", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@MembershipID", MembershipID);
                   
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

        /// <summary>
        /// Gets the type of the membership.
        /// </summary>
        /// <param name="MembershipTypeID">The membership type ID.</param>
        /// <param name="Active">if set to <c>true</c> [active].</param>
        /// <returns>Data Table</returns>
        public DataTable GetMembershipTypes(int MembershipTypeID, bool Active)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetMembershipTypes", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@MembershipTypeID", MembershipTypeID);
                    command.Parameters.AddWithValue("@Active",Active);
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


        /// <summary>
        /// Gets the employee memberships.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        /// <returns>Data Table</returns>
        public DataTable GetEmployeeMemberships(int EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeMemberships", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeMembershipID", EmployeeID);
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

        public DataTable GetEmployeeMembershipByEmployeeId(long EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeMembershipsByEmployeeId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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
       

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberShipDAL"/> class.
        /// </summary>
        public MemberShipDAL()
        {
             _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MemberShipDAL"/> class.
        /// </summary>
        /// <param name="MembershipTypeID">The membership type ID.</param>
        public MemberShipDAL(int EmployeeMembershipID)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            _employeeMembershipId = EmployeeMembershipID;
        } 
        
        #endregion

        #region Distructor
        ~MemberShipDAL()
        {
            GC.SuppressFinalize(this);
        } 
        #endregion
        
    }
}
