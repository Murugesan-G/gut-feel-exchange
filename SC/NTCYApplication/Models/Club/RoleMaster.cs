using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace NTCYApplication.Models.Club
{
    public class RoleMaster: RoleMasterInterface
    {
        private int _RoleId;
        private string _RoleName;
        private string _Username;
        private string _ScreenName;
        private string _Status;
      public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        public string ScreenName
        {
            get { return _ScreenName; }
            set { _ScreenName = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        Dictionary<string, object> RoleMasterDictionary = new Dictionary<string, object>();
        DBConnection db = new DBConnection();
        string BindDictionary()
        {
            RoleMasterDictionary.Add("RoleId", _RoleId);
            RoleMasterDictionary.Add("RoleName", _RoleName);
            RoleMasterDictionary.Add("Username", _Username);
            RoleMasterDictionary.Add("ScreenName", _ScreenName);
            RoleMasterDictionary.Add("Status", _Status);

            return "Success";
        }


        public int AddRoles(Dictionary<string, object> RoleMasterIDictionary)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            RoleMasterDictionary = RoleMasterIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            cmd.Parameters.AddWithValue("@RoleId", RoleMasterDictionary["RoleId"]);
            cmd.Parameters.AddWithValue("@RoleName", RoleMasterDictionary["RoleName"]);
            cmd.Parameters.AddWithValue("@Username", RoleMasterDictionary["Username"]);
            cmd.Parameters.AddWithValue("@ScreenName", RoleMasterDictionary["ScreenName"]);
            cmd.Parameters.AddWithValue("@Status", RoleMasterDictionary["Status"]);
            SqlConnection MyCon = db.OpenConnection();
            cmd.Connection = MyCon;
            try
            {
                SqlParameter Param = new SqlParameter("@RoleId", 0);
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                response = cmd.ExecuteNonQuery();
                int RoleId = Convert.ToInt32(Param.Value);
                response = RoleId;
            }
            catch (SqlException e)
            {
                response = RoleId;
            }
            db.CloseConnection();
            return response;
        }


        public List<RoleMaster> ViewAllRoleMaster()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<RoleMaster> list = new List<RoleMaster>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";

            try
            {
                SqlConnection MyCon = db.OpenConnection();
                cmd.Connection = MyCon;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RoleMaster Det = new RoleMaster();
                        Det._RoleId = Convert.ToInt32(ds.Tables[0].Rows[i]["RoleId"]);
                        Det._RoleName = ds.Tables[0].Rows[i]["RoleName"].ToString();
                        Det._Username = ds.Tables[0].Rows[i]["Username"].ToString();
                        Det._ScreenName = ds.Tables[0].Rows[i]["ScreenName"].ToString();
                        Det._Status = ds.Tables[0].Rows[i]["Status"].ToString();
                        list.Add(Det);
                    }
                }
            }
            catch (SqlException e)
            {
                response = e.ToString();

            }
            db.CloseConnection();
            return list;
        }



        public string UpdateRoles(Dictionary<string, object> RoleMasterDictionary)
        {
            throw new NotImplementedException();
        }

        string RoleMasterInterface.AddRoles(Dictionary<string, object> RoleMasterDictionary)
        {
            throw new NotImplementedException();
        }

    }
}