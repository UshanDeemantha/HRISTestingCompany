/// <summary>
/// Solution Name        : HRM
/// Project Name          : HR.BLL
/// Author                     : ushan deemantha
/// Class Description    : Qulification
/// </summary>
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using HRM.HR.DAL;
using System.ComponentModel;

namespace HRM.HR.BLL
{
    [DataObject]
    public class Qulification:Course
    {
         #region Fields

       QulificationDAL objQulificationDAL;
       
       private bool _isError;
       private string _errorMsg;

      
       private int _employeeQulificationId;
      

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

       public string ErrorMsg
       {

           get { return _errorMsg; }
       }

      
       /// <summary>
       /// Gets the employee Qulification ID.
       /// </summary>
       /// <value>The employee Qulification ID.</value>
       public int EmployeeQulificationID
       {
           get
           {
               return _employeeQulificationId;
           }
       }

       /// <summary>
       /// Gets the Qulification tyor ID.
       /// </summary>
       /// <value>The Qulification tyor ID.</value>
    
       #endregion

       #region Constructors

       /// <summary>
       /// Initializes a new instance of the <see cref="Qulification"/> class.
       /// </summary>
       public Qulification()
       {
           objQulificationDAL = new QulificationDAL();
           
       }

       

       

       #endregion




          #region Methods

       /// <summary>
       /// Sets the values (set the DAL Properties).
       /// </summary>
       

       /// <summary>
       /// Sets the default values (Errors).
       /// </summary>
       private void SetDefaultValues()
       {
           _isError = objQulificationDAL.IsError;
           _errorMsg = objQulificationDAL.ErrorMsg;
       }


       public void AddEmployeeQulifications(long EmployeeID, DataTable tbl)
       {
           try
           {
               foreach (DataRow row in tbl.Rows)
               {

                   objQulificationDAL.AddEmployeeQulification(EmployeeID, Convert.ToInt32(row["CourseId"].ToString()), Convert.ToInt32(row["Year"].ToString()));
                   SetDefaultValues();
               }

           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       public void AddEmployeeQulification2(int EmployeeID, int CourseId, int Year)
       {
           try
           {
               objQulificationDAL.AddEmployeeQulification(EmployeeID, CourseId, Year);
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }


            public void UpdateEmployeeQulifications(int EmployeeQulificationID,int EmployeeID,int CourseId,int Year)
       {
           try
           {
               objQulificationDAL.UpdateEmployeeQulification(EmployeeQulificationID,EmployeeID,CourseId,Year );
               SetDefaultValues();
           }
           catch(Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }


        public DataTable GetEmployeeQulification(int EmployeeQulificationId)
       {
           DataTable dTable = new DataTable();
           try
           {
               dTable = objQulificationDAL.GetEmployeeQulification(EmployeeQulificationId);
               SetDefaultValues();

           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
           return dTable;

       }

         [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEmployeeQulificationByEmployeeId(long EmployeeId)
        {
            DataTable dTable = new DataTable();
            try
            {
                dTable = objQulificationDAL.GetEmployeeQulificationByEmployeeId(EmployeeId);
                SetDefaultValues();

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dTable;

        }


        public void DeleteEmployeeQulification(int EmployeeQulificationId)
        {
           
            try
            {
                objQulificationDAL.DeleteEmployeeQulification(EmployeeQulificationId);
                SetDefaultValues();

            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
         

        }



       #endregion
       #region Distructor

       /// <summary>
       /// Releases unmanaged resources and performs other cleanup operations before the
       /// <see cref="Qulification"/> is reclaimed by garbage collection.
       /// </summary>
       ~Qulification()
       {
           GC.SuppressFinalize(this);
       } 

       #endregion


    }
}
