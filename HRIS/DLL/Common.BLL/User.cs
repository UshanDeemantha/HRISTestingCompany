/// <summary>
/// Solution Name        : HRM
/// Project Name          : Common.BLL
/// Author                     : ushan deemantha
/// Class Description    : User
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.DAL;
using System.Data;
using System.Reflection;
using System.Net.Mail;

namespace Common.BLL
{
    [DataObject]
    public class User
    {
        #region Fields
        
        private bool _isError;
        private string _errorMsg;
        private UserDAL objUserDAL;
        private string _userId="0";
        private string _userName = string.Empty;
        private string test = string.Empty;
        #endregion


        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
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
        public string UserId
        { get { return _userId; } }

        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
        }
        #endregion

         #region Constructor       
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public User()
        {
           objUserDAL=new UserDAL();
        } 
        #endregion


        #region Methods

        private void SetValues()
        {
            _isError = objUserDAL.IsError;
            _errorMsg = objUserDAL.ErrorMsg;
            _userId = objUserDAL.UserId;
            _userName = objUserDAL.UserName;

        }
        #endregion

        #region Methods

        #region User
        public void AddNewUser(string UserName, string Password, string Email, string Firstname, string LastName, bool IsBlock, string SecretQuestion, string Answer,string LogUserId)
        {
            try
            {
                objUserDAL.AddNewUser(UserName, Password, Email, Firstname, LastName, IsBlock, SecretQuestion, Answer, LogUserId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }

        public void UpdateUser(string UserId, string UserName,  string Email, string Firstname, string LastName, bool IsBlock, string SecretQuestion, string Answer, string LogUserID)
        {
            try
            {
                objUserDAL.UpdateUser(UserId, UserName,  Email, Firstname,LastName,IsBlock,  SecretQuestion, Answer, LogUserID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }


        public void BlockUser(string UserName, int IsBlock)
        {

            try
            {
               objUserDAL.BlockUser(UserName,IsBlock);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        
        
        }

        public void DeleteUsers(string UserId)
        {
            try
            {
                objUserDAL.DeleteUsers(UserId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        
        
        }

        public DataTable SearchUser(string UserName,string Email, string FirstName,string LastName)
        {
            DataTable dTable=new DataTable();
            try
            {
               dTable= objUserDAL.SearchUser(UserName,Email, FirstName,LastName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dTable;
        }

        public void VerifyUserNameANDPassword(string UserName, string Password)
        {
            try
            {
               objUserDAL.VerifyUserNameANDPassword(UserName,Password);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }


        }
        public DataTable GetUser(string UserId)
        {
            DataTable dTable = new DataTable();
            try
            {
                dTable = objUserDAL.GetUser(UserId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dTable;
        }

        public void sendemailInExchangeServer(string from, string to, string subject, string body)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.From = new MailAddress(from);
            mailMessage.Sender = new MailAddress(from);
            mailMessage.To.Add(new MailAddress(to));

            mailMessage.Subject = subject.Trim();
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body.Trim();
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            SmtpClient smtpClient = new SmtpClient("smarthost.enterprisenet.org", 25);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            FieldInfo transport = smtpClient.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo authModules = transport.GetValue(smtpClient).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);
            Array modulesArray = authModules.GetValue(transport.GetValue(smtpClient)) as Array;
            modulesArray.SetValue(modulesArray.GetValue(2), 0);
            modulesArray.SetValue(modulesArray.GetValue(2), 1);
            modulesArray.SetValue(modulesArray.GetValue(2), 3);

            smtpClient.Send(mailMessage);
        }
        #endregion
        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~User()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion

    }
}
