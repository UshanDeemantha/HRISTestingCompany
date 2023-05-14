/// <summary>
/// Solution Name        : HRM
/// Project Name         : HR.DAL
/// Author               : ushan deemantha
/// Class Description    : ChildDAL
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace HRM.HR.DAL
{
   
    public class ChildDAL
    {

   #region Fields
        private SqlConnection _dbConnection;
        private DataSet _childs;
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

        public long EmployeeChildId
        {
          get{ return _employeeChildId;}
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
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public ChildDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        #region Child
        /// <summary>
        /// Add  the Child.
        /// <param name="ChildId">The Child Id.</param>
        /// <param name="EmployeeId">The Employee Id.</param>
        /// <param name="FirstName">The First Name.</param>
        /// <param name="FullName">The Full Name.</param>
        /// <param name="LastName">The Last Name.</param>
        /// <param name="DateOfBirth">The Date Of Birth.</param>
        /// <param name="Gender">The Gender.</param>
        ///  <summary> 
        public void AddChild(long EmployeeId, string FirstName, string FullName, string LastName, DateTime DateOfBirth, string Gender)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddChild", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();


                    SqlParameter childIdInfo = new SqlParameter("@ChildId", SqlDbType.Int, 8);
                    childIdInfo.Direction = ParameterDirection.Output;
                    command.Parameters.Add(childIdInfo);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@FullName", FullName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);


                    command.ExecuteNonQuery();

                    _childId = Convert.ToInt32(childIdInfo.Value);
                    if (_childId < 0)
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
        /// Update the Child.
        /// <param name="ChildId">The Child Id.</param>
        /// <param name="EmployeeId">The Employee Id.</param>
        /// <param name="FirstName">The First Name.</param>
        /// <param name="FullName">The Full Name.</param>
        /// <param name="LastName">The Last Name.</param>
        /// <param name="DateOfBirth">The Date Of Birth.</param>
        /// <param name="Gender">The Gender.</param>
        /// </summary>
        public void UpdateChild(int ChildId, long EmployeeId, string FirstName, string FullName, string LastName, DateTime DateOfBirth, string Gender)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateChild", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@ChildId", ChildId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@FullName", FullName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);


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
        /// Gets the Child type.
        /// </summary>
        /// <param name="ChildId">Child Id As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetChild(int ChildId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetChild", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@ChildId", ChildId);
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



        public void DeleteChild(int ChildId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteChild", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@ChildId", ChildId);
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

        public void AddFileDetails(string FileCategoryCode, string FileCategoryName, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddFileType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@FileCategoryCode", FileCategoryCode);
                    command.Parameters.AddWithValue("@FileCategoryName", FileCategoryName);
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

        public void UpdateFileDetails(int FileTypeId, string FileCategoryCode, string FileCategoryName, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateFileType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@FileTypeId", FileTypeId);
                    command.Parameters.AddWithValue("@FileCategoryCode", FileCategoryCode);
                    command.Parameters.AddWithValue("@FileCategoryName", FileCategoryName);
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

        public void SaveFileDetails(int FileTypeId, string DesCription, string FileName, long hfEmployeeId, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_SaveFileDetails", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@FileTypeId", FileTypeId);
                    command.Parameters.AddWithValue("@DesCription", DesCription);
                    command.Parameters.AddWithValue("@FileName", FileName);
                    command.Parameters.AddWithValue("@EmployeeId", hfEmployeeId);
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

        public void UpdateFile(int FileTypeId, string DesCription, string FileName, long FileId, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateFile", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@FileTypeId", FileTypeId);
                    command.Parameters.AddWithValue("@DesCription", DesCription);
                    command.Parameters.AddWithValue("@FileName", FileName);
                    command.Parameters.AddWithValue("@FileId", FileId);
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

        public void DeleteFileDetails(int hfId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("DeleteFileUp", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@Id", hfId);
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

        public DataTable GetFileTypeCode(string FileTypeCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetFileTypeCode", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@FileTypeCode", FileTypeCode);
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

        public DataTable GetFileTypeCodeById(int FileTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetFileTypeCodeById", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@FileTypeId", FileTypeId);
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

        #region EmployeeChild
        /// <summary>
        /// Adds the employee Child.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int. </param>
        /// <param name="ChildId">The Child id as int.</param>
        public void AddEmployeeChild(long EmployeeId, int ChildId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeChildren", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ChildId", ChildId);
                    SqlParameter employeeChildId = new SqlParameter("@EmployeeChildId", SqlDbType.Int, 16);
                    employeeChildId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeChildId);
                    command.ExecuteNonQuery();
                    _employeeChildId = Convert.ToInt64(employeeChildId.Value);
                    if (_employeeChildId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _errorMsg = ex.Message;
            //}
            catch (DbException ex)
            {
                int i = ex.ErrorCode;
            }
            finally
            {
                _dbConnection.Close();
            }
        }



        /// <summary>
        /// Updates the employee Child.
        /// </summary>
        /// <param name="EmployeeChildId">The employee Child id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="ChildId">The Child id as int. </param>
        public void UpdateEmployeeChild(long EmployeeChildId, long EmployeeId, int ChildId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeChildren", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeChildId", EmployeeChildId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@ChildId", ChildId);
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
        /// Gets the employee Child.
        /// </summary>
        /// <param name="EmployeeChildId">The employee Child id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeChild(long EmployeeChildId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeChildren", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeChildId", EmployeeChildId);
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
        public DataTable GetEmployeeChildByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeChildrenByEmployeeId", _dbConnection))
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
        


        /// <summary>
        /// Deletes the employee Child.
        /// </summary>
        /// <param name="EmployeeChildId">The employee Child id as int.</param>
        public void DeleteEmployeeChild(long EmployeeChildId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeChildren", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeChildId", EmployeeChildId);
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



        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="StudentDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~ChildDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
