/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, NO. 51, Flower Road, Colombo 7, Sri Lanka.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name : HRM.Payroll.DAL
/// Cording Standard : MasterKey Cording Standards
/// Author : Asanka C. Amarasinghe
/// Created Timestamp : 25-August-2011
/// Class Description : HRM.Payroll.DAL.RawDataUploadDAL
/// Task Code: --

namespace HRM.Payroll.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class RawDataUploadDAL
    {
        #region Fields

        private SqlConnection dbConnection;

        private bool _isError;
        private string _errorMsg;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxTypesDAL"/> class.
        /// </summary>
        public RawDataUploadDAL()
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


        private void SetError(SqlException Ex)
        {
            
            _isError = true;
            _errorMsg = Ex.Message;
        }

        public Int64 AddEmployee(string _employeeCode, string _firstName, string _lastName, string _fullName, string _callName, 
            string _homeContactNo, string _mobileNo, string _address, string _email, string _nic, string _gender, 
            DateTime _dateOfBirth, int _status, string _image, int _branchId, int _jobCategoryId, int _orgStructureId, 
            int _designationId, int _jobGradeId, int _employmentTypeId, string _emergencyContactPerson, 
            string _emergencyContactNo, int _employmentGradeId, string _postalCode, DateTime _recrutementDate, 
            DateTime? _confirmationDate, DateTime? _secondReqruitmentDate, string _previousEmployeeNo, DateTime? _resignDate,
            int _companyID, string _remarks, string _createdUser, int _inactiveStatus)
        {
            Int64 _employeeId = -1;
            
            try
            {
                using (SqlCommand command = new SqlCommand("COM_ImportNewEmployeeFromExcel", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeCode", _employeeCode);
                    command.Parameters.AddWithValue("@FirstName", _firstName);
                    command.Parameters.AddWithValue("@LastName", _lastName);
                    command.Parameters.AddWithValue("@FullName", _fullName);
                    command.Parameters.AddWithValue("@CallName", _callName);
                    command.Parameters.AddWithValue("@HomeContactNo", _homeContactNo);
                    command.Parameters.AddWithValue("@MobileNo", _mobileNo);
                    command.Parameters.AddWithValue("@Address", _address);
                    command.Parameters.AddWithValue("@Email", _email);
                    command.Parameters.AddWithValue("@NIC", _nic);
                    command.Parameters.AddWithValue("@Gender", _gender);
                    command.Parameters.AddWithValue("@DateOfBirth", _dateOfBirth);
                    command.Parameters.AddWithValue("@Status", _status);
                    command.Parameters.AddWithValue("@Image", _image);
                    command.Parameters.AddWithValue("@BranchID", _branchId);
                    command.Parameters.AddWithValue("@JobCategoryID", _jobCategoryId);
                    command.Parameters.AddWithValue("@OrgStructureID", _orgStructureId);
                    command.Parameters.AddWithValue("@DesignationStuctureID", _designationId);
                    command.Parameters.AddWithValue("@JobGradeID", _jobGradeId);
                    command.Parameters.AddWithValue("@EmploymentTypeID", _employmentTypeId);
                    command.Parameters.AddWithValue("@EmergencyContactPerson", _emergencyContactPerson);
                    command.Parameters.AddWithValue("@EmergencyContactNo", _emergencyContactNo);
                    command.Parameters.AddWithValue("@EmploymentGradeID", _employmentGradeId);
                    command.Parameters.AddWithValue("@PostalCode", _postalCode);
                    command.Parameters.AddWithValue("@RecruitementDate", _recrutementDate);
                    command.Parameters.AddWithValue("@ConfirmationDate", _confirmationDate);
                    command.Parameters.AddWithValue("@SecondReqruitmentDate", _secondReqruitmentDate);
                    command.Parameters.AddWithValue("@PreviousEmployeeNo", _previousEmployeeNo);
                    command.Parameters.AddWithValue("@ResignDate", _resignDate);
                    command.Parameters.AddWithValue("@CompanyID", _companyID);
                    command.Parameters.AddWithValue("@Remarks", _remarks);
                    command.Parameters.AddWithValue("@InactiveStatus", _inactiveStatus);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);                    
                    
                    SqlParameter imployeeId = new SqlParameter("@EmployeeID", SqlDbType.BigInt, 16);
                    imployeeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(imployeeId);

                    command.ExecuteNonQuery();

                    _employeeId = Convert.ToInt64(imployeeId.Value);
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

            return _employeeId;
        }


        public Int64 
            AddEmployeeTime(int _empId, string _employeeCode, DateTime? _date,  DateTime? _inTime1,
     DateTime? _inTime2, DateTime? _outTime1, DateTime? _outTime2,
     string _createdUser,bool _isFormTimeSheet)
        {
            Int64 _employeeId = -1;

            try
            {
                double timevalue = 0.00;
                double lateHrs = 0.00;
                double earlyHrs = 0.00;
                double extraHrs = 0.00;
                double lessyHrs = 0.00;
                TimeSpan? totalHrs2 = null;

                using (SqlCommand command = new SqlCommand("COM_ImportNewEmployeeTimeFromExcel", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    if (_isFormTimeSheet)
                    {
                       
                      //  TimeSpan firstHalf = (TimeSpan)(_inTime2 - _outTime2);
                      //  TimeSpan secondHalf = (TimeSpan)(_inTime2 - _outTime2);
                      //  TimeSpan totalHrs = (TimeSpan)(firstHalf + secondHalf);


                        if (_outTime2 == null  && _inTime1 == null)
                        {

                           timevalue = 0.0;
                        }
                        else if (_outTime2 != null && _inTime1 != null)
                        {
                            DateTime? timeIN = _inTime1;
                          
                            if (_inTime1< DateTime.Parse("7:30"))
                            {
                                timeIN = DateTime.Parse("7:30");
                            }
                            TimeSpan totalHrs = (TimeSpan)(_outTime2 - timeIN);
                            totalHrs2 = totalHrs;
                           var mint = totalHrs.ToString();
                           var domain = mint.Split(':')[1];           // google.com
                          // var tld = domain.Substring(domain.IndexOf('.')) // .com
                                                        

                            timevalue = Convert.ToDouble(totalHrs.Hours + "." + domain);

                            if ((_outTime1 != null && _inTime2 != null))
                            {
                                TimeSpan outTime = (TimeSpan)(_inTime2 - _outTime1);

                                var mintTotal = outTime.ToString();
                                var domainTotal = mintTotal.Split(':')[1];
                                TimeSpan workingHrs = (TimeSpan)(totalHrs - outTime);
                                totalHrs2 = workingHrs;

                                double WorkingHours = Convert.ToDouble(workingHrs.Hours + "." + workingHrs.Minutes);

                                timevalue = WorkingHours;
                                
                            }

                            
                        }



                        //TimeSpan sincelast = TimeSpan.FromTicks( (_outTime1) - _inTime1);
                         string timein ="";
                         string outTime2 = "";
                       
                        if(_inTime1 != null)
                         timein = _inTime1.Value.ToShortTimeString();

                        if (_outTime2 != null)
                         outTime2 = _outTime2.Value.ToShortTimeString();


                        DateTime dt1 = DateTime.Parse("8:30");
                       // DateTime dt2 = DateTime.Parse(timein);
                         lateHrs = 0.00;

                         if (timein != "" && (dt1 < DateTime.Parse(timein)))
                         {
                             TimeSpan result = DateTime.Parse(timein) -dt1;
                             var mint = result.ToString();
                             var domain = mint.Split(':')[1];
                             lateHrs = Convert.ToDouble(result.Hours + "." + domain);

                         }

                         DateTime dt3 = DateTime.Parse("5:00 PM");
                       // DateTime dt4 = DateTime.Parse(outTime2);

                         earlyHrs = 0.00;

                         if (outTime2 != "" && (DateTime.Parse(outTime2) < dt3))
                         {
                             TimeSpan result = dt3 - DateTime.Parse(outTime2);
                             var mint = result.ToString();
                             var domain = mint.Split(':')[1];
                             earlyHrs = Convert.ToDouble(result.Hours + "." + domain);

                         }

                         if (timevalue > 8.30)
                         {

                             TimeSpan? result = TimeSpan.Parse("8:30");
                             result = totalHrs2-  TimeSpan.Parse("8:30") ;
                             var mint = result.ToString().Split(':')[1];
                             var hrs = result.ToString().Split(':')[0];
                             extraHrs = Convert.ToDouble(hrs + "." + mint);
                             
                         }
                         else
                         {
                             lessyHrs = (8.30 - timevalue);
                             TimeSpan? result = TimeSpan.Parse("8:30");                           
                             if (timevalue != 0)
                             {
                                 result = TimeSpan.Parse("8:30") - totalHrs2; 
                             }
                            
                             var mint = result.ToString().Split(':')[1];
                             var hrs = result.ToString().Split(':')[0];
                             lessyHrs = Convert.ToDouble(hrs + "." + mint);
                         }
                    }

                    command.Parameters.AddWithValue("@EmpID", _empId);
                    command.Parameters.AddWithValue("@EmployeeCode", _employeeCode);
                    command.Parameters.AddWithValue("@Date", _date);
                    command.Parameters.AddWithValue("@TimeIn", _inTime1);
                    command.Parameters.AddWithValue("@LunchOut", _outTime1);
                    command.Parameters.AddWithValue("@LunchIn", _inTime2);
                    command.Parameters.AddWithValue("@TimeOut", _outTime2);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
                    command.Parameters.AddWithValue("@LateHrs", lateHrs);
                    command.Parameters.AddWithValue("@EarlyHrs", earlyHrs);
                    command.Parameters.AddWithValue("@WorkingHrs", timevalue);
                    command.Parameters.AddWithValue("@ExtraHrs", extraHrs);
                    command.Parameters.AddWithValue("@LessHrs", lessyHrs);

                    SqlParameter imployeeId = new SqlParameter("@EmployeeID", SqlDbType.BigInt, 16);
                    imployeeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(imployeeId);

                    command.ExecuteNonQuery();

                    _employeeId = Convert.ToInt64(imployeeId.Value);
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

            return _employeeId;
        }
        public void ImportEmployeeInExcelUpdate(long EmployeeID, string ColumnName, string RowData, string EncryptionKey)
        {


            try
            {
                using (SqlCommand command = new SqlCommand("Emp_UpdateEmployeeForExcel", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@ColumnName", ColumnName);
                    command.Parameters.AddWithValue("@RowData", RowData);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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
        public Int64 ImportEmployeeIncrement(long EmployeeID, string ColumnName, decimal RowData, string EncryptionKey, string CreatedUser)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddSalaryIncrement", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@ColumnName", ColumnName);
                    command.Parameters.AddWithValue("@RowData", RowData);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }




        public Int64 AddEmployeeForCommonModule(string _employeeCode, string _trueEmployeeCode, string _firstName, string _lastName, string _fullName, string _callName,string _nameWithInitial,
            string _homeContactNo, string _homeContactNo2, string _mobileNo, string _mobileNo2, string _mobileNo3, string _officeNo, string _addressLine1, string _addressLine2, string _addresscity, string _tempaddress1, string _tempaddress2, string _tempaddresscity, string _email, string _nic, string _epfno, string _gender,string _bcard,
            DateTime _dateOfBirth, int _status, string _image, int _branchId, int _jobCategoryId, int _orgStructureId,
            int _designationId, int _jobGradeId, int _employmentTypeId, string _emergencyContactPerson, string _emergencyContactAddressLine1, string _emergencyContactAddressLine2, string _emergencyContactAddressCity,
            string _emergencyContactNo, string _emergencyContacthomeNo, string _relationshipOfContactPerson, int _employmentGradeId, string _postalCode, DateTime _recrutementDate, DateTime? _epfentitledate,
            DateTime? _confirmationDate, DateTime? _secondReqruitmentDate, string _previousEmployeeNo, DateTime? _resignDate,
            int _companyID, string _remarks, string _createdUser, int _inactiveStatus, string _occupationGrade, string _payrollAct, string _directIndirect, int? _costCenterId, DateTime? _probationStartDate,
            DateTime? _probationEndDate, DateTime? _fixedTermContractStartDate, DateTime? __fixedTermContractEndDate, DateTime? _consultancyStartDate, DateTime? _consultancyEndDate, DateTime _retirementDate)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("COM_ImportNewEmployeeFromExcel", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeCode", _employeeCode);
                    command.Parameters.AddWithValue("@TrueEmployeeCode", _trueEmployeeCode);
                    command.Parameters.AddWithValue("@FirstName", _firstName);
                    command.Parameters.AddWithValue("@LastName", _lastName);
                    command.Parameters.AddWithValue("@FullName", _fullName);
                    command.Parameters.AddWithValue("@CallName", _callName);
                    command.Parameters.AddWithValue("@NameWithInitial", _nameWithInitial);
                    command.Parameters.AddWithValue("@HomeContactNo", _homeContactNo);
                    command.Parameters.AddWithValue("@HomeContactNo2", _homeContactNo2);
                    command.Parameters.AddWithValue("@MobileNo", _mobileNo);
                    command.Parameters.AddWithValue("@MobileNo2", "");
                    command.Parameters.AddWithValue("@MobileNo3", "");
                    command.Parameters.AddWithValue("@OfficeNo", _officeNo);
                    command.Parameters.AddWithValue("@AddressLine1", _addressLine1);
                    command.Parameters.AddWithValue("@AddressLine2", _addressLine2);
                    command.Parameters.AddWithValue("@AddressCity", _addresscity);
                    command.Parameters.AddWithValue("@TempAddress1", _tempaddress1);
                    command.Parameters.AddWithValue("@TempAddress2", _tempaddress2);
                    command.Parameters.AddWithValue("@TempAddressCity", _tempaddresscity);
                    command.Parameters.AddWithValue("@Email", _email);
                    command.Parameters.AddWithValue("@NIC", _nic);
                    command.Parameters.AddWithValue("@EPFNo", _epfno);
                    command.Parameters.AddWithValue("@Gender", _gender);
                    command.Parameters.AddWithValue("@BCard", _bcard);
                    command.Parameters.AddWithValue("@DateOfBirth", _dateOfBirth);
                    command.Parameters.AddWithValue("@Status", _status);
                    command.Parameters.AddWithValue("@Image", _image);
                    command.Parameters.AddWithValue("@BranchID", _branchId);
                    command.Parameters.AddWithValue("@JobCategoryID", _jobCategoryId); // null
                    command.Parameters.AddWithValue("@OrgStructureID", _orgStructureId);
                    command.Parameters.AddWithValue("@DesignationID", _designationId); //null
                    command.Parameters.AddWithValue("@JobGradeID", _jobGradeId); // null
                    command.Parameters.AddWithValue("@EmploymentTypeID", _employmentTypeId); // null
                    command.Parameters.AddWithValue("@EmergencyContactPerson", _emergencyContactPerson);
                    command.Parameters.AddWithValue("@EmergencyContactAddressLine1", _emergencyContactAddressLine1);
                    command.Parameters.AddWithValue("@EmergencyContactAddressLine2", _emergencyContactAddressLine2);
                    command.Parameters.AddWithValue("@EmergencyContactCity", _emergencyContactAddressCity);
                    command.Parameters.AddWithValue("@EmergencyContactNo", _emergencyContactNo);
                    command.Parameters.AddWithValue("@EmergencyContactHomeNo", _emergencyContacthomeNo);
                    command.Parameters.AddWithValue("@RelationshipContactPerson", _relationshipOfContactPerson);
                    command.Parameters.AddWithValue("@EmploymentGradeID", _employmentGradeId); 
                    command.Parameters.AddWithValue("@PostalCode", _postalCode);
                    command.Parameters.AddWithValue("@RecruitementDate", _recrutementDate);
                    command.Parameters.AddWithValue("@EPFNoDate", _epfentitledate);
                    command.Parameters.AddWithValue("@ConfirmationDate", _confirmationDate);
                    command.Parameters.AddWithValue("@SecondReqruitmentDate", _secondReqruitmentDate);
                    command.Parameters.AddWithValue("@PreviousEmployeeNo", _previousEmployeeNo);
                    command.Parameters.AddWithValue("@ResignDate", _resignDate);
                    command.Parameters.AddWithValue("@CompanyID", _companyID);
                    command.Parameters.AddWithValue("@Remarks", _remarks);
                    command.Parameters.AddWithValue("@InactiveStatus", _inactiveStatus);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
                    command.Parameters.AddWithValue("@OccupationGrade", _occupationGrade);
                    command.Parameters.AddWithValue("@PayrollAct", _payrollAct);
                    command.Parameters.AddWithValue("@DirectIndirect", _directIndirect);
                    command.Parameters.AddWithValue("@CostCenterId", _costCenterId);
                    command.Parameters.AddWithValue("@ProbationStartDate", _probationStartDate);
                    command.Parameters.AddWithValue("@ProbationEndDate", _probationEndDate);
                    command.Parameters.AddWithValue("@FixedTermContractStartDate", _fixedTermContractStartDate);
                    command.Parameters.AddWithValue("@FixedTermContractEndDate", __fixedTermContractEndDate);
                    command.Parameters.AddWithValue("@ConsultancyStartDate", _consultancyStartDate);
                    command.Parameters.AddWithValue("@ConsultancyEndDate", _consultancyEndDate);
                    command.Parameters.AddWithValue("@RetirementDate", _retirementDate);

                    SqlParameter imployeeId = new SqlParameter("@EmployeeID", SqlDbType.BigInt, 16);
                    imployeeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(imployeeId);


                    command.ExecuteNonQuery();
                    command.Parameters.Clear();



                    _employeeId = Convert.ToInt64(imployeeId.Value);
                }
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeOrganizaitonLeval", dbConnection))
                {
                    OpenDB();
                    command.Parameters.AddWithValue("@OrgStructureID", _orgStructureId);
                    command.Parameters.AddWithValue("@CompanyID", _companyID);
                    command.Parameters.AddWithValue("@EmployeeID", _employeeId);
                   
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    
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

            return _employeeId;
        }

        public void AddEmployeeAdditionalDetails(long _employeeId, int? _nationalId, int? _religionId, string _bloodGroup, string _province, string _district, string _electoralDistrict,
            string _divisionalSecretariats, string _gsDivision, string _transportRoute, string _DistanceforPermanentAddress, string _createdUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddEmployeeAdditionalDetails", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeId", _employeeId);
                    command.Parameters.AddWithValue("@NationalityId", _nationalId);
                    command.Parameters.AddWithValue("@ReligionId", _religionId);
                    command.Parameters.AddWithValue("@BloodGroup", _bloodGroup);
                    command.Parameters.AddWithValue("@Province", _province);
                    command.Parameters.AddWithValue("@District", _district);
                    command.Parameters.AddWithValue("@ElectoralDistrict", _electoralDistrict);
                    command.Parameters.AddWithValue("@DivisionalSecretariats", _divisionalSecretariats);
                    command.Parameters.AddWithValue("@GSDivision", _gsDivision);
                    command.Parameters.AddWithValue("@TransportRoute", _transportRoute);
                    command.Parameters.AddWithValue("@DistanceforPermanentAddress", _DistanceforPermanentAddress);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
                    
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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


        public Int64 AddEmployeeInPayTxn(long _employeeId, int _payPeriodId, decimal _attendenceAll, decimal _noPayDays,
        decimal _otHRS1, decimal _otHRS2, decimal _otHRS3, bool _isPosted, decimal _payeTax, decimal _epf, bool _isDeleted,
        decimal _food, decimal _mobile, decimal _otherDedu, decimal _loan, decimal _singer)
        {
            
            try
            {
                using (SqlCommand command = new SqlCommand("PAY_ImportPayTxnFromExcel", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeId", _employeeId);
                    command.Parameters.AddWithValue("@PayPeriodId", _payPeriodId);
                    command.Parameters.AddWithValue("@AttendenceAll", _attendenceAll);
                    command.Parameters.AddWithValue("@NoPayDays", _noPayDays);
                    command.Parameters.AddWithValue("@OtHRS1", _otHRS1);
                    command.Parameters.AddWithValue("@OtHRS2", _otHRS2);
                    command.Parameters.AddWithValue("@OtHRS3", _otHRS3);
                    command.Parameters.AddWithValue("@IsPosted", _isPosted);
                    command.Parameters.AddWithValue("@PayeTax", _payeTax);
                    command.Parameters.AddWithValue("@Epf", _epf);
                    command.Parameters.AddWithValue("@IsDeleted", _isDeleted);
                    command.Parameters.AddWithValue("@Food", _food);
                    command.Parameters.AddWithValue("@Mobile", _mobile);
                    command.Parameters.AddWithValue("@OtherDedu", _otherDedu); // null
                    command.Parameters.AddWithValue("@Loan", _loan);
                    command.Parameters.AddWithValue("@Singer", _singer); //null
                  
                     SqlParameter imployeeId = new SqlParameter("@EmployeeID", SqlDbType.BigInt, 16);
                    imployeeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(imployeeId);


                    command.ExecuteNonQuery();
                    command.Parameters.Clear();



                    _employeeId = Convert.ToInt64(imployeeId.Value);
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

            return _employeeId;
        }


        public void AddTransactionFomExelColumn(string FieldCode)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddUserFieldByExcel", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FieldCode", FieldCode);
                    command.Parameters.AddWithValue("@FieldDescription", FieldCode);
                    command.Parameters.AddWithValue("@FieldType", "Transaction Field");
                    command.Parameters.AddWithValue("@DataType", "decimal");
                    command.Parameters.AddWithValue("@Length", 18);
                    command.Parameters.AddWithValue("@DecimalPoints", 2);
                    command.Parameters.AddWithValue("@IsNull", 1);
                    command.Parameters.AddWithValue("@DefaultValue", "0");
                    command.Parameters.AddWithValue("@CheckConstraints", " ");

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
                dbConnection.Close();
            }
        }

        public Int64 AddEmployeeFomExelFixAllowance(long EmployeeID, string ColumnName, decimal RowData, string EncryptionKey, string CreatedUser)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeFixAllowance", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@ColumnName", ColumnName);
                    command.Parameters.AddWithValue("@RowData", RowData);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }

        public Int64 AddEmployeeFromExelMobileBill(long EmployeeID, decimal RowData, string EncryptionKey)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeMobileBill", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@RowData", RowData);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }

        public Int64 ImportEmployeeAdvance(long EmployeeID, string FieldCode, decimal Amount, DateTime StartDate, int Installments, string EncryptionKey, string CreateUser)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_AddAdvanceFromExcel", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@AdvanceType", FieldCode);
                    command.Parameters.AddWithValue("@Amount", Amount);
                    command.Parameters.AddWithValue("@NoOfInstallments", Installments);
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.Parameters.AddWithValue("@CreatedUser", CreateUser);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }

        public Int64 AddEmployeeFomExelTransation(long EmployeeID, string ColumnName, decimal RowData, string EncryptionKey)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("PAY_AddEmployeeTransation", dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@ColumnName", ColumnName);
                    command.Parameters.AddWithValue("@RowData", RowData);
                    command.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }
        public Int64 AddEmployeeFomExel(string EmployeeCode, string Day, string Month, string Year, string punchTime , string attType, string CreatedUser,int CompanyId)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("COM_RaDataTimeFromExcel", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    //command.Parameters.AddWithValue("@attType", attType);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@punchTime", punchTime);
                    command.Parameters.AddWithValue("@CompanyId", punchTime);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }


        public Int64 AddTravellingAllowance(long EmployeeId, DateTime Date, decimal Hours, decimal Amount)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Time_RawDataTravellingAllowance", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@Hours", Hours);
                    command.Parameters.AddWithValue("@Amount", Amount);


                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }



        public Int64 AddLeaveEntitlement(long EmployeeId, decimal RowValue, decimal RowCount)
        {
            Int64 _employeeId = -1;

            try
            {
                using (SqlCommand command = new SqlCommand("Lev_RaDataLeaveEntitlement", dbConnection))
                {
                    OpenDB();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@RowValue", RowValue);
                    command.Parameters.AddWithValue("@RowCount", RowCount);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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

            return _employeeId;
        }


        


        #endregion

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FestivalAdvanceDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~RawDataUploadDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
