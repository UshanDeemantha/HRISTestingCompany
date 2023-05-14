
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HRM.HR.DAL
{
 
    public class CourseDAL
    {

        #region Fields
            private SqlConnection _dbConnection;
            private bool _isError;
            private string _errorMsg;

            private int _courseId;
            private int _courseTypeId;
          
        #endregion

        # region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        public string  ErrorMsg
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
        /// Gets or sets the Course code.
        /// </summary>
        /// <value>The Course code.</value>
      
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
            
        }

        #endregion

        #region Constructor

        public CourseDAL()
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

        #region Course

        /// <summary>
        /// Adds the course.
        /// </summary>
        /// <param name="CourseCode">The course code.</param>
        /// <param name="CourseName">Name of the course.</param>
        /// <param name="CourseTypeId">The course type id.</param>
        /// <param name="InstituteId">The institute id.</param>
        /// <param name="CourseStreem">The course streem.</param>
        public void AddCourse(string CourseCode, string CourseName, int CourseTypeId,int InstituteId,string CourseStreem)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddCourse", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CourseCode", CourseCode);
                    command.Parameters.AddWithValue("@CourseName", CourseName);
                    command.Parameters.AddWithValue("@CourseTypeID", CourseTypeId);
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
                    command.Parameters.AddWithValue("@CourseStreem", CourseStreem);
                    SqlParameter CourseId = new SqlParameter("@CourseID", SqlDbType.Int);
                    CourseId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(CourseId);

                    command.ExecuteNonQuery();
                    _courseId = Convert.ToInt32(CourseId.Value);
                    if (_courseId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
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
        /// Updates the course.
        /// </summary>
        /// <param name="CourseId">The course id.</param>
        /// <param name="CourseCode">The course code.</param>
        /// <param name="CourseName">Name of the course.</param>
        /// <param name="CourseTypeId">The course type id.</param>
        /// <param name="Institute">The institute.</param>
        /// <param name="CourseStreem">The course streem.</param>
        public void UpdateCourse(int CourseId, string CourseCode, string CourseName, int CourseTypeId,int InstituteId,string CourseStreem)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateCourse", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CourseID", CourseId);
                    command.Parameters.AddWithValue("@CourseCode", CourseCode);
                    command.Parameters.AddWithValue("@CourseName", CourseName);
                    command.Parameters.AddWithValue("@CourseTypeID", CourseTypeId);
                    command.Parameters.AddWithValue("@InstituteId", InstituteId);
                    command.Parameters.AddWithValue("@CourseStreem", CourseStreem);
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
        /// Gets the Course deatails.
        /// </summary>
        /// <param name="CourseId">CourseId as int</param>
        /// <returns>Retrun Datatable</returns>
        public DataTable GetCourseDeatails(int CourseId)
        {

            DataTable CourseDetails = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetCourse", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CourseId", CourseId);
                    SqlDataAdapter adaptor = new SqlDataAdapter(command);
                    adaptor.Fill(CourseDetails);
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
            return CourseDetails;
        }

        /// <summary>
        /// Deletes the Course.
        /// </summary>
        /// <param name="CourseId">The Course id.</param>
        public void DeleteCourse(int CourseId)
        {

           
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteCourse", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CourseId", CourseId);
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


        #endregion

        #region Course Types
        /// <summary>
        /// Adds the type of the Course.
        /// </summary>
        /// <param name="CourseTypeCode">The Course type code.</param>
        /// <param name="CourseTypeName">Name of the Course type.</param>
        public void AddCourseType(string CourseTypeCode, string CourseTypeName)
        {
            try 
            {
                using (SqlCommand command = new SqlCommand("HR_AddCourseType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CourseTypeCode",CourseTypeCode);
                    command.Parameters.AddWithValue("@CourseTypeName",CourseTypeName);
                    SqlParameter CourseType = new SqlParameter("@CourseTypeId", SqlDbType.Int);
                    CourseType.Direction = ParameterDirection.Output;
                    command.Parameters.Add(CourseType);

                    command.ExecuteNonQuery();
                    _courseTypeId = Convert.ToInt32(CourseType.Value);
                    if (_courseTypeId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
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
        /// Updates the type of the Course.
        /// </summary>
        /// <param name="CourseTypeId">The Course type id.</param>
        /// <param name="CourseTypeCode">The Course type code.</param>
        /// <param name="CourseTypeName">Name of the Course type.</param>
        public void UpdateCourseType(int CourseTypeId, string CourseTypeCode, string CourseTypeName)
        {
            try
            {
                using(SqlCommand command = new SqlCommand("HR_UpdateCourseType",_dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CourseTypeId", CourseTypeId);
                    command.Parameters.AddWithValue("@CourseTypeCode",CourseTypeCode);
                    command.Parameters.AddWithValue("@CourseTypeName", CourseTypeName);
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
        /// Gets the type of the Course.
        /// </summary>
        /// <param name="CourseTypeId">CourseTypeId as int</param>
        /// <returns>Returns DataTable </returns>
        public DataTable GetCourseType(int CourseTypeId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("HR_GetCourseType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.Add("@CourseTypeID", CourseTypeId);
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
        }


        public void DeleteCourseType(int CourseTypeId)
        {

          
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteCourseType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CourseTypeId", CourseTypeId);
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

        public DataTable GetCoursesByTypeId(int CourseTypeID)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("HR_GetCoursesByTypeId", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.Add("@CourseTypeID", CourseTypeID);
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
        }

        #endregion

        #endregion

    }
}
