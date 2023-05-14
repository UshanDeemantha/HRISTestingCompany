/// <summary>
/// Solution Name        : HRM
/// Project Name          : Common.DAL
/// Author                     : ushan deemantha
/// Class Description    : UserDAL
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Common.DAL
{
   public class UserDAL
    {

        #region Fields/Variables
        private SqlConnection _dbConnection;
        private string _errorMsg = string.Empty;
        private bool _isError = false;
        private string _userId;
        private string _userName = string.Empty;
        
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Error Message
        /// </summary>
        /// <value>ErrorMsg As string</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
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
        { get { return _userId; } }

        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
        }

        #endregion

        public UserDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }


        /// <summary>
        /// Adds the new user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Email">The email.</param>
        /// <param name="Firstname">The firstname.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="IsBlock">if set to <c>true</c> [is block].</param>
        /// <param name="SecretQuestion">The secret question.</param>
        /// <param name="Answer">The answer.</param>
        /// <param name="LogUserID">The log user ID.</param>
        public void AddNewUser(string UserName, string Password, string Email, string Firstname, string LastName,bool IsBlock,string SecretQuestion,string Answer,string LogUserID)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("mks_Sys_CreateUser", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Firstname", Firstname);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@IsBlock", IsBlock);
                    command.Parameters.AddWithValue("@SecretQuestion", SecretQuestion);
                    command.Parameters.AddWithValue("@Answer", Answer);
                    SqlParameter succes = new SqlParameter("@IsSucces", SqlDbType.Bit);
                    succes.Direction = ParameterDirection.Output;
                    command.Parameters.Add(succes);
                    SqlParameter result= new SqlParameter("@Result", SqlDbType.NVarChar,50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);
                    SqlParameter userId = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
                    userId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(userId);
                    command.Parameters.AddWithValue("@LogUserID", LogUserID);
                    command.ExecuteNonQuery();
                    _userId =userId.Value.ToString();
                    _errorMsg = result.Value.ToString();
                    if (_userId ==" 0" && succes.Value.ToString()=="0")
                    {
                        _isError = true;
                       
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
        }


        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Email">The email.</param>
        /// <param name="Firstname">The firstname.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="IsBlock">if set to <c>true</c> [is block].</param>
        /// <param name="SecretQuestion">The secret question.</param>
        /// <param name="Answer">The answer.</param>
        /// <param name="LogUserID">The log user ID.</param>
        public void UpdateUser(string UserId, string UserName, string Email, string Firstname, string LastName, bool IsBlock, string SecretQuestion, string Answer, string LogUserID)
        {
            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("mks_Sys_EditUser", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Firstname", Firstname);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@IsBlock", IsBlock);
                    command.Parameters.AddWithValue("@SecretQuestion", SecretQuestion);
                    command.Parameters.AddWithValue("@Answer", Answer);
                    SqlParameter success = new SqlParameter("@IsSuccess", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);
                    SqlParameter result = new SqlParameter("@IsResult", SqlDbType.NVarChar, 50);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);
                    command.Parameters.AddWithValue("@LogUserID", LogUserID);
                    command.ExecuteNonQuery();
                    _errorMsg = result.Value.ToString();
                    if ( success.Value.ToString() == "0")
                    {
                        _isError = true;
                        
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
        }

        /// <summary>
        /// Blocks the user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="IsBlock">The is block.</param>
        public void BlockUser(string UserName, int IsBlock)
        {
            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("mks_Sys_BlockUser", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@IsBlocked", IsBlock);
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
        /// Deletes the users.
        /// </summary>
        /// <param name="username">The UserName.</param>
        public void DeleteUsers(string UserId)
        {
            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("mks_Sys_DeleteUser", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
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
        /// Gets user.
        /// </summary>
        /// <returns></returns>
        public DataTable GetUser(string UserId)
        {
           
            DataTable dtTable = new DataTable();

            try
            {
                OpenDb();
                using (SqlCommand command = new SqlCommand("mks_Sys_GetUser", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
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
                _dbConnection.Close();
            }

            return dtTable;
        }

        /// <summary>
        /// Searches the user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Email">The email.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <returns></returns>
        public DataTable SearchUser(string UserName,string Email, string FirstName,string LastName)
        {
           
            DataTable dtTable = new DataTable();
            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("mks_Sys_SearchUser", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
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
                _dbConnection.Close();
            }
            return dtTable;
        }



        /// <summary>
        /// Verifies the user name AND password.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        public void VerifyUserNameANDPassword(string UserName, string Password)
        {

           
            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("mks_Sys_VerifyUserNameANDPassword", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    SqlParameter isSucces = new SqlParameter("@IsSucces", SqlDbType.Bit);
                    isSucces.Direction = ParameterDirection.Output;
                    command.Parameters.Add(isSucces);
                    SqlParameter result = new SqlParameter("@Result", SqlDbType.NVarChar,50);
                    result.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);
                    SqlParameter userId = new SqlParameter("@UserId", SqlDbType.NVarChar,50);
                    userId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(userId);
                    command.ExecuteNonQuery();
                    _userName = UserName;
                    _userId =userId.Value.ToString();
                    if (_userId == " 0")
                    {
                        _isError = true;
                     
                    }
                    _errorMsg = result.Value.ToString();

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

        public void ChangePassword(string UserId, string UserName, string SecretQuestion, string Answer,string OldPassword,string NewPassword, string LogUserID)
        {
            try
            {
                OpenDb();

                using (SqlCommand command = new SqlCommand("mks_Sys_EditUser", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    
                    command.Parameters.AddWithValue("@SecretQuestion", SecretQuestion);
                    command.Parameters.AddWithValue("@Answer", Answer);
                    SqlParameter success = new SqlParameter("@IsSuccess", SqlDbType.Bit);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(success);
                    SqlParameter result = new SqlParameter("@IsResult", SqlDbType.NVarChar, 50);
                    success.Direction = ParameterDirection.Output;
                    command.Parameters.Add(result);
                    command.Parameters.AddWithValue("@LogUserID", LogUserID);
                    command.ExecuteNonQuery();
                    _errorMsg = result.Value.ToString();
                    if (success.Value.ToString() == "0")
                    {
                        _isError = true;

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
        }





        #region Internal
        /// <summary>
        /// Opens the db.
        /// </summary>
        private void OpenDb()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }
        #endregion



    }
}
