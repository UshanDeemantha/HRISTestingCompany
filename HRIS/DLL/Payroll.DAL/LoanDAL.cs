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
/// Created Timestamp   : 7/18/2011
/// Class Description   : Loan Related Data
/// Task Code List      : HRMV2P000020, 
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
    public class LoanDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private string _result;
        private bool _isSuccess;
        #endregion
        
        #region Properties
        public bool IsError
        {
            get { return _isError; }
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        } 

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public string Result
        {
            get { return _result; }
        } 
        #endregion

        #region Constructor
        public LoanDAL()
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
            _isSuccess=true;
            _result=string.Empty;
        } 
        #endregion

        #region Loan Types
        public DataTable GetLoanTypes(int LoanTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetLoanTypes", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanTypeId", LoanTypeId);
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

        public void AddLoanType(string LoanTypeCode, string LoanTypeName, decimal InterestRate, decimal MinLoanAmount, decimal MaxLoanAmount, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddLoanType", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanTypeCode", LoanTypeCode);
                    command.Parameters.AddWithValue("@LoanTypeName", LoanTypeName);
                    command.Parameters.AddWithValue("@InterestRate", InterestRate);
                    command.Parameters.AddWithValue("@MinLoanAmount", MinLoanAmount);
                    command.Parameters.AddWithValue("@MaxLoanAmount", MaxLoanAmount);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    SqlParameter successState = new SqlParameter("@Success", SqlDbType.Bit);
                    successState.Direction = ParameterDirection.Output;
                    command.Parameters.Add(successState);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(successState.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void UpdateLoanType(int LoanTypeId, string LoanTypeCode, string LoanTypeName, decimal InterestRate, decimal MinLoanAmount, decimal MaxLoanAmount, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateLoanType", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanTypeId", LoanTypeId);
                    command.Parameters.AddWithValue("@LoanTypeCode", LoanTypeCode);
                    command.Parameters.AddWithValue("@LoanTypeName", LoanTypeName);
                    command.Parameters.AddWithValue("@InterestRate", InterestRate);
                    command.Parameters.AddWithValue("@MinLoanAmount", MinLoanAmount);
                    command.Parameters.AddWithValue("@MaxLoanAmount", MaxLoanAmount);
                    command.Parameters.AddWithValue("@Remarks", Remarks);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    SqlParameter successState = new SqlParameter("@Success", SqlDbType.Bit);
                    successState.Direction = ParameterDirection.Output;
                    command.Parameters.Add(successState);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void DeleteLoanType(int LoanTypeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteLoanType", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanTypeId", LoanTypeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        #region Loans
        public DataTable GetEmployeeLoans(long EmployeeId, long LoanId, int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeLoans", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LoanId", LoanId);
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
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

        public DataTable ViewEmployeeLoans(long LoanId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_ViewEmployeeLoans", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanId", LoanId);
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

        public DataTable GetEmployeeLoansForSettlement(long EmployeeId, long LoanId, int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeLoansForSettlement", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@LoanId", LoanId);
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
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

        public DataTable GetLoanEmployees(int PayCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetLoanEmployees", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
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

        public void AddEmployeeLoan(long EmployeeId, int PayPeriod, int LoanTypeId, DateTime LoanTakenDate, DateTime TakenDate, DateTime EndDate, int NoOfPremiums, decimal LoanAmount,
          decimal Premium, decimal InterestRate, decimal SetleAmount, int DueNoofPremiums, decimal InterestAmount, bool isHold, bool isCompleted,
            bool IsNoOfPremiumCalculated, bool IsPremiumCalculated, string CreateUser, int ActualNoOfPremiums, decimal ActualPremium)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddEmployeeLoan", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriod);
                    command.Parameters.AddWithValue("@LoanTypeId", LoanTypeId);
                    command.Parameters.AddWithValue("@LoanTakenDate", LoanTakenDate);
                    command.Parameters.AddWithValue("@TakenDate", TakenDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@NoOfPremiums", NoOfPremiums);
                    command.Parameters.AddWithValue("@LoanAmount", LoanAmount);
                    command.Parameters.AddWithValue("@Premium", Premium);
                    command.Parameters.AddWithValue("@InterestRate", InterestRate);
                    command.Parameters.AddWithValue("@SetleAmount", SetleAmount);
                    command.Parameters.AddWithValue("@DueNoofPremiums", DueNoofPremiums);
                    command.Parameters.AddWithValue("@InterestAmount", InterestAmount);
                    command.Parameters.AddWithValue("@IsHold", isHold);
                    command.Parameters.AddWithValue("@IsCompleted", isCompleted);
                    command.Parameters.AddWithValue("@IsNoOfPremiumCalculated", IsNoOfPremiumCalculated);
                    command.Parameters.AddWithValue("@IsPremiumCalculated", IsPremiumCalculated);
                    command.Parameters.AddWithValue("@CreateUser", CreateUser);
                    command.Parameters.AddWithValue("@ActualNoOfPremiums", ActualNoOfPremiums);
                    command.Parameters.AddWithValue("@ActualPremium", ActualPremium);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    SqlParameter sussessState = new SqlParameter("@Success", SqlDbType.Bit);
                    sussessState.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sussessState);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(sussessState.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void ProcessLoan(long EmployeeId, int PayPeriodId, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_ProcessLoan", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EncryptionKey", "007London");
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void UpdateEmployeeLoan(long LoanId, DateTime EndDate, int NoOfPremiums, int ExistingPremiums, decimal Premium, decimal InterestRate, int payPeriod, bool hold, bool completed, bool minusHold, string UpdateUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateEmployeeLoan", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanId", LoanId);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@NoOfPremiums", NoOfPremiums);
                    command.Parameters.AddWithValue("@Premium", Premium);
                    command.Parameters.AddWithValue("@InterestRate", InterestRate);
                    command.Parameters.AddWithValue("@PayPeriod", payPeriod);
                    command.Parameters.AddWithValue("@Hold", hold);
                    command.Parameters.AddWithValue("@Completed", completed);
                    command.Parameters.AddWithValue("@MinusHold", minusHold);
                    command.Parameters.AddWithValue("@UpdateUser", UpdateUser);


                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    SqlParameter sussessState = new SqlParameter("@Success", SqlDbType.Bit);
                    sussessState.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sussessState);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(sussessState.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void DeleteEmployeeLoan(long LoanId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteEmployeeLoan", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanId", LoanId);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    SqlParameter sussessState = new SqlParameter("@Success", SqlDbType.Bit);
                    sussessState.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sussessState);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(sussessState.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void SettleEmployeeLoan(long EmployeeId, int PayPeriodId,string PaySetupCode, bool Posted)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_SettleEmployeeLoan", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void ManualLoanSettlement(long LoanId, decimal SettleAmount, decimal Premium, int DueNoofPremiums, decimal BalanceAmount, string Reference, string ApprovedBy,
            int? BankId, int? BankBranchId, string Remarks, DateTime SettleDate, int PayPeriodId, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_ManualLoanSettlement", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoanId", LoanId);
                    command.Parameters.AddWithValue("@SettleAmount", SettleAmount);
                    command.Parameters.AddWithValue("@Premium", Premium);
                    command.Parameters.AddWithValue("@DueNoofPremiums", DueNoofPremiums);
                    command.Parameters.AddWithValue("@BalanceAmount", BalanceAmount);
                    command.Parameters.AddWithValue("@Reference", Reference);
                    command.Parameters.AddWithValue("@ApprovedBy", ApprovedBy);
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BankBranchId", BankBranchId);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
                    command.Parameters.AddWithValue("@SettleDate", SettleDate);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);

                    command.ExecuteNonQuery();
                    _result = "Submitted Successfully.";
                    _isSuccess = true;
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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

        public void CheckLoanFinalize(long EmployeeId, int PayPeriodId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_CheckLoanFinalize", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EncryptionKey", "007London");
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
                _errorMsg = sqlex.Message;
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
        ~LoanDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
