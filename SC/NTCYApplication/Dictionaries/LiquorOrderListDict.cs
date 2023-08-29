using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class LiquorOrderListDict
    {

        public Dictionary<string, object> LiquorOrderListDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            LiquorOrderListDictionary = new Dictionary<string, object>();
            LiquorOrderListDictionary.Add("SubOrderId", ds.Tables[0].Rows[0]["SubOrderId"].ToString());
            LiquorOrderListDictionary.Add("OrderId", ds.Tables[0].Rows[0]["OrderId"].ToString());
            LiquorOrderListDictionary.Add("LiquorName", ds.Tables[0].Rows[0]["LiquorName"].ToString());
            LiquorOrderListDictionary.Add("Quantity", ds.Tables[0].Rows[0]["Quantity"].ToString());
            LiquorOrderListDictionary.Add("Price", ds.Tables[0].Rows[0]["Price"].ToString());
            LiquorOrderListDictionary.Add("GST", ds.Tables[0].Rows[0]["GST"].ToString());
            return LiquorOrderListDictionary;

        }
    }
}