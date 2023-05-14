using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.Payroll.DAL
{
    public class BankAndBranchDAL
    {
        #region Fields

        private SqlConnection dbConnection;

        private bool _isError;
        private string _errorMsg;
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

        public BankAndBranchDAL()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        #endregion

        #region Methods

        #region Internal
        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }

            InitializeFields();
        }

        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = int.MinValue;
        }

        #endregion

        #region External
        public void UpdateBankDetailsFrBcode(int BankId, int BranchId, string BranchCode, string Branch)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateBranchWiseCode", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@BranchCode", BranchCode);
                    command.Parameters.AddWithValue("@Branch", Branch);


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
                dbConnection.Close();
            }
        }

        public void InsertBankDetailsFrBcode(int BankId, string BranchCode, string Branch)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_InsertBranchWiseCode", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchCode", BranchCode);
                    command.Parameters.AddWithValue("@Branch", Branch);


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
                dbConnection.Close();
            }
        }

        public DataTable GetBankBranch(int BankId)
        {
            InitializeFields();

            DataTable dtData = new DataTable();

            try
            {


                using (SqlCommand command = new SqlCommand("Pay_GetBankCodeWiseBranches", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@BankId", BankId);



                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtData);
                    }
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorMsg = sqlExp.Message;
                _errorNo = sqlExp.Number;
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

            return dtData;
        }

        public DataTable GetBankDetails()
        {
            InitializeFields();

            DataTable dtData = new DataTable();

            try
            {
                string sqlQuery = "SELECT BankCode, BankName, BankId FROM Pay_Bank";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {

                    command.CommandType = CommandType.Text;
                    OpenDB();

                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtData);
                    }
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorMsg = sqlExp.Message;
                _errorNo = sqlExp.Number;
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

            return dtData;
        }

        public DataTable GetBankBranchDetails()
        {
            InitializeFields();

            DataTable dtData = new DataTable();

            try
            {
                string sqlQuery = "SELECT BranchId, BankId, BranchCode, Branch FROM Pay_BankBranch";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {

                    command.CommandType = CommandType.Text;
                    OpenDB();

                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtData);
                    }
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorMsg = sqlExp.Message;
                _errorNo = sqlExp.Number;
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

            return dtData;
        }

        public DataTable GetBankBranchDetails(int bankID)
        {
            InitializeFields();

            DataTable dtData = new DataTable();

            try
            {
                string sqlQuery = "SELECT BranchId, BankId, BranchCode, Branch FROM Pay_BankBranch WHERE (BankId = " + bankID + ")";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {

                    command.CommandType = CommandType.Text;
                    OpenDB();

                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtData);
                    }
                }
            }
            catch (SqlException sqlExp)
            {
                _isError = true;
                _errorMsg = sqlExp.Message;
                _errorNo = sqlExp.Number;
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

            return dtData;
        }

        #endregion

        #endregion

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
        /// </summary>
        ~BankAndBranchDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
