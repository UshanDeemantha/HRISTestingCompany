/// <summary>
/// Solution Name        : HRM
/// Project Name          : HR.DAL
/// Author                     : ushan deemantha
/// Class Description    : Language DAL
/// </summary>


using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace HRM.HR.DAL
{
    
    public class LanguagesDAL
    {

        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _languageId = 0;
        private long _employeeLanguageId = 0;
        
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
        public int LanguageId
        {
            get { return _languageId; }
        }
        public long EmployeeLanguageId
        {

            get { return _employeeLanguageId; }
        }

        #endregion


          #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public LanguagesDAL()
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


         #region Languges

        /// <summary>
        /// Adds the language.
        /// </summary>
        /// <param name="LanguageName">Name of the language as string.</param>
        public void AddLanguage(string LanguageName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddLanguage", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LanguageName", LanguageName);
                    SqlParameter languageId = new SqlParameter("@LanguageId", SqlDbType.Int, 8);
                    languageId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(languageId);
                    command.ExecuteNonQuery();
                    _languageId = Convert.ToInt32(languageId.Value);
                    if (_languageId < 0)
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
        /// Updates the language.
        /// </summary>
        /// <param name="LanguageId">The language id as int.</param>
        /// <param name="LanguageName">Name of the language as string.</param>
        public void UpdateLanguage(int LanguageId, string LanguageName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateLanguage", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@LanguageId", LanguageId);
                    command.Parameters.AddWithValue("@LanguageName", LanguageName);

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
        /// Gets the language.
        /// </summary>
        /// <param name="LanguageId">The language id as int.</param>
        /// <returns></returns>
        public DataTable GetLanguage(int LanguageId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetLanguage", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@LanguageId", LanguageId);
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
        /// Deletes the language.
        /// </summary>
        /// <param name="LanguageId">The language id as int.</param>
        public void DeleteLanguage(int LanguageId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteLanguage", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LanguageId", LanguageId);
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


        #region Employee Languges
        /// <summary>
        /// Adds the employee language.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        /// <param name="LanguageID">The language ID.</param>
        public void AddEmployeeLanguage(long EmployeeID, int LanguageID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeLanguage", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@LanguageID", LanguageID);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    SqlParameter employeeLanguageId = new SqlParameter("@EmployeeLanguageId", SqlDbType.Int, 16);
                    employeeLanguageId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeLanguageId);
                    command.ExecuteNonQuery();
                    _employeeLanguageId = Convert.ToInt32(employeeLanguageId.Value);
                    if (_employeeLanguageId < 0)
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
        /// Updates the employee language.
        /// </summary>
        /// <param name="EmployeeLanguageId">The employee language id.</param>
        /// <param name="EmployeeID">The employee ID.</param>
        /// <param name="LanguageID">The language ID.</param>
        public void UpdateEmployeeLanguage(long EmployeeLanguageId, long EmployeeID, int LanguageID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeLanguage", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeLanguageId", EmployeeLanguageId);
                    command.Parameters.AddWithValue("@LanguageID", LanguageID);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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
        /// Gets the employee language.
        /// </summary>
        /// <param name="EmployeeLanguageId">The employee language id.</param>
        /// <returns></returns>
        public DataTable GetEmployeeLanguage(int EmployeeLanguageId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeLanguage", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeLanguageId", EmployeeLanguageId);
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
        /// Deletes the employee language.
        /// </summary>
        /// <param name="EmployeeLanguageId">The employee language id.</param>
        public void DeleteEmployeeLanguage(int EmployeeLanguageId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeLanguage", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeLanguageId", EmployeeLanguageId);
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
        ~LanguagesDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion



    }
}
