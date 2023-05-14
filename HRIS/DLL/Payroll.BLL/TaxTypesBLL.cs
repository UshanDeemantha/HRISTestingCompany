/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name : HRM.Payroll.BLL
/// Cording Standard : MasterKey Cording Standards
/// Author : Asanka C. Amarasinghe
/// Created Timestamp : 7/13/2011
/// Class Description : HRM.Payroll.BLL.TaxTypesBLL
/// Task Code: HRMV2P000009


using System;
using System.ComponentModel;
using System.Data;
using HRM.Payroll.DAL;

namespace HRM.Payroll.BLL
{
    [DataObject]
    public class TaxTypesBLL
    {
        #region Fields

        private TaxTypesDAL objTaxTypesDAL;
        
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public TaxTypesBLL()
        {
            objTaxTypesDAL = new TaxTypesDAL();
        }
         #endregion

        #region Propaties
        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
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

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        #endregion

        #region Methods

        #region Internal

        private void SetValues()
        {
            _isError = objTaxTypesDAL.IsError;
            _errorMsg = objTaxTypesDAL.ErrorMsg;
            _errorNo = objTaxTypesDAL.ErrorNo;
        }

        #endregion

        #region External

        public int AddTaxType(string taxType, decimal percentage, bool ReferTable)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int CreatedIndexID = -1;

            try
            {
                CreatedIndexID = objTaxTypesDAL.AddTaxType(taxType, percentage, ReferTable);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return CreatedIndexID;
        }

        public void DeleteTaxType(int taxTypeId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            try
            {
                objTaxTypesDAL.DeleteTaxType(taxTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public int EditTaxType(int taxTypeID, string taxType, decimal percentage, bool ReferTable)
        {
            _isError = false;
            _errorMsg = string.Empty;

            // return status -1 = if method returned an error
            // return status  0 = if tax type id specified cannot be found
            // return status  1 = if records updated
            // return status  2 = if type specified already exists
            int ReturnID = -1;

            try
            {
                ReturnID = objTaxTypesDAL.EditTaxType(taxTypeID, taxType, percentage, ReferTable);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return ReturnID;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetTaxTypes()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objTaxTypesDAL.GetTaxTypes(-1);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetTaxTypes(int taxTypeID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objTaxTypesDAL.GetTaxTypes(taxTypeID);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                if (_isError == false)
                {
                    SetValues();
                }
            }

            return dtTable;
        }

        #endregion

        #endregion
    }
}
