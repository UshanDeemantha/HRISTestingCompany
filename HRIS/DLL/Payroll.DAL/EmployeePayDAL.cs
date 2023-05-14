/// <summary>
/// Copyright (c) 2000-2008 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.

/// Solution Name : =HRM.Payroll.DAL
/// Cording Standard : MasterKey Cording Standards
/// Author : Chiran Chamara
/// Created Timestamp :7/15/2011
/// Class Description : HRM.Payroll.DAL.EmployeePayDAL
/// Task Code :HRMV2P000011
/// </summary>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.Payroll.DAL
{
   public class EmployeePayDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private long _employeeId;
        private string _createdUser;
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

        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        /// <value>The employee ID.</value>
        public long EmployeeID
        {
            get
            {
                return _employeeId;
            }
            set
            {
                _employeeId = value;
            }
        }

        public string CreatedUser
        {
            get
            {
                return _createdUser;
            }
            set
            {
                _createdUser = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
        /// </summary>
        public EmployeePayDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        public EmployeePayDAL(int EmployeeId)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            _employeeId = EmployeeId;
        }
        #endregion        

        #region Methods
        #region Internal
        /// <summary>
        /// Opens the DB.
        /// </summary>
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
            _errorNo = int.MinValue;
        }
        #endregion

        /// <summary>
        /// Adds the Employee Pay Sheet.
        /// </summary>
        /// <param name="EmployeeID">The employee ID. AS Long</param>
        public void AddEmployeePaySheet(long EmployeeID, int PayPeriodCategoryId, decimal BasicSalary, decimal BRA01, decimal BRA02, decimal INCREMENT2018, decimal PERFORMANCEINCENTIVE2018, decimal TRAVELLINGINCENTIVE, decimal TELEPHONEINCENTIVE, decimal SALESINCENTIVE,
                                        decimal OTHERINCENTIVE, decimal LEAVEENCASHMENT, decimal PREVIOUSMONTH, decimal GROSSSALARY, decimal LOAN, decimal SALAADVANCE, decimal MEALS, decimal DATA, bool Posted, decimal NOPAY, decimal STAFFWELFAIR, decimal CONTRIBUTION)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeePaySheet", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@BasicSalary", BasicSalary);
                    command.Parameters.AddWithValue("@BRA01", BRA01);
                    command.Parameters.AddWithValue("@BRA02", BRA02);
                    command.Parameters.AddWithValue("@INCREMENT", INCREMENT2018);
                    command.Parameters.AddWithValue("@PERFORMANCEINCENTIVE", PERFORMANCEINCENTIVE2018);
                    command.Parameters.AddWithValue("@TRAVELLINGINCENTIVE", TRAVELLINGINCENTIVE);
                    command.Parameters.AddWithValue("@TELEPHONEINCENTIVE", TELEPHONEINCENTIVE);
                    command.Parameters.AddWithValue("@SALESINCENTIVE", SALESINCENTIVE);
                    command.Parameters.AddWithValue("@OTHERINCENTIVE", OTHERINCENTIVE);
                    command.Parameters.AddWithValue("@LEAVEENCASHMENT", LEAVEENCASHMENT);
                    command.Parameters.AddWithValue("@PREVIOUSMONTH", PREVIOUSMONTH);
                    command.Parameters.AddWithValue("@GROSSSALARY", GROSSSALARY);
                    command.Parameters.AddWithValue("@LOAN", LOAN);
                    command.Parameters.AddWithValue("@SALAADVANCE", SALAADVANCE);
                    command.Parameters.AddWithValue("@MEALS", MEALS);
                    command.Parameters.AddWithValue("@DATA", DATA);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@NOPAY", NOPAY);
                    command.Parameters.AddWithValue("@STAFFWELFAIR", STAFFWELFAIR);
                    command.Parameters.AddWithValue("@CONTRIBUTION", CONTRIBUTION);
                    command.ExecuteNonQuery();
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
                _dbConnection.Close();
            }

        }
        public DataTable GetEmpDetail(int EmpId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("pay_GetEmpDetails", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@EmpId", EmpId);
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


        /// <summary>
        /// Adds the additional information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID. AS Long</param>
        public void AddEmployeePay(long EmployeeID, decimal BasicSalary, decimal FixedAllowance, decimal DailyWage, bool StopPay, int PayPeriodCategoryId, bool Posted, string CurrencyCode, decimal MaxAdvancePer, bool BankTranferRequired,bool Hold, string EncryptionKey,int EPFRate1,int EPFRate2,int EPFRate3, int EPFRate4, bool Isepf)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeePay", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@BasicSalary", BasicSalary);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@FixedAllowance", FixedAllowance);
                    command.Parameters.AddWithValue("@DailyWage", DailyWage);
                    command.Parameters.AddWithValue("@StopPay", StopPay);
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@MaxAdvancePer", MaxAdvancePer);
                    command.Parameters.AddWithValue("@BankTranferRequired", BankTranferRequired);
                    command.Parameters.AddWithValue("@Hold", Hold);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
                    command.Parameters.AddWithValue("@EPFRate1", EPFRate1);
                    command.Parameters.AddWithValue("@EPFRate2", EPFRate2);
                    command.Parameters.AddWithValue("@EPFRate3", EPFRate3);
                    command.Parameters.AddWithValue("@EPFRate4", EPFRate4);
                    command.Parameters.AddWithValue("@EPF", Isepf);
                    command.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlExp)
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
                _dbConnection.Close();
            }

        }

        public void AddEmployeePayWhenAddEmployee(long EmployeeID, decimal BasicSalary, decimal FixedAllowance, decimal DailyWage, bool StopPay, int PayPeriodCategoryId, bool Posted, string CurrencyCode, decimal MaxAdvancePer, bool BankTranferRequired, bool Hold, string EncryptionKey, int EPFRate1, int EPFRate2, int EPFRate3, int EPFRate4)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeePayWhenAddEmployee", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@BasicSalary", BasicSalary);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@FixedAllowance", FixedAllowance);
                    command.Parameters.AddWithValue("@DailyWage", DailyWage);
                    command.Parameters.AddWithValue("@StopPay", StopPay);
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@MaxAdvancePer", MaxAdvancePer);
                    command.Parameters.AddWithValue("@BankTranferRequired", BankTranferRequired);
                    command.Parameters.AddWithValue("@Hold", Hold);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
                    command.Parameters.AddWithValue("@EPFRate1", EPFRate1);
                    command.Parameters.AddWithValue("@EPFRate2", EPFRate2);
                    command.Parameters.AddWithValue("@EPFRate3", EPFRate3);
                    command.Parameters.AddWithValue("@EPFRate4", EPFRate4);
                    command.ExecuteNonQuery();
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
                _dbConnection.Close();
            }

        }

        /// <summary>
        /// Updates the additional information.
        /// </summary>
        /// <param name="EmoloyeeID">The emoloyee ID. As Long</param>
        public void UpdateEmployeePay(long EmployeeID, decimal BasicSalary, decimal FixedAllowance, decimal DailyWage, bool StopPay, int PayPeriodCategoryId, bool Posted, string CurrencyCode, decimal MaxAdvancePer, bool BankTranferRequired, bool Hold, string EncryptionKey, int PayPeriodId,bool Isepf,int CostCenter)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_UpdateEmployeePay", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@BasicSalary", BasicSalary);
                    command.Parameters.AddWithValue("@FixedAllowance", FixedAllowance);
                    command.Parameters.AddWithValue("@DailyWage", DailyWage);
                    command.Parameters.AddWithValue("@StopPay", StopPay);
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@MaxAdvancePer", MaxAdvancePer);
                    command.Parameters.AddWithValue("@BankTranferRequired", BankTranferRequired);
                    command.Parameters.AddWithValue("@Hold", Hold);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EPF", Isepf);
                    command.Parameters.AddWithValue("@CostCenter", CostCenter);
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

        public void DeleteEmployeePay(long EmployeeID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_DeleteEmployeePay", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
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
                _dbConnection.Close();
            }
        }

       public void AddChangePayFieldValue(long EmployeeID,int PayPeriodId,string FieldName,string Percentage,string Amount,bool isPercentage,bool isFixfield,string EncryptKey,string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddChangePayFieldValue", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@FieldName", FieldName);
                    command.Parameters.AddWithValue("@Percentage", Percentage);
                    command.Parameters.AddWithValue("@Amount", Amount);
                    command.Parameters.AddWithValue("@isPercentage", isPercentage);
                    command.Parameters.AddWithValue("@isFixfield", isFixfield);
                    command.Parameters.AddWithValue("@EncryptKey", EncryptKey);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
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
                _dbConnection.Close();
            }
        }

       public void UpdateChangePayFieldValue(int Id, string Percentage, string Amount, bool isPercentage, string EncryptKey, string CreatedUser)
       {
           try
           {
               using (SqlCommand command = new SqlCommand("PAY_UpdateChangePayFieldValue", _dbConnection))
               {

                   command.CommandType = CommandType.StoredProcedure;
                   OpenDB();
                   command.Parameters.AddWithValue("@Id", Id);
                   command.Parameters.AddWithValue("@Percentage", Percentage);
                   command.Parameters.AddWithValue("@Amount", Amount);
                   command.Parameters.AddWithValue("@isPercentage", isPercentage);
                   command.Parameters.AddWithValue("@EncryptKey", EncryptKey);
                   command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
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
               _dbConnection.Close();
           }
       }

       public void DeleteChangePayFieldValue(int Id)
       {
           try
           {
               using (SqlCommand command = new SqlCommand("PAY_DeleteChangePayFieldValue", _dbConnection))
               {

                   command.CommandType = CommandType.StoredProcedure;
                   OpenDB();
                   command.Parameters.AddWithValue("@Id", Id);
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
               _dbConnection.Close();
           }
       }

        /// <summary>
        /// Gets the additonal information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID . As Long</param>
        /// <returns></returns>
        public DataTable GetEmpoyeePay(long EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetEmployeePayByEmployeeId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
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

        public DataTable GetPayrollUnassignedEmployee(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_UnassignedEmployee", _dbConnection))
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
                _dbConnection.Close();

            }
            return dtTable;
        }

        public DataTable GetUserDefinedReport(int OrgStructureID, int CompanyId, int PayPeriodId,bool Active, string EncryptionKey)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetUserDefinedReport", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@Active", Active);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
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

        public DataTable GetEmployeeUserDefinedReport(int OrgStructureID, int CompanyId, bool Active)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeUserDefinedReport", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Active", Active);
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

        public DataTable GetPermissionViseEmpoyeePay(long EmployeeID, int CompanyID, int PayPeriodCatageryId, string UserName, string ApplicationCode, string EncryptionKey,int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetEmployeePayByEmployeeIdForPaySetup", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    command.Parameters.AddWithValue("@PayPeriodCatageryId", PayPeriodCatageryId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                   // command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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
    
        public void AddEmployeeTransactions(int PayPeriodId, long EmployeeId, string FieldType, decimal Value)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeTransactions", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FieldType", FieldType);
                    command.Parameters.AddWithValue("@Value", Value);              
                    command.ExecuteNonQuery();
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
                _dbConnection.Close();
            }

        }

        public DataTable GetEmpoyeePayByPayPeriod(int PayPeriodCategoryId, string CurrencyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeePayByPayPeriod", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
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

        public DataTable GetEmployeesByPayroll(int PayCategoryId, long EmployeeId, string FieldName, string OrganizationList, string DesignationList, int CompanyId, int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeesByPayroll", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FieldName", FieldName);
                    command.Parameters.AddWithValue("@OrganizationFilterQuery", OrganizationList);
                    command.Parameters.AddWithValue("@DesignationFilterQuery", DesignationList);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {

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

        public DataTable GetChangePayFieldValue(int PayCategoryId, int PayPeriodId, string EncryptKey)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetChangePayFieldValue", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EncryptKey", EncryptKey);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {

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

        public DataTable GetFixFields()
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetFixFields", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {

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

        public DataTable GetVariableFields()
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetVariableFields", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {

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

        public void AddEmployeeStaticData(long EmployeeId, string FieldName, decimal Value, string EncryptionKey, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeStaticData", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FieldName", FieldName);
                    command.Parameters.AddWithValue("@Value", Value);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
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

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
        /// </summary>
        ~EmployeePayDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
    