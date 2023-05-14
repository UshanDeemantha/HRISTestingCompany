using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.HR.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.HR.BLL
{   /// <summary>
    /// Solution Name        : HRM
    /// Project Name         : HR.BLL
    /// Author               : ushan deemantha
    /// Class Description    : Spouse
    /// </summary>
    /// 

    [DataObject]
    public class Spouse
    {




         #region Fields   
        SpouseDAL objSpouseDAL;
        DataTable  _careerPaths;
        private bool _isError;
        private string _errorMsg;
        private long _spouseId;
        private long _employeeId;
        private string _firstName;
        private string _fullName;
        private string _lastName;
        private string _compny;
        private string _contactNo;
        private string _spouseEmail;
        private string _gender;
        private DateTime _dateOfBirth;
        private string _designation;
        private bool _isActive;
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



        public long SpouseId
        {
            get { return _spouseId; }
            set { _spouseId = value; }
        }

        public long EmploymeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }

        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }

        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }

        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Company
        {
            get { return _compny; }
            set { _compny = value; }
        }

        public string ContactNo
        {
            get { return _contactNo; }
            set { _contactNo = value; }
        }

        public string SpouseEmail
        {
            get { return _spouseEmail; }
            set { _spouseEmail = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }

        }

        public string Designation
        {
            get { return _designation; }
            set { _designation = value; }

        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }








        #endregion

        #region Constructor       
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Spouse()
        {
           objSpouseDAL=new SpouseDAL();
        } 
        #endregion

        #region Methods
      
        private void SetValues()
        {
            _isError=objSpouseDAL.IsError;
            _errorMsg=objSpouseDAL.ErrorMsg;
        }

        public void AddSpouse(long EmployeeId, DataTable tbl)
        {
            try
            {
                foreach (DataRow row in tbl.Rows)
                {

                    objSpouseDAL.AddSpouse(EmployeeId, row["FirstName"].ToString(), row["FullName"].ToString(), row["LastName"].ToString(), row["Company"].ToString(), row["ContactNo"].ToString(), row["Email"].ToString(),
                        row["Gender"].ToString(), Convert.ToDateTime(row["DateOfBirth"].ToString()), row["Designation"].ToString(), true);

                    SetValues();
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddSpouse2(long EmployeeId, string FirstName, string FullName, string LastName, string Company, string ConatactNo, string SpouseEmail, string Gender, DateTime DateOfBirth, string Designation, bool IsActive)
        {
            try
            {
                objSpouseDAL.AddSpouse(EmployeeId, FirstName, FullName, LastName, Company, ConatactNo, SpouseEmail, Gender, DateOfBirth, Designation, IsActive);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void UpdateSpouse(long SpouceId, long EmployeeId, string FirstName, string FullName, string LastName, string Company, string ConatactNo, string SpouseEmail, string Gender, DateTime DateOfBirth, string Designation, bool IsActive)
        {
            try
            {
                objSpouseDAL.UpdateSpouse(SpouceId,EmployeeId,FirstName,FullName, LastName,Company,ConatactNo,SpouseEmail,Gender, DateOfBirth,Designation, IsActive );
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public DataTable GetSpouse(int SpouseId)
        {
            try
            {
                _careerPaths = objSpouseDAL.GetSpouse(SpouseId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _careerPaths;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetSpouseByEmployeeId(long EmployeeId)
        {
            try
            {
                _careerPaths = objSpouseDAL.GetSpouseByEmployeeId(EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _careerPaths;
        }

        public void DeleteSpouse(int SpouseId)
        {
            try
            {
                 objSpouseDAL.DeleteSpouse(SpouseId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            
        }



        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~Spouse()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
