using Microsoft.AspNetCore.Mvc;
using NTCY.Models;
using System.Diagnostics;
using NTCY.Models.Users;

namespace NTCY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string sUserName, string sPassword)
        {
            if(sUserName =="Admin" && sPassword == "admin@123")
            {
                return RedirectToAction("ViewMembers", "Member");
            }
            else
            {
                TempData["msg"] = "<script>alert('Member Updated Succesfully');</script>";
                return RedirectToAction("ViewMembers", "Member");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}