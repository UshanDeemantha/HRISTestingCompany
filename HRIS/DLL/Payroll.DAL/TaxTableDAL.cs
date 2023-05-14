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
/// Created Timestamp : 18-July-2011
/// Class Description : HRM.Payroll.DAL.TaxTableDAL
/// Task Code: HRMV2P000010

namespace HRM.Payroll.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;


    public class TaxTableDAL
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
        public TaxTableDAL()
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

        public int AddToTaxTable(int taxTypeId, string taxName, decimal fromAmount, decimal toAmount, decimal taxPercentage, decimal taxValue)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int CreatedIndexID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddToTaxTable", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@TaxTypeId", taxTypeId);
                    command.Parameters.AddWithValue("@TaxName", taxName);
                    command.Parameters.AddWithValue("@FromAmount", fromAmount);
                    command.Parameters.AddWithValue("@ToAmount", toAmount);
                    command.Parameters.AddWithValue("@TaxPercentage", taxPercentage);
                    command.Parameters.AddWithValue("@TaxValue", taxValue);

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

        public void DeleteFromTaxTable(int taxId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteFromTaxTable", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@TaxId", taxId);
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

        public int EditTaxTable(int taxId, int taxTypeId, string taxName, decimal fromAmount, decimal toAmount, decimal taxPercentage, decimal taxValue)
        {
            _isError = false;
            _errorMsg = string.Empty;

            // return status -1 = if method returned an error
            // return status  0 = if tax id specified cannot be found
            // return status  1 = if records updated
            // return status  2 = if type specified already exists
            int ReturnID = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_EditTaxTable", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@TaxId", taxId);
                    command.Parameters.AddWithValue("@TaxTypeId", taxTypeId);
                    command.Parameters.AddWithValue("@TaxName", taxName);
                    command.Parameters.AddWithValue("@FromAmount", fromAmount);
                    command.Parameters.AddWithValue("@ToAmount", toAmount);
                    command.Parameters.AddWithValue("@TaxPercentage", taxPercentage);
                    command.Parameters.AddWithValue("@TaxValue", taxValue);

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

        public DataTable GetTaxTypesForCombo()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetTaxTypesForCombo", dbConnection))
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
                dbConnection.Close();
            }

            return dtTable;
        }

        public DataTable GetTaxTable(int taxID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetTaxTable", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@TaxId", taxID);
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
        ~TaxTableDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
