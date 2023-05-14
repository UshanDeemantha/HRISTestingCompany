/// <summary>
/// /// <summary>
/// /// /// <summary>
/// /// /// Copyright (c) 2000-2008 MasterKey Solutions .
/// /// /// MasterKey Solutions, 194G, Nawala Road, Narahenpita, Colombo 5.
/// /// /// All right received.
/// /// /// This software is the confidential and proprietary information of
/// /// /// MasterKey Solutions (Confidential Information). You shall not disclose such
/// /// /// Confidential Information and shall use it only in accordance with the
/// /// /// terms of the license agreement you entered into with MasterKey Solutions.
/// /// ///
/// /// /// Solution Name : =HRM.Payroll.BLL
/// /// /// Cording Standard : MasterKey Cording Standards
/// /// /// Author : Chiran Chamara
/// /// /// Created Timestamp :7/21/2011
/// /// /// Class Description : HRM.Payroll.BLL.StandingOrders
/// ////// Task Code :HRMV2P000022
/// /// /// </summary>
/// /// </summary>
/// </summary>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using HRM.Payroll.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.Payroll.BLL
{
    [DataObject]
    public class StandingOrders
    {
        #region Fields

        private StandingOrdersDAL _objStandingOrders;

        private string _result;        
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public StandingOrders()
        {
            _objStandingOrders = new StandingOrdersDAL();
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
            _isError = _objStandingOrders.IsError;
            _errorMsg = _objStandingOrders.ErrorMsg;
            _errorNo = _objStandingOrders.ErrorNo;
           
        }


        #endregion

        #region External



        /// <summary>
        /// Adds the standing order.
        /// </summary>
        /// <param name="StandingOrderNo">The standing order no.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="TotalAmount">The total amount.</param>
        /// <param name="StandingOrderAmount">The standing order amount.</param>
        /// <param name="Premium">The premium.</param>
        /// <param name="NoOfPremiums">The no of premiums.</param>
        /// <param name="DueNoofPremiums">The due noof premiums.</param>
        /// <param name="SettleAmount">The settle amount.</param>
        /// <param name="FromPayPeriodId">From pay period id.</param>
        /// <param name="ToPayPeriodId">To pay period id.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="BankId">The bank id.</param>
        /// <param name="BranchId">The branch id.</param>
        /// <param name="Account">The account.</param>
        /// <param name="CurrentPayment">The current payment.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        /// <param name="IsCompleted">if set to <c>true</c> [is completed].</param>
        public void AddStandingOrder( long EmployeeId,decimal StandingOrderAmount, int BankId,int BranchId,string Account,bool Active,string user)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objStandingOrders.AddStandingOrder( EmployeeId,  StandingOrderAmount,  BankId,  BranchId,  Account,  Active,  user);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

     
        /// <summary>
        /// Updates the standing order.
        /// </summary>
        /// <param name="StandingOrderId">The standing order id.</param>
        /// <param name="StandingOrderNo">The standing order no.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="TotalAmount">The total amount.</param>
        /// <param name="StandingOrderAmount">The standing order amount.</param>
        /// <param name="Premium">The premium.</param>
        /// <param name="NoOfPremiums">The no of premiums.</param>
        /// <param name="DueNoofPremiums">The due noof premiums.</param>
        /// <param name="SettleAmount">The settle amount.</param>
        /// <param name="FromPayPeriodId">From pay period id.</param>
        /// <param name="ToPayPeriodId">To pay period id.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="BankId">The bank id.</param>
        /// <param name="BranchId">The branch id.</param>
        /// <param name="Account">The account.</param>
        /// <param name="CurrentPayment">The current payment.</param>
        /// <param name="IsPosted">if set to <c>true</c> [is posted].</param>
        /// <param name="IsCompleted">if set to <c>true</c> [is completed].</param>
        public void UpdateStandingOrder(Int64 standingOrderId, Int64 EmployeeId, decimal StandingOrderAmount, int BankId,int BranchId,string Account,bool Active, bool IsPosted,
                                      string modifieduser)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objStandingOrders.UpdateStandingOrder(standingOrderId,  EmployeeId,   StandingOrderAmount,    BankId,  BranchId,  Account, Active, IsPosted, modifieduser);
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
        public void DeleteStandingOrder(long StandingOrderId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            try
            {
                _objStandingOrders.DeleteStandingOrder(StandingOrderId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        
        /// <summary>
        /// Gets the standing order by employee.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetStandingOrderByEmployee(long EmployeeId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            DataTable dtStandingOrders = new DataTable();
            try
            {
                dtStandingOrders = _objStandingOrders.GetStandingOrderByEmployee(EmployeeId);
                SetValues();
            }
            catch
            { }

            return dtStandingOrders;

        }
        #endregion



        #endregion

    } 
      
}
