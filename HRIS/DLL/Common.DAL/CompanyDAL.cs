
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace HRM.Common.DAL
{

    public class CompanyDAL
    {
        # region Fields
            private SqlConnection _dbConnection;
            private bool  _isError;
            private string _errorMsg;

            private int _companyId;
            private string _companyCode;
            private string _companyName;
            private string _companyAddress;
            private string _companyContactNo1;
            private string _companyContactNo2;
            private string _companyFax;
            private string _companyEmail;
            private string _companyWeb;
            private string _companyRegistrationNo;
            private string _companyTaxRegistrationNo;
        # endregion

        #region Properties

            /// <summary>
            /// Gets the is error.
            /// </summary>
            /// <value>The is error.</value>
            public bool  IsError
            { 
                get { return _isError; } 
            }

            /// <summary>
            /// Gets the error MSG.
            /// </summary>
            /// <value>The error MSG.</value>
            public string  ErrorMsg
            {
                get { return _errorMsg; } 
            }

            /// <summary>
            /// Gets the company id.
            /// </summary>
            /// <value>The company id.</value>
            public int CompanyId
            {
                get { return _companyId; }
            }

            /// <summary>
            /// Gets or sets the company code.
            /// </summary>
            /// <value>The company code.</value>
            public string  CompanyCode 
            {
                get { return _companyCode; }
                set { _companyCode = value; }
            }

            /// <summary>
            /// Gets or sets the name of the company.
            /// </summary>
            /// <value>The name of the company.</value>
            public string  CompanyName
            {
                get { return _companyName; }
                set { _companyName = value; }
            }

            /// <summary>
            /// Gets or sets the company address.
            /// </summary>
            /// <value>The company address.</value>
            public string  CompanyAddress
            {
                get { return _companyContactNo1; }
                set { _companyContactNo1 = value; }
            }

            /// <summary>
            /// Gets or sets the company contact no1.
            /// </summary>
            /// <value>The company contact no1.</value>
            public string  CompanyContactNo1
            {
                get { return _companyContactNo1; }
                set { _companyContactNo1 = value; }
            }

            /// <summary>
            /// Gets or sets the company contact no2.
            /// </summary>
            /// <value>The company contact no2.</value>
            public string  CompanyContactNo2
            {
                get { return _companyContactNo2; }
                set { _companyContactNo2 = value; }
            }

            /// <summary>
            /// Gets or sets the company fax.
            /// </summary>
            /// <value>The company fax.</value>
            public string  CompanyFax
            {
                get { return _companyFax; }
                set { _companyFax = value; }
            }

            /// <summary>
            /// Gets or sets the company email.
            /// </summary>
            /// <value>The company email.</value>
            public string  CompanyEmail
            {
                get { return _companyEmail; }
                set { _companyEmail = value; }
            }

            /// <summary>
            /// Gets or sets the company web.
            /// </summary>
            /// <value>The company web.</value>
            public string CompanyWeb 
            {
                get { return _companyWeb; }
                set { _companyWeb = value; }
            }

            /// <summary>
            /// Gets or sets the company registration no.
            /// </summary>
            /// <value>The company registration no.</value>
            public string CompanyRegistrationNo
            {
                get { return _companyRegistrationNo; }
                set { _companyRegistrationNo = value; }
            }

            /// <summary>
            /// Gets or sets the company tax registration no.
            /// </summary>
            /// <value>The company tax registration no.</value>
            public string  CompanyTaxRegistrationNo
            {
                get { return _companyTaxRegistrationNo; }
                set { _companyTaxRegistrationNo = value; }
            }
        # endregion

        #region Constructor
            /// <summary>
            /// Initializes a new instance of the <see cref="CompanyDAL"/> class.
            /// </summary>
            public CompanyDAL()
            {
                _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }
        #endregion

        #region Methods

            /// <summary>
            /// Opens the DB.
            /// </summary>
            public void OpenDB()
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection.Open();
                }
            }

            /// <summary>
            /// Addcompanies the specified company code.
            /// </summary>
            /// <param name="companyCode">The company code.</param>
            /// <param name="companyName">Name of the company.</param>
            /// <param name="companyAddress">The company address.</param>
            /// <param name="companyContactNo1">The company contact no1.</param>
            /// <param name="companyContactNo2">The company contact no2.</param>
            /// <param name="companyFax">The company fax.</param>
            /// <param name="companyEmail">The company email.</param>
            /// <param name="companyWeb">The company web.</param>
            /// <param name="companyRegistrationNo">The company registration no.</param>
            /// <param name="companyTaxRegistrationNo">The company tax registration no.</param>
            public void AddCompany(string companyCode, string companyName, string companyAddress, string companyContactNo1, 
                                   string companyContactNo2, string companyFax, string companyEmail, string companyWeb,
                                   string companyRegistrationNo, string companyTaxRegistrationNo)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_AddCOmpany", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyCode", companyCode);
                        command.Parameters.AddWithValue("@CompanyName", companyName);
                        command.Parameters.AddWithValue("@CompanyAddress", companyAddress);
                        command.Parameters.AddWithValue("@CompanyContactNo1", companyContactNo1);
                        command.Parameters.AddWithValue("@CompanyContactNo2", companyContactNo2);
                        command.Parameters.AddWithValue("@CompanyFax", companyFax);
                        command.Parameters.AddWithValue("@CompanyEmail", companyEmail);
                        command.Parameters.AddWithValue("@CompanyWeb", companyWeb);
                        command.Parameters.AddWithValue("@CompanyRegistrationNo", companyRegistrationNo);
                        command.Parameters.AddWithValue("@CompanyTaxRegistrationNo", companyTaxRegistrationNo);

                        SqlParameter CompanyId = new SqlParameter("@CompanyID", SqlDbType.Int);
                        CompanyId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(CompanyId);

                        command.ExecuteNonQuery();
                        _companyId = Convert.ToInt32(CompanyId.Value);
                    }

                }
                catch (Exception ex)
                {

                    _isError = true;
                    _errorMsg = ex.Message;
                }
                finally 
                {
                    _dbConnection.Close();
                }
            }

            /// <summary>
            /// Updates the company.
            /// </summary>
            /// <param name="companyId">The company id.</param>
            /// <param name="companyCode">The company code.</param>
            /// <param name="companyName">Name of the company.</param>
            /// <param name="companyAddress">The company address.</param>
            /// <param name="companyContactNo1">The company contact no1.</param>
            /// <param name="companyContactNo2">The company contact no2.</param>
            /// <param name="companyFax">The company fax.</param>
            /// <param name="companyEmail">The company email.</param>
            /// <param name="companyWeb">The company web.</param>
            /// <param name="companyRegistrationNo">The company registration no.</param>
            /// <param name="companyTaxRegistrationNo">The company tax registration no.</param>
            public void UpdateCompany(int companyId, string companyCode, string companyName, string companyAddress, 
                                      string companyContactNo1,string companyContactNo2, string companyFax, string companyEmail, 
                                      string companyWeb,string companyRegistrationNo, string companyTaxRegistrationNo)
            {
                try
                {
                    using(SqlCommand command = new SqlCommand("COM_UpdateCompany",_dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@CompanyID", companyId);
                        command.Parameters.AddWithValue("@CompanyCode", companyCode);
                        command.Parameters.AddWithValue("@CompanyName", companyName);
                        command.Parameters.AddWithValue("@CompanyAddress", companyAddress);
                        command.Parameters.AddWithValue("@CompanyContactNo1", companyContactNo1);
                        command.Parameters.AddWithValue("@CompanyContactNo2", companyContactNo2);
                        command.Parameters.AddWithValue("@CompanyFax", companyFax);
                        command.Parameters.AddWithValue("@CompanyEmail", companyEmail);
                        command.Parameters.AddWithValue("@CompanyWeb", companyWeb);
                        command.Parameters.AddWithValue("@CompanyRegistrationNo", companyRegistrationNo);
                        command.Parameters.AddWithValue("@CompanyTaxRegistrationNo", companyTaxRegistrationNo);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
                }
                finally
                {
                    _dbConnection.Close();
                }
            }

            /// <summary>
            /// Gets the compnay.
            /// </summary>
            /// <param name="companyId">The company id.</param>
            /// <returns></returns>
            public DataTable GetCompany(int companyId)
            {
                using (DataTable dtTable = new DataTable())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("COM_GetCompany", _dbConnection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OpenDB();
                            command.Parameters.AddWithValue("@CompanyID", companyId);
                            //command.Parameters.Add("@CompanyID", companyId);
                            using (SqlDataAdapter daAdaper = new SqlDataAdapter(command))
                            {
                                daAdaper.Fill(dtTable);
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
                        _dbConnection.Close();
                    }
                    return dtTable;
                }
            }

            #region Inactive Employee Status

            public DataTable GetInactiveEmployeeStatus(int StatusId)
            {
                using (DataTable dtTable = new DataTable())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("COM_GetInactieEmployeeStatus", _dbConnection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OpenDB();
                            command.Parameters.AddWithValue("@StatusId", StatusId);
                            //command.Parameters.Add("@CompanyID", companyId);
                            using (SqlDataAdapter daAdaper = new SqlDataAdapter(command))
                            {
                                daAdaper.Fill(dtTable);
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
                        _dbConnection.Close();
                    }
                    return dtTable;
                }
            }

            #endregion



        #endregion

            #region Destructor
            ~CompanyDAL()
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
                GC.SuppressFinalize(this);
            }
        #endregion

    }
}
