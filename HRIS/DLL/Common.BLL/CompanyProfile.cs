using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.Common.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.Common.BLL
{
    [DataObject]
    public class CompanyProfile
    {

        #region Fields
        private CompanyProfileDAL objCompanyProfileDAL;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
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
        public CompanyProfile()
        {
            objCompanyProfileDAL = new CompanyProfileDAL();
        }
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objCompanyProfileDAL.IsError;
            _errorMsg = objCompanyProfileDAL.ErrorMsg;
        }
        #endregion

        #region CompanyProfile

        /// <summary>
        /// Adds the company profile.
        /// </summary>
        /// <param name="CompanyCode">The company code as string.</param>
        /// <param name="CompanyName">Name of the company as string. </param>
        /// <param name="CompanyAddress">The company address as string.</param>
        /// <param name="CompanyContactNo1">The company contact no1 as string.</param>
        /// <param name="CompanyContactNo2">The company contact no2 as string.</param>
        /// <param name="CompanyFax">The company fax as string.</param>
        /// <param name="CompanyEmail">The company email as string.</param>
        /// <param name="CompanyWeb">The company web as string.</param>
        /// <param name="CompanyRegistrationNo">The company registration no as string.</param>
        /// <param name="CompanyTaxRegistrationNo">The company tax registration no as string.</param>
        public void AddCompanyProfile(string CompanyCode, string CompanyName, string CompanyAddress, string CompanyContactNo1, string CompanyContactNo2, string CompanyFax, 
            string CompanyEmail, string CompanyWeb, string CompanyRegistrationNo, string CompanyTaxRegistrationNo, bool EmployeeNameStatus,
                                        string CompanyAccountNo, int BankId, int BranchId, string CompanyAccountNo2, int BankId2, int BranchId2, bool IsCustomEmployeeCode, string Prefix, int EmployeeReportViewName)
        {
            try
            {
                objCompanyProfileDAL.AddCompanyProfile(CompanyCode, CompanyName, CompanyAddress, CompanyContactNo1, CompanyContactNo2, CompanyFax, CompanyEmail, CompanyWeb, CompanyRegistrationNo, CompanyTaxRegistrationNo, EmployeeNameStatus,
                                                        CompanyAccountNo, BankId, BranchId, CompanyAccountNo2, BankId2, BranchId2, IsCustomEmployeeCode, Prefix, EmployeeReportViewName);
                _companyId = objCompanyProfileDAL.CompanyId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public DataTable GetCompany(int companyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    objCompanyProfileDAL.GetCompany(companyId);
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
                }

                return dtTable;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetInactiveEmployeeStatus(int StatusId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetInactiveEmployeeStatus(StatusId);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }

        /// <summary>
        /// Adds the company profile.
        /// </summary>
        /// <param name="CompanyId">The CompanyId as string.</param>
        /// <param name="CompanyCode">The company code as string.</param>
        /// <param name="CompanyName">Name of the company as string. </param>
        /// <param name="CompanyAddress">The company address as string.</param>
        /// <param name="CompanyContactNo1">The company contact no1 as string.</param>
        /// <param name="CompanyContactNo2">The company contact no2 as string.</param>
        /// <param name="CompanyFax">The company fax as string.</param>
        /// <param name="CompanyEmail">The company email as string.</param>
        /// <param name="CompanyWeb">The company web as string.</param>
        /// <param name="CompanyRegistrationNo">The company registration no as string.</param>
        /// <param name="CompanyTaxRegistrationNo">The company tax registration no as string.</param>
        public void UpdateCompanyProfile(int CompanyId, string CompanyCode, string CompanyName, string CompanyAddress, string CompanyContactNo1, string CompanyContactNo2, 
            string CompanyFax, string CompanyEmail, string CompanyWeb, string CompanyRegistrationNo, string CompanyTaxRegistrationNo, bool EmployeeNameStatus,
                                        string CompanyAccountNo, int BankId, int BranchId, string CompanyAccountNo2, int BankId2, int BranchId2, bool IsCustomEmployeeCode, string Prefix, int EmployeeReportViewName)
        {
            try
            {
                objCompanyProfileDAL.UpdateCompanyProfile(CompanyId, CompanyCode, CompanyName, CompanyAddress, CompanyContactNo1, CompanyContactNo2, CompanyFax, CompanyEmail, CompanyWeb, CompanyRegistrationNo, CompanyTaxRegistrationNo,EmployeeNameStatus,
                                                            CompanyAccountNo, BankId, BranchId, CompanyAccountNo2, BankId2, BranchId2, IsCustomEmployeeCode, Prefix, EmployeeReportViewName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public DataTable GetEmployeeCompanyDetails(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetEmployeeCompanyDetails(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCompanyCostCenters(int CompanyCostId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetCompanyCostCenters(CompanyCostId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }
        /// <summary>
        /// Gets the company profile.
        /// </summary>
        /// <param name="CompanyId">The company id as int.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCompanyProfile(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetCompanyProfile(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCompanyBankDetails(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetCompanyBankDetails(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }

        public void SaveCompanyBankDetails(int CompanyId, int BankId, int BranchId, string AccountNo, string Description, string CreatedUser)
        {
            try
            {
                objCompanyProfileDAL.SaveCompanyBankDetails(CompanyId, BankId, BranchId, AccountNo, Description, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void SaveNewCompanyBankDetails(int CompanyId,int BankId,int BranchId,string AccountNo,string Discription,string UserName)
        {
            try
            {
                objCompanyProfileDAL.SaveNewCompanyBankDetails(CompanyId, BankId, BranchId, AccountNo, Discription, UserName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void UpdateCompanyBankDetails(int Id, int BankId, int BranchId, string AccountNo, string Description, string CreatedUser)
        {
            try
            {
                objCompanyProfileDAL.UpdateCompanyBankDetails(Id, BankId, BranchId, AccountNo, Description, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCompanyBankDetailsFull(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetCompanyBankDetailsFull(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCompanyBank(int BankId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetCompanyBank(BankId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCompanyBranch(int BranchId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetCompanyBranch(BranchId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;

        }
        public void DeleteCompanyProfile(int CompanyId)
        {
            try
            {
                objCompanyProfileDAL.DeleteCompanyProfile(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddSIFInfomation(int CompanyId, string FileName)
        {
            try
            {
                objCompanyProfileDAL.AddSIFInfomation(CompanyId, FileName);
                SetValues();
            }
            catch
            {

            }
        }        

        public string GetSIFFileName(int CompanyId)
        {
            string sifFileName = string.Empty;
            try
            {
                sifFileName = objCompanyProfileDAL.GetSIFFileName(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                objCompanyProfileDAL.AddCompanyBranch(CompanyId, CompanyBrachCode, Location, Address, BrachEmail, ContactPerson, BrachContactNo);
                _companybranchId = objCompanyProfileDAL.CompanyBranchId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the company Branch.
        /// </summary>
        /// <param name="CompanyId">The company id as int.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCompanyBranch(int BranchId, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.GetCompanyBranch(BranchId, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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

        public void UpdateCompanyBranch(int CompanyBranchId, string CompanyBrachCode, string Location,string Address, string BrachEmail, string ContactPerson, string BrachContactNo)
        {
            try
            {
                objCompanyProfileDAL.UpdateCompanyBranch(CompanyBranchId, CompanyBrachCode,Location,Address, BrachEmail, ContactPerson, BrachContactNo);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                objCompanyProfileDAL.DeleteCompanyBranch(CompanyBranchId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public DataTable DeleteCompanyBranchwise(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objCompanyProfileDAL.DeleteCompanyBranchwise(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        #endregion
    }
}
