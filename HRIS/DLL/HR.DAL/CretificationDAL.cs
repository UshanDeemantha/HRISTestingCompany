
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HRM.HR.DAL
{
    
    public class CretificationDAL
    {
         #region Fields
        private SqlConnection _dbConnection;
        private DataSet _childs;
        private bool _isError;
        private string _errorMsg;
        private int _certificationId;
        private long _employeeQulificationId;
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


        public int CertificationId
        {
            get { return _certificationId; }
            set { _certificationId = value; }
        }
        public long EmployeeQulificationId
        {
            get { return _employeeQulificationId; }
            set { _employeeQulificationId = value; }
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
        public CretificationDAL()
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

        #region Certification

        /// <summary>
        /// Adds the cretification.
        /// </summary>
        /// <param name="CretificationCode">The cretification code as string.</param>
        /// <param name="CretificationName">Name of the cretification as string.</param>
        /// <param name="InstituteId">The institute id as int.</param>
        public void AddCretification(string CretificationCode, string CretificationName, int InstituteId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddCretification", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();



                    command.Parameters.AddWithValue("@CretificationCode", CretificationCode);
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
                    command.Parameters.AddWithValue("@CretificationName", CretificationName);
                    SqlParameter cretificationIdInfo = new SqlParameter("@CretificationId", SqlDbType.Int, 8);
                    cretificationIdInfo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(cretificationIdInfo);


                    command.ExecuteNonQuery();

                    _certificationId = Convert.ToInt32(cretificationIdInfo.Value);
                    if (_certificationId < 0)
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
        /// Updates the cretification.
        /// </summary>
        /// <param name="CretificationId">The cretification id.</param>
        /// <param name="CretificationCode">The cretification code.</param>
        /// <param name="CretificationName">Name of the cretification.</param>
        /// <param name="InstituteId">The institute id.</param>
        public void UpdateCretification(int CretificationId, string CretificationCode, string CretificationName, int InstituteId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateCretification", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CretificationId", CretificationId);
                    command.Parameters.AddWithValue("@CretificationCode", CretificationCode);
                    command.Parameters.AddWithValue("@CretificationName", CretificationName);
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
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
        /// Gets the cretification.
        /// </summary>
        /// <param name="CretificationId">The cretification id.</param>
        /// <returns></returns>
        public DataTable GetCretification(int CretificationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetCretification", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CretificationId", CretificationId);
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
        /// Deletes the cretification.
        /// </summary>
        /// <param name="CretificationId">The cretification id.</param>
        public void DeleteCretification(int CretificationId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteCretification", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CretificationId", CretificationId);
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

        #region Employee Qulification
        /// <summary>
        /// Adds the employee qulification.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="CourseId">The course id.</param>
        /// <param name="year">The year.</param>
        public void AddEmployeeQulification(long EmployeeId, string CourseId, int year)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddEmployeeQulification", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();



                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CourseId", CourseId);
                    SqlParameter qulificationIdInfo = new SqlParameter("@EmployeeQulificationId", SqlDbType.Int, 16);
                    qulificationIdInfo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(qulificationIdInfo);


                    command.ExecuteNonQuery();

                    _employeeQulificationId = Convert.ToInt32(qulificationIdInfo.Value);
                    if (_employeeQulificationId < 0)
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
        /// Updates the employee qulification.
        /// </summary>
        /// <param name="EmployeeQulificationId">The employee qulification id.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="CourseId">The course id.</param>
        /// <param name="year">The year.</param>
        public void UpdateEmployeeQulification(long EmployeeQulificationId,long EmployeeId, string CourseId, int year)
        
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateEmployeeQulification", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeQulificationId", EmployeeQulificationId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CourseId", CourseId);
                    command.Parameters.AddWithValue("@year", year);
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
        /// Gets the employee qulification.
        /// </summary>
        /// <param name="EmployeeQulificationId">The employee qulification id.</param>
        /// <returns></returns>
        public DataTable GetEmployeeQulification(int EmployeeQulificationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeQulification", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeQulificationId", EmployeeQulificationId);
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
        /// Deletes the employee qulification.
        /// </summary>
        /// <param name="EmployeeQulificationId">The employee qulification id.</param>
        public void DeleteEmployeeQulification(int EmployeeQulificationId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeQulification", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeQulificationId", EmployeeQulificationId);
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



        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="StudentDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~CretificationDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
