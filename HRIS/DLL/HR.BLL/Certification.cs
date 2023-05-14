
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.HR.DAL;
using System.ComponentModel;

namespace HRM.HR.BLL
{
    [DataObject]
    public class Certification:Institute
    {  
        #region Fields
        private CertificationDAL objCertificationDAL;   
        private bool _isError;
        private string _errorMsg;
        private int _certificationId;
       
      
       
        private long _employeeCertificationId;
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
        /// Gets the Certification id.
        /// </summary>
        /// <value>The Certification id.</value>
        public int CertificationId
        {
            get { return _certificationId; }            
        }

       

      

      
       

        public long EmployeeCertificationId
        {
            get { return _employeeCertificationId; }
        }

        #endregion

        #region Constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="Certification"/> class.
        /// </summary>
        public Certification()
        {
           objCertificationDAL=new CertificationDAL();
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objCertificationDAL.IsError;
            _errorMsg = objCertificationDAL.ErrorMsg;
        }
        #endregion

        #region Certification
        /// <summary>
        /// Adds the Certification.
        /// </summary>
        /// <param name="CertificationCode">CertificationCode As string.</param>
        /// <param name="CertificationTypeId">CertificationTypeId  As int.</param>
        /// <param name="CertificationName">CertificationName As string.</param>
        public void AddCertification(string CertificationCode, int InstituteId, string CertificationName)
        {
            try
            {
                objCertificationDAL.AddCertification(CertificationCode, InstituteId, CertificationName);
                _certificationId = objCertificationDAL.CertificationId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the Certification.
        /// </summary>
        /// <param name="CertificationId">The CertificationId As int.</param>
        /// <param name="CertificationCode">CertificationCode As string.</param>
        /// <param name="CertificationTypeId">CertificationTypeId  As int.</param>
        /// <param name="CertificationName">CertificationName As string.</param>
        public void UpdateCertification(int CertificationId, string CertificationCode, int InstituteId, string CertificationName)
        {
            try
            {
                objCertificationDAL.UpdateCertification(CertificationId, CertificationCode, InstituteId, CertificationName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        /// <summary>
        /// Deletes the Certification.
        /// </summary>
        /// <param name="CertificationId">The CertificationId as int.</param>
        public void DeleteCertification(int CertificationId)
        {
            try
            {
                objCertificationDAL.DeleteCertification(CertificationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }



        /// <summary>
        /// Gets the Certification.
        /// </summary>
        /// <param name="CertificationId">The CertificationId As int.</param>
        /// <returns></returns>
        public DataTable GetCertification(int CertificationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCertificationDAL.GetCertification(CertificationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        
        #endregion
         #region EmployeeCertification
        /// <summary>
        /// Adds the employee Certification.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="CertificationId">The Certification id as int.</param>
        public void AddEmployeeCertification(long EmployeeID, DataTable tbl)
        {
            try
            {
                foreach (DataRow row in tbl.Rows)
                {

                    objCertificationDAL.AddEmployeeCertification(EmployeeID, Convert.ToInt32(row["CertificationID"].ToString()), Convert.ToDateTime(row["Date"].ToString()), row["Grade"].ToString());
                    _employeeCertificationId = objCertificationDAL.EmployeeCertificationId;
                    SetValues();
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }


        }

        public void AddEmployeeCertification2(long EmployeeId, int CertificationId, DateTime Date, string Grade)
        {
            try
            {
                objCertificationDAL.AddEmployeeCertification(EmployeeId, CertificationId, Date, Grade);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }


        /// <summary>
        /// Updates the employee Certification.
        /// </summary>
        /// <param name="EmployeeCertificationId">The employee Certification id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="CertificationId">The Certification id as int.</param>
        public void UpdateEmployeeCertification(long EmployeeCertificationId, long EmployeeId, int CertificationId,DateTime Date,string Grade)
        {
            try
            {
                objCertificationDAL.UpdateEmployeeCertification(EmployeeCertificationId, EmployeeId, CertificationId,Date,Grade);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                dtTable = objCertificationDAL.GetEmployeeCertification(EmployeeCertificationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeCertificationByEmployeeID(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCertificationDAL.GetEmployeeCertificationByEmployeeID(EmployeeId);
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
        /// Deletes the employee Certification.
        /// </summary>
        /// <param name="EmployeeCertificationId">The employee Certification id as int.</param>
        public void DeleteEmployeeCertification(long EmployeeCertificationId)
        {
            try
            {
                objCertificationDAL.DeleteEmployeeCertification(EmployeeCertificationId);
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
        ~Certification()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
