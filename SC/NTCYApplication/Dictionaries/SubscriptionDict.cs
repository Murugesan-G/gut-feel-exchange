using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class SubscriptionDict
    {
        public Dictionary<string, object> SubcriptionDictionary { get; set; }
        //DataSet ds = new DataSet();

        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            SubcriptionDictionary=new Dictionary<string, object>();
            SubcriptionDictionary.Add("SubscriptionId",ds.Tables[0].Rows[0]["SubscriptionId"].ToString());
            SubcriptionDictionary.Add("SubscriptionType", ds.Tables[0].Rows[0]["SubscriptionType"].ToString());
            SubcriptionDictionary.Add("SubscriptionRate", ds.Tables[0].Rows[0]["SubscriptionRate"].ToString());
            SubcriptionDictionary.Add("SubscriptionValidity", ds.Tables[0].Rows[0]["SubscriptionValidity"].ToString());
            SubcriptionDictionary.Add("PaymentInstallments", ds.Tables[0].Rows[0]["PaymentInstallments"].ToString());
            SubcriptionDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
            SubcriptionDictionary.Add("JoiningAmount", ds.Tables[0].Rows[0]["JoiningAmount"].ToString());

            return SubcriptionDictionary;
        }
    }
}