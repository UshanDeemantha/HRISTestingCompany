/// <summary>
/// Copyright (c) 2000-2011 MasterKey Solutions .
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
///
/// Solution Name: HRM.Payroll.BLL
/// Cording Standard: MasterKey Cording Standards
/// Author: Chiran Chamara
/// Author: Asanka C. Amarasinghe
/// Created Timestamp: 7/18/2011
/// Class Description: HRM.Payroll.BLL.Currency
/// Task Code: HRMV2P000016
/// Task Code: HRMV2P000008
/// </summary>

using System;
using System.ComponentModel;
using System.Data;
using HRM.Payroll.DAL;

namespace HRM.Payroll.BLL
{
    [DataObject]
    public class Currency
    {
        
        #region Fields

        private CurrencyDAL objCurrency;

        private string _result;        
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public Currency()
        {
            objCurrency = new CurrencyDAL();
        }
         #endregion

        #region Propeties
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
            get
            {
                return _errorNo;
            }
        }

        public string Result
        {
            get { return _result; }
        }
        #endregion

        #region Methods

        #region Internal

        private void SetValues()
        {
            _isError = objCurrency.IsError;
            _errorMsg = objCurrency.ErrorMsg;
            _errorNo = objCurrency.ErrorNo;
        }

        #endregion

        #region External

        #region Currency Rates (Task Code :HRMV2P000016)

        /// <summary>
        /// Adds the currency rate.
        /// </summary>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="CurrencyCode">The currency code.</param>
        /// <param name="Date">The date.</param>
        /// <param name="Rate">The rate.</param>
        /// <param name="UserName">Name of the user.</param>
        public void AddCurrencyRate(int PayPeriodId, string CurrencyCode, DateTime Date, decimal Rate, string UserName)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                objCurrency.AddCurrencyRate(PayPeriodId, CurrencyCode, Date, Rate, UserName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the currency rate.
        /// </summary>
        /// <param name="RateId">The rate id.</param>
        /// <param name="PayPeriodId">The pay period id.</param>
        /// <param name="CurrencyCode">The currency code.</param>
        /// <param name="Date">The date.</param>
        /// <param name="Rate">The rate.</param>
        /// <param name="UserName">Name of the user.</param>
        public void UpdateCurrencyRate(long RateId, int PayPeriodId, string CurrencyCode, DateTime Date, decimal Rate, string UserName)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                objCurrency.UpdateCurrencyRate(RateId, PayPeriodId, CurrencyCode, Date, Rate, UserName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Deletes the currency rate.
        /// </summary>
        /// <param name="RateId">The rate id.</param>
        public void DeleteCurrencyRate(long RateId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                objCurrency.DeleteCurrencyRate(RateId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the currency rate.
        /// </summary>
        /// <param name="RateId">The rate id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCurrencyRate(long RateId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtCurrencyRates = new DataTable();
            try
            {
                dtCurrencyRates = objCurrency.GetCurrencyRate(RateId);
                SetValues();
            }
            catch
            { }

            return dtCurrencyRates;

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCurrencyRateByPayPeriod(int PayPeriodId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtCurrencyRates = new DataTable();
            try
            {
                dtCurrencyRates = objCurrency.GetCurrencyRateByPayPeriod(PayPeriodId);
                SetValues();
            }
            catch
            { }

            return dtCurrencyRates;

        } 
        #endregion

        #region Currency Types (Task Code :HRMV2P000008)

        public int AddCurrencyType(string currencyCode, string currencyName, string country)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ResultID = -1;

            try
            {
                ResultID = objCurrency.AddCurrencyType(currencyCode, currencyName, country);
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

            return ResultID;
        }

        public void DeleteCurrencyType(string currencyCode)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            try
            {
                objCurrency.DeleteCurrencyType(currencyCode);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        
        public int EditCurrencyType(string currencyCode, string currencyName, string country)
        {
            _isError = false;
            _errorMsg = string.Empty;

            // return status -1 = if method returned an error
            // return status  0 = if currency type specified cannot be found
            // return status  1 = if records updated
            // return status  2 = if type specified already exists
            int ResultID = -1;

            try
            {
                ResultID = objCurrency.EditCurrencyType(currencyCode, currencyName, country);
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

            return ResultID;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCurrencyTypes()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objCurrency.GetCurrencyTypes("");
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
        public DataTable GetCurrencyTypes(string currencyCode)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objCurrency.GetCurrencyTypes(currencyCode);
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
        public DataTable GetCurrencyTypesByPayPeriod(int PayPeriodId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objCurrency.GetCurrencyTypesByPayPeriod(PayPeriodId);
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
        public DataTable GetConvertionCurrencyTypes(string CurrencyCode)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objCurrency.GetConvertionCurrencyTypes(CurrencyCode);
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

        #region Currency Notes

        public int AddCurrencyNote(string currencyCode, string currencyNote, int value)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ResultID = -1;

            try
            {
                ResultID = objCurrency.AddCurrencyNote(currencyCode, currencyNote, value);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                
              SetValues();
                
            }

            return ResultID;
        }

        public void DeleteCurrencyNote(string currencyCode, string currencyNote)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            try
            {
                objCurrency.DeleteCurrencyNote(currencyCode, currencyNote);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                SetValues();
            }
        }

        public void EditCurrencyNote(string currencyCode, string currencyNote, int value)
        {
            _isError = false;
            _errorMsg = string.Empty;

            // return status -1 = if method returned an error
            // return status  0 = if currency type specified cannot be found
            // return status  1 = if records updated
            // return status  2 = if type specified already exists
   

            try
            {
              objCurrency.EditCurrencyNote(currencyCode, currencyNote, value);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                    SetValues();
                
            }

           
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCurrencyNotes()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objCurrency.GetCurrencyNotes(" "," ");
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
        public DataTable GetCurrencyNotes(string currencyCode)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objCurrency.GetCurrencyNotes(currencyCode, " ");
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
        public DataTable GetCurrencyNotes(string currencyCode, string currencyNote)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objCurrency.GetCurrencyNotes(currencyCode, currencyNote);
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

        #endregion
    }
}
