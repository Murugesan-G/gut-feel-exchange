using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class ClubCommitteeDict
    {
        string CommitteeMembers;
        public Dictionary<string, object> ClubCommitteeDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            ClubCommitteeDictionary=new Dictionary<string, object>();
            ClubCommitteeDictionary.Add("ClubId", ds.Tables[0].Rows[0]["ClubId"]);
            ClubCommitteeDictionary.Add("CommitteeId", ds.Tables[0].Rows[0]["CommitteeId"]);
            ClubCommitteeDictionary.Add("Chairman", ds.Tables[0].Rows[0]["Chairman"].ToString());
            ClubCommitteeDictionary.Add("President", ds.Tables[0].Rows[0]["President"].ToString());
            ClubCommitteeDictionary.Add("VicePresident", ds.Tables[0].Rows[0]["VicePresident"].ToString());
            ClubCommitteeDictionary.Add("Secretary", ds.Tables[0].Rows[0]["Secretary"].ToString());
            ClubCommitteeDictionary.Add("Treasurer", ds.Tables[0].Rows[0]["Treasurer"].ToString());

            string[] committeeMembers = ds.Tables[0].Rows[0]["CommitteeMembers"].ToString().Split(',');

            if (committeeMembers[0]=="")
            {
                CommitteeMembers= "Empty";
            }
            else
            {
                for (int j = 0; j<committeeMembers.Length; j++)
                {
                   
                        CommitteeMembers+=committeeMembers[j].ToString().Trim()+",";
                    
                }
            }
            
           
            ClubCommitteeDictionary.Add("CommitteeMembers", CommitteeMembers.ToString().TrimEnd(','));
            ClubCommitteeDictionary.Add("StartDate", ds.Tables[0].Rows[0]["StartDate"].ToString());
            ClubCommitteeDictionary.Add("EndDate", ds.Tables[0].Rows[0]["EndDate"].ToString());
            ClubCommitteeDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());

            return ClubCommitteeDictionary;
        }
    }
}