
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.HR;
using HRM.HR.DAL;
using System.Data;


namespace HRM.HR.BLL
{
    /// <summary>
    /// Handles All Infomation about the Institute
    /// </summary>
    public class Institute
    {
        # region Fields
            InstitutesDAL objInstituteDAL;
            DataTable _institutes;
            private bool _isError;
            private string _errorMsg;

            private int _instituteId;
            private string _instituteCode;
            private string _instituteName;
            private int _instituteTypeId;
        # endregion


        # region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }    

        /// <summary>
        /// Gets the institute id.
        /// </summary>
        /// <value>The institute id.</value>
       public int InstituteId
        {
            get { return _instituteId; }
        }

        /// <summary>
        /// Gets or sets the institute code.
        /// </summary>
        /// <value>The institute code.</value>
        public string InstituteCode
        {
            get
            {
                return _instituteCode;
            }
            set
            {
                _instituteCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the institute.
        /// </summary>
        /// <value>The name of the institute.</value>
        public string InstituteName
        {
            get
            {
                return _instituteName;
            }
            set
            {
                _instituteName = value;
            }
        }

        /// <summary>
        /// Gets or sets the institute type id.
        /// </summary>
        /// <value>The institute type id.</value>
        public int InstituteTypeId
        {
            get
            {
                return _instituteTypeId;
            }
            set
            {
                _instituteTypeId = value;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Institute"/> class.
        /// </summary>
        public Institute()
        {
            objInstituteDAL = new InstitutesDAL();
        }

        #endregion


        #region Methods
        #region Institute
        public void AddInstitute(string InstituteCode, string InstituteName, int InstituteTypeId,string Tel,string Fax,string Address)
        {
            try
            {
                objInstituteDAL.AddInstitute(InstituteCode, InstituteName, InstituteTypeId,Tel, Fax,Address);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetInstituteDetails(int InstituteId)
        {
            try
            {
                _institutes = objInstituteDAL.GetInstituteDeatails(InstituteId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _institutes;
        }

        public void UpdateInstitute(int InstituteId, string InstituteCode, string InstituteName, int InstituteTypeId,string Tel,string Fax,string Address)
        {
            try
            {
                objInstituteDAL.UpdateInstitute(InstituteId, InstituteCode, InstituteName, InstituteTypeId,  Tel, Fax, Address);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteInstitute(int InstituteId)
        {
            try
            {
                objInstituteDAL.DeleteInstitute(InstituteId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        } 
        #endregion

        #region Institute Type
        public void AddInstituteType(string InstituteTypeCode, string InstituteTypeName)
        {
            try
            {
                objInstituteDAL.AddInstituteType(InstituteTypeCode, InstituteTypeName);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetInstituteTypeDetails(int InstituteTypeId)
        {
            try
            {
                _institutes = objInstituteDAL.GetInstituteType(InstituteTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _institutes;
        }

        public void UpdateInstituteType(int InstituteTypeId, string InstituteTypeCode, string InstituteTypeName)
        {
            try
            {
                objInstituteDAL.UpdateInstituteType(InstituteTypeId, InstituteTypeCode, InstituteTypeName);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteInstituteType(int InstituteTypeId)
        {
            try
            {
                objInstituteDAL.DeleteInstituteType(InstituteTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public DataTable GetInstituteByTypeId(int InstituteTypeId)
        {
            try
            {
                _institutes = objInstituteDAL.GetInstituteByTypeId(InstituteTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _institutes;
        }
        #endregion
        #endregion
    }
}
