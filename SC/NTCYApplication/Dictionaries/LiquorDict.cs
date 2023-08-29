using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class LiquorDict 
    {
        public Dictionary<string, object> LiquorDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            LiquorDictionary = new Dictionary<string, object>();
            LiquorDictionary.Add("LiquorId", ds.Tables[0].Rows[0]["LiquorId"].ToString());
            LiquorDictionary.Add("LiquorName", ds.Tables[0].Rows[0]["LiquorName"].ToString());
            LiquorDictionary.Add("CategoryName", ds.Tables[0].Rows[0]["Liquor_Cat_Id"].ToString());
            //LiquorDictionary.Add("Vendor", ds.Tables[0].Rows[0]["Vendor"].ToString());
            LiquorDictionary.Add("EffectiveDate", ds.Tables[0].Rows[0]["EffectiveDate"].ToString());
            LiquorDictionary.Add("PegorBottle", ds.Tables[0].Rows[0]["PegorBottle"].ToString());
            LiquorDictionary.Add("PegsPerBottle", ds.Tables[0].Rows[0]["PegsPerBottle"].ToString());
            LiquorDictionary.Add("Volume", ds.Tables[0].Rows[0]["Volume"].ToString());
            //LiquorDictionary.Add("RatePerPeg", ds.Tables[0].Rows[0]["RatePerPeg"].ToString());
            //LiquorDictionary.Add("RatePerBottle", ds.Tables[0].Rows[0]["RatePerBottle"].ToString());
            LiquorDictionary.Add("SellingPriceBottle", ds.Tables[0].Rows[0]["SellingPricePerBottle"].ToString());
            LiquorDictionary.Add("SellingPricePeg", ds.Tables[0].Rows[0]["SellingPricePerPeg"].ToString());
            LiquorDictionary.Add("GST", ds.Tables[0].Rows[0]["GST_Rate"].ToString());
            LiquorDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
            return LiquorDictionary;
        }
    }
}
