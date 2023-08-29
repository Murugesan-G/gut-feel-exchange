using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    interface IUser
    {
        string CreateUser(Dictionary<string, object> UsrDictionary);
        string EditUser(Dictionary<string, object> UsrDictionary); 
        string DeleteUser(int? UserId);
        Dictionary<string, object> SelectUser(int? UserId);
        List<UserMaster> ViewAllUser();
        Dictionary<string, object> UserLogin(Dictionary<string, object> UserDictionary);
        
    }
}
