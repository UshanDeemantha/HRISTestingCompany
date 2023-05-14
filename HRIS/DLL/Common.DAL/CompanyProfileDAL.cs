/// <summary>
/// Solution Name       : HRM
/// Project Name        : Common.DAL
/// Author              : ushan deemantha
/// Class Description   : Company Profile DAL
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HRM.Common.DAL
{
    
   public class CompanyProfileDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;
        private CompanyProfileDAL objFluencyDAL=null;
        private int _companyId = 0;
        private int _companybranchId = 0;
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

        public int CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }


        public int CompanyBranchId
        {
            get { return _companybranchId; }
        }


        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Fluency"/> class.
        /// </summary>
       public CompanyProfileDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        } 
        #endregion



       #region Methods
       #region Internal
       private void OpenDB()
       {
           if (_dbConnection.State != ConnectionState.Open)
           {
               _dbConnection.Open();
           }
           InitializeErrorFields();
       }

       private void InitializeErrorFields()
       {
           _isError = false;
           _errorMsg = string.Empty;
       }
       #endregion
       #region Company Profile


       /// <summary>
       /// Adds the company profile.
       /// </summary>
       /// <param name="CompanyCode">The company code As string.</param>
       /// <param name="CompanyName">Name of the company as string.</param>
       /// <param name="CompanyAddress">The company address as string.</param>
       /// <param name="CompanyContactNo1">The company contact no1 as string.</param>
       /// <param name="CompanyContactNo2">The company contact no2 as string.</param>
       /// <param name="CompanyFax">The company fax as string.</param>
       /// <param name="CompanyEmail">The company email as string.</param>
       /// <param name="CompanyWeb">The company web. as string</param>
       /// <param name="CompanyRegistrationNo">The company registration no as string.</param>
       /// <param name="CompanyTaxRegistrationNo">The company tax registration no as string.</param>
       public void AddCompanyProfile(string CompanyCode, string CompanyName, string CompanyAddress, string CompanyContactNo1, string CompanyContactNo2, string CompanyFax, string CompanyEmail, string CompanyWeb, 
           string CompanyRegistrationNo, string CompanyTaxRegistrationNo, bool EmployeeNameStatus,
           string CompanyAccountNo, int BankId, int BranchId, string CompanyAccountNo2, int BankId2, int BranchId2, bool IsCustomEmployeeCode, string Prefix, int EmployeeReportViewName)
       {
           try
           {
               using (SqlCommand command = new SqlCommand("COM_AddCompanyProfile", _dbConnection))
               {
                   OpenDB();
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyCode", CompanyCode);
                   command.Parameters.AddWithValue("@CompanyName", CompanyName);
                   command.Parameters.AddWithValue("@CompanyAddress", CompanyAddress);
                   command.Parameters.AddWithValue("@CompanyContactNo1", CompanyContactNo1);
                   command.Parameters.AddWithValue("@CompanyContactNo2", CompanyContactNo2);
                   command.Parameters.AddWithValue("@CompanyFax", CompanyFax);
                   command.Parameters.AddWithValue("@CompanyEmail", CompanyEmail);
                   command.Parameters.AddWithValue("@CompanyWeb", CompanyWeb);
                   command.Parameters.AddWithValue("@CompanyRegistrationNo", CompanyRegistrationNo);
                   command.Parameters.AddWithValue("@CompanyTaxRegistrationNo", CompanyTaxRegistrationNo);
                   command.Parameters.AddWithValue("@EmployeeNameStatus", EmployeeNameStatus);
                   command.Parameters.AddWithValue("@CompanyAccountNo", CompanyAccountNo);
                   command.Parameters.AddWithValue("@BankId", BankId);
                   command.Parameters.AddWithValue("@BranchId", BranchId);
                   command.Parameters.AddWithValue("@CompanyAccountNo2", CompanyAccountNo2);
                   command.Parameters.AddWithValue("@BankId2", BankId2);
                   command.Parameters.AddWithValue("@BranchId2", BranchId2);
                   command.Parameters.AddWithValue("@IsCustomEmployeeCode", IsCustomEmployeeCode);
                   command.Parameters.AddWithValue("@Prefix", Prefix);
                   command.Parameters.AddWithValue("@EmployeeReportViewName", EmployeeReportViewName); 
                   SqlParameter companyId = new SqlParameter("@CompanyId", SqlDbType.Int, 8);
                   companyId.Direction = ParameterDirection.Output;
                   command.Parameters.Add(companyId);

                   command.ExecuteNonQuery();
                   _companyId = Convert.ToInt32(companyId.Value);
                   if (_companyId < 0)
                   {
                       _isError = true;
                       _errorMsg = "Record Already Exists!";
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
       }

       /// <summary>
       /// Adds the company profile.
       /// </summary>
       /// <param name="CompanyID">The company id As int.</param>
       /// <param name="CompanyCode">The company code As string.</param>
       /// <param name="CompanyName">Name of the company as string.</param>
       /// <param name="CompanyAddress">The company address as string.</param>
       /// <param name="CompanyContactNo1">The company contact no1 as string.</param>
       /// <param name="CompanyContactNo2">The company contact no2 as string.</param>
       /// <param name="CompanyFax">The company fax as string.</param>
       /// <param name="CompanyEmail">The company email as string.</param>
       /// <param name="CompanyWeb">The company web. as string</param>
       /// <param name="CompanyRegistrationNo">The company registration no as string.</param>
       /// <param name="CompanyTaxRegistrationNo">The company tax registration no as string.</param>
       public void UpdateCompanyProfile(int CompanyId, string CompanyCode, string CompanyName, string CompanyAddress, string CompanyContactNo1, string CompanyContactNo2, 
           string CompanyFax, string CompanyEmail, string CompanyWeb, string CompanyRegistrationNo, string CompanyTaxRegistrationNo,bool EmployeeNameStatus,
           string CompanyAccountNo, int BankId, int BranchId, string CompanyAccountNo2, int BankId2, int BranchId2, bool IsCustomEmployeeCode, string Prefix, int EmployeeReportViewName)
       {
           try
           {
               using (SqlCommand command = new SqlCommand("COM_UpdateCompanyProfile", _dbConnection))
               {
                   OpenDB();
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyId", CompanyId);
                   command.Parameters.AddWithValue("@CompanyCode", CompanyCode);
                   command.Parameters.AddWithValue("@CompanyName", CompanyName);
                   command.Parameters.AddWithValue("@CompanyAddress", CompanyAddress);
                   command.Parameters.AddWithValue("@CompanyContactNo1", CompanyContactNo1);
                   command.Parameters.AddWithValue("@CompanyContactNo2", CompanyContactNo2);
                   command.Parameters.AddWithValue("@CompanyFax", CompanyFax);
                   command.Parameters.AddWithValue("@CompanyEmail", CompanyEmail);
                   command.Parameters.AddWithValue("@CompanyWeb", CompanyWeb);
                   command.Parameters.AddWithValue("@CompanyRegistrationNo", CompanyRegistrationNo);
                   command.Parameters.AddWithValue("@CompanyTaxRegistrationNo", CompanyTaxRegistrationNo);
                   command.Parameters.AddWithValue("@EmployeeNameStatus", EmployeeNameStatus);
                   command.Parameters.AddWithValue("@CompanyAccountNo", CompanyAccountNo);
                   command.Parameters.AddWithValue("@BankId", BankId);
                   command.Parameters.AddWithValue("@BranchId", BranchId);
                   command.Parameters.AddWithValue("@CompanyAccountNo2", CompanyAccountNo2);
                   command.Parameters.AddWithValue("@BankId2", BankId2);
                   command.Parameters.AddWithValue("@BranchId2", BranchId2);
                   command.Parameters.AddWithValue("@IsCustomEmployeeCode", IsCustomEmployeeCode);
                   command.Parameters.AddWithValue("@Prefix", Prefix);
                   command.Parameters.AddWithValue("@EmployeeReportViewName", EmployeeReportViewName); 
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
        public DataTable GetCompanyCostCenters(int CompanyCostId)
        {

            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetCompanyCostCenter", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyCostID", CompanyCostId);
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
        public DataTable GetEmployeeCompanyDetails(int CompanyId)
        {

            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetCompanyName", _dbConnection))
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
        /// <summary>
        /// Gets the company profile.
        /// </summary>
        /// <param name="CompanyId">The company id as int.</param>
        /// <returns></returns>
        public DataTable GetCompanyProfile(int CompanyId)
       {

           DataTable dtTable = new DataTable();
           try
           {
               using (SqlCommand command = new SqlCommand("COM_GetCompanyProfile", _dbConnection))
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


       public DataTable GetCompanyBankDetails(int CompanyId)
       {

           DataTable dtTable = new DataTable();
           try
           {
               using (SqlCommand command = new SqlCommand("COM_GetCompanyBankDetails", _dbConnection))
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

        public void SaveCompanyBankDetails(int CompanyId, int BankId, int BranchId, string AccountNo, string Description, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_SaveCompanyBankDetails", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@AccountNo", AccountNo);
                    command.Parameters.AddWithValue("@Description", Description);
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
        public void SaveNewCompanyBankDetails(int CompanyId, int BankId, int BranchId, string AccountNo, string Discription, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_SaveNewCompanyBankDetails", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@AccountNo", AccountNo);
                    command.Parameters.AddWithValue("@Description", Discription);
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
        public void UpdateCompanyBankDetails(int Id, int BankId, int BranchId, string AccountNo, string Description, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateCompanyBankDetails", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@AccountNo", AccountNo);
                    command.Parameters.AddWithValue("@Description", Description);
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

        public DataTable GetCompanyBankDetailsFull(int CompanyId)
       {

           DataTable dtTable = new DataTable();
           try
           {
               using (SqlCommand command = new SqlCommand("COM_GetCompanyBankDetailsFull", _dbConnection))
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
        public DataTable GetCompanyBank(int BankId)
        {

            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetCompanyBankId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@BankId", BankId);
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
        public DataTable GetCompanyBranch(int BranchId)
        {

            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetCompanyBranchId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@BranchId", BranchId);
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
        /// <summary>
        /// Gets the compnay.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public DataTable GetCompany(int companyId)
       {
           DataTable dtTable = new DataTable();

           try
           {
               using (SqlCommand command = new SqlCommand("COM_GetCompanyProfile", _dbConnection))
               {
                   OpenDB();

                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyID", companyId);

                   using (SqlDataAdapter daAdaper = new SqlDataAdapter(command))
                   {
                       daAdaper.Fill(dtTable);
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

       public DataTable GetInactiveEmployeeStatus(int StatusId)
       {
           DataTable dtTable = new DataTable();

           try
           {
               using (SqlCommand command = new SqlCommand("COM_GetInactieEmployeeStatus", _dbConnection))
               {
                   OpenDB();

                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@StatusId", StatusId);

                   using (SqlDataAdapter daAdaper = new SqlDataAdapter(command))
                   {
                       daAdaper.Fill(dtTable);
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

       /// <summary>
       /// Deletes the company profile.
       /// </summary>
       /// <param name="CompanyId">The company id as int.</param>
       public void DeleteCompanyProfile(int CompanyId)
       {
           try
           {
               OpenDB();
               using (SqlCommand command = new SqlCommand("COM_DeleteCompanyProfile", _dbConnection))
               {
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyId", CompanyId);
                   command.ExecuteNonQuery();
               }
           }
           catch (SqlException ex)
           {
               _isError = true;
               if (ex.Number == 547)
               {
                   _errorMsg = "Already Assign This Code";
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

       public void AddSIFInfomation(int CompanyId, string FileName)
       {
           try
           {
               using (SqlCommand command = new SqlCommand("Com_AddSIFInfomation", _dbConnection))
               {
                   OpenDB();
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyId", CompanyId);
                   command.Parameters.AddWithValue("@FileName", FileName);
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

       /// <summary>
       /// Gets the company profile.
       /// </summary>
       /// <param name="CompanyId">The company id as int.</param>
       /// <returns></returns>
       public string GetSIFFileName(int CompanyId)
       {
           string sifFileName = string.Empty;
           try
           {
               using (SqlCommand command = new SqlCommand("COM_GetSIFFileName", _dbConnection))
               {
                   OpenDB();
                   command.CommandType = CommandType.StoredProcedure;                   
                   command.Parameters.AddWithValue("@CompanyId", CompanyId);

                   SqlParameter filename = new SqlParameter("@FileName", SqlDbType.NVarChar, 50);
                   filename.Direction = ParameterDirection.Output;
                   command.Parameters.Add(filename);
                   command.ExecuteNonQuery();

                   sifFileName = filename.Value.ToString();
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
           return sifFileName;
       }
       #endregion

       #endregion

       #region CompanyBranch

       /// <summary>
       /// Adds the company branch.
       /// </summary>
       /// <param name="BranchCode">The company branch code as string.</param>
       /// <param name="BranchLocation">Location of the branch as string. </param>
       /// <param name="BranchEmail">The branch email as string.</param>
       /// <param name="BranchContactPerson">The branch contact person as string.</param>
       /// <param name="BranchContactNo">The branch  no as string.</param>

       public void AddCompanyBranch(int CompanyId, string CompanyBrachCode, string Location, string Address, string BrachEmail, string ContactPerson, string BrachContactNo)
       {
           try
           {
               using (SqlCommand command = new SqlCommand("COM_AddCompanybranch", _dbConnection))
               {
                   OpenDB();
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyId", CompanyId);
                   command.Parameters.AddWithValue("@CompanyBranchCode", CompanyBrachCode);
                   
                    command.Parameters.AddWithValue("@Location", Location);
                   command.Parameters.AddWithValue("@Address", Address);
                   command.Parameters.AddWithValue("@BranchEmail", BrachEmail);
                   command.Parameters.AddWithValue("@ContactPerson", ContactPerson);
                   command.Parameters.AddWithValue("@ContactNo", BrachContactNo);

                   SqlParameter companybranchId = new SqlParameter("@CompanyBrachId", SqlDbType.Int, 8);
                   companybranchId.Direction = ParameterDirection.Output;
                   command.Parameters.Add(companybranchId);

                   command.ExecuteNonQuery();
                   _companybranchId = Convert.ToInt32(companybranchId.Value);
                   if (_companybranchId < 0)
                   {
                       _isError = true;
                       _errorMsg = "Record Already Exists!";
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
       }
        
        public DataTable DeleteCompanyBranchwise(int CompanyId)
          {
            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteCompanyBranchwise", _dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    using (SqlDataAdapter daAdaper = new SqlDataAdapter(command))
                    {
                        daAdaper.Fill(dtTable);
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
        /// <summary>
        /// Gets the compnay.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public DataTable GetCompanyBranch(int BranchId, int CompanyId)
       {
           DataTable dtTable = new DataTable();

           try
           {
               using (SqlCommand command = new SqlCommand("COM_GetCompanyBranch", _dbConnection))
               {
                   OpenDB();

                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyBranchID", BranchId);
                   command.Parameters.AddWithValue("@CompanyId", CompanyId);

                   using (SqlDataAdapter daAdaper = new SqlDataAdapter(command))
                   {
                       daAdaper.Fill(dtTable);
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

       /// <summary>
       /// Update the company branch.
       /// </summary>
       /// <param name="BranchCode">The companybranchId  as int.</param>
       /// <param name="BranchCode">The company branch code as string.</param>
       /// <param name="BranchLocation">Location of the branch as string. </param>
       /// <param name="BranchEmail">The branch email as string.</param>
       /// <param name="BranchContactPerson">The branch contact person as string.</param>
       /// <param name="BranchContactNo">The branch  no as string.</param>

       public void UpdateCompanyBranch(int CompanyBranchId, string CompanyBrachCode, string Location, string Address, string BrachEmail, string ContactPerson, string BrachContactNo)
       {
           try
           {
               using (SqlCommand command = new SqlCommand("COM_UpdateCompanyBranch", _dbConnection))
               {
                   OpenDB();
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyBranchId", CompanyBranchId);
                   command.Parameters.AddWithValue("@CompanyBranchCode", CompanyBrachCode);
                   
                    command.Parameters.AddWithValue("@Location", Location);
                   command.Parameters.AddWithValue("@Address", Address);
                   command.Parameters.AddWithValue("@BranchEmail", BrachEmail);
                   command.Parameters.AddWithValue("@ContactPerson", ContactPerson);
                   command.Parameters.AddWithValue("@ContactNo", BrachContactNo);
       
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

       /// <summary>
       /// Deletes the company branch.
       /// </summary>
       /// <param name="CompanyId">The companybranch id as int.</param>
       public void DeleteCompanyBranch(int CompanyBranchId)
       {
           try
           {
               OpenDB();
               using (SqlCommand command = new SqlCommand("COM_DeleteCompanyBranch", _dbConnection))
               {
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@CompanyBranchId", CompanyBranchId);
                   command.ExecuteNonQuery();
               }
           }
           catch (SqlException ex)
           {
               _isError = true;
               if (ex.Number == 547)
               {
                   _errorMsg = "Already Assign This Code";
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
       /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
       ~CompanyProfileDAL()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion



    }
}
