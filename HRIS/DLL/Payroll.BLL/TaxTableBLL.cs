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
/// Created Timestamp : 7/18/2011
/// Class Description : HRM.Payroll.BLL.TaxTableBLL
/// Task Code: HRMV2P000010


using System;
using System.ComponentModel;
using System.Data;
using HRM.Payroll.DAL;

namespace HRM.Payroll.BLL
{
    [DataObject]
    public class TaxTableBLL
    {
        #region Fields

        private TaxTableDAL objTaxTableDAL;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public TaxTableBLL()
        {
            objTaxTableDAL = new TaxTableDAL();
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
            _isError = objTaxTableDAL.IsError;
            _errorMsg = objTaxTableDAL.ErrorMsg;
            _errorNo = objTaxTableDAL.ErrorNo;
        }

        #endregion

        #region External

        public int AddToTaxTable(int taxTypeId, string taxName, decimal fromAmount, decimal toAmount, decimal taxPercentage, decimal taxValue)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int CreatedIndexID = -1;

            try
            {
                CreatedIndexID = objTaxTableDAL.AddToTaxTable(taxTypeId, taxName, fromAmount, toAmount, taxPercentage, taxValue);
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

        public void DeleteFromTaxTable(int taxId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            try
            {
                objTaxTableDAL.DeleteFromTaxTable(taxId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public int EditTaxTable(int taxId, int taxTypeId, string taxName, decimal fromAmount, decimal toAmount, decimal taxPercentage, decimal taxValue)
        {
            _isError = false;
            _errorMsg = string.Empty;

            // return status -1 = if method returned an error
            // return status  0 = if tax id specified cannot be found
            // return status  1 = if records updated
            // return status  2 = if type specified already exists
            int ReturnID = -1;

            try
            {
                ReturnID = objTaxTableDAL.EditTaxTable(taxId, taxTypeId, taxName, fromAmount, toAmount, taxPercentage, taxValue);
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
        public DataTable GetTaxTypesForCombo()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objTaxTableDAL.GetTaxTypesForCombo();
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
        public DataTable GetTaxTable()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objTaxTableDAL.GetTaxTable(-1);
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
        public DataTable GetTaxTable(int taxID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objTaxTableDAL.GetTaxTable(taxID);
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
