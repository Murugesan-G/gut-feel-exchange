using NTCYApplication.Interfaces;
using NTCYApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models
{
    public class DBConnection
    {

        public DBConnection()
        {

        }
        public static string DatabaseConnection
        {
            get { return ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim(); }
        }

        public SqlConnection OpenConnection()
        {
            SqlConnection con = new SqlConnection(DBConnection.DatabaseConnection.ToString());

            try
            {
                con.Open();
              
            }catch(Exception e)
            {
                string exception =e.Message.ToString();
            }
            return con;
        }
         
        public SqlConnection CloseConnection()
        {
            SqlConnection con = new SqlConnection(DBConnection.DatabaseConnection.ToString());
            try
            {
                con.Close();
            }
           catch(Exception e)
            {
                string exception = e.ToString();
            }
            return con;
        }

    }
}