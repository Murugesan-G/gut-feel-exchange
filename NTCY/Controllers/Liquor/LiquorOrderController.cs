using Microsoft.AspNetCore.Mvc;
using NTCY.Models.LiquorDetails;
using NTCY.Services.LiquorOrderS;

namespace NTCY.Controllers.Liquor
{
    public class LiquorOrderController : Controller
    {
        ILiquorOrderService los = new LiquorOrderService();

        [HttpGet]
        public ActionResult ViewProcessingOrders()
        {
            try
            {
                List<LiquorOrder> LiquorOrderList;
                LiquorOrderList = los.ViewAllLiquorOrder();
                if (LiquorOrderList != null)
                {
                    ViewBag.LiquorOrderList = LiquorOrderList;
                }
                else
                {
                    ViewBag.message = "Liquor Order Details Not Available";
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
                List<LiquorOrder> LiquorOrderList;
                LiquorOrderList = los.ViewCompletedOrders();
                if (LiquorOrderList != null)
                {
                    ViewBag.LiquorOrderList = LiquorOrderList;
                }
                else
                {
                    ViewBag.message = "Liquor order Details Not Available";
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
            return RedirectToAction("ViewCompletedOrder", "LiquorOrder");
        }
    }
}
