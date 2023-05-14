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
    public class OTType
    {
        OTTypesDAL objOTtype;

       #region Fields

        private DataTable dtTable;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _dayTypeId = 0;
  
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
        public int OTTypeId
        {
            get { return _dayTypeId; }
        }
        
        #endregion

        #region Constructor

        public OTType ()
        {
            objOTtype = new OTTypesDAL();
        }
        #endregion

        #region Methods

        #region OTType

        [DataObjectMethod(DataObjectMethodType.Insert,true)]
        public void AddOTTypes(string OTTypeCode,string OTDescription,decimal OTRate,decimal MaxHours,string CreatedUser)
        {
            try
            {
                objOTtype.AddOTTypes(OTTypeCode, OTDescription, OTRate, MaxHours, CreatedUser);
                _dayTypeId = objOTtype.OTTypeId;
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

         [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateOTType(int OTTypeId,string OTDescription,decimal OTRate,decimal MaxHours,string ModifyUser)
        {
            try
            {
                objOTtype.UpdateOTType(OTTypeId,OTDescription,OTRate, MaxHours, ModifyUser);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

       [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetOTType()
        {
            try
            {
                dtTable = objOTtype.GetOTType();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]     
       public DataTable GetOTTypeById(int OTTypeId)
       {
           try
           {
               dtTable = objOTtype.GetOTTypeById(OTTypeId);
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }

           return dtTable;
       }

        public DataTable CheckOTTypeByCode(string OTTypeCode)
        {
            try
            {
                dtTable = objOTtype.CheckOTTypeTypeCode(OTTypeCode);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public void DeleteOTType(int OTTypeId)
        {
            try
            {
                objOTtype.DeleteOTType(OTTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetShiftSetupByOTType(int OTTypeId)
        {
            try
            {
                dtTable = objOTtype.GetShiftSetupByOTType(OTTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        #endregion

        #endregion

        #region Destructor

        ~OTType()
            {
                GC.SuppressFinalize(this);
            }
        #endregion
    }
}
