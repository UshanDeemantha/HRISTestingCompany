/// <summary>
/// Solution Name        : HRM
/// Project Name          : HR.DAL
/// Author                     : ushan deemantha
/// Class Description    : Event DAL
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HRM.HR.DAL
{
   
    public class EventDAL
    {
         #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _eventId = 0;
        private long _eventTypeId = 0;
        
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
        public int EventId
        {
            get { return _eventId; }
        }
        public long EventTypeId
        {

            get { return _eventTypeId; }
        }

        #endregion


          #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public EventDAL()
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


         #region Event

        
        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="EventTypeId">The event type id.</param>
        /// <param name="EventCode">The event code.</param>
        /// <param name="Name">The name.</param>
        /// <param name="Description">The description.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="ToDate">To date.</param>
        /// <param name="EventDate">The event date.</param>
        public void AddEvent(int EventTypeId,string EventCode,string Name,string Description,DateTime FromDate,DateTime ToDate,DateTime EventDate)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEvent", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EventTypeId", EventTypeId);
                    command.Parameters.AddWithValue("@EventCode", EventCode);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    command.Parameters.AddWithValue("@EventDate", EventDate);
                    SqlParameter eventId = new SqlParameter("@EventId", SqlDbType.Int, 8);
                    eventId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(eventId);
                    command.ExecuteNonQuery();
                    _eventId = Convert.ToInt32(eventId.Value);
                    if (_eventId < 0)
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
        /// Updates the event.
        /// </summary>
        /// <param name="EventId">The event id.</param>
        /// <param name="EventTypeId">The event type id.</param>
        /// <param name="EventCode">The event code.</param>
        /// <param name="Name">The name.</param>
        /// <param name="Description">The description.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="ToDate">To date.</param>
        /// <param name="EventDate">The event date.</param>
        public void UpdateEvent(int EventId,int EventTypeId,string EventCode,string Name,string Description,DateTime FromDate,DateTime ToDate,DateTime EventDate,bool IsActive)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEvent", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EventId", EventId);
                    command.Parameters.AddWithValue("@EventTypeId", EventTypeId);
                   command.Parameters.AddWithValue("@EventCode", EventCode);
                    command.Parameters.AddWithValue("@Name", Name);
                     command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                     command.Parameters.AddWithValue("@ToDate", ToDate);
                     command.Parameters.AddWithValue("@EventDate", EventDate);
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
        /// Gets the event.
        /// </summary>
        /// <param name="EventId">The event id.</param>
        /// <returns></returns>
        public DataTable GetEvent(int EventId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEvent", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EventId", EventId);
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
        /// Deletes the Event.
        /// </summary>
        /// <param name="EventId">The Event id as int.</param>
        public void DeleteEvent(int EventId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteEvent", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EventId", EventId);
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


        #region Event Type
       
        /// <summary>
        /// Adds the type of the event.
        /// </summary>
        /// <param name="EventTypeCode">The event type code.</param>
        /// <param name="EventTypeName">Name of the event type.</param>
        public void AddEventType(string EventTypeCode,string EventTypeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEventType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EventTypeCode", EventTypeCode);
                    command.Parameters.AddWithValue("@EventTypeName", EventTypeName);
                    SqlParameter eventTypeId = new SqlParameter("@EventTypeId", SqlDbType.Int, 8);
                    eventTypeId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(eventTypeId);
                    command.ExecuteNonQuery();
                    _eventTypeId = Convert.ToInt32(eventTypeId.Value);
                    if (_eventTypeId < 0)
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
        /// Updates the type of the event.
        /// </summary>
        /// <param name="EventTypeId">The event type id.</param>
        /// <param name="EventTypeCode">The event type code.</param>
        /// <param name="EventTypeName">Name of the event type.</param>
        public void UpdateEventType(int EventTypeId, string EventTypeCode, string EventTypeName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEventType", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EventTypeId", EventTypeId);
                    command.Parameters.AddWithValue("@EventTypeCode", EventTypeCode);
                    command.Parameters.AddWithValue("@EventTypeName", EventTypeName);
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
        /// Gets the type of the event.
        /// </summary>
        /// <param name="EventTypeId">The event type id.</param>
        /// <returns></returns>
        public DataTable GetEventType(int EventTypeId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEventType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EventTypeId", EventTypeId);
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



        
        public void DeleteEventType(int EventTypeId)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("HR_DeleteEventType", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EventTypeId", EventTypeId);
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
        ~EventDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
