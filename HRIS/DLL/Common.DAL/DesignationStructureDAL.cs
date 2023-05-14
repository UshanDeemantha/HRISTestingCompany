
using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HRM.Common.DAL
{

    public class DesignationStructureDAL
    {
        #region Fields

            private SqlConnection _dbConnection;
            private bool _isError;
            private string _errorMsg;

            private int _designationId;
            private string _designationCode;
            private string _designationName;
            private string _designationTypeId;

        #endregion

        #region Properties

            public int DesignationId
            {
                get { return -DesignationId; }
                set { _designationId = value; }
            }

            public string  DesignationCode 
            {
                get { return _designationCode; }
                set { _designationCode = value; }
            }

            public string  DesignationName 
            {
                get { return _designationName; }
                set { _designationName = value; }
            }

        #endregion

        # region Constructor

            public DesignationStructureDAL()
            {
                _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }

        #endregion

        #region Methods

            public void OpenDB()
            {
                if (_dbConnection.State != ConnectionState .Open)
                {
                    _dbConnection.Open();
                }
            }

            public void AddDesignation(string desigationCode,string designationName)
            {
                try
                {
                    using(SqlCommand command = new SqlCommand("COM_AddDesignation",_dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@DesignationCode",desigationCode);
                        command.Parameters.AddWithValue("@DesignationName",designationName);

                        SqlParameter DesignationId = new SqlParameter("@DesignationId", SqlDbType.Int);
                        DesignationId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(DesignationId);

                        command.ExecuteNonQuery();
                        _designationId= Convert.ToInt32(DesignationId.Value);

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

            public void UpdateDesignation(int designationId, string designationCode, string designationName)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("COM_UpdateDesignation", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@DesignationId", designationId);
                        command.Parameters.AddWithValue("@DesignationCode", designationCode);
                        command.Parameters.AddWithValue("@DesignationName", designationName);

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

            public DataTable  GetDesignation(int designationId)
            {
                using(DataTable dtTable = new DataTable())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("COM_GetDesignation", _dbConnection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OpenDB();
                            command.Parameters.AddWithValue("@DesignationId", designationId);
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

            public void DeleteDesignation(int customerId)
            { 

            }

        
       #region DesignationType

            public void AddDesigantionType(int treeIndex,int designatinId,int parentId)
            {
                try
                {
                    using(SqlCommand command = new SqlCommand("COM_AddDesignationStructure",_dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@TreeIndex",treeIndex);
                        command.Parameters.AddWithValue("@DesignationID", designatinId);
                        command.Parameters.AddWithValue("@ParentID", parentId);

                        SqlParameter designationTypeId = new SqlParameter("@DesignationStuctureID", SqlDbType.Int);
                        designationTypeId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(designationTypeId);

                        command.ExecuteNonQuery();
                     //   _designationTypeId = Convert.ToInt32(designationTypeId.Value);
                    }
                }
                catch (Exception ex)
                {

                    _isError = true;
                    _errorMsg = ex.Message;
                }
            }

            public void UpdateDesigantionType(int DesignationStuctureID, int treeIndex, int designationId, int parentId)
            {
                try
                {
                    using(SqlCommand command = new SqlCommand("COM_UpdateDesignationStructure",_dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();

                        command.Parameters.AddWithValue("@DesignationStuctureID",DesignationStuctureID);
                        command.Parameters.AddWithValue("@TreeIndex",treeIndex);
                        command.Parameters.AddWithValue("@DesignationID",designationId);
                        command.Parameters.AddWithValue("@ParentID",parentId);

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

            public DataTable GetDesignationType(int designationId)
            {
                using (DataTable dtTable = new DataTable())
                {
                    try
                    {
                        using(SqlCommand command = new SqlCommand("COM_GetDesignationStructure",_dbConnection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OpenDB();
                            command.Parameters.AddWithValue("@DesignationStuctureID",designationId);

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

        
       #endregion

       
    }
}
