using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{ 
    public class LiquorOrderAreaDict
    {
        public Dictionary<string, object> LiquororderAreaDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            LiquororderAreaDictionary = new Dictionary<string, object>();
            LiquororderAreaDictionary.Add("OrderNumber", ds.Tables[0].Rows[0]["OrderNumber"].ToString());
            LiquororderAreaDictionary.Add("MembershipNo", ds.Tables[0].Rows[0]["MembershipNo"].ToString());
            LiquororderAreaDictionary.Add("TableNo", ds.Tables[0].Rows[0]["TableNo"].ToString());
            LiquororderAreaDictionary.Add("Items", ds.Tables[0].Rows[0]["Items"].ToString());
            LiquororderAreaDictionary.Add("Qty", ds.Tables[0].Rows[0]["Qty"].ToString());
            LiquororderAreaDictionary.Add("Type", ds.Tables[0].Rows[0]["Type"].ToString());
            LiquororderAreaDictionary.Add("SubOrderId", ds.Tables[0].Rows[0]["SubOrderID"].ToString());
            return LiquororderAreaDictionary;
        }

    }
}