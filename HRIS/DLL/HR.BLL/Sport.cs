
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.HR.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.HR.BLL
{
   [DataObject]
   public  class Sport
    {

        #region Fields
        SportDAL objSportDAL;
        private DataTable _sports;
        private bool _isError;
        private string _errorMsg;
        private int _sportId;
        private long _employeeSportId;
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


        public int SportId
        {
            get { return _sportId; }
            set { _sportId = value; }
        }
       public long EmployeeSportId
       {
           get{return _employeeSportId;}
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
        public Sport()
        {
           objSportDAL=new SportDAL();
        } 
        #endregion

        #region Methods

        private void SetValues()
        {
            _isError = objSportDAL.IsError;
            _errorMsg = objSportDAL.ErrorMsg;
        }

        #region Sport
        /// <summary>
        /// Adds the sport.
        /// </summary>
        /// <param name="SportCode">The sport code.</param>
        /// <param name="SportName">Name of the sport.</param>
        public void AddSport(string SportCode, string SportName)
        {
            try
            {
                objSportDAL.AddSport(SportCode, SportName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the sport.
        /// </summary>
        /// <param name="SportId">The sport id.</param>
        /// <param name="SportCode">The sport code.</param>
        /// <param name="SportName">Name of the sport.</param>
        public void UpdateSport(int SportId, string SportCode, string SportName)
        {
            try
            {
                objSportDAL.UpdateSport(SportId, SportCode, SportName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Gets the sport.
        /// </summary>
        /// <param name="SportId">The sport id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetSport(int SportId)
        {
            DataTable dtNew = new DataTable();
            try
            {
                dtNew = objSportDAL.GetSport(SportId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtNew;
        }


        /// <summary>
        /// Deletes the sport.
        /// </summary>
        /// <param name="SportId">The sport id.</param>
        public void DeleteSport(int SportId)
        {
            try
            {
                objSportDAL.DeleteSport(SportId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }

        } 
        #endregion

        #region EmployeeSport
        /// <summary>
        /// Adds the employee Sport.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="SportId">The Sport id as int.</param>
        public void AddEmployeeSport(long EmployeeId, DataTable tbl)
        {
            try
            {
                foreach (DataRow row in tbl.Rows)
                {

                    objSportDAL.AddEmployeeSport(EmployeeId, Convert.ToInt32(row["SportID"].ToString()));
                    _employeeSportId = objSportDAL.EmployeeSportId;
                    SetValues();

                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }


        }


        public void AddEmployeeSport2(long EmployeeId, int SportId)
        {
            try
            {
                objSportDAL.AddEmployeeSport(EmployeeId, SportId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        /// <summary>
        /// Updates the employee Sport.
        /// </summary>
        /// <param name="EmployeeSportId">The employee Sport id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="SportId">The Sport id as int.</param>
        public void UpdateEmployeeSport(long EmployeeSportId, long EmployeeId, int SportId)
        {
            try
            {
                objSportDAL.UpdateEmployeeSport(EmployeeSportId, EmployeeId, SportId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        /// <summary>
        /// Gets the employee Sport.
        /// </summary>
        /// <param name="EmployeeSportId">The employee Sport id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeSport(long EmployeeSportId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objSportDAL.GetEmployeeSport(EmployeeSportId);
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
        public DataTable GetEmployeeSportByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objSportDAL.GetEmployeeSportByEmployeeId(EmployeeId);
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
        /// Deletes the employee Sport.
        /// </summary>
        /// <param name="EmployeeSportId">The employee Sport id as int.</param>
        public void DeleteEmployeeSport(long EmployeeSportId)
        {
            try
            {
                objSportDAL.DeleteEmployeeSport(EmployeeSportId);
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
        ~Sport()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion


    }
}
