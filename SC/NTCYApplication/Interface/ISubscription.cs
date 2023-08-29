using NTCYApplication.Models.Club;
using NTCYApplication.Models.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Interfaces
{
    public interface ISubscription
    {
        int Save();
        string CreateSubscription(Dictionary<string, object> SubcriptionDictionary);
        string UpdateSubscription(Dictionary<string, object> SubcriptionDictionary); 
        Dictionary<string, object> SelectSubscription(int? SubscriptionId);
        string DeleteSubscription(int? SubscriptionId);
        List<Subscription> ViewAllSubscriptions();
       
    }
}