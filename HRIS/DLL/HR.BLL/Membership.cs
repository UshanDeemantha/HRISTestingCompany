
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
   public class Membership
   {
       #region Fields

       MemberShipDAL objMembershipDAL;
       
       private bool _isError;
       private string _errorMsg;

       private int _memberShipId;
       private string _membershipCode;
       private string _membershipName;

       private int _membershipTypeId;
       private string _membershipTypeCode;
       private string _membershipTypeName;
       private int _employeeMembershipId;
       private bool _active;

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
       /// Gets the member ship ID.
       /// </summary>
       /// <value>The member ship ID.</value>
       public int MemberShipID
       {
           get
           {
               return _memberShipId;
           }
       }

       /// <summary>
       /// Gets or sets the member ship code.
       /// </summary>
       /// <value>The member ship code.</value>
       public string MemberShipCode
       {
           get
           {
               return _membershipCode;
           }
           set
           {
               _membershipCode = value;
           }
       }

       /// <summary>
       /// Gets or sets the name of the member ship.
       /// </summary>
       /// <value>The name of the member ship.</value>
       public string MemberShipName
       {
           get
           {
               return _membershipName;
           }
           set
           {
               _membershipName = value;
           }
       }

       /// <summary>
       /// Gets the employee membership ID.
       /// </summary>
       /// <value>The employee membership ID.</value>
       public int EmployeeMembershipID
       {
           get
           {
               return _employeeMembershipId;
           }
       }

       /// <summary>
       /// Gets the membership tyor ID.
       /// </summary>
       /// <value>The membership tyor ID.</value>
       public int MembershipTypeID
       {
           get
           {
               return _membershipTypeId;
           }

       } 



       #endregion

       #region Constructors

       /// <summary>
       /// Initializes a new instance of the <see cref="Membership"/> class.
       /// </summary>
       public Membership()
       {
           objMembershipDAL = new MemberShipDAL();
           
       }

       /// <summary>
       /// Initializes a new instance of the <see cref="Membership"/> class.
       /// </summary>
       /// <param name="EmployeeMembershipID">The employee membership ID.</param>
       public Membership(int EmployeeMembershipID)
       {
           objMembershipDAL = new MemberShipDAL(_employeeMembershipId);
           _employeeMembershipId = EmployeeMembershipID;
       }

       

       #endregion


       #region Methods

       /// <summary>
       /// Sets the values (set the DAL Properties).
       /// </summary>
       public void SetValues()
       {
           objMembershipDAL.MemberShipCode = _membershipCode;
           objMembershipDAL.MemberShipName = _membershipName;
          
       }

       /// <summary>
       /// Sets the default values (Errors).
       /// </summary>
       private void SetDefaultValues()
       {
           _isError = objMembershipDAL.IsError;
           _errorMsg = objMembershipDAL.ErrorMsg;
       }

       /// <summary>
       /// Adds the membership.
       /// </summary>
       public void AddMembership(string MembershipCode,int MembershipTypeId,string MembershipName)
       {
           try
           {
               SetValues();
               objMembershipDAL.AddMembership(MembershipCode,MembershipTypeId,MembershipName);
               SetDefaultValues();
           }
           catch(Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       /// <summary>
       /// Adds the membership types.
       /// </summary>
       /// <param name="MembershipTypeCode">The membership type code.</param>
       /// <param name="MemebrshipTypeName">Name of the memebrship type.</param>
       public void AddMembershipTypes(string MembershipTypeCode, string MemebrshipTypeName)
       {
           try
           {
              
               objMembershipDAL.AddMembersipType(MembershipTypeCode,MemebrshipTypeName);
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
 
           }
       }

       /// <summary>
       /// Adds the employee memberships.
       /// </summary>
       /// <param name="EmployeeID">The employee ID.</param>
       /// <param name="MembershipID">The membership ID.</param>
       /// <param name="FromDate">From date.</param>
       /// <param name="ToDate">To date.</param>
       /// <param name="Grade">The grade.</param>
       /// <param name="Remark">The remark.</param>
       public void AddEmployeeMemberships(int EmployeeID, int MembershipID, DateTime FromDate, DateTime ToDate,string Grade, string Remark)
       {
           try
           {
               objMembershipDAL.AddEmployeeMemberships(EmployeeID, MembershipID, FromDate, ToDate, Grade, Remark);
               SetDefaultValues();
           }
           catch(Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }
       public void AddEmployeeMemberships(int EmployeeID, DataTable tbl)
       {
           try
           {
               foreach (DataRow row in tbl.Rows)
               {

                   objMembershipDAL.AddEmployeeMemberships(EmployeeID, Convert.ToInt32(row["MembershipID"].ToString()), Convert.ToDateTime(row["FromDate"].ToString()), Convert.ToDateTime(row["ToDate"].ToString()), Convert.ToBoolean(row["IsToDate"]), row["Grade"].ToString(), row["Remark"].ToString());
                   SetDefaultValues();
               }

           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }


       public void AddEmployeeMemberships2(int EmployeeID, int MembershipID, DateTime FromDate, DateTime ToDate, bool IsToDate, string Grade, string Remark)
       {
           try
           {
               objMembershipDAL.AddEmployeeMemberships(EmployeeID, MembershipID, FromDate, ToDate, IsToDate, Grade, Remark);
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       /// <summary>
       /// Updates the membership.
       /// </summary>
       /// <param name="MembershipID">The membership ID.</param>
       /// <param name="IsUpdate">if set to <c>true</c> [is update].</param>
       public void UpdateMembership(int MembershipID, string MembershipCode, int MembershipTypeId, string MembershipName)
       {
           try
           {
               SetValues();
               objMembershipDAL.UpdateMembership(MembershipID,MembershipCode,MembershipTypeId,MembershipName);
               SetDefaultValues();
           }
           catch(Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }

       }

       /// <summary>
       /// Updates the membership types.
       /// </summary>
       /// <param name="MembershipTypeID">The membership type ID.</param>
       /// <param name="IsUpdate">if set to <c>true</c> [is update].</param>
       public void UpdateMembershipTypes(int MembershipTypeID,bool IsUpdate)
       {
           try
           {
                objMembershipDAL.MemberShipCode=MemberShipCode;
                objMembershipDAL.MemberShipName = MemberShipName;
                objMembershipDAL.UpdateMembershipType(MembershipTypeID, IsUpdate);

              
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       /// <summary>
       /// Updates the employee memberships.
       /// </summary>
       /// <param name="EmployeeID">The employee ID.</param>
       /// <param name="MembershipID">The membership ID.</param>
       /// <param name="FromDate">From date.</param>
       /// <param name="ToDate">To date.</param>
       /// <param name="Grade">The grade.</param>
       /// <param name="Remark">The remark.</param>
       public void UpdateEmployeeMemberships(int EmployeeMembershipID,int EmployeeID, int MembershipID, DateTime FromDate, DateTime ToDate,string Grade, string Remark)
       {
           try
           {
               objMembershipDAL.UpdateEmployeeMemberships(EmployeeMembershipID,EmployeeID, MembershipID, FromDate, ToDate, Grade, Remark);
               SetDefaultValues();
           }
           catch(Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       public void UpdateEmployeeMemberships(int EmployeeMembershipID, int EmployeeID, int MembershipID, DateTime FromDate, DateTime ToDate, bool IsTodate, string Grade, string Remark)
       {
           try
           {
               objMembershipDAL.UpdateEmployeeMemberships(EmployeeMembershipID, EmployeeID, MembershipID, FromDate, ToDate,IsTodate, Grade, Remark);
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       /// <summary>
       /// Deletes the employee memberships.
       /// </summary>
       /// <param name="EmployeeMembershipsID">The employee memberships ID.</param>
       public void DeleteEmployeeMemberships(int EmployeeMembershipsID)
       {
           try
           {
               objMembershipDAL.DeleteEmployeeMembership(EmployeeMembershipsID);
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }


       /// <summary>
       /// Deletes the membership.
       /// </summary>
       /// <param name="MembershipId">The membership id as int.</param>
       public void DeleteMembership(int MembershipId)
       {
           try
           {
               objMembershipDAL.DeleteMembership(MembershipId);
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }

       /// <summary>
       /// Deletes the type of the membership.
       /// </summary>
       /// <param name="MembershipTypeId">The membership type id as int.</param>
       public void DeleteMembershipType(int MembershipTypeId)
       {
           try
           {
               objMembershipDAL.DeleteMembershipType(MembershipTypeId);
               SetDefaultValues();
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
       }


       /// <summary>
       /// Gets the type of the membership.
       /// </summary>
       /// <param name="MemebershipTypeId">The memebership type id as int.</param>
       /// <returns></returns>
       /// 

       [DataObjectMethod(DataObjectMethodType.Select, false)]
       public DataTable GetMembershipType(int MembershipTypeId)
       {
           DataTable dTable = new DataTable();
           try
           {
              
               dTable= objMembershipDAL.GetMembershipTypes(MembershipTypeId,true);
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
       public DataTable GetMembership(int MembershipId) 
       {
           DataTable dTable = new DataTable();
           try
           {
               dTable = objMembershipDAL.GetMemberships(MembershipId);
               SetDefaultValues();
              
           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
           return dTable;
       
       }

       public DataTable GetEmployeeMembership(int EmployeeMembershipId)
       {
           DataTable dTable = new DataTable();
           try
           {
               dTable = objMembershipDAL.GetEmployeeMemberships(EmployeeMembershipId);
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
       public DataTable GetEmployeeMembershipByEmployeeId(long EmployeeId)
       {
           DataTable dTable = new DataTable();
           try
           {
               dTable = objMembershipDAL.GetEmployeeMembershipByEmployeeId(EmployeeId);
               SetDefaultValues();

           }
           catch (Exception ex)
           {
               _isError = true;
               _errorMsg = ex.Message;
           }
           return dTable;

       }

       #endregion
       #region Distructor

       /// <summary>
       /// Releases unmanaged resources and performs other cleanup operations before the
       /// <see cref="Membership"/> is reclaimed by garbage collection.
       /// </summary>
       ~Membership()
       {
           GC.SuppressFinalize(this);
       } 

       #endregion




       

   }
}
