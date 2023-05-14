
 
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.Payroll.DAL
{    
    public class EmployeeTransactionDAL
    {
        #region Fields
        private SqlConnection dbConnection;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private long _employeeId;
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
        public long EmployeeId
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
        #endregion

        #region Constructor
        public EmployeeTransactionDAL()
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
        /// <summary>
        /// Adds the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="AttendenceAllowans">The attendence allowans.</param>
        /// <param name="NoPayDays">The no pay days.</param>
        /// <param name="OTRate1">The OT rate1.</param>
        /// <param name="OTHRS1">The OTHR s1.</param>
        /// <param name="OTRate2">The OT rate2.</param>
        /// <param name="OTHrs2">The OT HRS2.</param>
        /// <param name="OTRate3">The OT rate3.</param>
        /// <param name="OTHrs3">The OT HRS3.</param>
        /// <param name="EPF">The EPF.</param>
        /// <param name="PayeTax">The paye tax.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        public void AddEmployeeTxn(long EmployeeId, int PayPeriodId, decimal AttendenceAllowans, decimal NoPayDays, decimal OTRate1, decimal OTHRS1, decimal OTRate2, decimal OTHrs2, decimal OTRate3, decimal OTHrs3, bool IsPosted)
        {
            InitializeFields();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeTransAction", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@AttendenceAllowans", AttendenceAllowans);
                    command.Parameters.AddWithValue("@NoPayDays", NoPayDays);
                    command.Parameters.AddWithValue("@OTRate1", OTRate1);
                    command.Parameters.AddWithValue("@OTHRS1", OTHRS1);
                    command.Parameters.AddWithValue("@OTRate2", OTRate2);
                    command.Parameters.AddWithValue("@OTHrs2", OTHrs2);
                    command.Parameters.AddWithValue("@OTRate3", OTRate3);
                    command.Parameters.AddWithValue("@OTHrs3", OTHrs3);
                    command.Parameters.AddWithValue("@IsPosted", IsPosted);
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
                dbConnection.Close();
            }

        }

        public DataTable GetPayEmployeeRecordsDatail(int Year, int Month, int CompanyId, string EncryptionKey, string catList)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetCompanyEmployeePayrollDatail", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", catList);
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
        public DataTable GetEPFETFCompanyDetails(int companyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetCompanyETFEPFDatail", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyID", companyId);

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
        public DataTable GetCompanyEmployeeRecordsDatail(int Year, int Month, int CompanyId, string EncryptionKey, string catList)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetEPFTest", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", catList);
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

        public DataTable GetCompanyEmployeeETFRecords(int Year, int Month, int CompanyId, string EncryptionKey, string catList)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetETFTest", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CatID", catList);
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
        
             public DataTable CheckForStandingOrder(Int64 EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetStandingOrder", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                 

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
        public DataTable GetCompanyBankDetails(int companyId, int BankID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetCompanyBankDatail", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyID", companyId);
                    command.Parameters.AddWithValue("@Id", BankID);

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


        public DataTable GetBankCode(int bankId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetBankCode", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", bankId);
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

        public void AddEmployeeTxn(long EmployeeId, int PayPeriodId, decimal AttendenceAllowans, decimal NoPayDays,decimal lateHrs,
                                                                                     decimal OTRate1, decimal OTHRS1, decimal OTRate2, decimal OTHrs2, decimal OTRate3, decimal OTHrs3, bool IsPosted)
        {
            InitializeFields();
            try
            {
                //using (SqlCommand command = new SqlCommand("PAY_AddEmployeeAllTransAction", dbConnection))
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeTransAction", dbConnection))
                    
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@AttendenceAllowans", AttendenceAllowans);
                    command.Parameters.AddWithValue("@NoPayDays", NoPayDays);
                    command.Parameters.AddWithValue("@LATEHRS", lateHrs);
                    command.Parameters.AddWithValue("@OTRate1", OTRate1);
                    command.Parameters.AddWithValue("@OTHRS1", OTHRS1);
                    command.Parameters.AddWithValue("@OTRate2", OTRate2);
                    command.Parameters.AddWithValue("@OTHrs2", OTHrs2);
                    command.Parameters.AddWithValue("@OTRate3", OTRate3);
                    command.Parameters.AddWithValue("@OTHrs3", OTHrs3);
                    command.Parameters.AddWithValue("@IsPosted", IsPosted);
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
                dbConnection.Close();
            }

        }

        /// <summary>
        /// Updates the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="AttendenceAllowans">The attendence allowans.</param>
        /// <param name="NoPayDays">The no pay days.</param>
        /// <param name="OTRate1">The OT rate1.</param>
        /// <param name="OTHRS1">The OTHR s1.</param>
        /// <param name="OTRate2">The OT rate2.</param>
        /// <param name="OTHrs2">The OT HRS2.</param>
        /// <param name="OTRate3">The OT rate3.</param>
        /// <param name="OTHrs3">The OT HRS3.</param>
        /// <param name="EPF">The EPF.</param>
        /// <param name="PayeTax">The paye tax.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        public void UpdateEmployeeTxn(long EmployeeId, int PayPeriodId, decimal AttendenceAllowans, decimal NoPayDays, decimal PayLateHrs, decimal OTRate1, decimal OTHRS1, decimal OTRate2, decimal OTHrs2, decimal OTRate3, decimal OTHrs3, bool IsPosted)
        {
            try
            {
                InitializeFields();
                using (SqlCommand command = new SqlCommand("PAY_UpdateEmployeeTransAction", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@AttendenceAllowans", AttendenceAllowans);
                    command.Parameters.AddWithValue("@NoPayDays", NoPayDays);
                    command.Parameters.AddWithValue("@LATEHRS", PayLateHrs);
                    command.Parameters.AddWithValue("@OTRate1", OTRate1);
                    command.Parameters.AddWithValue("@OTHRS1", OTHRS1);
                    command.Parameters.AddWithValue("@OTRate2", OTRate2);
                    command.Parameters.AddWithValue("@OTHrs2", OTHrs2);
                    command.Parameters.AddWithValue("@OTRate3", OTRate3);
                    command.Parameters.AddWithValue("@OTHrs3", OTHrs3);
                    command.Parameters.AddWithValue("@IsPosted", IsPosted);
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

        /// <summary>
        /// Deletes the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        public void DeleteEmployeeTxn(long EmployeeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_DeleteEmployeeTransAction", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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
        
        /// <summary>
        /// Gets the employee TXN.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <returns></returns>
        public DataTable GetEmployeeTxn(int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetEmployeeTransAction", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        public DataTable GetEmployeeTxn(int PayCategoryId, string FieldName, string organizationFilterQuery, string designationFilterQuery, int CompanyId, int OrgStructureId,int PayPeriodId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetEmployeeTxnByFieldName", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayCategoryId);
                    command.Parameters.AddWithValue("@FieldName", FieldName);
                    command.Parameters.AddWithValue("@OrganizationFilterQuery", organizationFilterQuery);
                    command.Parameters.AddWithValue("@DesignationFilterQuery", designationFilterQuery);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureId);
                    command.Parameters.AddWithValue("@EncryptionKey", "007London");
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
                dbConnection.Close();

            }
            return dtTable;
        }

        public void AddEmployeeTxn(long EmployeeId, int PayPeriodId, string FieldName, decimal Value, string EncryptionKey)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeTxnByDynamicField", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@FieldName", FieldName);
                    command.Parameters.AddWithValue("@Value", Value);
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
                dbConnection.Close();

            }
        }


        public DataTable GetEmployeePayTransactions(int orStruct)
        {
            DataTable dtTable = new DataTable();
            try
            {
              
                string sqlQuery = "";
                
                    sqlQuery += " WITH OrgStruc AS( "
                    + " select OrgStructureID, ParentID,LevelIndex,OrganizationTypeID,CompanyId from OrganisationStructure WHERE CompanyId=1"
                     + " AND OrgStructureID=" + orStruct + " UNION ALL "
                      + " select OG.OrgStructureID,OG.ParentID,OG.LevelIndex,OG.OrganizationTypeID,OG.CompanyId from OrganisationStructure OG "
                       + " INNER JOIN OrgStruc  OS ON OG.ParentID=OS.OrgStructureID ) ";




                    sqlQuery += " SELECT        (dbo.Employee.EmployeeCode +'/'+ dbo.Employee.FullName) as EmployeeCode,Pay_EmplyoeePay.* "

                                               + "  FROM dbo.Employee "
                                                     + " INNER JOIN Pay_EmplyoeePay  ON Pay_EmplyoeePay.EmployeeId=dbo.Employee.EmployeeId ";
                                                      

              

                
                    sqlQuery += "AND OrgStructureID IN(SELECT OrgStructureID FROM OrgStruc )  ";
               

                sqlQuery += " ORDER BY dbo.Employee.EmployeeCode ASC ";

                using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@OrgStuctureID", orStruct);
                  
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

        #region Payroll Data
        public DataTable GetEmployeePayroll(int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetPaysheetByPayPeriodId", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    // command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        public DataTable GetEmployeePayrollDatail(int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetEmployeePayrollDatail", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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

        public DataTable GetPayrollSummery(int PayPeriodId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_GetPayrollSummery", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayPeriodId", PayPeriodId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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
        #endregion

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
        /// </summary>
        ~EmployeeTransactionDAL()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}