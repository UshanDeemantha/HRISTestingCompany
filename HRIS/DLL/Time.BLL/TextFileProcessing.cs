/// Copyright (c) 2000-2011 MasterKey Solutions.
/// MasterKey Solutions, No. 51, Flower Road, Colombo 7.
/// All right received.
/// This software is the confidential and proprietary information of
/// MasterKey Solutions (Confidential Information). You shall not disclose such
/// Confidential Information and shall use it only in accordance with the
/// terms of the license agreement you entered into with MasterKey Solutions.
/// 
/// Solution Name       : HRM.Payroll.DAL
/// Cording Standard    : MasterKey Cording Standards
/// Author              : Asanka C. Amarasinghe
/// Created Timestamp   : 30-September-2011
/// Class Description   : HRM.Time.BLL.TextFileProcessing
/// Task Code           : --

namespace HRM.Time.BLL
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using HRM.Time.DAL;
    using System.Collections.Generic;


    [DataObject]
    public class TextFileProcessing
    {
        #region Fields

        private TextFileProcessingDAL objTextFileProcessingDAL;
        
        private bool _isError;
        private string _errorMsg;

        #endregion

        #region Constructor

        public TextFileProcessing()
        {
            objTextFileProcessingDAL = new TextFileProcessingDAL();
        }

        #endregion

        #region Propeties

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
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

        public List<string> FileProcessErrors
        { get; set; }

        #endregion

        #region Methods

        #region Internal

        private void SetValues()
        {
            _isError = objTextFileProcessingDAL.IsError;
            _errorMsg = objTextFileProcessingDAL.ErrorMsg;           
        }

        #endregion

        #region External

        public Int64 AddTextFileReadingSetting(string configurationName, string configurationString)
        {
            _isError = false;
            _errorMsg = string.Empty;

            Int64 CreatedIndexID = -1;

            try
            {
                CreatedIndexID = objTextFileProcessingDAL.AddTextFileReadingSetting(configurationName, configurationString);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return CreatedIndexID;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int DeleteTextFileReadingSetting(long configurationID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            int ReturnStatus = -1;

            try
            {
                ReturnStatus = objTextFileProcessingDAL.DeleteTextFileReadingSetting(configurationID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return ReturnStatus;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetTextFileReadingSetting()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtSavedData = new DataTable();

            try
            {
                dtSavedData = objTextFileProcessingDAL.GetTextFileReadingSetting();
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtSavedData;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUploadDates()
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtSavedData = new DataTable();

            try
            {
                dtSavedData = objTextFileProcessingDAL.GetUploadDates();
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtSavedData;
        }

        public int AddRowData(DataTable dtRowData)
        {
            int returnStats = 0;

            _isError = false;
            _errorMsg = string.Empty;

            string cardNo = string.Empty;
            string cardDate = string.Empty;
            string cardTime = string.Empty;
            string functionKey = string.Empty;
            string miscData = string.Empty;
            DateTime cardDateTime = new DateTime();
            DateTime uploadDate = new DateTime();

            long returnValue = -1;

            FileProcessErrors = new List<string>();

            try
            {
                foreach (DataRow drData in dtRowData.Rows)
                {
                    cardNo = drData["CardNo"].ToString();
                    cardDate = drData["CardDate"].ToString();
                    cardTime = drData["CardTime"].ToString();
                    functionKey = drData["FunctionKey"].ToString();
                    miscData = drData["MiscData"].ToString();
                    cardDateTime = Convert.ToDateTime(drData["CardDateTime"].ToString());
                    uploadDate = Convert.ToDateTime(drData["UploadDate"].ToString());

                    returnValue = objTextFileProcessingDAL.AddRowData(cardNo, cardDate, cardTime, functionKey, miscData, cardDateTime, uploadDate);
                    if (returnValue > 0)
                    {
                        returnStats += 1;
                    }
                    else
                    {
                        if (returnValue < 0)
                        {
                            FileProcessErrors.Add(objTextFileProcessingDAL.ErrorMsg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnStats = -1;

                _isError = true;
                _errorMsg = ex.Message;
            }

            return returnStats;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUploadedData(DateTime dtUploadedDate)
        {
            _isError = false;
            _errorMsg = string.Empty;

            DataTable dtSavedData = new DataTable();

            try
            {
                dtSavedData = objTextFileProcessingDAL.GetUploadedData(dtUploadedDate);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtSavedData;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<string> GetTextFileReadingSettingForID(int configurationID)
        {
            _isError = false;
            _errorMsg = string.Empty;

            List<string> returnConfigSegments = new List<string>();
            string configurationString = string.Empty;

            try
            {
                configurationString = objTextFileProcessingDAL.GetTextFileReadingSettingForID(configurationID);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            if (configurationString != string.Empty)
            {
                string[] config = configurationString.Split(new string[] { "$break" }, StringSplitOptions.None);
                foreach (string item in config)
                {
                    returnConfigSegments.Add(item);
                }
            }

            return returnConfigSegments;
        }

        private void ValidateLoadedData(List<string> fileData, int readConfigSettingID)
        {
            int remainder = 0;
            int quotient = 0;
            int count = 0;

            int employeeNumberPlaceholderCount = 0;
            string dateFormat = string.Empty;
            string timeFormat = string.Empty;
            int functionPlaceholderCount = 0;
            int miscPlaceholderCount = 0;

            int lineNumber = 0;

            List<string> configStrings = new List<string>();

            try
            {
                configStrings = GetTextFileReadingSettingForID(readConfigSettingID);
                if (_isError == true)
                {
                    return;
                }

                quotient = Math.DivRem(configStrings.Count, 2, out remainder);
                string[] seperators = new string[quotient];

                for (int i = 1; i < configStrings.Count; i = i + 2)
                {
                    seperators[count] = configStrings[i];
                    count++;
                }

                foreach (string config in configStrings)
                {
                    if (config.ToUpperInvariant().Contains("EMP:") == true)
                    {
                        employeeNumberPlaceholderCount = Convert.ToInt32(config.Replace("EMP:", ""));
                    }
                    else if (config.ToUpperInvariant().Contains("DATE:") == true)
                    {
                        dateFormat = config.Replace("DATE:", "");
                    }
                    else if (config.ToUpperInvariant().Contains("TIME:") == true)
                    {
                        timeFormat = config.Replace("TIME:", "");
                    }
                    else if (config.ToUpperInvariant().Contains("FUN:") == true)
                    {
                        functionPlaceholderCount = Convert.ToInt32(config.Replace("FUN:", ""));
                    }
                    else if (config.ToUpperInvariant().Contains("MSC:") == true)
                    {
                        miscPlaceholderCount = Convert.ToInt32(config.Replace("MSC:", ""));
                    }
                }

                foreach (string stringData in fileData)
                {

                    lineNumber++;
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        #endregion

        #endregion
    }
}
