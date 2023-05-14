/// <summary>
/// /// <summary>
/// /// Solution Name : HR Mannagement System/
/// /// Author : ushan deemantha
/// /// Class Description : EmployeeAdditional.DAL
/// /// </summary>
/// </summary>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.HR.DAL
{

    public class EmployeeAdditionalDAL
    {

        #region Fields

        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;

        private int _employeeId;
        private string _bloodGroup;
        private string _maritalStatus;
        private int _raceId;
        private int _nationalityId;
        private string _pollingDivision;
        private string _nearestPolliceStation;
        private string _updatedUser;
        private double _collerSize;
        private string _province;
        private string _district;
        private string _electoralDistrict;
        private string _divisionalSecretariats;
        private string _gsDivision;
        private string _transportRoute;
        private string _distanceforPermanentAddress;
        private string _createdUser;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
        /// </summary>
        public EmployeeAdditionalDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        public EmployeeAdditionalDAL(int EmployeeId)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            _employeeId = EmployeeId;
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

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        /// <value>The employee ID.</value>
        public int EmployeeID
        {
            get
            {
                return _employeeId;
            }
            set
            {
                _employeeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the blood group.
        /// </summary>
        /// <value>The blood group.</value>
        public string BloodGroup
        {
            get
            {
                return _bloodGroup;
            }
            set
            {
                _bloodGroup = value;
            }
        }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>The marital status.</value>
        public string MaritalStatus
        {
            get
            {
                return _maritalStatus;
            }
            set
            {
                _maritalStatus = value;
            }
        }

        /// <summary>
        /// Gets or sets the race ID.
        /// </summary>
        /// <value>The race ID.</value>
        public int RaceID
        {
            get
            {
                return _raceId;
            }
            set
            {
                _raceId = value;
            }
        }

        /// <summary>
        /// Gets or sets the nationality ID.
        /// </summary>
        /// <value>The nationality ID.</value>
        public int NationalityID
        {
            get
            {
                return _nationalityId;
            }
            set
            {
                _nationalityId = value;
            }
        }

        /// <summary>
        /// Gets or sets the polling division.
        /// </summary>
        /// <value>The polling division.</value>
        public string PollingDivision
        {
            get
            {
                return _pollingDivision;
            }
            set
            {
                _pollingDivision = value;
            }
        }

        /// <summary>
        /// Gets or sets the nearest pollice station.
        /// </summary>
        /// <value>The nearest pollice station.</value>
        public string NearestPolliceStation
        {
            get
            {
                return _nearestPolliceStation;
            }
            set
            {
                _nearestPolliceStation = value;
            }
        }

        public string Province
        {
            get
            {
                return _province;
            }
            set
            {
                _province = value;
            }
        }

        public string District
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value;
            }
        }

        public string ElectoralDistrict
        {
            get
            {
                return _electoralDistrict;
            }
            set
            {
                _electoralDistrict = value;
            }
        }

        public string DivisionalSecretariats
        {
            get
            {
                return _divisionalSecretariats;
            }
            set
            {
                _divisionalSecretariats = value;
            }
        }

        public string GSDivision
        {
            get
            {
                return _gsDivision;
            }
            set
            {
                _gsDivision = value;
            }
        }

        public string TransportRoute
        {
            get
            {
                return _transportRoute;
            }
            set
            {
                _transportRoute = value;
            }
        }

        public string DistanceforPermanentAddress
        {
            get
            {
                return _distanceforPermanentAddress;
            }
            set
            {
                _distanceforPermanentAddress = value;
            }
        }

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

        public string UpdatedUser
        {
            get
            {
                return _updatedUser;
            }
            set
            {
                _updatedUser = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the coller.
        /// </summary>
        /// <value>The size of the coller.</value>
        public double CollerSize
        {
            get
            {
                return _collerSize;
            }
            set
            {
                _collerSize = value;
            }

        }
        #endregion

        #region Methods

        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        /// <summary>
        /// Adds the additional information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID. AS Long</param>
        public void AddAdditionalInformation(long EmployeeID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddEmployeeAdditionalDetails", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@NationalityId", _nationalityId);
                    command.Parameters.AddWithValue("@ReligionId", _raceId);
                    command.Parameters.AddWithValue("@BloodGroup", _bloodGroup);
                    command.Parameters.AddWithValue("@Province", _province);
                    command.Parameters.AddWithValue("@District", _district);
                    command.Parameters.AddWithValue("@ElectoralDistrict", _electoralDistrict);
                    command.Parameters.AddWithValue("@DivisionalSecretariats", _divisionalSecretariats);
                    command.Parameters.AddWithValue("@GSDivision", _gsDivision);
                    command.Parameters.AddWithValue("@TransportRoute", _transportRoute);
                    command.Parameters.AddWithValue("@DistanceforPermanentAddress", _distanceforPermanentAddress);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
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
        /// Updates the additional information.
        /// </summary>
        /// <param name="EmoloyeeID">The emoloyee ID. As Long</param>
        public void UpdateAdditionalInformation(long EmployeeID)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateEmployeeAdditionalDetails", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    command.Parameters.AddWithValue("@NationalityId", _nationalityId);
                    command.Parameters.AddWithValue("@ReligionId", _raceId);
                    command.Parameters.AddWithValue("@BloodGroup", _bloodGroup);
                    command.Parameters.AddWithValue("@Province", _province);
                    command.Parameters.AddWithValue("@District", _district);
                    command.Parameters.AddWithValue("@ElectoralDistrict", _electoralDistrict);
                    command.Parameters.AddWithValue("@DivisionalSecretariats", _divisionalSecretariats);
                    command.Parameters.AddWithValue("@GSDivision", _gsDivision);
                    command.Parameters.AddWithValue("@TransportRoute", _transportRoute);
                    command.Parameters.AddWithValue("@DistanceforPermanentAddress", _distanceforPermanentAddress);
                    command.Parameters.AddWithValue("@CreatedUser", _createdUser);
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

        
        public void AddEmpGsDevision(int CompanyId, string Code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_AddGsDivision", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@Name", Name);
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
        public void UpdateEmpGsDevision(int gsId, string Code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_UpdateGsDivision", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@gsId", gsId);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@Name", Name);
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
        public void DeleteEmpGsDevision(int gsId,string Code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_DeleteGsDivision", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@gsId", gsId);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@Name", Name);
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

        public void AddEmpElectoralDistrict(int CompanyId,string Code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_AddElectoralDistrict", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@Name", Name);
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

        public void UpdateEmpElectoralDistrict(int ELId,string Code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_UpdateElectoralDistrict", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@ELId", ELId);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@Name", Name);
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

        public void DeleteEmpElectoralDistrict(int ELId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_DeleteElectoralDistrict", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@ELId", ELId);
   
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
        public void AddEmpDivisional(int CompanyId,string Code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_AddDivisionalsec", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@Name", Name);
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

        public void UpdateEmpDivisional(int DvId,string Code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_UpdateDivisinaldata", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@DvId", DvId);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@Name", Name);
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
        public void DeleteEmpDivisional(int DvId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_DeleteDivisinaldata", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@DvId", DvId);

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

        public void AddEmpTransportRout(int CompanyId,string number, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_AddTransportrout", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@number", number);
                    command.Parameters.AddWithValue("@Name", Name);
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

        public void UpdateEmpTransportRout(int TrId,string number, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_UpdateTransportrout", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@TrId", TrId);
                    command.Parameters.AddWithValue("@number", number);
                    command.Parameters.AddWithValue("@Name", Name);
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
        public void DeleteEmpTransport(int TrId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_DeleteTransportrout", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@TrId", TrId);
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
        public void AddEmpVacciene(int CompanyId,string code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_AddEmpVacciene", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@Name", Name);
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
        public void UpdateEmpVacciene(int vcId, string code, string Name)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_UpdateEmpVacciene", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
             
                    command.Parameters.AddWithValue("@vcId", vcId);
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@Name", Name);
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
        public void DeleteEmpVacciene(int VcId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Emp_DeleteEmpVacciene", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@VcId", VcId);
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
        /// Gets the additonal information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID . As Long</param>
        /// <returns></returns>
        public DataTable GetAdditonalInformation(long EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeAdditionalInfomation", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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
        /// Gets the full employee information.
        /// </summary>
        /// <param name="EmployeeID">The employee ID. As Long</param>
        /// <returns></returns>
        public DataTable GetFullEmployeeInformation(long EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetFullEmployeeInfomation", _dbConnection))
                 {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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

        public DataTable GetPortalEmployeeInfo(long EmployeeID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("POR_GetEmployee", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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

        public DataTable GetNationality()
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetNationality", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@NationalityID", 0);
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

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
        /// </summary>
        ~EmployeeAdditionalDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
