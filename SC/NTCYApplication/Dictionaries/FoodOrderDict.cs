using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class FoodOrderDict
    {
        public Dictionary<string,object> FoodOrderDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary (Dictionary<string,object> Dict)
        {
            FoodOrderDictionary = new Dictionary<string, object>();
            FoodOrderDictionary.Add("OrderId", ds.Tables[0].Rows[0]["OrderId"].ToString());
            FoodOrderDictionary.Add("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
            FoodOrderDictionary.Add("MembershipNo", ds.Tables[0].Rows[0]["MembershipNo"].ToString());
            FoodOrderDictionary.Add("foodOrderList", ds.Tables[0].Rows[0]["foodOrderList"].ToString());
            FoodOrderDictionary.Add("TotalGST", ds.Tables[0].Rows[0]["TotalGST"].ToString());
            FoodOrderDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
            FoodOrderDictionary.Add("TableNo", ds.Tables[0].Rows[0]["TableNo"].ToString());
            FoodOrderDictionary.Add("TotalAmount", ds.Tables[0].Rows[0]["TotalAmount"].ToString());
            return FoodOrderDictionary;
        }
    }
}