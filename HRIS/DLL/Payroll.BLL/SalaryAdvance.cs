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
    public class SalaryAdvance
    {
        #region Fields

        private SalaryAdvanceDAL objSalaryAdvanceDAL;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public SalaryAdvance()
        {
            objSalaryAdvanceDAL = new SalaryAdvanceDAL();
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
            _isError = objSalaryAdvanceDAL.IsError;
            _errorMsg = objSalaryAdvanceDAL.ErrorMsg;
            _errorNo = objSalaryAdvanceDAL.ErrorNo;
        }

        #endregion

        #region External

        public Int64 AddSalaryAdvance(Int64 employeeId, DateTime takenDate, int payPeriodId, decimal amount, bool IsWorkflow)
        {
            _isError = false;
            _errorMsg = string.Empty;

            Int64 CreatedIndexID = -1;

            try
            {
                CreatedIndexID = objSalaryAdvanceDAL.AddSalaryAdvance(employeeId, takenDate, payPeriodId, amount, IsWorkflow);
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

        public Int64 UpdateSalaryAdvance(Int64 SalaryAdvanceId, Int64 employeeId, DateTime takenDate, int payPeriodId, decimal amount)
        {
            _isError = false;
            _errorMsg = string.Empty;
            Int64 CreatedIndexID = -1;
            try
            {
                CreatedIndexID = objSalaryAdvanceDAL.UpdateSalaryAdvance(SalaryAdvanceId,employeeId, takenDate, payPeriodId, amount);
              
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

        public void DeleteSalaryAdvance(Int64 SalaryAdvanceId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            try
            {
                objSalaryAdvanceDAL.DeleteSalaryAdvance(SalaryAdvanceId);

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
        }


        public DataTable GetLastPostedPayperiod(int PayCategoryId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objSalaryAdvanceDAL.GetLastPostedPayperiod(PayCategoryId);
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

        public void AddAdvance(long EmployeeId, string AdvanceType, decimal Amount, DateTime StartDate, DateTime EndDate, int NoOfInstallments, bool IsCompleted, bool IsHold, string EncryptionKey, string CreatedUser)
        {
            _isError = false;
            _errorMsg = string.Empty;

            try
            {
                objSalaryAdvanceDAL.AddAdvance(EmployeeId, AdvanceType, Amount, StartDate, EndDate, NoOfInstallments, IsCompleted, IsHold, EncryptionKey, CreatedUser);

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

        }

        public void UpdateAdvance(int Id, bool IsCompleted, bool IsHold, string CreatedUser)
        {
            _isError = false;
            _errorMsg = string.Empty;

            try
            {
                objSalaryAdvanceDAL.UpdateAdvance(Id, IsCompleted, IsHold, CreatedUser);

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

        }

        public DataTable GetUserField()
        {
            DataTable _UserFields = new DataTable();
            try
            {
                _UserFields = objSalaryAdvanceDAL.GetUserField();
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return _UserFields;
        }

        public DataTable GetAdvance(int Id, int PayCategoryId, string EncryptionKey, int CompanyId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objSalaryAdvanceDAL.GetAdvance(Id, PayCategoryId, EncryptionKey, CompanyId);
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

        public DataTable GetMaxAdvanceAmount(long EmployeeId, string EncryptionKey)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objSalaryAdvanceDAL.GetMaxAdvanceAmount(EmployeeId, EncryptionKey);
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

        public DataTable GetNotCompletedAdvances(long EmployeeId, string AdvanceType)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objSalaryAdvanceDAL.GetNotCompletedAdvances(EmployeeId, AdvanceType);
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

        public DataTable CheckSalaryPost(long EmployeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objSalaryAdvanceDAL.CheckSalaryPost(EmployeeId);
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

        public void DeleteAdvance(int Id)
        {
            _isError = false;
            _errorMsg = string.Empty;

            try
            {
                objSalaryAdvanceDAL.DeleteAdvance(Id);

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
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetPayPeriodsForSalaryAdvanceCombo(Int64 employeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objSalaryAdvanceDAL.GetPayPeriodsForSalaryAdvanceCombo(employeeId);
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
        public DataTable GetSalaryAdvancesForEmployee(Int64 employeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objSalaryAdvanceDAL.GetSalaryAdvancesForEmployee(employeeId);
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
