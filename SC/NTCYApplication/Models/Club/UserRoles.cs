using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    public class UserRoles:IUserRoles
    {
        int _RoleId;
        string _RoleName;
        public int RoleId
        {
            get { return RoleId; }
            set { RoleId = value; }
        }
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        DBConnection db = new DBConnection();
        public List<UserRoles> ShowUserRoles()
        { 
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<UserRoles> list = new List<UserRoles>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetUserRoles";
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
                        UserRoles roles = new UserRoles();

                        roles._RoleId=Convert.ToInt32(ds.Tables[0].Rows[i]["RoleId"]);
                        roles._RoleName=ds.Tables[0].Rows[i]["RoleName"].ToString();
                        list.Add(roles);
                    }
                }

            }
            catch (SqlException e)
            {
                response=e.ToString();

            }
            db.CloseConnection();
            return list;
        }
    }
}