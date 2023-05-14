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
/// Class Description : HRM.Payroll.BLL.FestivalAdvance
/// Task Code: HRMV2P000018

namespace HRM.Payroll.BLL
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using HRM.Payroll.DAL;

    [DataObject]
    public class FestivalAdvance
    {
        #region Fields

        private FestivalAdvanceDAL objFestivalAdvanceDAL;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public FestivalAdvance()
        {
            objFestivalAdvanceDAL = new FestivalAdvanceDAL();
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
            _isError = objFestivalAdvanceDAL.IsError;
            _errorMsg = objFestivalAdvanceDAL.ErrorMsg;
            _errorNo = objFestivalAdvanceDAL.ErrorNo;
        }

        #endregion

        #region External

        public Int64 AddFestivalAdvance(Int64 employeeId, int AdvanceTypeId, DateTime takenDate, decimal festivalAdvanceAmount, DateTime startDate, DateTime endDate, int noOfPremiums, decimal premium, bool IsWorkFolw)
        {
            _isError = false;
            _errorMsg = string.Empty;

            Int64 CreatedIndexID = -1;

            try
            {
                CreatedIndexID = objFestivalAdvanceDAL.AddFestivalAdvance(employeeId, AdvanceTypeId, takenDate, festivalAdvanceAmount, startDate, endDate, noOfPremiums, premium, IsWorkFolw);
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

        public int UpdateFestivalAdvance(Int64 festivalAdvanceId, Int32 AdvanceTypeId, DateTime takenDate, decimal festivalAdvanceAmount, DateTime startDate, DateTime endDate, int noOfPremiums,int dueNoofPremiums, int NewDueNoFoPrimiums, decimal premium)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ReturnValue = -1;

            try
            {
                ReturnValue = objFestivalAdvanceDAL.UpdateFestivalAdvance(festivalAdvanceId, AdvanceTypeId, takenDate, festivalAdvanceAmount, startDate, endDate, noOfPremiums, dueNoofPremiums,NewDueNoFoPrimiums, premium);
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

            return ReturnValue;
        }

        public int DeleteFestivalAdvance(Int64 festivalAdvanceId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ReturnValue = -1;

            try
            {
                ReturnValue = objFestivalAdvanceDAL.DeleteFestivalAdvance(festivalAdvanceId);
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

            return ReturnValue;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetPayPeriodsForCombo()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objFestivalAdvanceDAL.GetPayPeriodsForCombo();
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
        public DataTable GetFestivalAdvancesForEmployee(Int64 employeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objFestivalAdvanceDAL.GetFestivalAdvancesForEmployee(employeeId);
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
