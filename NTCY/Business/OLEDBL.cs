using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace NTCY.Business
{
    public class OLEDBL
    {
        public DataTable GetListByQuery(string strSPName)
        {
            DataTable dt = new DataTable();
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string cnn = configuration.GetConnectionString("WebApiDatabase");

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strSPName;

            using (SqlConnection connection = new SqlConnection(cnn))
            {
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        //if (reader.Read())
                        //{
                        //    dt.Load(reader);
                        //}
                    }
                    connection.Close();
                }
                catch(Exception ex)
                {
                    // Log Error
                }
                
            }
            return dt;
        }
    
        public Int32 ExecuteStrNonQuery(string strSQL)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string cnn = configuration.GetConnectionString("WebApiDatabase");

            Int32 iResult = 0;
            try
            {
                using(SqlConnection connection = new SqlConnection(cnn))
                {
                    SqlCommand cmd = new SqlCommand(strSQL,connection);
                    connection.Open();
                    iResult = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Write Log
                //iResult = 0;
            }

            return iResult;
        }
    }
}
