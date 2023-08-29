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
    public class ManageUsersController : Controller
    {
        private ApplicationDbContext context;
        // GET: ManageUsers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UsersWithRoles()
        {
            context = new ApplicationDbContext();
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_in_Role_Model()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }


        public ActionResult DeleteUser(string Uname)
        {
            context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser user = context.Users.Where(
                    u => u.UserName.Equals(Uname, StringComparison.CurrentCultureIgnoreCase)
                ).FirstOrDefault();

            UserManager.Delete(user);
            return RedirectToAction("UsersWithRoles");

        }


        public ActionResult ApplyRolesToUser(FormCollection Form)
        {
            context = new ApplicationDbContext();
            var username = Form["txt_username"].ToString();
            var Roles = context.Roles.ToList();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            ApplicationUser user = context.Users.Where(
                    u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)
                ).FirstOrDefault();
            

            foreach (var role in Roles)
            {
                bool isRoleSet = (Form["chk_" + role.Name] ?? "").Equals("on", StringComparison.CurrentCultureIgnoreCase);
                if (isRoleSet)
                {
                    UserManager.AddToRole(user.Id, role.Name);
                }
                else
                {
                    UserManager.RemoveFromRole(user.Id, role.Name);
                }
                
            }

            return RedirectToAction("UsersWithRoles", "ManageUsers");
        }

        public ActionResult Edit(FormCollection Form)
        {
            var email = Form["hf_email"].ToString();
            var username = Form["hf_username"].ToString();
            context = new ApplicationDbContext();
            ViewBag.Roles = new SelectList(context.Roles.ToList(), "Name", "Name");

            ApplicationUser user = context.Users.Where(
                    u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)
                ).FirstOrDefault();
                        
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);

           /* var account = new MVCUserRoles.Controllers.AccountController();
            ViewBag.RolesForThisUser = account.UserManager.GetRolesAsync(user.Id);*/

            ViewBag.txtName = username;
            ViewBag.txtemail = email;
            return View();
        }
    }
}