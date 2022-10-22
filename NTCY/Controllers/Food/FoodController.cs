using Microsoft.AspNetCore.Mvc;
using NTCY.Services.FoodService;
using NTCY.Models.Foods;
using Microsoft.Extensions.Options;
using AutoMapper;
using NTCY.Utils;
using NTCY.Services.Table;
using NTCY.Models.Table;
using NTCY.Entities;

namespace NTCY.Controllers.Foods
{
    public class FoodController : Controller
    {
        private IFoodService _foodService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public FoodController(IFoodService foodService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _foodService = foodService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateFood()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateFood(Food food)
        {
            _foodService.Add(food);
            TempData["msg"] = "<script>alert('Food Added Succesfully');</script>";
            return RedirectToAction("ViewFood", "Food");
        }
        [HttpGet]
        public IActionResult ViewFood()
        {
            try
            {
                List<FoodOrder> food = new List<FoodOrder>();

                var foodData = _foodService.GetAll();
                foreach (var fooddata in foodData)
                {
                    var foodObj = _mapper.Map<FoodOrder>(fooddata);
                    food.Add(foodObj);
                }
                if (food != null)
                {
                    ViewBag.FoodList = food;
                }
                else
                {
                    TempData["msg"] = "<script>alert('Food Details Not Available');</script>";
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
        public IActionResult UpdateFood(int foodId)
        {
            var foodData = _foodService.GetById(foodId);
            return View(foodData);
        }
        [HttpPost]
        public IActionResult UpdateFood(int foodId,Food food)
        {
            _foodService.Update(foodId, food);
            TempData["msg"] = "<script>alert('Food Updated Succesfully');</script>";
            return RedirectToAction("ViewFood", "Food");
        }
        public IActionResult DeleteFood(int foodId)
        {
            _foodService.Delete(foodId);
            TempData["msg"] = "<script>alert('Food Deleted Succesfully');</script>";
            return RedirectToAction("ViewFood", "Food");
        }
    }
}
