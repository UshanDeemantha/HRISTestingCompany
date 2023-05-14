/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, No. 51, Flower Road, Colombo 7.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name       : HRM.Payroll.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Asanka C. Amarasinghe
/// Created Timestamp   : 30-September-2011
/// Class Description   : HRM.Time.DAL.TextFileProcessingDAL
/// Task Code           : --


namespace HRM.Time.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Data;


    public class TextFileProcessingDAL
    {
        #region Fields

        private SqlConnection dbConnection;

        private bool _isError;
        private string _errorMsg;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxTypesDAL"/> class.
        /// </summary>
        public TextFileProcessingDAL()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>Occurred Error Message As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }

        #region Text File Reading Settings

        public Int64 AddTextFileReadingSetting(string configurationName, string configurationString)
        {
            _isError = false;
            _errorMsg = string.Empty;

            Int64 CreatedIndexID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddTextFileReadingSetting", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ConfigurationName", configurationName);
                    command.Parameters.AddWithValue("@ConfigurationString", configurationString);

                    CreatedIndexID = Convert.ToInt64(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return CreatedIndexID;
        }

        public Int64 AddRowData(string cardNo, string cardDate, string cardTime, string functionKey, string miscData, DateTime cardDateTime, DateTime uploadDate)
        {
            _isError = false;
            _errorMsg = string.Empty;

            Int64 CreatedIndexID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddRawData", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CardNo", cardNo);
                    command.Parameters.AddWithValue("@CardDate", cardDate);
                    command.Parameters.AddWithValue("@CardTime", cardTime);
                    command.Parameters.AddWithValue("@FunctionKey", functionKey);
                    command.Parameters.AddWithValue("@MiscData", miscData);
                    command.Parameters.AddWithValue("@CardDateTime", cardDateTime);
                    command.Parameters.AddWithValue("@UploadDate", uploadDate);

                    CreatedIndexID = Convert.ToInt64(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return CreatedIndexID;
        }

        public int DeleteTextFileReadingSetting(Int64 configurationID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ReturnStatus = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteTextFileReadingSetting", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ConfigurationID", configurationID);

                    ReturnStatus = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return ReturnStatus;
        }

        public DataTable GetTextFileReadingSetting()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                string sqlQuery = "SELECT ConfigurationID, ConfigurationName, ConfigurationString FROM Time_TextFileReadingConfiguration";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {
                    command.CommandType = CommandType.Text;

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
                dbConnection.Close();
            }

            return dtTable;
        }

        public string GetTextFileReadingSettingForID(int configurationID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            string configurationString = string.Empty;
            DataTable dtTable = new DataTable();

            try
            {
                string sqlQuery = "SELECT ConfigurationString FROM Time_TextFileReadingConfiguration WHERE (ConfigurationID = " + configurationID + ")";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {
                    command.CommandType = CommandType.Text;

                    OpenDB();

                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }

                    if (dtTable.Rows.Count > 0)
                    {
                        configurationString = dtTable.Rows[0]["ConfigurationString"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                configurationString = string.Empty;

                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return configurationString;
        }

        public DataTable GetUploadDates()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                string sqlQuery = "SELECT DISTINCT UploadDate FROM Time_RawData ORDER BY UploadDate DESC";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {
                    command.CommandType = CommandType.Text;

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
                dbConnection.Close();
            }

            return dtTable;
        }

        public DataTable GetUploadedData(DateTime dtUploadedDate)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetUploadedRawData", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@UploadedDate", dtUploadedDate);

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
                dbConnection.Close();
            }

            return dtTable;
        }

        #endregion

        #endregion

        #region Distructor

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FestivalAdvanceDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~TextFileProcessingDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
