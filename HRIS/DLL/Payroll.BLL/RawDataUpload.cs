namespace HRM.Payroll.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Data;
    using HRM.Payroll.DAL;
    using System.ComponentModel;
    using HRM.Common;
    using HRM.Common.BLL;
    using System.Globalization;
    using System.Configuration;
    using System.Linq;
    using HRM.Leave.BLL;


    public class RawDataUpload
    {
        #region Fields

        private EmployeePay objEmployeePay;
        private RawDataUploadDAL objRawDataUploadDAL;
        Loan objLoan = new Loan();
        Leave objLeaveReferences = new Leave();
        MksLeave objMksLeave = new MksLeave();

        private string EncryptionKey = "007London";
        private bool _isError;
        private string _errorMsg;

        public class ExcelUplodedData
        {
          
            // employee master
            public string employeeCode { get; set; }
            public string trueEmployeeCode { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string fullName { get; set; }
            public string nameWithInitial { get; set; }
            public string callName { get; set; }
            public string homeContactNo { get; set; }
            public string homeContactNo2 { get; set; }
            public string mobileNo { get; set; }
            public string mobileNo2 { get; set; }
            public string mobileNo3 { get; set; }
            public string officeNo { get; set; }
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string addresscity { get; set; }
            public string tempaddress1 { get; set; }
            public string tempaddress2 { get; set; }
            public string tempaddresscity { get; set; }
            public string email { get; set; }
            public string nic { get; set; }
            public string epfno { get; set; }
            public string gender { get; set; }
            public string bcard { get; set; }

            public DateTime dateOfBirth { get; set; }
            public DateTime retirementDate { get; set; }
            public string image { get; set; }
            public int jobCategoryId { get; set; }
            
            public int orgStructureId { get; set; }
            public int designationId { get; set; }
            public int jobGradeId { get; set; }
            public int employmentTypeId { get; set; }
            public int? religionId { get; set; }
            public int? nationalId { get; set; }
            public string bloodGroup { get; set; }
            public string emergencyContactPerson { get; set; }
            public string emergencyContactNo { get; set; }
            public string emergencyContacthomeNo { get; set; }
            public string relationshipOfContactPerson { get; set; }
            public string postalCode { get; set; }
            public DateTime recrutementDate { get; set; }
            public DateTime? epfentitledate { get; set; }
            public DateTime? confirmationDate { get; set; }
            public DateTime? secondReqruitmentDate { get; set; }
            public string previousEmployeeNo { get; set; }
            public DateTime? resignDate { get; set; }
            public int companyID { get; set; }
            public string remarks { get; set; }
            public int inactiveStatus { get; set; }
            public string emergencyContactAddressLine1 { get; set; }
            public string emergencyContactAddressLine2 { get; set; }
            public string emergencyContactAddressCity { get; set; }

            // employee pay
            public decimal BasicSalary { get; set; }
            public decimal FixedAllowance { get; set; }
            public decimal DailyWage { get; set; }
            public bool StopPay { get; set; }
            public int PayPeriodCategoryId { get; set; }
            public bool Posted { get; set; }
            public string CurrencyCode { get; set; }
            public decimal MaxAdvancePer { get; set; }
            public bool BankTranferRequired { get; set; }
            public int BankId { get; set; }
            public int PayPeriodId { get; set; }
            public int BranchId { get; set; }
            public int BankBranchId { get; set; }
            public int EmploymentGradeID { get; set; }
            public string AccountNo { get; set; }
            public string NameAsPerBank { get; set; }
            public string PayrollAct { get; set; }
            public string DirectIndirect { get; set; }
            public string OccupationGrade { get; set; }
            public int CostCenter { get; set; }
            public DateTime? ProbationStartDate { get; set; }
            public DateTime? ProbationEndDate { get; set; }
            public DateTime? FixedTermContarctStartDate { get; set; }
            public DateTime? FixedTermContarctEndDate { get; set; }
            public DateTime? ConsultancyStartDate { get; set; }
            public DateTime? ConsultancyEndDate { get; set; }
         
            public int EPFRate1 { get; set; }
            public int EPFRate2  { get; set; }
            public int EPFRate3  { get; set; }
            public int EPFRate4  { get; set; }
            public bool isepf { get; set; }

            public string Province { get; set; }
            public string District { get; set; }
            public string ElectoralDistrict { get; set; }
            public string DivisionalSecretariats { get; set; }
            public string GSDivision { get; set; }
            public string TransportRoute { get; set; }
            public string DistanceforPermanentAddress { get; set; }


            //employee paysheet

            public decimal BRA01 { get; set; }
            public decimal BRA02 { get; set; }
            public decimal INCREMENT2018 { get; set; }
            public decimal TOTALEARNINGS { get; set; }
            public decimal PERFORMANCEINCENTIVE2018 { get; set; }
            public decimal TRAVELLINGINCENTIVE { get; set; }
            public decimal TELEPHONEINCENTIVE { get; set; }
            public decimal SALESINCENTIVE { get; set; }
            public decimal OTHERINCENTIVE { get; set; }
            public decimal LEAVEENCASHMENT { get; set; }
            public decimal PREVIOUSMONTH { get; set; }
            public decimal GROSSSALARY { get; set; }
            public decimal LOAN { get; set; }
            public decimal MEALS { get; set; }
            public decimal SALAADVANCE { get; set; }
            public decimal NOPAY { get; set; }
            public decimal STAFFWELFAIR { get; set; }
            public decimal CONTRIBUTION { get; set; }
            public decimal EPF8 { get; set; }
            public decimal TOTALDEDUCTION { get; set; }
            public decimal SALARYTRANSFERTOBANK { get; set; }
            public decimal SALARYBALANCE { get; set; }
            public decimal DATA { get; set; }

            //employee payroll txn
            public long employeeId { get; set; }
            public int payPeriodId { get; set; }
            public decimal attendenceAll { get; set; }
            public decimal noPayDays { get; set; }
            public decimal otHRS1 { get; set; }
            public decimal otHRS2 { get; set; }
            public decimal otHRS3 { get; set; }
            public bool isPosted { get; set; }
            public decimal payeTax { get; set; }
            public decimal epf { get; set; }
            public bool isDeleted { get; set; }
            public decimal food { get; set; }
            public decimal mobile { get; set; }
            public decimal otherDedu { get; set; }
            public decimal loan { get; set; }
            public decimal singer { get; set; }
   
        }

        public class ExcelUplodedTimeData
        {
            // employee master
            public int empID { get; set; }
            public string employeeCode { get; set; }
            public DateTime? date { get; set; }
            public DateTime? inTime1  { get; set; }
            public DateTime? inTime2 { get; set; }
            public DateTime? outTime1 { get; set; }
            public DateTime? outTime2 { get; set; }
          
        }

        #endregion

        #region Constructor

        public RawDataUpload()
        {
            ErrorsOccurred = new List<string>();

            objEmployeePay = new EmployeePay();
            objRawDataUploadDAL = new RawDataUploadDAL();
        }

         #endregion

        #region Propaties

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

        public List<string> ErrorsOccurred { get; set; }

        #endregion

        #region Methods

        #region Internal

        private void SetValues()
        {
            _isError = false;
            _errorMsg = string.Empty;
        }

        #endregion

        #region External

        public int ImportEmployeePaySheet(DataTable dtEmployees, int PayPeriod, string AddedUserName)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();

                int maritalStatus = 0;
                //Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;

                    #region Data For Upload

                    // employee pay
                    uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                    uploadedData.PayPeriodCategoryId = PayPeriod;
                    uploadedData.BasicSalary =Convert.ToDecimal(drSaveItems["BASIC SALARY"].ToString().Trim());
                    uploadedData.BRA01 = Convert.ToDecimal(drSaveItems["BRA01"].ToString().Trim());
                    uploadedData.BRA02 = Convert.ToDecimal(drSaveItems["BRA02"].ToString().Trim());
                    uploadedData.INCREMENT2018 = Convert.ToDecimal(drSaveItems["INCREMENT2018"].ToString().Trim());
                    uploadedData.PERFORMANCEINCENTIVE2018 = Convert.ToDecimal(drSaveItems["PERFORMANCE INCENTIVE2018"].ToString().Trim());
                    uploadedData.TELEPHONEINCENTIVE = Convert.ToDecimal(drSaveItems["TELEPHONE INCENTIVE"].ToString().Trim());
                    uploadedData.TRAVELLINGINCENTIVE = Convert.ToDecimal(drSaveItems["TRAVELLING INCENTIVE"].ToString().Trim());
                    uploadedData.SALESINCENTIVE = Convert.ToDecimal(drSaveItems["SALES INCENTIVE"].ToString().Trim());
                    uploadedData.OTHERINCENTIVE = Convert.ToDecimal(drSaveItems["OTHER INCENTIVE"].ToString().Trim());
                    uploadedData.LEAVEENCASHMENT = Convert.ToDecimal(drSaveItems["LEAVE ENCASHMENT"].ToString().Trim());
                    uploadedData.PREVIOUSMONTH = Convert.ToDecimal(drSaveItems["PREVIOUS MONTH ADJESTMENT"].ToString().Trim());
                    uploadedData.GROSSSALARY = Convert.ToDecimal(drSaveItems["GROSS SALARY"].ToString().Trim());
                    uploadedData.LOAN = Convert.ToDecimal(drSaveItems["LOAN"].ToString().Trim());
                    uploadedData.SALAADVANCE = Convert.ToDecimal(drSaveItems["SALARY ADVANCE"].ToString().Trim());
                    uploadedData.MEALS = Convert.ToDecimal(drSaveItems["MEALS"].ToString().Trim());
                    uploadedData.DATA = Convert.ToDecimal(drSaveItems["TELEPHONE&DATA"].ToString().Trim());
                    uploadedData.Posted = true;
                    uploadedData.NOPAY = Convert.ToDecimal(drSaveItems["NOPAY"].ToString().Trim());
                    uploadedData.STAFFWELFAIR = Convert.ToDecimal(drSaveItems["STAFF WELFAIR"].ToString().Trim());
                    uploadedData.CONTRIBUTION = Convert.ToDecimal(drSaveItems["CONTRIBUTION&OTHERDEDUCTION"].ToString().Trim());

                    #endregion

                    objEmployeePay.AddEmployeePaySheet(uploadedData.employeeId, uploadedData.PayPeriodCategoryId, uploadedData.BasicSalary, uploadedData.BRA01, uploadedData.BRA02, uploadedData.INCREMENT2018,
                        uploadedData.PERFORMANCEINCENTIVE2018, uploadedData.TRAVELLINGINCENTIVE, uploadedData.TELEPHONEINCENTIVE, uploadedData.SALESINCENTIVE, uploadedData.OTHERINCENTIVE,
                        uploadedData.LEAVEENCASHMENT, uploadedData.PREVIOUSMONTH, uploadedData.GROSSSALARY, uploadedData.LOAN, uploadedData.SALAADVANCE, uploadedData.MEALS, uploadedData.DATA, uploadedData.Posted, uploadedData.NOPAY, uploadedData.STAFFWELFAIR,
                    uploadedData.CONTRIBUTION);

                    if (objEmployeePay.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }


                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }

        public int ImportEmployeeBankDetails(DataTable dtEmployees)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();

                int maritalStatus = 0;
                //Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;

                    #region Data For Upload

                    // employee pay
                    uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                    uploadedData.BankId = Convert.ToInt32(drSaveItems["BankID"].ToString().Trim());
                    uploadedData.BranchId = Convert.ToInt32(drSaveItems["BankBranchID"].ToString().Trim());
                    uploadedData.AccountNo = drSaveItems["BankAccountNumber"].ToString().Trim();
                    uploadedData.NameAsPerBank = drSaveItems["NameAsPerBank"].ToString().Trim();

                    #endregion

                    objEmployeePay.AddEmployeeBankDetails(
                                 uploadedData.employeeId, uploadedData.BankId, uploadedData.BranchId, uploadedData.AccountNo, uploadedData.NameAsPerBank);

                    if (objEmployeePay.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }


                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }

        public int ImportEmployeeLoans(DataTable dtEmployees, string CreateUser)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();

                int maritalStatus = 0;
                //Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;
                    int loanTypeId = Convert.ToInt32(drSaveItems["LoanTypeId"].ToString().Trim());
                    DateTime takenDate = Convert.ToDateTime(drSaveItems["TakenDate"].ToString().Trim());
                    DateTime startDate = Convert.ToDateTime(drSaveItems["StartDate"].ToString().Trim());
                    decimal loanAmount = Convert.ToDecimal(drSaveItems["LoanAmount"].ToString().Trim());
                    bool calculateWithInstallments = Convert.ToBoolean(drSaveItems["CalculatewithInstallments"].ToString().Trim());
                    bool calculatewithPremiums = Convert.ToBoolean(drSaveItems["CalculatewithPremiums"].ToString().Trim());
                    decimal interestAmount = loanAmount * (Convert.ToDecimal(drSaveItems["InterestRate"].ToString().Trim()) / 100);
                    decimal tottalLoanAmount = loanAmount + interestAmount;
                    int premiumCount = 0;
                    decimal premium = 0;

                    if (calculateWithInstallments)
                    {
                        premiumCount = Convert.ToInt32(drSaveItems["Installments"].ToString().Trim());
                        premium = tottalLoanAmount / premiumCount;
                    }
                    else if (calculatewithPremiums)
                    {
                        premium = Convert.ToDecimal(drSaveItems["PremiumAmount"].ToString().Trim());
                        premiumCount = (int)(tottalLoanAmount / premium);
                        if (tottalLoanAmount % premium > 0)
                        {
                            premiumCount = premiumCount + 1;
                        }
                    }

                    DateTime endDate = Convert.ToDateTime(drSaveItems["StartDate"].ToString().Trim()).AddMonths(premiumCount - 1);

                    #region Data For Upload

                    // employee pay
                    uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());


                    #endregion

                    objLoan.AddEmployeeLoan(uploadedData.employeeId, 0, loanTypeId, takenDate, startDate, endDate, premiumCount, tottalLoanAmount, premium, Convert.ToDecimal(drSaveItems["InterestRate"].ToString().Trim()),
                        0, premiumCount, interestAmount, false, false, calculateWithInstallments, calculatewithPremiums, CreateUser, premiumCount, premium);

                    if (objLoan.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }


                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }

        public int ImportEmployeeIncrement(DataTable dtEmployees, string AddedUserName, string EncryptionKey)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();


                bool isRecordUploaded = false;
                int rowCount = 0;
                decimal rowvalue = 0;
                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;


                    for (int i = 0; i < dtEmployees.Columns.Count; i++)
                    {
                        string ColName = dtEmployees.Columns[i].ColumnName.ToString();

                        if (ColName != "EmployeeCode" && ColName != "IsExported" && ColName != "EmployeeName" && ColName != "EmployeeId" && ColName != "EPFNo")
                        {
                            // for (int a = 0; a < dtEmployees.Rows.Count; a++)
                            {
                                uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                                string RowData = dtEmployees.Rows[rowCount][i].ToString().Trim();
                                if (RowData == "")
                                {
                                    rowvalue = 0;
                                }
                                else
                                {
                                    rowvalue = Convert.ToDecimal(RowData);
                                }
                                objRawDataUploadDAL.ImportEmployeeIncrement(uploadedData.employeeId, "BasicSalary", rowvalue, EncryptionKey, AddedUserName);
                            }
                        }

                    }
                    rowCount = rowCount + 1;

                    if (objEmployeePay.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }


                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }


        public int ImportEmployee(DataTable dtEmployees, int EmployeeGradeID, int DefaultInactiveStatus, int PayPeriodCategory, string DefaultCurrency, string AddedUserName)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();

                int maritalStatus = 0;
                string StopPay;
                string BankTranferRequired;
                //Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;

                    #region Data For Upload

                    // employee pay
                    uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                    long EmpId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());

                    if (EmpId != 0)
                    {
                        uploadedData.BasicSalary = Convert.ToDecimal(drSaveItems["BasicSalary"].ToString().Trim());
                        uploadedData.FixedAllowance = Convert.ToDecimal(drSaveItems["FixedAllowance"].ToString().Trim());
                        uploadedData.DailyWage = Convert.ToDecimal(drSaveItems["DailyWage"].ToString().Trim());
                        uploadedData.StopPay = Convert.ToBoolean(drSaveItems["StopPay"].ToString().Trim());
                        uploadedData.PayPeriodCategoryId = PayPeriodCategory;
                        uploadedData.Posted = false;
                        uploadedData.CurrencyCode = DefaultCurrency;
                        uploadedData.MaxAdvancePer = Convert.ToDecimal(drSaveItems["MaxAdvancePercentage"].ToString().Trim());
                        uploadedData.BankTranferRequired = Convert.ToBoolean(drSaveItems["BankTranferRequired"].ToString().Trim());
                        uploadedData.BankId = Convert.ToInt32(drSaveItems["BankID"].ToString().Trim());
                        uploadedData.BranchId = Convert.ToInt32(drSaveItems["BankBranchID"].ToString().Trim());
                        uploadedData.AccountNo = drSaveItems["BankAccountNumber"].ToString().Trim();
                        uploadedData.NameAsPerBank = drSaveItems["NameAsPerBank"].ToString().Trim();
                        objEmployeePay.CreatedUser = AddedUserName;
                        uploadedData.EPFRate1 = 8;
                        uploadedData.EPFRate2 = 12;
                        uploadedData.EPFRate3 = 3;
                        uploadedData.EPFRate4 = 20;
                        uploadedData.isepf = false;
                        #endregion

                        objEmployeePay.AddEmployeePay(uploadedData.employeeId, uploadedData.BasicSalary, uploadedData.FixedAllowance, uploadedData.DailyWage,
                                    uploadedData.StopPay, uploadedData.PayPeriodCategoryId, uploadedData.Posted, uploadedData.CurrencyCode, uploadedData.MaxAdvancePer,
                                    uploadedData.BankTranferRequired, uploadedData.BankId, uploadedData.BranchId, uploadedData.AccountNo, uploadedData.NameAsPerBank,false, EncryptionKey, uploadedData.EPFRate1, uploadedData.EPFRate2, uploadedData.EPFRate3, uploadedData.EPFRate4, false);

                        if (objEmployeePay.IsError == true)
                        {
                            isRecordUploaded = false;
                            ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                        }
                        else
                        {
                            isRecordUploaded = true;
                        }


                        if (isRecordUploaded == true)
                        {
                            SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }


        public int ImportEmployeeFixAllowance(DataTable dtEmployees, string AddedUserName, string EncryptionKey)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();


                bool isRecordUploaded = false;
                int rowCount = 0;
                decimal rowvalue = 0;
                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;
                    for (int i = 0; i < dtEmployees.Columns.Count; i++)
                    {
                        string ColName = dtEmployees.Columns[i].ColumnName.ToString();

                        if (ColName != "EmployeeCode" && ColName != "IsExported" && ColName != "EmployeeName")
                        {
                            
                            {
                                uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                                long EmpId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                                
                                if (EmpId != 0)
                                {
                                    string RowData = dtEmployees.Rows[rowCount][i].ToString().Trim();
                                    if (RowData == "")
                                    {
                                        rowvalue = 0;
                                    }
                                    else
                                    {
                                        rowvalue = Convert.ToDecimal(RowData);
                                    }
                                    objRawDataUploadDAL.AddEmployeeFomExelFixAllowance(uploadedData.employeeId, ColName, rowvalue, EncryptionKey, AddedUserName);

                                    if (objEmployeePay.IsError == true)
                                    {
                                        isRecordUploaded = false;
                                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                                    }
                                    else
                                    {
                                        isRecordUploaded = true;
                                    }
                                }
                            }
                        }

                    }

                    rowCount = rowCount + 1;
                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }

        public int ImportEmployeeMobileNo(DataTable dtEmployees, string AddedUserName, string EncryptionKey)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();


                bool isRecordUploaded = false;
                int rowCount = 0;
                decimal rowvalue = 0;
                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                
                var groupData = dtEmployees.AsEnumerable()
                      .GroupBy(r => r.Field<long>("EmployeeId"))
                      .Select(g => new
                      {
                          EmployeeId = g.Key,
                          Amount = g.Sum(x => x.Field<double?>("Amount")),
                      }).ToList();

                foreach (var drSaveItems in groupData)
                {
                    isRecordUploaded = false;

                    uploadedData.employeeId = drSaveItems.EmployeeId;
                    long EmpId = drSaveItems.EmployeeId;

                    if (EmpId != 0)
                    {
                        rowvalue = (decimal)drSaveItems.Amount;
                        objRawDataUploadDAL.AddEmployeeFromExelMobileBill(uploadedData.employeeId, rowvalue, EncryptionKey);

                        if (objEmployeePay.IsError == true)
                        {
                            isRecordUploaded = false;
                            ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                        }
                        else
                        {
                            isRecordUploaded = true;
                        }
                    }

                    rowCount = rowCount + 1;
                    if (isRecordUploaded == true)
                    {
                        int trueRowCount = dtEmployees.AsEnumerable().Count(x => x.Field<long>("EmployeeId") == EmpId);
                        SavedItemCount = SavedItemCount + trueRowCount;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }


        public int ImportEmployeeAdvance(DataTable dtEmployees, string AddedUserName, string EncryptionKey)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();


                bool isRecordUploaded = false;
                int rowCount = 0;
                decimal rowvalue = 0;
                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                for (int i = 0; i < dtEmployees.Rows.Count; i++)
                {
                    isRecordUploaded = false;

                    uploadedData.employeeId = Convert.ToInt64(dtEmployees.Rows[i]["EmployeeId"].ToString());
                    objRawDataUploadDAL.ImportEmployeeAdvance(uploadedData.employeeId, dtEmployees.Rows[i]["FieldCode"].ToString(), Convert.ToDecimal(dtEmployees.Rows[i]["AdvanceAmount"].ToString()), Convert.ToDateTime(dtEmployees.Rows[i]["StartDate"].ToString()), Convert.ToInt32(dtEmployees.Rows[i]["Installments"].ToString()), EncryptionKey, AddedUserName);
                    if (objRawDataUploadDAL.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }


                    rowCount = rowCount + 1;
                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }


        public int ImportEmployeeTransaction(DataTable dtEmployees, string AddedUserName, string EncryptionKey)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();


                decimal rowvalue = 0;
                //Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                int rowCount = 0;


                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;


                    for (int i = 0; i < dtEmployees.Columns.Count; i++)
                    {
                        string ColName = dtEmployees.Columns[i].ColumnName.ToString();

                        if (ColName != "EmployeeCode" && ColName != "IsExported" && ColName != "EmployeeName")
                        {
                            //for (int a = 0; a < dtEmployees.Rows.Count; a++)
                            {
                                uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                                long EmpId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());

                                if (EmpId != 0)
                                {
                                    string RowData = dtEmployees.Rows[rowCount][i].ToString().Trim();
                                    if (RowData == "")
                                    {
                                        rowvalue = 0;
                                    }
                                    else
                                    {
                                        rowvalue = Convert.ToDecimal(RowData);
                                    }

                                    objRawDataUploadDAL.AddEmployeeFomExelTransation(uploadedData.employeeId, ColName, rowvalue, EncryptionKey);

                                    if (objEmployeePay.IsError == true)
                                    {
                                        isRecordUploaded = false;
                                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                                    }
                                    else
                                    {
                                        isRecordUploaded = true;
                                    }

                                }
                            }
                        }

                    }
                    rowCount = rowCount + 1;
                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }

       


        public int ImportEmployeeInCommon(DataTable dtEmployees, string AddedUserName)
        {
            

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();

                int maritalStatus = 0;
                Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    ExcelUplodedData uploadedData = new ExcelUplodedData();
                    isRecordUploaded = false;
                    bool isEmployeeSalaryUpload = false;

                    #region Data For Upload

                    switch (drSaveItems["MaritalStatus"].ToString().Trim().ToLowerInvariant())
                    {
                        case "married":
                            {
                                maritalStatus = 1;
                                break;
                            }
                        default:
                            {
                                maritalStatus = 0;
                                break;
                            }
                    }

                    uploadedData.employeeCode = drSaveItems["EmployeeCode"].ToString().Trim();
                    uploadedData.trueEmployeeCode = drSaveItems["TrueEmployeeCode"].ToString().Trim();
                    uploadedData.firstName = drSaveItems["FirstName"].ToString().Trim();
                    uploadedData.lastName = drSaveItems["LastName"].ToString().Trim();
                    uploadedData.fullName = drSaveItems["FullName"].ToString().Trim();//
                    uploadedData.callName = drSaveItems["CallingName"].ToString().Trim();//
                    uploadedData.nameWithInitial = drSaveItems["Initial"].ToString().Trim();//
                    uploadedData.homeContactNo = drSaveItems["HomeContactNo"].ToString().Trim();
                    uploadedData.mobileNo = drSaveItems["MobileNo"].ToString().Trim();
                    uploadedData.officeNo = drSaveItems["OfficeNo"].ToString().Trim();
                    uploadedData.addressLine1 = drSaveItems["AddressLine1"].ToString().Trim();
                    uploadedData.addressLine2 = drSaveItems["AddressLine2"].ToString().Trim();
                    uploadedData.addresscity = drSaveItems["City"].ToString().Trim();
                    uploadedData.tempaddress1 = drSaveItems["TemporaryAddressLine1"].ToString().Trim();
                    uploadedData.tempaddress2 = drSaveItems["TemporaryAddressLine2"].ToString().Trim();
                    uploadedData.tempaddresscity = drSaveItems["TemporaryCity"].ToString().Trim();
                    uploadedData.email = drSaveItems["E-mail"].ToString().Trim();//
                    uploadedData.nic = drSaveItems["NIC"].ToString().Trim();
                    uploadedData.OccupationGrade = drSaveItems["OccupationGrade"].ToString().Trim();
                    uploadedData.PayrollAct = drSaveItems["PayrollAct"].ToString().Trim();
                    uploadedData.DirectIndirect = drSaveItems["DirectIndirectStatus"].ToString().Trim();
                    uploadedData.CostCenter = Convert.ToInt32(drSaveItems["CostCenterID"].ToString().Trim());

                    uploadedData.ProbationStartDate = null;
                    uploadedData.ProbationEndDate = null;
                    uploadedData.FixedTermContarctStartDate = null;
                    uploadedData.FixedTermContarctEndDate = null;
                    uploadedData.ConsultancyStartDate = null;
                    uploadedData.ConsultancyEndDate = null;

                    if (drSaveItems["EmploymentTypeCode"].ToString().Trim() == "Probation")
                    {
                        uploadedData.ProbationStartDate = Convert.ToDateTime(drSaveItems["StartDate"].ToString().Trim());
                        uploadedData.ProbationEndDate = Convert.ToDateTime(drSaveItems["EndDate"].ToString().Trim());
                    }else if(drSaveItems["EmploymentTypeCode"].ToString().Trim() == "FixedTermContarct")
                    {
                        uploadedData.FixedTermContarctStartDate = Convert.ToDateTime(drSaveItems["StartDate"].ToString().Trim());
                        uploadedData.FixedTermContarctEndDate = Convert.ToDateTime(drSaveItems["EndDate"].ToString().Trim());
                    }else if(drSaveItems["EmploymentTypeCode"].ToString().Trim() == "Consultancy")
                    {
                        uploadedData.ConsultancyStartDate = Convert.ToDateTime(drSaveItems["StartDate"].ToString().Trim());
                        uploadedData.ConsultancyEndDate = Convert.ToDateTime(drSaveItems["EndDate"].ToString().Trim());
                    }


                    uploadedData.gender = drSaveItems["Gender"].ToString().Trim();
                    uploadedData.bcard = drSaveItems["BCardAvailability"].ToString().Trim();
                    uploadedData.dateOfBirth = Convert.ToDateTime(drSaveItems["DateOfBirth"].ToString().Trim());
                    uploadedData.image = string.Empty;
                    uploadedData.EmploymentGradeID = Convert.ToInt32(drSaveItems["EmploymentGradeID"].ToString().Trim());
                    uploadedData.BranchId = Convert.ToInt32(drSaveItems["BranchID"].ToString().Trim());
                    uploadedData.jobCategoryId = Convert.ToInt32(drSaveItems["JobCategoryID"].ToString().Trim());
                    uploadedData.orgStructureId = Convert.ToInt32(drSaveItems["OrganizationStructureID"].ToString().Trim());
                    uploadedData.designationId = Convert.ToInt32(drSaveItems["DesignationID"].ToString().Trim());
                    uploadedData.jobGradeId = Convert.ToInt32(drSaveItems["JobGradeID"].ToString().Trim());
                    uploadedData.employmentTypeId = Convert.ToInt32(drSaveItems["EmploymentTypeID"].ToString().Trim());
                    uploadedData.emergencyContactPerson = drSaveItems["EmergencyContactPerson"].ToString().Trim();
                    uploadedData.emergencyContactNo = drSaveItems["EmergencyContactMobileNo"].ToString().Trim();
                    uploadedData.emergencyContacthomeNo =  drSaveItems["EmergencyContactLandNo"].ToString().Trim();
                    uploadedData.relationshipOfContactPerson = drSaveItems["RelationshipOfContactPerson"].ToString().Trim();
                    uploadedData.postalCode = drSaveItems["PostalCode"].ToString().Trim();//
                    uploadedData.recrutementDate = Convert.ToDateTime(drSaveItems["RecruitementDate"].ToString().Trim());
                    uploadedData.emergencyContactAddressLine1 = drSaveItems["EmergencyContactAddressLine1"].ToString().Trim();
                    uploadedData.emergencyContactAddressLine2 = drSaveItems["EmergencyContactAddressLine2"].ToString().Trim();
                    uploadedData.emergencyContactAddressCity = drSaveItems["EmergencyContactCity"].ToString().Trim();
                    isEmployeeSalaryUpload = Convert.ToBoolean(drSaveItems["EmployeeSalary"].ToString().Trim());


                    DateTime BirthDay = Convert.ToDateTime(uploadedData.dateOfBirth);
                    int BirthYear = BirthDay.Year;
                    int CuruntYear = DateTime.Today.Year;
                    int CuruntAge = CuruntYear - BirthYear;
                    int RetireToYears = 0;

                    if (uploadedData.gender.Equals("Male"))
                    {
                        RetireToYears = 55;
                        uploadedData.retirementDate = BirthDay.AddYears(RetireToYears);
                    }
                    else
                    {
                        RetireToYears = 50;
                        uploadedData.retirementDate = BirthDay.AddYears(RetireToYears);
                    }

                    if (isEmployeeSalaryUpload) 
                    {
                        uploadedData.BasicSalary = Convert.ToDecimal(drSaveItems["BasicSalary"].ToString().Trim());
                        uploadedData.FixedAllowance = Convert.ToDecimal(drSaveItems["FixedAllowance"].ToString().Trim());
                        uploadedData.DailyWage = Convert.ToDecimal(drSaveItems["DailyWage"].ToString().Trim());
                        uploadedData.StopPay = Convert.ToBoolean(drSaveItems["StopPay"].ToString().Trim());
                        uploadedData.PayPeriodCategoryId = Convert.ToInt32(drSaveItems["PayCategoryID"].ToString().Trim());
                        uploadedData.Posted = false;
                        uploadedData.CurrencyCode = "LKR";
                        uploadedData.MaxAdvancePer = Convert.ToDecimal(drSaveItems["MaxAdvancePercentage"].ToString().Trim());
                        uploadedData.BankTranferRequired = Convert.ToBoolean(drSaveItems["BankTranferRequired"].ToString().Trim());
                        uploadedData.BankId = Convert.ToInt32(drSaveItems["BankID"].ToString().Trim());
                        uploadedData.BankBranchId = Convert.ToInt32(drSaveItems["BankBranchID"].ToString().Trim());
                        uploadedData.AccountNo = drSaveItems["AccountNo"].ToString().Trim();
                        uploadedData.NameAsPerBank = drSaveItems["EmployeeNameAsPerBank"].ToString().Trim();
                        uploadedData.EPFRate1 = 8;
                        uploadedData.EPFRate2 = 12;
                        uploadedData.EPFRate3 = 3;
                        uploadedData.EPFRate4 = 20;
                        objEmployeePay.CreatedUser = AddedUserName;
                    }

                    //if (drSaveItems["ReligionID"].ToString().Trim() != string.Empty)
                    //{
                    //    uploadedData.religionId = Convert.ToInt32(drSaveItems["ReligionID"].ToString().Trim());
                    //}
                    //else
                    //{
                    //    uploadedData.religionId = null;
                    //}

                    //if (drSaveItems["NationalID"].ToString().Trim() != string.Empty)
                    //{
                    //    uploadedData.nationalId = Convert.ToInt32(drSaveItems["NationalID"].ToString().Trim());
                    //}
                    //else
                    //{
                    //    uploadedData.nationalId = null;
                    //}

                    uploadedData.epfno = string.Empty;
                    if (drSaveItems["EPFNo"].ToString().Trim() != string.Empty)
                    {
                        uploadedData.epfno = (drSaveItems["EPFNo"].ToString().Trim());
                    }
                    else
                    {
                        uploadedData.epfentitledate = null;
                    }


                    if (drSaveItems["EPFEntitlementDate"].ToString().Trim() != string.Empty)
                    {
                        uploadedData.epfentitledate = Convert.ToDateTime(drSaveItems["EPFEntitlementDate"].ToString().Trim());
                    }
                    else
                    {
                        uploadedData.epfentitledate = null;
                    }

                    if (drSaveItems["ConfirmationDate"].ToString().Trim() != string.Empty)
                    {
                        uploadedData.confirmationDate = Convert.ToDateTime(drSaveItems["ConfirmationDate"].ToString().Trim());
                    }
                    else
                    {
                        uploadedData.confirmationDate = null;
                    }

                    if (drSaveItems["SecondRecruitmentDate"].ToString().Trim() != string.Empty)
                    {
                        uploadedData.secondReqruitmentDate = Convert.ToDateTime(drSaveItems["SecondReqruitmentDate"].ToString().Trim());
                    }
                    else
                    {
                        uploadedData.secondReqruitmentDate = null;
                    }

                    if (drSaveItems["ResignDate"].ToString().Trim() != string.Empty)
                    {
                        uploadedData.resignDate = Convert.ToDateTime(drSaveItems["ResignDate"].ToString().Trim());
                    }
                    else
                    {
                        uploadedData.resignDate = null;
                    }

                   uploadedData.previousEmployeeNo = drSaveItems["PreviousEmployeeNo"].ToString().Trim();
                   
                    uploadedData.companyID = Convert.ToInt32(drSaveItems["CompanyID"].ToString().Trim());
                    uploadedData.remarks = drSaveItems["Remark"].ToString().Trim();//

                    if (Convert.ToBoolean(drSaveItems["Active"].ToString().Trim()) == true)
                    {
                        uploadedData.inactiveStatus = 1;
                    }
                    else
                    {
                        uploadedData.inactiveStatus = 4;
                    }

                    #endregion

                    employeeNumber = objRawDataUploadDAL.AddEmployeeForCommonModule(uploadedData.employeeCode, uploadedData.trueEmployeeCode, uploadedData.firstName, uploadedData.lastName, uploadedData.fullName, uploadedData.callName,uploadedData.nameWithInitial,
                        uploadedData.homeContactNo, uploadedData.homeContactNo2, uploadedData.mobileNo, uploadedData.mobileNo2, uploadedData.mobileNo3, uploadedData.officeNo, uploadedData.addressLine1, uploadedData.addressLine2, uploadedData.addresscity, uploadedData.tempaddress1, uploadedData.tempaddress2, uploadedData.tempaddresscity, uploadedData.email, uploadedData.nic,uploadedData.epfno, uploadedData.gender,uploadedData.bcard, uploadedData.dateOfBirth,
                        maritalStatus, uploadedData.image,uploadedData.BranchId, uploadedData.jobCategoryId, uploadedData.orgStructureId, uploadedData.designationId, uploadedData.jobGradeId,
                        uploadedData.employmentTypeId, uploadedData.emergencyContactPerson,uploadedData.emergencyContactAddressLine1, uploadedData.emergencyContactAddressLine2, uploadedData.emergencyContactAddressCity, uploadedData.emergencyContactNo, uploadedData.emergencyContacthomeNo, uploadedData.relationshipOfContactPerson, uploadedData.EmploymentGradeID, uploadedData.postalCode,
                        uploadedData.recrutementDate,uploadedData.epfentitledate, uploadedData.confirmationDate, uploadedData.secondReqruitmentDate, uploadedData.previousEmployeeNo, uploadedData.resignDate,
                        uploadedData.companyID, uploadedData.remarks, AddedUserName, uploadedData.inactiveStatus, uploadedData.OccupationGrade, uploadedData.PayrollAct, uploadedData.DirectIndirect, uploadedData.CostCenter, uploadedData.ProbationStartDate, uploadedData.ProbationEndDate,
                        uploadedData.FixedTermContarctStartDate, uploadedData.FixedTermContarctEndDate, uploadedData.ConsultancyStartDate, uploadedData.ConsultancyEndDate, uploadedData.retirementDate);

                    if (objRawDataUploadDAL.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        if (employeeNumber > 0 && isEmployeeSalaryUpload)
                        {

                            objEmployeePay.AddEmployeePayWhenAddEmployee(employeeNumber, uploadedData.BasicSalary, uploadedData.FixedAllowance, uploadedData.DailyWage,
                                    uploadedData.StopPay, uploadedData.PayPeriodCategoryId, uploadedData.Posted, uploadedData.CurrencyCode, uploadedData.MaxAdvancePer,
                                    uploadedData.BankTranferRequired, uploadedData.BankId, uploadedData.BankBranchId, uploadedData.AccountNo, uploadedData.NameAsPerBank,false, EncryptionKey, uploadedData.EPFRate1 , uploadedData.EPFRate2 , uploadedData.EPFRate3 , uploadedData.EPFRate4 );

                            if (objEmployeePay.IsError == true)
                            {
                                isRecordUploaded = false;
                                ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                            }
                            else
                            {
                                isRecordUploaded = true;
                            }
                        }
                        else
                        {
                            isRecordUploaded = true;
                        }
                        
                    }

                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }

        public int ImportEmployeeAdditionalDetails(DataTable dtEmployees, string AddedUserName)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();
                bool isRecordUploaded = false;

                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;

                    #region Data For Upload

                    uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeID"].ToString().Trim());
                    if(drSaveItems["NationalityID"].ToString().Trim() != "0")
                    {
                        uploadedData.nationalId = Convert.ToInt32(drSaveItems["NationalityID"].ToString().Trim());
                    }
                    if (drSaveItems["ReligionID"].ToString().Trim() != "0")
                    {
                        uploadedData.religionId = Convert.ToInt32(drSaveItems["ReligionID"].ToString().Trim());
                    }
                    uploadedData.bloodGroup = drSaveItems["BloodGroup"].ToString().Trim();
                    uploadedData.Province = drSaveItems["Province"].ToString().Trim();
                    uploadedData.District = drSaveItems["District"].ToString().Trim();
                    uploadedData.ElectoralDistrict = drSaveItems["ElectoralDistrict"].ToString().Trim();
                    uploadedData.DivisionalSecretariats = drSaveItems["DivisionalSecretariats"].ToString().Trim();
                    uploadedData.GSDivision = drSaveItems["GSDivision"].ToString().Trim();
                    uploadedData.TransportRoute = drSaveItems["TransportRoute"].ToString().Trim();
                    uploadedData.DistanceforPermanentAddress = drSaveItems["DistanceforPermanentAddress"].ToString().Trim();

                    #endregion

                    objRawDataUploadDAL.AddEmployeeAdditionalDetails(uploadedData.employeeId, uploadedData.nationalId, uploadedData.religionId, uploadedData.bloodGroup, uploadedData.Province, uploadedData.District,
                    uploadedData.ElectoralDistrict, uploadedData.DivisionalSecretariats, uploadedData.GSDivision, uploadedData.TransportRoute, uploadedData.DistanceforPermanentAddress, AddedUserName);

                    if (objRawDataUploadDAL.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }

                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }


        public int ImportEmployeeInPayTxn(DataTable dtEmployees, string AddedUserName)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();

               
                Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;

                    #region Data For Upload

                    uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                    uploadedData.payPeriodId = Convert.ToInt32(drSaveItems["PayPeriodId"].ToString().Trim());
                    uploadedData.attendenceAll = Convert.ToDecimal(drSaveItems["AttendenceAllowans"].ToString().Trim());
                    uploadedData.noPayDays = Convert.ToDecimal(drSaveItems["NoPayDays"].ToString().Trim());
                    uploadedData.otHRS1 = Convert.ToDecimal(drSaveItems["OTHRS1"].ToString().Trim());//
                    uploadedData.otHRS2 = Convert.ToDecimal(drSaveItems["OTHRS2"].ToString().Trim());
                    uploadedData.otHRS3 = Convert.ToDecimal(drSaveItems["OTHRS3"].ToString().Trim());
                    uploadedData.isPosted = Convert.ToBoolean(drSaveItems["IsPosted"].ToString().Trim());//
                    uploadedData.payeTax = Convert.ToDecimal(drSaveItems["PayeTax"].ToString().Trim());
                    uploadedData.epf = Convert.ToDecimal(drSaveItems["EPF"].ToString().Trim());
                    uploadedData.isDeleted = Convert.ToBoolean(drSaveItems["IsDeleted"].ToString().Trim());
                    uploadedData.food = Convert.ToDecimal(drSaveItems["FOOD"].ToString().Trim());
                    uploadedData.mobile = Convert.ToDecimal(drSaveItems["MOBILE"].ToString().Trim());
                    uploadedData.otherDedu = Convert.ToDecimal(drSaveItems["OTHERDEDU"].ToString().Trim());
                    uploadedData.loan = Convert.ToDecimal(drSaveItems["LOAN"].ToString().Trim());
                    uploadedData.singer = Convert.ToDecimal(drSaveItems["SINGER"].ToString().Trim());
                
                    #endregion

                    employeeNumber = objRawDataUploadDAL.AddEmployeeInPayTxn(uploadedData.employeeId, uploadedData.payPeriodId, uploadedData.attendenceAll, uploadedData.noPayDays,
                        uploadedData.otHRS1, uploadedData.otHRS2, uploadedData.otHRS3, uploadedData.isPosted, uploadedData.payeTax, uploadedData.epf, uploadedData.isDeleted, uploadedData.food,
                        uploadedData.mobile, uploadedData.otherDedu, uploadedData.loan, uploadedData.singer);

                    if (objRawDataUploadDAL.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }

                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }




        //public int ImportEmployeeTimeInCommon(DataTable dtEmployees, string AddedUserName,int companyID)
        //{
        //    ExcelUplodedTimeData uploadedTimeData = new ExcelUplodedTimeData();
        //    Int64 employeeNumber = -1;
        //    int SavedItemCount = 0;
        //    bool isRecordUploaded = false;
        //    DateTime? date = new DateTime();

        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        //ds.Columns.Add("MyRow", type(System.Int32));

        //        ErrorsOccurred = new List<string>();
        //        //Get all Employees
        //        Employee  getEmployee = new Employee();
        //        DataTable dt = getEmployee.GetEmployee(0, AddedUserName, Convert.ToInt32(companyID), "Common");

        //        int empNo = 0;

        //        foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
        //        {

        //            #region Data For Upload

        //            uploadedTimeData.inTime1 = null;
        //            uploadedTimeData.outTime1 = null;
        //            uploadedTimeData.inTime2 = null;
        //            uploadedTimeData.outTime2 = null;

        //            uploadedTimeData.employeeCode = drSaveItems["UserID"].ToString().Trim();

        //            DataColumn[] keyColumn = new DataColumn[1];
        //            keyColumn[0] = dt.Columns["EmployeeCode"];
        //            dt.PrimaryKey = keyColumn;


        //            //string expression;
        //            // expression = "EmployeeCode = " + uploadedTimeData.employeeCode + "";

        //            // if(dtValues.Select("EmployeeCode='" + uploadedTimeData.employeeCode + "'").Length == 0)

        //            DataRow[] advRow = dt.Select("EmployeeCode='" + uploadedTimeData.employeeCode + "'");

        //            if (advRow.Length > 0)
        //            {
        //                uploadedTimeData.empID = Convert.ToInt32(advRow[0].ItemArray[0].ToString());
        //                dt.Rows.Remove(advRow[0]);


        //                dt.AcceptChanges();


        //                if (drSaveItems["Date"].ToString().Trim() != string.Empty)
        //                {
        //                    uploadedTimeData.date = Convert.ToDateTime(drSaveItems["Date"].ToString().Trim());
        //                    date = uploadedTimeData.date;
        //                }
        //                else
        //                {
        //                    uploadedTimeData.date = null;
        //                }

        //                if (drSaveItems["1"].ToString().Trim() != string.Empty)
        //                {
        //                    uploadedTimeData.inTime1 = Convert.ToDateTime(drSaveItems["1"].ToString().Trim());

        //                }
        //                else
        //                {
        //                    uploadedTimeData.inTime1 = null;
        //                }

        //                if (drSaveItems["2"].ToString().Trim() != string.Empty && drSaveItems["3"].ToString().Trim() != string.Empty)
        //                {
        //                    uploadedTimeData.outTime1 = Convert.ToDateTime(drSaveItems["2"].ToString().Trim());
        //                }
        //                else if (drSaveItems["2"].ToString().Trim() != string.Empty && drSaveItems["3"].ToString().Trim() == string.Empty)
        //                {
        //                    uploadedTimeData.outTime2 = Convert.ToDateTime(drSaveItems["2"].ToString().Trim()); ;
        //                }
        //                else
        //                {
        //                    uploadedTimeData.outTime1 = null;
        //                }


        //                if (drSaveItems["3"].ToString().Trim() != string.Empty && drSaveItems["4"].ToString().Trim() != string.Empty)
        //                {
        //                    uploadedTimeData.inTime2 = Convert.ToDateTime(drSaveItems["3"].ToString().Trim());
        //                }
        //                else if (drSaveItems["3"].ToString().Trim() != string.Empty && drSaveItems["4"].ToString().Trim() == string.Empty)
        //                {
        //                    uploadedTimeData.outTime2 = Convert.ToDateTime(drSaveItems["3"].ToString().Trim());

        //                }
        //                else if (drSaveItems["3"].ToString().Trim() == string.Empty)
        //                {
        //                    uploadedTimeData.inTime2 = null;
        //                }

        //                if (drSaveItems["4"].ToString().Trim() != string.Empty)
        //                {
        //                    uploadedTimeData.outTime2 = Convert.ToDateTime(drSaveItems["4"].ToString().Trim());
        //                }



        //            #endregion

        //                employeeNumber = objRawDataUploadDAL.AddEmployeeTime(uploadedTimeData.empID, uploadedTimeData.employeeCode, uploadedTimeData.date, uploadedTimeData.inTime1, uploadedTimeData.inTime2,
        //                    uploadedTimeData.outTime1, uploadedTimeData.outTime2, AddedUserName, true);

        //                if (objRawDataUploadDAL.IsError == true)
        //                {
        //                    isRecordUploaded = false;
        //                    ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
        //                }
        //                else
        //                {
        //                    isRecordUploaded = true;
        //                }

        //                if (isRecordUploaded == true)
        //                {
        //                    SavedItemCount++;
        //                }
        //            }
        //        }

        //        foreach (DataRow drSaveItems in dt.DefaultView.Table.Rows)
        //        {
        //            uploadedTimeData.empID =Convert.ToInt32( drSaveItems["EmployeeId"].ToString().Trim());
        //              uploadedTimeData.employeeCode = drSaveItems["EmployeeCode"].ToString().Trim();

        //              uploadedTimeData.inTime1 =null;
        //             uploadedTimeData.inTime2=null;
        //               uploadedTimeData.outTime1=null;
        //               uploadedTimeData.outTime2=null;
        //            employeeNumber = objRawDataUploadDAL.AddEmployeeTime(uploadedTimeData.empID, uploadedTimeData.employeeCode, uploadedTimeData.date, uploadedTimeData.inTime1, uploadedTimeData.inTime2,
        //                uploadedTimeData.outTime1, uploadedTimeData.outTime2, AddedUserName,false);


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _errorMsg = ex.Message;
        //    }
        //    finally
        //    {
        //        if (_isError == false)
        //        {
        //            SetValues();
        //        }
        //    }

        //    return SavedItemCount;
        //}


        public int ImportEmployeeTimeInCommon(DataTable dtEmployees, string AddedUserName, int companyID)
        {
            ExcelUplodedTimeData uploadedTimeData = new ExcelUplodedTimeData();
            int SavedItemCount = 0;

            string timeIn = "";
            string timeOut = "";
            try
            {
                DataSet ds = new DataSet();
                //ds.Columns.Add("MyRow", type(System.Int32));

                //IEnumerable<DataRow> orderedRows = dtEmployees.AsEnumerable().OrderBy(r => r.Field<DateTime>("DateTime"));

                // DataTable dt = orderedRows.CopyToDataTable();

                // ErrorsOccurred = new List<string>();
                //Get all Employees
                //Employee  getEmployee = new Employee();
                //DataTable dt = getEmployee.GetEmployee(0, AddedUserName, Convert.ToInt32(companyID), "Common");

                for (int i = 0; i < dtEmployees.Rows.Count; i++)
                {
                    // public Int64 AddEmployeeFomExel(string _employeeCode,  string  CardDate,string CardTime,string CardNo,string CardDateTime,)
                    //objRawDataUploadDAL.AddEmployeeFomExel();
                    uploadedTimeData.employeeCode = dtEmployees.Rows[i]["EmployeeCode"].ToString();

                    string attendanceType = "";

                    string date = dtEmployees.Rows[i]["Date"].ToString();
                    string[] DateArr = date.Split('-');
                    string Month = DateArr[1].ToString();
                    string Day = DateArr[2].ToString();
                    string Year = DateArr[0].ToString();
                    string punchTime = dtEmployees.Rows[i]["Time"].ToString();
                    string[] subs = punchTime.Split(':');
                    punchTime = subs[0] + ':' + subs[1];
                    objRawDataUploadDAL.AddEmployeeFomExel(uploadedTimeData.employeeCode, Day, Month, Year, punchTime, attendanceType, AddedUserName,companyID);

                    if (objRawDataUploadDAL.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }

                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
                        timeIn = "";
                        timeOut = "";
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;

        }


        #endregion


        #endregion


        public decimal GetLeaveQty(long employeeId, int leaveTypeId, DateTime fromDate, DateTime toDate, bool isHalfday, int companyId)
        {
            int dayOffFullLeaveTypeId = 0;
            int dayOffHalfLeaveTypeId = 0;
            DataTable dtLeaveType = objMksLeave.getLeaveTypeId("DAYOFFFULL");
            if (dtLeaveType.Rows.Count > 0)
            {
                dayOffFullLeaveTypeId = Convert.ToInt32(dtLeaveType.Rows[0]["LeaveTypeId"]);
            }

            
            dtLeaveType = objMksLeave.getLeaveTypeId("DAYOFFHALF");
            if (dtLeaveType.Rows.Count > 0)
            {
                dayOffHalfLeaveTypeId = Convert.ToInt32(dtLeaveType.Rows[0]["LeaveTypeId"]);
            }

            try
            {
                double LevCount = 0;
                DataTable getLevDates = objMksLeave.GetLevRequestDates(fromDate, toDate, companyId);
                foreach (DataRow date in getLevDates.Rows)
                {
                    string ReqDate = date["CalendarDate"].ToString();
                    DataTable checkShift = objMksLeave.CheckShiftDate(Convert.ToDateTime(ReqDate), employeeId);
                    if (checkShift.Rows.Count > 0)
                    {
                        bool isDayOff = Convert.ToBoolean(checkShift.Rows[0]["IsDayOff"]);
                        double dayOffHalfLeaveCount = Convert.ToDouble(checkShift.Rows[0]["DayOffLeaveQty"]);
                        double shiftLeaveCount = Convert.ToDouble(checkShift.Rows[0]["AppliedLeaveQty"]);
                        if ((isDayOff && dayOffHalfLeaveCount == 0.5) || (!isDayOff && shiftLeaveCount == 0.5) || isHalfday)
                        {
                            LevCount = LevCount + 0.5;
                        }
                        else if (!isDayOff && shiftLeaveCount == 1)
                        {
                            LevCount = LevCount + 1;
                        }
                    }
                    else if (leaveTypeId == dayOffFullLeaveTypeId || leaveTypeId == dayOffHalfLeaveTypeId)
                    {
                        if(leaveTypeId == dayOffHalfLeaveTypeId || isHalfday)
                        {
                            LevCount = LevCount + 0.5;
                        }
                        else
                        {
                            LevCount = LevCount + 1;
                        }
                    }
                }
                return (decimal)LevCount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int ImportEmployeeInExcelUpdate(DataTable dtEmployees, string AddedUserName)
        {
            ExcelUplodedData uploadedData = new ExcelUplodedData();

            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();


                decimal rowvalue = 0;
                //Int64 employeeNumber = -1;
                bool isRecordUploaded = false;

                int rowCount = 0;


                //dtEmployees.DefaultView.RowFilter = "IsExported = " + true;
                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;


                    for (int i = 0; i < dtEmployees.Columns.Count; i++)
                    {
                        string ColName = dtEmployees.Columns[i].ColumnName.ToString();

                        if (ColName != "EmployeeCode" && ColName != "IsExported" && ColName != "EmployeeName")
                        {
                            //for (int a = 0; a < dtEmployees.Rows.Count; a++)
                            {
                                uploadedData.employeeId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());
                                long EmpId = Convert.ToInt64(drSaveItems["EmployeeId"].ToString().Trim());

                                if (EmpId != 0)
                                {
                                    string RowData = dtEmployees.Rows[rowCount][i].ToString().Trim();

                                    objRawDataUploadDAL.ImportEmployeeInExcelUpdate(uploadedData.employeeId, ColName, RowData, EncryptionKey);

                                    if (objEmployeePay.IsError == true)
                                    {
                                        isRecordUploaded = false;
                                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                                    }
                                    else
                                    {
                                        isRecordUploaded = true;
                                    }

                                }
                            }
                        }

                    }
                    rowCount = rowCount + 1;
                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }


        public int ImportLeaveEntitlement(DataTable dtEmployees, string AddedUserName)
        {
            ExcelUplodedTimeData uploadedLeaveEntitlement = new ExcelUplodedTimeData();
            int SavedItemCount = 0;

            string time1 = "";
            string time2 = "";

            try
            {

                ErrorsOccurred = new List<string>();
                bool isRecordUploaded = false;
                int rowCount = 0;
                decimal rowvalue = 0;

                foreach (DataRow drSaveItems in dtEmployees.DefaultView.Table.Rows)
                {
                    isRecordUploaded = false;
                    int ColumnCount;

                    ColumnCount = Convert.ToInt32(ConfigurationManager.AppSettings["LeaveColumnsCount"].ToString());
                    long EmployeeId = Convert.ToInt64(dtEmployees.Rows[rowCount]["EmployeeId"].ToString());

                    for (int i = 0; i <= ColumnCount; i++)
                    {
                        string ColName = dtEmployees.Columns[i].ColumnName.ToString();
                        int Count = i;
                        if (ColName != "EmployeeCode" && ColName != "EmployeeName" && ColName != "EmployeeId")
                        {

                            {

                                string RowData = dtEmployees.Rows[rowCount][i].ToString().Trim();

                                if (RowData == "")
                                {
                                    rowvalue = 0;
                                }
                                else
                                {
                                    rowvalue = Convert.ToDecimal(RowData);
                                }
                                objRawDataUploadDAL.AddLeaveEntitlement(EmployeeId, rowvalue, Convert.ToDecimal(Count));
                            }
                        }

                    }
                    rowCount = rowCount + 1;

                    if (objEmployeePay.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }


                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }

        public int ImportLeaveBulkEntry(DataTable dtEmployees, string AddedUserName, int CompanyId)
        {
            ExcelUplodedTimeData uploadedLeaveEntitlement = new ExcelUplodedTimeData();
            int SavedItemCount = 0;

            try
            {
                ErrorsOccurred = new List<string>();
                bool isRecordUploaded = false;
                
                for (int i = 0; i < dtEmployees.Rows.Count; i++)
                {
                    long employeeId = Convert.ToInt64(dtEmployees.Rows[i]["EmployeeId"].ToString());
                    DateTime fromDate = Convert.ToDateTime(dtEmployees.Rows[i]["FromDate"].ToString());
                    DateTime toDate = Convert.ToDateTime(dtEmployees.Rows[i]["ToDate"].ToString());
                    int dayOffHalfDayShift = Convert.ToInt32(dtEmployees.Rows[i]["HalfDayShiftId"].ToString());
                    int leaveTypeId = Convert.ToInt32(dtEmployees.Rows[i]["LeaveTypeId"].ToString());
                    bool isHalfday = Convert.ToBoolean(dtEmployees.Rows[i]["HalfDay"].ToString());
                    decimal noOfdays = GetLeaveQty(employeeId, leaveTypeId, fromDate, toDate, isHalfday, CompanyId);
                    int leaveReasonId = Convert.ToInt32(dtEmployees.Rows[i]["LeaveReasonId"].ToString());
                    string leaveTypeCode = dtEmployees.Rows[i]["LeaveTypeCode"].ToString();

                    bool isFirst = true;
                    long LeaveId = 0;
                    DataTable getLevDates = objMksLeave.GetLevRequestDates(fromDate, toDate, CompanyId);
                    foreach (DataRow date in getLevDates.Rows)
                    {
                        string ReqDate = date["CalendarDate"].ToString();
                        string res = "";
                        if (isFirst)
                        {
                            objMksLeave.AddLeaveHeaderNew(CompanyId, employeeId, leaveTypeId, fromDate, toDate,
                             Convert.ToDecimal(noOfdays), 0, AddedUserName, "LeaveApplication", getLevDates, false, "", leaveReasonId);

                            res = objMksLeave.Result;
                            LeaveId = Convert.ToInt64(objMksLeave.LeaveId);
                            isFirst = false;

                        }

                        if (leaveTypeCode == "SHORT")
                        {
                            try
                            {
                                DateTime rd = Convert.ToDateTime(ReqDate);
                                string DateName = rd.DayOfWeek.ToString();
                                string sessionMorning = "ShortLeave";
                                DataTable checkShift = objMksLeave.CheckShiftDate(Convert.ToDateTime(ReqDate), employeeId);
                                if (checkShift.Rows.Count > 0)
                                {
                                    DataTable checkDate = objMksLeave.CheckDate(Convert.ToDateTime(ReqDate));
                                    if (checkDate.Rows.Count > 0)
                                    {
                                        string DayType2 = checkDate.Rows[0]["DayTypeId"].ToString();
                                        objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(0.25), sessionMorning, Convert.ToInt32(DayType2),
                                            1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                        else if (leaveTypeCode == "MATERNITY")
                        {
                            try
                            {
                                DateTime rd = Convert.ToDateTime(ReqDate);
                                string DateName = rd.DayOfWeek.ToString();
                                string payrollAct = "";
                                Employee objEmployee = new Employee();
                                DataTable dtEmployee = objEmployee.GetEmployee(employeeId, AddedUserName, CompanyId, "Common");
                                if (dtEmployee.Rows.Count > 0)
                                {
                                    payrollAct = dtEmployee.Rows[0]["PayrollAct"].ToString();
                                }

                                DataTable checkDate = objMksLeave.CheckDate(Convert.ToDateTime(ReqDate));
                                if (checkDate.Rows.Count > 0)
                                {
                                    string DayType2 = checkDate.Rows[0]["DayTypeId"].ToString();
                                    if (payrollAct == "Shop & Office")
                                    {
                                        if (DateName == "Saturday")
                                        {
                                            objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(0.5), "HalfDay", Convert.ToInt32(DayType2),
                                            1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                        }
                                        else
                                        {
                                            if (DateName != "Sunday")
                                            {
                                                objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(1), "FullDay", Convert.ToInt32(DayType2),
                                                1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                            }
                                        }
                                    }
                                    else if (payrollAct == "Wages Board")
                                    {
                                        if (DateName != "Sunday")
                                        {
                                            objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(1), "FullDay", Convert.ToInt32(DayType2),
                                            1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                        }
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            if (isHalfday == true)
                            {
                                try
                                {
                                    DateTime rd = Convert.ToDateTime(ReqDate);
                                    string DateName = rd.DayOfWeek.ToString();
                                    DataTable checkShift = objMksLeave.CheckShiftDate(Convert.ToDateTime(ReqDate), employeeId);

                                    if (checkShift.Rows.Count > 0)
                                    {
                                        DataTable checkDate = objMksLeave.CheckDate(Convert.ToDateTime(ReqDate));
                                        if (checkDate.Rows.Count > 0)
                                        {
                                            string DayType2 = checkDate.Rows[0]["DayTypeId"].ToString();
                                            objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(0.5), "HalfDay", Convert.ToInt32(DayType2),
                                                1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                        }
                                    }
                                    else if (leaveTypeCode == "DAYOFFHALF")
                                    {
                                        objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(0.5), "HalfDay", Convert.ToInt32(1007),
                                                1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            if (isHalfday == false)
                            {
                                try
                                {
                                    DateTime rd = Convert.ToDateTime(ReqDate);
                                    string DateName = rd.DayOfWeek.ToString();
                                    DataTable checkShift = objMksLeave.CheckShiftDate(Convert.ToDateTime(ReqDate), employeeId);
                                    if (checkShift.Rows.Count > 0)
                                    {
                                        bool isDayOff = Convert.ToBoolean(checkShift.Rows[0]["IsDayOff"]);
                                        double dayOffHalfLeaveCount = Convert.ToDouble(checkShift.Rows[0]["DayOffLeaveQty"]);
                                        double shiftLeaveCount = Convert.ToDouble(checkShift.Rows[0]["AppliedLeaveQty"]);
                                        string dayType = checkShift.Rows[0]["DayTypeId"].ToString();
                                        if ((isDayOff && dayOffHalfLeaveCount == 0.5) || (!isDayOff && shiftLeaveCount == 0.5))
                                        {
                                            objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(0.5), "HalfDay", Convert.ToInt32(dayType),
                                                1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                        }
                                        else if (!isDayOff && shiftLeaveCount == 1)
                                        {
                                            objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(1), "FullDay", Convert.ToInt32(dayType),
                                                1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                        }
                                    }
                                    else if (leaveTypeCode == "DAYOFFFULL")
                                    {
                                        objMksLeave.AddLeaveDetails(LeaveId, leaveTypeId, Convert.ToDateTime(ReqDate), Convert.ToDecimal(1), "FullDay", Convert.ToInt32(1007),
                                                1, DateName, "", false, Convert.ToDecimal(0), employeeId, dayOffHalfDayShift, AddedUserName);
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }

                    if (objMksLeave.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }

                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }


        public int ImportTravellingAllowance(DataTable dtEmployees, string AddedUserName)
        {
            ExcelUplodedTimeData uploadedTravellingAllowance = new ExcelUplodedTimeData();
            int SavedItemCount = 0;

            try
            {
                DataSet ds = new DataSet();

                ErrorsOccurred = new List<string>();


                for (int i = 0; i < dtEmployees.Rows.Count; i++)
                {

                    long EmployeeId = Convert.ToInt64(dtEmployees.Rows[i]["EmployeeId"].ToString());
                    DateTime Date = Convert.ToDateTime(dtEmployees.Rows[i]["Date"].ToString());
                    decimal Hours = Convert.ToDecimal(dtEmployees.Rows[i]["Hours"].ToString());
                    decimal Amount = Convert.ToDecimal(dtEmployees.Rows[i]["Amount"].ToString());

                    objRawDataUploadDAL.AddTravellingAllowance(EmployeeId, Date, Hours, Amount);

                    if (objRawDataUploadDAL.IsError == true)
                    {
                        isRecordUploaded = false;
                        ErrorsOccurred.Add(objRawDataUploadDAL.ErrorMsg);
                    }
                    else
                    {
                        isRecordUploaded = true;
                    }

                    if (isRecordUploaded == true)
                    {
                        SavedItemCount++;
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
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return SavedItemCount;
        }



        public bool isRecordUploaded { get; set; }
    }
}
