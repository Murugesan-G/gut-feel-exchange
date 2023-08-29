using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    class Sauna : Services
    {
        int SaunaId { get; set; }
        string MembershipId { get; set; }
        DateTime Usage { get; set; }
        string Status { get; set; }
        double Charges { get; set; }


        Dictionary<string, object> SaunaUsageDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            SaunaUsageDictionary.Add("SaunaId", SaunaId);
            SaunaUsageDictionary.Add("MembershipId", MembershipId);
            SaunaUsageDictionary.Add("Usage", Usage);
            SaunaUsageDictionary.Add("Status", Status);
            SaunaUsageDictionary.Add("Charges", Charges);
            return "Success";
        }


        string BookSauna(Dictionary<string, object> SaunaUsageDictionary)
        {
            return "Success";
        }


        string EditSauna(Dictionary<string, object> SaunaUsageDictionary)
        {
            return "Success";
        }

        string DeleteSauna(int SaunaId)
        {
            return "Success";
        }

        string ViewAllSauna()
        {
            return "Success";
        }

    }
}