using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HRM.HR.DAL
{
    /// <summary>
    /// Solution Name        : HRM
    /// Project Name         : HR.DAL
    /// Author               : ushan deemantha
    /// Class Description    : Spouse DAL
    /// </summary>
    public class SpouseDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private DataSet _students;
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
        ///
        public bool IsError
        {
            get { return _isError; }

        }
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
            get { return _fullName;}
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
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public SpouseDAL()
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


        /// <summary>
        /// Add  the Spouse.
        /// <param name="SpouseId">The Spouse Id.</param>
        /// <param name="EmployeeId">The Employee Id.</param>
        /// <param name="FirstName">The First Name.</param>
        /// <param name="FullName">The Full Name.</param>
        /// <param name="LastName">The Last Name.</param>
        /// <param name="Company">The Company.</param>
        /// <param name="ContactNo">The ContactNo.</param>
        /// <param name="SpouseEmail">The SpouseEmail.</param>
        /// <param name="Gender">The Gender.</param>
        /// <param name="DateOfBirth">The Date Of Birth.</param>
        /// <param name="IsActive">The IsActive.</param>
        ///  <summary> 
        public void AddSpouse( long EmployeeId, string FirstName, string FullName, string LastName,string Company,string ConatactNo,string SpouseEmail,string Gender, DateTime DateOfBirth,string Designation, bool IsActive )
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddSpouse", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();


                    SqlParameter spouceId = new SqlParameter("@SpouseId", SqlDbType.BigInt, 16);
                    spouceId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(spouceId);
                    
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@FullName", FullName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Company", Company);
                    command.Parameters.AddWithValue("@ContactNo", ConatactNo);
                    command.Parameters.AddWithValue("@SpouseEmail", SpouseEmail);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Designation", Designation);
                    command.Parameters.AddWithValue("@Active", IsActive);
                    command.ExecuteNonQuery();

                    _spouseId = Convert.ToInt64(spouceId.Value);
                    if (_spouseId < 0)
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
        /// Update  the Spouse.
        /// <param name="SpouseId">The Spouse Id.</param>
        /// <param name="EmployeeId">The Employee Id.</param>
        /// <param name="FirstName">The First Name.</param>
        /// <param name="FullName">The Full Name.</param>
        /// <param name="LastName">The Last Name.</param>
        /// <param name="Company">The Company.</param>
        /// <param name="ContactNo">The ContactNo.</param>
        /// <param name="SpouseEmail">The SpouseEmail.</param>
        /// <param name="Gender">The Gender.</param>
        /// <param name="DateOfBirth">The Date Of Birth.</param>
        /// <param name="IsActive">The IsActive.</param>
        ///  <summary> 
        public void UpdateSpouse(long SpouceId, long EmployeeId, string FirstName, string FullName, string LastName, string Company, string ConatactNo, string SpouseEmail, string Gender, DateTime DateOfBirth, string Designation, bool IsActive)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateSpouse", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();


                
                    command.Parameters.AddWithValue("@SpouseId", SpouceId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@FullName", FullName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Company", Company);
                    command.Parameters.AddWithValue("@ContactNo", ConatactNo);
                    command.Parameters.AddWithValue("@SpouseEmail", SpouseEmail);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Designation", Designation);
                    command.Parameters.AddWithValue("@Active", IsActive);
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
        /// Gets the Spouse.
        /// </summary>
        /// <param name="SpouseId">Spouse Id  As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetSpouse(int SpouseId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetSpouse", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SpouseId",SpouseId);
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

        public DataTable GetSpouseByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetSpouseByEmployeeId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeId);
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



        public void DeleteSpouse(int SpouseId)
        {
            
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteSpouse", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SpouseId", SpouseId);
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
        


        #endregion


        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="StudentDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~SpouseDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
