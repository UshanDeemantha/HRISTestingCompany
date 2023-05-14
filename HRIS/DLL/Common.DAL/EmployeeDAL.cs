/// <summary>
/// Solution Name : HRM
/// Project Name : Common.DAL
/// Author : ushan deemantha
/// Class Description : Employee DAL
/// </summary>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.Common.DAL
{
    public class EmployeeDAL
    {
        #region Fields

        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private int _companyID;
        private long _employeeId;
        private long _empId;
        private string _employeeCode;
        private string _epfNo;
        private string _firstName;
        private string _fullName;
        private string _lastName;
        private string _callName;
        private string _nameInitials;
        private string _homeContactNo;
        private string _homeContactNo2;
        private string _officeContactNo;
        private string _mobileNo;
        private string _mobileNo2;
        private string _mobileNo3;
        private string _address;
        private string _address2;
        private string _addressCity;
        private string _email;
        private string _nic;
        private string _gender;
        private DateTime _dateOfBirth;
        private int _status;
        private string _image;
        private int _branchId;
        private int _jobCategoryId;
        private int _orgStructureId;
        private int _designationId;
        private int _jobGradeId;
        private int _employmentTypeId;
        private string _emergencyContactNo;
        private string _emergencyContactNoHome;
        private string _emergencyContactPerson;
        private string _emergencyRelationship;
        private string _emergencyContactAddressLine1;
        private string _emergencyContactAddressLine2;
        private string _emergencyContactAddressCity;
        private string _temporaryAddressLine1;
        private string _temporaryAddressLine2;
        private string _temporaryCity;
        private string _occupationGrade;
        private string _postalCode;
        private int _employmentGradeId;
        private int _uploadId;
        private DateTime _recrutementDate;
        private DateTime _retirementDate;
        private DateTime _confirmationDate;
        private DateTime _epfnoDate;
        private DateTime _resignDate;
        private string _barCodeId;
        private string _remarks;
        private string _createdUser;
        private string _updateUser;
        private int _organizationTypeID;
        private string _userPreFix;
        private string _passportNo;
        private DateTime? _passportExpiryDate;
        private string _visaNo;
        private DateTime? _visaExpiryDate;
        private bool _isExpatriate;
        private string _empcard;
        private bool _active;
        private int _inactiveStatus;
        private string _payrollAct;
        private string _directIndirect;
        private int _costCenter;
        private DateTime? _probationStartDate;
        private DateTime? _probationEndDate;
        private DateTime? _fixedTermContarctStartDate;
        private DateTime? _fixedTermContarctEndDate;
        private DateTime? _consultancyStartDate;
        private DateTime? _consultancyEndDate;
        private DateTime? _inactiveDate;
        private DateTime? _terminatedDate;
        private DateTime? _reActivatedDate;

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


        public string PassPortNo
        {
            get { return _passportNo; }
            set
            {
                _passportNo = value;
            }
        }
        public string VisaNo
        {
            get { return _visaNo; }
            set
            {
                _visaNo = value;
            }
        }
        public DateTime? PasportExpiryDate
        {
            get { return _passportExpiryDate; }
            set
            {
                _passportExpiryDate = value;
            }
        }
        public DateTime? VisaExpiryDate
        {
            get { return _visaExpiryDate; }
            set
            {
                _visaExpiryDate = value;
            }
        }

        public bool IsExpatriate
        {
            get { return _isExpatriate; }
            set
            {
                _isExpatriate = value;
            }
        }

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        } 

        /// <summary>
        /// Gets the employee ID.
        /// </summary>
        /// <value>The employee ID.</value>
        public long EmployeeID
        {
            get
            {
                return _employeeId;
            }
        }

        public long EmpID
        {
            get
            {
                return _empId;
            }
        }



        public string NameInitials
        {
            get
            {
                return _nameInitials;
            }
            set
            {
                _nameInitials = value;
            }
        }
        public string TemporaryAddressLine1
        {
            get
            {
                return _temporaryAddressLine1;

            }
            set
            {
                _temporaryAddressLine1 = value;
            }
        }
        public string TemporaryAddressLine2
        {
            get
            {
                return _temporaryAddressLine2;

            }
            set
            {
                _temporaryAddressLine2 = value;
            }
        }
        public string TemporaryAddressCity
        {
            get
            {
                return _temporaryCity;

            }
            set
            {
                _temporaryCity = value;
            }
        }

        /// <summary>
        /// Gets or sets the employee code.
        /// </summary>
        /// <value>The employee code.</value>
        public string EmployeeCode
        {
            get
            {
                return _employeeCode;
            }
            set
            {
                _employeeCode = value;
            }
        }

        public string EPFNo
        {
            get
            {
                return _epfNo;
            }
            set
            {
                _epfNo = value;
            }
        }


        public int InactiveStatus
        {
            get
            {
                return _inactiveStatus;
            }
            set
            {
                _inactiveStatus = value;
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        /// <summary>
        /// Gets or sets the cast name.
        /// </summary>
        /// <value>The cast name.</value>
        public string CallName
        {
            get
            {
                return _callName;
            }
            set
            {
                _callName = value;
            }
        }

        /// <summary>
        /// Gets or sets the home contact no.
        /// </summary>
        /// <value>The home contact no.</value>
        public string HomeContactNo
        {
            get
            {
                return _homeContactNo;
            }
            set
            {
                _homeContactNo = value;
            }
        }

        public string HomeContactNo2
        {
            get
            {
                return _homeContactNo2;
            }
            set
            {
                _homeContactNo2 = value;
            }
        }
        /// <summary>
        /// Gets or sets the office contact no.
        /// </summary>
        /// <value>The office contact no.</value>
        public string OfficeContactNo
        {
            get
            {
                return _officeContactNo;
            }
            set
            {
                _officeContactNo = value;
            }
        }
        /// <summary>
        /// Gets or sets the mobile no.
        /// </summary>
        /// <value>The mobile no.</value>
        public string MobileNo
        {
            get
            {
                return _mobileNo;
            }
            set
            {
                _mobileNo = value;
            }
        }

        public string MobileNo2
        {
            get
            {
                return _mobileNo2;
            }
            set
            {
                _mobileNo2 = value;
            }
        }

        public string MobileNo3
        {
            get
            {
                return _mobileNo3;
            }
            set
            {
                _mobileNo3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
            }
        }
        public string City
        {
            get
            {
                return _addressCity;
            }
            set
            {
                _addressCity = value;
            }
        }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// Gets or sets the NIC.
        /// </summary>
        /// <value>The NIC.</value>
        public string NIC
        {
            get
            {
                return _nic;
            }
            set
            {
                _nic = value;
            }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                try
                {
                    _dateOfBirth = value;
                }
                catch { }
            }
        }


        public DateTime RetirementDate
        {
            set
            {
                try
                {
                    _retirementDate = value;
                }
                catch { }
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        /// <summary>
        /// Gets or sets the branch ID.
        /// </summary>
        /// <value>The branch ID.</value>
        public int BranchID
        {
            get
            {
                return _branchId;
            }
            set
            {
                _branchId = value;
            }
        }

        /// <summary>
        /// Gets or sets the job category ID.
        /// </summary>
        /// <value>The job category ID.</value>
        public int JobCategoryID
        {
            get
            {
                return _jobCategoryId;
            }
            set
            {
                _jobCategoryId = value;
            }
        }

        /// <summary>
        /// Gets or sets the org structure ID.
        /// </summary>
        /// <value>The org structure ID.</value>
        public int OrgStructureID
        {
            get
            {
                return _orgStructureId;
            }
            set
            {
                _orgStructureId = value;
            }
        }

        /// <summary>
        /// Gets or sets the designation stucture ID.
        /// </summary>
        /// <value>The designation stucture ID.</value>
        public int DesignationID
        {
            get
            {
                return _designationId;
            }
            set
            {
                _designationId = value;
            }
        }

        /// <summary>
        /// Gets or sets the job grade ID.
        /// </summary>
        /// <value>The job grade ID.</value>
        public int JobGradeID
        {
            get
            {
                return _jobGradeId;
            }
            set
            {
                _jobGradeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the employment type ID.
        /// </summary>
        /// <value>The employment type ID.</value>
        public int EmploymentTypeID
        {
            get
            {
                return _employmentTypeId;
            }
            set
            {
                _employmentTypeId = value;
            }
        }


        /// <summary>
        /// Gets or sets the emergency contact person.
        /// </summary>
        /// <value>The emergency contact person.</value>
        public string EmergencyContactPerson
        {
            get
            {
                return _emergencyContactPerson;
            }
            set
            {
                _emergencyContactPerson = value;
            }
        }

        /// <summary>
        /// Gets or sets the emergency contact person.
        /// </summary>
        /// <value>The emergency contact person.</value>
        public string EmergencyContactPersonRelationship
        {
            get
            {
                return _emergencyRelationship;
            }
            set
            {
                _emergencyRelationship = value;
            }
        }

        public string EmergencyContactAddressLine1
        {
            get
            {
                return _emergencyContactAddressLine1;
            }
            set
            {
                _emergencyContactAddressLine1 = value;
            }
        }
        public string EmergencyContactAddressLine2
        {
            get
            {
                return _emergencyContactAddressLine2;
            }
            set
            {
                _emergencyContactAddressLine2 = value;
            }
        }
        public string EmergencyContactCity
        {
            get
            {
                return _emergencyContactAddressCity;
            }
            set
            {
                _emergencyContactAddressCity = value;
            }
        }

        public string OccupationGrade
        {
            get
            {
                return _occupationGrade;
            }
            set
            {
                _occupationGrade = value;
            }
        }

        /// <summary>
        /// Gets or sets the emergency contact no.
        /// </summary>
        /// <value>The emergency contact no.</value>

        public string EmergencyContactNo
        {
            get
            {
                return _emergencyContactNo;

            }
            set
            {
                _emergencyContactNo = value;
            }
        }

        public string EmergencyContactNoHome
        {
            get
            {
                return _emergencyContactNoHome;

            }
            set
            {
                _emergencyContactNoHome = value;
            }
        }

        /// <summary>
        /// Gets or sets the employment grade ID.
        /// </summary>
        /// <value>The employment grade ID.</value>
        public int EmploymentGradeID
        {
            get
            {
                return _employmentGradeId;
            }
            set
            {
                _employmentGradeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the upload  ID.
        /// </summary>
        /// <value>The upload ID.</value>
        public int FileUploadID
        {
            get
            {
                return _uploadId;
            }
            set
            {
                _uploadId = value;
            }
        }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                _postalCode = value;
            }
        }

        /// <summary>
        /// Sets the resign date.
        /// </summary>
        /// <value>The resign date.</value>
        public DateTime ResignDate
        {
            set
            {
                _resignDate = value;
            }
        }

        public DateTime? InactiveDate
        {
            get
            {
                return _inactiveDate;
            }
            set
            {
                _inactiveDate = value;
            }
        }

        public DateTime? TerminateDate
        {
            get
            {
                return _terminatedDate;
            }
            set
            {
                _terminatedDate = value;
            }
        }
        public DateTime? ReActivatedDate
        {
            get
            {
                return _reActivatedDate;
            }
            set
            {
                _reActivatedDate = value;
            }
        }
        

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EmployeeDAL"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
         /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EmployeeDAL"/> is empcard.
        /// </summary>
        /// <value><c>true</c> if empcard; otherwise, <c>false</c>.</value>
        public string EMPCard
        {
            get
            {
                return _empcard;
            }
            set
            {
                _empcard = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the recrutement date.
        /// </summary>
        /// <value>The recrutement date.</value>
        public DateTime RecrutementDate
        {
            set
            {
                try
                {
                    _recrutementDate = value;
                }
                catch { }
            }
        }

        /// <summary>
        /// Gets or sets the Permanent Date.
        /// </summary>
        /// <value>The Permanent Date.</value>
        public DateTime ConfirmationDate
        {
            set
            {
                try
                {
                    _confirmationDate = value;
                }
                catch { }
            }
        }

        /// <summary>
        /// Gets or sets the Permanent Date.
        /// </summary>
        /// <value>The Permanent Date.</value>
        public DateTime EpfNoDate
        {
            set
            {
                try
                {
                    _epfnoDate = value;
                }
                catch { }
            }
        }

        /// <summary>
        /// Gets or sets the bar code ID.
        /// </summary>
        /// <value>The bar code ID.</value>
        public string BarCodeID
        {
            get
            {
                return _barCodeId;
            }
            set
            {
                _barCodeId = value;
            }
        }

        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                _remarks = value;
            }
        }
        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }
     

        public string CreatedUser
        {
            set
            {
                _createdUser = value;
            }
        }
        public string UpdateUser
        {
            set
            {
                _updateUser = value;
            }
        }
        public int OrganizationTypeID
        {
            set
            {
                _organizationTypeID = value;
            }
        }

        public string UserPreFix
        {
            set
            {
                _userPreFix = value;
            }
        }

        public string PayrollAct
        {
            get
            {
                return _payrollAct;
            }
            set
            {
                _payrollAct = value;
            }
        }

        public string DirectIndirect
        {
            get
            {
                return _directIndirect;
            }
            set
            {
                _directIndirect = value;
            }
        }

        public int CostCenter
        {
            get
            {
                return _costCenter;
            }
            set
            {
                _costCenter = value;
            }
        }

        public DateTime? ProbationStartDate
        {
            get { return _probationStartDate; }
            set
            {
                _probationStartDate = value;
            }
        }

        public DateTime? ProbationEndDate
        {
            get { return _probationEndDate; }
            set
            {
                _probationEndDate = value;
            }
        }

        public DateTime? FixedTermContarctStartDate
        {
            get { return _fixedTermContarctStartDate; }
            set
            {
                _fixedTermContarctStartDate = value;
            }
        }

        public DateTime? FixedTermContarctEndDate
        {
            get { return _fixedTermContarctEndDate; }
            set
            {
                _fixedTermContarctEndDate = value;
            }
        }

        public DateTime? ConsultancyStartDate
        {
            get { return _consultancyStartDate; }
            set
            {
                _consultancyStartDate = value;
            }
        }

        public DateTime? ConsultancyEndDate
        {
            get { return _consultancyEndDate; }
            set
            {
                _consultancyEndDate = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeDAL"/> class.
        /// </summary>
        public EmployeeDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeDAL"/> class.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        public EmployeeDAL(int EmployeeID)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            _employeeId = EmployeeID;
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

        private void SetError(SqlException Ex)
        {           
            _isError = true;
            _errorMsg = Ex.Message;            
            _errorNo = Ex.Number;
        }
        #endregion

        public DataTable GetEmploymeeIdforexcelupdate(int EmployeeCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeesIdfroexcel", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
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
        public DataTable GetEmployeeCode(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GETEmployeeCode", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    OpenDB();
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

        public DataTable CheckNicNo(long EmployeeId, string NicNo)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_CheckNicNoUpdate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NicNo", NicNo);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    OpenDB();
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

        public DataTable CheckEpfCode(string EpfCode, int CompanyId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_CheckEpfCodeUpdate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EpfCode", EpfCode);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    OpenDB();
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


        public DataTable GetEmployeeOrganizationFlow(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeOrganizationFlow", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
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
                _dbConnection.Close();

            }
            return dtTable;
        }

        public DataTable GetEmployeeForCatogory(string UserName, int CompanyID, string ApplicationCode, int catID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeForCatogory", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    command.Parameters.AddWithValue("@CatID", catID);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
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

        public DataTable GetEmployeeBrancId(int OrgStructureID, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeBrancId", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@CompanyID", CompanyId);

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

        public DataTable CheckNicNo(string NicNo)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_CheckNicNo", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NicNo", NicNo);
                    OpenDB();
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

        public DataTable CheckEmployeeCode(string EmployeeCode, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_CheckEmpCode", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    OpenDB();
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

        public DataTable CheckEmployeeCodeWhenUpdate(string EmployeeCode, int CompanyId, long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_CheckEmpCodeUpdate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    OpenDB();
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

        public DataTable CheckEpfCode(string EpfCode, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_CheckEpfCode", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EpfCode", EpfCode);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    OpenDB();
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

        public void SetLeave_LeaveEntitlment(Int64 empID, string user)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Lev_NewEmployeeStatutoryLeaveEntitlements", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", empID);
                    command.Parameters.AddWithValue("@CreateUser", user);

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
        public void SetProbationEmployeeLeaveEntitlment(Int64 empID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Leave_SetEmployeeProbationLeave", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", empID);

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
        /// Adds the employee.
        /// </summary>
        public void AddEmployee()
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddNewEmployee", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;     
                    //command.Parameters.AddWithValue("@EmployeeCode", _employeeCode);
                    if (_epfNo == "")
                        _epfNo = null;
                    command.Parameters.AddWithValue("@EPFNo", _epfNo);
                    command.Parameters.AddWithValue("@EmpCard", _empcard);
                    command.Parameters.AddWithValue("@FirstName", _firstName);
                    command.Parameters.AddWithValue("@FullName", _fullName);
                    command.Parameters.AddWithValue("@LastName", _lastName);
                    command.Parameters.AddWithValue("@NameInitials", _nameInitials);
                    command.Parameters.AddWithValue("@CallName", _callName);
                    command.Parameters.AddWithValue("@HomeContactNo", _homeContactNo);
                    command.Parameters.AddWithValue("@HomeContactNo2", _homeContactNo2);
                    command.Parameters.AddWithValue("@OfficeContactNo", _officeContactNo);
                    command.Parameters.AddWithValue("@MobileNo", _mobileNo);
                    command.Parameters.AddWithValue("@MobileNo2", _mobileNo2);
                    command.Parameters.AddWithValue("@MobileNo3", _mobileNo3);
                    command.Parameters.AddWithValue("@AddressLine1", _address);
                    command.Parameters.AddWithValue("@AddressLine2", _address2);
                    command.Parameters.AddWithValue("@City", _addressCity);
                    command.Parameters.AddWithValue("@Email", _email);
                    command.Parameters.AddWithValue("@NIC", _nic);
                    command.Parameters.AddWithValue("@Gender", _gender);
                    command.Parameters.AddWithValue("@DateOfBirth", _dateOfBirth);
                    command.Parameters.AddWithValue("@Status", _status);
                    command.Parameters.AddWithValue("@Image", _image);
                    command.Parameters.AddWithValue("@BranchId", _branchId);
                    command.Parameters.AddWithValue("@JobCategoryID", _jobCategoryId);
                    command.Parameters.AddWithValue("@OrgStructureID", _orgStructureId);
                    command.Parameters.AddWithValue("@DesignationID", _designationId);
                    command.Parameters.AddWithValue("@JobGradeID", _jobGradeId);
                    command.Parameters.AddWithValue("@EmploymentTypeID", _employmentTypeId);
                    command.Parameters.AddWithValue("@EmergencyContactPerson", _emergencyContactPerson);
                    command.Parameters.AddWithValue("@EmergencyContactNo", _emergencyContactNo);
                    command.Parameters.AddWithValue("@EmergencyContactNoHome", _emergencyContactNoHome);
                    command.Parameters.AddWithValue("@EmergencyContactAddressLine1", _emergencyContactAddressLine1);
                    command.Parameters.AddWithValue("@EmergencyContactAddressLine2", _emergencyContactAddressLine2);
                    command.Parameters.AddWithValue("@EmergencyContactCity", _emergencyContactAddressCity);
                    command.Parameters.AddWithValue("@TempAddress1", _temporaryAddressLine1);
                    command.Parameters.AddWithValue("@TempAddress2", _temporaryAddressLine2);
                    command.Parameters.AddWithValue("@TempAddressCity", _temporaryCity);
                    command.Parameters.AddWithValue("@Relationship", _emergencyRelationship);
                    command.Parameters.AddWithValue("@EmploymentGradeID", _employmentGradeId);
                    command.Parameters.AddWithValue("@PostalCode", _postalCode);
                    command.Parameters.AddWithValue("@RecruitementDate", _recrutementDate);
                    command.Parameters.AddWithValue("@OccupationGrade", _occupationGrade);
                    command.Parameters.AddWithValue("@PayrollAct", _payrollAct);
                    command.Parameters.AddWithValue("@CostCenter", _costCenter);
                    command.Parameters.AddWithValue("@DirectIndirect", _directIndirect);
                    command.Parameters.AddWithValue("@ProbationStartDate", _probationStartDate);
                    command.Parameters.AddWithValue("@ProbationEndDate", _probationEndDate);
                    command.Parameters.AddWithValue("@FixedTermContractStartDate", _fixedTermContarctStartDate);
                    command.Parameters.AddWithValue("@FixedTermContractEndDate", _fixedTermContarctEndDate);
                    command.Parameters.AddWithValue("@ConsultancyStartDate", _consultancyStartDate);
                    command.Parameters.AddWithValue("@ConsultancyEndDate", _consultancyEndDate);

                    if (_confirmationDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                        command.Parameters.AddWithValue("@ConfirmationDate", null);
                    else
                        command.Parameters.AddWithValue("@ConfirmationDate", _confirmationDate);

                    if (_epfnoDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                        command.Parameters.AddWithValue("@EpfNoDate", null);
                    else
                        command.Parameters.AddWithValue("@EpfNoDate", _epfnoDate);
               
                    command.Parameters.AddWithValue("@CompanyID", _companyID);
                    command.Parameters.AddWithValue("@BarcodeID", 0);
                    command.Parameters.AddWithValue("@Remarks", _remarks);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
                    command.Parameters.AddWithValue("@OrganizationTypeID", _organizationTypeID);
                    command.Parameters.AddWithValue("@UserPreFix", string.Empty);
                    command.Parameters.AddWithValue("@InactiveStatus", _inactiveStatus);

                    command.Parameters.AddWithValue("@PassportNo",_passportNo);
                    command.Parameters.AddWithValue("@PassPortExpiryDate", _passportExpiryDate);
                    command.Parameters.AddWithValue("@VisaNo", _visaNo);
                    command.Parameters.AddWithValue("@VisaExpiryDate", _visaExpiryDate);
                    command.Parameters.AddWithValue("@IsExpatriate", _isExpatriate);

                    SqlParameter empId = new SqlParameter("@EmpId", SqlDbType.Int, 16);
                    empId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(empId);

                    SqlParameter imployeeId = new SqlParameter("@EmployeeID", SqlDbType.BigInt, 16);
                    imployeeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(imployeeId);

                    SqlParameter empCode = new SqlParameter("@EmpCode", SqlDbType.VarChar, 16);
                    empCode.Direction = ParameterDirection.Output;
                    command.Parameters.Add(empCode);

                    command.ExecuteNonQuery();

                    _empId = Convert.ToInt64(empId.Value);
                    _employeeCode = empCode.Value.ToString();

                    if (_empId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Employee Code Already Exists!";
                    }

                    _employeeId = Convert.ToInt64(imployeeId.Value);

              
                }
                using (SqlCommand command = new SqlCommand("Pay_GetEmployeeOrganizaitonLeval", _dbConnection))
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
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        public void UpdateEmployee(long EmployeeID, bool IsUpdate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateEmployee", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@EmployeeCode", _employeeCode);
                    command.Parameters.AddWithValue("@EPFNo", _epfNo.Trim());
                    command.Parameters.AddWithValue("@FirstName", _firstName);
                    command.Parameters.AddWithValue("@FullName", _fullName);
                    command.Parameters.AddWithValue("@NameInitials", _nameInitials);
                    command.Parameters.AddWithValue("@LastName", _lastName);
                    command.Parameters.AddWithValue("@CallName", _callName);
                    command.Parameters.AddWithValue("@Gender", _gender);
                    command.Parameters.AddWithValue("@BranchId", _branchId);
                    command.Parameters.AddWithValue("@DateOfBirth", _dateOfBirth);
                    command.Parameters.AddWithValue("@Status", _status);
                    command.Parameters.AddWithValue("@Image", _image);
                    command.Parameters.AddWithValue("@JobCategoryID", _jobCategoryId);
                    command.Parameters.AddWithValue("@OrgStructureID", _orgStructureId);
                    command.Parameters.AddWithValue("@DesignationID", _designationId);
                    command.Parameters.AddWithValue("@JobGradeID", _jobGradeId);
                    command.Parameters.AddWithValue("@EmploymentTypeID", _employmentTypeId);
                    command.Parameters.AddWithValue("@EmploymentGradeID", _employmentGradeId);
                    command.Parameters.AddWithValue("@Active", _active);
                    command.Parameters.AddWithValue("@EmpCard", _empcard);
                    command.Parameters.AddWithValue("@RecrutementDate", _recrutementDate);
                    command.Parameters.AddWithValue("@OccupationGrade", _occupationGrade);
                    command.Parameters.AddWithValue("@PayrollAct", _payrollAct);
                    command.Parameters.AddWithValue("@CostCenter", _costCenter);
                    command.Parameters.AddWithValue("@DirectIndirect", _directIndirect);
                    command.Parameters.AddWithValue("@ProbationStartDate", _probationStartDate);
                    command.Parameters.AddWithValue("@ProbationEndDate", _probationEndDate);
                    command.Parameters.AddWithValue("@FixedTermContractStartDate", _fixedTermContarctStartDate);
                    command.Parameters.AddWithValue("@FixedTermContractEndDate", _fixedTermContarctEndDate);
                    command.Parameters.AddWithValue("@ConsultancyStartDate", _consultancyStartDate);
                    command.Parameters.AddWithValue("@ConsultancyEndDate", _consultancyEndDate);
                    command.Parameters.AddWithValue("@InactiveDate", _inactiveDate);
                    command.Parameters.AddWithValue("@TerminateDate", _terminatedDate);
                    //command.Parameters.AddWithValue("@ReActivatedDate", _reActivatedDate);
                    command.Parameters.AddWithValue("@CompanyID", _companyID);

                    if (_confirmationDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                        command.Parameters.AddWithValue("@ConfirmationDate", null);
                    else
                        command.Parameters.AddWithValue("@ConfirmationDate", _confirmationDate);

                    if (_retirementDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                        command.Parameters.AddWithValue("@RetirementDate", null);
                    else
                        command.Parameters.AddWithValue("@RetirementDate", _retirementDate);

                    if (_epfnoDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                        command.Parameters.AddWithValue("@EpfNoDate", null);
                    else
                        command.Parameters.AddWithValue("@EpfNoDate", _epfnoDate);

                    if (_resignDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                        command.Parameters.AddWithValue("@ResignDate", null);
                    else
                        command.Parameters.AddWithValue("@ResignDate", _resignDate);

                    if (_reActivatedDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                        command.Parameters.AddWithValue("@ReActivatedDate", null);
                    else
                        command.Parameters.AddWithValue("@ReActivatedDate", _reActivatedDate);

                    command.Parameters.AddWithValue("@InactiveStatus", _inactiveStatus);

                    command.Parameters.AddWithValue("@UpdateUser", _updateUser);

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
        public DataTable SearchEmployee(string hfEmployeeId, string ColumnName, string @EncryptionKey)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_ExcelColumnFilteration", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", hfEmployeeId);
                    command.Parameters.AddWithValue("@ColumnName", ColumnName);
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
        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        public void UpdateEmployeeContact(long EmployeeID, bool IsUpdate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateEmployeeContact", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@HomeContactNo", _homeContactNo);
                    command.Parameters.AddWithValue("@HomeContactNo2", _homeContactNo2);
                    command.Parameters.AddWithValue("@OfficeContactNo", _officeContactNo);
                    command.Parameters.AddWithValue("@MobileNo", _mobileNo);
                    command.Parameters.AddWithValue("@MobileNo2", _mobileNo2);
                    command.Parameters.AddWithValue("@MobileNo3", _mobileNo3);
                    command.Parameters.AddWithValue("@Address", _address);
                    command.Parameters.AddWithValue("@Address2", _address2);
                    command.Parameters.AddWithValue("@AddressCity", _addressCity);
                    command.Parameters.AddWithValue("@Email", _email);
                    command.Parameters.AddWithValue("@NIC", _nic);
                    command.Parameters.AddWithValue("@EmergencyContactPerson", _emergencyContactPerson);
                    command.Parameters.AddWithValue("@EmergencyContactNo", _emergencyContactNo);
                    command.Parameters.AddWithValue("@EmergencyContactNoHome", _emergencyContactNoHome);
                    command.Parameters.AddWithValue("@Relationship", _emergencyRelationship);
                    command.Parameters.AddWithValue("@PostalCode", _postalCode);
                    command.Parameters.AddWithValue("@Remark", _remarks);
                    command.Parameters.AddWithValue("@PassportNo", _passportNo);
                    command.Parameters.AddWithValue("@EmergencyContactAddressLine1", _emergencyContactAddressLine1);
                    command.Parameters.AddWithValue("@EmergencyContactAddressLine2", _emergencyContactAddressLine2);
                    command.Parameters.AddWithValue("@EmergencyContactAddressCity", _emergencyContactAddressCity);
                    command.Parameters.AddWithValue("@TemporaryAddressLine1", _temporaryAddressLine1);
                    command.Parameters.AddWithValue("@TemporaryAddressLine2", _temporaryAddressLine2);
                    command.Parameters.AddWithValue("@TemporaryCity", _temporaryCity);
                    if (_passportNo != string.Empty)
                    {
                        command.Parameters.AddWithValue("@PassPortExpiryDate", _passportExpiryDate);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PassPortExpiryDate", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("@VisaNo", _visaNo);

                    if (_isExpatriate)
                    {
                        command.Parameters.AddWithValue("@VisaExpiryDate", _visaExpiryDate);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@VisaExpiryDate", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@IsExpatriate", _isExpatriate);
                    command.Parameters.AddWithValue("@UpdateUser", _updateUser);
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

        /// <summary>
        /// Deletes Employee.
        /// </summary>
        /// <param name="JobCategoryId">The Employee id as int.</param>
        public void DeleteEmployee(long EmpID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteEmployee", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", EmpID);
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



        public void UpdatePersonalChangeInfoStat(long EmployeeID, string Status)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdatePersonalInfoStatus", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
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
        /// <summary>
        /// Gets the employe.
        /// </summary>
        /// <param name="EmployeeId">EmployeeId As long</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetEmployee(long EmployeeId, string UserName, int CompanyID, string ApplicationCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployee", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
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


        public DataTable GetEmployeeOrg(long EmployeeId, int OrgStructureID, string UserName, int CompanyID, string ApplicationCode, int StatusId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeOrg", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
                    command.Parameters.AddWithValue("@StatusId", StatusId);
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


        public DataTable GetBranchEmployee(long EmployeeId, string UserName, int CompanyID, string ApplicationCode, string Branch)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetBranchEmployee", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
                    command.Parameters.AddWithValue("@Branch", Branch);
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

        public DataTable GetEmployeeOfPersonalInfoChange(long EmployeeId, string UserName, int CompanyID, string ApplicationCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("POR_GetEmployeeOfPersonalInfoChange", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
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

        public DataTable GetEmployeeByCompany(int CompanyID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeByCompany", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyID", CompanyID);
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
        /// Gets the employe.
        /// </summary>
        /// <param name="EmployeeCode">The employee code.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="DateOfBirth">The date of birth.</param>
        /// <param name="NIC">The NIC.</param>
        /// <param name="MobileNo">The mobile no.</param>
        /// <param name="HomeNo">The home no.</param>
        /// <param name="Gender">The gender.</param>
        /// <param name="Status">The status.</param>
        /// <returns></returns>
        public DataTable GetEmployee(string EmployeeCode, string FirstName, string LastName, DateTime DateOfBirth, string NIC, string MobileNo, string HomeNo, string Gender, int Status)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_SearchEmployee", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@NIC", NIC);
                    command.Parameters.AddWithValue("@MobileNO", FirstName);
                    command.Parameters.AddWithValue("@HomeContactNo", LastName);
                    command.Parameters.AddWithValue("@Gender", DateOfBirth);
                    command.Parameters.AddWithValue("@Status", NIC);
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
        public DataTable SearchEmployee(int CompanyId, int OrgId, int DesignationId, long hfEmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_SearchEMployeeForExcelUpdate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgId", OrgId);
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
                    command.Parameters.AddWithValue("@hfEmployeeId", hfEmployeeId);
                 
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
        public DataTable GetEmployeesInExcelUpdate(int CompanyId, int OrgId, int DesignationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEMployeeForExcelUpdate", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrgId", OrgId);
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
                   
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
        /// Gets the employe.
        /// </summary>
        /// <param name="CompanyId">CompanyId As int</param>
        /// <param name="OrganizationStructureId">OrganizationStructureId As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetEmployee(int CompanyId, int OrganizationStructureId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeByOrganization", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationStructureId", OrganizationStructureId);
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
        public DataTable GetEmployeeByOrg(int CompanyId, int OrganizationStructureId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeByOrganizationFORDEMO", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@OrganizationStructureId", OrganizationStructureId);
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
        
        public DataTable GetEmployeesForSearch(string ContainCollom, string ContainText, int ContainPossition)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("GetEmployeesForSearch", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ContainCollom", ContainCollom);
                    command.Parameters.AddWithValue("@ContainText", ContainText);
                    command.Parameters.AddWithValue("@ContainPossition", ContainPossition);
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
        /// Gets the Employee ID.
        /// </summary>
        /// <returns>Returns DataTable</returns>
        public DataTable GetEmployeId(string EmployeeCode, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeIdByEmployeeCode", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
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
        /// Gets the User Field.
        /// </summary>
        /// <returns>Returns DataTable</returns>
        public DataTable GetUserField(string FieldType)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GetUserFields", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@FieldType", FieldType);
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


        public long GetEmployeesByEmployeeCode(string EmployeeCode)
        {
            long EmployeeId = 0;
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeIdByEmployeeCode", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                    EmployeeId = Convert.ToInt64(command.ExecuteScalar());

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
            return EmployeeId;
        }

        public string GetEmployeesCodeByEmployeeID(int EmployeID)
        {
            string EmployeeCode= null ;
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeCodeByEmployeeID", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeID);
                    EmployeeCode = Convert.ToString(command.ExecuteScalar());

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
            return EmployeeCode;
        }
        public string GetEmployeesCodeByEmployeeID(Int64 EmployeID)
        {
            string EmployeeCode = null;
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeCodeByEmployeeID", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeID);
                    EmployeeCode = Convert.ToString(command.ExecuteScalar());

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
            return EmployeeCode;
        }
        #region Change Employee
        /// <summary>
        /// Change Employee Company.
        /// </summary>
        public void ChangeEmployeeCompany(long EmployeeId, int PreviousCompany, int NewCompany, int OrgStructureID, int DesignationId, DateTime Date, int JobCategoryId, string EpfNo, string CreatedUser )
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_ChangeEmployeeCompany", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@PreviousCompany", PreviousCompany);
                    command.Parameters.AddWithValue("@NewCompany", NewCompany);
                    command.Parameters.AddWithValue("@OrgStructureID", OrgStructureID);
                    command.Parameters.AddWithValue("@DesignationId", DesignationId);
                    command.Parameters.AddWithValue("@JobCategoryId", JobCategoryId);
                    command.Parameters.AddWithValue("@EpfNo", EpfNo);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@Date", Date); 
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


        public DataTable GetEmployeePayCatagery(int EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeePayCatagery", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
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
                _dbConnection.Close();

            }
            return dtTable;
        }





        #endregion

        #region Employment Type

        /// <summary>
        /// Add  the EmploymentType.
        /// <param name="EmploymentTypeId">The employmentType ID.</param>
        /// <param name="EmploymentTypeCode">The employmentType Code.</param>
        /// <param name="EmploymentTypeName">The employmentType Name.</param>
        /// <param name="Remark">The remark.</param>
        ///  <summary> 
        public void AddEmploymentType(string EmploymentTypeCode, string EmploymentTypeName, string Remark)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddEmploymentType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmploymentTypeCode", EmploymentTypeCode);
                    command.Parameters.AddWithValue("@EmploymentTypeName", EmploymentTypeName);
                    command.Parameters.AddWithValue("@Remark", Remark);
                    SqlParameter emloymentTypeId = new SqlParameter("@EmploymentTypeId", SqlDbType.Int, 8);
                    emloymentTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(emloymentTypeId);
                    command.ExecuteNonQuery();
                    _employmentTypeId = Convert.ToInt32(emloymentTypeId.Value);
                    if (_employmentTypeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
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
        /// Update  the EmploymentType.
        /// <param name="EmploymentTypeId">The employmentType ID.</param>
        /// <param name="EmploymentTypeCode">The employmentType Code.</param>
        /// <param name="EmploymentTypeName">The employmentType Name.</param>
        /// <param name="Remark">The remark.</param>
        /// </summary>
        public void UpdateEmploymentType(int EmploymentTypeId, string EmploymentTypeCode, string EmploymentTypeName, string Remark)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateEmploymentType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmploymentTypeId", EmploymentTypeId);
                    command.Parameters.AddWithValue("@EmploymentTypeCode", EmploymentTypeCode);
                    command.Parameters.AddWithValue("@EmploymentTypeName", EmploymentTypeName);
                    command.Parameters.AddWithValue("@Remark", Remark);



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
        /// Deletes the type of the employment.
        /// </summary>
        /// <param name="EmploymentTypeId">The employment type id.</param>
        public void DeleteEmploymentType(int EmploymentTypeId)
        {
            try
            {

                using (SqlCommand command = new SqlCommand("COM_DeleteEmploymentType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmploymentTypeId", EmploymentTypeId);
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


        /// <summary>
        /// Gets the employment type.
        /// </summary>
        /// <param name="EmploymentTypeId">EmploymentTypeId As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetEmploymentType(int EmploymentTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmploymentType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmploymentTypeId", EmploymentTypeId);
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



        #region Employment Grade

        /// <summary>
        /// Add  the EmploymentGrade.
        /// <param name="EmploymentGradeId">The employmentGrade ID.</param>
        /// <param name="EmploymentGradeCode">The employmentGrade Code.</param>
        /// <param name="EmploymentGradeName">The employmentGrade Name.</param>
        ///  <summary> 
        public void AddEmploymentGrade(string EmploymentGradeCode, string EmploymentGradeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddEmploymentGrade", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmploymentGradeCode", EmploymentGradeCode);
                    command.Parameters.AddWithValue("@EmploymentGradeName", EmploymentGradeName);
                    SqlParameter emloymentGradeId = new SqlParameter("@EmploymentGradeId", SqlDbType.Int, 8);
                    emloymentGradeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(emloymentGradeId);
                    command.ExecuteNonQuery();
                    _employmentGradeId = Convert.ToInt32(emloymentGradeId.Value);
                    if (_employmentGradeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
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
        /// Update  the EmploymentGrade.
        /// <param name="EmploymentGradeId">The employmentGrade ID.</param>
        /// <param name="EmploymentGradeCode">The employmentGrade Code.</param>
        /// <param name="EmploymentGradeName">The employmentGrade Name.</param>
        /// </summary>
        public void UpdateEmploymentGrade(int EmploymentGradeId, string EmploymentGradeCode, string EmploymentGradeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateEmploymentGrade", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmploymentGradeId", EmploymentGradeId);
                    command.Parameters.AddWithValue("@EmploymentGradeCode", EmploymentGradeCode);
                    command.Parameters.AddWithValue("@EmploymentGradeName", EmploymentGradeName);
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
        /// Deletes the Grade of the employment.
        /// </summary>
        /// <param name="EmploymentGradeId">The employment Grade id.</param>
        public void DeleteEmploymentGrade(int EmploymentGradeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteEmploymentGrade", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("@EmploymentGradeId", EmploymentGradeId);
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


        /// <summary>
        /// Gets the employment Grade.
        /// </summary>
        /// <param name="EmploymentGradeId">EmploymentGradeId As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetEmploymentGrade(int EmploymentGradeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetEmploymentGrade", _dbConnection))
                {
                 
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmploymentGradeId", EmploymentGradeId);
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

        public DataTable GetNaionality()
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetNaionality", _dbConnection))
                {

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

        public DataTable GetReligion()
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetReligion", _dbConnection))
                {

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

        #endregion




        #endregion


        public string GetEmployeeEmailByEmployeeId(long EmployeeId)
        {
            string EmployeeEmail = string.Empty;
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeEmailByEmpId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    EmployeeEmail = command.ExecuteScalar().ToString();

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
            return EmployeeEmail;
        }

        public DataTable GetEmployeId(int ComapnyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeeIdByEpf", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", ComapnyId);
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

        public DataTable GetEmployeNotInTime()
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeesNotInTime", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
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

        public DataTable GetEmployeesInTimeModule(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetEmployeesInTimeModule", _dbConnection))
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


        #region File Upload

        public void AddFileUploadOriginal(long EmployeeId, string FolderName, string FileName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddFileUploadOriginal", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FolderName", FolderName);
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


        public void AddFileUploadCopy(long EmployeeId, string FolderName, string FileName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddFileUploadCopy", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FolderName", FolderName);
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


        public void AddFileUploadNormal(long EmployeeId, string FolderName, string FileName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddFileUploadNormal", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FolderName", FolderName);
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
        /// Deletes the Uploaded file.
        /// </summary>

        public void DeleteUploadedFile(string FileName,string FolderName )
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_DeleteUploadedFile", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FileName", FileName);
                    command.Parameters.AddWithValue("@FolderName", FolderName);
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


        public void AssignFolderToEmployee(string FolderName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Common_AssignNewFolderForEmployee", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FolderName", FolderName);
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

    }

    
}
