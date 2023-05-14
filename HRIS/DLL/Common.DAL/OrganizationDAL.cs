
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HRM.Common.DAL
{
    public class OrganizationDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
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
        //private int _organizationLevelId;
        private int _organizationalIndex;
        private string _address;
        private string _contactNo;
        private string _fax;

        private int _organizationStructureId;
        private int _organizationLevelIndex;
        #endregion

        #region Propreties
        public bool IsError
        {
            get { return _isError; }
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public string Result
        {
            get { return _result; }
        } 

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

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

        public int OrganizationTypeId
        {
            get { return _organizationTypeId; }
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

        public string Fax 
        {
            get { return _fax; }
            set { _fax = value; }
        }

        public int OrganizationStructureId 
        {
            get { return _organizationStructureId; }
        }

        public int OrganizationLevelIndex 
        {
            get { return _organizationStructureId; }
            set { _organizationStructureId = value; }
        }

        #endregion

        #region Constructor
        public OrganizationDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        }
        #endregion

        #region Methods
        #region Internal
        public void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            InitializeFields();
        }

        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = int.MinValue;
            _isSuccess = true;
            _result = string.Empty;
        }     

        private void SetError(SqlException Ex)
        {
            _isError = true;
            _errorMsg = Ex.Message;
            _errorNo = Ex.Number;
        } 
        #endregion

        #region Organization Level
        /// <summary>
        /// Adds the organization level.
        /// </summary>
        /// <param name="CompanyId">CompanyId</param>
        /// <param name="OrganizationalLevel">The organizational level.</param>
        /// <param name="OrganizationalIndex">Index of the organizational.</param>
        public void AddOrganizationLevel(int CompanyId, string OrganizationalLevel, int OrganizationalIndex)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddOrganizationalLevel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationalLevel", OrganizationalLevel);
                    command.Parameters.AddWithValue("@OrganizationalIndex", OrganizationalIndex);

                    SqlParameter suceessPara = new SqlParameter("@Suceess", SqlDbType.Bit);
                    suceessPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(suceessPara);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    SqlParameter sqlpara_organizationalLevelId = new SqlParameter("@OrganizationalLevelID", SqlDbType.Int);
                    sqlpara_organizationalLevelId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sqlpara_organizationalLevelId);

                    command.ExecuteNonQuery();
                    _organizationLevelId = Convert.ToInt32(sqlpara_organizationalLevelId.Value);
                    _result = resultPara.Value.ToString();
                    _isSuccess = Convert.ToBoolean(suceessPara.Value);
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

  
        public DataTable GetParentOrganizationTypesforsearch(int CompanyId, int OrgstrucId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetParentOrganizationalTypesforsearch", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgstrucId", OrgstrucId);
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
        public DataTable GetOrganizationFlow(int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationFlow", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
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
        /// Updates the organization level.
        /// </summary>
        /// <param name="OrganizationalLevelId">The organizational level id.</param>
        /// <param name="OrganizationalLevel">The organizational level.</param>
        /// <param name="OrganizationalIndex">Index of the organizational.</param>
        public void UpdateOrganizationLevel(int OrganizationalLevelId, string OrganizationalLevel, int OrganizationalIndex)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateOrganizationalLevel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;  
                    command.Parameters.AddWithValue("@OrganizationalLevelID", OrganizationalLevelId);
                    command.Parameters.AddWithValue("@OrganizationalLevel", OrganizationalLevel);
                    command.Parameters.AddWithValue("@OrganizationalIndex", OrganizationalIndex);                  

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
        /// Deletes the organization level.
        /// </summary>
        /// <param name="OrganizationalLevelId">The organizational level id.</param>
        public void DeleteOrganizationLevel(int OrganizationalLevelId, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteOrganizationalLevel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrganizationalLevelID", OrganizationalLevelId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    SqlParameter suceessPara = new SqlParameter("@Suceess", SqlDbType.Bit);
                    suceessPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(suceessPara);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _result = resultPara.Value.ToString();
                    _isSuccess = Convert.ToBoolean(suceessPara.Value);
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
        /// Gets the organization level.
        /// </summary>
        /// <param name="OrganizationalLevelId">The organizational level id.</param>
        /// <returns></returns>
        public DataTable GetOrganizationLevel(int CompanyId, int OrganizationalLevelId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationalLevel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationalLevelID", OrganizationalLevelId);
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

        public int GetOrganizationChildLevel(int OrgStructureID)
        {
            int childLevelId = -100;
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationChildLevel", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);

                    SqlParameter childLevelPara = new SqlParameter("@ChildLevelId", SqlDbType.Int);
                    childLevelPara.Direction=ParameterDirection.Output;
                    command.Parameters.Add(childLevelPara);
                    command.ExecuteNonQuery();

                    childLevelId=Convert.ToInt32(childLevelPara.Value);
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
            return childLevelId;
        }
        #endregion

        #region OrganizationTypes
        public void AddOrganizationTypes(string OrganizationTypeCode, string OrganizationTypeName, int OrganizationalLevelID, int OrganizationalIndex, string Address, string ContactNo, string Fax, string Email, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddOrganizationaTypes", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@OrganizationTypeCode", OrganizationTypeCode);
                    command.Parameters.AddWithValue("@OrganizationTypeName", OrganizationTypeName);
                    command.Parameters.AddWithValue("@OrganizationalLevelID", OrganizationalLevelID);
                    command.Parameters.AddWithValue("@OrganizationalIndex", OrganizationalIndex);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@ContactNo", ContactNo);
                    command.Parameters.AddWithValue("@Fax", Fax);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@CompanyID", CompanyId);

                    SqlParameter sqlpara_organizationalTypelId = new SqlParameter("@OrganizationTypeID", SqlDbType.Int);
                    sqlpara_organizationalTypelId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sqlpara_organizationalTypelId);

                    command.ExecuteNonQuery();
                    _organizationTypeId = Convert.ToInt32(sqlpara_organizationalTypelId.Value);
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

        public void UpdateOrganizationTypes(int OrganizationTypeID, string OrganizationTypeCode, string OrganizationTypeName, int OrganizationalLevelID, int OrganizationalIndex, string Address, string ContactNo, string Fax, string Email)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateOrganizationaTypes", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
                    command.Parameters.AddWithValue("@OrganizationTypeCode", OrganizationTypeCode);
                    command.Parameters.AddWithValue("@OrganizationTypeName", OrganizationTypeName);
                    command.Parameters.AddWithValue("@OrganizationalLevelID", OrganizationalLevelID);
                    command.Parameters.AddWithValue("@OrganizationalIndex", OrganizationalIndex);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@ContactNo", ContactNo);
                    command.Parameters.AddWithValue("@Fax", Fax);
                    command.Parameters.AddWithValue("@Email", Email);
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

        public void DelecteOrganizationTypes(int OrganizationTypeID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteOrganizationaTypes", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
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



        public DataTable GetParentOrganizationTypes(int CompanyId, int OrganizationTypeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetParentOrganizationalTypes", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
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

        public DataTable GetOrganizationTypesExcel(int CompanyId, int OrganizationTypeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationalTypesExcel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
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


        public DataTable GetOrganizationTypes(int CompanyId, int OrganizationTypeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationalTypes", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
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

        public DataTable GetOrganizationTypes(int CompanyId, int OrganizationLevelId, int OrganizationTypeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationTypesByLevel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationLevelId", OrganizationLevelId);
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
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

        public DataTable GetOrganizationTypesByLevelIndex(int CompanyId, int OrganizationLevelIndex)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationTypesByLevelIndex", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);                    
                    command.Parameters.AddWithValue("@OrganizationLevelIndex", OrganizationLevelIndex);
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

        public int GetOrganizationTypeId(int OrgStructureID)
        {
            int orgTypeId = -100;
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationTypeId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);

                    SqlParameter orgTypeIdPara = new SqlParameter("@OrganizationTypeID", SqlDbType.Int);
                    orgTypeIdPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(orgTypeIdPara);
                    command.ExecuteNonQuery();

                    orgTypeId = Convert.ToInt32(orgTypeIdPara.Value);
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
            return orgTypeId;
        }
        #endregion
        
        #region Organizational Structure
        public void AddOrganizationalStructure(int CompanyId, int ParentID, int LevelIndex, int OrganizationTypeID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddOrganizationaalStructure", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@ParentID", ParentID);
                    command.Parameters.AddWithValue("@LevelIndex", LevelIndex);
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
                    SqlParameter sqlpara_organizationalStructurelId = new SqlParameter("@OrgStructureID", SqlDbType.Int);
                    sqlpara_organizationalStructurelId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sqlpara_organizationalStructurelId);
                   
                    command.ExecuteNonQuery();
                    _organizationLevelId = Convert.ToInt32(sqlpara_organizationalStructurelId.Value);
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

        public void UpdateOrganizationalStucture(int OrgStructureID, int ParentID, int LevelIndex, int OrganizationTypeID)
        {
           try
           {
                using (SqlCommand command = new SqlCommand("COM_UpdateOrganizationaalStructure", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@ParentID", ParentID);
                    command.Parameters.AddWithValue("@LevelIndex", LevelIndex);
                    command.Parameters.AddWithValue("@OrganizationTypeID", OrganizationTypeID);
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

        public void DeleteOrganizationalStucture(int OrgStructureID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteOrganisationStructure", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);                  
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

        public DataTable GetOrganizationalStuctureRootLevel(int CompanyId, int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationalStuctureByCompany", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
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

        public DataTable GetOrganizationalStucture(int OrgStructureID, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationaalStucture", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
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

        public DataTable GetOrganizationalStucture(int OrgStructureID, bool GetByParent)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationaalStucture", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@GetByParent", GetByParent);
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

        public DataTable GetOrganizationLevelIndexByOrganizationTypeId(int OrganizationTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationLevelIndexByOrganizationTypeId ", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrganizationTypeId", OrganizationTypeId);
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

        public DataTable GetOrganizationLevel1(int OrganizationalLevelID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationLevel1 ", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrganizationalLevelID", OrganizationalLevelID);
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

        public DataTable GetChildrensByParetStuctureId(int ParentID, int OrganizationTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationaalStuctureByParentId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@ParentID", ParentID);
                    command.Parameters.AddWithValue("@OrganizationTypeId", OrganizationTypeId);
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

        public DataTable GetRowsWhereParentIDEquals(int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetOrganizationStructerRowsWhereParentIDEquals", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
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
    }
}
