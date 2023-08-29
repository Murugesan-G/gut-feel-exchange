
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    interface iEmployee
    { 
        string CreateEmployee(Dictionary<string, object> EmployeesDictionary);
        string UpdateEmployee(Dictionary<string, object> EmployeesDictionary);
        List<Employees>ViewAllEmployee();
        string DeleteEmployee(int EmployeeId);
        Dictionary<string, object> SelectEmployee(int EmployeeId);


    }
}
