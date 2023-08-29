using NTCYApplication.Interfaces;
using NTCYApplication.Models.Liquor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.LiqquorC
{
    [Authorize(Roles = "BarTender,Management,Admin")]
    public class LiquorAreaOrderController : Controller
    {
        ILiquorOrderArea li = new LiquorOrderArea();

        [HttpGet]
        public ActionResult ViewAllOrders() 
        {
            try
            {
                List<LiquorOrderArea> LiquorOrderlist;
                LiquorOrderlist=li.ViewAllLiquorOrders();
                if (LiquorOrderlist!=null)
                {
                    ViewBag.LiquorOrderlist=LiquorOrderlist;
                }
                else
                {
                    ViewBag.message="Liquor order Details Not Available";
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
                    string response = li.EditLiquorOrder(CompleteOrderDict);
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

        public ActionResult ViewCompletedLiquorOrder()
        {
            try
            {
                List<LiquorOrderArea> LiquorOrderlist;
                LiquorOrderlist=li.ViewCompletedOrders();
                if (LiquorOrderlist!=null)
                {
                    ViewBag.LiquorOrderlist=LiquorOrderlist;
                }
                else
                {
                    ViewBag.message="Liquor order Details Not Available";
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
