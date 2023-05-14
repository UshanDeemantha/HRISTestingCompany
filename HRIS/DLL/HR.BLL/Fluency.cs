
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.HR.DAL;
using System.ComponentModel;

namespace HRM.HR.BLL
{
    /// <summary>
    /// Fluency Class(Handles all function relavent to Fluency)
    /// </summary>
    
    [DataObject]
    public class Fluency
    {
        #region Fields
        private FluencyDAL objFluencyDAL;   
        private bool _isError;
        private string _errorMsg;
        private int _fluencyId;
        private string _fluencyCode;
        private string _fluencyName;
        private int _fluencyTypeId;
        private string _fluencyTypeCode;
        private string _fluencyTypeName;
        private long _employeeFluencyId;
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
        /// Gets the fluency id.
        /// </summary>
        /// <value>The fluency id.</value>
        public int FluencyId
        {
            get { return _fluencyId; }            
        }

        public string FluencyCode
        {
            get { return _fluencyCode; }
            set { _fluencyCode = value; }
        }

        public string FluencyName
        {
            get { return _fluencyName; }
            set { _fluencyName = value; }
        }

        public int FluencyTypeId
        {
            get { return _fluencyTypeId; }
        }

        public string FluencyTypeCode
        {
            get { return _fluencyTypeCode; }
            set { _fluencyTypeCode = value; }
        }

        public string FluencyTypeName
        {
            get { return _fluencyTypeName; }
            set { _fluencyTypeName = value; }
        }

        public long EmployeeFluencyId
        {
            get { return _employeeFluencyId; }
        }

        #endregion

        #region Constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="Fluency"/> class.
        /// </summary>
        public Fluency()
        {
           objFluencyDAL=new FluencyDAL();
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objFluencyDAL.IsError;
            _errorMsg = objFluencyDAL.ErrorMsg;
        }
        #endregion

        #region Fluency
        /// <summary>
        /// Adds the fluency.
        /// </summary>
        /// <param name="FluencyCode">FluencyCode As string.</param>
        /// <param name="FluencyTypeId">FluencyTypeId  As int.</param>
        /// <param name="FluencyName">FluencyName As string.</param>
        public void AddFluency(string FluencyCode, int FluencyTypeId, string FluencyName)
        {
            try
            {
                objFluencyDAL.AddFluency(FluencyCode, FluencyTypeId, FluencyName);
                _fluencyId = objFluencyDAL.FluencyId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the fluency.
        /// </summary>
        /// <param name="FluencyId">The FluencyId As int.</param>
        /// <param name="FluencyCode">FluencyCode As string.</param>
        /// <param name="FluencyTypeId">FluencyTypeId  As int.</param>
        /// <param name="FluencyName">FluencyName As string.</param>
        public void UpdateFluency(int FluencyId, string FluencyCode, int FluencyTypeId, string FluencyName)
        {
            try
            {
                objFluencyDAL.UpdateFluency(FluencyId, FluencyCode, FluencyTypeId, FluencyName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        /// <summary>
        /// Deletes the fluency.
        /// </summary>
        /// <param name="FluencyId">The FluencyId as int.</param>
        public void DeleteFluency(int FluencyId)
        {
            try
            {
                objFluencyDAL.DeleteFluency(FluencyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }



        /// <summary>
        /// Gets the fluency.
        /// </summary>
        /// <param name="FluencyId">The FluencyId As int.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetFluency(int FluencyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objFluencyDAL.GetFluency(FluencyId);
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

        #region FluencyTypes
        /// <summary>
        /// Adds Fluency Types
        /// </summary>
        /// <param name="FluencyTypeCode">FluencyTypeCode As string</param>
        /// <param name="FluencyTypeName">FluencyTypeName As string</param>
        public void AddFluencyType(string FluencyTypeCode, string FluencyTypeName)
        {
            try
            {
                objFluencyDAL.AddFluencyType(FluencyTypeCode, FluencyTypeName);
                _fluencyTypeId = objFluencyDAL.FluencyTypeId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                objFluencyDAL.UpdateFluencyType(FluencyTypeId, FluencyTypeCode, FluencyTypeName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Deletes the Fluency Type.
        /// </summary>
        /// <param name="FluencyTypeId">FluencyTypeId As int(0=All, -1=None, Any other=Selected)</param>        
        public DataTable GetFluencyType(int FluencyTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objFluencyDAL.GetFluencyType(FluencyTypeId);
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
        /// Deletes the Fluency Type.
        /// </summary>
        /// <param name="FluencyTypeId">FluencyTypeId As int</param>        
        public void DeleteFluencyType(int FluencyTypeId)
        {
            try
            {
                objFluencyDAL.DeleteFluencyType(FluencyTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        
        #endregion


        #region EmployeeFluency
        /// <summary>
        /// Adds the employee fluency.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="FluencyId">The fluency id as int.</param>
        public void AddEmployeeFluency(long EmployeeId, DataTable tbl)
        {
            try
            {

                foreach (DataRow row in tbl.Rows)
                {

                    objFluencyDAL.AddEmployeeFluency(EmployeeId, Convert.ToInt32(row["LanguageID"].ToString()), row["Reading"].ToString(), row["Writing"].ToString(), row["Speaking"].ToString(), row["Listening"].ToString(), EmployeeId);
                    _employeeFluencyId = objFluencyDAL.EmployeeFluencyId;
                    SetValues();
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }


        }


        public void AddEmployeeFluency2(long EmployeeId, int LanguageID, string Reading, string Writing, string Speaking, string Listening, long UpdateUser)
        {
            try
            {
                objFluencyDAL.AddEmployeeFluency(EmployeeId, LanguageID, Reading, Writing, Speaking, Listening, UpdateUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }


        /// <summary>
        /// Updates the employee fluency.
        /// </summary>
        /// <param name="EmployeeFluencyId">The employee fluency id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="FluencyId">The fluency id as int.</param>
        public void UpdateEmployeeFluency(long LanguId, int LanguageID, long EmployeeId, string Reading, string Writing, string Speaking, string Listening, long UpdateUser)
        {
            try
            {
                objFluencyDAL.UpdateEmployeeFluency(LanguId, LanguageID, EmployeeId, Reading, Writing, Speaking, Listening, UpdateUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }


        public DataTable GetEmployeeFluency(long LanguId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objFluencyDAL.GetEmployeeFluency(LanguId);
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
        public DataTable GetEmployeeFluencyByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objFluencyDAL.GetEmployeeFluencyByEmployeeId(EmployeeId);
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
        /// Deletes the employee fluency.
        /// </summary>
        /// <param name="EmployeeFluencyId">The employee fluency id as int.</param>
        public void DeleteEmployeeFluency(long EmployeeFluencyId)
        {
            try
            {
                objFluencyDAL.DeleteEmployeeFluency(EmployeeFluencyId);
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
        ~Fluency()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion

    }
}
