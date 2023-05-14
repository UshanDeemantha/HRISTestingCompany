/// <summary>
/// Solution Name        : HRM
/// Project Name         : Common.BLL
/// Author               : ushan deemantha
/// Class Description    : Reference
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.Common.DAL;
using System.ComponentModel;
using System.Configuration;

namespace HRM.Common.BLL
{
    [DataObject]
    public class Reference
    {        
        #region Fields
        HRM.Common.DAL.ReferenceDAL objReferenceDAL;
        private DataTable _employmentTypes;
        private DataTable _jobCategories;
        private DataTable _Branch;
        private DataTable _jobGrades;
        private DataTable _jobSpecifiction;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private int _nationalityId = 0;
        private int _employmentTypeId;
        private string _employmentTypeCode;
        private string _employmentTypeName;
        private string _remark;
        private int _jobCategoryId;
        private int _branchId;
        private string _jobCategoryCode;
        private string _jobCategoryName;
        private int _jobGradeId;
        private string _jobGradeCode;
        private string _jobGrade;
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

        public int ErrorNo
        {
            get {return _errorNo; }
        }

        public int NationalityId
        {
            get { return _nationalityId; }
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

        public int Branch
        {
            get { return _branchId; }
            set { _branchId = value; }
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
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Reference()
        {
           objReferenceDAL=new ReferenceDAL();
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objReferenceDAL.IsError;
            _errorMsg = objReferenceDAL.ErrorMsg;
            _errorNo = objReferenceDAL.ErrorNo;
            if (_isError == true)
            {
                switch (_errorNo)
                {
                    case 2601: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    case 2627: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    case 547: _errorMsg = ConfigurationManager.AppSettings["CannotDelete"].ToString();
                        break;
                    default:
                        break;
                }
            }
        }    
        #endregion  

        #region External
        #region Job Category
        /// <summary>
        /// Adds the job category.
        /// </summary>
        /// <param name="JobCategoryCode">The job category code.</param>
        /// <param name="JobCategoryName">Name of the job category.</param>
        public void AddJobCategory(string JobCategoryCode, string JobCategoryName, int CompanyId)
        {
            try
            {
                objReferenceDAL.AddJobCategory(JobCategoryCode, JobCategoryName, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        
        /// <summary>
        /// Updates the job category.
        /// </summary>
        /// <param name="JobCategoryId">The job category id.</param>
        /// <param name="JobCategoryCode">The job category code.</param>
        /// <param name="JobCategoryName">Name of the job category.</param>
        public void UpdateJobCategory(int JobCategoryId, string JobCategoryCode, string JobCategoryName, int CompanyId)
        {
            try
            {
                objReferenceDAL.UpdateJobCategory(JobCategoryId, JobCategoryCode, JobCategoryName, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the job category.
        /// </summary>
        /// <param name="JobCategoryId">The job category id.</param>
        /// <returns></returns>
        /// 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetJobCategory(int JobCategoryId)
        {
            try
            {
                _jobCategories = objReferenceDAL.GetJobCategory(JobCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _jobCategories;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetJobCategoryCompanyWise(int CompanyID)
        {
            try
            {
                _jobCategories = objReferenceDAL.GetJobCategoryCompanyWise(CompanyID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _jobCategories;
        }

        /// <summary>
        /// Deletes the job category.
        /// </summary>
        /// <param name="JobCategoryId">The job category id.</param>
        public void DeleteJobCategory(int JobCategoryId)
        {
            try
            {
                objReferenceDAL.DeleteJobCategory(JobCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        #endregion

        #region Job Grade
        /// <summary>
        /// Adds the job grade.
        /// </summary>
        /// <param name="JobGradeCode">The job grade code.</param>
        /// <param name="JobGrade">The job grade.</param>
        public void AddJobGrade(string JobGradeCode, string JobGrade)
        {
            try
            {
                objReferenceDAL.AddJobGrade(JobGradeCode, JobGrade);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the job grade.
        /// </summary>
        /// <param name="JobGradeId">The job grade id.</param>
        /// <param name="JobGradeCode">The job grade code.</param>
        /// <param name="JobGrade">The job grade.</param>
        public void UpdateJobGrade(int JobGradeId, string JobGradeCode, string JobGrade)
        {
            try
            {
                objReferenceDAL.UpdateJobGrade(JobGradeId, JobGradeCode, JobGrade);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the job grade.
        /// </summary>
        /// <param name="JobGradeId">The job grade id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetJobGrade(int JobGradeId)
        {
            try
            {
                _jobGrades = objReferenceDAL.GetJobGrade(JobGradeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _jobGrades;
        }

        /// <summary>
        /// Deletes the job grade.
        /// </summary>
        /// <param name="JobGradeId">The job grade id.</param>
        public void DeleteJobGrade(int JobGradeId)
        {
            try
            {
                objReferenceDAL.DeleteJobGrade(JobGradeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion

        #region Job Specifiction

        /// <summary>
        /// Adds the job specifiction.
        /// </summary>
        /// <param name="DesignationId">The designation id.</param>
        /// <param name="Responsibilities">The responsibilities.</param>
        /// <param name="Benefits">The benefits.</param>
        /// <param name="Remarks">The remarks.</param>
        public void AddJobSpecifiction(int DesignationId, string Responsibilities, string Benefits, string Remarks)
        {
            try
            {
                objReferenceDAL.AddJobSpecification(DesignationId, Responsibilities, Benefits, Remarks);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the job specifiction.
        /// </summary>
        /// <param name="JobSpecifictionId">The job specifiction id.</param>
        /// <param name="DesignationId">The designation id.</param>
        /// <param name="Responsibilities">The responsibilities.</param>
        /// <param name="Benefits">The benefits.</param>
        /// <param name="Remarks">The remarks.</param>
        public void UpdateJobSpecifiction(int JobSpecifictionId, int DesignationId, string Responsibilities, string Benefits, string Remarks)
        {
            try
            {
                objReferenceDAL.UpdateJobSpecification(JobSpecifictionId, DesignationId, Responsibilities, Benefits, Remarks);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the job specifiction.
        /// </summary>
        /// <param name="JobSpecifictionId">The job specifiction id.</param>
        /// <returns></returns>
        public DataTable GetJobSpecifiction(int JobSpecifictionId)
        {
            try
            {
                _jobSpecifiction = objReferenceDAL.GetJoSpecification(JobSpecifictionId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _jobSpecifiction;
        }


        public void DeleteJobSpecifiction(int JobSpecifictionId)
        {
            try
            {
                objReferenceDAL.DeleteJobSpecification(JobSpecifictionId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
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
                objReferenceDAL.AddNationality(Country, NationalityName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                objReferenceDAL.UpdateNationality(NationalityId, Country, NationalityName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the nationality.
        /// </summary>
        /// <param name="NationalityId">The nationality id.</param>
        /// <returns></returns>
        public DataTable GetNationality(int NationalityId)
        {
            DataTable dtNationalitys = new DataTable();
            try
            {
                dtNationalitys = objReferenceDAL.GetNationality(NationalityId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtNationalitys;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetNationalityList(int NationalityId)
        {
            DataTable dtNationalitys = new DataTable();
            try
            {
                dtNationalitys = objReferenceDAL.GetNationality(NationalityId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtNationalitys;
        }

        /// <summary>
        /// Deletes the nationality.
        /// </summary>
        /// <param name="NationalityId">The nationality id.</param>
        public void DeleteNationality(int NationalityId)
        {
            try
            {
                objReferenceDAL.DeleteNationality(NationalityId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        } 
        #endregion


        /// <summary>
        /// Gets the Branch.
        /// </summary>
        /// <param name="JobCategoryId">The company branchId id.</param>
        /// <returns></returns>
        /// 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetBranch(int BranchId, int CompanyId)
        {
            try
            {
                _Branch = objReferenceDAL.GetBranch(BranchId, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _Branch;
        }


        #endregion
        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~Reference()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}