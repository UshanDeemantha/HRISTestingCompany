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
/// Created Timestamp : 23-August-2011
/// Class Description : HRM.Payroll.DAL.ReferencesDAL
/// Task Code: --

namespace HRM.Payroll.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class ReferencesDAL
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
        public ReferencesDAL()
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

        /// <summary>
        /// Gets the Error Number.
        /// </summary>
        /// <value>Occurred Error as a Numeric Representation</value>
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
        public DataTable GetEmployeeCodeForUnitCompanies()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeCodeForUnitCompanies", dbConnection))
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
        public DataTable GetEmployeesNicNo()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeesNicNo", dbConnection))
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
        public DataTable GetBankById(int bankId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetBankById", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@BankId", bankId);
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

        public DataTable GetBankBranchByBankId(int bankId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetBankBranchByBankId", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

                    command.Parameters.AddWithValue("@BankId", bankId);
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

        public DataTable GetPayEmployeePay()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetPayEmployeePay", dbConnection))
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

        public DataTable GetEmployeeCodeNICANDEPFNo(int CompanyId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeCodeNICANDEPFNo", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    OpenDB();

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
                dbConnection.Close();
            }

            return dtTable;
        }

        public DataTable GetFullDesignationStructure()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                string sqlQuery = "SELECT DesignationStuctureID, TreeIndex, DesignationID, ParentID FROM DesignationStucture";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.Text;
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

        public DataTable GetFullOrganizationStructure()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                string sqlQuery = "SELECT OrgStructureID, ParentID, LevelIndex, OrganizationTypeID FROM OrganisationStructure";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.Text;
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

        public DataTable GetUnpostedPayPeriodCategories()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetUnpostedPayPeriodCategories", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;
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


        public DataTable GetPayPeriod()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayPeriodsByExcel", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;
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
        /// <see cref="FestivalAdvanceDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~ReferencesDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
