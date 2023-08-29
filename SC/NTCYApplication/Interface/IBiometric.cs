using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces 
{
    public interface IBiometric
    {
        string CreateBiometricDetails(Dictionary<string, object> BiometricsDictionary);
        
        string EditBiometricDetails(Dictionary<string, object> BiometricsDictionary);

        Dictionary<string, object> ViewBiometricDetails();
    } 
}
