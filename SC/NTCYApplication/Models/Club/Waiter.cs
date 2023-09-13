using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NTCYApplication.Models.Club
{
    public class Waiter : IWaiter
    {
        private int _Waiter_Id;
        private string _Waiter_Name;

        public int Waiter_Id
        {
            get { return _Waiter_Id; }
            set { _Waiter_Id = value; }
        }
        public string Waiter_Name
        {
            get { return _Waiter_Name; }
            set { _Waiter_Name = value; }
        }

        DBConnection db = new DBConnection();
        public DataSet GetWaiters(string Prefix)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetWaiterName";
            cmd.Parameters.AddWithValue("@Waiter_Name", Prefix);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return ds;
            }
        }
    }
}