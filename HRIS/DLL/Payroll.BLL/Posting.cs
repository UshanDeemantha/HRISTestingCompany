
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using HRM.Payroll.DAL;
using HRM.Common.BLL;
using System.ComponentModel;

namespace HRM.Payroll.BLL
{
    /// <summary>
    /// Posting Class(Main Processing/Unprocessing)
    /// </summary>
    /// 
  
    [DataObject]
    public class Posting
    {
       

        #region Fields
        private PostingDAL objPosting; 
        private bool _isError;
        private int _errorNo;
        private string _errorMsg;
        private string _result;
        private bool _isSuccess;
        private int _payPeriodId;
        private bool isDuplicate;
        private bool _isMinusSalary;
        #endregion

        #region Properties
        public int PayPeriodId
        {
            get { return _payPeriodId; }
            set { _payPeriodId = value; }
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public bool IsError
        {
            get { return _isError; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        } 

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public bool IsDuplicate
        {
            get { return isDuplicate; }
            set { isDuplicate = value; }
        }

        public string Result
        {
            get { return _result; }
        }

        public bool IsMinusSalary
        {
            get { return _isMinusSalary; }
        }
        #endregion

        #region Constructor
        public Posting()
        {
            objPosting = new PostingDAL();
        }

        public Posting(int PayPeriodId)
        {
            objPosting = new PostingDAL();
            _payPeriodId = PayPeriodId;
        } 
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objPosting.IsError;
            _errorNo = objPosting.ErrorNo;
            _errorMsg = objPosting.ErrorMsg;
            _result = objPosting.Result;
            _isSuccess = objPosting.IsSuccess;
            if (_isError == true)
            {
                switch (_errorNo)
                {
                    case 2601: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    case 2627: _errorMsg = ConfigurationManager.AppSettings["Duplicate"].ToString();
                        break;
                    default:
                        break;
                }
            }           
        }
        #endregion

     
        public void GetTimeAllTimeLogs(long EmployeeId, string CardDate, string time, DateTime CardDateTime, int AttendancesType, string Location)
        {
            try
            {
                objPosting.GetTimeAllTimeLogs(EmployeeId, CardDate, time, CardDateTime, AttendancesType, Location);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public DataTable GetDevicesIP(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetDevicesIP(CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        #region External
        public DataTable GetEmployeeList(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
             dtTable=objPosting.GetEmployeeList(PayPeriodCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _isSuccess = false;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        private DataTable GetPaySetup(int PayPeriodCategoryId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable=objPosting.GetPaySetup(PayPeriodCategoryId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _isSuccess = false;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEportExcelData(int PayPeriodId, string Status, string EncryptionKey)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetEportExcelData(PayPeriodId, Status, EncryptionKey);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _isSuccess = false;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeeNotPostedList(int PayPeriodCategoryId, int PayPeriodId, int CompanyId, int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetEmployeeNotPostedList(PayPeriodCategoryId, PayPeriodId, CompanyId, OrgStructureID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _isSuccess = false;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetEmployeePostedList(int PayPeriodCategoryId, int PayPeriodId, int CompanyId, int OrgStructureID)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objPosting.GetEmployeePostedList(PayPeriodCategoryId, PayPeriodId, CompanyId, OrgStructureID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _isSuccess = false;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void Post(int PayPeriodCategoryId, int PayPeriodId, long EmployeeId, string EncryptionKey)
        {
            bool Posted = true;
            _isMinusSalary = false;
            DataTable dtPaySetup = GetPaySetup(PayPeriodCategoryId);
            if (EmployeeId == 3408)
            {
                int a = 0;
            }
            if (dtPaySetup.Rows.Count <= 0)
            {
                _isError = true;
                _errorMsg = "Pay Setup Not Define";
                return;
            }
            for (int x = 0; x < dtPaySetup.Rows.Count; x++)
            {
                //if (IsDuplicate)
                //{
                //    continue;
                //}
                // if calculate from Function
                #region Function
                string funcName = dtPaySetup.Rows[x]["FunctionName"].ToString().ToUpper();
                string[] func = dtPaySetup.Rows[x]["FunctionName"].ToString().Split('(');
                if (func.Length > 1)
                {
                    funcName = func[0].ToString().ToUpper();
                }
                switch (funcName)
                {
                    case "BSAL":
                        objPosting.Bsal(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), EncryptionKey);
                        break;

                    case "NOPAY":
                        objPosting.NoPay(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), EncryptionKey);
                        break;

                    case "STAMPDUTY":
                        objPosting.Stamp(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), EncryptionKey);
                        break;

                    case "LOAN":
                        LoanDAL objLoan = new LoanDAL();
                        try
                        {
                            objLoan.SettleEmployeeLoan(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objLoan != null)
                            {
                                GC.SuppressFinalize(objLoan);
                            }
                        }
                        break;

                    case "SALADV":
                        SalaryAdvanceDAL objSalAdv = new SalaryAdvanceDAL();
                        try
                        {
                            objSalAdv.SettleSlaryAdvance(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objSalAdv != null)
                            {
                                GC.SuppressFinalize(objSalAdv);
                            }
                        }
                        break;

                    case "FESADV":
                        FestivalAdvanceDAL objFesAdv = new FestivalAdvanceDAL();
                        try
                        {
                            objFesAdv.SettleFestivalAdvance(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objFesAdv != null)
                            {
                                GC.SuppressFinalize(objFesAdv);
                            }
                        }
                        break;

                    case "STDORD":
                        StandingOrdersDAL objStdOrd = new StandingOrdersDAL();
                        try
                        {
                            objStdOrd.SettleStandingOrders(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objStdOrd != null)
                            {
                                GC.SuppressFinalize(objStdOrd);
                            }
                        }
                        break;

                    case "PAYETAX":
                        objPosting.PAYETAX(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), EncryptionKey);
                        break;

                    case "WELFARE":
                        objPosting.WELFARE(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), EncryptionKey);
                        break;

                    case "COINBF":
                        objPosting.CoinBF(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString());
                        break;

                    case "COINCF":
                        string[] paraList = dtPaySetup.Rows[x]["ParameterList"].ToString().Split(',');
                        if (paraList.Length > 1)
                        {
                            decimal returnValue = objPosting.GetPaySetupValue(EmployeeId, PayPeriodId, paraList[0], false);
                            if (returnValue < 0)
                            {
                                returnValue = 0;
                            }
                            objPosting.CoinCF(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), returnValue, Convert.ToInt32(paraList[1]), EncryptionKey);
                        }
                        break;

                    case "ROUNDING":
                        string[] parameterList = dtPaySetup.Rows[x]["ParameterList"].ToString().Split(',');
                        if (parameterList.Length > 1)
                        {
                            decimal returnValue = objPosting.GetPaySetupValue(EmployeeId, PayPeriodId, parameterList[0], false);
                            objPosting.Rounding(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), returnValue, Convert.ToInt32(parameterList[1]));
                        }
                        break;

                    case "GRATIVITYFUND":
                        objPosting.GrativityFund(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        break;
                    default:
                        break;
                }
                #endregion

                // if calculate from Formula
                #region Formula
                string formula = dtPaySetup.Rows[x]["Formula"].ToString();
                string paySetupCode = dtPaySetup.Rows[x]["PaySetupCode"].ToString().ToLower();
                if (dtPaySetup.Rows[x]["Formula"].ToString() != string.Empty)
                {
                    string OT = ConfigurationManager.AppSettings["OT"];
                    string Late = ConfigurationManager.AppSettings["LATE"];
                    bool Check = false;
                    if (formula == OT || formula == Late)
                    {
                        Check = true;
                    }
                    if (paySetupCode == "netsalary")
                    {
                        Console.Write("netsalary");
                    }
                    if ("TotalDeductions"== dtPaySetup.Rows[x]["PaySetupCode"].ToString())
                    {
                        int a = 0;
                    }
                    decimal amount = CalculateFormula(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["Formula"].ToString(), Check);
                    //if (amount < 0)
                    //{
                    //amount = Math.Abs(amount);
                    //}

                    if (paySetupCode == "netsalaryminus" && amount < 0)
                    {
                        _isMinusSalary = true;
                        return;
                    }
                    objPosting.SetPaySetupValue(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), amount, EncryptionKey);
                }
                #endregion
            }
            objPosting.PostUnpostRecord(PayPeriodId, EmployeeId, Posted);
        }

        private decimal CalculateFormula(long EmployeeId, int PayPeriodId, string Formula, bool Check)
        {
          
           
                if ((Formula == string.Empty) || Formula.TrimEnd() == "")
                {
                    return -99;
                }
                char[] CharacterArray = Formula.ToCharArray();
            decimal TotalPayCodeValue = 0M, PayCodeValue = 0M;
                StringBuilder sbFullString = new StringBuilder();
                StringBuilder sbPayCode = new StringBuilder();
                char previousOperator = char.MinValue, currentOperator = '0';



                StringBuilder builder = new StringBuilder(Formula);
                builder.Replace("(", "@(@");
                builder.Replace("+", "@+@");
                builder.Replace("-", "@-@");
                builder.Replace("/", "@/@");
                builder.Replace("*", "@*@");
                builder.Replace(")", "@)@");

                var elementArray = builder.ToString().Split(new char[] { '@' });


                for (int count = 0; count < elementArray.Length; count++)
                {
                    string currentItem = elementArray[count].ToString();

                    double Num;

                    if (currentItem.Contains("(") || currentItem.Contains("+") || currentItem.Contains("-") || currentItem.Contains("/") || currentItem.Contains("*") || currentItem.Contains(")"))
                    {
                        currentOperator = Convert.ToChar(currentItem);
                        PayCodeValue = 0;

                    }

                    else if (currentItem.Contains(".") && !double.TryParse(currentItem, out Num))
                    {
                        string[] tableField = currentItem.ToString().Split('.');
                        PayCodeValue = objPosting.GetPayTableFieldValue(EmployeeId, PayPeriodId, tableField[0], tableField[1], Check);
                        previousOperator = currentOperator;
                        currentOperator = '0';

                    }
                    else
                    {
                        PayCodeValue = objPosting.GetPaySetupValue(EmployeeId, PayPeriodId, currentItem.ToString(), Check);
                        previousOperator = currentOperator;
                        currentOperator = '0';
                    }

                    if (TotalPayCodeValue == 0)
                    {
                        if (PayCodeValue > 0 && (previousOperator == '0'))
                        {
                            TotalPayCodeValue = PayCodeValue;
                        }
                    }

                    switch (previousOperator)
                    {
                        case '+':
                            TotalPayCodeValue += PayCodeValue;
                            previousOperator = currentOperator;
                            PayCodeValue = 0;
                            break;

                        case '-':
                            TotalPayCodeValue -= PayCodeValue;
                            previousOperator = currentOperator;
                            PayCodeValue = 0;
                            break;

                        case '*':
                            TotalPayCodeValue *= PayCodeValue;
                            previousOperator = currentOperator;
                            PayCodeValue = 0;
                            break;

                        case '/':
                            TotalPayCodeValue /= PayCodeValue;
                            previousOperator = currentOperator;
                            PayCodeValue = 0;
                            break;

                    }
                }
          
           

            return TotalPayCodeValue;
        }


        public void UnPost(int PayPeriodCategoryId, int PayPeriodId, long EmployeeId, string EncryptionKey)
        {
            bool Posted = false;
            DataTable dtPaySetup = GetPaySetup(PayPeriodCategoryId);
            if (dtPaySetup.Rows.Count <= 0)
            {
                _isError = true;
                _errorMsg = "Pay Setup Not Define";
                return;
            }

            for (int x = 0; x < dtPaySetup.Rows.Count; x++)
            {
                // if calculate from Function
                #region Function
                string funcName = dtPaySetup.Rows[x]["FunctionName"].ToString().ToUpper();
                string[] func = dtPaySetup.Rows[x]["FunctionName"].ToString().Split('(');
                if (func.Length > 1)
                {
                    funcName = func[0].ToString().ToUpper();
                }
                switch (funcName)
                {
                    case "BSAL": objPosting.Bsal(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), EncryptionKey);
                        break;

                    case "NOPAY": objPosting.NoPay(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(),EncryptionKey);
                        break;

                    case "LOAN": LoanDAL objLoan = new LoanDAL();
                        try
                        {
                            objLoan.SettleEmployeeLoan(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objLoan != null)
                            {
                                GC.SuppressFinalize(objLoan);
                            }
                        }
                        break;

                    case "SALADV": SalaryAdvanceDAL objSalAdv = new SalaryAdvanceDAL();
                        try
                        {
                            objSalAdv.SettleSlaryAdvance(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objSalAdv != null)
                            {
                                GC.SuppressFinalize(objSalAdv);
                            }
                        }
                        break;

                    case "FESADV": FestivalAdvanceDAL objFesAdv = new FestivalAdvanceDAL();
                        try
                        {
                            objFesAdv.SettleFestivalAdvance(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objFesAdv != null)
                            {
                                GC.SuppressFinalize(objFesAdv);
                            }
                        }
                        break;

                    case "STDORD": StandingOrdersDAL objStdOrd = new StandingOrdersDAL();
                        try
                        {
                            objStdOrd.SettleStandingOrders(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _errorMsg = ex.Message;
                        }
                        finally
                        {
                            if (objStdOrd != null)
                            {
                                GC.SuppressFinalize(objStdOrd);
                            }
                        }
                        break;

                    case "PAYETAX": objPosting.PAYETAX(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), EncryptionKey);
                        break;

                    case "COINBF": objPosting.CoinBF(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString());
                        break;

                    case "COINCF": string[] paraList = dtPaySetup.Rows[x]["ParameterList"].ToString().Split(',');
                        if (paraList.Length > 1)
                        {
                            decimal returnValue = objPosting.GetPaySetupValue(EmployeeId, PayPeriodId, paraList[0],false);
                            objPosting.CoinCF(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), returnValue, Convert.ToInt32(paraList[1]), EncryptionKey);
                        }
                        break;

                    case "ROUNDING": string[] parameterList = dtPaySetup.Rows[x]["ParameterList"].ToString().Split(',');
                        if (parameterList.Length > 1)
                        {
                            decimal returnValue = objPosting.GetPaySetupValue(EmployeeId, PayPeriodId, parameterList[0],false);
                            objPosting.Rounding(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), returnValue, Convert.ToInt32(parameterList[1]));
                        }
                        break;

                    case "GRATIVITYFUND": objPosting.GrativityFund(EmployeeId, PayPeriodId, dtPaySetup.Rows[x]["PaySetupCode"].ToString(), Posted);
                        break;
                    default:
                        break;
                }
                #endregion
            }
            objPosting.PostUnpostRecord(PayPeriodId, EmployeeId, Posted);
        }
        #endregion
        #endregion

        #region Destructor
        ~Posting()
        {           
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
