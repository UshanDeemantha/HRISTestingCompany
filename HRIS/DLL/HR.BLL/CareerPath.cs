using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.HR.DAL;
using System.Data;

namespace HRM.HR.BLL
{ 
    public class CareerPath
    {

        #region Fields   
        CareerPathDAL objCareerPathDAL;
        DataTable  _careerPaths;
        private bool _isError;
        private string _errorMsg;
        private int _careerPathId;
        private string _careerPathCode;
        private string _existingDesignation;
        private string _nextDesignation;
        private int _careerPathDetailId;
        private int _skillId;
        private int _flunceyId;
        private int _qulificationId;
        private int _experience;
        private string _remarks;
        private long _employeeCareerPathId;
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

        public int CareerPathId
        {
            get { return _careerPathId; }
            set { _careerPathId = value; }
        }
        public string CareerPathCode
        {
            get { return _careerPathCode; }
            set { _careerPathCode = value; }
        }
        public string ExistingDesignation
        {
            get { return _existingDesignation; }
            set { _existingDesignation = value; }
        }
        public string NextDesignation
        {
            get { return _nextDesignation; }
            set { _nextDesignation = value; }
        }
        public int CareerPathDetailId
        {
            get { return _careerPathDetailId; }
            set { _careerPathDetailId = value; }
        }
        public int SkillId
        {
            get { return _skillId; }
            set { _skillId = value; }
        }
        public int FlunceyId
        {
            get { return _flunceyId; }
            set { _flunceyId = value; }
        }
        public int QulificationId
        {
            get { return _qulificationId; }
            set { _qulificationId = value; }
        }
        public int Experience
        {
            get { return _experience; }
            set { _experience = value; }
        }
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }







        #endregion

        #region Constructor       
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public CareerPath()
        {
           objCareerPathDAL=new CareerPathDAL();
        } 
        #endregion

        #region Methods
      
        private void SetValues()
        {
            _isError=objCareerPathDAL.IsError;
            _errorMsg=objCareerPathDAL.ErrorMsg;
        }

        public void AddCareerPath( string CareerPathCode, int ExcistingDesignationId,int NextDesignationId)
        {
            try
            {
                objCareerPathDAL.AddCareerPath( CareerPathCode, ExcistingDesignationId,NextDesignationId);
               SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }
        public void UpdateCareerPath(int CareerPathId, string CareerPathCode, int ExcistingDesignationId, int NextDesignationId)
        {
            try
            {
                objCareerPathDAL.UpdateCareerPath(CareerPathId, CareerPathCode, ExcistingDesignationId, NextDesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public DataTable GetCareerPath(int CareerPathId)
        {
            try
            {
                _careerPaths = objCareerPathDAL.GetCareerPath(CareerPathId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _careerPaths;
        }

        public void DeleteCareerPath(int CareerPathId)
        {
            try
            {
                objCareerPathDAL.DeleteCareerPath(CareerPathId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            
        }

        public void AddCareerPathDetail( int CareerPathId, int FluencyId, int SkillId, int QulificationId, int Experiance, string Remarks)
        {
            try
            {
                objCareerPathDAL.AddCareerPathDetail(  CareerPathId, FluencyId, SkillId, QulificationId,Experiance, Remarks);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void UpdateCareerPathDetail(int CareerPathDetailId, int CareerPathId, int FluencyId, int SkillId,int QulificationId,int Experiance,string Remarks)
        {
            try
            {
                objCareerPathDAL.UpdateCareerPathDetail( CareerPathDetailId,  CareerPathId, FluencyId, SkillId, QulificationId,Experiance, Remarks);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public DataTable GetCareerPathDetail(int CareerPathDetailId)
        {
            try
            {
                _careerPaths = objCareerPathDAL.GetCareerPathDetail(CareerPathDetailId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _careerPaths;
        }
        public void DeleteCareerPathDetail(int CareerPathDetailId)
        {
            try
            {
                objCareerPathDAL.DeleteCareerPathDetail(CareerPathDetailId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }


        }

        #region EmployeeCareerPath
        /// <summary>
        /// Adds the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="CareerPathId">The CareerPath id as int.</param>
        public void AddEmployeeCareerPath(long EmployeeId, int CareerPathId)
        {
            try
            {
                objCareerPathDAL.AddEmployeeCareerPath(EmployeeId, CareerPathId);
                _employeeCareerPathId = objCareerPathDAL.EmployeeCareerPathId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }


        }

        /// <summary>
        /// Updates the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeCareerPathId">The employee CareerPath id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="CareerPathId">The CareerPath id as int.</param>
        public void UpdateEmployeeCareerPath(long EmployeeCareerPathId, long EmployeeId, int CareerPathId)
        {
            try
            {
                objCareerPathDAL.UpdateEmployeeCareerPath(EmployeeCareerPathId, EmployeeId, CareerPathId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        /// <summary>
        /// Gets the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeCareerPathId">The employee CareerPath id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeCareerPath(long EmployeeCareerPathId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCareerPathDAL.GetEmployeeCareerPath(EmployeeCareerPathId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }


        /// <summary>
        /// Deletes the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeCareerPathId">The employee CareerPath id as int.</param>
        public void DeleteEmployeeCareerPath(long EmployeeCareerPathId)
        {
            try
            {
                objCareerPathDAL.DeleteEmployeeCareerPath(EmployeeCareerPathId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion


        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~CareerPath()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
