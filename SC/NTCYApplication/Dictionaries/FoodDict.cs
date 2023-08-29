using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class FoodDict
    {
        public Dictionary<string, object> FoodDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            FoodDictionary = new Dictionary<string, object>();
            FoodDictionary.Add("FoodId", ds.Tables[0].Rows[0]["FoodId"].ToString());
            FoodDictionary.Add("ItemCode", ds.Tables[0].Rows[0]["FoodCode"].ToString());
            FoodDictionary.Add("ItemName", ds.Tables[0].Rows[0]["FoodName"].ToString());
            FoodDictionary.Add("ItemCategory", ds.Tables[0].Rows[0]["Category"].ToString());
            FoodDictionary.Add("Description", ds.Tables[0].Rows[0]["FoodDescription"].ToString());
            FoodDictionary.Add("Quantity", ds.Tables[0].Rows[0]["Quantity"].ToString());
            FoodDictionary.Add("RatePerUnit", ds.Tables[0].Rows[0]["Price"].ToString());
            FoodDictionary.Add("GST", ds.Tables[0].Rows[0]["GST"].ToString());
            FoodDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

            return FoodDictionary;
        }
    }
}