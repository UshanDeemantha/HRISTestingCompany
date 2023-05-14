/// <summary>
/// Solution Name        : HRM
/// Project Name          : HR.DAL
/// Author                     : ushan deemantha
/// Class Description    : Qulification DAL
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace HRM.HR.DAL
{

    public class QulificationDAL
    {
        
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError=false;
        private string _errorMsg=string.Empty;
        private int _qulificationId=0;
      
     
        private long _employeeQulificationId=0;
       
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

        public int QulificationId
        {
            get { return _qulificationId; }           
        }

   

        


        public long EmployeeQulificationId
        {
          get {return _employeeQulificationId;}
        }

      
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="QulificationDAL"/> class.
        /// </summary>
        public QulificationDAL()
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
            InitializeErrorFields();
        }

        private void InitializeErrorFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
        } 
        #endregion



          #region EmployeeQulification
        /// <summary>
        /// Adds the employee Qulification.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int. </param>
        /// <param name="QulificationId">The Qulification id as int.</param>
        public void AddEmployeeQulification(long EmployeeId,int CourseId,int Year)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeQulification", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CourseId", CourseId);
                    command.Parameters.AddWithValue("@Year", Year);
                    SqlParameter employeeQulificationId = new SqlParameter("@EmployeeQulificationId", SqlDbType.Int, 16);
                    employeeQulificationId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeQulificationId);
                    command.ExecuteNonQuery();
                    _employeeQulificationId = Convert.ToInt64(employeeQulificationId.Value);
                    if (_employeeQulificationId < 0)
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
        /// Updates the employee Qulification.
        /// </summary>
        /// <param name="EmployeeQulificationId">The employee Qulification id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="QulificationId">The Qulification id as int. </param>
        public void UpdateEmployeeQulification(long EmployeeQulificationId, long EmployeeId,int CourseId,int Year)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeQulification", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeQulificationId", EmployeeQulificationId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CourseId", CourseId);
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

        /// <summary>
        /// Gets the employee Qulification.
        /// </summary>
        /// <param name="EmployeeQulificationId">The employee Qulification id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeQulification(long EmployeeQulificationId)
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

        public DataTable GetEmployeeQulificationByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeQulificationByEmployeeId", _dbConnection))
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
        /// Deletes the employee Qulification.
        /// </summary>
        /// <param name="EmployeeQulificationId">The employee Qulification id as int.</param>
        public void DeleteEmployeeQulification(long EmployeeQulificationId)
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
        ~QulificationDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
