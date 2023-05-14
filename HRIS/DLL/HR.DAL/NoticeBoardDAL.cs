using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HRM.HR.DAL
{
   public class NoticeBoardDAL
    {
        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _noticeBoardId = 0;
      

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
        public int NoticeBoardId
        {
            get { return _noticeBoardId; }
        }
        
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public NoticeBoardDAL()
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


         #region Notice Bord



        /// <summary>
        /// Adds the notice board.
        /// </summary>
        /// <param name="NoticeBordCode">The notice bord code.</param>
        /// <param name="NoticeBoardName">Name of the notice board.</param>
        /// <param name="NoticeBoardDescription">The notice board description.</param>
        /// <param name="NoticeBoardCreateDate">The notice board create date.</param>
        /// <param name="NoticeBoardFromDate">The notice board from date.</param>
        /// <param name="NoticeBoardToDate">The notice board to date.</param>
        public void AddNoticeBoard(string NoticeBoardCode,string NoticeBoardName,string NoticeBoardDescription,DateTime NoticeBoardFromDate,DateTime NoticeBoardToDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddNoticeBoard", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@NoticeBoardCode", NoticeBoardCode);
                    command.Parameters.AddWithValue("@NoticeBoardTitle", NoticeBoardName);
                    command.Parameters.AddWithValue("@NoticeBoardDescription", NoticeBoardDescription);
                    command.Parameters.AddWithValue("@NoticeBoardFromDate", NoticeBoardFromDate);
                    command.Parameters.AddWithValue("@NoticeBoardToDate", NoticeBoardToDate);
                    SqlParameter noticeBoardId = new SqlParameter("@NoticeBoardId", SqlDbType.Int, 8);
                    noticeBoardId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(noticeBoardId);
                    command.ExecuteNonQuery();
                    _noticeBoardId = Convert.ToInt32(noticeBoardId.Value);
                    if (_noticeBoardId < 0)
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
        /// Updates the notice board.
        /// </summary>
        /// <param name="NoticeBoardId">The notice board id.</param>
        /// <param name="NoticeBordCode">The notice bord code.</param>
        /// <param name="NoticeBoardName">Name of the notice board.</param>
        /// <param name="NoticeBoardDescription">The notice board description.</param>
        /// <param name="NoticeBoardCreateDate">The notice board create date.</param>
        /// <param name="NoticeBoardFromDate">The notice board from date.</param>
        /// <param name="NoticeBoardToDate">The notice board to date.</param>
        public void UpdateNoticeBoard(int NoticeBoardId,string NoticeBordCode, string NoticeBoardName, string NoticeBoardDescription,  DateTime NoticeBoardFromDate, DateTime NoticeBoardToDate,bool IsActive)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateNoticeBoard", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@NoticeBoardId", NoticeBoardId);
                    command.Parameters.AddWithValue("@NoticeBoardCode", NoticeBordCode);
                    command.Parameters.AddWithValue("@NoticeBoardTitle", NoticeBoardName);
                    command.Parameters.AddWithValue("@NoticeBoardDescription", NoticeBoardDescription);
                    command.Parameters.AddWithValue("@NoticeBoardFromDate", NoticeBoardFromDate);
                    command.Parameters.AddWithValue("@NoticeBoardToDate", NoticeBoardToDate);
                    command.Parameters.AddWithValue("@Active", IsActive);
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
        /// Gets the notice board.
        /// </summary>
        /// <param name="NoticeBoardId">The notice board id.</param>
        /// <returns></returns>
        public DataTable GetNoticeBoard(int NoticeBoardId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetNoticeBoard", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@NoticeBoardId", NoticeBoardId);
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


       
        public void DeleteNoticeBoard(int NoticeBoardId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteNoticeBoard", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NoticeBoardId", NoticeBoardId);
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
        ~NoticeBoardDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
