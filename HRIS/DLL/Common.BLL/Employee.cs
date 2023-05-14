
/// </summary>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using HRM.Common.DAL;
using System.ComponentModel;

namespace HRM.Common.BLL
{
   [DataObject]
   public class Employee
    {
        #region Fields

        EmployeeDAL objEmployeeDAL;
        private DataTable _employmentTypes;
        private DataTable _emppaycatagery;
        private DataTable _employeeID;
        private DataTable _UserFields;
        private DataTable _employmentGrades;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;
        private int _companyID;
        private int _employeeId;
        private long _EmpId;
        private string _employeeCode;
        private string _epfNo;
        private string _firstName;
        private string _fullName;
        private string _lastName;
        private string _nameInitials;
        private string _callName;
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
        private string _emergencyContactAddressLine1;
        private string _emergencyContactAddressLine2;
        private string _emergencyContactAddressCity;
        private string _temporaryAddressLine1;
        private string _temporaryAddressLine2;
        private string _temporaryCity;
        private string _occupationGrade;
        private string _emergencyContactPerson;
        private string _emergencyRelationship;
        private string  _postalCode;
        private int _enploymentGradeId;
        private bool _active;
        private string _empcard;
        private DateTime _recruitementDate;
        private DateTime _confirmationDate;
        private DateTime _retirementDate;
        private DateTime _epfnoDate;
        private DateTime _resignDate;
        private DateTime? _inactiveDate;
        private DateTime? _terminatedDate;
        private DateTime? _reActivateDate;

