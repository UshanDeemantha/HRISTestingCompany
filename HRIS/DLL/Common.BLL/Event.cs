using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.Common.DAL;

namespace HRM.Common.BLL
{
    public class Event
    {
        #region Fields

        EventDAL objEvent;
        DataTable dtEvent;
        private bool _isError;
        private string _errorMsg;

        private int _eventId;
        private int _eventTypeId;
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

        public string EventCode
        {
            get { return _eventCode; }
            set { _eventCode = value; }
        }

        public string EventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }

        public string EventDescription
        {
            get { return _eventDescription; }
            set { _eventDescription = value; }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }

        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        public DateTime EventDate
        {
            get { return _eventDate; }
            set { _eventDate = value; }
        }

        #endregion

        # region Constructor
        public Event()
        {
            objEvent = new EventDAL();
        }
        # endregion

        # region Methods

        public void SetValues()
        {
            _isError = objEvent.IsError;
            _errorMsg = objEvent.ErrorMsg;
        }

        public void AddEvent(int eventTypeId, string eventCode, string eventName, string description,DateTime toDate,DateTime fromDate,DateTime evnetDate)
        {
            try
            {
                objEvent.AddEvent(eventTypeId, eventCode, eventName, description, toDate, fromDate, EventDate);
                SetValues();
            }
            catch (Exception ex)
            {

                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public void UpdateEvent(int eventId,int eventTypeId, string eventCode, string eventName, string description, DateTime toDate, DateTime fromDate, DateTime evnetDate)
        {
            try
            {
                objEvent.UpdateEvent (eventId , eventTypeId, eventCode, eventName, description, toDate, fromDate, EventDate);
                SetValues();
            }
            catch (Exception ex)
            {

                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        public DataTable GetEvent(int eventId)
        {
            try
            {
                dtEvent = objEvent.GetEvent(eventId);
            }
            catch (Exception ex)
            {
                
                _isError = objEvent.IsError;
                _errorMsg = objEvent.ErrorMsg;
            }
            return dtEvent;
        }

        public void DeleteEvent(int eventId)
        {
            try
            {
               objEvent.DeleteEvent(eventId);
            }
            catch (Exception ex)
            {
                _isError = objEvent.IsError;
                _errorMsg = objEvent.ErrorMsg;
            }
        }

        # endregion

        # region Destructor

        ~Event()
        {
            GC.SuppressFinalize(this);
        }

        # endregion
    }
}
