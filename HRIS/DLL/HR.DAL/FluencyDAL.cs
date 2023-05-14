/// <summary>
/// Solution Name       : HRM
/// Project Name        : HR.DAL
/// Author              : ushan deemantha
/// Class Description   : Fluency DAL
/// </summary>

using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace HRM.HR.DAL
{

    public class FluencyDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError=false;
        private string _errorMsg=string.Empty;
        private int _fluencyId=0;
      
        private int _fluencyTypeId=0;
        private long _employeeFluencyId=0;
       
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

        public int FluencyId
        {
            get { return _fluencyId; }           
        }

   

        public int FluencyTypeId 
        {           
            get { return _fluencyTypeId; }
        }


        public long EmployeeFluencyId
        {
          get {return _employeeFluencyId;}
        }

      
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FluencyDAL"/> class.
        /// </summary>
        public FluencyDAL()
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


        #region Fluency

        /// <summary>
        /// Adds the fluency.
        /// </summary>
        /// <param name="FluencyCode">The FluencyCode As string.</param>
        /// <param name="FluencyTypeId">The FluencyTypeId As int.</param>
        /// <param name="FluencyName">FluencyName As string.</param>
        public void AddFluency(string FluencyCode, int FluencyTypeId, string FluencyName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddFluency", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FluencyCode", FluencyCode);
                    command.Parameters.AddWithValue("@FluencyTypeId", FluencyTypeId);
                    command.Parameters.AddWithValue("@FluencyName", FluencyName);

                    SqlParameter fluencyId = new SqlParameter("@FluencyId", SqlDbType.Int, 8);
                    fluencyId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(fluencyId);

                    command.ExecuteNonQuery();
                    _fluencyId = Convert.ToInt32(fluencyId.Value);
                    if (_fluencyId < 0)
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

        /// <summary>
        /// Updates the fluency.
        /// </summary>
        /// <param name="FluencyId">The FluencyId As int.</param>
        /// <param name="FluencyCode">The FluencyCode As string.</param>
        /// <param name="FluencyTypeId">The FluencyTypeId As int.</param>
        /// <param name="FluencyName">FluencyName As string.</param>
        public void UpdateFluency(int FluencyId, string FluencyCode, int FluencyTypeId, string FluencyName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateFluency", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@FluencyCode", FluencyCode);
                    command.Parameters.AddWithValue("@FluencyTypeId", FluencyTypeId);
                    command.Parameters.AddWithValue("@FluencyName", FluencyName);
                    command.Parameters.AddWithValue("@FluencyId", FluencyId);

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
        /// Deletes the fluency.
        /// </summary>
        /// <param name="FluencyId">The FluencyId As  int.</param>
        public void DeleteFluency(int FluencyId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteFluency", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FluencyId", FluencyId);
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

        /// <summary>
        /// Gets the fluency.
        /// </summary>
        /// <param name="FulencyId">The FulencyId As int.</param>
        /// <returns></returns>
        public DataTable GetFluency(int FulencyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetFluency", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@FluencyId", FulencyId);
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

        #region Fluency Types

        /// <summary>
        /// Adds Fluency Types
        /// </summary>
        /// <param name="FluencyTypeCode">FluencyTypeCode As string</param>
        /// <param name="FluencyTypeName">FluencyTypeName As string</param>
        public void AddFluencyType(string FluencyTypeCode, string FluencyTypeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddFluencyType", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FluencyTypeId", FluencyTypeId);
                    command.Parameters.AddWithValue("@FluencyTypeCode", FluencyTypeCode);
                    command.Parameters.AddWithValue("@FluencyTypeName", FluencyTypeName);
                    command.ExecuteNonQuery();
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
        /// Updates Fluency Types.
        /// </summary>
        /// <param name="FluencyTypeId">FluencyTypeId As int</param>
        /// <param name="FluencyTypeCode">FluencyTypeCode As string</param>
        /// <param name="FluencyTypeName">FluencyTypeName As string</param>
        public void UpdateFluencyType(int FluencyTypeId, string FluencyTypeCode, string FluencyTypeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateFluencyType", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@FluencyTypeCode", FluencyTypeCode);
                    command.Parameters.AddWithValue("@FluencyTypeName", FluencyTypeName);
                    command.Parameters.AddWithValue("@FluencyTypeId", FluencyTypeId);
                   

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
        /// Get Fluency Types
        /// </summary>
        /// <param name="FluencyTypeId">FluencyTypeId As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetFluencyType(int FluencyTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetFluencyType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@FluencyTypeId", FluencyTypeId);
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
        /// Deletes the Fluency Type.
        /// </summary>
        /// <param name="FluencyTypeId">FluencyTypeId As int(0=All, -1=None, Any other=Selected)</param>        
        public void DeleteFluencyType(int FluencyTypeId)
        {           
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteFluencyType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FluencyTypeId", FluencyTypeId);                   
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


        #region EmployeeFluency
        /// <summary>
        /// Adds the employee fluency.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int. </param>
        /// <param name="FluencyId">The fluency id as int.</param>
        public void AddEmployeeFluency(long EmployeeId, int LanguageId, string Reading, string Writing, string Speaking, string Listening, long CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeFluency", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LanguageID", LanguageId);
                    command.Parameters.AddWithValue("@Reading", Reading);
                    command.Parameters.AddWithValue("@Writing", Writing);
                    command.Parameters.AddWithValue("@Speaking", Speaking);
                    command.Parameters.AddWithValue("@Listening", Listening);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    SqlParameter employeeFluencyId = new SqlParameter("@EmployeeFluencyId", SqlDbType.Int, 16);
                    employeeFluencyId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeFluencyId);
                    command.ExecuteNonQuery();
                    _employeeFluencyId = Convert.ToInt64(employeeFluencyId.Value);
                    if (_employeeFluencyId < 0)
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
        /// Updates the employee fluency.
        /// </summary>
        /// <param name="EmployeeFluencyId">The employee fluency id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="FluencyId">The fluency id as int. </param>
        public void UpdateEmployeeFluency(long LanguId, int LanguageID, long EmployeeId, string Reading, string Writing, string Speaking, string Listening, long UpdateUser)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeFluency", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@LanguId", LanguId);
                    command.Parameters.AddWithValue("@LanguageID", LanguageID);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Reading", Reading);
                    command.Parameters.AddWithValue("@Writing", Writing);
                    command.Parameters.AddWithValue("@Speaking", Speaking);
                    command.Parameters.AddWithValue("@Listening", Listening);
                    command.Parameters.AddWithValue("@UpdateUser", UpdateUser);
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
        /// Gets the employee fluency.
        /// </summary>
        /// <param name="EmployeeFluencyId">The employee fluency id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeFluency(long LanguId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeFluency", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@LanguId", LanguId);
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

        public DataTable GetEmployeeFluencyByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeFluencyByEmployeeId", _dbConnection))
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


        /// <summary>
        /// Deletes the employee fluency.
        /// </summary>
        /// <param name="EmployeeFluencyId">The employee fluency id as int.</param>
        public void DeleteEmployeeFluency(long EmployeeFluencyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeFluency", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeFluencyId", EmployeeFluencyId);
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
        ~FluencyDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
