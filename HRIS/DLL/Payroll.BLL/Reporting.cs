/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name       : HRM.Payroll.BLL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Asanka C. Amarasinghe
/// Created Timestamp   : 7/18/2011
/// Class Description   : HRM.Payroll.BLL.Reporting
/// Task Code           : --

namespace HRM.Payroll.BLL
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using HRM.Payroll.DAL;

    [DataObject]
    public class Reporting
    {
        #region Fields

        private ReportingDAL objReportingDAL;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public Reporting()
        {
            objReportingDAL = new ReportingDAL();
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
            _isError = objReportingDAL.IsError;
            _errorMsg = objReportingDAL.ErrorMsg;
            _errorNo = objReportingDAL.ErrorNo;
        }

        #endregion

        #region External

        public Int32[] GenerateSelectedEmployeeReportRecords(string computerName, string userName, string organizationSelectCriteria, string designationSelectCriteria)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();
            
            Int32[] returnStat = new int[2];
            returnStat[0] = -1;
            returnStat[1] = -1;

            try
            {
                dtTable = objReportingDAL.GenerateSelectedEmployeeReportRecords(computerName, userName, organizationSelectCriteria, designationSelectCriteria);
                if (objReportingDAL.IsError == false)
                {
                    if (dtTable != null)
                    {
                        if (dtTable.Rows.Count > 0)
                        {
                            returnStat[0] = Convert.ToInt32(dtTable.Rows[0][0].ToString().Trim());
                            returnStat[1] = Convert.ToInt32(dtTable.Rows[0][1].ToString().Trim());
                        }
                    }   
                }
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

            return returnStat;
        }

        public DataTable GetUnitWiseSum(int CompanyId, string EncriptionKey, int Year, int Month, string CatId, int LevelIndex)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objReportingDAL.GetUnitWiseSum(CompanyId, EncriptionKey, Year, Month, CatId, LevelIndex);
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
    }
}
