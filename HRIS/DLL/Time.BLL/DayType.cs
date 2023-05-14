using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HRM.Time.DAL;
using System.ComponentModel;

namespace HRM.Time.BLL
{
    [DataObject]
    public class DayType
    {
        
       #region Fields

        private DataTable dtTable;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _dayTypeId = 0;
        DayTypesDAL objDaytype;

        #endregion

       #region Properties

        public bool IsError
        {
            get { return _isError; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        public int DayTypeId
        {
            get { return _dayTypeId; }
        }
        
        #endregion

        #region Constructor

        public DayType ()
        {
            objDaytype = new DayTypesDAL();
        }

        public DayType(int CompanyId)
        {
            objDaytype = new DayTypesDAL();
        }
        #endregion

        #region Methods

        #region DayType

        public void AddDayTypes(string DayTypeCode, string DayType, string BackGroundColour, string FontColour, string CreatedUser, DateTime CreatedDate, bool IsDefault)
        {
            try
            {
                objDaytype.AddDayTypes(DayTypeCode,DayType,BackGroundColour,FontColour,CreatedUser,CreatedDate,IsDefault);
                _dayTypeId = objDaytype.DayTypeId;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void UpdateDayType(int DayTypeId, string DayType, string BackGroundColour, string FontColour, string ModifyUser, DateTime ModifyDate, bool Default)
        {
            try
            {
                objDaytype.UpdateDayType(DayTypeId,DayType,BackGroundColour,FontColour,ModifyUser,ModifyDate,Default);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public DataTable tbledaytypecheck(int DayTypeId)
        {
            try
            {
                dtTable = objDaytype.tbledaytypecheck(DayTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
       public DataTable GetDayType(int DayTypeId)
        {
            try
            {
                dtTable = objDaytype.GetDayType(DayTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

       public DataTable GetDayTypeCode(string DayTypeCode)
       {
           try
           {
               dtTable = objDaytype.GetDayTypeCode(DayTypeCode);
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }

           return dtTable;
       }

       public DataTable GetDayTypeById(int DayTypeId)
       {
           try
           {
               dtTable = objDaytype.GetDayType(DayTypeId);
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }

           return dtTable;
       }

       public DataTable CheckDefaultDayType(bool Default)
       {
           try
           {
               dtTable = objDaytype.CheckDefaultDayType(Default);
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }

           return dtTable;
       }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public void DeleteDayType(int DayTypeId)
        {
            try
            {
                objDaytype.DeleteDayType(DayTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }   
        #endregion


        #region Calender

        public DataTable GetCalender(int CompanyId,int Year)
        {
            try
            {
                dtTable = objDaytype.GetCalender(CompanyId, Year);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public DataTable GetCalenderByYearMonthDay(int CompanyId, int Year,int Month,int Date)
        {
            try
            {
                dtTable = objDaytype.GetCalenderByYearMonthDay(CompanyId, Year, Month, Date);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }


        public void SetEmpLeaveLev(int CompanyId, DateTime CalenderDate,string CreatedUser)
        {
            try
            {
                objDaytype.SetEmpLeaveLev( CompanyId,  CalenderDate,  CreatedUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void AddCalender(DateTime CalenderDate, int CompanyId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, bool Posted, string CreatedUser,string Subject)
        {
            try
            {
                objDaytype.AddCalender(CalenderDate, CompanyId, StartTime, EndTime, Day, Month, Year, DayTypeId, Posted, CreatedUser, Subject);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void CopyCalendar(int FromCompanyId, int ToCompanyId, string SelectedYear, string CreatedUser)
        {
            try
            {
                objDaytype.CopyCalendar(FromCompanyId, ToCompanyId, SelectedYear, CreatedUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void UpdateCalender(DateTime CalenderDate, int CompanyId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, bool Posted, string CreatedUser)
        {
            try
            {
                objDaytype.UpdateCalender(CalenderDate, CompanyId, StartTime, EndTime, Day, Month, Year, DayTypeId, Posted, CreatedUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteCalender(DateTime CalenderDate, int CompanyId)
        {
            try
            {
                objDaytype.DeleteCalender(CalenderDate, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void DeleteCalenderById(Int64 DayId, int CompanyId)
        {
            try
            {
                objDaytype.DeleteCalenderById(DayId, CompanyId);
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

        ~DayType()
            {
                GC.SuppressFinalize(this);
            }
        #endregion
    }
}
