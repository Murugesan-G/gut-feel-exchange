using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.Clubs
{
    public class UserMasterController : Controller
    {
        IUser iuser = new UserMaster();
        IUserRoles roles = new UserRoles();

        //
        // GET: /UserMaster/
        List<UserRoles> List;
        public ActionResult CreateUser()
        {
            List=roles.ShowUserRoles();
            if (List!=null)
            {
                ViewBag.RolesList=List;
            }
            else
            {
                ViewBag.RolesList="Subscriptions Not Available";
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection Form)
        {
            Dictionary<string, object> UserDictionary = new Dictionary<string, object>();
            var UserId = Form["UserId"];
            if (Form["UserId"]=="0"||Form["UserId"]==""||Form["UserId"]==null)
            {
                ViewBag.submit="Submit";
                UserDictionary.Add("UserName", Form["UserName"]);
                UserDictionary.Add("Password", Form["Password"]);
                UserDictionary.Add("UserRoles", Form["UserRoles"]);
                UserDictionary.Add("Status", Form["Status"]);

                iuser.CreateUser(UserDictionary);
                ViewBag.message="User Details Inserted Successfully";
            }
            else
            {
                UserDictionary.Add("UserId", Form["UserId"]);
                UserDictionary.Add("UserName", Form["UserName"]);
                UserDictionary.Add("Password", Form["Password"]);
                UserDictionary.Add("UserRoles", Form["UserRoles"]);
                UserDictionary.Add("Status", Form["Status"]);

                iuser.EditUser(UserDictionary);

                ViewBag.submit="Submit";
                ViewBag.message="User Details Updated Successfully ";

            }
           
            return View();
        } 


        [HttpGet]
        public ActionResult ViewAllUser()
        {
           
            List<UserMaster> userList;
            userList=iuser.ViewAllUser();

            if (userList!=null||userList.Count!=0)
            {
                ViewBag.UserList=userList;
            }
            else
            {
                ViewBag.UserList="0";
            }

            return View(userList);
        }


        [HttpGet]
        public ActionResult SelectUser(int? UserId)
        { 
            Dictionary<string, object> UserDictionary;
            UserDictionary=iuser.SelectUser(UserId);
            if (UserDictionary.Count==1)
            {
                ViewBag.Error=UserDictionary["response"].ToString();
            }
            else
            {
                ViewBag.Submit="Update";
                ViewData["UserId"]=UserDictionary["UserId"].ToString();
                ViewData["UserName"]=UserDictionary["UserName"].ToString();
                ViewData["Password"]=UserDictionary["Password"].ToString();
                ViewData["UserRoles"]=UserDictionary["UserRoles"].ToString();
                ViewData["Status"]=UserDictionary["Status"].ToString();

            }
         
            return View("CreateUser");
        }

         
        public ActionResult DeleteUser(int? UserId)
        {
            string User;
            User=iuser.DeleteUser(UserId);
            if (User==null)
            {
                ViewBag.message="Unable to Delete User";
            }
            else
            {
                ViewBag.message="User Deleted ";
            }

            return RedirectToAction("ViewAllUser");
            // return View();
        }

    }
}
