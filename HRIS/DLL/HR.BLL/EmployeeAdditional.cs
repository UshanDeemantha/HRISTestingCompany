/// <summary>
///  /// <summary>
///  /// Solution Name : HRM
///  /// Project Name : HRM.HR.BLL
///  /// Author : ushan deemantha
///  /// Class Description : EmployeeAdditional
/// /// </summary>
/// </summary>

using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.HR.DAL;
using HRM.Common.BLL;
namespace HRM.HR.BLL
{

   public class EmployeeAdditional : Employee
   {
       #region Fields
       EmployeeAdditionalDAL objEmployeeAdditional;

       private bool _isError;
       private string _errorMsg;

       private long _employeeId;
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
       public long EmployeeID
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
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
        /// </summary>
        public EmployeeAdditional()
       {
           objEmployeeAdditional = new EmployeeAdditionalDAL();
       }

       /// <summary>
       /// Initializes a new instance of the <see cref="EmployeeAdditional"/> class.
       /// </summary>
       /// <param name="EmployeeId">The employee id.</param>
       public EmployeeAdditional(long EmployeeId)
       {
           objEmployeeAdditional = new EmployeeAdditionalDAL();
           _employeeId = EmployeeId;
       } 
       #endregion

       #region Metords
       /// <summary>
       /// Initialises this instance (Set the DAL Valies).
       /// </summary>
       private void Initialise()
       {
           try
           {
               // objEmployeeAdditional.EmployeeID = _employeeId;
               objEmployeeAdditional.BloodGroup = _bloodGroup;
               objEmployeeAdditional.MaritalStatus = _maritalStatus;
               objEmployeeAdditional.RaceID = _raceId;
               objEmployeeAdditional.NationalityID = _nationalityId;
               objEmployeeAdditional.PollingDivision = _pollingDivision;
               objEmployeeAdditional.NearestPolliceStation = _nearestPolliceStation;
               objEmployeeAdditional.CollerSize = _collerSize;
               objEmployeeAdditional.UpdatedUser = _updatedUser;
                objEmployeeAdditional.Province = _province;
                objEmployeeAdditional.District = _district;
                objEmployeeAdditional.ElectoralDistrict = _electoralDistrict;
                objEmployeeAdditional.DivisionalSecretariats = _divisionalSecretariats;
                objEmployeeAdditional.GSDivision = _gsDivision;
                objEmployeeAdditional.TransportRoute = _transportRoute;
                objEmployeeAdditional.DistanceforPermanentAddress = _distanceforPermanentAddress;
                objEmployeeAdditional.CreatedUser = _createdUser;
            }
           catch
           {

           }
       }

       /// <summary>
       /// Sets the values.
       /// </summary>
       private void SetValues()
       {
           _isError = objEmployeeAdditional.IsError;
           _errorMsg = objEmployeeAdditional.ErrorMsg;
       }

       public bool ValidateFields()
       {
          // if()
           return true;
       }


