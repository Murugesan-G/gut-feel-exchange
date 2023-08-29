using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{ 
  
    public class ServiceDict
    {
        public Dictionary<string, object> ServiceDictionary { get; set; }
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            ServiceDictionary = new Dictionary<string, object>();
            ServiceDictionary.Add("ServiceId", ds.Tables[0].Rows[0]["ServiceId"].ToString());
            ServiceDictionary.Add("ServiceCode", ds.Tables[0].Rows[0]["ServiceCode"].ToString());
            ServiceDictionary.Add("ServiceName", ds.Tables[0].Rows[0]["ServiceName"].ToString());
            ServiceDictionary.Add("Description", ds.Tables[0].Rows[0]["Description"].ToString());
            ServiceDictionary.Add("AvailabilityStatus", ds.Tables[0].Rows[0]["AvailabilityStatus"].ToString());
            ServiceDictionary.Add("PerHour", ds.Tables[0].Rows[0]["PerHour"].ToString());
            ServiceDictionary.Add("PerHalfDay", ds.Tables[0].Rows[0]["PerHalfDay"].ToString());
            ServiceDictionary.Add("FullDay", ds.Tables[0].Rows[0]["FullDay"].ToString());
            ServiceDictionary.Add("FullMonth", ds.Tables[0].Rows[0]["FullMonth"].ToString());
            ServiceDictionary.Add("Rate", ds.Tables[0].Rows[0]["Rate"].ToString());
            ServiceDictionary.Add("Validity", ds.Tables[0].Rows[0]["Validity"]);
            ServiceDictionary.Add("GST", ds.Tables[0].Rows[0]["GST"]);
            return ServiceDictionary;
        }
    }
}