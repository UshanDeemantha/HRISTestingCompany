
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HRM.Common.DAL
{    
   public class DesignationDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private DataSet _Designations;
        private bool _isError;
        private string _errorMsg;
        private int _designationId;
        private int _organizationStructureId;
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

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        public int DesignationId
        {
            get { return _designationId; }
            set { _designationId = value; }
        }

        public int OrganizationStructureId
        { get { return _organizationStructureId; } }


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
        public DesignationDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
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
            InitializeFields();
        }

        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
        }

        private void SetError(SqlException Ex)
        {
            _isError = true;
            _errorMsg = Ex.Message;
            _errorNo = Ex.Number;
        }
        #endregion

        #region Designation
        /// <summary>
        /// Adds the Designation.
        /// </summary>
        /// <param name="DesignationCode">The Designation code as string.</param>
        /// <param name="DesignationName">Name of the Designation as string.</param>
        /// 

        public DataTable GetParentDesignationTypes(int DesignationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetParentDesignationTypes", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Designation", DesignationId);
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
        public void AddDesignation(string DesignationCode, string DesignationName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddDesignation", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("@DesignationCode", DesignationCode);
                    command.Parameters.AddWithValue("@DesignationName", DesignationName);
                    SqlParameter designationIdInfo = new SqlParameter("@DesignationId", SqlDbType.Int, 8);
                    designationIdInfo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(designationIdInfo);
                    command.ExecuteNonQuery();

                    _designationId = Convert.ToInt32(designationIdInfo.Value);
                    if (_designationId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists!";
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
        public DataTable GetDesignationTypesExcel(int DesignationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetDesignationTypesExcel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DesignationID", DesignationId);
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
        /// Updates the Designation.
        /// </summary>
        /// <param name="DesignationId">The Designation id as int.</param>
        /// <param name="DesignationCode">The Designation code as string.</param>
        /// <param name="DesignationName">Name of the Designation as string.</param>
        public void UpdateDesignation(int DesignationId, string DesignationCode, string DesignationName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateDesignation", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@DesignationCode", DesignationCode);
                    command.Parameters.AddWithValue("@DesignationName", DesignationName);
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
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
            
        public DataTable GetDesignation(int DesignationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetDesignation", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
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
       
        public void DeleteDesignation(int DesignationId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteDesigantion", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
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

        #region Designation Structure

        public DataTable GetParentDesignationTypes(int CompanyId, int DesignationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetParentDesignationTypes", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Designation", DesignationId);
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

        public DataTable GetDesignationTypesExcel(int CompanyId, int DesignationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetDesignationTypesExcel", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@DesignationID", DesignationId);
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





        public void AddDesignationStructure(int ParentID, int TreeIndex, int DesignationID, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddDesignationStructure", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddWithValue("@ParentID", ParentID);
                    command.Parameters.AddWithValue("@TreeIndex", TreeIndex);
                    command.Parameters.AddWithValue("@DesignationID", DesignationID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    SqlParameter sqlpara_DesignationStructurelId = new SqlParameter("@DesignationStuctureID", SqlDbType.Int);
                    sqlpara_DesignationStructurelId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sqlpara_DesignationStructurelId);
                    command.ExecuteNonQuery();

                    _organizationStructureId = Convert.ToInt32(sqlpara_DesignationStructurelId.Value);
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

        public void UpdateDesignationStucture(int DesignationStructureID, int ParentID, int TreeIndex, int DesignationID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateDesignationStructure", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@DesignationStuctureID", DesignationStructureID);
                    command.Parameters.AddWithValue("@ParentID", ParentID);
                    command.Parameters.AddWithValue("@TreeIndex", TreeIndex);
                    command.Parameters.AddWithValue("@DesignationID", DesignationID);
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

        public void DeleteDesignationStucture(int DesignationStructureID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteDesignationStucture", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@DesignationStuctureID", DesignationStructureID);   
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

        public DataTable GetDesignationStucture(int DesignationStructureID, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetDesignationStructure", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@DesignationStuctureID", DesignationStructureID);
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

        public DataTable GetRowsWhereParentIDEquals(int DesignationStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetDesignationStructerRowsWhereParentIDEquals", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@DesignationStructureID", DesignationStructureID);
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
        ~DesignationDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
