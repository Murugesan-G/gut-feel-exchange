using Microsoft.AspNetCore.Mvc;

namespace NTCY.Controllers.Users
{
    public class UserMasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
