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
/// Class Description   : Posting [Main Processing/Unprocessing]
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
    /// <summary>
    /// Posting Class(Main Processing/Unprocessing)
    /// </summary>
    public class PostingDAL
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
        public PostingDAL()
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

        public void GetTimeAllTimeLogs(long EmployeeId, string CardDate, string time, DateTime CardDateTime, int AttendancesType, string Location)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetDevicesAttLogs", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CardDate", CardDate);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@CardDateTime", CardDateTime);
                    command.Parameters.AddWithValue("@AttendancesType", AttendancesType);
                    command.Parameters.AddWithValue("@Location", Location);
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
        public DataTable GetDevicesIP(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Time_GetIPList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
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
        
        public DataTable GetPaySetup(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetPaySetup", _dbConnection))
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

        public DataTable GetEmployeeNotPostedList(int PayPeriodCategoryId, int PayPeriodId, int CompanyId, int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeNotPostList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
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

        public DataTable GetEmployeePostedList(int PayPeriodCategoryId, int PayPeriodId, int CompanyId, int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeePostList", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
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

        public DataTable GetEportExcelData(int PayPeriodId, string Status, string EncryptionKey)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEportExcelData", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@Status", Status);
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




        public void PostUnpostRecord(int PayPeriodId, long EmployeeId, bool Posted)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_PostUnpostRecord", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Posted", Posted);
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
       
        #region Functions
        public void Bsal(long EmployeeId, int PayPeriodId, string PaySetupCode, string EncryptionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_BSal_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
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


        public void Stamp(long EmployeeId, int PayPeriodId, string PaySetupCode, string EncryptionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_Stamp_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
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


        public void NoPay(long EmployeeId, int PayPeriodId, string PaySetupCode, string EncryptionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_NoPay_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
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

        public void PAYETAX(long EmployeeId, int PayPeriodId, string PaySetupCode, string EncryptionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_PAYETAX_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    //command.Parameters.AddWithValue("@TaxableValue ", TaxableValue);
                    //command.Parameters.AddWithValue("@TaxTypeId ", TaxTypeId);
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


        public void WELFARE(long EmployeeId, int PayPeriodId, string PaySetupCode, string EncryptionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_WELFARE_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    //command.Parameters.AddWithValue("@TaxableValue ", TaxableValue);
                    //command.Parameters.AddWithValue("@TaxTypeId ", TaxTypeId);
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

        //public void CoinCF(long EmployeeId, int PayPeriodId, string PaySetupCode)
        //{
        //    try
        //    {
        //        using (SqlCommand command = new SqlCommand("Pay_CoinCF_Function", _dbConnection))
        //        {
        //            OpenDb();
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
        //            command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
        //            command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _errorMsg = ex.Message;
        //    }
        //    finally
        //    {
        //        _dbConnection.Close();
        //    }
        //}

        public void CoinCF(long EmployeeId, int PayPeriodId, string PaySetupCode, decimal EstimateValue, int RoundingValue, string EncryptionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_CoinCF_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@EstimateValue", EstimateValue);
                    command.Parameters.AddWithValue("@RoundingValue", RoundingValue);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
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

        //public void CoinBF(long EmployeeId, int PayPeriodId, string PaySetupCode, decimal EstimateValue, int RoundingValue)
        //{
        //    try
        //    {
        //        using (SqlCommand command = new SqlCommand("Pay_COINBF_Function", _dbConnection))
        //        {
        //            OpenDb();
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
        //            command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
        //            command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
        //            command.Parameters.AddWithValue("@EstimateValue", EstimateValue);
        //            command.Parameters.AddWithValue("@RoundingValue", RoundingValue);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _errorMsg = ex.Message;
        //    }
        //    finally
        //    {
        //        _dbConnection.Close();
        //    }
        //}

        public void CoinBF(long EmployeeId, int PayPeriodId, string PaySetupCode)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_CoinBF_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
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

        public void Rounding(long EmployeeId, int PayPeriodId, string PaySetupCode, decimal OriginalValue, int RoundingValue)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_Rounding_Function", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@OriginalNumber", OriginalValue);
                    command.Parameters.AddWithValue("@RoundedNumber", RoundingValue);
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

        public void GrativityFund(long EmployeeId, int PayPeriodId, string PaySetupCode, bool Posted)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GrativityFund_Function", _dbConnection))
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

        public decimal GetPaySetupValue(long EmployeeId, int PayPeriodId, string PaySetupCode, bool Check)
        {
            decimal returnValue = 0M;
            string Value = string.Empty;
            string OT = ConfigurationManager.AppSettings["OT"];
            string Late = ConfigurationManager.AppSettings["LATE"];
            string OTRate = ConfigurationManager.AppSettings["OTRATE"];
            string OTHrs1 = ConfigurationManager.AppSettings["OTHRS1"];
            string LateRate = ConfigurationManager.AppSettings["LATERATE"];
            string LateHrs = ConfigurationManager.AppSettings["LateHrs"];
            DataTable dtTable = new DataTable();
            try
            {
                string Str = PaySetupCode;

                double Num;

                bool isNum = double.TryParse(Str, out Num);

                if (isNum)
                    using (SqlCommand command = new SqlCommand("Select " + PaySetupCode + " from Pay_PaySheet WHERE EmployeeId=" + EmployeeId + " AND  PayPeriodId=" + PayPeriodId, _dbConnection))
                    {
                        OpenDb();
                        command.CommandType = CommandType.Text;
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                        if (dtTable.Rows.Count > 0)
                        {
                            returnValue = Convert.ToDecimal(dtTable.Rows[0][0]);

                        }
                    }

                else
                {
                    using (SqlCommand command = new SqlCommand("Select DecryptedValue=Convert(varchar(100),DECRYPTBYPASSPHRASE('007London'," + PaySetupCode + "))  from Pay_PaySheet WHERE EmployeeId=" + EmployeeId + " AND  PayPeriodId=" + PayPeriodId, _dbConnection))
                    {
                        OpenDb();
                        command.CommandType = CommandType.Text;
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                        if (dtTable.Rows.Count > 0)
                        {
                            if (((OTRate == PaySetupCode) || (LateRate == PaySetupCode)) && Check==true)
                            {
                                returnValue = Convert.ToDecimal(dtTable.Rows[0][0]);

                                returnValue = returnValue / 60;
                            }
                            else if (((OTHrs1 == PaySetupCode) || (LateHrs == PaySetupCode)) && Check == true)
                            {
                                returnValue = Convert.ToDecimal(dtTable.Rows[0][0]);

                                var values = returnValue.ToString().Split('.');
                                int firstValue = 0;
                                int secondValue = 0;
                                string second;
                                if (values.Length == 2)
                                {
                                    firstValue = int.Parse(values[0]);
                                    firstValue = firstValue * 60;
                                    secondValue = int.Parse(values[1]);
                                    second = secondValue.ToString();
                                    if (second.Length > 1)
                                    {
                                        returnValue = firstValue + secondValue;
                                    }
                                    else
                                    {
                                        returnValue = firstValue + secondValue;
                                    }
                                }
                                else
                                {
                                    firstValue = int.Parse(values[0]);
                                    firstValue = firstValue * 60;
                                    returnValue = firstValue;
                                }


                            }
                            else
                            {
                                returnValue = Convert.ToDecimal(dtTable.Rows[0][0]);
                            }

                        }
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
                dtTable.Dispose();
                _dbConnection.Close();
            }
            return returnValue;
        }


        public decimal GetPayTableFieldValue(long EmployeeId, int PayPeriodId, string TableName, string FieldName, bool Check)
        {
            decimal returnValue = 0M;
            decimal balanceAmount = 0;
            decimal paysheetAmount = 0;
            decimal actualAmount = 0;
            string EncryptbalanceAmount = "";
            string EncryptpaysheetAmount = "";
            string EncryptactualAmount = "";
            string Value = string.Empty;
            DataTable dtTable = new DataTable();
            try
            {
                string sqlQuery = string.Empty;
                string updateQuery = string.Empty;
                //Percentage And amount get by below same queries.

                if (TableName == "Pay_EmployeeTxn")
                {
                    sqlQuery = "Select DecryptedValue=Convert(varchar(100),DECRYPTBYPASSPHRASE('007London',Pay_EmployeeTxn." + FieldName + ")),ChangeAmount=Convert(varchar(100),DECRYPTBYPASSPHRASE('007London',Pay_ChangePayFieldValue.ChangeAmount)),Percentage=Convert(varchar(100),DECRYPTBYPASSPHRASE('007London',Pay_ChangePayFieldValue.Percentage)),IsPercentage from " + TableName +
                        " LEFT OUTER JOIN Pay_ChangePayFieldValue on Pay_EmployeeTxn.PayPeriodId=Pay_ChangePayFieldValue.PayPeriodId And Pay_EmployeeTxn.EmployeeId=Pay_ChangePayFieldValue.EmployeeId And (Pay_ChangePayFieldValue.FieldName='" + FieldName +
                        "' or Pay_ChangePayFieldValue.FieldName=null) WHERE Pay_EmployeeTxn.EmployeeId=" + EmployeeId + " AND  Pay_EmployeeTxn.PayPeriodId=" + PayPeriodId;
                }
                else if (TableName == "Pay_EmplyoeePay")
                {
                    sqlQuery = "Select DecryptedValue=Convert(varchar(100),DECRYPTBYPASSPHRASE('007London',Pay_EmplyoeePay." + FieldName + ")),ChangeAmount=Convert(varchar(100),DECRYPTBYPASSPHRASE('007London',Pay_ChangePayFieldValue.ChangeAmount)),Percentage=Convert(varchar(100),DECRYPTBYPASSPHRASE('007London',Pay_ChangePayFieldValue.Percentage)),IsPercentage  from " + TableName +
                        " LEFT OUTER JOIN Pay_ChangePayFieldValue on Pay_EmplyoeePay.EmployeeId=Pay_ChangePayFieldValue.EmployeeId And Pay_ChangePayFieldValue.PayPeriodId=" + PayPeriodId +
                        " And (Pay_ChangePayFieldValue.FieldName='" + FieldName + "' or Pay_ChangePayFieldValue.FieldName=null) WHERE Pay_EmplyoeePay.EmployeeId=" + EmployeeId;
                }
                else
                {
                    sqlQuery = "Select " + FieldName + " from " + TableName + " WHERE EmployeeId=" + EmployeeId;
                }

                using (SqlCommand command = new SqlCommand(sqlQuery, _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.Text;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }

                if (dtTable.Rows.Count > 0)
                {
                    if (DBNull.Value.Equals(dtTable.Rows[0][1]) && DBNull.Value.Equals(dtTable.Rows[0][2]))
                    {
                        if (DBNull.Value.Equals(dtTable.Rows[0][0]))
                        {
                            returnValue = 0;
                        }
                        else
                        {
                            returnValue = Convert.ToDecimal(dtTable.Rows[0][0]);
                        }
                    }
                    else if (!DBNull.Value.Equals(dtTable.Rows[0][1]) || !DBNull.Value.Equals(dtTable.Rows[0][2]))
                    {
                        if (!DBNull.Value.Equals(dtTable.Rows[0][1]))
                        {
                            if (DBNull.Value.Equals(dtTable.Rows[0][0]))
                            {
                                actualAmount = 0;
                            }
                            else
                            {
                                actualAmount = Convert.ToDecimal(dtTable.Rows[0][0]);
                            }

                            paysheetAmount = Convert.ToDecimal(dtTable.Rows[0][1]);

                            balanceAmount = actualAmount - paysheetAmount;

                            returnValue = paysheetAmount;
                        }
                        else if (!DBNull.Value.Equals(dtTable.Rows[0][2]))
                        {
                            if (DBNull.Value.Equals(dtTable.Rows[0][0]))
                            {
                                actualAmount = 0;
                            }
                            else
                            {
                                actualAmount = Convert.ToDecimal(dtTable.Rows[0][0]);
                            }

                            if (actualAmount > 0)
                            {
                                paysheetAmount = (actualAmount / 100) * Convert.ToDecimal(dtTable.Rows[0][2]);
                            }

                            balanceAmount = actualAmount - paysheetAmount;

                            returnValue = paysheetAmount;
                        }

                        EncryptactualAmount = actualAmount.ToString();
                        EncryptbalanceAmount = balanceAmount.ToString();
                        EncryptpaysheetAmount = paysheetAmount.ToString();

                        updateQuery = "Update Pay_ChangePayFieldValue Set ActualAmount=EncryptByPassPhrase('007London', '" + EncryptactualAmount + "'),PaySheetAmount=EncryptByPassPhrase('007London', '" + EncryptpaysheetAmount + "'),BalanceAmount=EncryptByPassPhrase('007London', '" + EncryptbalanceAmount + "') Where EmployeeId=" + EmployeeId +
                            "And PayPeriodId="+PayPeriodId+" And FieldName='"+FieldName+"'";


                        using (SqlCommand command = new SqlCommand(updateQuery, _dbConnection))
                        {
                            OpenDb();
                            command.CommandType = CommandType.Text;
                            command.ExecuteNonQuery();
                        }
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
                dtTable.Dispose();
                _dbConnection.Close();
            }
            return returnValue;
        }
        public void SetPaySetupValue(long EmployeeId, int PayPeriodId, string PaySetupCode, decimal Amount, string EncryptionKey)
        {           
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_SetPaySetupValue", _dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@PaySetupCode", PaySetupCode);
                    command.Parameters.AddWithValue("@Amount", Amount);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
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

        #region Destructor
        ~PostingDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
