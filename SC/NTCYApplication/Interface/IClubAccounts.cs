using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface IClubAccounts 
    {
        string CreateDetails(Dictionary<string, object> TallyIntegrationDictionary);

        string EditDetails(Dictionary<string, object> TallyIntegrationDictionary);
        Dictionary<string, object> ViewDetails(); 
    }
}
