using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{ 
    public class RoleMasterDict
    {
        public Dictionary<string, object> RoleMasterDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            RoleMasterDictionary = new Dictionary<string, object>();
            RoleMasterDictionary.Add("RoleId", ds.Tables[0].Rows[0]["RoleId"].ToString());
            RoleMasterDictionary.Add("RoleName", ds.Tables[0].Rows[0]["RoleName"].ToString());
            RoleMasterDictionary.Add("UserName", ds.Tables[0].Rows[0]["UserName"].ToString());
            RoleMasterDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

            return RoleMasterDictionary;
        }
    }
}