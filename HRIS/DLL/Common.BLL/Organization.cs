using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using HRM.Common.DAL;
using System.ComponentModel;
using System.Configuration;

namespace HRM.Common.BLL
{
    [DataObject]
    public class Organization
    {
        #region Fields
        OrganizationDAL objOrganizationDAL;
        private DataTable _organizationLevels;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private string _result;
        private bool _isSuccess;

        private int _organizationLevelId;
        private string _organizationLevel;
        private int _organizationIndex;

        private int _organizationTypeId;
        private string _organizationTypeCode;
        private string _organizationTypeName;
        private int _organizationalIndex;
        private string _address;
        private string _contactNo;
        private string _email;

        private int _organizationStructureId;
        private int _parentId;
        private int _organizationStructureLevelIndex;
        #endregion

        #region Propreties
        public int OrganizationStructureId
        {
            get {return _organizationStructureId; }
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public bool IsError
        {
            get { return _isError; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        public string Result
        {
            get { return _result; }
        } 

        #region Orfanization Leve
        public int  OrganizationLevelId
        {
            get { return _organizationLevelId; }
        }

        public string OrganizationLevel
        {
            get { return _organizationLevel; }
            set { _organizationLevel = value; }
        }

        public int OrganizationIndex 
        {
            get { return _organizationIndex; }
            set { _organizationIndex = value; }
        }

        #endregion

        #region Organization Type
        public int OrganizationTypeId
        {
            get { return _organizationTypeId; }
        }

        public int OrganizationalTypeLevelID
        {
            get { return _organizationLevelId; }
            set { _organizationLevelId = value; }
        }

        public string  OrganizationTypeCode 
        {
            get { return _organizationTypeCode; }
            set { _organizationTypeCode = value; }
        }

        public string  OrganizationTypeName 
        {
            get { return _organizationTypeName; }
            set { _organizationTypeName = value; }
        }

        public int OrganizationalIndex 
        {
            get { return _organizationalIndex; }
            set { _organizationalIndex = value; }
        }

        public string Address 
        {
            get { return _address; }
            set { _address = value; }
        }

        public string  ContactNo  
        {
            get { return _contactNo; }
            set { _contactNo = value; }
        }

        public string Email 
        {
            get { return _email; }
            set { _email = value; }
        }

        #endregion

        #region Organizazion Structure

        public int OrganizationStructureLevelIndex 
        {
            get { return _organizationStructureLevelIndex; }
            set { _organizationStructureLevelIndex = value; }
        }

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public int OrganizationStuctureTypeID
        {
            get { return _organizationTypeId; }
            set { _organizationTypeId = value; }
        }

        #endregion

        #endregion

        #region Constractor
        public Organization()
        {
            objOrganizationDAL = new OrganizationDAL();
        }

        public Organization(int OrganizationLevelId)
        {
            objOrganizationDAL = new OrganizationDAL();
            _organizationLevelId = OrganizationLevelId;
        } 
        #endregion
        
        #region Methods
        #region Internal
        /// <summary>
        /// Sets the default values.
        /// </summary>
        private void SetDefaultValues()
        {
            _isError = objOrganizationDAL.IsError;
            _errorMsg = objOrganizationDAL.ErrorMsg;
            _errorNo = objOrganizationDAL.ErrorNo;
            _isSuccess = objOrganizationDAL.IsSuccess;
            _result = objOrganizationDAL.Result;
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
        #region Organization levels
        /// <summary>
        /// Adds the organization level.
        /// </summary>
        public void AddOrganizationLevel(int CompanyId, string OrganizationalLevel, int OrganizationalIndex)
        {
            try
            {
                objOrganizationDAL.AddOrganizationLevel(CompanyId, OrganizationalLevel, OrganizationalIndex);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetParentOrganizationTypes(int CompanyId, int OrganizationTypeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetParentOrganizationTypes(CompanyId, OrganizationTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetParentOrganizationTypesforsearch(int CompanyId, int OrgstrucId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetParentOrganizationTypesforsearch(CompanyId, OrgstrucId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }
        public DataTable GetOrganizationFlow(int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objOrganizationDAL.GetOrganizationFlow(OrgStructureID);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        /// <summary>
        /// Updates the organization level.
        /// </summary>
        public void UpdateOrganizationLevel(int OrganizationalLevelId, string OrganizationalLevel, int OrganizationalIndex)
        {
            try
            {
                objOrganizationDAL.UpdateOrganizationLevel(OrganizationalLevelId, OrganizationalLevel, OrganizationalIndex);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Deletes the organization level.
        /// </summary>
        public void DeleteOrganizationLevel(int OrganizationalLevelId, int CompanyId)
        {
            try
            {
                objOrganizationDAL.DeleteOrganizationLevel(OrganizationalLevelId, CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the organization level.
        /// </summary>
        /// <param name="OrganizationLevelId">The organization level id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetOrganizationLevel(int CompanyId, int OrganizationLevelId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationLevel(CompanyId, OrganizationLevelId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public int GetOrganizationChildLevel(int OrgStructureID)
        {
            int childLevelId = -100;
            try
            {
                childLevelId = objOrganizationDAL.GetOrganizationChildLevel(OrgStructureID);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return childLevelId;
        }
        #endregion

        #region Organization Types
        /// <summary>
        /// Adds the type of the organization.
        /// </summary>
        public void AddOrganizationType(string OrganizationTypeCode, string OrganizationTypeName, int OrganizationalLevelID, int OrganizationalIndex, string Address, string ContactNo, string Fax, string Email,int companyId)
        {
            try
            {
                objOrganizationDAL.AddOrganizationTypes(OrganizationTypeCode, OrganizationTypeName, OrganizationalLevelID, OrganizationalIndex, Address, ContactNo, Fax, Email, companyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the type of the organization.
        /// </summary>
        public void UpdateOrganizationType(int OrganizationTypelId, string OrganizationTypeCode, string OrganizationTypeName, int OrganizationalLevelID, int OrganizationalIndex, string Address, string ContactNo, string Fax, string Email)
        {
            try
            {
                objOrganizationDAL.UpdateOrganizationTypes(OrganizationTypelId, OrganizationTypeCode, OrganizationTypeName, OrganizationalLevelID, OrganizationalIndex, Address, ContactNo, Fax, Email);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Deletes the type of the organization.
        /// </summary>
        public void DeleteOrganizationType(int OrganizationTypelId)
        {
            try
            {
                objOrganizationDAL.DelecteOrganizationTypes(OrganizationTypelId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the organization level.
        /// </summary>
        /// <param name="OrganizationLevelId">The organization level id.</param>
        /// <returns></returns>
        /// 



        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetOrganizationTypesExcel(int CompanyId, int OrganizationTypeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationTypesExcel(CompanyId, OrganizationTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }



        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetOrganizationTypes(int CompanyId, int OrganizationTypeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationTypes(CompanyId, OrganizationTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetOrganizationTypes(int CompanyId, int OrganizationLevelId, int OrganizationTypeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationTypes(CompanyId, OrganizationLevelId, OrganizationTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        public DataTable GetOrganizationTypesByLevelIndex(int CompanyId, int OrganizationLevelIndex)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objOrganizationDAL.GetOrganizationTypesByLevelIndex(CompanyId, OrganizationLevelIndex);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
            return dtTable;
        }

        public int GetOrganizationTypeId(int OrgStructureID)
        {
            int orgTypeId = -100;
            try
            {
                orgTypeId = objOrganizationDAL.GetOrganizationTypeId(OrgStructureID);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return orgTypeId;
        }
        #endregion

        #region Organization Structure
        private bool ValidateStuctureUpdate(int StuctureId, int StuctureParentId)
        {
            if (StuctureParentId == StuctureId)
            {
                _isError = true;
                _errorMsg = "Parent and Stucrure canot me the same...!";
                return false;
            }
            else
                return true;
        }

        private bool ValidateDuplicateChilds(int StuctureParentId, int OrganizationTypeId)
        {
            if (GetChildrensByParetStuctureId(StuctureParentId, OrganizationTypeId).Rows.Count > 0)
            {
                _isError = true;
                _errorMsg = "This Department Type already in the structure...!";
                return false;
            }
            else
                return true;
        }

        private DataTable GetChildrensByParetStuctureId(int ParentID, int OrganizationTypeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetChildrensByParetStuctureId(ParentID, OrganizationTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        /// <summary>
        /// Adds the type of the organization.
        /// </summary>
        /// 
        public void AddOrganizationalStructure(int CompanyId)
        {
            try
            {
                if (ValidateDuplicateChilds(_parentId, _organizationTypeId))
                {
                    objOrganizationDAL.AddOrganizationalStructure(CompanyId, _parentId, _organizationStructureLevelIndex, _organizationTypeId);
                    SetDefaultValues();
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the type of the organization.
        /// </summary>
        public void UpdateOrganizationalStucture(int OrganizationStructurelId)
        {
            try
            {
                if (ValidateStuctureUpdate(OrganizationStructurelId, _parentId))
                {
                    objOrganizationDAL.UpdateOrganizationalStucture(OrganizationStructurelId, _parentId, _organizationStructureLevelIndex, _organizationTypeId);
                    SetDefaultValues();
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteOrganizationalStucture(int OrgStructureID)
        {
            try
            {
                objOrganizationDAL.DeleteOrganizationalStucture(OrgStructureID);
                SetDefaultValues();
            }          
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }          
        }

        public DataTable GetOrganizationStructureRootLevel(int CompanyId, int OrganizationStructureId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationalStuctureRootLevel(CompanyId, OrganizationStructureId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        /// <summary>
        /// Gets the organization level.
        /// </summary>
        /// <param name="OrganizationLevelId">The organization level id.</param>
        /// <returns></returns>
        public DataTable GetOrganizationStructure(int OrganizationStructureId,int CompanyId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationalStucture(OrganizationStructureId, CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        public DataTable GetOrganizationStructure(int OrganizationStructureId, bool GetByParent)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationalStucture(OrganizationStructureId, GetByParent);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        public DataTable GetRowsWhereParentIDEquals(int OrganizationStructureId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetRowsWhereParentIDEquals(OrganizationStructureId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        public DataTable GetOrganizationLevelIndexByOrganizationTypeId(int OrganizationTypeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationLevelIndexByOrganizationTypeId(OrganizationTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        public DataTable GetOrganizationLevel1(int OrganizationalLevelID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objOrganizationDAL.GetOrganizationLevel1(OrganizationalLevelID);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dataTable;
        }

        #endregion         
        #endregion
        #endregion
    }
}