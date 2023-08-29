using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class EmployeeDict 
    {
        public Dictionary<string, object> EmployeeDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            EmployeeDictionary = new Dictionary<string, object>();
            EmployeeDictionary.Add("EmployeeId", ds.Tables[0].Rows[0]["EmployeeId"].ToString());
            EmployeeDictionary.Add("Empid", ds.Tables[0].Rows[0]["Empid"].ToString());
            EmployeeDictionary.Add("EmployeeName", ds.Tables[0].Rows[0]["EmployeeName"].ToString());
            EmployeeDictionary.Add("EmployeeCategory", ds.Tables[0].Rows[0]["EmployeeCategory"].ToString());
            EmployeeDictionary.Add("Gender", ds.Tables[0].Rows[0]["Gender"].ToString());
            EmployeeDictionary.Add("DOB", ds.Tables[0].Rows[0]["DOB"].ToString());
            EmployeeDictionary.Add("PhoneNo", ds.Tables[0].Rows[0]["PhoneNo"].ToString());
            EmployeeDictionary.Add("EmailId", ds.Tables[0].Rows[0]["EmailId"].ToString());
            EmployeeDictionary.Add("Address", ds.Tables[0].Rows[0]["Address"].ToString());
          //  EmployeeDictionary.Add("DepartmentId", ds.Tables[0].Rows[0]["DepartmentId"].ToString());
            EmployeeDictionary.Add("JoiningDate", ds.Tables[0].Rows[0]["JoiningDate"].ToString());
            EmployeeDictionary.Add("LeavingDate", ds.Tables[0].Rows[0]["LeavingDate"].ToString());
            EmployeeDictionary.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
            return EmployeeDictionary;
        }
    }
}