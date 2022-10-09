using Microsoft.AspNetCore.Mvc;
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

namespace NTCY.Controllers.Liquor
{
    public class LiquorDetailController : Controller
    {
        private ILiquorService _LiquorService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public LiquorDetailController(ILiquorService liquorService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _LiquorService = liquorService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult CreateLiquor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateLiquor(NTCY.Models.LiquorDetails.Liquor liquor)
        {
            _LiquorService.Add(liquor);
            TempData["msg"] = "<script>alert('Liquor Added Succesfully');</script>";
            return RedirectToAction("Viewliquor", "Liquor");
        }
        [HttpGet]
        public IActionResult ViewLiquor()
        {
            try
            {
                List<NTCY.Models.LiquorDetails.Liquor> liquor = new List<NTCY.Models.LiquorDetails.Liquor>();

                var liquorData = _LiquorService.GetAll();
                foreach (var liquordata in liquorData)
                {
                    var liquorCategoryObj = _mapper.Map<NTCY.Models.LiquorDetails.Liquor>(liquordata);
                    liquor.Add(liquorCategoryObj);
                }
                if (liquor != null)
                {
                    ViewBag.LiquorList = liquor;
                }
                else
                {
                    TempData["msg"] = "<script>alert('Liquor Details Not Available');</script>";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.innerEx = ex.InnerException.Message;
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateLiquor(int liquorId)
        {
            var liquorData = _LiquorService.GetById(liquorId);
            return View(liquorData);
        }
        [HttpPost]
        public IActionResult UpdateLiquor(int liquorId, NTCY.Models.LiquorDetails.Liquor liquor)
        {
            _LiquorService.Update(liquorId, liquor);
            TempData["msg"] = "<script>alert('liquor Updated Succesfully');</script>";
            return RedirectToAction("ViewLiquor", "Liquor");
        }
        public IActionResult DeleteLiquor(int liquorId)
        {
            _LiquorService.Delete(liquorId);
            TempData["msg"] = "<script>alert('Liquor Deleted Succesfully');</script>";
            return RedirectToAction("ViewLiquor", "Liquor");
        }
    }
}
