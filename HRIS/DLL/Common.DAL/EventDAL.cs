using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HRM.Common.DAL
{
    public class EventDAL
    {
        #region Fields

            private SqlConnection _dbConnection;
            private bool _isError;
            private string _errorMsg;

            private int _eventId;
            private int _eventTypeId;
            private string _eventTypeCode;
            private string  _eventTypeName;
            private string _eventCode;
            private string _eventName;
            private string _eventDescription;
            private DateTime _toDate;
            private DateTime _fromDate;
            private DateTime _eventDate;

        #endregion

        #region Propreties

            public bool IsError
            {
                get { return _isError; }
            }

            public string ErrorMsg
            {
                get { return _errorMsg; }
            }

            public int EventId 
            {
                get { return _eventId; }
                set { _eventId = value; }
            }

            public int eventTypeId 
            { 
                get { return _eventTypeId; }
                set { _eventTypeId = value; }
            }

            public string  EventTypeCode 
            {
                get { return _eventTypeCode; }
                set { _eventTypeCode = value; }
            }

            public string EventTypeName 
            {
                get { return _eventTypeName; }
                set { _eventTypeName = value; }
            }

            public string  EventCode
            { 
                get { return _eventCode; }
                set { _eventCode = value; }
            }

            public string  EventName 
            {
                get { return _eventName; }
                set { _eventName = value; }
            }

            public string  EventDescription
            {
                get { return _eventDescription; }
                set { _eventDescription = value; }
            }

            public DateTime  ToDate
            {
                get { return _toDate; }
                set { _toDate = value; }
            }

            public DateTime  FromDate 
            {
                get { return _fromDate; }
                set { _fromDate = value; }
            }

            public DateTime  EventDate 
            {
                get { return _eventDate; }
                set { _eventDate = value; }
            }

        #endregion

        #region Constructor
            public EventDAL ()
            {
                _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connnectionstring"].ConnectionString);
            }
        #endregion

        #region Methods

            public void OpenDb()
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection.Open();
                }
            }

            #region EventType

            public void AddEventType(int eventTypeId, string eventTypeCode, string eventTypeName)
            {
                try
                {
                    using(SqlCommand command = new SqlCommand("COM_AddEventType",_dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDb();

                        command.Parameters.AddWithValue("@EventTypeCode",eventTypeCode);
                        command.Parameters.AddWithValue("@EventTypeName",eventTypeName);

                        SqlParameter EventTypeId = new SqlParameter("@EventTypeID", SqlDbType.Int);
                        EventTypeId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(EventTypeId);

                        command.ExecuteNonQuery();
                        _eventTypeId = Convert.ToInt32(EventTypeId.Value);

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

            public void UpdateEventType(int eventTypeId, string eventTypeCode, string eventTypeName)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_UpdateEventType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDb();

                        command.Parameters.AddWithValue("@EventTypeID",eventTypeId);
                        command.Parameters.AddWithValue("@EventTypeCode",eventTypeCode);
                        command.Parameters.AddWithValue("@EventTypeName",eventTypeName);

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

            public void DeleteEventType(int eventTypeId)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_DeleteEventType", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDb();

                        command.Parameters.AddWithValue("@EventTypeID",eventTypeId);
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

            public DataTable GetEventTypes(int eventTypeId)
            { 
                using(DataTable dtTable = new DataTable())
                {
                    try 
	                {	        
                		using(SqlCommand command  = new SqlCommand("COM_GetEventType",_dbConnection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OpenDb();

                            command.Parameters.AddWithValue("@EventTypeID",eventTypeId);

                            using(SqlDataAdapter daAdapter = new SqlDataAdapter(command))
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
                    return dtTable;
                }
            }
            #endregion

            #region Events
            public void AddEvent(int eventTypeId, string eventCode, string eventName, string description,DateTime toDate,DateTime fromDate,DateTime evnetDate)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_AddEvent",_dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDb();

                        command.Parameters.AddWithValue("@EventTypeID",eventTypeId);
                        command.Parameters.AddWithValue("@EventCode",eventCode);
                        command.Parameters.AddWithValue("@Name",eventName);
                        command.Parameters.AddWithValue("@Description",description);
                        command.Parameters.AddWithValue("@ToDate",toDate);
                        command.Parameters.AddWithValue("@FromDate",fromDate);
                        command.Parameters.AddWithValue("@EventDate",EventDate);

                        SqlParameter eventId = new SqlParameter("@EventId", SqlDbType.Int);
                        eventId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(eventId);

                        command.ExecuteNonQuery();
                        _eventId = Convert.ToInt32(eventId);
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

            public void UpdateEvent(int eventId, int eventTypeId, string eventCode, string eventName, string description, DateTime toDate, DateTime fromDate, DateTime evnetDate)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_UpdateEvent", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDb();

                        command.Parameters.AddWithValue("@EventId", EventId);
                        command.Parameters.AddWithValue("@EventTypeID", eventTypeId);
                        command.Parameters.AddWithValue("@EventCode", eventCode);
                        command.Parameters.AddWithValue("@Name", eventName);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@ToDate", toDate);
                        command.Parameters.AddWithValue("@FromDate", fromDate);
                        command.Parameters.AddWithValue("@EventDate", EventDate);

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

            public void DeleteEvent(int eventId)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_DeleteEvent", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDb();

                        command.Parameters.AddWithValue("@EventId", EventId);
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

            public DataTable  GetEvent(int eventId)
            {
                using (DataTable dtTable = new DataTable())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("COM_GetEvent", _dbConnection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OpenDb();

                            command.Parameters.AddWithValue("@EventId",eventId);

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
                    return dtTable;
                }
            }
            #endregion

        #endregion

        #region Destructor
            ~EventDAL()
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
                GC.SuppressFinalize(this);
            }
        #endregion

    }
}
