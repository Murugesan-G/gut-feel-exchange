using Microsoft.AspNetCore.Mvc;
using NTCY.Models;
using NTCY.Services.FoodService;
using NTCY.Models.LiquorDetails;
using Microsoft.Extensions.Options;
using AutoMapper;
using NTCY.Utils;
using NTCY.Services.Table;
using NTCY.Models.Table;
using NTCY.Entities;
using NTCY.Models.Foods;
using NTCY.Services.LiquorDetails;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTCY.Controllers.Liquor
{
    public class LiquorDetailController : Controller
    {
        [HttpGet]
        public ActionResult CreateLiquor()
        {
            LiquorDet liquorDet = new LiquorDet();
            LiquorService liquorService = new LiquorService();
            liquorDet.Categories = liquorService.GetLiquorCategories();
            return View(liquorDet);
        }

        [HttpPost]
        public ActionResult CreateLiquor(LiquorDet liquorDet)
        {
            LiquorService liquorService = new LiquorService();
            liquorService.SaveLiquourDetails(liquorDet);
            TempData["msg"] = "<script>alert('Liquor Added Succesfully');</script>";
            return RedirectToAction("Viewliquor", "LiquorDetail");
        }

        [HttpGet]
        public IActionResult ViewLiquor()
        {
            List<LiquorDet> liquor = new List<LiquorDet>();

            LiquorService liquorService = new LiquorService();
            liquor = (List<LiquorDet>)liquorService.GetAll();
            ViewBag.LiquorList = liquor;
            return View();
        }

        [HttpGet]
        public IActionResult UpdateLiquor(int liquorId)
        {
            LiquorDet liquorDet = new LiquorDet();
            LiquorService liquorService = new LiquorService();
            liquorDet = liquorService.GetById(liquorId);
            liquorDet.Categories = liquorService.GetLiquorCategories();
            return View(liquorDet);
        }
        [HttpPost]
        public IActionResult UpdateLiquor(int liquorId, LiquorDet liquorDet)
        {
            LiquorService liquorService = new LiquorService();
            liquorService.Update(liquorId,liquorDet);
            TempData["msg"] = "<script>alert('liquor Updated Succesfully');</script>";
            return RedirectToAction("ViewLiquor", "LiquorDetail");
        }
        public IActionResult DeleteLiquor(int liquorId)
        {
            LiquorService liquorService = new LiquorService();
            liquorService.Delete(liquorId);
            TempData["msg"] = "<script>alert('Liquor Deleted Succesfully');</script>";
            return RedirectToAction("ViewLiquor", "LiquorDetail");
        }
        [NonAction]
        public void FinalAddUpdate(LiquorDet liquorDet)
        {
            //To reduce the code in Main Controller we can use NonAction to keep the clean code. It will avoid duplicate coding.
        }
    }
}
