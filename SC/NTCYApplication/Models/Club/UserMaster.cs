using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    public class UserMaster : IUser
    {
        int _UserId { get; set; }
        string _UserName { get; set; }
        string _Password { get; set; }
        string _Status { get; set; }
        string _UserRoles { get; set; }

        public int UserId
        {
            get { return _UserId; }
            set { _UserId=value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName=value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password=value; }
        }
        public string UserRoles
        { 
            get { return _UserRoles; }
            set { _UserRoles=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }

        Dictionary<string, object> UserDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            UserDictionary.Add("UserId", _UserId);
            UserDictionary.Add("UserName", _UserName);
            UserDictionary.Add("Password", _Password);
            UserDictionary.Add("UserRoles", _UserRoles);
            UserDictionary.Add("Status", _Status);

            return "Success";
        }


        DBConnection db = new DBConnection();

        public string CreateUser(Dictionary<string, object> UsrDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            UserDictionary=UsrDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertUserMaster";
            cmd.Parameters.AddWithValue("@UserName", UserDictionary["UserName"]);
            cmd.Parameters.AddWithValue("@Password", UserDictionary["Password"]);
            cmd.Parameters.AddWithValue("@UserRoles", UserDictionary["UserRoles"]);
            cmd.Parameters.AddWithValue("@Status", UserDictionary["Status"]);

            SqlConnection MyCon = db.OpenConnection();
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
            db.CloseConnection();
            return response;
        }

        public string EditUser(Dictionary<string, object> UsrDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            UserDictionary=UsrDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateUserMaster";
            cmd.Parameters.AddWithValue("@UserId", UserDictionary["UserId"]);
            cmd.Parameters.AddWithValue("@UserName", UserDictionary["UserName"]);
            cmd.Parameters.AddWithValue("@Password", UserDictionary["Password"]);
            cmd.Parameters.AddWithValue("@UserRoles", UserDictionary["UserRoles"]);
            cmd.Parameters.AddWithValue("@Status", UserDictionary["Status"]);
            SqlConnection MyCon = db.OpenConnection();
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
            db.CloseConnection();
            return response;
        }

        public string DeleteUser(int? UserId)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteUserMaster";
            cmd.Parameters.AddWithValue("@UserId", UserId);
            SqlConnection MyCon = db.OpenConnection();
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
            db.CloseConnection();
            return response;
        }

        public Dictionary<string, object> SelectUser(int? UserId)
        {
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectUser";
            cmd.Parameters.AddWithValue("@UserId", UserId);
            SqlConnection MyCon = db.OpenConnection();
            cmd.Connection=MyCon;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count>0)
                {
                    UserDictionary=new Dictionary<string, object>();
                    UserDictionary.Add("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
                    UserDictionary.Add("UserName", ds.Tables[0].Rows[0]["UserName"].ToString());
                    UserDictionary.Add("Password", ds.Tables[0].Rows[0]["Password"].ToString());
                    UserDictionary.Add("UserRoles", ds.Tables[0].Rows[0]["UserRoles"].ToString());
                    UserDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

                }
                else
                {
                    UserDictionary.Add("response", 0);
                }
            }
            catch (SqlException e)
            {
                ErrorString=e.ToString();
            }
            db.CloseConnection();
            return UserDictionary;
        }

        public List<UserMaster> ViewAllUser()
        {
            String ErrorString = string.Empty;
            DataSet ds = new DataSet();
            List<UserMaster> list = new List<UserMaster>();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewUserMaster";
            SqlConnection MyCon = db.OpenConnection();
            cmd.Connection=MyCon;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        UserMaster user = new UserMaster();
                        user._UserId=Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                        user._UserName=ds.Tables[0].Rows[i]["UserName"].ToString();
                        user._Password=ds.Tables[0].Rows[i]["Password"].ToString();
                        user._UserRoles=ds.Tables[0].Rows[i]["UserRoles"].ToString();
                        user._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                        list.Add(user);
                    }

                }
            }
            catch (SqlException e)
            {
                ErrorString=e.ToString();
            }
            db.CloseConnection();
            return list;
        }

        public Dictionary<string, object> UserLogin(Dictionary<string, object> UserDictionary)
        {
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="UserLogin";
            cmd.Parameters.AddWithValue("@UserName", UserDictionary["UserName"]);
            cmd.Parameters.AddWithValue("@Password", UserDictionary["Password"]);
            SqlConnection MyCon = db.OpenConnection();
            cmd.Connection=MyCon;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count>0)
                {
                    UserDictionary=new Dictionary<string, object>();
                    UserDictionary.Add("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
                    UserDictionary.Add("UserName", ds.Tables[0].Rows[0]["UserName"].ToString());
                    UserDictionary.Add("Password", ds.Tables[0].Rows[0]["Password"].ToString());
                    UserDictionary.Add("UserRoles", ds.Tables[0].Rows[0]["UserRoles"].ToString());
                    UserDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

                }
                else
                {
                    UserDictionary.Add("response", 0);
                }
            }
            catch (SqlException e)
            {
                ErrorString=e.ToString();
            }
            db.CloseConnection();
            return UserDictionary;
        }
    }
}