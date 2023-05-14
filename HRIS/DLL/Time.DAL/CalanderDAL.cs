using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM.Time.DAL
{
    public class CalanderDAL
    {
        #region Fields
        private SqlConnection _dbConnection;

        private bool _isError = false;
        private string _errorMsg = string.Empty;
        private int _dayTypeId = 0;
        int _errorNo = 0;

        #endregion

        #region Properties

        public bool IsError
        {
            get { return _isError; }
        }

        public int ErrorNo
        {
            get { return _errorNo; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        public int DayTypeId
        {
            get { return _dayTypeId; }
        }
        
        #endregion

        #region Constructor

        public CalanderDAL()
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

        public DataTable GetCalander(int CompanyId)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetCalender", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Year", 2012);
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
        }

        public DataTable GetCalander(int CompanyId,int year)
        {
            using (DataTable dtTable = new DataTable())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Time_GetCalender", _dbConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        OpenDB();
                        command.Parameters.AddWithValue("@CompanyId", CompanyId);
                        command.Parameters.AddWithValue("@Year", year);
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
        }


        #region DayType

        //public void AddDayTypes(string DayTypeCode, string DayType, string BackGroundColour, string FontColour, string CreatedUser, DateTime CreatedDate, bool IsDefault)
        //{
        //    try
        //    {
        //        using (SqlCommand command = new SqlCommand("Time_AddDayType", _dbConnection))
        //        {

        //            command.CommandType = CommandType.StoredProcedure;
        //            OpenDB();

        //            command.Parameters.AddWithValue("@DayTypeCode", DayTypeCode);
        //            command.Parameters.AddWithValue("@DayType", DayType);
        //            command.Parameters.AddWithValue("@BackGroundColour", BackGroundColour);
        //            command.Parameters.AddWithValue("@FontColour", FontColour);
        //            command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
        //            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
        //            command.Parameters.AddWithValue("@IsDefault", IsDefault);

        //            SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
        //            DayTypeId.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(DayTypeId);
        //            command.ExecuteNonQuery();
        //            _dayTypeId = Convert.ToInt32(DayTypeId.Value);
        //            if (_dayTypeId < 0)
        //            {
        //                _isError = true;
        //                _errorMsg = "Record Already Exists!";
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _errorMsg = ex.Message;
        //    }
        //    finally
        //    {
        //        _dbConnection.Close();
        //    }
        //}

       // public void UpdateDayType(int DayTypeId, string DayType, string BackGroundColour, string FontColour, string ModifyUser, DateTime ModifyDate,bool Default)
        //{
            //try
            //{
            //    using (SqlCommand command = new SqlCommand("Time_UpdateDayType", _dbConnection))
            //    {

            //        command.CommandType = CommandType.StoredProcedure;
            //        OpenDB();

            //        command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
            //        command.Parameters.AddWithValue("@DayType", DayType);
            //        command.Parameters.AddWithValue("@BackGroundColour", BackGroundColour);
            //        command.Parameters.AddWithValue("@FontColour", FontColour);
            //        command.Parameters.AddWithValue("@ModifiedUser", ModifyUser);
            //        command.Parameters.AddWithValue("@ModifiedDate", ModifyDate);
            //        command.Parameters.AddWithValue("@IsDefault", Default);

            //        command.ExecuteNonQuery();


            //    }
            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _errorMsg = ex.Message;
            //}
            //finally
            //{
            //    _dbConnection.Close();
            //}
       // }

        //public DataTable GetDayType(int DayTypeId)
        //{
            //using (DataTable dtTable = new DataTable())
            //{
            //    try
            //    {
            //        using (SqlCommand command = new SqlCommand("Time_GetDayType", _dbConnection))
            //        {
            //            command.CommandType = CommandType.StoredProcedure;
            //            OpenDB();
            //            command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
            //            using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
            //            {
            //                daAdapter.Fill(dtTable);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _isError = true;
            //        _errorMsg = ex.Message;
            //    }
            //    finally
            //    {
            //        _dbConnection.Close();

            //    }
            //    return dtTable;
            //}
        //}

        //public DataTable GetDayTypeCode(string DayTypeCode)
        //{
        //    using (DataTable dtTable = new DataTable())
        //    {
        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand("Time_GetDayTypeByCode", _dbConnection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                OpenDB();
        //                command.Parameters.AddWithValue("@DayTypeCode", DayTypeCode);
        //                using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
        //                {
        //                    daAdapter.Fill(dtTable);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _isError = true;
        //            _errorMsg = ex.Message;
        //        }
        //        finally
        //        {
        //            _dbConnection.Close();

        //        }
        //        return dtTable;
        //    }
        //}

        //public DataTable GetDayTypeById(int DayTypeId)
        //{
        //    using (DataTable dtTable = new DataTable())
        //    {
        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand("Time_GetDayTypeById", _dbConnection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                OpenDB();
        //                command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
        //                using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
        //                {
        //                    daAdapter.Fill(dtTable);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _isError = true;
        //            _errorMsg = ex.Message;
        //        }
        //        finally
        //        {
        //            _dbConnection.Close();

        //        }
        //        return dtTable;
        //    }
        //}

        
        //public void DeleteDayType(int DayTypeId)
        //{
        //    try
        //    {
        //        OpenDB();
        //        using (SqlCommand command = new SqlCommand("Time_DeleteDayType", _dbConnection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        _isError = true;
        //        if (ex.Number == 547)
        //        {
        //            _errorMsg = "Already Assign This Code";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _errorMsg = ex.Message;
        //    }
        //    finally
        //    {
        //        _dbConnection.Close();
        //    }
        //}
        #endregion


        #region Calender

        //public DataTable GetCalender(int CompanyId)
        //{
        //    using (DataTable dtTable = new DataTable())
        //    {
        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand("Time_GetCalender", _dbConnection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                OpenDB();
        //                command.Parameters.AddWithValue("@CompanyId", CompanyId);
        //                using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
        //                {
        //                    daAdapter.Fill(dtTable);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _isError = true;
        //            _errorMsg = ex.Message;
        //        }
        //        finally
        //        {
        //            _dbConnection.Close();

        //        }
        //        return dtTable;
        //    }
        //}

        public void AddCalender(DateTime CalenderDate, int CompanyId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, bool Posted, string CreatedUser, string Subject)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_AddCalender", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@StartTime", StartTime);
                    command.Parameters.AddWithValue("@EndTime", EndTime);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    //SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    //DayTypeId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    //_dayTypeId = Convert.ToInt32(DayTypeId.Value);


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



        public void UpdateCalender(DateTime CalenderDate, int CompanyId, DateTime StartTime, DateTime EndTime, int Day, int Month, int Year, int DayTypeId, bool Posted, string CreatedUser)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_UpdateCalender", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);
                    command.Parameters.AddWithValue("@StartTime", StartTime);
                    command.Parameters.AddWithValue("@EndTime", EndTime);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@DayTypeId", DayTypeId);
                    command.Parameters.AddWithValue("@Posted", Posted);
                    command.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    //SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    //DayTypeId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    //_dayTypeId = Convert.ToInt32(DayTypeId.Value);


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


        public void DeleteCalender(DateTime CalenderDate, int CompanyId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("Time_DeleteCalender", _dbConnection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();

                    command.Parameters.AddWithValue("@CalenderDate", CalenderDate);
                    command.Parameters.AddWithValue("@CompanyId", CompanyId);

                    //SqlParameter DayTypeId = new SqlParameter("@DayTypeId", SqlDbType.Int, 8);
                    //DayTypeId.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(DayTypeId);
                    command.ExecuteNonQuery();
                    //_dayTypeId = Convert.ToInt32(DayTypeId.Value);


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

        ~CalanderDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
