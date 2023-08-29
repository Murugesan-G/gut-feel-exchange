using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.Clubs
{
    public class RoleMasterController : Controller
    {
        // IRoles iRoleMaster = new RoleMaster();
        public ActionResult AddRoles(RoleMaster r)
        {
            return View();
        }



        [HttpPost]
        public ActionResult AddRoles(FormCollection FormData)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewAllRoles()
        {
            return View();
        }
    }
}
