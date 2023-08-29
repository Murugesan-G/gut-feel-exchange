using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{ 
    public class CardGameDict
    {
        public Dictionary<string, object> CardGameDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        { 
            CardGameDictionary=new Dictionary<string, object>();
            CardGameDictionary.Add("GameId", ds.Tables[0].Rows[0]["GameId"].ToString());
            CardGameDictionary.Add("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
            CardGameDictionary.Add("TableNo", ds.Tables[0].Rows[0]["TableNo"].ToString());
            CardGameDictionary.Add("NoofMembers", ds.Tables[0].Rows[0]["NoofMembers"].ToString());
            CardGameDictionary.Add("Winner", ds.Tables[0].Rows[0]["Winner"].ToString());
            CardGameDictionary.Add("Game", ds.Tables[0].Rows[0]["Game"].ToString());
            CardGameDictionary.Add("AmountPaid", ds.Tables[0].Rows[0]["AmountPaid"].ToString());
            CardGameDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
            CardGameDictionary.Add("Date", ds.Tables[0].Rows[0]["Date"].ToString());

            return CardGameDictionary;
        }
    }
}

