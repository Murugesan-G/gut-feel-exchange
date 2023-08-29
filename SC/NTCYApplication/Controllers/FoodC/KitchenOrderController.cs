using NTCYApplication.Interfaces;
using NTCYApplication.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.FoodC
{
    [Authorize(Roles = "Admin,Chef,Management")]
    public class KitchenOrderController : Controller
    {
        //
        // GET: /KitchenOrder/
        KitchenOrderInterface li = new KitchenOrder();

        [HttpGet]
        public ActionResult ViewAllOrders()
        {
            try
            {
                List<KitchenOrder> KitchenList;
                KitchenList=li.ViewAllKitchenOrder();
                if (KitchenList!=null)
                {
                    ViewBag.KitchenList=KitchenList;
                }
                else
                {
                    ViewBag.message="KitchenOrder Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            return View();
        }

        [HttpPost]
        public ActionResult CompleteOrder(int SubOrderId)
        {
            try
            {
                Dictionary<string, object> CompleteOrderDict = new Dictionary<string, object>();

                if (SubOrderId>0)
                {
                    //CompleteOrderDict.Add("OrderNumber", OrderNumber);
                    //CompleteOrderDict.Add("MembershipId", MembershipId);
                    //CompleteOrderDict.Add("TableNo", TableNo);
                    //CompleteOrderDict.Add("Items", Items);
                    // CompleteOrderDict.Add("Qty", Qty);
                    CompleteOrderDict.Add("SubOrderId", SubOrderId);
                    //   CompleteOrderDict.Add("UserId", 12);
                    string response = li.EditKitchenOrder(CompleteOrderDict);
                    if (response=="1")
                    {
                        ViewBag.Status="Completed";
                            
                    }
                }

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            return RedirectToAction("ViewAllOrders");
        }


        public ActionResult ViewCompletedKitchenOrder()
        {
            try
            {
                List<KitchenOrder> KitchenOrderlist;
                KitchenOrderlist=li.ViewCompletedOrders();
                if (KitchenOrderlist!=null)
                {
                    ViewBag.KitchenOrderlist=KitchenOrderlist;
                }
                else
                {
                    ViewBag.message="Kitchen order Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            return View();
        }



    }
}
