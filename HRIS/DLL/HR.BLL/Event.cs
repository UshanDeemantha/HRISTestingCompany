/// <summary>
/// Solution Name        : HRM
/// Project Name          : HR.BLL
/// Author                     : ushan deemantha
/// Class Description    : Event
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.HR.DAL;

namespace HRM.HR.BLL
{
  
    public class Event
    {
          #region Fields
        EventDAL objEventDAL = null;
        private DataTable dtEvents = null;
        private DataTable dtEventTypes = null;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _eventId = 0;
        private int _eventTypeId = 0;
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

         public int EventTypeId
        {
            get { return _eventTypeId; }
        }

        #endregion



          #region Constructor       
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Event()
        {
            objEventDAL = new EventDAL();
        } 
        #endregion

        #region Methods

        private void SetValues()
        {
            _isError = objEventDAL.IsError;
            _errorMsg = objEventDAL.ErrorMsg;
        }


    
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
                objEventDAL.AddEvent(EventTypeId,EventCode,Name,Description ,FromDate,ToDate, EventDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
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
                objEventDAL.UpdateEvent(EventId, EventTypeId,EventCode, Name,Description, FromDate,ToDate,EventDate,IsActive);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

       
        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="EventId">The event id.</param>
        /// <returns></returns>
        public DataTable GetEvent(int EventId)
        {
            try
            {
                dtEvents = objEventDAL.GetEvent(EventId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtEvents;
        }


        
        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="EventId">The event id.</param>
        public void DeleteEvent(int EventId)
        {
            try
            {
                objEventDAL.DeleteEvent(EventId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
        }





       
        /// <summary>
        /// Adds the type of the event.
        /// </summary>
        /// <param name="EventTypeCode">The event type code.</param>
        /// <param name="EventTypeName">Name of the event type.</param>
        public void AddEventType(string EventTypeCode,string EventTypeName)
        {
            try
            {
                objEventDAL.AddEventType(EventTypeCode,EventTypeName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        
        /// <summary>
        /// Updates the type of the event.
        /// </summary>
        /// <param name="EventTypeId">The event type id.</param>
        /// <param name="EventTypeCode">The event type code.</param>
        /// <param name="EventTypeName">Name of the event type.</param>
        public void UpdateEventType(int EventTypeId,string EventTypeCode,string EventTypeName)
        {
            try
            {
                objEventDAL.UpdateEventType(EventTypeId,EventTypeCode,EventTypeName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <param name="EventTypeId">The event type id.</param>
        /// <returns></returns>
        public DataTable GetEventType(int EventTypeId)
        {
            try
            {
                dtEventTypes = objEventDAL.GetEventType(EventTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtEventTypes;
        }



     
        /// <summary>
        /// Deletes the type of the event.
        /// </summary>
        /// <param name="EventTypeId">The event type id.</param>
        public void DeleteEventType(int EventTypeId)
        {
            try
            {
                objEventDAL.DeleteEventType(EventTypeId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
        } 



        #endregion


         #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Student"/> is reclaimed by garbage collection.
        /// </summary>
        ~Event()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion

    }
}
