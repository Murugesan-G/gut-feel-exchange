using NTCYApplication.Models.Club;
using NTCYApplication.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTCYApplication.Interfaces
{
    public interface ServiceInterface
    {
        int Save();
        string CreateService(Dictionary<string, object> ServiceIDictionary);
        string UpdateService(Dictionary<string, object> ServiceIDictionary);
        Dictionary<string, object> SelectService(int ServiceId);
        string DeleteService(int ServiceCode);
        List<Services> ViewAllServices();
        
    }
}