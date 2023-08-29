using NTCYApplication.Models.Liquor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface LiquorInterface
    {
        string CreateLiquorDetails(Dictionary<string, object> LiquorIDictionary);
        string UpdateLiquorDetails(Dictionary<string, object> ClubCDictionary);

        List<Liquor> ViewAllLiquorDetails();
        List<Liquor> ViewAllLiquorCategory(); 
        string DeleteLiquor(int LiquorId);
        DataSet GetLiquors(string Prefix);
        Dictionary<string, object> SelectLiquor(int LiquorId);
    }
}