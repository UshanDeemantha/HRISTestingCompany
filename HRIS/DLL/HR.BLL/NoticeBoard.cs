using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.HR.DAL;
using System.Data;

namespace HRM.HR.BLL
{
   public class NoticeBoard
    {
         #region Fields
        NoticeBoardDAL objNoticeBoardDAL = null;
        private DataTable dtNoticeBoards = null;
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
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public NoticeBoard()
        {
            objNoticeBoardDAL = new NoticeBoardDAL();
        } 
        #endregion



        #region Methods

        private void SetValues()
        {
            _isError = objNoticeBoardDAL.IsError;
            _errorMsg = objNoticeBoardDAL.ErrorMsg;
        }


    
       
        /// <summary>
        /// Adds the notice board.
        /// </summary>
        /// <param name="NoticeBoardCode">The notice board code.</param>
        /// <param name="NoticeBoardName">Name of the notice board.</param>
        /// <param name="NoticeBoardDescription">The notice board description.</param>
        /// <param name="NoticeBoardCreateDate">The notice board create date.</param>
        /// <param name="NoticeBoardFromDate">The notice board from date.</param>
        /// <param name="NoticeBoardToDate">The notice board to date.</param>
        public void AddNoticeBoard(string NoticeBoardCode,string NoticeBoardName,string NoticeBoardDescription,DateTime NoticeBoardFromDate,DateTime NoticeBoardToDate )
        {
            try
            {
                objNoticeBoardDAL.AddNoticeBoard( NoticeBoardCode, NoticeBoardName,NoticeBoardDescription, NoticeBoardFromDate, NoticeBoardToDate );
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        
        
        /// <summary>
        /// Updates the notice board.
        /// </summary>
        /// <param name="NoticeBoardId">The notice board id.</param>
        /// <param name="NoticeBoardCode">The notice board code.</param>
        /// <param name="NoticeBoardName">Name of the notice board.</param>
        /// <param name="NoticeBoardDescription">The notice board description.</param>
        /// <param name="NoticeBoardCreateDate">The notice board create date.</param>
        /// <param name="NoticeBoardFromDate">The notice board from date.</param>
        /// <param name="NoticeBoardToDate">The notice board to date.</param>
        public void UpdateNoticeBoard(int NoticeBoardId,string NoticeBoardCode,string NoticeBoardName,string NoticeBoardDescription,DateTime NoticeBoardFromDate,DateTime NoticeBoardToDate ,bool IsActive)
        {
            try
            {
                objNoticeBoardDAL.UpdateNoticeBoard(NoticeBoardId, NoticeBoardCode,NoticeBoardName,NoticeBoardDescription,NoticeBoardFromDate,NoticeBoardToDate,IsActive );
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

       
       
        /// <summary>
        /// Gets the notice board.
        /// </summary>
        /// <param name="NoticeBoardId">The notice board id.</param>
        /// <returns></returns>
        public DataTable GetNoticeBoard(int NoticeBoardId)
        {
            try
            {
                dtNoticeBoards = objNoticeBoardDAL.GetNoticeBoard(NoticeBoardId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtNoticeBoards;
        }


       
        /// <summary>
        /// Deletes the notice board.
        /// </summary>
        /// <param name="NoticeBoardId">The notice board id.</param>
        public void DeleteNoticeBoard(int NoticeBoardId)
        {
            try
            {
                objNoticeBoardDAL.DeleteNoticeBoard(NoticeBoardId);
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
        ~NoticeBoard()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion


    }
}
