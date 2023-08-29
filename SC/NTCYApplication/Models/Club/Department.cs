using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    class Department
    {

        int DepartmentId { get; set; }
        string DepartmentName { get; set; }
        string Status { get; set; }

        string AddDepartment(string DepartmentName, string Status)
        {
            return "Success";
        }

        string EditDepartment(int DepartmentId, string DepartmentName, string Status)
        {
            return "Success";
        }

        string DeleteDepartment(int DepartmentId)
        {
            return "Success";
        }

        string ViewAllDepartment()
        {
            return "Success";
        }
    }
}