using NTCYApplication.Interfaces;
using NTCYApplication.Models.Food;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace NTCYApplication.Controllers.FoodC
{
    [Authorize(Roles = "Admin,Management")]
    public class FoodOrderListController : Controller
    {
        FoodOrderListInterface li = new FoodOrderList();

        //
        // GET: /SubFoodOrderList/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewAllFoodOrderList()
        {
            try
            {
                List<FoodOrderList> FoodOrderList;
                FoodOrderList=li.ViewAllFoodOrderDetails();
                if (FoodOrderList!=null)
                {
                    ViewBag.SubOrderList=FoodOrderList;
                }
                else
                {
                    ViewBag.message="Subscriptions Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            //  Dictionary<string, object> SubFoodOrderDictionary = new Dictionary<string, object>();

            return View();
        }

    }
}
