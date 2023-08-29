using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class FoodOrderListDict 
    {
        public Dictionary<string, object> FoodOrderListDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            FoodOrderListDictionary = new Dictionary<string, object>();
            FoodOrderListDictionary.Add("SubOrderId", ds.Tables[0].Rows[0]["SubOrderId"].ToString());
            FoodOrderListDictionary.Add("OrderId", ds.Tables[0].Rows[0]["OrderId"].ToString());
            FoodOrderListDictionary.Add("FoodName", ds.Tables[0].Rows[0]["FoodName"].ToString());
            FoodOrderListDictionary.Add("Quantity", ds.Tables[0].Rows[0]["Quantity"].ToString());
            FoodOrderListDictionary.Add("Price", ds.Tables[0].Rows[0]["Price"].ToString());
            FoodOrderListDictionary.Add("GST", ds.Tables[0].Rows[0]["GST"].ToString());
            FoodOrderListDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
            return FoodOrderListDictionary;
        }
    }
}