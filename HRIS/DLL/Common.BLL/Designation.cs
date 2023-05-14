/// <summary>
/// Solution Name        : HRM
/// Project Name          : HR.Web
/// Author                     : ushan deemantha
/// Class Description    : Designation
/// </summary>
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using HRM.Common.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.Common.BLL
{
    [DataObject]
    public class Designation
    {        
        #region Fields
        DesignationDAL objDesignationDAL;
        private DataTable _Designations;
        private bool _isError;
        private string _errorMsg;
        private int _designationId;
        private int _errorNo;
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
        
        public int DesignationId
        {
            get { return _designationId; }
            set { _designationId = value; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
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
        /// Initializes a new instance of the <see cref="Designation"/> class.
        /// </summary>
        public Designation()
        {
           objDesignationDAL=new DesignationDAL();
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objDesignationDAL.IsError;
            _errorMsg = objDesignationDAL.ErrorMsg;
            _errorNo = objDesignationDAL.ErrorNo;
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
        #region Designation
        /// <summary>
        /// Adds the Designation.
        /// </summary>
        /// <param name="DesignationCode">The Designation code.</param>
        /// <param name="DesignationName">Name of the Designation.</param>
        ///         [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetParentOrganizationTypes(int CompanyId, int DesignationId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objDesignationDAL.GetParentDesignationTypes(CompanyId, DesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }
        public DataTable GetParentOrganizationTypes( int DesignationId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objDesignationDAL.GetParentDesignationTypes( DesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        public void AddDesignation(string DesignationCode, string DesignationName)
        {
            try
            {
                objDesignationDAL.AddDesignation(DesignationCode, DesignationName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetDesignationTypesExcel(int DesignationId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objDesignationDAL.GetDesignationTypesExcel(DesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }
        /// <summary>
        /// Updates the Designation.
        /// </summary>
        /// <param name="DesignationId">The Designation id.</param>
        /// <param name="DesignationCode">The Designation code.</param>
        /// <param name="DesignationName">Name of the Designation.</param>
        public void UpdateDesignation(int DesignationId, string DesignationCode, string DesignationName)
        {
            try
            {
                objDesignationDAL.UpdateDesignation(DesignationId, DesignationCode, DesignationName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
       
        /// <summary>
        /// Gets the Designation.
        /// </summary>
        /// <param name="DesignationId">The Designation id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetDesignation(int DesignationId)
        {
            try
            {
                _Designations = objDesignationDAL.GetDesignation(DesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _Designations;
        }

        /// <summary>
        /// Deletes the Designation.
        /// </summary>
        /// <param name="DesignationId">The Designation id.</param>
        public void DeleteDesignation(int DesignationId)
        {
            try
            {
                objDesignationDAL.DeleteDesignation(DesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }

        }
        #endregion

        #region Designation Structure



        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetDesignationTypesExcel(int CompanyId, int DesignationId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objDesignationDAL.GetDesignationTypesExcel(CompanyId, DesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }





        /// <summary>
        /// Adds the type of the Designation.
        /// </summary>
        public void AddDesignationStructure(int ParentID, int TreeIndex, int DesignationId, int CompanyId)
        {
            try
            {
                objDesignationDAL.AddDesignationStructure(ParentID, TreeIndex, DesignationId, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the type of the Designation.
        /// </summary>
        public void UpdateDesignationStucture(int DesignationStructurelId, int ParentID, int TreeIndex, int DesignationId)
        {
            try
            {
                objDesignationDAL.UpdateDesignationStucture(DesignationStructurelId, ParentID, TreeIndex, DesignationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the Designation .
        /// </summary>
        /// <param name="DesignationId">The Designation id.</param>
        /// <returns></returns>

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetDesignationStructure(int DesignationStructureId, int CompanyId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objDesignationDAL.GetDesignationStucture(DesignationStructureId, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable GetRowsWhereParentIDEquals(int DesignationStructureId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objDesignationDAL.GetRowsWhereParentIDEquals(DesignationStructureId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        /// <summary>
        /// Delete the type of the Designation.
        /// </summary>
        public void DeleteDesignationStucture(int DesignationStructurelId)
        {
            try
            {
                objDesignationDAL.DeleteDesignationStucture(DesignationStructurelId);
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
        #endregion
        
        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~Designation()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}