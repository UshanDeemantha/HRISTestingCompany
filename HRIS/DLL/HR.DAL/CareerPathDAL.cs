using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace HRM.HR.DAL
{
    public class CareerPathDAL
    {
      #region Fields
        private SqlConnection _dbConnection;
        private DataSet _careerPaths;
        private bool _isError;
        private string _errorMsg;
        private int _careerPathId;
       
        private int _careerPathDetailId;
        private int _skillId;
        private int _flunceyId;
        private int _qulificationId;
        private long _employeeCareerPathId;

      #endregion



      #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError {
            get { return _isError; }
           
        }
        public string ErrorMsg {
            get { return _errorMsg; }
          
        }
        public int CareerPathId {
            get { return _careerPathId; }
            set { _careerPathId = value; }
        }
     
      
      
       public int CareerPathDetailId {
           get { return _careerPathDetailId; }
           set { _careerPathDetailId = value; }
       }    
       public int SkillId {
            get { return _skillId;}
            set { _skillId = value; }
        }
        public int FlunceyId {
            get { return _flunceyId; }
            set {_flunceyId=value; }
        }

        public int QulificationId {
            get { return _qulificationId; }
            set {_qulificationId=value; }
        }


        public long EmployeeCareerPathId
        { get { return _employeeCareerPathId; } }
      #endregion

      #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentDAL"/> class.
        /// </summary>
        public CareerPathDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
      #endregion


      #region Methods
        private void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        /// <summary>
        /// Add  the CareerPath.
        /// <param name="CareerPathId">The CareerPath Id.</param>
        /// <param name="CareerPathCode">The CareerPathCode .</param>
        /// <param name="ExcistingDesignationId">The ExcistingDesignation Id.</param>
        /// <param name="NextDesignationId">The NextDesignation Id.</param>
        ///  <summary> 
        public void AddCareerPath( string CareerPathCode, int ExcistingDesignationId,int NextDesignationId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddCareerPath", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CareerPathCode", CareerPathCode);
                    command.Parameters.AddWithValue("@ExcistingDesignationId", ExcistingDesignationId);
                    command.Parameters.AddWithValue("@NextDesignationId", NextDesignationId);
                    SqlParameter careerPathId = new SqlParameter("@CareerPathId", SqlDbType.Int, 8);
                    careerPathId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(careerPathId);
                    command.ExecuteNonQuery();
                    _careerPathId = Convert.ToInt32(careerPathId.Value);
                    if (_careerPathId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Already Exists";
                    }

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

        /// <summary>
        /// Add  the CareerPath.
        /// <param name="CareerPathId">The CareerPath Id.</param>
        /// <param name="CareerPathCode">The CareerPathCode .</param>
        /// <param name="ExcistingDesignationId">The ExcistingDesignation Id.</param>
        /// <param name="NextDesignationId">The NextDesignation Id.</param>
        ///  <summary> 
        public void UpdateCareerPath(int CareerPathId, string CareerPathCode, int ExcistingDesignationId, int NextDesignationId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateCareerPath", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();


                    

                    command.Parameters.AddWithValue("@CareerPathId", CareerPathId);
                    command.Parameters.AddWithValue("@CareerPathCode", CareerPathCode);
                    command.Parameters.AddWithValue("@ExcistingDesignationId", ExcistingDesignationId);
                    command.Parameters.AddWithValue("@NextDesignationId", NextDesignationId);

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



        /// <summary>
        /// Gets the CareerPath.
        /// </summary>
        /// <param name="CareerPathId">CareerPath Id  As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetCareerPath(int CareerPathId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetCareerPath", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CareerPathId", CareerPathId);
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
            finally
            {
                _dbConnection.Close();

            }
            return dtTable;
        }





        /// <summary>
        /// Deletes the career path.
        /// </summary>
        /// <param name="CareerPathId">The career path id.</param>
        public void DeleteCareerPath(int CareerPathId)
        {
            
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteCareerPath", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CareerPathId", CareerPathId);
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


        #region Career Path Detail

        /// <summary>
        /// Add  the CareerPathDetail.
        /// <param name="CareerPathDetailId">The CareerPathDetail Id.</param>
        /// <param name="CareerPathId">The FluencyId .</param>
        /// <param name="SkillId">The SkillId Id.</param>
        /// <param name="QulificationId">The Qulification Id param>
        /// <param name="Experiance">The Experiance .</param>
        /// <param name="Remark">The Remark. </param>
        ///  <summary> 
        public void AddCareerPathDetail(int CareerPathId, int FluencyId, int SkillId, int QulificationId, int Experiance, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddCareerPathDetail", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();



                    command.Parameters.AddWithValue("@CareerPathDetailId", CareerPathId);
                    command.Parameters.AddWithValue("@CareerPathId", CareerPathId);
                    command.Parameters.AddWithValue("@FluencyId", FluencyId);
                    command.Parameters.AddWithValue("@SkillId", SkillId);
                    command.Parameters.AddWithValue("@QulificationId", QulificationId);
                    command.Parameters.AddWithValue("@Experiance", Experiance);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
                    SqlParameter careerPathDetailId = new SqlParameter("@CareerPathDetailId", SqlDbType.Int, 16);
                    careerPathDetailId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(careerPathDetailId);
                    command.ExecuteNonQuery();

                    _careerPathDetailId = Convert.ToInt32(careerPathDetailId.Value);

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

        /// <summary>
        ///Update the CareerPathDetail.
        /// <param name="CareerPathDetailId">The CareerPathDetail Id.</param>
        /// <param name="FluencyId">The FluencyId .</param>
        /// <param name="SkillId">The SkillId Id.</param>
        /// <param name="QulificationId">The Qulification Id param>
        /// <param name="Experiance">The Experiance .</param>
        /// <param name="Remark">The Remark. </param>
        ///  <summary> 
        public void UpdateCareerPathDetail(int CareerPathDetailId, int CareerPathId, int FluencyId, int SkillId, int QulificationId, int Experiance, string Remarks)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateCareerPathDetail", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();




                    command.Parameters.AddWithValue("@CareerPathDetailId", CareerPathId);
                    command.Parameters.AddWithValue("@CareerPathId", CareerPathId);
                    command.Parameters.AddWithValue("@FluencyId", FluencyId);
                    command.Parameters.AddWithValue("@SkillId", SkillId);
                    command.Parameters.AddWithValue("@QulificationId", QulificationId);
                    command.Parameters.AddWithValue("@Experiance", Experiance);
                    command.Parameters.AddWithValue("@Remarks", Remarks);
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

        /// <summary>
        /// Gets the CareerPathDetail.
        /// </summary>
        /// <param name="CareerPathDetailId">CareerPathDetail Id  As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetCareerPathDetail(int CareerPathDetailId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetCareerPathDetail", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CareerPathDetailId", CareerPathId);
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
            finally
            {
                _dbConnection.Close();

            }
            return dtTable;
        }
        /// <summary>
        /// Deletes the career path detail.
        /// </summary>
        /// <param name="CareerPathDetailId">The career path detail id.</param>
        public void DeleteCareerPathDetail(int CareerPathDetailId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteCareerPathDetail", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@CareerPathDetailId", CareerPathId);
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
        
        #endregion
        #region EmployeeCareerPath
        /// <summary>
        /// Adds the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeId">The employee id as int. </param>
        /// <param name="CareerPathId">The CareerPath id as int.</param>
        public void AddEmployeeCareerPath(long EmployeeId, int CareerPathId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_AddEmployeeCareerPath", _dbConnection))
                {
                    OpenDB();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CareerPathId", CareerPathId);
                    SqlParameter employeeCareerPathId = new SqlParameter("@EmployeeCareerPathId", SqlDbType.Int, 16);
                    employeeCareerPathId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeCareerPathId);
                    command.ExecuteNonQuery();
                    _employeeCareerPathId = Convert.ToInt64(employeeCareerPathId.Value);
                    if (_employeeCareerPathId < 0)
                    {
                        _isError = true;
                        _errorMsg = "Record Already Exists!";
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _errorMsg = ex.Message;
            //}
            catch (DbException ex)
            {
                int i = ex.ErrorCode;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Updates the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeCareerPathId">The employee CareerPath id as int.</param>
        /// <param name="EmployeeId">The employee id as int.</param>
        /// <param name="CareerPathId">The CareerPath id as int. </param>
        public void UpdateEmployeeCareerPath(long EmployeeCareerPathId, long EmployeeId, int CareerPathId)
        {

            try
            {
                using (SqlCommand command = new SqlCommand("HR_UpdateEmployeeCareerPath", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeCareerPathId", EmployeeCareerPathId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@CareerPathId", CareerPathId);
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

        /// <summary>
        /// Gets the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeCareerPathId">The employee CareerPath id as int.</param>
        /// <returns></returns>
        public DataTable GetEmployeeCareerPath(long EmployeeCareerPathId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("HR_GetEmployeeCareerPath", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@EmployeeCareerPathId", EmployeeCareerPathId);
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
            finally
            {
                _dbConnection.Close();

            }
            return dtTable;
        }


        /// <summary>
        /// Deletes the employee CareerPath.
        /// </summary>
        /// <param name="EmployeeCareerPathId">The employee CareerPath id as int.</param>
        public void DeleteEmployeeCareerPath(long EmployeeCareerPathId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("HR_DeleteEmployeeCareerPath", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@EmployeeCareerPathId", EmployeeCareerPathId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _isError = true;
                if (ex.Number == 547)
                {
                    _errorMsg = "Already Assign This Code";
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

        #endregion
      #endregion

      #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="StudentDAL"/> is reclaimed by garbage collection.
        /// </summary>
        ~CareerPathDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
