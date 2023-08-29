using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    class Gym : Services
    {
        int GymId { get; set; }
        int UsagePermonth { get; set; }
        DateTime AvailableTimings { get; set; }
        string MembershipId { get; set; }
        string Status { get; set; }

        Dictionary<string, object> GymDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            GymDictionary.Add("GymId", GymId);
            GymDictionary.Add("UsagePermonth", UsagePermonth);
            GymDictionary.Add("AvailableTimings", AvailableTimings);
            GymDictionary.Add("MembershipId", MembershipId);
            GymDictionary.Add("Status", Status);

            return "Success";
        }


        string AddGym(Dictionary<string, object> GymDictionary)
        {
            return "Success";
        }


        string EditGym(Dictionary<string, object> GymDictionary)
        {
            return "Success";
        }

        string DeleteGym(int GymId)
        {
            return "Success";
        }

        string GenerateBill(Dictionary<string, object> GymDictionary)
        {
            return "Success";
        }

        string ViewAllGym()
        {
            return "Success";
        }
    }
}