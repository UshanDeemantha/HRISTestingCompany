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
/// Created Timestamp : 2011-July-25
/// Class Description : HRM.Payroll.BLL.SalaryAdvance
/// Task Code: HRMV2P000019

namespace HRM.Payroll.BLL
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using HRM.Payroll.DAL;

    [DataObject]
    public class References
    {
        #region Fields

        private ReferencesDAL objReferencesDAL;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public References()
        {
            objReferencesDAL = new ReferencesDAL();
        }

         #endregion

        #region Propaties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the Error MSG.
        /// </summary>
        /// <value>Occurred Error Message As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        /// <summary>
        /// Gets the Error Number.
        /// </summary>
        /// <value>Occurred Error as a Numeric Representation</value>
        public int ErrorNo
        {
            get { return _errorNo; }
        }

        #endregion

        #region Methods

        #region Internal

        private void SetValues()
        {
            _isError = objReferencesDAL.IsError;
            _errorMsg = objReferencesDAL.ErrorMsg;
            _errorNo = objReferencesDAL.ErrorNo;
        }

        #endregion

        #region External
        public DataTable GetEmployeeCodeForUnitCompanies()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetEmployeeCodeForUnitCompanies();
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
        public DataTable GetEmployeesNicNo()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetEmployeesNicNo();
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
        public DataTable GetBankById(int bankId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetBankById(bankId);
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
        public DataTable GetBankBranchByBankId(int bankId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetBankBranchByBankId(bankId);
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

        public DataTable GetPayEmployeePay()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetPayEmployeePay();
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
        public DataTable GetEmployeeCodeNICANDEPFNo(int CompanyId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetEmployeeCodeNICANDEPFNo(CompanyId);
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

        public DataTable GetFullDesignationStructure()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetFullDesignationStructure();
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

        public DataTable GetFullOrganizationStructure()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetFullOrganizationStructure();
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
        public DataTable GetUnpostedPayPeriodCategories()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetUnpostedPayPeriodCategories();
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
        public DataTable GetPayPeriod()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objReferencesDAL.GetPayPeriod();
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
