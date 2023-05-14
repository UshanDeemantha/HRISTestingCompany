/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name : HRM.Payroll.DAL
/// Cording Standard : MasterKey Cording Standards
/// Author : Asanka C. Amarasinghe
/// Created Timestamp : 7/13/2011
/// Class Description : HRM.Payroll.DAL.TaxTypesDAL
/// Task Code: HRMV2P000009

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HRM.Payroll.DAL
{
    public class TaxTypesDAL
    {
        #region Fields

        private SqlConnection dbConnection;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxTypesDAL"/> class.
        /// </summary>
        public TaxTypesDAL()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

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

        public int ErrorNo
        {
            get { return _errorNo; }
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

        public int AddTaxType(string taxType, decimal percentage, bool ReferTable)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int CreatedIndexID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddTaxType", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@TaxType", taxType);
                    command.Parameters.AddWithValue("@Percentage", percentage);
                    command.Parameters.AddWithValue("@ReferTable", ReferTable);
                    CreatedIndexID = Convert.ToInt32(command.ExecuteScalar());
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

        public void DeleteTaxType(int taxTypeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteTaxType", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@TaxTypeId", taxTypeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorNo = sqlExp.Number;
                _errorMsg = sqlExp.Message;
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
        }

        public int EditTaxType(int taxTypeID, string taxType, decimal percentage, bool ReferTable)
        {
            _isError = false;
            _errorMsg = string.Empty;

            // return status -1 = if method returned an error
            // return status  0 = if tax type id specified cannot be found
            // return status  1 = if records updated
            // return status  2 = if type specified already exists
            int ReturnID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_EditTaxType", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@TaxTypeID", taxTypeID);
                    command.Parameters.AddWithValue("@TaxType", taxType);
                    command.Parameters.AddWithValue("@Percentage", percentage);
                    command.Parameters.AddWithValue("@ReferTable", ReferTable);

                    ReturnID = Convert.ToInt32(command.ExecuteScalar());
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

            return ReturnID;
        }

        public DataTable GetTaxTypes(int taxTypeID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();
            
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetTaxTypes", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    OpenDB();

                    command.Parameters.AddWithValue("@TaxTypeID", taxTypeID);
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

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="TaxTypesDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~TaxTypesDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}