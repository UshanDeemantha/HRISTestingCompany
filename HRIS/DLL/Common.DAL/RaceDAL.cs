using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HRM.Common.DAL
{
   
    public class RaceDAL
    {
          #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _raceId = 0;
      

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
        public int RaceId
        {
            get { return _raceId; }
        }
        
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public RaceDAL()
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


         #region Race




      
        /// <summary>
        /// Adds the race.
        /// </summary>
        /// <param name="RaceName">Name of the race.</param>
        public void AddRace(string RaceName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_AddRace", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@RaceName", RaceName);
                   SqlParameter raceId = new SqlParameter("@RaceId", SqlDbType.Int, 8);
                   raceId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(raceId);
                    command.ExecuteNonQuery();
                    _raceId = Convert.ToInt32(raceId.Value);
                    if (_raceId < 0)
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
        /// Updates the race.
        /// </summary>
        /// <param name="RaceId">The race id.</param>
        /// <param name="RaceName">Name of the race.</param>
        public void UpdateRace(int RaceId, string RaceName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("COM_UpdateRace", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@RaceId", RaceId);
                    command.Parameters.AddWithValue("@RaceName", RaceName);
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
        /// Gets the race.
        /// </summary>
        /// <param name="RaceId">The race id.</param>
        /// <returns></returns>
        public DataTable GetRace(int RaceId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("COM_GetRace", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@RaceId", RaceId);
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
        /// Deletes the race.
        /// </summary>
        /// <param name="RaceId">The race id.</param>
        public void DeleteRace(int RaceId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("COM_DeleteRace", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceId", RaceId);
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
        ~RaceDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
