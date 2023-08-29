using NTCYApplication.Models.Club;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface ClubInterface
    {
        string CreateClubDetails(Dictionary<string, object> ClubCDictionary);

        Dictionary<string,object> EditClubDetails(int ClubId);  
        List<Club> ViewClubDetails(int ClubId);
        string UpdateClubDetails(Dictionary<string, object> ClubCDictionary);
         
    }
}
