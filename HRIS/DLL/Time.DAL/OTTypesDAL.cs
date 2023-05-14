using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM.Time.DAL
{
    public class OTTypesDAL
    {
        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _dayTypeId = 0;
      

        #endregion

        #region Properties

        public bool IsError
        {
            get { return _isError; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        public int OTTypeId
        {
            get { return _dayTypeId; }
        }
        
        #endregion

        #region Constructor

        public OTTypesDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods

        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }


         #region OTType

        public void AddOTTypes(string OTTypeCode,string OTDescription,decimal OTRate,decimal MaxHours,string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddOTType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@OTType", OTTypeCode);
                    command.Parameters.AddWithValue("@OTDescription", OTDescription);
                    command.Parameters.AddWithValue("@OTRate", OTRate);
                    command.Parameters.AddWithValue("@MaxOTHours", MaxHours);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    //command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    SqlParameter OTTypeId = new SqlParameter("@OTTypeId", SqlDbType.Int, 8);
                    OTTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(OTTypeId);
                    command.ExecuteNonQuery();
                    _dayTypeId = Convert.ToInt32(OTTypeId.Value);
                    if (_dayTypeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
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

        public void UpdateOTType(int OTTypeId, string OTDescription, decimal OTRate, decimal MaxHours, string ModifyUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateOTType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@OTTypeId", OTTypeId);
                    command.Parameters.AddWithValue("@OTDescription", OTDescription);
                    command.Parameters.AddWithValue("@OTRate", OTRate);
                    command.Parameters.AddWithValue("@MaxOTHours", MaxHours);
                    command.Parameters.AddWithValue("@ModifiedUser", ModifyUser);

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

        public DataTable GetOTType()
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetOTType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();                 
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

        public DataTable GetOTTypeById(int OTTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetOTTypeById", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@OTTypeId", OTTypeId);
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

        public DataTable CheckOTTypeTypeCode(string OTTypeCode)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetOTTypeByCode", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@OTType", OTTypeCode);
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

        public DataTable GetShiftSetupByOTType(int OTTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("HR_GetShiftSetupByOTType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.Add("@OTTypeId", OTTypeId);
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

        public void DeleteOTType(int OTTypeId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("Time_DeleteOTType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OTTypeId", OTTypeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _isError = true;
                if (ex.Number == 547)
                {
                    _errorMsg = "Already Assign This Code";
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

        #endregion

        #region Destructor

        ~OTTypesDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
