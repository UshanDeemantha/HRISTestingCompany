using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.HR.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.HR.BLL
{
    [DataObject]
    public class Course:Institute
    {

         # region Fields
            CourseDAL objCourseDAL;
            DataTable _courses;
            private bool _isError;
            private string _errorMsg;

            private int _courseId;
                      private int _courseTypeId;
        # endregion


        # region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }    

        /// <summary>
        /// Gets the Course id.
        /// </summary>
        /// <value>The Course id.</value>
       public int CourseId
        {
            get { return _courseId; }
        }

        
       

        /// <summary>
        /// Gets or sets the Course type id.
        /// </summary>
        /// <value>The Course type id.</value>
        public int CourseTypeId
        {
            get
            {
                return _courseTypeId;
            }
            set
            {
                _courseTypeId = value;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Course"/> class.
        /// </summary>
        public Course()
        {
            objCourseDAL = new CourseDAL();
        }

        #endregion


        #region Methods
        #region Course
        public void AddCourse(string CourseCode, string CourseName, int CourseTypeId ,int InstituteId,string CourseStreem)
        {
            try
            {
                objCourseDAL.AddCourse(CourseCode, CourseName, CourseTypeId,InstituteId,CourseStreem);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetCourseDetails(int CourseId)
        {
            try
            {
                _courses = objCourseDAL.GetCourseDeatails(CourseId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _courses;
        }

        public void UpdateCourse(int CourseId, string CourseCode, string CourseName, int CourseTypeId,int InstituteId,string CourseStreem)
        {
            try
            {
                objCourseDAL.UpdateCourse(CourseId, CourseCode, CourseName, CourseTypeId,InstituteId,CourseStreem);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteCourse(int CourseId)
        {
            try
            {
                objCourseDAL.DeleteCourse(CourseId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        } 
        #endregion

        #region Course Type
        public void AddCourseType(string CourseTypeCode, string CourseTypeName)
        {
            try
            {
                objCourseDAL.AddCourseType(CourseTypeCode, CourseTypeName);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetCourseTypeDetails(int CourseTypeId)
        {
            try
            {
                _courses = objCourseDAL.GetCourseType(CourseTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _courses;
        }

        public void UpdateCourseType(int CourseTypeId, string CourseTypeCode, string CourseTypeName)
        {
            try
            {
                objCourseDAL.UpdateCourseType(CourseTypeId, CourseTypeCode, CourseTypeName);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void DeleteCourseType(int CourseTypeId)
        {
            try
            {
                objCourseDAL.DeleteCourseType(CourseTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

        }
        public DataTable GetCoursesByTypeId(int CourseTypeID)
        {
            try
            {
                _courses = objCourseDAL.GetCoursesByTypeId(CourseTypeID);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return _courses;
        }
        #endregion
        #endregion

    }
}
