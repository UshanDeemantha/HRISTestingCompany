/// <summary>
/// Solution Name        : HRM
/// Project Name         : HR
/// Author               : ushan deemantha
/// Class Description    :
/// </summary>
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace HRM.HR.DAL
{
    public class InstitutesDAL
    {
        #region Fields
            private SqlConnection _dbConnection;
            private bool _isError;
            private string _errorMsg;

            private int _instituteId;
            private string _instituteCode;
            private string _instituteName;
            private int _instituteTypeId;
            private string _instituteTypeCode;
            private string _instituteTypeName;
        #endregion

        # region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        public string  ErrorMsg
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

        public InstitutesDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }


        #endregion
       

        #region Methods
        /// <summary>
        /// Opens the DB.
        /// </summary>
        public void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        #region Institute
        /// <summary>
        /// Adds the institute.
        /// </summary>
        /// <param name="InstituteCode">The institute code.</param>
        /// <param name="InstituteName">Name of the institute.</param>
        /// <param name="InstituteTypeId">The institute type id.</param>
        public void AddInstitute(string InstituteCode, string InstituteName, int InstituteTypeId,string Tel,string Fax,string Address)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddInstitute", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@InstituteCode", InstituteCode);
                    command.Parameters.AddWithValue("@InstituteName", InstituteName);
                    command.Parameters.AddWithValue("@InstituteTypeID", InstituteTypeId);
                    command.Parameters.AddWithValue("@Tel", Tel);
                    command.Parameters.AddWithValue("@Fax", Fax);
                    command.Parameters.AddWithValue("@Address", Address);
                    SqlParameter instituteId = new SqlParameter("@InstituteID", SqlDbType.Int);
                    instituteId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(instituteId);

                    command.ExecuteNonQuery();
                    _instituteId = Convert.ToInt32(instituteId.Value);
                    if (_instituteId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
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

        /// <summary>
        /// Updates the institute.
        /// </summary>
        /// <param name="InstituteId">The institute id.</param>
        /// <param name="InstituteCode">The institute code.</param>
        /// <param name="InstituteName">Name of the institute.</param>
        /// <param name="InstituteTypeId">The institute type id.</param>
        public void UpdateInstitute(int InstituteId, string InstituteCode, string InstituteName, int InstituteTypeId,string Tel,string Fax,string Address)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateInstitute", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@InstituteID", InstituteId);
                    command.Parameters.AddWithValue("@InstituteCode", InstituteCode);
                    command.Parameters.AddWithValue("@InstituteName", InstituteName);
                    command.Parameters.AddWithValue("@InstituteTypeID", InstituteTypeId);
                    command.Parameters.AddWithValue("@Tel", Tel);
                    command.Parameters.AddWithValue("@Fax", Fax);
                    command.Parameters.AddWithValue("@Address", Address);
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
        /// Gets the institute deatails.
        /// </summary>
        /// <param name="InstituteId">InstituteId as int</param>
        /// <returns>Retrun Datatable</returns>
        public DataTable GetInstituteDeatails(int InstituteId)
        {

            DataTable InstituteDetails = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetInstitute", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
                    SqlDataAdapter adaptor = new SqlDataAdapter(command);
                    adaptor.Fill(InstituteDetails);
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
            return InstituteDetails;
        }

        /// <summary>
        /// Deletes the institute.
        /// </summary>
        /// <param name="InstituteId">The institute id.</param>
        public void DeleteInstitute(int InstituteId)
        {

           
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteInstitute", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
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


        #endregion

        #region Institute Types
        /// <summary>
        /// Adds the type of the institute.
        /// </summary>
        /// <param name="InstituteTypeCode">The institute type code.</param>
        /// <param name="InstituteTypeName">Name of the institute type.</param>
        public void AddInstituteType(string InstituteTypeCode, string InstituteTypeName)
        {
            try 
            {
                using (SqlCommand command = new SqlCommand("HR_AddInstituteType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@InstituteTypeCode",InstituteTypeCode);
                    command.Parameters.AddWithValue("@InstituteTypeName",InstituteTypeName);
                    SqlParameter instituteType = new SqlParameter("@InstituteTypeId", SqlDbType.Int);
                    instituteType.Direction = ParameterDirection.Output;
                    command.Parameters.Add(instituteType);

                    command.ExecuteNonQuery();
                    _instituteTypeId = Convert.ToInt32(instituteType.Value);
                    if (_instituteTypeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
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

        /// <summary>
        /// Updates the type of the institute.
        /// </summary>
        /// <param name="InstituteTypeId">The institute type id.</param>
        /// <param name="InstituteTypeCode">The institute type code.</param>
        /// <param name="InstituteTypeName">Name of the institute type.</param>
        public void UpdateInstituteType(int InstituteTypeId, string InstituteTypeCode, string InstituteTypeName)
        {
            try
            {
                using(SqlCommand command = new SqlCommand("HR_UpdateInstituteType",_dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@InstituteTypeId", InstituteTypeId);
                    command.Parameters.AddWithValue("@InstituteTypeCode",InstituteTypeCode);
                    command.Parameters.AddWithValue("@InstituteTypeName", InstituteTypeName);
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
        /// Gets the type of the institute.
        /// </summary>
        /// <param name="InstituteTypeId">InstituteTypeId as int</param>
        /// <returns>Returns DataTable </returns>
        public DataTable GetInstituteType(int InstituteTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("HR_GetInstituteType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.Add("@InstituteTypeID", InstituteTypeId);
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
        }


        public void DeleteInstituteType(int InstituteTypeId)
        {

          
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteInstituteType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@InstituteTypeId", InstituteTypeId);
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

        public DataTable GetInstituteByTypeId(int InstituteTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("HR_GetInstituteByTypeId", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.Add("@InstituteTypeID", InstituteTypeId);
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
        }

        #endregion

        #endregion
    }
}
