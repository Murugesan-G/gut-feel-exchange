using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    public class Biometric : IBiometric
    {
        string _IPAddress { get; set; }
        //string _DataBaseName { get; set; }
        string _Catalog { get; set; }
        string _Username { get; set; }
        string _Password { get; set; }
        string _Status { get; set; }
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress=value; }
        }
        //public string DataBaseName 
        //{
        //    get { return _DataBaseName; }
        //    set { _DataBaseName=value; }
        //}
        public string Catalog
        {
            get { return _Catalog; }
            set { _Catalog=value; }
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

        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }

        Dictionary<string, object> BiometricDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            BiometricDictionary.Add("IPAddress", _IPAddress);
            //BiometricDictionary.Add("DatabaseName", _DataBaseName);
            BiometricDictionary.Add("Catalog", _Catalog);
            BiometricDictionary.Add("Username", _Username);
            BiometricDictionary.Add("Password", _Password);
            BiometricDictionary.Add("Status", _Status);
            return "Success";
        }

        DBConnection db = new DBConnection();

        public string CreateBiometricDetails(Dictionary<string, object> BiometricsDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            BiometricDictionary=BiometricsDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertBiometric";
            cmd.Parameters.AddWithValue("@IPAddress", BiometricDictionary["IPAddress"]);
            //cmd.Parameters.AddWithValue("@DataBaseName", BiometricDictionary["DataBaseName"]); 
            cmd.Parameters.AddWithValue("@Catalog", BiometricDictionary["Catalog"]);
            cmd.Parameters.AddWithValue("@Username", BiometricDictionary["Username"]);
            cmd.Parameters.AddWithValue("@Password", BiometricDictionary["Password"]);
            cmd.Parameters.AddWithValue("@Status", BiometricDictionary["Status"]);
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

        public string EditBiometricDetails(Dictionary<string, object> BiometricsDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            BiometricDictionary=BiometricsDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateBiometric";
            cmd.Parameters.AddWithValue("@IPAddress", BiometricDictionary["IPAddress"]);
            //cmd.Parameters.AddWithValue("@DataBaseName", BiometricDictionary["DataBaseName"]);
            cmd.Parameters.AddWithValue("@Catalog", BiometricDictionary["Catalog"]);
            cmd.Parameters.AddWithValue("@Username", BiometricDictionary["Username"]);
            cmd.Parameters.AddWithValue("@Password", BiometricDictionary["Password"]);
            cmd.Parameters.AddWithValue("@Status", BiometricDictionary["Status"]);

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

        public Dictionary<string, object> ViewBiometricDetails()
        {
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewBiometric";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        BiometricDictionary=new Dictionary<string, object>();
                        BiometricDictionary.Add("IPAddress", ds.Tables[0].Rows[0]["IPAddress"].ToString());
                        //BiometricDictionary.Add("DataBaseName", ds.Tables[0].Rows[0]["DataBaseName"].ToString());
                        BiometricDictionary.Add("Catalog", ds.Tables[0].Rows[0]["Catalog"].ToString());
                        BiometricDictionary.Add("Username", ds.Tables[0].Rows[0]["Username"].ToString());
                        BiometricDictionary.Add("Password", ds.Tables[0].Rows[0]["Password"].ToString());
                        BiometricDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

                    }
                    else
                    {
                        BiometricDictionary.Add("response", "0");
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
                return BiometricDictionary;
            }
        }
    }
}