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
    public interface RoleMasterInterface
    {
        string AddRoles(Dictionary<string, object> RoleMasterDictionary);
        List<RoleMaster> ViewAllRoleMaster();
        string UpdateRoles(Dictionary<string, object> RoleMasterDictionary);
    }
}
