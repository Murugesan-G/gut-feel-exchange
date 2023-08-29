using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{ 
    public class LiquorOrderDict
    {
        public Dictionary<string, object> LiquorOrderDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            LiquorOrderDictionary = new Dictionary<string, object>();
            LiquorOrderDictionary.Add("OrderId", ds.Tables[0].Rows[0]["OrderId"].ToString());
            LiquorOrderDictionary.Add("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
            LiquorOrderDictionary.Add("MembershipNo", ds.Tables[0].Rows[0]["MembershipNo"].ToString());
            LiquorOrderDictionary.Add("foodOrderList", ds.Tables[0].Rows[0]["foodOrderList"].ToString());
            LiquorOrderDictionary.Add("TotalGST", ds.Tables[0].Rows[0]["TotalGST"].ToString());
            LiquorOrderDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
            LiquorOrderDictionary.Add("TableNo", ds.Tables[0].Rows[0]["TableNo"].ToString());
            LiquorOrderDictionary.Add("TotalAmount", ds.Tables[0].Rows[0]["TotalAmount"].ToString());
            LiquorOrderDictionary.Add("Type", ds.Tables[0].Rows[0]["Type"].ToString());
            return LiquorOrderDictionary;
        }

    }
}