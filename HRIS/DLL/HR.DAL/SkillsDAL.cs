/// <summary>
/// Solution Name       : HRM
/// Project Name        : HR.DAL
/// Author              : ushan deemantha
/// Class Description   : Skills DAL
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HRM.HR.DAL
{
 
    public class SkillsDAL
    {
        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError=false;
        private string _errorMsg=string.Empty;
        private int _skillId=0;
        private int _skillGradeId=0;
        private long _employeeSkillId = 0;
        private long _employeeId = 0;
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

        public int SkillId
        {
            get { return _skillId; }
            set {_skillId=value; }
        }
     
        public int SkillGradeId 
        {
            get { return _skillGradeId; }
            set { _skillGradeId = value; }
        }

        public long employeeSkillId
        {
            get { return _employeeSkillId; }
            set { _employeeSkillId=value;}
        }

        public long EmployeeId
        {
            get{return _employeeId;}
            set{_employeeId=value;}
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public SkillsDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }



        #region Skill
        /// <summary>
        /// Add  the Skill.
        /// <param name="SkillId">The Skill Id As int .</param>
        /// <param name="SkillCode">The SkillCode As int .</param>
        /// <param name="SkillName">The SkillName As string.</param>
        /// <param name="Description">The Description As string.</param>
        ///  <summary> 
        public void AddSkill(string SkillCode, string SkillName, string Description)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddSkill", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SkillCode", SkillCode);
                    command.Parameters.AddWithValue("@SkillName", SkillName);
                    command.Parameters.AddWithValue("@Description", Description);
                    SqlParameter skillId = new SqlParameter("@SkillId", SqlDbType.Int, 8);
                    skillId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(skillId);
                    command.ExecuteNonQuery();
                    _skillId = Convert.ToInt32(skillId.Value);
                    if (_skillId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
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
        /// Update the Skill.
        /// <param name="SkillId">The Skill Id .</param>
        /// <param name="SkillCode">The SkillCode .</param>
        /// <param name="SkillName">The SkillName.</param>
        /// <param name="Description">The Description.</param>
        ///  <summary> 
        public void UpdateSkill(int SkillId, string SkillCode, string SkillName, string Description)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateSkill", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SkillCode", SkillCode);
                    command.Parameters.AddWithValue("@SkillName", SkillName);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@SkillId", SkillId);                   
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
        /// Gets the skill.
        /// </summary>
        /// <param name="SkillId">The skill id.</param>
        /// <returns></returns>
        public DataTable GetSkill(int SkillId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetSkill", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SkillID", SkillId);
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
        /// Deletes the skill.
        /// </summary>
        /// <param name="SkillId">The skill id As int.</param>
        public void DeleteSkill(int SkillId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteSkill", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SkillId", SkillId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _isError = true;
                if (ex.Number == 547)
                {
                    _errorMsg = "Already Assign This Code";
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
        #endregion


        #region Skill Grade



        /// <summary>
        /// Add  the SkillGrade.

        /// <param name="SkillGradeCode">The SkillGradeCode  As string.</param>
        /// <param name="SkillGradeName">The SkillGradeName As string.</param>
        ///  <summary> 
        public void AddSkillGrade(string SkillGradeCode, string SkillGradeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddSkillGrade", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SkillGradeCode", SkillGradeCode);
                    command.Parameters.AddWithValue("@SkillGradeName", SkillGradeName);
                    SqlParameter skillGradeId = new SqlParameter("@SkillGradeID", SqlDbType.Int, 8);
                    skillGradeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(skillGradeId);
                    command.ExecuteNonQuery();
                    _skillGradeId = Convert.ToInt32(skillGradeId.Value);
                    if (_skillGradeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
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
        ///Update  the SkillGrade.
        /// <param name="SkillGradeId">The SkillGradeId  .</param>
        /// <param name="SkillGradeCode">The SkillGradeCode .</param>
        /// <param name="SkillGradeName">The SkillGradeName.</param>
        ///  <summary> 
        public void UpdateSkillGrade(int SkillGradeId, string SkillGradeCode, string SkillGradeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateSkillGrade", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SkillGradeCode", SkillGradeCode);
                    command.Parameters.AddWithValue("@SkillGradeName", SkillGradeName);
                    command.Parameters.AddWithValue("@SkillGradeID", SkillGradeId);
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
        /// Gets the skill grade.
        /// </summary>
        /// <param name="SkillGradeId">The skill grade id as int.</param>
        /// <returns></returns>
        public DataTable GetSkillGrade(int SkillGradeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetSkillGrade", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SkillGradeId", SkillGradeId);
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
        /// Deletes the skill grade.
        /// </summary>
        /// <param name="SkillGradeId">The skill grade id as int.</param>
        public void DeleteSkillGrade(int SkillGradeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteSkillGrade", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@SkillGradeId", SkillGradeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _isError = true;
                if (ex.Number == 547)
                {
                    _errorMsg = "Already Assign This Code";
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
        #endregion

        #region EmployeeSkill
        /// <summary>
        /// Adds the employee skill.
        /// </summary>
        /// <param name="EmployeeId">The employee id As int.</param>
        /// <param name="SkillId">The skill id as int.</param>
        /// <param name="SkillGradeId">The skill grade id as int.</param>
        public void AddEmployeeSkill(long EmployeeId, int SkillId, int SkillGradeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeSkill", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@SkillId", SkillId);
                    command.Parameters.AddWithValue("@SkillGradeId", SkillGradeId);

                    SqlParameter employeeSkillId = new SqlParameter("@EmployeeSkillId", SqlDbType.BigInt, 16);
                    employeeSkillId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeSkillId);
                    command.ExecuteNonQuery();
                    _employeeSkillId = Convert.ToInt64(employeeSkillId.Value);
                    if (_employeeSkillId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
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
        /// Upadates the employee skill.
        /// </summary>
        /// <param name="EmployeeSkillId">The employee skill id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="SkillId">The skill id as int .</param>
        /// <param name="SkillGradeId">The skill grade id as int.</param>
        public void UpadateEmployeeSkill(long EmployeeSkillId, long EmployeeId, int SkillId, int SkillGradeId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeSkill", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@SkillId", SkillId);
                    command.Parameters.AddWithValue("@SkillGradeId", SkillGradeId);
                    command.Parameters.AddWithValue("@EmployeeSkillID", EmployeeSkillId);
                    SqlParameter employeeSkillId = new SqlParameter("@EmployeeSkillId_Out", SqlDbType.BigInt, 16);
                    employeeSkillId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeSkillId);
                    command.ExecuteNonQuery();
                    _employeeSkillId = Convert.ToInt64(employeeSkillId.Value);
                    
                    if (_employeeSkillId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
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
        /// Gets the employee skill.
        /// </summary>
        /// <param name="EmployeeSkillId">The employee skill id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeSkill(long EmployeeSkillId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeSkill", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeSkillId", EmployeeSkillId);
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
        /// Gets the employee skill.
        /// </summary>
        /// <param name="EmployeeSkillId">The employee skill id.</param>
        /// <returns></returns>
        public DataTable GetEmployeeSkillByEmployeeId(long EmployeeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeSkillByEmployeeId", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeId);
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
        /// Deletes the employee skill.
        /// </summary>
        /// <param name="EmployeeSkillId">The employee skill id as int.</param>
        public void DeleteEmployeeSkill(long EmployeeSkillId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeSkill", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeSkillId", EmployeeSkillId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _isError = true;
                if (ex.Number == 547)
                {
                    _errorMsg = "Already Assign This Code";
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

        #endregion

        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="StudentDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~SkillsDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
