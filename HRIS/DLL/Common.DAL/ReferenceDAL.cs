using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HRM.Common.DAL
{
    public class ReferenceDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
       
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private int _employmentTypeId;
        private string _employmentTypeCode;
        private string _employmentTypeName;
        private string _remark;
        private int _jobCategoryId;
        private string _jobCategoryCode;
        private string _jobCategoryName;
        private int _branchId;
        private int _jobGradeId;
        private string _jobGradeCode;
        private string _jobGrade;
        private int _jobSpecificationId;
        private int _nationalityId;
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
        /// Gets the Error Number.
        /// </summary>
        /// <value>Occurred Error as a Numeric Representation</value>
        public int ErrorNo
        {
            get { return _errorNo; }
        }

        public int EmploymentTypeId
        {
            get { return _employmentTypeId; }
            set { _employmentTypeId = value; }
        }
        public string EmploymentTypeCode
        {
            get { return _employmentTypeCode; }
            set { _employmentTypeCode = value; }
        }
        public string EmploymentTypeName
        {
            get { return _employmentTypeName; }
            set { _employmentTypeName = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public int NationalityId
        {
            get { return _nationalityId; }
        }

        public int Branch
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        public int JobCategoryId
        {
            get { return _jobCategoryId; }
            set { _jobCategoryId = value; }
        }
        public string JobCategoryCode
        {
            get { return _jobCategoryCode; }
            set { _jobCategoryCode = value; }
        }
        public string JobCategoryName
        {
            get { return _jobCategoryName; }
            set { _jobCategoryName = value; }
        }
        public int JobGradeId
        {
            get { return _jobGradeId; }
            set { _jobGradeId = value; }
        }
        public string JobGradeCode
        {
            get { return _jobGradeCode; }
            set { _jobGradeCode = value; }
        }
        public string JobGrade
        {
            get { return _jobGrade; }
            set { _jobGrade = value; }
        }

        public int JobSpecificationId
        {
            get { return _jobSpecificationId; } 
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
        public ReferenceDAL()
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
            _errorNo = int.MinValue;
        }

        private void SetError(SqlException Ex)
        {           
            _isError = true;
            _errorMsg = Ex.Message;          
            _errorNo = Ex.Number;
        }
        #endregion
        
        #region External
        #region Job Category
        /// <summary>
        /// Add  the Job Category.
        /// <param name="JobCategoryId">The JobCategory ID.</param>
        /// <param name="JobCategoryCode">The JobCategory Code.</param>
        /// <param name="JobCategoryName">The JobCategory Name.</param>
        /// </summary>
        public void AddJobCategory(string JobCategoryCode, string JobCategoryName, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddJobCategory", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobCategoryCode", JobCategoryCode);
                    command.Parameters.AddWithValue("@JobCategoryName", JobCategoryName);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    SqlParameter categoryId = new SqlParameter("@JobCategoryId", SqlDbType.Int, 8);
                    categoryId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(categoryId);
                    command.ExecuteNonQuery();
                    if(categoryId.Value != System.DBNull.Value)
                    {
                        _jobCategoryId = Convert.ToInt32(categoryId.Value);
                    }
                    if (_jobCategoryId <= 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
                    }
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

        /// <summary>
        /// Update  the Job Category.
        /// <param name="JobCategoryId">The JobCategory ID.</param>
        /// <param name="JobCategoryCode">The JobCategory Code.</param>
        /// <param name="JobCategoryName">The JobCategory Name.</param>
        /// </summary>
        public void UpdateJobCategory(int JobCategoryId, string JobCategoryCode, string JobCategoryName, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateJobCategory", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobCategoryId", JobCategoryId);
                    command.Parameters.AddWithValue("@JobCategoryCode", JobCategoryCode);
                    command.Parameters.AddWithValue("@JobCategoryName", JobCategoryName);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    SqlParameter error = new SqlParameter("@OutPut", SqlDbType.VarChar,100);
                    error.Direction = ParameterDirection.Output;
                    command.Parameters.Add(error);
                    command.ExecuteNonQuery();
                    string errorMsg = "";
                    if (error.Value != System.DBNull.Value)
                    {
                        errorMsg = error.Value.ToString();
                    }
                    if (errorMsg != "")
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
                    }
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

        /// <summary>
        /// Deletes the job category.
        /// </summary>
        /// <param name="JobCategoryId">The job category id as int.</param>
        public void DeleteJobCategory(int JobCategoryId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteJobCategory", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobCategoryId", JobCategoryId);
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

        /// <summary>
        /// Gets the Job Category.
        /// </summary>
        /// <param name="JobCategoryId">JobCategory id As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetJobCategory(int JobCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetJobCategory", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobCategoryId", JobCategoryId);
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

        public DataTable GetJobCategoryCompanyWise(int CompanyID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetJobCategoryCompanyWise", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyID);
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

        #region Job Grade
        /// <summary>
        /// Add  the Job Grade.
        /// <param name="JobGradeId">The JobGrade ID.</param>
        /// <param name="JobGradeCode">The JobGrade Code.</param>
        /// <param name="JobGrade">The JobGrade.</param>
        /// </summary>
        public void AddJobGrade(string JobGradeCode, string JobGrade)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddJobGrade", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobGradeCode", JobGradeCode);
                    command.Parameters.AddWithValue("@JobGrade", JobGrade);
                    SqlParameter gradeId = new SqlParameter("@JobGradeId", SqlDbType.Int, 8);
                    gradeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(gradeId);
                    command.ExecuteNonQuery();
                    _jobCategoryId = Convert.ToInt32(gradeId.Value);
                    if (_jobCategoryId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
                    }
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

        /// <summary>
        /// Update  the Job Grade.
        /// <param name="JobGradeId">The JobGrade ID.</param>
        /// <param name="JobGradeCode">The JobGrade Code.</param>
        /// <param name="JobGrade">The JobGrade.</param>
        /// </summary>
        public void UpdateJobGrade(int JobGradeId, string JobGradeCode, string JobGrade)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateJobGrade", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobGradeId", JobGradeId);
                    command.Parameters.AddWithValue("@JobGradeCode", JobGradeCode);
                    command.Parameters.AddWithValue("@JobGrade", JobGrade);
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

        /// <summary>
        /// Deletes the job grade.
        /// </summary>
        /// <param name="JobGradeId">The job grade id as int.</param>
        public void DeleteJobGrade(int JobGradeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteJobGrade", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobGradeId", JobGradeId);
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

        /// <summary>
        /// Gets the Job Grade.
        /// </summary>
        /// <param name="JobGradeId">JobGrade id As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetJobGrade(int JobGradeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetJobGrade", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@JobGradeId", JobGradeId);
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

        #region Job Specification
        /// <summary>
        /// Adds the job specification.
        /// </summary>
        /// <param name="DesignationId">The designation id.</param>
        /// <param name="Responsibilities">The responsibilities.</param>
        /// <param name="Benefits">The benefits.</param>
        /// <param name="Remarks">The remarks.</param>
        public void AddJobSpecification(int DesignationId, string Responsibilities, string Benefits, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddJobSpecification", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
                    command.Parameters.AddWithValue("@Responsibilities", Responsibilities);
                    command.Parameters.AddWithValue("@Benifites", Benefits);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
                    SqlParameter jobSpecificationId = new SqlParameter("@JobSpecificationId", SqlDbType.Int, 8);
                    jobSpecificationId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(jobSpecificationId);
                    command.ExecuteNonQuery();
                    _jobSpecificationId = Convert.ToInt32(jobSpecificationId.Value);
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

        /// <summary>
        /// Updates the job specification.
        /// </summary>
        /// <param name="JobSpecificationId">The job specification id.</param>
        /// <param name="DesignationId">The designation id.</param>
        /// <param name="Responsibilities">The responsibilities.</param>
        /// <param name="Benefits">The benefits.</param>
        /// <param name="Remarks">The remarks.</param>
        public void UpdateJobSpecification(int JobSpecificationId, int DesignationId, string Responsibilities, string Benefits, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateJobSpecification", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;   
                    command.Parameters.AddWithValue("@JobSpecificationId", JobSpecificationId);
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
                    command.Parameters.AddWithValue("@Responsibilities", Responsibilities);
                    command.Parameters.AddWithValue("@Benifites", Benefits);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
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

        /// <summary>
        /// Deletes the job specification.
        /// </summary>
        /// <param name="JobSpecificationId">The job specification id.</param>
        public void DeleteJobSpecification(int JobSpecificationId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteJobSpecification", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@JobSpecificationId", JobSpecificationId);
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

        public DataTable GetJoSpecification(int JobSpecificationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetJobSpecification", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@JobSpecificationId", JobSpecificationId);
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

        #region Nationality
        /// <summary>
        /// Adds the nationality.
        /// </summary>
        /// <param name="Country">The country.</param>
        /// <param name="NationalityName">Name of the nationality.</param>
        public void AddNationality(string Country, string NationalityName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddNationality", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;   
                    command.Parameters.AddWithValue("@Country", Country);
                    command.Parameters.AddWithValue("@NationalityName", NationalityName);
                    SqlParameter nationalityId = new SqlParameter("@NationalityId", SqlDbType.Int, 8);
                    nationalityId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(nationalityId);
                    command.ExecuteNonQuery();
                    _nationalityId = Convert.ToInt32(nationalityId.Value);
                    if (_nationalityId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
                    }
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
        
        /// <summary>
        /// Updates the nationality.
        /// </summary>
        /// <param name="NationalityId">The nationality id.</param>
        /// <param name="Country">The country.</param>
        /// <param name="NationalityName">Name of the nationality.</param>
        public void UpdateNationality(int NationalityId, string Country, string NationalityName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateNationality", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("@NationalityId", NationalityId);
                    command.Parameters.AddWithValue("@Country", Country);
                    command.Parameters.AddWithValue("@NationalityName", NationalityName);
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
        /// Gets the nationality.
        /// </summary>
        /// <param name="NationalityId">The nationality id.</param>
        /// <returns></returns>
        public DataTable GetNationality(int NationalityId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetNationality", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@NationalityId", NationalityId);
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
        /// Deletes the nationality.
        /// </summary>
        /// <param name="NationalityId">The nationality id.</param>
        public void DeleteNationality(int NationalityId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_DeleteNationality", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalityId", NationalityId);
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
        #endregion

        /// <summary>
        /// Gets the Branch.
        /// </summary>
        /// <param name="JobCategoryId">branch id As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetBranch(int BranchId, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetBranch", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BranchId", BranchId);
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


        #endregion             
        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="StudentDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~ReferenceDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

