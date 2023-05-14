/// <summary>
/// Solution Name        : HRM
/// Project Name         : HR.BLL
/// Author               : ushan deemantha
/// Class Description    : Child
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.HR.DAL;
using System.ComponentModel;

namespace HRM.HR.BLL
{
       [DataObject]
    public class Child
    {
        #region Fields
        ChildDAL objChildDAL;
        private DataTable _childs;
        private bool _isError;
        private string _errorMsg;
        private int _childId;
        private long _employeeId;
        private string _firstName;
        private string _fullName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _gender;
        private long _employeeChildId;
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


        public long EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }
        public int ChildId
        {
            get { return _childId; }
            set { _childId = value; }
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
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }



        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        #endregion

        #region Constructor       
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Child()
        {
           objChildDAL=new ChildDAL();
        } 
        #endregion

        #region Methods

        private void SetValues()
        {
            _isError = objChildDAL.IsError;
            _errorMsg = objChildDAL.ErrorMsg;
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="FullName">The full name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="DateOfBirth">The date of birth.</param>
        /// <param name="Gender">The gender.</param>
        public void AddChild(long EmployeeId, DataTable tbl)
        {
            try
            {
                foreach (DataRow row in tbl.Rows)
                {

                    objChildDAL.AddChild(EmployeeId, row["FirstName"].ToString(), row["FullName"].ToString(), row["LastName"].ToString(), Convert.ToDateTime(row["DateOfBirth"].ToString()), row["Gender"].ToString());
                    SetValues();

                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void AddChild2(long EmployeeId, string FirstName, string FullName, string LastName, DateTime DateOfBirth, string Gender)
        {
            try
            {
                objChildDAL.AddChild(EmployeeId, FirstName, FullName, LastName, DateOfBirth, Gender);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the child.
        /// </summary>
        /// <param name="ChildId">The child id.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="FullName">The full name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="DateOfBirth">The date of birth.</param>
        /// <param name="Gender">The gender.</param>
        public void UpdateChild(int ChildId, long EmployeeId, string FirstName, string FullName, string LastName, DateTime DateOfBirth, string Gender)
        {
            try
            {
                objChildDAL.UpdateChild(ChildId, EmployeeId, FirstName,FullName,LastName,DateOfBirth, Gender);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Gets the child.
        /// </summary>
        /// <param name="ChildId">The child id.</param>
        /// <returns></returns>
        public DataTable GetChild(int ChildId)
        {
            try
            {
                _childs = objChildDAL.GetChild(ChildId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _childs;
        }

           [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetChild(long  EmployeeId)
        {
            try
            {
                _childs = objChildDAL.GetEmployeeChildByEmployeeId(EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _childs;
        }
        /// <summary>
        /// Deletes the child.
        /// </summary>
        /// <param name="ChildId">The child id.</param>
        public void DeleteChild(int ChildId)
        {
            try
            {
               objChildDAL.DeleteChild(ChildId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

           
        }

        public void AddFileDetails(string FileCategoryCode, string FileCategoryName, string CreatedUser)
        {
            try
            {
                objChildDAL.AddFileDetails(FileCategoryCode, FileCategoryName, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateFileDetails(int FileTypeId, string FileCategoryCode, string FileCategoryName, string CreatedUser)
        {
            try
            {
                objChildDAL.UpdateFileDetails(FileTypeId, FileCategoryCode, FileCategoryName, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void SaveFileDetails(int FileTypeId, string DesCription, string FileName, long hfEmployeeId, string CreatedUser)
        {
            try
            {
                objChildDAL.SaveFileDetails(FileTypeId, DesCription, FileName, hfEmployeeId, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateFile(int FileTypeId, string DesCription, string FileName, long FileId, string CreatedUser)
        {
            try
            {
                objChildDAL.UpdateFile(FileTypeId, DesCription, FileName, FileId, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteFileDetails(int hfId)
        {
            try
            {
                objChildDAL.DeleteFileDetails(hfId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetFileTypeCode(string FileTypeCode)
        {
            try
            {
                _childs = objChildDAL.GetFileTypeCode(FileTypeCode);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _childs;
        }

        public DataTable GetFileTypeCodeById(int FileTypeId)
        {
            try
            {
                _childs = objChildDAL.GetFileTypeCodeById(FileTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _childs;
        }

        #region EmployeeChild
        /// <summary>
        /// Adds the employee Child.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="ChildId">The Child id as int.</param>
        public void AddEmployeeChild(long EmployeeId, int ChildId)
        {
            try
            {
                objChildDAL.AddEmployeeChild(EmployeeId, ChildId);
                _employeeChildId = objChildDAL.EmployeeChildId;
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }


        }

     

        /// <summary>
        /// Updates the employee Child.
        /// </summary>
        /// <param name="EmployeeChildId">The employee Child id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="ChildId">The Child id as int.</param>
        public void UpdateEmployeeChild(long EmployeeChildId, long EmployeeId, int ChildId)
        {
            try
            {
                objChildDAL.UpdateEmployeeChild(EmployeeChildId, EmployeeId, ChildId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        /// <summary>
        /// Gets the employee Child.
        /// </summary>
        /// <param name="EmployeeChildId">The employee Child id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeChild(long EmployeeChildId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objChildDAL.GetEmployeeChild(EmployeeChildId);
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
        public DataTable GetEmployeeChildByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objChildDAL.GetEmployeeChildByEmployeeId(EmployeeId);
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
        /// Deletes the employee Child.
        /// </summary>
        /// <param name="EmployeeChildId">The employee Child id as int.</param>
        public void DeleteEmployeeChild(long EmployeeChildId)
        {
            try
            {
                objChildDAL.DeleteEmployeeChild(EmployeeChildId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        #endregion


        #endregion
        
        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~Child()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
