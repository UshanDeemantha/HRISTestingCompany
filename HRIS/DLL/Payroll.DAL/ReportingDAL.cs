/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, No. 51, Flower Road, Colombo 7.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name       : HRM.Payroll.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Asanka C. Amarasinghe
/// Created Timestamp   : 08-August-2011
/// Class Description   : HRM.Payroll.DAL.ReportingDAL
/// Task Code           : --

namespace HRM.Payroll.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;


    public class ReportingDAL
    {
        #region Fields

        private SqlConnection dbConnection;

        private bool _isError;
        private string _errorMsg;
        private int _errorNo;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingDAL"/> class.
        /// </summary>
        public ReportingDAL()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        #endregion

        #region Properties

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

        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }

        public DataTable GenerateSelectedEmployeeReportRecords(string computerName, string userName, string organizationSelectCriteria, string designationSelectCriteria)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("Pay_GenerateSelectedEmployeeReportRecords", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@machineName", computerName);
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@organizationSelectCriteria", organizationSelectCriteria);
                    command.Parameters.AddWithValue("@designationSelectCriteria", designationSelectCriteria);

                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
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
                dbConnection.Close();
            }

            return dtTable;
        }

        public DataTable GetUnitWiseSum(int CompanyId, string EncriptionKey, int Year, int Month, string CatId, int LevelIndex)
        {
            DataTable dtTable = new DataTable();
            DataTable dtPayCodes = new DataTable();
            DataTable dtDevisions = new DataTable();
            int zeroCount = 0;

            try
            {
                using (SqlCommand command = new SqlCommand("GetPayCodes", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtPayCodes);
                    }
                }

                using (SqlCommand command = new SqlCommand("GetDevisions", dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@LevelIndex", LevelIndex);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtDevisions);
                    }
                }

                for (int c = 0; c < dtPayCodes.Rows.Count; c++)
                {
                    if (!dtTable.Columns.Contains("Description"))
                    {
                        dtTable.Columns.Add("Description", typeof(string));
                    }
                    if (!dtTable.Columns.Contains("PayCategoryName"))
                    {
                        dtTable.Columns.Add("PayCategoryName", typeof(string));
                    }
                    if (!dtTable.Columns.Contains("Year"))
                    {
                        dtTable.Columns.Add("Year", typeof(string));
                    }
                    if (!dtTable.Columns.Contains("PayPeriodName"))
                    {
                        dtTable.Columns.Add("PayPeriodName", typeof(string));
                    }

                    zeroCount = 0;
                    DataRow dtrow = dtTable.NewRow();

                    string payCode = dtPayCodes.Rows[c]["PaySetUpCode"].ToString();
                    string payCodeDes = dtPayCodes.Rows[c]["Description"].ToString();

                    dtrow["Description"] = payCodeDes;

                    int divisionCount = 0;
                    int divisionTotalCount = 0;
                    decimal divisionTotalSum = 0;
                    for (int d = 0; d < dtDevisions.Rows.Count; d++)
                    {
                        divisionCount++;
                        string devisionName = dtDevisions.Rows[d]["DivisionName"].ToString();
                        int devisionId = Convert.ToInt32(dtDevisions.Rows[d]["OrgStructureID"].ToString());

                        DataTable dtUnites = new DataTable();

                        using (SqlCommand command = new SqlCommand("GetUnites", dbConnection))
                        {
                            OpenDB();
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CompanyId", CompanyId);
                            command.Parameters.AddWithValue("@LevelIndex", LevelIndex);
                            command.Parameters.AddWithValue("@DivisionId", devisionId);
                            using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                            {
                                daAdapter.Fill(dtUnites);
                            }
                        }

                        int uCount = 0;
                        int countSum = 0;
                        decimal amountSum = 0;
                        for (int u = 0; u < dtUnites.Rows.Count; u++)
                        {
                            string unit = dtUnites.Rows[u]["Unit"].ToString();
                            int unitOrgId = Convert.ToInt32(dtUnites.Rows[u]["UnitOrgStrucId"].ToString());

                            DataTable dtUnitAmount = new DataTable();

                            if (!dtTable.Columns.Contains(unit))
                            {
                                dtTable.Columns.Add(unit, typeof(decimal));
                                dtTable.Columns.Add(unit + "Count", typeof(int));
                            }

                            uCount++;

                            if (uCount == dtUnites.Rows.Count && !dtTable.Columns.Contains(devisionName + "TotalAmount"))
                            {
                                dtTable.Columns.Add(devisionName + "TotalAmount", typeof(decimal));
                            }

                            if (uCount == dtUnites.Rows.Count && !dtTable.Columns.Contains(devisionName + "TotalCount"))
                            {
                                dtTable.Columns.Add(devisionName + "TotalCount", typeof(int));
                            }

                            using (SqlCommand command = new SqlCommand("GetUniteWiseAmount", dbConnection))
                            {
                                OpenDB();
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Month", Month);
                                command.Parameters.AddWithValue("@Year", Year);
                                command.Parameters.AddWithValue("@CatID", CatId);
                                command.Parameters.AddWithValue("@PaySetupCode", payCode);
                                command.Parameters.AddWithValue("@OrgStructureID", unitOrgId);
                                command.Parameters.AddWithValue("@CompanyId", CompanyId);
                                command.Parameters.AddWithValue("@EncryptKey", EncriptionKey);
                                using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                                {
                                    daAdapter.Fill(dtUnitAmount);
                                }
                            }

                            if (dtUnitAmount.Rows.Count > 0)
                            {
                                dtrow[unit] = dtUnitAmount.Rows[0]["UnitSum"].ToString();
                                dtrow[unit + "Count"] = dtUnitAmount.Rows[0]["UnitCount"].ToString();
                                divisionTotalSum = divisionTotalSum + Convert.ToDecimal(dtUnitAmount.Rows[0]["UnitSum"].ToString());
                                divisionTotalCount = divisionTotalCount + Convert.ToInt32(dtUnitAmount.Rows[0]["UnitCount"].ToString());
                            }
                            else
                            {
                                dtrow[unit] = 0;
                                dtrow[unit + "Count"] = 0;
                            }

                            dtrow["PayCategoryName"] = dtUnitAmount.Rows[0]["PayCategoryName"].ToString();
                            dtrow["Year"] = dtUnitAmount.Rows[0]["Year"].ToString();
                            dtrow["PayPeriodName"] = dtUnitAmount.Rows[0]["PayPeriodName"].ToString();

                            amountSum = amountSum + Convert.ToDecimal(dtUnitAmount.Rows[0]["UnitSum"].ToString());
                            countSum = countSum + Convert.ToInt32(dtUnitAmount.Rows[0]["UnitCount"].ToString());

                            if (uCount == dtUnites.Rows.Count)
                            {
                                if (amountSum == 0)
                                {
                                    zeroCount++;
                                }
                                dtrow[devisionName + "TotalAmount"] = amountSum;
                                dtrow[devisionName + "TotalCount"] = countSum;
                            }



                        }

                        if (divisionCount == dtDevisions.Rows.Count)
                        {
                            if(LevelIndex==1)
                            {
                                if (uCount == dtUnites.Rows.Count && !dtTable.Columns.Contains(devisionName + "TotalAmount"))
                                {
                                    dtTable.Columns.Add(devisionName + "TotalAmount", typeof(decimal));
                                }

                                if (uCount == dtUnites.Rows.Count && !dtTable.Columns.Contains(devisionName + "TotalCount"))
                                {
                                    dtTable.Columns.Add(devisionName + "TotalCount", typeof(int));
                                }

                                if (!dtTable.Columns.Contains("GrandSum"))
                                {
                                    dtTable.Columns.Add("GrandSum", typeof(decimal));
                                }
                                if (!dtTable.Columns.Contains("GrandCount"))
                                {
                                    dtTable.Columns.Add("GrandCount", typeof(int));
                                }
                                if (!dtTable.Columns.Contains("CompanyID"))
                                {
                                    dtTable.Columns.Add("CompanyID", typeof(int));
                                }

                                DataTable dtUnitAmount = new DataTable();
                                using (SqlCommand command = new SqlCommand("GetUniteWiseAmount", dbConnection))
                                {
                                    OpenDB();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@Month", Month);
                                    command.Parameters.AddWithValue("@Year", Year);
                                    command.Parameters.AddWithValue("@CatID", CatId);
                                    command.Parameters.AddWithValue("@PaySetupCode", payCode);
                                    command.Parameters.AddWithValue("@OrgStructureID", devisionId);
                                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                                    command.Parameters.AddWithValue("@EncryptKey", EncriptionKey);
                                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                                    {
                                        daAdapter.Fill(dtUnitAmount);
                                    }
                                }

                                if (dtUnitAmount.Rows.Count > 0)
                                {
                                    dtrow[devisionName + "TotalAmount"] = dtUnitAmount.Rows[0]["UnitSum"].ToString();
                                    dtrow[devisionName + "TotalCount"] = dtUnitAmount.Rows[0]["UnitCount"].ToString();
                                    dtrow["GrandSum"] = dtUnitAmount.Rows[0]["UnitSum"].ToString(); ;
                                    dtrow["GrandCount"] = dtUnitAmount.Rows[0]["UnitCount"].ToString();
                                    dtrow["PayCategoryName"] = dtUnitAmount.Rows[0]["PayCategoryName"].ToString();
                                    dtrow["Year"] = dtUnitAmount.Rows[0]["Year"].ToString();
                                    dtrow["PayPeriodName"] = dtUnitAmount.Rows[0]["PayPeriodName"].ToString();
                                    dtrow["CompanyID"] = CompanyId;

                                    if (Convert.ToDecimal(dtUnitAmount.Rows[0]["UnitSum"].ToString()) == 0)
                                    {
                                        zeroCount++;
                                    }
                                }
                            }
                            else if (LevelIndex > 1)
                            {
                                if (!dtTable.Columns.Contains("GrandSum"))
                                {
                                    dtTable.Columns.Add("GrandSum", typeof(decimal));
                                }
                                if (!dtTable.Columns.Contains("GrandCount"))
                                {
                                    dtTable.Columns.Add("GrandCount", typeof(int));
                                }
                                if (!dtTable.Columns.Contains("CompanyID"))
                                {
                                    dtTable.Columns.Add("CompanyID", typeof(int));
                                }

                                dtrow["GrandSum"] = divisionTotalSum;
                                dtrow["GrandCount"] = divisionTotalCount;
                                dtrow["CompanyID"] = CompanyId;
                            }
                        }
                    }

                    //if (zeroCount != dtDevisions.Rows.Count)
                    //{
                    //    dtTable.Rows.Add(dtrow);
                    //}
                    dtTable.Rows.Add(dtrow);
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }
            return dtTable;
        }

        #endregion

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FestivalAdvanceDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~ReportingDAL()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
