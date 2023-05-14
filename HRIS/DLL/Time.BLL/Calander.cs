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
    public class Calander
    {

       #region Fields

        private DataTable dtTable;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _dayTypeId = 0;
        CalanderDAL objCalander;

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

        public Calander ()
        {
            objCalander = new CalanderDAL();
        }
        #endregion


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCalander(int CompanyId)
        {
            try
            {
                dtTable = objCalander.GetCalander(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCalander(int CompanyId,int Year)
        {
            try
            {
                dtTable = objCalander.GetCalander(CompanyId,Year);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

    }
}
