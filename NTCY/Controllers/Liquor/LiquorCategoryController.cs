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

namespace NTCY.Controllers.LiquorC
{
    public class LiquorCategoryController : Controller
    {
        private ILiquorCategoryService _LiquorCategoryService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public LiquorCategoryController(ILiquorCategoryService liquorCategoryService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _LiquorCategoryService = liquorCategoryService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateLiquorCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateLiquorCategory(LiquorCategory liquorCategory)
        {
            _LiquorCategoryService.Add(liquorCategory);
            TempData["msg"] = "<script>alert('Liquor Category Added Succesfully');</script>";
            return RedirectToAction("ViewliquorCategory", "LiquorCategory");
        }
        [HttpGet]
        public IActionResult ViewLiquorCategory()
        {
            try
            {
                List<LiquorCategory> liquorCategory = new List<LiquorCategory>();

                var liquorCategoryData = _LiquorCategoryService.GetAll();
                foreach (var liquorCategorydata in liquorCategoryData)
                {
                    var liquorCategoryObj = _mapper.Map<LiquorCategory>(liquorCategorydata);
                    liquorCategory.Add(liquorCategoryObj);
                }
                if (liquorCategory != null)
                {
                    ViewBag.LiquorCategoryList = liquorCategory;
                }
                else
                {
                    TempData["msg"] = "<script>alert('Liquor Category Details Not Available');</script>";
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
        public IActionResult UpdateLiquorCategory(int liquorCatId)
        {
            var liquorCatData = _LiquorCategoryService.GetById(liquorCatId);
            return View(liquorCatData);
        }
        [HttpPost]
        public IActionResult UpdateLiquorCategory(int liquorCatId, LiquorCategory liquorCategory)
        {
            _LiquorCategoryService.Update(liquorCatId, liquorCategory);
            TempData["msg"] = "<script>alert('Liquor Category Updated Succesfully');</script>";
            return RedirectToAction("ViewLiquorCategory", "LiquorCategory");
        }
        public IActionResult DeleteLiquorCategory(int liquorCatId)
        {
            _LiquorCategoryService.Delete(liquorCatId);
            TempData["msg"] = "<script>alert('Liquor Category Deleted Succesfully');</script>";
            return RedirectToAction("ViewLiquorCategory", "LiquorCategory");
        }
    }
}
