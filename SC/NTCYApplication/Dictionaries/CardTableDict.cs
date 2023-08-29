using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class CardTableDict 
    {
        public Dictionary<string, object> CardTableDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            CardTableDictionary = new Dictionary<string, object>();
            CardTableDictionary.Add("TableNo", ds.Tables[0].Rows[0]["TableNo"].ToString());
            CardTableDictionary.Add("TableName", ds.Tables[0].Rows[0]["TableName"].ToString());
            CardTableDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

            return CardTableDictionary;
        }
    }
}