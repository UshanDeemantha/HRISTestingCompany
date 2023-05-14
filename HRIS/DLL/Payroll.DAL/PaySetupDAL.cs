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
/// Created Timestamp   : 7/13/2011
/// Class Description   : Payroll Setup Related Data
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
    ///<summary>
   ///Manage Payroll Setup Related Data
    /// </summary>
    public class PaySetupDAL
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
        public PaySetupDAL()
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
            _isSuccess = true ;
            _result = string.Empty;
        }

        private void SetError(SqlException Ex)
        {
            _isSuccess = false;
            _isError = true;
            _errorMsg = Ex.Message;
            _result = string.Empty;
            _errorNo = Ex.Number;
        }
        #endregion

        #region Pay Period Category   
        public DataTable Pay_GetUserAssignedPayCategory(int CompanyId, int PayPeriodCategoryId, string UserName)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetUserAssignedPayCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@UserName", UserName);
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

        public DataTable getSalControl(int OrgStructureID, int CompanyId, string EncryptionKey, int Month, int Year, string CatID, int OrgLevel, string UserName)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetSalaryControlCompanyWise", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
                    command.Parameters.AddWithValue("@UserName", UserName);
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

        public DataTable getSignatureList(int OrgStructureID, int CompanyId, string EncryptionKey, int Month, int Year, string CatID, string UserName, int OrgLevel, int SalaryType)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeSignatureList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
                    command.Parameters.AddWithValue("@SalaryType", SalaryType);
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

        public DataTable getSalBank(int OrgStructureID, int CompanyId, string EncryptionKey, int Month, int Year, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeSalarytoBankList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getMaleFemale(int OrgStructureID, int CompanyId, string CatID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetMaleFemaleRatio", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@CatID", CatID);
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

        public DataTable getMaleFemaleSummary(int OrgStructureID, int CompanyId, string CatID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetMaleFemaleRatioSummary", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@CatID", CatID);
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

        public DataTable getWCISummary(int OrgStructureID, int CompanyId, string EncryptionKey, string CatID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetWCISummary", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
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

        public DataTable getEPFCform(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetCFormDetails", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getEmployeePaySheet(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel, int EmployeeId, DateTime PaidDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetOfficeStaffPayDetailsPaySheet", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PaidDate", PaidDate);
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

        public DataTable getEmployeePaySlip(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel, int EmployeeId, DateTime PaidDate)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetOfficeStaffPayDetailsPaySheet", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PaidDate", PaidDate);
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

        public DataTable getETFCform(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetETFCForm", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getEmployeeContributionA4Report(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, string FieldStatus, int Semester, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEPFandETFA4Report", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@FieldStatus", Month);
                    command.Parameters.AddWithValue("@Semester", Month);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getEPFReport(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetETFCForm", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getETFReport(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetETFCForm", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getETFETFStatement(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetETFCForm", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable GetPayPeriodCategory(int CompanyId, int PayPeriodCategoryId, string UserName)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@UserName", UserName);
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

        public DataTable GetPayPeriodCategory(int CompanyId, int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetAllPayCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
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

        public void AddPayPeriodCategory(int CompanyId, string PayPeriodCategory, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddPayPeriodCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@PayPeriodCategory", PayPeriodCategory);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
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

        public void UpdatePayPeriodCategory(int PayPeriodCategoryId, int CompanyId, string PayPeriodCategory, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdatePayPeriodCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@PayPeriodCategory", PayPeriodCategory);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
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

        public void DeletePayPeriodCategory(int PayPeriodCategoryId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeletePayPeriodCategory", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
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

        public DataTable getStampDuty(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetStampDutyReport", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getStampDutyMonthly(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetStampDutyReportwithGrossandNetAmount", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getStampDutySemester(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, int Semester, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetStampDutyReportSemesterly", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Semester", Semester);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

        public DataTable getStampDutyQuarter(int OrgStructureID, int CompanyId, string EncryptionKey, int Year, int EmployeeId, int Month, int Quarter, string CatID, string UserName, int OrgLevel)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetStampDutyReportQuarterly", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", CatID);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Quarter", Quarter);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OrgLevel", OrgLevel);
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

            #region Pay Periods

            public DataTable GetPayPeriod(int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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

        public DataTable GetPayPeriods(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayPeriods", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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

        public DataTable GetPreviousPayPeriods(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPrvsPayPeriods", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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

        public DataTable GetPaySetupCodes(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayPeriodCdesByPayCategory", _dbConnection))
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
       
        public int GetPayPeriodsByPayPeriodCode(string PayPeriodCode)
        {
           // DataTable dtTable = new DataTable();
            int PayPeriodId = 0;
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayPeriodIdByCode", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCode", PayPeriodCode);
                    PayPeriodId = Convert.ToInt32(command.ExecuteScalar());

                    //using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    //{
                    //    daAdapter.Fill(dtTable);
                    //}
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
            return PayPeriodId;
        }

        public DataTable GetUnCompletedPayPeriods(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetUnCompletedPayPeriods", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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
        
        public DataTable GetHistoryPayPeriod(int PayPeriodCategoryId, int PayPeriodId, int Year, int Month)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetHistoryPayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Month", Month);
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

        public void AddPayPeriod(int PayPeriodCategoryId, string PayPeriodName, DateTime BeginDate, DateTime EndDate, int Month, int Year, string CurrencyCode, int PreviousPayPeriodId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddPayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodName", PayPeriodName);
                    command.Parameters.AddWithValue("@BeginDate", BeginDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@PreviousPayPeriodId", PreviousPayPeriodId);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 500);
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

        public void UpdatePayPeriod(int PayPeriodId, int PayPeriodCategoryId, string PayPeriodName, DateTime BeginDate, DateTime EndDate, int Month, int Year, string CurrencyCode, int PreviousPayPeriodId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdatePayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodName", PayPeriodName);
                    command.Parameters.AddWithValue("@BeginDate", BeginDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    command.Parameters.AddWithValue("@PreviousPayPeriodId", PreviousPayPeriodId);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 500);
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

        public void DeletePayPeriod(int PayPeriodId, int PreviousPayPeriodId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeletePayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PreviousPayPeriodId", PreviousPayPeriodId);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 500);
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

        public void PostPayPeriod(int PayPeriodId, bool Posted, string UserName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_PostPayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@UserName", UserName);
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

        public void MonthEndPayPeriod(int PayPeriodId, string UserName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_MonthEndPayPeriod", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@UserName", UserName);
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
        #endregion

        #region UserFields
        public DataTable GetUserFields(int FieldId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayFields", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FieldId", FieldId);
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

        public DataTable GetUserFields(string FieldType, int FieldId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayFieldsByType", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FieldType", FieldType);
                    command.Parameters.AddWithValue("@FieldId", FieldId);
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

        public void AddUserField(string FieldCode, string FieldDescription, string FieldType, string DataType, int Length, int DecimalPoints, bool IsNull, bool IsDeduction, bool IsLoanType, string DefaultValue, string CheckConstraints)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddUserField", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FieldCode", FieldCode);
                    command.Parameters.AddWithValue("@FieldDescription", FieldDescription);
                    command.Parameters.AddWithValue("@FieldType", FieldType);
                    command.Parameters.AddWithValue("@DataType", DataType);
                    command.Parameters.AddWithValue("@Length", Length);
                    command.Parameters.AddWithValue("@DecimalPoints", DecimalPoints);
                    command.Parameters.AddWithValue("@IsNull", IsNull);
                    command.Parameters.AddWithValue("@DefaultValue", DefaultValue);
                    command.Parameters.AddWithValue("@CheckConstraints", CheckConstraints);
                    command.Parameters.AddWithValue("@IsDeduction", IsDeduction);
                    command.Parameters.AddWithValue("@IsLoanType", IsLoanType);

                    SqlParameter success = new SqlParameter("@Success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _isSuccess = Convert.ToBoolean(success.Value);
                    _result = result.Value.ToString();
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

        public void UpdateUserField(int FieldId, string FieldCode, string FieldDescription, string FieldType, string DataType, int Length, int DecimalPoints, bool IsNull, bool IsDeduction, bool IsLoanType, string DefaultValue, string CheckConstraints)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateUserField", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FieldId", FieldId);
                    command.Parameters.AddWithValue("@FieldCode", FieldCode);
                    command.Parameters.AddWithValue("@FieldDescription", FieldDescription);
                    command.Parameters.AddWithValue("@FieldType", FieldType);
                    command.Parameters.AddWithValue("@DataType", DataType);
                    command.Parameters.AddWithValue("@Length", Length);
                    command.Parameters.AddWithValue("@DecimalPoints", DecimalPoints);
                    command.Parameters.AddWithValue("@IsNull", IsNull);
                    command.Parameters.AddWithValue("@DefaultValue", DefaultValue);
                    command.Parameters.AddWithValue("@CheckConstraints", CheckConstraints);
                    command.Parameters.AddWithValue("@IsDeduction", IsDeduction);
                    command.Parameters.AddWithValue("@IsLoanType", IsLoanType);

                    SqlParameter success = new SqlParameter("@Success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _isSuccess = Convert.ToBoolean(success.Value);
                    _result = result.Value.ToString();
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


        public void DeleteUserField(int FieldId,string FieldCode, string FieldType, string DataType)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeleteUserField", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FieldId", FieldId);
                    command.Parameters.AddWithValue("@FieldCode", FieldCode);
                    command.Parameters.AddWithValue("@FieldType", FieldType);
                    command.Parameters.AddWithValue("@DataType", DataType);

                    SqlParameter success = new SqlParameter("@Success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _isSuccess = Convert.ToBoolean(success.Value);
                    _result = result.Value.ToString();
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
        #endregion

        #region Payroll Field
        public DataTable GetPayrollFields(int PayCategoryId, int SequenceNo)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayrollFields", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@SequenceNo", SequenceNo);
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

        public DataTable GetPayrollFields(int IndexId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayrollFieldByIndex", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IndexId", IndexId);                   
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

        public DataTable GetPayrollFunctions()
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPayrollFunctions", _dbConnection))
                {
                    OpenDb();
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
                _dbConnection.Close();
            }
            return dtTable;
        }


        public void PaySetupRowMove(long IndexId, string Status)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_PaySetupRowMove", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IndexId", IndexId);
                    command.Parameters.AddWithValue("@Status", Status);
                    
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

        public void AddPayrollField(int PayCategoryId, string PaySetupCode, string PaySetupDescription, string Formula, int SeqNo, int DependOn, bool IsDeduct, string FunctionName, string ParameterList)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddPayrollField", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@PaySetupDescription", PaySetupDescription);
                    command.Parameters.AddWithValue("@Formula", Formula);
                    command.Parameters.AddWithValue("@SeqNo", SeqNo);
                    command.Parameters.AddWithValue("@DependOn", DependOn);
                    command.Parameters.AddWithValue("@IsDeduct", IsDeduct);
                    command.Parameters.AddWithValue("@FunctionName", FunctionName);
                    command.Parameters.AddWithValue("@ParameterList", ParameterList);
                  
                    SqlParameter success = new SqlParameter("@Success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _isSuccess = Convert.ToBoolean(success.Value);
                    _result = result.Value.ToString();
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

        public void UpdatePayrollField(int IndexId, string OLDPayCode, int PayCategoryId, string PaySetupCode, string PaySetupDescription, string Formula, int SeqNo, int DependOn, bool IsDeduct, string FunctionName, string ParameterList)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_UpdatePayrollField", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IndexId", IndexId);
                    command.Parameters.AddWithValue("@OLDPayCode", OLDPayCode);
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@PaySetupDescription", PaySetupDescription);
                    command.Parameters.AddWithValue("@Formula", Formula);
                    command.Parameters.AddWithValue("@SeqNo", SeqNo);
                    command.Parameters.AddWithValue("@DependOn", DependOn);
                    command.Parameters.AddWithValue("@IsDeduct", IsDeduct);
                    command.Parameters.AddWithValue("@FunctionName", FunctionName);
                    command.Parameters.AddWithValue("@ParameterList", ParameterList);

                    SqlParameter success = new SqlParameter("@Success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _isSuccess = Convert.ToBoolean(success.Value);
                    _result = result.Value.ToString();
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

        public void DeletePayrollField(int IndexId, int PayCategoryId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_DeletePayrollField", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IndexId", IndexId);
                    command.Parameters.AddWithValue("@PayCategoryId", PayCategoryId);
                 
                    SqlParameter success = new SqlParameter("@Success", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _isSuccess = Convert.ToBoolean(success.Value);
                    _result = result.Value.ToString();
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
        #endregion

        #region Application Sessions
        public DataTable GetApplicationSession(int PayPeriodCategoryId, int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetApplicationSession", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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

        public void SetApplicationSession(int PayPeriodCategoryId, int PayPeriodId, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_SetApplicationSession", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
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

        public void SetApplicationSession(int PayPeriodCategoryId, int PayPeriodId, bool IsPosted, bool IsArchive, bool IsHistory)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_SetApplicationSessionStatus", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@IsPosted", IsPosted);
                    command.Parameters.AddWithValue("@IsArchive", IsArchive);
                    command.Parameters.AddWithValue("@IsHistory", IsHistory);
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


        #endregion

        #region Destructor
        ~PaySetupDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
