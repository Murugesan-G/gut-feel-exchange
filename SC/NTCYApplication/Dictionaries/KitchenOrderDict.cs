using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace NTCYApplication.Dictionaries
{ 
    public class KitchenOrderDict
    {
        public Dictionary<string,Object> KitchenOrderDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string,object> BindDictionary(DataSet ds)
        {
            KitchenOrderDictionary = new Dictionary<string, object>();
            KitchenOrderDictionary.Add("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
            KitchenOrderDictionary.Add("MembershipNo", ds.Tables[0].Rows[0]["MembershipNo"].ToString());
            KitchenOrderDictionary.Add("TableNo", ds.Tables[0].Rows[0]["TableNo"].ToString());
            KitchenOrderDictionary.Add("Items", ds.Tables[0].Rows[0]["Items"].ToString());
            KitchenOrderDictionary.Add("Qty", ds.Tables[0].Rows[0]["Qty"].ToString());
            return KitchenOrderDictionary;
        }
    }
}