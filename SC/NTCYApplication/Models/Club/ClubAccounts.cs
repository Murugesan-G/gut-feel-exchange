using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    class ClubAccounts : IClubAccounts
    {
        string _Url { get; set; }
        int _Port { get; set; }
        string _Username { get; set; }
        string _Password { get; set; }
        string _Ledger1 { get; set; }
        string _Ledger2 { get; set; }
        string _Ledger3 { get; set; }
        string _Ledger4 { get; set; }
        string _Status { get; set; }

        public string Url
        {
            get { return _Url; }
            set { _Url=value; }
        }

        public int Port
        {
            get { return _Port; }
            set { _Port=value; }
        }
        public string Username
        {
            get { return _Username; }
            set { _Username=value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password=value; }
        }
        public string Ledger1
        {
            get { return _Ledger1; }
            set { _Ledger1=value; }
        }
        public string Ledger2
        {
            get { return _Ledger2; }
            set { _Ledger2=value; }
        }
        public string Ledger3
        {
            get { return _Ledger3; }
            set { _Ledger3=value; }
        }
        public string Ledger4
        {
            get { return _Ledger4; }
            set { _Ledger4=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }

        Dictionary<string, object> TallyIntegrationDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            TallyIntegrationDictionary.Add("Url", Url);
            TallyIntegrationDictionary.Add("Port", _Port);
            TallyIntegrationDictionary.Add("Username", _Username);
            TallyIntegrationDictionary.Add("Password", _Password);
            TallyIntegrationDictionary.Add("Ledger1", _Ledger1);
            TallyIntegrationDictionary.Add("Ledger2", _Ledger2);
            TallyIntegrationDictionary.Add("Ledger3", _Ledger3);
            TallyIntegrationDictionary.Add("Ledger4", _Ledger4);
            TallyIntegrationDictionary.Add("Status", _Status);

            return "Success";
        }

        DBConnection db = new DBConnection();
        public string CreateDetails(Dictionary<string, object> TallyIntegrationDictionary)
        {
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertAccounts";
            cmd.Parameters.AddWithValue("@Url", TallyIntegrationDictionary["Url"]);
            cmd.Parameters.AddWithValue("@Port", TallyIntegrationDictionary["Port"]);
            cmd.Parameters.AddWithValue("@Username", TallyIntegrationDictionary["Username"]);
            cmd.Parameters.AddWithValue("@Password", TallyIntegrationDictionary["Password"]);
            cmd.Parameters.AddWithValue("@Ledger1", TallyIntegrationDictionary["Ledger1"]);
            cmd.Parameters.AddWithValue("@Ledger2", TallyIntegrationDictionary["Ledger2"]);
            cmd.Parameters.AddWithValue("@Ledger3", TallyIntegrationDictionary["Ledger3"]);
            cmd.Parameters.AddWithValue("@Ledger4", TallyIntegrationDictionary["Ledger4"]);
            cmd.Parameters.AddWithValue("@Status", TallyIntegrationDictionary["Status"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //Check for errors using try catch 
                    response=cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public string EditDetails(Dictionary<string, object> TallyIntegrationDictionary)
        {
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateAccounts";
            cmd.Parameters.AddWithValue("@Url", TallyIntegrationDictionary["Url"]);
            cmd.Parameters.AddWithValue("@Port", TallyIntegrationDictionary["Port"]);
            cmd.Parameters.AddWithValue("@Username", TallyIntegrationDictionary["Username"]);
            cmd.Parameters.AddWithValue("@Password", TallyIntegrationDictionary["Password"]);
            cmd.Parameters.AddWithValue("@Ledger1", TallyIntegrationDictionary["Ledger1"]);
            cmd.Parameters.AddWithValue("@Ledger2", TallyIntegrationDictionary["Ledger2"]);
            cmd.Parameters.AddWithValue("@Ledger3", TallyIntegrationDictionary["Ledger3"]);
            cmd.Parameters.AddWithValue("@Ledger4", TallyIntegrationDictionary["Ledger4"]);
            cmd.Parameters.AddWithValue("@Status", TallyIntegrationDictionary["Status"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //Check for errors using try catch 
                    response=cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public Dictionary<string, object> ViewDetails()
        {
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAccounts";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        TallyIntegrationDictionary=new Dictionary<string, object>();
                        TallyIntegrationDictionary.Add("Url", ds.Tables[0].Rows[0]["Url"].ToString());
                        TallyIntegrationDictionary.Add("Port", ds.Tables[0].Rows[0]["Port"].ToString());
                        TallyIntegrationDictionary.Add("Username", ds.Tables[0].Rows[0]["Username"].ToString());
                        TallyIntegrationDictionary.Add("Password", ds.Tables[0].Rows[0]["Password"].ToString());
                        TallyIntegrationDictionary.Add("Ledger1", ds.Tables[0].Rows[0]["Ledger1"].ToString());
                        TallyIntegrationDictionary.Add("Ledger2", ds.Tables[0].Rows[0]["Ledger2"].ToString());
                        TallyIntegrationDictionary.Add("Ledger3", ds.Tables[0].Rows[0]["Ledger3"].ToString());
                        TallyIntegrationDictionary.Add("Ledger4", ds.Tables[0].Rows[0]["Ledger4"].ToString());
                        TallyIntegrationDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

                    }
                    else
                    {
                        TallyIntegrationDictionary.Add("response", "0");
                    }

                }
                catch (SqlException e)
                {
                    ErrorString=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return TallyIntegrationDictionary;
            }
        }
    }
}