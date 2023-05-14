
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
    public class CertificationDAL
    {
          #region Fields
        private SqlConnection _dbConnection;
        private bool _isError=false;
        private string _errorMsg=string.Empty;
        private int _certificationId=0;
      
     
        private long _employeeCertificationId=0;
       
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

        public int CertificationId
        {
            get { return _certificationId; }           
        }

   

    

        public long EmployeeCertificationId
        {
          get {return _employeeCertificationId;}
        }

      
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificationDAL"/> class.
        /// </summary>
        public CertificationDAL()
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
        #region Certification


        /// <summary>
        /// Adds the certification.
        /// </summary>
        /// <param name="CertificationCode">The certification code.</param>
        /// <param name="InstituteId">The institute id.</param>
        /// <param name="CertificationName">Name of the certification.</param>
        public void AddCertification(string CertificationCode,int InstituteId,  string CertificationName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddCertifications", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CertificationCode", CertificationCode);
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
                    command.Parameters.AddWithValue("@CertificationName", CertificationName);

                    SqlParameter CertificationId = new SqlParameter("@CertificationId", SqlDbType.Int, 8);
                    CertificationId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(CertificationId);

                    command.ExecuteNonQuery();
                    _certificationId = Convert.ToInt32(CertificationId.Value);
                    if (_certificationId < 0)
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
        /// Updates the certification.
        /// </summary>
        /// <param name="CertificationId">The certification id.</param>
        /// <param name="CertificationCode">The certification code.</param>
        /// <param name="InstituteId">The institute id.</param>
        /// <param name="CertificationName">Name of the certification.</param>
        public void UpdateCertification(int CertificationId, string CertificationCode, int InstituteId, string CertificationName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateCertifications", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CertificationCode", CertificationCode);
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
                    command.Parameters.AddWithValue("@CertificationName", CertificationName);
                    command.Parameters.AddWithValue("@CertificationId", CertificationId);

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
        /// Deletes the Certification.
        /// </summary>
        /// <param name="CertificationId">The CertificationId As  int.</param>
        public void DeleteCertification(int CertificationId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteCertifications", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CertificationId", CertificationId);
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
        /// Gets the Certification.
        /// </summary>
        /// <param name="FulencyId">The FulencyId As int.</param>
        /// <returns></returns>
        public DataTable GetCertification(int CertificationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetCertifications", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CertificationId", CertificationId);
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
        #region EmployeeCertification

        /// <summary>
        /// Adds the employee certification.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="CertificationId">The certification id.</param>
        /// <param name="Date">The date.</param>
        /// <param name="Grade">The grade.</param>
        public void AddEmployeeCertification(long EmployeeId, int CertificationId,DateTime Date,string Grade)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeCertification", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CertificationId", CertificationId);
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@Grade", Grade);
                    SqlParameter employeeCertificationId = new SqlParameter("@EmployeeCertificationId", SqlDbType.Int, 16);
                    employeeCertificationId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeCertificationId);
                    command.ExecuteNonQuery();
                    _employeeCertificationId = Convert.ToInt64(employeeCertificationId.Value);
                    if (_employeeCertificationId < 0)
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
        /// Updates the employee certification.
        /// </summary>
        /// <param name="EmployeeCertificationId">The employee certification id.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="CertificationId">The certification id.</param>
        /// <param name="Date">The date.</param>
        /// <param name="Grade">The grade.</param>
        public void UpdateEmployeeCertification(long EmployeeCertificationId, long EmployeeId, int CertificationId,DateTime Date,string Grade)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeCertification", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeCertificationId", EmployeeCertificationId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CertificationId", CertificationId);
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@Grade", Grade);
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
        /// Gets the employee Certification.
        /// </summary>
        /// <param name="EmployeeCertificationId">The employee Certification id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeCertification(long EmployeeCertificationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeCertification", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeCertificationId", EmployeeCertificationId);
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

        public DataTable GetEmployeeCertificationByEmployeeID(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeCertificationByEmployeeId", _dbConnection))
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
        /// Deletes the employee Certification.
        /// </summary>
        /// <param name="EmployeeCertificationId">The employee Certification id as int.</param>
        public void DeleteEmployeeCertification(long EmployeeCertificationId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeCertification", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeCertificationId", EmployeeCertificationId);
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
        ~CertificationDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
