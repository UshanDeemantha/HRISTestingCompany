/// <summary>
/// Solution Name        : HRM
/// Project Name          : HR.DAL
/// Author                     : ushan deemantha
/// Class Description    : Sport DAL
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace HRM.HR.DAL
{
    public class SportDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private DataSet _sports;
        private bool _isError;
        private string _errorMsg;
        private int _sportId;
        private long _employeeSportId;
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


        public int SportId
        {
            get { return _sportId; }
            set { _sportId = value; }
        }

        public long EmployeeSportId
        {
            get { return _employeeSportId; }
        }


        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public SportDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }


        #region Sport
        /// <summary>
        /// Adds the sport.
        /// </summary>
        /// <param name="SportCode">The sport code as string.</param>
        /// <param name="SportName">Name of the sport as string.</param>
        public void AddSport(string SportCode, string SportName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddSport", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SportCode", SportCode);
                    command.Parameters.AddWithValue("@SportName", SportName);
                    SqlParameter sportIdInfo = new SqlParameter("@SportId", SqlDbType.Int, 16);
                    sportIdInfo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sportIdInfo);
                    command.ExecuteNonQuery();

                    _sportId = Convert.ToInt32(sportIdInfo.Value);
                    if (_sportId < 0)
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
        /// Updates the sport.
        /// </summary>
        /// <param name="SportId">The sport id as int.</param>
        /// <param name="SportCode">The sport code as string.</param>
        /// <param name="SportName">Name of the sport as string.</param>
        public void UpdateSport(int SportId, string SportCode, string SportName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateSport", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SportCode", SportCode);
                    command.Parameters.AddWithValue("@SportName", SportName);
                    command.Parameters.AddWithValue("@SportId", SportId);
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







        public DataTable GetSport(int SportId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetSport", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SportId", SportId);
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


        public void DeleteSport(int SportId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteSport", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SportId", SportId);
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
        #endregion

        #region Employee Sport

        /// <summary>
        /// Adds the employee sport.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="FluencyId">The fluency id.</param>
        public void AddEmployeeSport(long EmployeeId, int SportId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeSportActivities", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@SportId", SportId);
                    SqlParameter employeeSportId = new SqlParameter("@EmployeeSportActivityID", SqlDbType.Int, 16);
                    employeeSportId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeSportId);
                    command.ExecuteNonQuery();
                    _employeeSportId = Convert.ToInt64(employeeSportId.Value);
                    if (_employeeSportId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _errorMsg = ex.Message;
            //}
            catch (DbException ex)
            {
                int i = ex.ErrorCode;
            }
            finally
            {
                _dbConnection.Close();
            }
        }


        /// <summary>
        /// Updates the employee sport.
        /// </summary>
        /// <param name="EmployeeSportId">The employee sport id.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="SportId">The sport id.</param>
        public void UpdateEmployeeSport(long EmployeeSportId, long EmployeeId, int SportId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeSport", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeSportId", EmployeeSportId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@SportId", SportId);
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
        /// Gets the employee sport.
        /// </summary>
        /// <param name="EmployeeSportId">The employee sport id.</param>
        /// <returns></returns>
        public DataTable GetEmployeeSport(long EmployeeSportId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeSportActivities", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeSportActivityID", EmployeeSportId);
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

        public DataTable GetEmployeeSportByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeSportActivitiesByEmployeeId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeId);
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
        /// Deletes the employee sport.
        /// </summary>
        /// <param name="EmployeeSportId">The employee sport id.</param>
        public void DeleteEmployeeSport(long EmployeeSportId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeSportActivities", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeSportActivityID", EmployeeSportId);
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

        #endregion

        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="StudentDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~SportDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
