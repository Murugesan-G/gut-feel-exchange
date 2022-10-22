using Microsoft.AspNetCore.Mvc;
using NTCY.Models.Foods;
using NTCY.Services.FoodOrderS;

namespace NTCY.Controllers.FoodOrderC
{
    public class FoodOrderController : Controller
    {
        IFoodOrderService fos = new FoodOrderService();
        public IActionResult FoodOrder()
        {
            List<NTCY.Models.Foods.Food> foodsList = new List<NTCY.Models.Foods.Food>();
            
            foodsList = fos.ViewAllFoodDetails("Veg");
            if(foodsList.Count>0)
            {
                ViewBag.VegFoodList = foodsList;
            }

            foodsList = fos.ViewAllFoodDetails("Non-Veg");
            if (foodsList.Count > 0)
            {
                ViewBag.NonVegFoodList = foodsList;
            }

            foodsList = fos.ViewAllFoodDetails("Juice");
            if (foodsList.Count > 0)
            {
                ViewBag.JuiceFoodList = foodsList;
            }

            return View();
        }

        [HttpGet]
        public ActionResult ViewProcessingOrders()
        {
            try
            {
                List<FoodOrder> FoodOrderList;
                FoodOrderList = fos.ViewAllFoodOrder();
                if (FoodOrderList != null)
                {
                    ViewBag.FoodOrderList = FoodOrderList;
                }
                else
                {
                    ViewBag.message = "Food Order Details Not Available";
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
        public ActionResult ViewCompletedOrder()
        {
            try
            {
                List<FoodOrder> FoodOrderList;
                FoodOrderList = fos.ViewCompletedOrders();
                if (FoodOrderList != null)
                {
                    ViewBag.FoodOrderList = FoodOrderList;
                }
                else
                {
                    ViewBag.message = "Food order Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ServedOrder()
        {
            return RedirectToAction("ViewCompletedOrder", "FoodOrder");
        }
    }
}
