
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using HRM.HR.DAL;
using System.Data;

namespace HRM.HR.BLL
{

  
    
    public class Languages
    {

        #region Fields
        LanguagesDAL objLanguagesDAL = null;
        private DataTable dtLanguages = null;
        private DataTable dtEmployeeLanguages = null;
        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _languageId = 0;
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

        public int LanguageId
        {
            get { return _languageId; }
        }


        #endregion



          #region Constructor       
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Languages()
        {
            objLanguagesDAL = new LanguagesDAL();
        } 
        #endregion

        #region Methods

        private void SetValues()
        {
            _isError = objLanguagesDAL.IsError;
            _errorMsg = objLanguagesDAL.ErrorMsg;
        }


        /// <summary>
        /// Adds the language.
        /// </summary>
        /// <param name="LanguageName">Name of the language as string.</param>
        public void AddLanguage(string LanguageName)
        {
            try
            {
                objLanguagesDAL.AddLanguage(LanguageName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Updates the language.
        /// </summary>
        /// <param name="LanguageId">The language id as int.</param>
        /// <param name="LanguageName">Name of the language as string.</param>
        public void UpdateLanguage(int LanguageId,string LanguageName)
        {
            try
            {
                objLanguagesDAL.UpdateLanguage(LanguageId,LanguageName);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <param name="LanguageId">The language id as int.</param>
        /// <returns></returns>
        public DataTable GetLanguage(int LanguageId)
        {
            try
            {
                dtLanguages = objLanguagesDAL.GetLanguage(LanguageId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtLanguages;
        }


        /// <summary>
        /// Deletes the language.
        /// </summary>
        /// <param name="LanguageId">The language id as int.</param>
        public void DeleteLanguage(int LanguageId)
        {
            try
            {
                objLanguagesDAL.DeleteLanguage(LanguageId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;

            }
        }





        /// <summary>
        /// Adds the employee language.
        /// </summary>
        /// <param name="EmplyeeId">The emplyee id.</param>
        /// <param name="LanguageId">The language id.</param>
        public void AddEmployeeLanguage(int EmplyeeId,int LanguageId)
        {
            try
            {
                objLanguagesDAL.AddEmployeeLanguage(EmplyeeId,LanguageId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        /// <summary>
        /// Updates the emplyee language.
        /// </summary>
        /// <param name="EmployeeLanguageId">The employee language id.</param>
        /// <param name="EmployeeId">The employee id.</param>
        /// <param name="LanguageId">The language id.</param>
        public void UpdateEmployeeLanguage(long EmployeeLanguageId,long EmployeeId,int LanguageId)
        {
            try
            {
                objLanguagesDAL.UpdateEmployeeLanguage(EmployeeLanguageId,EmployeeId,LanguageId);
                SetValues();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }


        /// <summary>
        /// Gets the employee language.
        /// </summary>
        /// <param name="EmployeeLanguageId">The employee language id.</param>
        /// <returns></returns>
        public DataTable GetEmployeeLanguage(int EmployeeLanguageId)
        {
            try
            {
                dtEmployeeLanguages = objLanguagesDAL.GetEmployeeLanguage(EmployeeLanguageId);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }

            return dtEmployeeLanguages;
        }



        /// <summary>
        /// Deletes the employee language.
        /// </summary>
        /// <param name="EmployeeLanguageId">The employee language id.</param>
        public void DeleteEmployeeLanguage(int EmployeeLanguageId)
        {
            try
            {
                objLanguagesDAL.DeleteEmployeeLanguage(EmployeeLanguageId);
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
        ~Languages()
        {          
            GC.SuppressFinalize(this);
        } 
        #endregion

    }
}