       /// <summary>
       /// Adds the additional information.
       /// </summary>
       /// <param name="EmployeeId">The employee id.</param>
       public void AddAdditionalInformation(long EmployeeId)
       {
           try
           {
               Initialise();
               objEmployeeAdditional.AddAdditionalInformation(EmployeeId);
               SetValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       /// <summary>
       /// Updates the additional information.
       /// </summary>
       /// <param name="EmployeeId">The employee id.</param>
       public void UpdateAdditionalInformation(long EmployeeId)
       {
           try
           {
               Initialise();
               objEmployeeAdditional.UpdateAdditionalInformation(EmployeeId);
               SetValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

        /// <summary>
        /// Gets the additonal information.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <returns></returns>
        /// 
        
        public void AddEmpGsDevision(int CompanyId,string Code,string Name)
        {
            try
            {
                
                objEmployeeAdditional.AddEmpGsDevision(CompanyId, Code,Name);
                
            }
            catch (Exception ex)
            {
              
            }
        }
        public void UpdateEmpGsDevision( int gsId,string Code, string Name)
        {
            try
            {

                objEmployeeAdditional.UpdateEmpGsDevision( gsId,Code, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteEmpGsDevision(int gsId, string Code, string Name)
        {
            try
            {

                objEmployeeAdditional.DeleteEmpGsDevision(gsId, Code, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void AddEmpElectoralDistrict(int CompanyId,string Code, string Name)
        {
            try
            {

                objEmployeeAdditional.AddEmpElectoralDistrict(CompanyId,Code, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateEmpElectoralDistrict(int ELId, string Code, string Name)
        {
            try
            {

                objEmployeeAdditional.UpdateEmpElectoralDistrict(ELId, Code, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteEmpElectoralDistrict(int ELId)
        {
            try
            {

                objEmployeeAdditional.DeleteEmpElectoralDistrict(ELId);

            }
            catch (Exception ex)
            {

            }
        }
        public void AddEmpDivisional(int CompanyId,string Code, string Name)
        {
            try
            {

                objEmployeeAdditional.AddEmpDivisional(CompanyId,Code, Name);

            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateEmpDivisional(int DvId,string Code, string Name)
        {
            try
            {

                objEmployeeAdditional.UpdateEmpDivisional(DvId, Code, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteEmpDivisional(int DvId)
        {
            try
            {

                objEmployeeAdditional.DeleteEmpDivisional(DvId);

            }
            catch (Exception ex)
            {

            }
        }
        public void AddEmpTransportRout(int CompanyId, string number, string Name)
        {
            try
            {

                objEmployeeAdditional.AddEmpTransportRout(CompanyId, number, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateEmpTransportRout(int TrId, string Number, string Name)
        {
            try
            {

                objEmployeeAdditional.UpdateEmpTransportRout(TrId, Number, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteEmpTransport(int TrId)
        {
            try
            {

                objEmployeeAdditional.DeleteEmpTransport(TrId);

            }
            catch (Exception ex)
            {

            }
        }
        public void AddEmpVacciene(int CompanyId,string code, string Name)
        {
            try
            {

                objEmployeeAdditional.AddEmpVacciene(CompanyId, code, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateEmpVacciene( int vcId, string Code, string Name)
        {
            try
            {

                objEmployeeAdditional.UpdateEmpVacciene( vcId, Code, Name);

            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteEmpVacciene(int VcId)
        {
            try
            {

                objEmployeeAdditional.DeleteEmpVacciene(VcId);

            }
            catch (Exception ex)
            {

            }
        }
        public DataTable GetAdditonalInformation(long EmployeeId)
       {
           DataTable dataTable = new DataTable();
           try
           {
               dataTable = objEmployeeAdditional.GetAdditonalInformation(EmployeeId);
               SetValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;

           }
           return dataTable;
       }

        /// <summary>
        /// Gets the full employee information.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <returns></returns>
        public DataTable GetFullEmployeeInformation(long EmployeeId)
       {
           DataTable dataTable = new DataTable();
           try
           {
               dataTable = objEmployeeAdditional.GetFullEmployeeInformation(EmployeeId);
               SetValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;

           }
           return dataTable;
       }

       public DataTable GetPortalEmployeeInfo(long EmployeeId)
       {
           DataTable dataTable = new DataTable();
           try
           {
               dataTable = objEmployeeAdditional.GetPortalEmployeeInfo(EmployeeId);
               SetValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;

           }
           return dataTable;
       }

       public DataTable GeNationality()
       {
           DataTable dataTable = new DataTable();
           try
           {
               dataTable = objEmployeeAdditional.GetNationality();
               SetValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;

           }
           return dataTable;
       } 
       #endregion

       #region Distructor
       /// <summary>
       /// Releases unmanaged resources and performs other cleanup operations before the
       /// <see cref="EmployeeAdditional"/> is reclaimed by garbage collection.
       /// </summary>
       ~EmployeeAdditional()
       {
           GC.SuppressFinalize(this);
       } 
       #endregion
   }
}
