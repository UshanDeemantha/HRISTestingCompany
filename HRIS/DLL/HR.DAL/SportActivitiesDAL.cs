/// <summary>
///  /// <summary>
///  /// Solution Name : HRM
///  /// Project Name : HRM.HR.DAL
///  /// Author : ushan deemantha
///  /// Class Description : SportActivitiesDAL
/// /// </summary>
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace HRM.HR.DAL
{
  
    public class SportActivitiesDAL
    {

        #region Fields

        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;
        private int _sqlErrorNo;

        private int _sportId;
        private string _sportCode;
        private string _sportName;
        private bool _active;
 
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

        /// <summary>
        /// Gets the SQL error number.
        /// </summary>
        /// <value>The SQL error number.</value>
        public int SqlErrorNumber
        {
            get { return _sqlErrorNo; }
        }



        /// <summary>
        /// Gets the sport id.
        /// </summary>
        /// <value>The sport id.</value>
        public int SportId
        {
            get
            {
                return _sportId;
            }
        }

        /// <summary>
        /// Gets or sets the sport code.
        /// </summary>
        /// <value>The sport code.</value>
        public string SportCode
        {
            get
            {
                return _sportCode;
            }
            set
            {
                _sportCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the sport.
        /// </summary>
        /// <value>The name of the sport.</value>
        public string SportName
        {
            get
            {
                return _sportName;
            }
            set
            {
                _sportName = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SportActivities"/> class.
        /// </summary>
        public SportActivitiesDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SportActivities"/> class.
        /// </summary>
        /// <param name="SportId">The sport id. As Int</param>
        public SportActivitiesDAL(int SportId)
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            _sportId = SportId;
        } 
        #endregion

        #region Methods

        /// <summary>
        /// Opens the DB.
        /// </summary>
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        /// <summary>
        /// Adds the sport activity.
        /// </summary>
        public void AddSportActivity()
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddSportActivities", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@SportCode", _sportCode);
                    command.Parameters.AddWithValue("@SportName", _sportName);
                    SqlParameter sprtId = new SqlParameter("@SportID", SqlDbType.Int, 16);
                    sprtId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(sprtId);
                    command.ExecuteNonQuery();
                    _sportId = Convert.ToInt32(sprtId.Value);

                }
            }
            catch (SqlException sqlEx)
            {
                _isError = true;
                _sqlErrorNo = sqlEx.Number;
                _errorMsg = sqlEx.Message;
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
        /// Updates the sport activities.
        /// </summary>
        /// <param name="SprortId">The sprort id. As Int</param>
        public void UpdateSportActivities(int SprortId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateSportActivities", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@SportID", SprortId);
                    command.Parameters.AddWithValue("@SportCode", _sportCode);
                    command.Parameters.AddWithValue("@SportName", _sportName);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                _isError = true;
                _sqlErrorNo = sqlEx.Number;
                _errorMsg = sqlEx.Message;
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
        /// Deletes the sport activities.
        /// </summary>
        /// <param name="SportId">The sport id As Int</param>
        public void DeleteSportActivities(int SportId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteSportActivities", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@SportID", SportId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                _isError = true;
                _sqlErrorNo = sqlEx.Number;
                _errorMsg = sqlEx.Message;
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

        #region Distructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SportActivities"/> is reclaimed by garbage collection.
        /// </summary>
        ~SportActivitiesDAL()
        {
            GC.SuppressFinalize(this);
        } 
        #endregion

    }
}
