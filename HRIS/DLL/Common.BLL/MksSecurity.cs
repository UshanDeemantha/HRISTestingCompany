namespace HRM.Common.BLL
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using HRM.Common.DAL;
    using System.Collections.Generic;
    using System.Net.Mail;
using System.Text;

    [DataObject]
    public class MksSecurity
    {
        #region Fields
        
        private string _errorMsg = string.Empty;
        private int _errorNo = 0;
        private bool _isError = false;
        private bool _isSuccess = false;
        private string _result = string.Empty;
        private string _userName=string.Empty;
        private int _companyId = 0;
        private string _companyName = string.Empty;

        MksSecurityDAL objMKS_Security;
        
        DataTable dtTable;

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

        public string UserName
        {
            get { return _userName; }
        }

        public int CompanyId
        {
            get { return _companyId; }
        }

        public string CompanyName
        {
            get { return _companyName; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MksSecurity"/> class.
        /// </summary>
        public MksSecurity()
        {
            objMKS_Security = new MksSecurityDAL();
        }
        #endregion

        #region Methods
        #region Internal
        private void SetValues()
        {
            _isError = objMKS_Security.IsError;
            _errorMsg = objMKS_Security.ErrorMsg;
            _isSuccess = objMKS_Security.IsSuccess;
            _errorNo = objMKS_Security.ErrorNo;
            _result = objMKS_Security.Result;
        }     
        #endregion

        #region External
               
        public DataTable IsExistCompany(string UserId, int CompanyId)
        {
            try
            {
                dtTable = objMKS_Security.IsExistsCompany(UserId, CompanyId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable SelectUsersToAssignRights(string currentUserName)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objMKS_Security.SelectUsersToAssignRights(currentUserName);
                
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        #region Level Wise Rights Assignment

        public DataTable GetUserLevelOrganizationalStucture(int orgStructureID, string userID, string applicationID, int companyId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objMKS_Security.GetUserLevelOrganizationalStucture(orgStructureID, userID, applicationID, companyId);

                SetValues();
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
                dtTable = objMKS_Security.GetUserLevelDesignationStucture(desigStructureID, userID, applicationID);

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public int SaveDesignationAndOrganizationRights(string userID, string appID, List<MksSecurityDAL.OrgStructureToSave> orgStructure, List<MksSecurityDAL.DesigStructureToSave> desigStructure, string createdUser, int companyId)
        {
            int ReturnStatus = -1;

            try
            {
                ReturnStatus = objMKS_Security.SaveDesignationAndOrganizationRights(userID, appID, orgStructure, desigStructure, createdUser, companyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return ReturnStatus;
        }

        public bool AllowNewLevelRightsSave(string userID, string appID, int companyId)
        {
            bool ReturnStatus = false;

            try
            {
                ReturnStatus = objMKS_Security.AllowNewLevelRightsSave(userID, appID, companyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return ReturnStatus;
        }

        public int DeleteUserLevelRights(string userID, string appID)
        {
            int ReturnStatus = -1;

            try
            {
                ReturnStatus = objMKS_Security.DeleteUserLevelRights(userID, appID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           

            return ReturnStatus;
        }

        public int UpdateDesignationAndOrganizationRights(string userID, string appID, List<MksSecurityDAL.OrgStructureToSave> orgStructure, List<MksSecurityDAL.DesigStructureToSave> desigStructure, string createdUser, int companyId)
        {
            int ReturnStatus = -1;

            try
            {
                int deleteStatus = DeleteUserLevelRights(userID, appID); 
                if (deleteStatus == 1)
                {
                    ReturnStatus = objMKS_Security.SaveDesignationAndOrganizationRights(userID, appID, orgStructure, desigStructure, createdUser, companyId);
                }

                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return ReturnStatus;
        }
        
        #endregion

        #region Password Reset

        public Int64 AddPasswordResetRequest(string userID, int companyID, string encryptionKey, DateTime submittedDateTime, string checksumID, string requestByClientID)
        {
            Int64 createdIndexID = 0;

            try
            {
                createdIndexID = objMKS_Security.AddPasswordResetRequest(userID, companyID, encryptionKey, submittedDateTime, checksumID, requestByClientID);
                if (createdIndexID == -1)
                {
                    _isError = objMKS_Security.IsError;
                    _errorMsg = objMKS_Security.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return createdIndexID;
        }

        public bool CheckForUserCompanyAccessRights(string userID, int companyID)
        {
            bool ReturnStatus = false;

            try
            {
                ReturnStatus = objMKS_Security.CheckForUserCompanyAccessRights(userID, companyID);
                if (ReturnStatus == false)
                {
                    _isError = objMKS_Security.IsError;
                    _errorMsg = objMKS_Security.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                ReturnStatus = false;

                _isError = true;
                _errorMsg = ex.Message;
            }

            return ReturnStatus;
        }

        public string GetUserIdForUserName(string userName)
        {
            string ReturnStatus = string.Empty;

            try
            {
                ReturnStatus = objMKS_Security.GetUserIdForUserName(userName);
                if (ReturnStatus == string.Empty)
                {
                    _isError = objMKS_Security.IsError;
                    _errorMsg = objMKS_Security.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return ReturnStatus;
        }

        public string GetEmailForUserName(string userName)
        {
            string userEmail = string.Empty;

            try
            {
                userEmail = objMKS_Security.GetEmailForUserName(userName);
                if (userEmail == string.Empty)
                {
                    _isError = objMKS_Security.IsError;
                    _errorMsg = objMKS_Security.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return userEmail;
        }

        public DataTable GetLastPasswordRequest(string userID, int companyID)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objMKS_Security.GetLastPasswordRequest(userID, companyID);
                if (objMKS_Security.IsError == true)
                {
                    _isError = objMKS_Security.IsError;
                    _errorMsg = objMKS_Security.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public string GetPasswordResetRequestStatus(string userName, string checksum, int companyID, DateTime currentDateTime)
        {
            DataTable dtTable = new DataTable();
            string returnString = string.Empty;

            try
            {
                dtTable = objMKS_Security.GetLastPasswordRequestFromCheckSum(userName, checksum, companyID);
                if (objMKS_Security.IsError == true)
                {
                    _isError = objMKS_Security.IsError;
                    _errorMsg = objMKS_Security.ErrorMsg;

                    returnString = "error";
                }
                else
                {
                    if (dtTable.Rows.Count > 0)
                    {
                        DateTime dTRequestTime = Convert.ToDateTime(dtTable.Rows[0]["SubmittedDateTime"].ToString());
                        if (currentDateTime.Subtract(dTRequestTime).Seconds <= 1800 && currentDateTime.Subtract(dTRequestTime).Seconds >= 0)
                        {
                            returnString = "request ok";
                        }
                        else
                        {
                            returnString = "timeout";
                        }
                    }
                    else
                    {
                        returnString = "invalid request";
                    }
                }
            }
            catch (Exception ex)
            {
                returnString = "error";

                _isError = true;
                _errorMsg = ex.Message;
            }

            return returnString;
        }

        public bool SendSMTPEmailMessage(string emailFromAddress,
            string smtpCredentialPassword,
            string mailMessageSubject,
            string mailMessageBody,
            string smtpHost,
            List<string> emailRecipientList,
            int smtpDefaultPort = 25,
            bool smtpSslEnabled = false,
            bool smtpCredentialsUsed = false)
        {
            bool isSuccess = false;

            try
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress(emailFromAddress);
                foreach (string toEmail in emailRecipientList)
                {
                    msg.To.Add(new MailAddress(toEmail));
                }
                msg.Subject = mailMessageSubject;
                msg.Body = mailMessageBody;
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Host = smtpHost;
                smtp.Port = smtpDefaultPort;
                smtp.EnableSsl = smtpSslEnabled;

                if (smtpCredentialsUsed)
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(emailFromAddress, smtpCredentialPassword);
                }

                smtp.Send(msg);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public string GenerateEmailBody(string username, string requestSubmitTime, string requestFrom, string pwdResetURL)
        {
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            emailBody.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            emailBody.Append("<head>");
            emailBody.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            emailBody.Append("</head><body><div align=\"center\">");

            // start of table data
            emailBody.Append("<table width=\"600\">");
            emailBody.Append("<tr><td colspan=\"2\" align=\"center\"><h1><strong><em>Your password reset request is submitted.</em></strong></h1></td></tr>");
            emailBody.Append("<tr><td></td><td></td></tr>");
            
            emailBody.Append("<tr><td width=\"190\">Username:</td><td>");
            emailBody.Append(username);
            emailBody.Append("</td></tr>");

            emailBody.Append("<tr><td width=\"190\">Request Submission Time:</td><td>");
            emailBody.Append(requestSubmitTime);
            emailBody.Append("</td></tr>");
            
            emailBody.Append("<tr><td width=\"190\">Your Request will Expire In:</td><td>30 minutes</td></tr>");
            
            emailBody.Append("<tr style=\"color:Red;\"><td width=\"190\">Request Submitted From:</td><td>");
            emailBody.Append(requestFrom);
            emailBody.Append("</td></tr>");
            
            emailBody.Append("<tr><td></td><td></td></tr>");

            // external link, to be embedded into email
            emailBody.Append("<tr><td colspan=\"2\" align=\"right\"><a href=\"");
            emailBody.Append(pwdResetURL);
           

            emailBody.Append("<tr><td colspan=\"2\" align=\"center\"><hr/></td></tr>");
            emailBody.Append("<tr><td colspan=\"2\" align=\"justify\">");
            emailBody.Append("If recipient of this email (you) did not make such a request, please ignore this ");
            emailBody.Append("email. This email is intended for recipients only; please avoid forwarding it ");
            emailBody.Append("in consideration of MasterKey: HRM, data & user account safety. Although contents ");
            emailBody.Append("of this email may not be confidential; it is privileged & if you receive this ");
            emailBody.Append("email erroneously please delete it and notify sender immediately.");
            emailBody.Append("</td></tr>");
            // end of table data
            emailBody.Append("</table>");
            // end of html document
            emailBody.Append("</div></body></html>");

            return emailBody.ToString();
        }

        public bool ChangeUserPasswordWithoutConfirmation(string userName, string userID, string newPassword)
        {
            bool isPasswordChangeSuccess = false;

            try
            {
                isPasswordChangeSuccess = objMKS_Security.ChangeUserPasswordWithoutConfirmation(userName, userID, newPassword);
                if (objMKS_Security.IsError == true)
                {
                    _isError = objMKS_Security.IsError;
                    _errorMsg = objMKS_Security.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                isPasswordChangeSuccess = false;

                _isError = true;
                _errorMsg = ex.Message;
            }

            return isPasswordChangeSuccess;
        }

        #endregion

        #region Application

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetApplication(string ApplicationId)
        {
            DataTable dtTable = new DataTable();
            
            try
            {
                dtTable = objMKS_Security.GetApplication(ApplicationId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public void AddApplication(string ApplicationCode, string ApplicationName)
        {
            try
            {
                objMKS_Security.AddApplication(ApplicationCode, ApplicationName);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }          
        }
        public void SaveAccountDetails(int EmployeeId, int BankId,int BranchId,string AccountNo,string NameasperBank,string Percentage)
        {
            try
            {
                objMKS_Security.SaveAccountDetails(EmployeeId, BankId, BranchId, AccountNo, NameasperBank, Percentage);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        public void UpdateAccountDetails(int EmployeeId, int BankId, int BranchId, string AccountNo, string NameasperBank, string Percentage)
        {
            try
            {
                objMKS_Security.UpdateAccountDetails(EmployeeId, BankId, BranchId, AccountNo, NameasperBank, Percentage);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateApplication(string ApplicationId, string ApplicationCode, string ApplicationName)
        {
            try
            {
                objMKS_Security.UpdateApplication(ApplicationId, ApplicationCode, ApplicationName);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void DeleteApplication(string ApplicationId)
        {
            try
            {
                objMKS_Security.DeleteApplication(ApplicationId);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }          
        }

        #endregion

        #region Menu
        public DataTable GetMenu(string ApplicationId, string MenuId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetMenu(ApplicationId, MenuId);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
            return dtTable;
        }

        public void AddMenu(string ApplicationId, string MenuName)
        {
            try
            {
                objMKS_Security.AddMenu(ApplicationId, MenuName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }            
        }

        public void UpdateMenu(string MenuId, string ApplicationId, string MenuName)
        {
            try
            {
                objMKS_Security.UpdateMenu(MenuId, ApplicationId, MenuName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void DeleteMenu(string MenuId)
        {
            try
            {
                objMKS_Security.DeleteMenu(MenuId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }
        #endregion

        #region Form
        public DataTable GetForm(string MenuId, string FormId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetForm(MenuId, FormId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddForm(string MenuId, string FormName, string FormDescription, int FormIndexNo)
        {
            try
            {
                objMKS_Security.AddForm(MenuId, FormName, FormDescription, FormIndexNo);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateForm(string FormId, string FormName, string FormDescription, int FormIndexNo)
        {
            try
            {
                objMKS_Security.UpdateForm(FormId, FormName, FormDescription, FormIndexNo);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteForm(string FormId)
        {
            try
            {
                objMKS_Security.DeleteForm(FormId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        } 
        #endregion

        #region Permission Type
        public DataTable GetPermission(string PermissionId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetPermission(PermissionId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddPermission(string Permission, int PermissionOrderNo, string Remarks)
        {
            try
            {
                objMKS_Security.AddPermission(Permission, PermissionOrderNo, Remarks);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdatePermission(string PermissionId, string Permission, int PermissionOrderNo, string Remarks)
        {
            try
            {
                objMKS_Security.UpdatePermission(PermissionId, Permission, PermissionOrderNo, Remarks);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeletePermission(string PermissionId)
        {
            try
            {
                objMKS_Security.DeletePermission(PermissionId);
                SetValues();
            }
            catch (SqlException ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }   
        #endregion

        #region User Permissions
        public void AddUserPermission(string UserId, string PermissionId, string FormId, bool Active)
        {
            try
            {
                objMKS_Security.AddUserPermission(UserId, PermissionId, FormId, Active);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUserApplication(string UserId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetUserApplication(UserId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        public DataTable ViewUserRights(string UserId, string ApplicationId, string MenuId)
        {
            DataTable dtTable = new DataTable();

            try
            {
                dtTable = objMKS_Security.ViewUserRights(UserId, ApplicationId, MenuId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtTable;
        }

        public void copyUserPermission(string fromId, string toId)
        {
            try
            {
                objMKS_Security.copyUserPermission(fromId, toId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateUserPermission(string UserId, string PermissionId, string FormId)
        {
            try
            {
                objMKS_Security.UpdateUserPermission(UserId, PermissionId, FormId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteUserPermission(string UserId, string PermissionId, string FormId)
        {
            try
            {
                objMKS_Security.DeleteUserPermission(UserId, PermissionId, FormId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public AccessRights GetUserPermissions(string UserName, string ApplicationCode, string FormName)
        {
            string aceesRights = string.Empty;
            AccessRights accessPermission = AccessRights.None;

            try
            {
                aceesRights = objMKS_Security.GetUserPermissions(UserName, ApplicationCode, FormName);
                SetValues();
                
                switch (aceesRights.ToLowerInvariant().Trim())
                {
                    case "viewonly": accessPermission = AccessRights.ViewOnly;
                        break;
                    case "addonly": accessPermission = AccessRights.AddOnly;
                        break;
                    case "editonly": accessPermission = AccessRights.EditOnly;
                        break;
                    case "deleteonly": accessPermission = AccessRights.DeleteOnly;
                        break;
                    case "fullcontrol": accessPermission = AccessRights.FullControl;
                        break;
                    default: accessPermission = AccessRights.None;
                        break;
                }              
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return accessPermission;
        }

        public bool GetSpecificAccessRights(string userName, string applicationName, string formName, AccessRights accessPermissionToCheck)
        {
            bool isHaveRights = false;

            AccessRights accessPermission = GetUserPermissions(userName, applicationName, formName);

            switch (accessPermissionToCheck)
            {
                case AccessRights.ViewOnly:
                    {
                        if (accessPermission == AccessRights.None)
                        {
                            isHaveRights = false;
                        }
                        else
                        {
                            isHaveRights = true;
                        }
                        break;
                    }
                case AccessRights.AddOnly:
                    {
                        if (accessPermission == AccessRights.FullControl || accessPermission == AccessRights.AddOnly)
                        {
                            isHaveRights = true;
                        }
                        else
                        {
                            isHaveRights = false;
                        }
                        break;
                    }
                case AccessRights.EditOnly:
                    {
                        if (accessPermission == AccessRights.FullControl || accessPermission == AccessRights.EditOnly)
                        {
                            isHaveRights = true;
                        }
                        else
                        {
                            isHaveRights = false;
                        }
                        break;
                    }
                case AccessRights.DeleteOnly:
                    {
                        if (accessPermission == AccessRights.FullControl || accessPermission == AccessRights.DeleteOnly)
                        {
                            isHaveRights = true;
                        }
                        else
                        {
                            isHaveRights = false;
                        }
                        break;
                    }
                case AccessRights.FullControl:
                    {
                        if (accessPermission == AccessRights.FullControl)
                        {
                            isHaveRights = true;
                        }
                        else
                        {
                            isHaveRights = false;
                        }
                        break;
                    }
                default:
                    {
                        isHaveRights = false;
                        break;
                    }
            }

            return isHaveRights;
        }

        #endregion

        #endregion
       
        #region Login
        /// <summary>
        /// Gets the User Authentication.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="CompanyId">CompanyId As string</param>
        /// <returns>Returns DataTable</returns>
        public void GetUserAuthentication(string UserName, string Password, int CompanyId)
        {
            bool isAuthenticate = false;
            try
            {
                objMKS_Security.GetUserAuthentication(UserName, Password);
                SetValues();

                if (objMKS_Security.IsSuccess == true && objMKS_Security.IsError == false)
                {
                    isAuthenticate = objMKS_Security.GetCompanyAuthentication(objMKS_Security.UserId, CompanyId);

                    if (objMKS_Security.IsError == false)
                    {
                        if (isAuthenticate == true)
                        {
                            _userName = UserName.Trim();
                            _companyId = CompanyId;

                            _companyName = objMKS_Security.CompanyName;
                        }
                        else
                        {
                            _isSuccess = false;
                            _errorMsg = "Access Permissions Denied for Selected Company!" + Environment.NewLine + "Contact System Administrator!";

                            return;
                        }
                    }
                }
                else
                {
                    if (objMKS_Security.IsError == true)
                    {
                        _errorMsg = objMKS_Security.ErrorMsg;
                        return;
                    }

                    if (objMKS_Security.IsSuccess == false)
                    {
                        _errorMsg = objMKS_Security.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        #endregion

        #region User Company
        public void AddUserCompany(string UserId, int CompanyId, bool Selected)
        {
            try
            {
                objMKS_Security.AddUserCompany(UserId, CompanyId, Selected);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUserCompany(string UserId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetUserCompany(UserId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetAllUsersCompany()
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetAllUsersCompany();
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetActiveUserCompany(string UserId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetActiveUserCompany(UserId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        #endregion

        #region User Payroll Permission
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetPayCategory(int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetPayCategory(CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddUserPayRoll(string UserId, int CompanyId, int PayPeriodCategoryId, bool Selected)
        {
            try
            {
                objMKS_Security.AddUserPayroll(UserId, CompanyId, PayPeriodCategoryId, Selected);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetUserPayRoll(string UserId, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetUserPayroll(UserId, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetActiveUserPayRoll(string UserId, int CompanyId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetActiveUserPayroll(UserId, CompanyId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }
        #endregion

        #region Users
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetUser(string UserId, int UserTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable= objMKS_Security.GetUser(UserId, UserTypeId);
                SetValues();
            }          
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public DataTable GetActiveUser(string UserId, int UserTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objMKS_Security.GetActiveUser(UserId, UserTypeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        public void AddSystemUser(string UserName, string Password, string Email, int UserTypeId, string Firstname, string LastName, string CreatedUser)
        {
            try
            {
                objMKS_Security.AddSystemUser(UserName, Password, Email, UserTypeId, Firstname, LastName, CreatedUser);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
     
        public void UpdateSystemUser(string UserId, string Password, string Email, string Firstname, string LastName, int UserTypeId, bool IsBlock, string UpdatedUser)
        {
            try
            {
                objMKS_Security.UpdateSystemUser(UserId, Password, Email, Firstname, LastName, UserTypeId, IsBlock, UpdatedUser);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            } 
        }

        public void DeleteSystemUser(string UserId, string LoginUser)
        {
            try
            {
                objMKS_Security.DeleteSystemUser(UserId, LoginUser);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }

        public void ChangeUserPassword(string UserName, string OldPassword, string NewPassword, string LoginUser)
        {
            try
            {
                objMKS_Security.ChangeUserPassword(UserName, OldPassword, NewPassword, LoginUser);
                SetValues();
            }           
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }          
        }

        public void BlockUser(string UserId, bool Block)
        {
            try
            {
                objMKS_Security.BlockUser(UserId, Block);
                SetValues();
            }            
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }           
        }
        #endregion
        #endregion

        #region Destructor
        ~MksSecurity()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
