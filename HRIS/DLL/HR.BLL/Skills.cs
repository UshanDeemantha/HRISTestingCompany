
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.HR.DAL;
using System.ComponentModel;

namespace HRM.HR.BLL
{
   [DataObject]
    public class Skills
    {

        #region Fields
        SkillsDAL objSkillsDAL=null; 
        private DataTable dtSkills=null;
        private DataTable dtSkillGrades=null;
        private DataTable dtEmployeeSkills = null;
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
            set { _skillId = value; }
        }
     
        public int SkillGradeId
        {
            get { return _skillGradeId; }
            set { _skillGradeId = value; }
        }

        public long EmployeeSkillId
        {
            get { return _employeeSkillId; }
            set { _employeeSkillId = value; }
        }
        public long EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }
        #endregion

        #region Constructor       
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Skills()
        {
           objSkillsDAL=new SkillsDAL();
        } 
        #endregion

        #region Methods

        private void SetValues()
        {
            _isError = objSkillsDAL.IsError;
            _errorMsg = objSkillsDAL.ErrorMsg;
        }

        #region Skill
        /// <summary>
        /// Adds the skill.
        /// </summary>
        /// <param name="SkillId">The skill id.</param>
        /// <param name="SkillCode">The skill code.</param>
        /// <param name="SkillName">Name of the skill.</param>
        /// <param name="Description">The description.</param>
        public void AddSkill(string SkillCode, string SkillName, string Description)
        {
            try
            {
                objSkillsDAL.AddSkill(SkillCode, SkillName, Description);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the skill.
        /// </summary>
        /// <param name="SkillId">The skill id.</param>
        /// <param name="SkillCode">The skill code.</param>
        /// <param name="SkillName">Name of the skill.</param>
        /// <param name="Description">The description.</param>
        public void UpdateSkill(int SkillId, string SkillCode, string SkillName, string Description)
        {
            try
            {
                objSkillsDAL.UpdateSkill(SkillId, SkillCode, SkillName, Description);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Gets the skill.
        /// </summary>
        /// <param name="SkillId">The skill id.</param>
        /// <returns></returns>
        public DataTable GetSkill(int SkillId)
        {
            try
            {
                dtSkills = objSkillsDAL.GetSkill(SkillId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtSkills;
        }


        /// <summary>
        /// Deletes the skill.
        /// </summary>
        /// <param name="SkillId">The skill id As int.</param>
        public void DeleteSkill(int SkillId)
        {
            try
            {
                objSkillsDAL.DeleteSkill(SkillId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        } 
        #endregion

        #region Skill Grade

        /// <summary>
        /// Adds the skill grade.
        /// </summary>
        /// <param name="SkillGradeCode">The SkillGradeCode As string.</param>
        /// <param name="SkillGradeName">The SkillGradeName As strinh.</param>
        public void AddSkillGrade(string SkillGradeCode, string SkillGradeName)
        {
            try
            {
                objSkillsDAL.AddSkillGrade(SkillGradeCode, SkillGradeName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }
        /// <summary>
        /// Updates the skill grade.
        /// </summary>
        /// <param name="SkillGradeId">The SkillGradeId As int.</param>
        /// <param name="SkillGradeCode">The SkillGradeCode As string.</param>
        /// <param name="SkillGradeName">The SkillGradeName As strinh.</param>
        public void UpdateSkillGrade(int SkillGradeId, string SkillGradeCode, string SkillGradeName)
        {
            try
            {
                objSkillsDAL.UpdateSkillGrade(SkillGradeId, SkillGradeCode, SkillGradeName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }



        /// <summary>
        /// Gets the skill grade.
        /// </summary>
        /// <param name="SkillId">The SkillId As int.</param>
        /// <returns></returns>
        public DataTable GetSkillGrade(int SkillGradeId)
        {
            try
            {
                dtSkillGrades = objSkillsDAL.GetSkillGrade(SkillGradeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtSkillGrades;
        }

        /// <summary>
        /// Deletes the skill grade.
        /// </summary>
        /// <param name="SkillGradeId">The skill grade id as int.</param>
        public void DeleteSkillGrade(int SkillGradeId)
        {
            try
            {
                objSkillsDAL.DeleteSkillGrade(SkillGradeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
        } 
        #endregion

        #region EmployeeSkill
        /// <summary>
        /// Adds the employee skill.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="SkillId">The skill id as int.</param>
        /// <param name="SkillGrade">The skill grade as int.</param>
        public void AddEmployeeSkill(long EmployeeId, DataTable tbl)
        {
            try
            {
                foreach (DataRow row in tbl.Rows)
                {

                    objSkillsDAL.AddEmployeeSkill(EmployeeId, Convert.ToInt32(row["SkillID"].ToString()), Convert.ToInt32(row["SkillGradeID"].ToString()));
                    SetValues();

                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        public void AddEmployeeSkill2(long EmployeeId, int SkillId, int SkillGradeId)
        {
            try
            {
                objSkillsDAL.AddEmployeeSkill(EmployeeId, SkillId, SkillGradeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        /// <summary>
        /// Updates the employee skill.
        /// </summary>
        /// <param name="EmployeeSkillId">The employee skill id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="SkillId">The skill id as int.</param>
        /// <param name="SkillGrade">The skill grade as int.</param>
        public void UpdateEmployeeSkill(long EmployeeSkillId,long EmployeeId, int SkillId, int SkillGradeId)
        {
            try
            {
                objSkillsDAL.UpadateEmployeeSkill( EmployeeSkillId,EmployeeId,  SkillId, SkillGradeId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the employee skill.
        /// </summary>
        /// <param name="EmployeeSkillId">The employee skill id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeSkill(int EmployeeSkillId)
        {
            try
            {
                dtEmployeeSkills = objSkillsDAL.GetEmployeeSkill(EmployeeSkillId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtEmployeeSkills;
        }

          [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeSkillByEmployeeId(long EmployeeId)
        {
            try
            {
                dtEmployeeSkills = objSkillsDAL.GetEmployeeSkillByEmployeeId(EmployeeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtEmployeeSkills;
        }

        /// <summary>
        /// Deletes the employee skill.
        /// </summary>
        /// <param name="EmployeeSkillId">The employee skill id as int.</param>
        public void DeleteEmployeeSkill(int EmployeeSkillId)
        {
            try
            {
                objSkillsDAL.DeleteEmployeeSkill(EmployeeSkillId);
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
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~Skills()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
