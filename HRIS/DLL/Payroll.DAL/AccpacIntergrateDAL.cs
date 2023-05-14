/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name       : HRM
/// Project Name        : HRM.Payroll.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Anusha Gunasekara
/// Created Timestamp   : 7/20/2011
/// Class Description   : AccpacIntergation
/// Task Code List      : 
/// </summary>
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace HRM.Payroll.DAL
{
    public class AccpacIntergrateDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        #endregion
        
        #region Properties
        public bool IsError
        {
            get { return _isError; }
        }    

        public int ErrorNo
        {
            get { return _errorNo; }
        } 

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }       
        #endregion

        #region Constructor
        public AccpacIntergrateDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        } 
        #endregion

        #region Methods
        #region Internal
        private void OpenDb()
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
        } 

        private void SetError(SqlException SqlEx)
        {
            _isError=true;
            _errorNo=SqlEx.Number;
            _errorMsg = SqlEx.Message;
        }
        #endregion

        #region Accpac Categories
        public DataTable GetEmployeeList(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
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

        public void AddCategory(string AccPacCategory, string EPFAccount, string DynamicAllowanceAccount, string OTAccount, string StaticAllowanceAccount, string AirfareDeductionAccount, string FestivalAdvanceAccount, string C3DeductionAccount, string GrativityAccount, string AirfareProvisionAccount,
            string AirfareDeductionCredit, string FestivalAdvanceCredit, string C3DeductionCredit, string GrativityProvisionCredit, string AirfareProvisionCredit)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_AddCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccPacCategory", AccPacCategory);
                    command.Parameters.AddWithValue("@EPFAccount", EPFAccount);
                    command.Parameters.AddWithValue("@DynamicAllowanceAccount", DynamicAllowanceAccount);
                    command.Parameters.AddWithValue("@OTAccount", OTAccount);
                    command.Parameters.AddWithValue("@StaticAllowanceAccount", StaticAllowanceAccount);
                    command.Parameters.AddWithValue("@AirfareDeductionAccount", AirfareDeductionAccount);
                    command.Parameters.AddWithValue("@FestivalAdvanceAccount", FestivalAdvanceAccount);
                    command.Parameters.AddWithValue("@C3DeductionAccount", C3DeductionAccount);
                    command.Parameters.AddWithValue("@GrativityAccount", GrativityAccount);
                    command.Parameters.AddWithValue("@AirfareProvisionAccount", AirfareProvisionAccount);

                    command.Parameters.AddWithValue("@AirfareDeductionCredit", AirfareDeductionCredit);
                    command.Parameters.AddWithValue("@FestivalAdvanceCredit", FestivalAdvanceCredit);
                    command.Parameters.AddWithValue("@C3DeductionCredit", C3DeductionCredit);
                    command.Parameters.AddWithValue("@GrativityProvisionCredit", GrativityProvisionCredit);
                    command.Parameters.AddWithValue("@AirfareProvisionCredit", AirfareProvisionCredit);
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

        public void UpdateCategory(int AccPacCategoryId, string AccPacCategory, string EPFAccount, string DynamicAllowanceAccount, string OTAccount, string StaticAllowanceAccount, string AirfareDeductionAccount, string FestivalAdvanceAccount, string C3DeductionAccount, string GrativityAccount, string AirfareProvisionAccount,
                        string AirfareDeductionCredit, string FestivalAdvanceCredit, string C3DeductionCredit, string GrativityProvisionCredit, string AirfareProvisionCredit)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_UpdateCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccPacCategoryId", AccPacCategoryId);
                    command.Parameters.AddWithValue("@AccPacCategory", AccPacCategory);
                    command.Parameters.AddWithValue("@EPFAccount", EPFAccount);
                    command.Parameters.AddWithValue("@DynamicAllowanceAccount", DynamicAllowanceAccount);
                    command.Parameters.AddWithValue("@OTAccount", OTAccount);
                    command.Parameters.AddWithValue("@StaticAllowanceAccount", StaticAllowanceAccount);
                    command.Parameters.AddWithValue("@AirfareDeductionAccount", AirfareDeductionAccount);
                    command.Parameters.AddWithValue("@FestivalAdvanceAccount", FestivalAdvanceAccount);
                    command.Parameters.AddWithValue("@C3DeductionAccount", C3DeductionAccount);
                    command.Parameters.AddWithValue("@GrativityAccount", GrativityAccount);
                    command.Parameters.AddWithValue("@AirfareProvisionAccount", AirfareProvisionAccount);
                    command.Parameters.AddWithValue("@AirfareDeductionCredit", AirfareDeductionCredit);
                    command.Parameters.AddWithValue("@FestivalAdvanceCredit", FestivalAdvanceCredit);
                    command.Parameters.AddWithValue("@C3DeductionCredit", C3DeductionCredit);
                    command.Parameters.AddWithValue("@GrativityProvisionCredit", GrativityProvisionCredit);
                    command.Parameters.AddWithValue("@AirfareProvisionCredit", AirfareProvisionCredit);
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

        public void DeleteCategory(int AccPacCategoryId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_DeleteCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccPacCategoryId", AccPacCategoryId);
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

        public DataTable GetCategory(int AccPacCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_GetCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccPacCategoryId", AccPacCategoryId);
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


        #region Employee Accpac Code asign
        public void AsignEmployeeCategory(long EmployeeId, int AccPacCategoryId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_AsignEmployeeCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@AccPacCategoryId", AccPacCategoryId);                
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

        public void RemoveEmployeeCategory(long EmployeeId, int AccPacCategoryId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_RemoveEmployeeCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@AccPacCategoryId", AccPacCategoryId);
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

        public DataTable GetEmployeeAccpacAccounts(long EmployeeId, int AccPacCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_GetEmployeeAccpacAccounts", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@AccPacCategoryId", AccPacCategoryId);
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

        public DataTable GetEmployeeAccpacAccounts(long EmployeeId, int AccPacCategoryId, string OrganizationFilter, string DesignationFilter)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_GetEmployeeAccpacAccountsWithFilters", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@AccPacCategoryId", AccPacCategoryId);
                    command.Parameters.AddWithValue("@OrganizationFilterQuery", OrganizationFilter);
                    command.Parameters.AddWithValue("@DesignationFilterQuery", DesignationFilter);
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

        public int GetEmployeeAccpacCategory(long EmployeeId)
        {
            int accpacCategoryId = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("Accpac_GetEmployeeAccpacCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    SqlParameter EmpAccpacCategoryId = new SqlParameter("@AccPacCategoryId", SqlDbType.Int, 8);
                    EmpAccpacCategoryId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(EmpAccpacCategoryId);
                    command.ExecuteNonQuery();
                    accpacCategoryId = Convert.ToInt32(EmpAccpacCategoryId.Value);
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
            return accpacCategoryId;
        }
        #endregion
        #endregion

        #region Destructor
        ~AccpacIntergrateDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