        private string _remaks;
        private string  _createdUser;
        private string _updateUser;
        private string  _userPrifix;
        private string _passportNo;
        private DateTime? _passportExpiryDate;
        private string _visaNo;
        private DateTime? _visaExpiryDate;
        private bool _isExpatriate;
        private int _organizationTypeID;
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
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        public Employee()
        {
            objEmployeeDAL = new EmployeeDAL();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        public Employee(int EmployeeID)
        {
            objEmployeeDAL = new EmployeeDAL();
            _employeeId = EmployeeID;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmploymeeIdforexcelupdate(int EmployeeCode)
        {
            try
            {
                _employeeID = objEmployeeDAL.GetEmploymeeIdforexcelupdate(EmployeeCode);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _employeeID;
        }
        public Employee(long EmpID)
        {
            objEmployeeDAL = new EmployeeDAL();
            _EmpId = EmpID;
        }

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
        public int EmployeeID
        {
            get
            {
                return _employeeId;
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

        public long GetEmployeeID
        {
            get
            {
                return _EmpId;
            }
            set
            {
                _EmpId = value;
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
        /// Gets or sets the call name.
        /// </summary>
        /// <value>The call name.</value>
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
                _dateOfBirth = value;
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
        /// Gets or sets the emergency contact person relationship.
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
       public string TemporaryCity
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
        /// Gets or sets the employment grade ID.
        /// </summary>
        /// <value>The employment grade ID.</value>
        public int EmploymentGradeID
        {
            get
            {
                return _enploymentGradeId;
            }
            set
            {
                _enploymentGradeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string  PostalCode
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
        /// Gets or sets a value indicating whether this <see cref="EmployeeDAL"/> is EmpCard.
        /// </summary>
        /// <value><c>true</c> if EmpCard; otherwise, <c>false</c>.</value>
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
        /// Gets or sets the remaks.
        /// </summary>
        /// <value>The remaks.</value>
        public string Remaks
        {
            get { return _remaks; }
            set
            {
                _remaks = value;
            }
        }
        /// <summary>
        /// Sets the created user.
        /// </summary>
        /// <value>The created user.</value>
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

        /// <summary>
        /// Sets the update user.
        /// </summary>
        /// <value>The update user.</value>
        public string UpdateUser
        {
            set
            {
                _updateUser = value;
            }
        }
        /// <summary>
        /// Sets the user prifix.
        /// </summary>
        /// <value>The user prifix.</value>
        public string UserPrifix
        {
            set
            {
                _userPrifix = value;
            }
        }
        /// <summary>
        /// Sets the organization type ID.
        /// </summary>
        /// <value>The organization type ID.</value>
        public int OrganizationTypeID
        {
            set
            {
                _organizationTypeID = value;
            }
        }
        /// <summary>
        /// Sets the recruitement date.
        /// </summary>
        /// <value>The recruitement date.</value>
        public DateTime RecruitementDate
        {
            set
            {
                _recruitementDate = value;
            }
        }
        /// <summary>
        /// Sets the permanent date
        /// </summary>
        /// <value>The permanent date.</value>
        public DateTime ConfirmationDate
        {
            set
            {
                _confirmationDate = value;
            }
        }


        public DateTime RetirementDate
        {
            set
            {
                _retirementDate = value;
            }
        }

        /// <summary>
        /// Sets the permanent date
        /// </summary>
        /// <value>The permanent date.</value>
        public DateTime EpfNoDate
        {
            set
            {
                _epfnoDate = value;
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

        public DateTime? ReActivateDate
        {
            get
            {
                return _reActivateDate;
            }
            set
            {
                _reActivateDate = value;
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

        #region Methods

        /// <summary>
        /// Sets the values.
        /// </summary>
        private void SetDALValues()
        {
             objEmployeeDAL.CompanyID = _companyID;
             objEmployeeDAL.EmployeeCode = _employeeCode;
             objEmployeeDAL.EPFNo = _epfNo; 
             objEmployeeDAL.FirstName = _firstName;
             objEmployeeDAL.FullName = _fullName;
             objEmployeeDAL.LastName = _lastName;
             objEmployeeDAL.NameInitials = _nameInitials;
             objEmployeeDAL.CallName = _callName;
             objEmployeeDAL.HomeContactNo = _homeContactNo;
             objEmployeeDAL.HomeContactNo2 = _homeContactNo2;
             objEmployeeDAL.OfficeContactNo = _officeContactNo;
             objEmployeeDAL.MobileNo = _mobileNo;
             objEmployeeDAL.MobileNo2 = _mobileNo2;
             objEmployeeDAL.MobileNo3 = _mobileNo3;
             objEmployeeDAL.Address = _address;
             objEmployeeDAL.Address2 = _address2;
             objEmployeeDAL.City = _addressCity;
            objEmployeeDAL.Email = _email;
             objEmployeeDAL.NIC = _nic;
             objEmployeeDAL.Gender = _gender;
             objEmployeeDAL.DateOfBirth = _dateOfBirth;
             objEmployeeDAL.Status = _status;
             objEmployeeDAL.Image = _image;
             objEmployeeDAL.BranchID = _branchId;
             objEmployeeDAL.JobCategoryID = _jobCategoryId;
             objEmployeeDAL.OrgStructureID = _orgStructureId;
             objEmployeeDAL.DesignationID = _designationId;
             objEmployeeDAL.JobGradeID = _jobGradeId;
             objEmployeeDAL.EmploymentTypeID = _employmentTypeId;
             objEmployeeDAL.EmergencyContactNo = _emergencyContactNo;
             objEmployeeDAL.EmergencyContactNoHome = _emergencyContactNoHome;
             objEmployeeDAL.EmergencyContactPerson = _emergencyContactPerson;
             objEmployeeDAL.EmergencyContactPersonRelationship = _emergencyRelationship;
             objEmployeeDAL.EmergencyContactAddressLine1 = _emergencyContactAddressLine1;
            objEmployeeDAL.EmergencyContactAddressLine2 = _emergencyContactAddressLine2;
            objEmployeeDAL.EmergencyContactCity = _emergencyContactAddressCity;
            objEmployeeDAL.TemporaryAddressLine1 = _temporaryAddressLine1;
            objEmployeeDAL.TemporaryAddressLine2 = _temporaryAddressLine2;
            objEmployeeDAL.TemporaryAddressCity = _temporaryCity;
            objEmployeeDAL.OccupationGrade = _occupationGrade;
             objEmployeeDAL.PostalCode = _postalCode;
             objEmployeeDAL.RecrutementDate = _recruitementDate;
             objEmployeeDAL.ConfirmationDate = _confirmationDate;
             objEmployeeDAL.EpfNoDate = _epfnoDate;
             objEmployeeDAL.ResignDate = _resignDate;
             objEmployeeDAL.EmploymentGradeID = _enploymentGradeId;
             objEmployeeDAL.Active = _active;
             objEmployeeDAL.EMPCard = _empcard;
             objEmployeeDAL.Remarks = _remaks;
             objEmployeeDAL.CreatedUser = _createdUser;
             objEmployeeDAL.UpdateUser = _updateUser;
             objEmployeeDAL.UserPreFix = _userPrifix;
             objEmployeeDAL.OrganizationTypeID = _organizationTypeID;
             objEmployeeDAL.InactiveStatus = _inactiveStatus;
            objEmployeeDAL.InactiveDate = _inactiveDate;

            objEmployeeDAL.TerminateDate = _terminatedDate;
            objEmployeeDAL.ReActivatedDate = _reActivateDate;


            objEmployeeDAL.PassPortNo = _passportNo;
             objEmployeeDAL.PasportExpiryDate = _passportExpiryDate;
             objEmployeeDAL.VisaNo = _visaNo;
             objEmployeeDAL.VisaExpiryDate = _visaExpiryDate;
             objEmployeeDAL.IsExpatriate = _isExpatriate;

            objEmployeeDAL.PayrollAct = _payrollAct;
            objEmployeeDAL.CostCenter = _costCenter;
            objEmployeeDAL.DirectIndirect = _directIndirect;
            objEmployeeDAL.ProbationStartDate = _probationStartDate;
            objEmployeeDAL.ProbationEndDate = _probationEndDate;
            objEmployeeDAL.FixedTermContarctStartDate = _fixedTermContarctStartDate;
            objEmployeeDAL.FixedTermContarctEndDate = _fixedTermContarctEndDate;
            objEmployeeDAL.ConsultancyStartDate = _consultancyStartDate;
            objEmployeeDAL.ConsultancyEndDate = _consultancyEndDate;
        }

        /// <summary>
        /// Sets the default values (DB errors).
        /// </summary>
        private void SetDefaultValues()
        {
            _isError = objEmployeeDAL.IsError;
            _errorMsg = objEmployeeDAL.ErrorMsg;
            _errorNo = objEmployeeDAL.ErrorNo;
            if (_isError == true)
            {
                switch (_errorNo)
                {
                    case 2601: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    case 2627: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    case 547: _errorMsg = ConfigurationManager.AppSettings["CannotDelete"].ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeCode(int CompanyId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeeCode(CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable CheckNicNo(long EmployeeId, string NicNo)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.CheckNicNo(EmployeeId, NicNo);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable CheckEpfCode(string EpfCode, int CompanyId, long EmployeeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.CheckEpfCode(EpfCode, CompanyId, EmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeOrganizationFlow(long EmployeeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeeOrganizationFlow(EmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable GetEmployeId(int ComapnyId)
        {
            try
            {
                _employeeID = objEmployeeDAL.GetEmployeId(ComapnyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _employeeID;
        }

        public DataTable GetEmployeNotInTime()
        {
            try
            {
                _employeeID = objEmployeeDAL.GetEmployeNotInTime();
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _employeeID;
        }

        public DataTable GetEmployeesInTimeModule(int CompanyId)
        {
            try
            {
                _employeeID = objEmployeeDAL.GetEmployeesInTimeModule(CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _employeeID;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeBrancId(int OrgStructureID, int CompanyId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeeBrancId(OrgStructureID, CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeForCatogory(string UserName, int CompanyID, string ApplicationCode, int catid)
        {
            DataTable dataTable = new DataTable();
            try
            {

                dataTable = objEmployeeDAL.GetEmployeeForCatogory(UserName, CompanyID, ApplicationCode, catid);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// 

        public void AddEmployee()
        {
            try
            {
                SetDALValues();
                objEmployeeDAL.AddEmployee();
                _EmpId = objEmployeeDAL.EmployeeID;
                _employeeCode = objEmployeeDAL.EmployeeCode;
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }       
        }

        public void SetProbationEmployeeLeaveEntitlment(Int64 empID)
        {
            try
            {

                objEmployeeDAL.SetProbationEmployeeLeaveEntitlment(empID);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void SetLeave_LeaveEntitlment(Int64 empID, string userName)
        {
            try
            {

                objEmployeeDAL.SetLeave_LeaveEntitlment(empID, userName);

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        /// <param name="IsUpdate">if set to <c>true</c> [is update].</param>
        public void UpdateEmployee(long EmployeeID, bool IsUpdate)
        {
            try
            {
                SetDALValues();
                objEmployeeDAL.UpdateEmployee(EmployeeID,IsUpdate);
                SetDefaultValues();
               
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            } 
        }

        public DataTable SearchEmployee(string hfEmployeeId, string ColumnName, string @EncryptionKey)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.SearchEmployee(hfEmployeeId, ColumnName, @EncryptionKey);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        /// <summary>
        /// Updates the employee Contact.
        /// </summary>
        /// <param name="EmployeeID">The employee ID.</param>
        /// <param name="IsUpdate">if set to <c>true</c> [is update].</param>
        public void UpdateEmployeeContact(long EmployeeID, bool IsUpdate)
        {
            try
            {
                SetDALValues();
                objEmployeeDAL.UpdateEmployeeContact(EmployeeID, IsUpdate);
                SetDefaultValues();

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Deletes The Employee.
        /// </summary>
        /// <param name="JobCategoryId">The job category id.</param>
        public void DeleteEmployee(long EmpID)
        {
            try
            {
                objEmployeeDAL = new EmployeeDAL();
                objEmployeeDAL.DeleteEmployee(EmpID);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public void UpdatePersonalChangeInfoStat(long EmployeeID, string Status)
        {
            try
            {
                SetDALValues();
                objEmployeeDAL.UpdatePersonalChangeInfoStat(EmployeeID, Status);
                SetDefaultValues();

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <returns></returns>

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployee(long EmployeeId, string UserName, int CompanyID, string ApplicationCode)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployee(EmployeeId, UserName, CompanyID, ApplicationCode);
                SetDefaultValues();
            }
            catch(Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
                
            }
            return dataTable;
        }


        public DataTable CheckNicNo(string NicNo)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.CheckNicNo(NicNo);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable CheckEmployeeCode(string EmployeeCode, int CompanyId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.CheckEmployeeCode(EmployeeCode, CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable CheckEmployeeCodeWhenUpdate(string EmployeeCode, int CompanyId, long EmployeeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.CheckEmployeeCodeWhenUpdate(EmployeeCode, CompanyId, EmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable CheckEpfCode(string EpfCode, int CompanyId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.CheckEpfCode(EpfCode, CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }



        /// <summary>
        /// Gets the employee Org.
        /// </summary>
        /// <returns></returns>


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeOrg(long EmployeeId, int OrgStructureID, string UserName, int CompanyID, string ApplicationCode, int StatusId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeeOrg(EmployeeId, OrgStructureID, UserName, CompanyID, ApplicationCode, StatusId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }



        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetBranchEmployee(long EmployeeId, string UserName, int CompanyID, string ApplicationCode, string Branch)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetBranchEmployee(EmployeeId, UserName, CompanyID, ApplicationCode, Branch);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

       [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeOfPersonalInfoChange(long EmployeeId, string UserName, int CompanyID, string ApplicationCode)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeeOfPersonalInfoChange(EmployeeId, UserName, CompanyID, ApplicationCode);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeesForSearch(string ContainCollom, string ContainText, int ContainPossition)
        {

            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objEmployeeDAL.GetEmployeesForSearch(ContainCollom, ContainText, ContainPossition);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetDefaultValues();
            }
            return dtTable;
        }

        /// <summary>
        /// Gets the employee.
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
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployee(EmployeeCode,FirstName,LastName,DateOfBirth, NIC, MobileNo,HomeNo,Gender, Status);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable SearchEmployee(int CompanyId, int OrgId,int DesignationId,long hfEmployeeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.SearchEmployee(CompanyId, OrgId, DesignationId, hfEmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable GetEmployeesInExcelUpdate(int CompanyId, int OrgId, int DesignationId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeesInExcelUpdate(CompanyId, OrgId, DesignationId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        /// <summary>
        /// Gets the ID of the Employee.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeId(string EmployeeCode , int CompanyId)
        {
            try
            {
                _employeeID = objEmployeeDAL.GetEmployeId(EmployeeCode, CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _employeeID;
        }

        /// <summary>
        /// Gets the User Field.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUserField(string FieldType)
        {
            try
            {
                _UserFields = objEmployeeDAL.GetUserField(FieldType);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _UserFields;
        }



        public long GetEmployeeIdByEmployeeCode(string EmployeeCode)
        {
            long EmloyeeId = 0;
            try
            {
                EmloyeeId = objEmployeeDAL.GetEmployeesByEmployeeCode(EmployeeCode);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return EmloyeeId;
        }

        public string GetEmployeesCodeByEmployeeID(int EmployeeId)
        {
            string EmloyeeCode = null;
            try
            {
                EmloyeeCode = objEmployeeDAL.GetEmployeesCodeByEmployeeID(EmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return EmloyeeCode;
        }
        public string GetEmployeesCodeByEmployeeID(Int64 EmployeeId)
        {
            string EmloyeeCode = null;
            try
            {
                EmloyeeCode = objEmployeeDAL.GetEmployeesCodeByEmployeeID(EmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return EmloyeeCode;
        }

        public string GetEmployeeEmailByEmployeeId(long EmployeeId)
        {
            string email = string.Empty;
            try
            {
                email = objEmployeeDAL.GetEmployeeEmailByEmployeeId(EmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return email;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeByOrg(int CompanyId, int OrganizationStructureId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeeByOrg(CompanyId, OrganizationStructureId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployee(int CompanyId, int OrganizationStructureId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployee(CompanyId, OrganizationStructureId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }

        public DataTable GetEmployeeByCompany(int CompanyId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = objEmployeeDAL.GetEmployeeByCompany(CompanyId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
            return dataTable;
        }
        #region Employee Change
        /// <summary>
        /// Change Employee Company.
        /// </summary>
        public void ChangeEmployeeCompany(long EmployeeId, int PreviousCompany, int NewCompany, int OrgStructureID, int DesignationId, DateTime Date, int JobCategoryId, string EpfNo, string CreatedUser)
        {
            try
            {
                objEmployeeDAL.ChangeEmployeeCompany(EmployeeId, PreviousCompany, NewCompany, OrgStructureID, DesignationId, Date, JobCategoryId,  EpfNo, CreatedUser);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeePayCatagery(int EmployeeId)
        {
            try
            {
                _emppaycatagery = objEmployeeDAL.GetEmployeePayCatagery(EmployeeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _emppaycatagery;
        }


        #endregion
        #region Employment Type
        /// <summary>
        /// Adds the type of the employment.
        /// </summary>
        /// <param name="EmploymentTypeCode">The employment type code.</param>
        /// <param name="EmploymentTypeName">Name of the employment type.</param>
        /// <param name="Remark">The remark.</param>
        public void AddEmploymentType(string EmploymentTypeCode, string EmploymentTypeName, string Remark)
        {
            try
            {
                objEmployeeDAL.AddEmploymentType(EmploymentTypeCode, EmploymentTypeName, Remark);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the type of the employment.
        /// </summary>
        /// <param name="EmploymentTypeId">The employment type id.</param>
        /// <param name="EmploymentTypeCode">The employment type code.</param>
        /// <param name="EmploymentTypeName">Name of the employment type.</param>
        /// <param name="Remark">The remark.</param>
        public void UpdateEmploymentType(int EmploymentTypeId, string EmploymentTypeCode, string EmploymentTypeName, string Remark)
        {
            try
            {
                objEmployeeDAL.UpdateEmploymentType(EmploymentTypeId, EmploymentTypeCode, EmploymentTypeName, Remark);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                objEmployeeDAL.DeleteEmploymentType(EmploymentTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        /// <summary>
        /// Gets the type of the employment.
        /// </summary>
        /// <param name="EmploymentTypeId">The employment type id.</param>
        /// <returns></returns>

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmploymentType(int EmploymentTypeId)
        {
            try
            {
                _employmentTypes = objEmployeeDAL.GetEmploymentType(EmploymentTypeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _employmentTypes;
        }
        #endregion


        #region Employment Grade
        /// <summary>
        /// Adds the Grade of the employment.
        /// </summary>
        /// <param name="EmploymentGradeCode">The employment Grade code.</param>
        /// <param name="EmploymentGradeName">Name of the employment Grade.</param>
        public void AddEmploymentGrade(string EmploymentGradeCode, string EmploymentGradeName)
        {
            try
            {
                objEmployeeDAL.AddEmploymentGrade(EmploymentGradeCode, EmploymentGradeName);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the Grade of the employment.
        /// </summary>
        /// <param name="EmploymentGradeId">The employment Grade id.</param>
        /// <param name="EmploymentGradeCode">The employment Grade code.</param>
        /// <param name="EmploymentGradeName">Name of the employment Grade.</param>
        public void UpdateEmploymentGrade(int EmploymentGradeId, string EmploymentGradeCode, string EmploymentGradeName)
        {
            try
            {
                objEmployeeDAL.UpdateEmploymentGrade(EmploymentGradeId, EmploymentGradeCode, EmploymentGradeName);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                objEmployeeDAL.DeleteEmploymentGrade(EmploymentGradeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        /// <summary>
        /// Gets the Grade of the employment.
        /// </summary>
        /// <param name="EmploymentGradeId">The employment Grade id.</param>
        /// <returns></returns>

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmploymentGrade(int EmploymentGradeId)
        {
            try
            {
                _employmentGrades = objEmployeeDAL.GetEmploymentGrade(EmploymentGradeId);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _employmentGrades;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetNaionality()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objEmployeeDAL.GetNaionality();
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dt;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetReligion()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objEmployeeDAL.GetReligion();
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dt;
        }

        #endregion
        #region File Upload


        /// <summary>
        /// Adds the file upload.
        /// </summary>
        /// <param name="EmploymentTypeCode">The employeeId type Int.</param>
        /// <param name="EmploymentTypeName">Name of the foldername .</param>
        /// <param name="Remark">The remark.</param>
        public void AddFileUploadOriginal(long EmployeeId, string FolderName, string FileName)
        {
            try
            {
                objEmployeeDAL.AddFileUploadOriginal(EmployeeId, FolderName, FileName);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

      
        /// <summary>
        /// Adds the file upload.
        /// </summary>
        /// <param name="EmploymentTypeCode">The employeeId type Int.</param>
        /// <param name="EmploymentTypeName">Name of the foldername .</param>
        /// <param name="Remark">The remark.</param>
        public void AddFileUploadCopy(long EmployeeId, string FolderName, string FileName)
        {
            try
            {
                objEmployeeDAL.AddFileUploadCopy(EmployeeId, FolderName, FileName);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AssignFolderToEmployee(string FolderName)
        {
            try
            {
                objEmployeeDAL.AssignFolderToEmployee(FolderName);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteUploadedFile(string FileName,string FolderName )
        {
            try
            {
                objEmployeeDAL.DeleteUploadedFile(FileName, FolderName);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Adds the file upload.
        /// </summary>
        /// <param name="EmploymentTypeCode">The employeeId type Int.</param>
        /// <param name="EmploymentTypeName">Name of the foldername .</param>
        /// <param name="Remark">The remark.</param>
        public void AddFileUploadNormal(long EmployeeId, string FolderName, string FileName)
        {
            try
            {
                objEmployeeDAL.AddFileUploadNormal(EmployeeId, FolderName, FileName);
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        #endregion

        #endregion

        #region Distructor
        ~Employee()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
