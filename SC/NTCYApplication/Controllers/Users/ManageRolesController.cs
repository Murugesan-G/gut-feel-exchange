using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NTCYApplication.Models;
using NTCYApplication.Models.Users;
using System;
using System.Linq;
using System.Web.Mvc;
namespace NTCYApplication.Controllers.Users
{
    [Authorize(Roles = "Admin,Management")]
    public class ManageRolesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: ManageRoles
    
        public ActionResult Index()
        {
           
            var roles = context.Roles.ToList();
            return View(roles);
        }
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection Form)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
           if (!roleManager.RoleExists(Form["txt_RoleName"].ToString()))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = Form["txt_RoleName"].ToString();
                roleManager.Create(role);
                ViewBag.ResultMessage = "Role created successfully !";
            }
            return RedirectToAction("Index");
        }
 

        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}