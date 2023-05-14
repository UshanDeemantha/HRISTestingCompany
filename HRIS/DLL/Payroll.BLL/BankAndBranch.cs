namespace HRM.Payroll.BLL
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using HRM.Payroll.DAL;

    [DataObject]
    public class BankAndBranch
    {
        #region Fields

        private BankAndBranchDAL objBankAndBranch;

        private string _result;
        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructor

        public BankAndBranch()
        {
            objBankAndBranch = new BankAndBranchDAL();
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
            _isError = objBankAndBranch.IsError;
            _errorMsg = objBankAndBranch.ErrorMsg;
            _errorNo = objBankAndBranch.ErrorNo;
        }

        #endregion

        #region External
        public void InsertBankDetailsFrBcode(int BankId, string BranchCode, string Branch)
        {
            try
            {
                objBankAndBranch.InsertBankDetailsFrBcode(BankId, BranchCode, Branch);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void UpdateBankDetailsFrBcode(int BankId, int BranchId, string BranchCode, string Branch)
        {
            try
            {
                objBankAndBranch.UpdateBankDetailsFrBcode(BankId, BranchId, BranchCode, Branch);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public DataTable GetBankBranch(int BankId)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            DataTable dtData = new DataTable();

            try
            {
                dtData = objBankAndBranch.GetBankBranch(BankId);
                SetValues();
            }
            catch
            { }

            return dtData;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetBankDetails()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            DataTable dtData = new DataTable();

            try
            {
                dtData = objBankAndBranch.GetBankDetails();
                SetValues();
            }
            catch
            { }

            return dtData;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetBankBranchDetails()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            DataTable dtData = new DataTable();

            try
            {
                dtData = objBankAndBranch.GetBankBranchDetails();
                SetValues();
            }
            catch
            { }

            return dtData;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetBankBranchDetails(int bankID)
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = 0;

            DataTable dtData = new DataTable();

            try
            {
                dtData = objBankAndBranch.GetBankBranchDetails(bankID);
                SetValues();
            }
            catch
            { }

            return dtData;
        }
                
        #endregion

        #endregion
    }
}
