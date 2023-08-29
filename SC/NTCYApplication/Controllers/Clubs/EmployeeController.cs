using System.Collections.Generic;
using System.Web.Mvc;
using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;

namespace NTCYApplication.Controllers.Clubs
{
    [Authorize(Roles = "Admin,Management")]
    public class EmployeeController : Controller
    {
        iEmployee iEmployee = new Employees();
        //
        // GET: /Employee/

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(FormCollection Form)
        {
            try
            {
                Dictionary<string, object> EmployeeDictionary = new Dictionary<string, object>();

                var EmployeeId = Form["EmployeeId"];
                if (Form["EmployeeId"]=="0"||Form["EmployeeId"]==""||Form["EmployeeId"]==null)
                {
                    ViewBag.submit="Submit";
                    EmployeeDictionary.Add("Empid", Form["Empid"]);
                    EmployeeDictionary.Add("EmployeeCategory", Form["EmployeeCategory"]);
                    EmployeeDictionary.Add("EmployeeName", Form["EmployeeName"]);
                    EmployeeDictionary.Add("DOB", Form["DOB"]);
                    // EmployeeDictionary.Add("Age", Form["Age"]);
                    EmployeeDictionary.Add("Gender", Form["Gender"]);
                    EmployeeDictionary.Add("PhoneNo", Form["PhoneNo"]);
                    EmployeeDictionary.Add("Address", Form["Address"]);
                    EmployeeDictionary.Add("EmailId", Form["EmailId"]);
                   // EmployeeDictionary.Add("DepartmentId", Form["DepartmentId"]);
                    EmployeeDictionary.Add("JoiningDate", Form["JoiningDate"]);
                    var leavingdate = Form["LeavingDate"];
                    if (leavingdate==""||leavingdate==null)
                    {
                        EmployeeDictionary.Add("LeavingDate", DBNull.Value);
                    }
                    else
                    {
                        EmployeeDictionary.Add("LeavingDate", leavingdate);
                    }

                    EmployeeDictionary.Add("Status", Form["Status"]);

                    iEmployee.CreateEmployee(EmployeeDictionary);
                    ViewBag.message="Employee Details Inserted Successfully";

                }
                else
                {
                    EmployeeDictionary.Add("EmployeeId", Form["EmployeeId"]);
                    EmployeeDictionary.Add("Empid", Form["Empid"]);
                    EmployeeDictionary.Add("EmployeeCategory", Form["EmployeeCategory"]);
                    EmployeeDictionary.Add("EmployeeName", Form["EmployeeName"]);
                    EmployeeDictionary.Add("DOB", Form["DOB"]);
                    //EmployeeDictionary.Add("Age", Form["Age"]);
                    EmployeeDictionary.Add("Gender", Form["Gender"]);
                    EmployeeDictionary.Add("PhoneNo", Form["PhoneNo"]);
                    EmployeeDictionary.Add("Address", Form["Address"]);
                    EmployeeDictionary.Add("EmailId", Form["EmailId"]);
                   // EmployeeDictionary.Add("DepartmentId", Form["DepartmentId"]);
                    EmployeeDictionary.Add("JoiningDate", Form["JoiningDate"]);
                    var leavingdate = Form["LeavingDate"];
                    if (leavingdate==""||leavingdate==null)
                    {
                        EmployeeDictionary.Add("LeavingDate", DBNull.Value);
                    }
                    else
                    {
                        EmployeeDictionary.Add("LeavingDate", leavingdate);
                    }

                    EmployeeDictionary.Add("Status", Form["Status"]);

                    iEmployee.UpdateEmployee(EmployeeDictionary);

                    ViewBag.submit="Submit";
                    ViewBag.message="Employee Details Updated Successfully ";
                }

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult ViewAllEmployees()
        {
            try
            {
                List<Employees> sublist;
                sublist=iEmployee.ViewAllEmployee();
                if (sublist!=null)
                {
                    ViewBag.SubList=sublist;
                }
                else
                {
                    ViewBag.message="Employee Details Not Available";
                }

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }


        public ActionResult ViewEmployee(int EmployeeID)
        {
            try
            {
                Dictionary<string, object> EmployeeDictionary;
                EmployeeDictionary=iEmployee.SelectEmployee(EmployeeID);
                if (EmployeeDictionary.Count>0)
                {
                    ViewBag.submit="Update";
                    ViewData["EmployeeId"]=EmployeeDictionary["EmployeeId"].ToString();
                    ViewData["Empid"] = EmployeeDictionary["Empid"].ToString();
                    ViewData["EmployeeCategory"]=EmployeeDictionary["EmployeeCategory"].ToString();
                    ViewData["EmployeeName"]=EmployeeDictionary["EmployeeName"].ToString();
                    DateTime x = Convert.ToDateTime(EmployeeDictionary["DOB"].ToString());
                    ViewData["DOB"]=x.ToShortDateString();                    
                    ViewData["Gender"]=EmployeeDictionary["Gender"].ToString();
                    ViewData["PhoneNo"]=EmployeeDictionary["PhoneNo"].ToString();
                    ViewData["Address"]=EmployeeDictionary["Address"].ToString();
                    ViewData["EmailId"]=EmployeeDictionary["EmailId"].ToString();
                    // ViewData["DepartmentId"]=EmployeeDictionary["DepartmentId"].ToString();
                    DateTime y = Convert.ToDateTime(EmployeeDictionary["JoiningDate"].ToString());
                    ViewData["JoiningDate"]=y.ToShortDateString();
                    DateTime z = Convert.ToDateTime(EmployeeDictionary["LeavingDate"].ToString());
                    ViewData["LeavingDate"] = z.ToShortDateString();
                    ViewData["Status"]=EmployeeDictionary["Status"].ToString();
                }
                else
                {
                    ViewBag.message="Employee Details Failed to Load";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View("CreateEmployee", ViewBag.Data);
        }

        public ActionResult DeleteEmployee(int EmployeeID)
        {
            try
            {
                string sub;
                sub=iEmployee.DeleteEmployee(EmployeeID);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Employee Details";
                }
                else
                {
                    ViewBag.message="Employee Details Deleted Successfully";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return RedirectToAction("ViewAllEmployees");

        }
    }
}
