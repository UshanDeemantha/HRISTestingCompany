
namespace HRM.Common.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class MksSecurityDAL
    {
        #region Complex Data Types

        public class OrgStructureToSave
        {
            public int OrgStructureID { get; set; }
            public int ParentID { get; set; }
        }

        public class DesigStructureToSave
        {
            public int DesignationStuctureID { get; set; }
            public int ParentID { get; set; }
        }

        #endregion

        #region Fields

        private SqlConnection dbConnection;
        private string _errorMsg = string.Empty;
        private int _errorNo = 0;
        private bool _isError = false;
        private string _userId;
        private int _usertypeId = 0;
        private bool _isSuccess = false;
        private string _result = string.Empty;
        private string _companyName = string.Empty;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        public string UserId
        {
            get { return _userId; }
        }

        public int UserTypeId
        {
            get { return _usertypeId; }
            set { _usertypeId = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess
        {
            get { return _isSuccess; }
        }

        public string Result
        {
            get { return _result; }
        }

        public string CompanyName
        {
            get { return _companyName; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyDAL"/> class.
        /// </summary>
        public MksSecurityDAL()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods

        #region Internal

        /// <summary>
        /// Opens the db.
        /// </summary>
        private void OpenDb()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            InitializeFields();
        }

        private void InitializeFields()
        {
            _isError = false;
            _isSuccess = false;
            _errorMsg = string.Empty;
            _errorNo = 0;
            _result = string.Empty;
        }

        private void SetError(SqlException Ex)
        {
            _isSuccess = false;
            _isError = true;
            _errorMsg = Ex.Message;
            _result = string.Empty;

            switch (Ex.Number)
            {
                case 2601:
                    _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 2627:
                    _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 547:
                    _errorMsg = "Can not Delete. Alredy Assign!";
                    break;
                default: break;
            }
        }
        #endregion

        #region External

        public DataTable IsExistsCompany(string UserId, int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("MKS_Com_ExistsUserCompany", dbConnection))
                    {
                        OpenDb();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", UserId);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                        {
                            daAdpter.Fill(dtTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _isError = true;
                    _errorMsg = ex.Message;
                }
                return dtTable;
            }
        }

        #region Password Reset

        public string GetEmailForUserName(string userName)
        {
            string userEmail = string.Empty;

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_GetEmailForUserName", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);

                    userEmail = command.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                userEmail = string.Empty;

                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return userEmail;
        }

        public string GetUserIdForUserName(string userName)
        {
            string ReturnStatus = string.Empty;

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_GetUserIdForUserName", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);
                    ReturnStatus = command.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                ReturnStatus = string.Empty;

                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return ReturnStatus;
        }

        public bool CheckForUserCompanyAccessRights(string userID, int companyID)
        {
            bool ReturnStatus = false;

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_CheckForUserCompanyAccessRights", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CompanyID", companyID);

                    ReturnStatus = Convert.ToBoolean(command.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                ReturnStatus = false;

                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return ReturnStatus;
        }

        public DataTable GetLastPasswordRequest(string userID, int companyID)
        {
            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_GetPreviousPasswordRequest", dbConnection))
                {
                    OpenDb();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userID);
                    command.Parameters.AddWithValue("@CompanyID", companyID);

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
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

        public DataTable GetLastPasswordRequestFromCheckSum(string userName, string checksum, int companyID)
        {
            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_GetPreviousPasswordRequestFromCheckSum", dbConnection))
                {
                    OpenDb();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@ChecksumID", checksum);
                    command.Parameters.AddWithValue("@CompanyID", companyID);

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
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

        public Int64 AddPasswordResetRequest(string userID, int companyID, string encryptionKey, DateTime submittedDateTime, string checksumID, string requestByClientID)
        {
            Int64 createdIndexID = 0;

            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddPasswordResetRequest", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CompanyID", companyID);
                    command.Parameters.AddWithValue("@EncryptionKey", encryptionKey);
                    command.Parameters.AddWithValue("@SubmittedDateTime", submittedDateTime);
                    command.Parameters.AddWithValue("@ChecksumID", checksumID);
                    command.Parameters.AddWithValue("@RequestByClientID", requestByClientID);

                    createdIndexID = Convert.ToInt64(command.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                createdIndexID = -1;

                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return createdIndexID;
        }

        public bool ChangeUserPasswordWithoutConfirmation(string userName, string userID, string newPassword)
        {
            bool isPasswordChangeSuccess = false;

            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("MKS_COM_ChangeUserPasswordWithoutConfirmation", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@UserId", userID);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);

                    isPasswordChangeSuccess = Convert.ToBoolean(command.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                isPasswordChangeSuccess = false;

                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return isPasswordChangeSuccess;
        }

        #endregion

        #region Level Wise Rights Assignment

        public DataTable SelectUsersToAssignRights(string currentUserName)
        {
            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_SelectUsersToAssignRights", dbConnection))
                {
                    OpenDb();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CurrentUserName", currentUserName);

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
                    }
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public DataTable GetUserLevelOrganizationalStucture(int orgStructureID, string userID, string applicationID, int companyId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_GetUserLevelOrganizationalStucture", dbConnection))
                {
                    OpenDb();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrgStructureID", orgStructureID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ApplicationID", applicationID);
                    command.Parameters.AddWithValue("@CompanyId", companyId);

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
                    }
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public DataTable GetUserLevelDesignationStucture(int desigStructureID, string userID, string applicationID)
        {
            DataTable dtTable = new DataTable();

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_GetUserLevelDesignationStucture", dbConnection))
                {
                    OpenDb();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DesignationStuctureID", desigStructureID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ApplicationID", applicationID);

                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
                    }
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public int SaveDesignationAndOrganizationRights(string userID, string appID, List<OrgStructureToSave> orgStructure, List<DesigStructureToSave> desigStructure, string createdUser, int companyId)
        {
            int ReturnStatus = -1;

            try
            {
                OpenDb();

                foreach (OrgStructureToSave org in orgStructure)
                {
                    using (SqlCommand command = new SqlCommand("MKS_Com_SaveUserLevelOrganizationalStucture", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@ApplicationID", appID);
                        command.Parameters.AddWithValue("@CreatedByUser", createdUser);
                        command.Parameters.AddWithValue("@OrgStructureID", org.OrgStructureID);
                        command.Parameters.AddWithValue("@ParentID", org.ParentID);
                        command.Parameters.AddWithValue("@CompanyId", companyId);

                        command.ExecuteNonQuery();
                    }
                }

                foreach (DesigStructureToSave desig in desigStructure)
                {
                    using (SqlCommand command = new SqlCommand("MKS_Com_SaveUserLevelDesignationStucture", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@ApplicationID", appID);
                        command.Parameters.AddWithValue("@CompanyId", companyId);
                        command.Parameters.AddWithValue("@DesignationStuctureID", desig.DesignationStuctureID);
                        command.Parameters.AddWithValue("@ParentID", desig.ParentID);
                        command.Parameters.AddWithValue("@CreatedByUser", createdUser);

                        command.ExecuteNonQuery();
                    }
                }

                ReturnStatus = 1;
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

            return ReturnStatus;
        }


        public bool AllowNewLevelRightsSave(string userID, string appID, int companyId)
        {
            bool ReturnStatus = false;

            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("MKS_Com_AllowNewLevelRightsSave", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ApplicationID", appID);
                    command.Parameters.AddWithValue("@CompanyId", companyId);

                    ReturnStatus = Convert.ToBoolean(command.ExecuteScalar());
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

            return ReturnStatus;
        }

        public int DeleteUserLevelRights(string userID, string appID)
        {
            int ReturnStatus = -1;

            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("MKS_Com_DeleteUserLevelRights", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ApplicationID", appID);

                    ReturnStatus = Convert.ToInt32(command.ExecuteScalar());
                }

                ReturnStatus = 1;
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

            return ReturnStatus;
        }

        #endregion

        #region Application

        public DataTable GetApplication(string ApplicationId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_GetApplication", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return dtTable;
        }

        public void AddApplication(string ApplicationCode, string ApplicationName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_AddApplication", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
                    command.Parameters.AddWithValue("@ApplicationName", ApplicationName);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);
                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void SaveAccountDetails(int EmployeeId, int BankId, int BranchId, string AccountNo, string NameasperBank, string Percentage)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_SaveAccountDetails", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@AccountNo", AccountNo);
                    command.Parameters.AddWithValue("@NameasperBank", NameasperBank);
                    command.Parameters.AddWithValue("@Percentage", Percentage);
                    //SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    //resultPara.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(resultPara);
                    command.ExecuteNonQuery();
                    _isSuccess = true;
                   // _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void UpdateAccountDetails(int EmployeeId, int BankId, int BranchId, string AccountNo, string NameasperBank, string Percentage)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Pay_UpdateAccountDetails", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@BankId", BankId);
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@AccountNo", AccountNo);
                    command.Parameters.AddWithValue("@NameasperBank", NameasperBank);
                    command.Parameters.AddWithValue("@Percentage", Percentage);
                    //SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    //resultPara.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(resultPara);
                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    // _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void UpdateApplication(string ApplicationId, string ApplicationCode, string ApplicationName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_UpdateApplication", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
                    command.Parameters.AddWithValue("@ApplicationName", ApplicationName);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void DeleteApplication(string ApplicationId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_DeleteApplication", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationId", ApplicationId);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        #endregion

        #region Menu
        public DataTable GetMenu(string ApplicationId, string MenuId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_GetMenu", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    command.Parameters.AddWithValue("@MenuId", MenuId);
                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return dtTable;
        }

        public void AddMenu(string ApplicationId, string MenuName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_AddMenu", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    command.Parameters.AddWithValue("@MenuName", MenuName);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void UpdateMenu(string MenuId, string ApplicationId, string MenuName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_UpdateMenu", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MenuId", MenuId);
                    command.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    command.Parameters.AddWithValue("@MenuName", MenuName);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void DeleteMenu(string MenuId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_DeleteMenu", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MenuId", MenuId);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
        #endregion

        #region Form
        public DataTable GetForm(string MenuId, string FormId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_GetForm", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MenuId", MenuId);
                    command.Parameters.AddWithValue("@FormId", FormId);
                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return dtTable;
        }

        public void AddForm(string MenuId, string FormName, string FormDescription, int FormIndexNo)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_AddForm", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MenuId", MenuId);
                    command.Parameters.AddWithValue("@FormName", FormName);
                    command.Parameters.AddWithValue("@FormDescription", FormDescription);
                    command.Parameters.AddWithValue("@FormIndexNo", FormIndexNo);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void UpdateForm(string FormId, string FormName, string FormDescription, int FormIndexNo)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_UpdateForm", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FormId", FormId);
                    command.Parameters.AddWithValue("@FormName", FormName);
                    command.Parameters.AddWithValue("@FormDescription", FormDescription);
                    command.Parameters.AddWithValue("@FormIndexNo", FormIndexNo);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void DeleteForm(string FormId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_DeleteForm", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FormId", FormId);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
        #endregion

        #region Permission
        public DataTable GetPermission(string PermissionId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_GetPermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PermissionId", PermissionId);
                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                    {
                        daAdpter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return dtTable;
        }

        public void AddPermission(string Permission, int PermissionOrderNo, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_AddPermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@PermissionOrderNo", PermissionOrderNo);
                    command.Parameters.AddWithValue("@Remarks", Remarks);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void UpdatePermission(string PermissionId, string Permission, int PermissionOrderNo, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_UpdatePermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PermissionId", PermissionId);
                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@PermissionOrderNo", PermissionOrderNo);
                    command.Parameters.AddWithValue("@Remarks", Remarks);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void DeletePermission(string PermissionId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_DeletePermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PermissionId", PermissionId);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
        #endregion

        #region User Permissions
        public void AddUserPermission(string UserId, string PermissionId, string FormId, bool Active)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_AddUserPermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@PermissionId", PermissionId);
                    command.Parameters.AddWithValue("@FormId", FormId);
                    command.Parameters.AddWithValue("@Active", Active);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
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
        }
        //public DataTable ViewUserRights1(string UserId, string ApplicationId, string MenuId)
        //{

        //    DataTable dtPermission = new DataTable();
        //    DataTable dtForms = new DataTable();
        //    try
        //    {

        //        dtPermission.Columns.Add("UserName", typeof(string));
        //        dtPermission.Columns.Add("ApplicationName", typeof(string));
        //        dtPermission.Columns.Add("MenuName", typeof(string));
        //        dtPermission.Columns.Add("FormId", typeof(string));
        //        dtPermission.Columns.Add("FormName", typeof(string));
        //        dtPermission.Columns.Add("FormDescription", typeof(string));
        //        dtPermission.Columns.Add("None", typeof(bool));
        //        dtPermission.Columns.Add("ViewOnly", typeof(bool));
        //        dtPermission.Columns.Add("AddOnly", typeof(bool));
        //        dtPermission.Columns.Add("EditOnly", typeof(bool));
        //        dtPermission.Columns.Add("DeleteOnly", typeof(bool));
        //        dtPermission.Columns.Add("FullControl", typeof(bool));

        //        string[] ApplicationNumber = ApplicationId.Split(',');

        //        int appCount = ApplicationNumber.Length;
        //        if (appCount > 0)
        //        {

        //            for (int a = 0; a < appCount; a++)
        //            {
        //                string appID = ApplicationNumber[a].ToString();
        //                using (SqlCommand command = new SqlCommand("MKS_Com_GetAllFormsUserwiseSecond", dbConnection))
        //                {
        //                    OpenDb();
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.Parameters.AddWithValue("@ApplicationId", 0);
        //                    command.Parameters.AddWithValue("@UserId", 0);

        //                    using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
        //                    {
        //                        daAdpter.Fill(dtForms);
        //                    }
        //                    for (int c = 0; c < dtForms.Rows.Count; c++)
        //                    {
        //                        DataRow dtrow = dtPermission.NewRow();
        //                        dtrow["UserName"] = Convert.ToString(dtForms.Rows[c]["UserName"]);
        //                        dtrow["ApplicationName"] = Convert.ToString(dtForms.Rows[c]["ApplicationName"]);
        //                        dtrow["MenuName"] = Convert.ToString(dtForms.Rows[c]["MenuName"]);
        //                        dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
        //                        dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
        //                        dtrow["FormDescription"] = Convert.ToString(dtForms.Rows[c]["FormDescription"]);
        //                        dtrow["FormId"] = Convert.ToString(dtForms.Rows[c]["FormId"]);
        //                        string fromId = Convert.ToString(dtForms.Rows[c]["FormId"]);

        //                        DataTable dtFormPermission = new DataTable();
        //                        using (SqlCommand commandPermmision = new SqlCommand("COM_GetUserPermissionSecond", dbConnection))
        //                        {

        //                            commandPermmision.CommandType = CommandType.StoredProcedure;

        //                            using (SqlDataAdapter daAdpter = new SqlDataAdapter(commandPermmision))
        //                            {
        //                                daAdpter.Fill(dtFormPermission);
        //                            }
        //                            dtrow["None"] = 0;
        //                            dtrow["ViewOnly"] = 0;
        //                            dtrow["EditOnly"] = 0;
        //                            dtrow["AddOnly"] = 0;
        //                            dtrow["DeleteOnly"] = 0;
        //                            dtrow["FullControl"] = 0;
        //                            for (int b = 0; b < dtFormPermission.Rows.Count; b++)
        //                            {

        //                                if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "None")
        //                                {
        //                                    dtrow["None"] = 1;
        //                                }

        //                                if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "ViewOnly")
        //                                {
        //                                    dtrow["ViewOnly"] = 1;
        //                                }
        //                                if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "AddOnly")
        //                                {
        //                                    dtrow["AddOnly"] = 1;
        //                                }
        //                                if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "EditOnly")
        //                                {
        //                                    dtrow["EditOnly"] = 1;
        //                                }
        //                                if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "DeleteOnly")
        //                                {
        //                                    dtrow["DeleteOnly"] = 1;
        //                                }
        //                                if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "FullControl")
        //                                {
        //                                    dtrow["FullControl"] = 1;
        //                                }
        //                            }
        //                        }
        //                        dtPermission.Rows.Add(dtrow);
        //                    }


        //                }
        //            }

        //        }

        //    }
        //    catch (SqlException ex)
        //    {
        //        SetError(ex);
        //    }
        //    finally
        //    {
        //        dbConnection.Close();
        //    }
        //    return dtPermission;
        //}
        //public DataTable GetUserApplication(string UserId)
        //{


        //        DataTable dtTable = new DataTable();
        //        try
        //        {
        //            OpenDb();

        //            using (SqlCommand command = new SqlCommand("COM_GetUserPayrolltextchange", dbConnection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@UserId", UserId);
        //                using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
        //                {
        //                    daAdapter.Fill(dtTable);
        //                }
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (SqlException sqlex)
        //        {
        //            _isError = true;
        //            _errorNo = sqlex.Number;
        //        }
        //        catch (Exception ex)
        //        {
        //            _isError = true;
        //            _errorMsg = ex.Message;
        //        }
        //        finally
        //        {
        //            dbConnection.Close();
        //        }
        //        return dtTable;



        //}

        public DataTable GetUserApplication(string UserId)
        {
           
                DataTable dtPermission = new DataTable();
                DataTable dtForms = new DataTable();
                try
                {

                    dtPermission.Columns.Add("UserName", typeof(string));
                    dtPermission.Columns.Add("ApplicationName", typeof(string));
                    dtPermission.Columns.Add("MenuName", typeof(string));
                    dtPermission.Columns.Add("FormId", typeof(string));
                    dtPermission.Columns.Add("FormName", typeof(string));
                    dtPermission.Columns.Add("FormDescription", typeof(string));
                dtPermission.Columns.Add("ApplicationId", typeof(string));
                dtPermission.Columns.Add("None", typeof(bool));
                    dtPermission.Columns.Add("ViewOnly", typeof(bool));
                    dtPermission.Columns.Add("AddOnly", typeof(bool));
                    dtPermission.Columns.Add("EditOnly", typeof(bool));
                    dtPermission.Columns.Add("DeleteOnly", typeof(bool));
               
                dtPermission.Columns.Add("FullControl", typeof(bool));

                    
                            using (SqlCommand command = new SqlCommand("COM_GetUserPayrolltextchange", dbConnection))
                            {
                                OpenDb();
                                command.CommandType = CommandType.StoredProcedure;
                                
                                command.Parameters.AddWithValue("@UserId", UserId);

                                using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                                {
                                    daAdpter.Fill(dtForms);
                                }
                                for (int c = 0; c < dtForms.Rows.Count; c++)
                                {
                                    DataRow dtrow = dtPermission.NewRow();
                                    dtrow["UserName"] = Convert.ToString(dtForms.Rows[c]["UserName"]);
                                    dtrow["ApplicationName"] = Convert.ToString(dtForms.Rows[c]["ApplicationName"]);
                                    dtrow["MenuName"] = Convert.ToString(dtForms.Rows[c]["MenuName"]);
                                    dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
                                    dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
                                    dtrow["FormDescription"] = Convert.ToString(dtForms.Rows[c]["FormDescription"]);
                        dtrow["ApplicationId"] = Convert.ToString(dtForms.Rows[c]["ApplicationId"]);
                        dtrow["FormId"] = Convert.ToString(dtForms.Rows[c]["FormId"]);
                                    string fromId = Convert.ToString(dtForms.Rows[c]["FormId"]);

                                    DataTable dtFormPermission = new DataTable();
                                    using (SqlCommand commandPermmision = new SqlCommand("COM_GetUserPermissionSecond", dbConnection))
                                    {

                                        commandPermmision.CommandType = CommandType.StoredProcedure;

                                        using (SqlDataAdapter daAdpter = new SqlDataAdapter(commandPermmision))
                                        {
                                            daAdpter.Fill(dtFormPermission);
                                        }
                                        dtrow["None"] = 0;
                                        dtrow["ViewOnly"] = 0;
                                        dtrow["EditOnly"] = 0;
                                        dtrow["AddOnly"] = 0;
                                        dtrow["DeleteOnly"] = 0;
                                        dtrow["FullControl"] = 0;
                                        // for (int b = 0; b < dtFormPermission.Rows.Count; b++)
                                        {

                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "None")
                                            {
                                                dtrow["None"] = 1;
                                            }

                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "ViewOnly")
                                            {
                                                dtrow["ViewOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "AddOnly")
                                            {
                                                dtrow["AddOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "EditOnly")
                                            {
                                                dtrow["EditOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "DeleteOnly")
                                            {
                                                dtrow["DeleteOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "FullControl")
                                            {
                                                dtrow["FullControl"] = 1;
                                            }
                                        }
                                    }
                                    dtPermission.Rows.Add(dtrow);
                                }


                            }
                        

                    

                }
                catch (SqlException ex)
                {
                    SetError(ex);
                }
                finally
                {
                    dbConnection.Close();
                }
                return dtPermission;
            
        }
            public DataTable ViewUserRights(string UserId, string ApplicationId, string MenuId)
        {
            if (UserId == "0")
            {
                DataTable dtPermission = new DataTable();
                DataTable dtForms = new DataTable();
                try
                {

                    dtPermission.Columns.Add("UserName", typeof(string));
                    dtPermission.Columns.Add("ApplicationName", typeof(string));
                    dtPermission.Columns.Add("MenuName", typeof(string));
                    dtPermission.Columns.Add("FormId", typeof(string));
                    dtPermission.Columns.Add("FormName", typeof(string));
                    dtPermission.Columns.Add("FormDescription", typeof(string));
                    dtPermission.Columns.Add("None", typeof(bool));
                    dtPermission.Columns.Add("ViewOnly", typeof(bool));
                    dtPermission.Columns.Add("AddOnly", typeof(bool));
                    dtPermission.Columns.Add("EditOnly", typeof(bool));
                    dtPermission.Columns.Add("DeleteOnly", typeof(bool));
                    dtPermission.Columns.Add("FullControl", typeof(bool));

                    string[] ApplicationNumber = ApplicationId.Split(',');

                    int appCount = ApplicationNumber.Length;
                    if (appCount > 0)
                    {

                        for (int a = 0; a < appCount; a++)
                        {
                            string appID = ApplicationNumber[a].ToString();
                            using (SqlCommand command = new SqlCommand("MKS_Com_GetAllFormsUserwiseSecond", dbConnection))
                            {
                                OpenDb();
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@ApplicationId", 0);
                                command.Parameters.AddWithValue("@UserId", 0);

                                using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                                {
                                    daAdpter.Fill(dtForms);
                                }
                                for (int c = 0; c < dtForms.Rows.Count; c++)
                                {
                                    DataRow dtrow = dtPermission.NewRow();
                                    dtrow["UserName"] = Convert.ToString(dtForms.Rows[c]["UserName"]);
                                    dtrow["ApplicationName"] = Convert.ToString(dtForms.Rows[c]["ApplicationName"]);
                                    dtrow["MenuName"] = Convert.ToString(dtForms.Rows[c]["MenuName"]);
                                    dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
                                    dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
                                    dtrow["FormDescription"] = Convert.ToString(dtForms.Rows[c]["FormDescription"]);
                                    dtrow["FormId"] = Convert.ToString(dtForms.Rows[c]["FormId"]);
                                    string fromId = Convert.ToString(dtForms.Rows[c]["FormId"]);

                                    DataTable dtFormPermission = new DataTable();
                                    using (SqlCommand commandPermmision = new SqlCommand("COM_GetUserPermissionSecond", dbConnection))
                                    {

                                        commandPermmision.CommandType = CommandType.StoredProcedure;

                                        using (SqlDataAdapter daAdpter = new SqlDataAdapter(commandPermmision))
                                        {
                                            daAdpter.Fill(dtFormPermission);
                                        }
                                        dtrow["None"] = 0;
                                        dtrow["ViewOnly"] = 0;
                                        dtrow["EditOnly"] = 0;
                                        dtrow["AddOnly"] = 0;
                                        dtrow["DeleteOnly"] = 0;
                                        dtrow["FullControl"] = 0;
                                        // for (int b = 0; b < dtFormPermission.Rows.Count; b++)
                                        {

                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "None")
                                            {
                                                dtrow["None"] = 1;
                                            }

                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "ViewOnly")
                                            {
                                                dtrow["ViewOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "AddOnly")
                                            {
                                                dtrow["AddOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "EditOnly")
                                            {
                                                dtrow["EditOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "DeleteOnly")
                                            {
                                                dtrow["DeleteOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[c]["Permission"].ToString()) == "FullControl")
                                            {
                                                dtrow["FullControl"] = 1;
                                            }
                                        }
                                    }
                                    dtPermission.Rows.Add(dtrow);
                                }


                            }
                        }

                    }

                }
                catch (SqlException ex)
                {
                    SetError(ex);
                }
                finally
                {
                    dbConnection.Close();
                }
                return dtPermission;
            }
            else
            {
                DataTable dtPermission = new DataTable();
                DataTable dtForms = new DataTable();
                try
                {

                    dtPermission.Columns.Add("UserName", typeof(string));
                    dtPermission.Columns.Add("ApplicationName", typeof(string));
                    dtPermission.Columns.Add("MenuName", typeof(string));
                    dtPermission.Columns.Add("FormId", typeof(string));
                    dtPermission.Columns.Add("FormName", typeof(string));
                    dtPermission.Columns.Add("FormDescription", typeof(string));
                    dtPermission.Columns.Add("None", typeof(bool));
                    dtPermission.Columns.Add("ViewOnly", typeof(bool));
                    dtPermission.Columns.Add("AddOnly", typeof(bool));
                    dtPermission.Columns.Add("EditOnly", typeof(bool));
                    dtPermission.Columns.Add("DeleteOnly", typeof(bool));
                    dtPermission.Columns.Add("FullControl", typeof(bool));

                    string[] ApplicationNumber = ApplicationId.Split(',');

                    int appCount = ApplicationNumber.Length;
                    if (appCount > 0)
                    {

                        for (int a = 0; a < appCount; a++)
                        {
                            string appID = ApplicationNumber[a].ToString();
                            using (SqlCommand command = new SqlCommand("MKS_Com_GetAllFormsUserwise", dbConnection))
                            {
                                OpenDb();
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@ApplicationId", appID);
                                command.Parameters.AddWithValue("@UserId", UserId);

                                using (SqlDataAdapter daAdpter = new SqlDataAdapter(command))
                                {
                                    daAdpter.Fill(dtForms);
                                }
                                for (int c = 0; c < dtForms.Rows.Count; c++)
                                {
                                    DataRow dtrow = dtPermission.NewRow();
                                    dtrow["UserName"] = Convert.ToString(dtForms.Rows[c]["UserName"]);
                                    dtrow["ApplicationName"] = Convert.ToString(dtForms.Rows[c]["ApplicationName"]);
                                    dtrow["MenuName"] = Convert.ToString(dtForms.Rows[c]["MenuName"]);
                                    dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
                                    dtrow["FormName"] = Convert.ToString(dtForms.Rows[c]["FormName"]);
                                    dtrow["FormDescription"] = Convert.ToString(dtForms.Rows[c]["FormDescription"]);
                                    dtrow["FormId"] = Convert.ToString(dtForms.Rows[c]["FormId"]);
                                    string fromId = Convert.ToString(dtForms.Rows[c]["FormId"]);

                                    DataTable dtFormPermission = new DataTable();
                                    using (SqlCommand commandPermmision = new SqlCommand("COM_GetUserPermission", dbConnection))
                                    {

                                        commandPermmision.CommandType = CommandType.StoredProcedure;
                                        commandPermmision.Parameters.AddWithValue("@FormId", fromId);
                                        commandPermmision.Parameters.AddWithValue("@UserId", UserId);
                                        using (SqlDataAdapter daAdpter = new SqlDataAdapter(commandPermmision))
                                        {
                                            daAdpter.Fill(dtFormPermission);
                                        }
                                        dtrow["None"] = 0;
                                        dtrow["ViewOnly"] = 0;
                                        dtrow["EditOnly"] = 0;
                                        dtrow["AddOnly"] = 0;
                                        dtrow["DeleteOnly"] = 0;
                                        dtrow["FullControl"] = 0;
                                        for (int b = 0; b < dtFormPermission.Rows.Count; b++)
                                        {

                                            if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "None")
                                            {
                                                dtrow["None"] = 1;
                                            }

                                            if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "ViewOnly")
                                            {
                                                dtrow["ViewOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "AddOnly")
                                            {
                                                dtrow["AddOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "EditOnly")
                                            {
                                                dtrow["EditOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "DeleteOnly")
                                            {
                                                dtrow["DeleteOnly"] = 1;
                                            }
                                            if (Convert.ToString(dtFormPermission.Rows[b]["Permission"].ToString()) == "FullControl")
                                            {
                                                dtrow["FullControl"] = 1;
                                            }
                                        }
                                    }
                                    dtPermission.Rows.Add(dtrow);
                                }


                            }
                        }

                    }

                }
                catch (SqlException ex)
                {
                    SetError(ex);
                }
                finally
                {
                    dbConnection.Close();
                }
                return dtPermission;
            }

        }

        public void copyUserPermission(string fromId, string toId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_CopyUserPermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromUserId", fromId);
                    command.Parameters.AddWithValue("@ToUserId", toId);

                    command.ExecuteNonQuery();
                    _isSuccess = true;

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
        }

        public void UpdateUserPermission(string UserId, string PermissionId, string FormId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_UpdateUserPermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@PermissionId", PermissionId);
                    command.Parameters.AddWithValue("@FormId", FormId);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
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
        }

        public void DeleteUserPermission(string UserId, string PermissionId, string FormId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_DeleteUserPermission", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@PermissionId", PermissionId);
                    command.Parameters.AddWithValue("@FormId", FormId);

                    SqlParameter resultPara = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    resultPara.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultPara);

                    command.ExecuteNonQuery();
                    _isSuccess = true;
                    _result = resultPara.Value.ToString();
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
        }

        public string GetUserPermissions(string UserName, string ApplicationCode, string FormName)
        {
            string aceesRights = "None";

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_COM_GetUserPermissions", dbConnection))
                {
                    OpenDb();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
                    command.Parameters.AddWithValue("@FormName", FormName);

                    SqlParameter permission = new SqlParameter("@Permission", SqlDbType.NVarChar, 50);
                    permission.Direction = ParameterDirection.Output;
                    command.Parameters.Add(permission);

                    command.ExecuteNonQuery();
                    aceesRights = permission.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                SetError(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return aceesRights;
        }
        #endregion

        #endregion

        #region 


        public bool IsUserBlock(string UserId)
        {
            bool ReturnStatus = false;

            try
            {
                using (SqlCommand command = new SqlCommand("MKS_Com_IsUserBlocked", dbConnection))
                {
                    OpenDb();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);

                    ReturnStatus = Convert.ToBoolean(command.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                ReturnStatus = false;

                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return ReturnStatus;
        }
        /// <summary>
        /// Gets the MLS user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        public void GetUserAuthentication(string UserName, string Password)
        {
            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("MKS_COM_UserAuthentication", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);

                        SqlParameter isSuccess = new SqlParameter("@IsSucces", SqlDbType.Bit);
                        isSuccess.Direction = ParameterDirection.Output;
                        command.Parameters.Add(isSuccess);

                        SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                        result.Direction = ParameterDirection.Output;
                        command.Parameters.Add(result);

                        SqlParameter userId = new SqlParameter("@UserId", SqlDbType.NVarChar, 50);
                        userId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(userId);

                        SqlParameter usertypeId = new SqlParameter("@UserTyprId", SqlDbType.Int, 32);
                        usertypeId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(usertypeId);

                        command.ExecuteNonQuery();

                        _result = result.Value.ToString();
                        _isSuccess = Convert.ToBoolean(isSuccess.Value);
                        _userId = userId.Value.ToString();
                        _usertypeId = Convert.ToInt32(usertypeId.Value.ToString());
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
        }

        public bool GetCompanyAuthentication(string UserId, int CompanyId)
        {
            bool isAuthenticate = false;
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_CompanyAuthentication", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);

                        SqlParameter company = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 50);
                        company.Direction = ParameterDirection.Output;
                        command.Parameters.Add(company);

                        SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                        result.Direction = ParameterDirection.Output;
                        command.Parameters.Add(result);

                        SqlParameter isSuccess = new SqlParameter("@IsSucces", SqlDbType.Bit);
                        isSuccess.Direction = ParameterDirection.Output;
                        command.Parameters.Add(isSuccess);
                        command.ExecuteNonQuery();
                        isAuthenticate = Convert.ToBoolean(isSuccess.Value);
                        _companyName = company.Value.ToString();
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
            return isAuthenticate;
        }
        #endregion

        #region User Company
        public void AddUserCompany(string UserId, int CompanyId, bool Selected)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("COM_AddUserCompany", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@Selected", Selected);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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
        }

        public DataTable GetUserCompany(string UserId)
        {
            if (UserId == null)
            {

                DataTable dtTable = new DataTable();
                try
                {
                    OpenDb();

                    using (SqlCommand command = new SqlCommand("COM_GetUserCompanyDetails", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", 0);
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sqlex)
                {
                    _isError = true;
                    _errorNo = sqlex.Number;
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
            else
            {
                DataTable dtTable = new DataTable();
                try
                {
                    OpenDb();

                    using (SqlCommand command = new SqlCommand("COM_GetUserCompany", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", UserId);
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sqlex)
                {
                    _isError = true;
                    _errorNo = sqlex.Number;
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
        }

        public DataTable GetAllUsersCompany()
        {
          
                DataTable dtTable = new DataTable();
                try
                {
                    OpenDb();

                    using (SqlCommand command = new SqlCommand("GetAllUsersCompany", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sqlex)
                {
                    _isError = true;
                    _errorNo = sqlex.Number;
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
            public DataTable GetActiveUserCompany(string UserId)
        {
            if (UserId == null)
            {

                DataTable dtTable = new DataTable();
                try
                {
                    OpenDb();

                    using (SqlCommand command = new SqlCommand("GetUserCompanySp", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", 0);
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sqlex)
                {
                    _isError = true;
                    _errorNo = sqlex.Number;
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
            else
            {
                DataTable dtTable = new DataTable();
                try
                {
                    OpenDb();

                    using (SqlCommand command = new SqlCommand("COM_GetActiveUserCompany", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", UserId);
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sqlex)
                {
                    _isError = true;
                    _errorNo = sqlex.Number;
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
        }

        #endregion

        #region UserPayroll Permission
        public DataTable GetPayCategory(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("COM_GetAllPayCategory", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.ExecuteNonQuery();
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
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

        public void AddUserPayroll(string UserId, int CompanyId, int PayPeriodCategoryId, bool Selected)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("COM_AddUserPayroll", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@PayPeriodCategoryId", PayPeriodCategoryId);
                    command.Parameters.AddWithValue("@Selected", Selected);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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
        }

        public DataTable GetUserPayroll(string UserId, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                if (UserId == "0")
                {
                    OpenDb();
                    using (SqlCommand command = new SqlCommand("COM_GetUserPayrollSecond", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                    }
                }
                else
                {
                    OpenDb();
                    using (SqlCommand command = new SqlCommand("COM_GetUserPayroll", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", UserId);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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

        public DataTable GetActiveUserPayroll(string UserId, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                if (UserId == "0")
                {
                    OpenDb();
                    using (SqlCommand command = new SqlCommand("COM_GeActivetUserPayrollSecond", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                    }
                }
                else
                {
                    OpenDb();
                    using (SqlCommand command = new SqlCommand("COM_GetActiveUserPayroll", dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", UserId);
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                        {
                            daAdapter.Fill(dtTable);
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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

        #region Users
        public DataTable GetUser(string UserId, int UserTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_GetUser", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@UserTypeId", UserTypeId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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

        public DataTable GetActiveUser(string UserId, int UserTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_GetUser", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@UserTypeId", UserTypeId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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

        public DataTable GetAllUser(string UserId, int UserTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_GetAllUser", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@UserTypeId", UserTypeId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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

        public void AddSystemUser(string UserName, string Password, string Email, int UserTypeId, string Firstname, string LastName, string CreatedUser)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_AddSystemUser", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@UserTypeId ", UserTypeId);
                    command.Parameters.AddWithValue("@Firstname", Firstname);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    SqlParameter isSuccess = new SqlParameter("@IsSucces", SqlDbType.Bit);
                    isSuccess.Direction = ParameterDirection.Output;
                    command.Parameters.Add(isSuccess);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(isSuccess.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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

        }

        public void UpdateSystemUser(string UserId, string Password, string Email, string Firstname, string LastName, int UserTypeId, bool IsBlock, string UpdatedUser)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_UpdateSystemUser", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Firstname", Firstname);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@UserTypeId", UserTypeId);
                    command.Parameters.AddWithValue("@IsBlock", IsBlock);
                    command.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);

                    SqlParameter isSuccess = new SqlParameter("@IsSucces", SqlDbType.Bit);
                    isSuccess.Direction = ParameterDirection.Output;
                    command.Parameters.Add(isSuccess);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(isSuccess.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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

        }

        public void DeleteSystemUser(string UserId, string LoginUser)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_DeleteSystemUser", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@LoginUser", LoginUser);

                    SqlParameter isSuccess = new SqlParameter("@IsSucces", SqlDbType.Bit);
                    isSuccess.Direction = ParameterDirection.Output;
                    command.Parameters.Add(isSuccess);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(isSuccess.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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
        }

        public void ChangeUserPassword(string UserName, string OldPassword, string NewPassword, string LoginUser)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_ChangeUserPassword", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@OldPassword", OldPassword);
                    command.Parameters.AddWithValue("@NewPassword", NewPassword);
                    command.Parameters.AddWithValue("@LoginUser", LoginUser);

                    SqlParameter isSuccess = new SqlParameter("@IsSucces", SqlDbType.Bit);
                    isSuccess.Direction = ParameterDirection.Output;
                    command.Parameters.Add(isSuccess);

                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar, 50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);

                    command.ExecuteNonQuery();
                    _result = result.Value.ToString();
                    _isSuccess = Convert.ToBoolean(isSuccess.Value);
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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
        }

        public void BlockUser(string UserId, bool Block)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("MKS_COM_BlockUser", dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@Block", Block);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                _isError = true;
                _errorNo = sqlex.Number;
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
        }
        #endregion

        #endregion

        #region Destructor
        ~MksSecurityDAL()
        {
            //_dbConnection.Close();
            //_dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
