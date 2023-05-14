/// <summary>
/// Solution Name        : HRM
/// Project Name          : Common.BLL
/// Author                     : ushan deemantha
/// Created Timestamp : 3/31/2011
/// Class Description    : Race
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.Common.DAL;
using System.Data;
using System.ComponentModel;

namespace HRM.Common.BLL
{
     [DataObject]
    public class Race
    {
 #region Fields
        RaceDAL objRaceDAL = null;
        private DataTable dtRaces = null;
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
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Race()
        {
            objRaceDAL = new RaceDAL();
        } 
        #endregion



        #region Methods

        private void SetValues()
        {
            _isError = objRaceDAL.IsError;
            _errorMsg = objRaceDAL.ErrorMsg;
        }


    
       
   
    
        /// <summary>
        /// Adds the Race.
        /// </summary>
        /// <param name="RaceName">Name of the Race.</param>
        public void AddRace(string RaceName)
        {
            try
            {
                objRaceDAL.AddRace(RaceName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        
      
       
        /// <summary>
        /// Updates the race.
        /// </summary>
        /// <param name="RaceId">The race id.</param>
        /// <param name="RaceName">Name of the race.</param>
        public void UpdateRace(int RaceId,string RaceName)
        {
            try
            {
                objRaceDAL.UpdateRace(RaceId,RaceName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

       
       
     
       
        /// <summary>
        /// Gets the race.
        /// </summary>
        /// <param name="RaceId">The race id.</param>
        /// <returns></returns>
        public DataTable GetRace(int RaceId)
        {
            try
            {
                dtRaces = objRaceDAL.GetRace(RaceId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtRaces;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetRaceList(int RaceId)
        {
            try
            {
                dtRaces = objRaceDAL.GetRace(RaceId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtRaces;
        }


        /// <summary>
        /// Deletes the Race.
        /// </summary>
        /// <param name="RaceId">The Race id.</param>
        public void DeleteRace(int RaceId)
        {
            try
            {
                objRaceDAL.DeleteRace(RaceId);
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
        ~Race()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
