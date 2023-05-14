using System;
using System.Collections.Generic;
////using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for Menu
/// </summary>
namespace HRM.Common.DAL
{
    public class Menu
    {
        # region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private string _errorMsg;
        #endregion


        #region Properties

        /// <summary>
        /// Gets the is error.
        /// </summary>
        /// <value>The is error.</value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        #endregion

        public Menu()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        }
        #region Methods

        /// <summary>
        /// Opens the DB.
        /// </summary>
        public void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        public List<MenuItem> LoadMainMenu()
        {

            List<MenuItem> menuItemList = new List<MenuItem>();
            try
            {
                using (SqlCommand command = new SqlCommand("LoadMainMenu", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MenuItem item = new MenuItem();
                        item.MenuID = reader.GetInt32(reader.GetOrdinal("menuID"));
                        item.Title = reader.GetString(reader.GetOrdinal("Title"));
                        item.URL = reader.GetString(reader.GetOrdinal("URL"));
                        item.OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));
                        menuItemList.Add(item);

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
            return menuItemList;

        }

       



        public List<MenuItem> LoadSubMenuItems(int parentMenuId)
        {


            List<MenuItem> menuItemList = new List<MenuItem>();
            try
            {
                using (SqlCommand command = new SqlCommand("LoadSubMenu", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@ParentMenuID", parentMenuId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MenuItem item = new MenuItem();
                        item.MenuID = reader.GetInt32(reader.GetOrdinal("menuID"));
                        item.Title = reader.GetString(reader.GetOrdinal("Title"));
                        item.URL = reader.GetString(reader.GetOrdinal("URL"));
                        item.OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));

                        menuItemList.Add(item);
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
            return menuItemList;


        }
        #endregion
    }
    public class MenuItem : Menu
    {
        public int MenuID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public int ParentMenuID { get; set; }
        public int OrderID { get; set; }

    }
}