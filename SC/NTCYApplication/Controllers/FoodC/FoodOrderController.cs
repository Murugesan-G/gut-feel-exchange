using NTCYApplication.Interfaces;
using NTCYApplication.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace NTCYApplication.Controllers.FoodC
{
    [Authorize(Roles = "Admin,RestaurantWaiter,Management")]
    public class FoodOrderController : Controller
    {
        FoodOrderInterface li = new FoodOrder();
        FoodOrderListInterface li1 = new FoodOrderList();
        KitchenOrderInterface IKitchenOrder = new KitchenOrder();
        private static ReaderWriterLock lockobj = new ReaderWriterLock();
        // GET: /FoorOrder/


        public ActionResult TakeFoodOrder()
        {
            try
            {
                string veg = "Veg";
                string nonVeg = "Non-Veg";
                string juice = "Juice";
                string iceCream = "IceCream/Desserts";
                List<Food> foodVegList;
                foodVegList=li.ViewAllFoodDetails(veg);
                if (foodVegList.Count>0)
                {
                    ViewBag.FoodVegList=foodVegList;
                }
                else
                {
                   // ViewBag.message="Food Details Not Available";
                }

                List<Food> foodNonVegList;
                foodNonVegList=li.ViewAllFoodDetails(nonVeg);
                if (foodNonVegList.Count>0)
                {
                    ViewBag.FoodNonVegList=foodNonVegList;
                }
                else
                {
                   // ViewBag.message="Food Details Not Available";
                }

                List<Food> foodJuiceList;
                foodJuiceList=li.ViewAllFoodDetails(juice);
                if (foodJuiceList.Count>0)
                {
                    ViewBag.FoodJuiceList=foodJuiceList;
                }
                else
                {
                   // ViewBag.message="Food Details Not Available";
                }

                List<Food> foodIceCreamList;
                foodIceCreamList=li.ViewAllFoodDetails(iceCream);
                if (foodIceCreamList.Count>0)
                {
                    ViewBag.FoodIceCreamList=foodIceCreamList;
                }
                else
                {
                   // ViewBag.message="Food Details Not Available";
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
        public ActionResult SelectMethod(string category)
        {
            string type = "";
            if (type == null || type == "")
            {
                type = category;
            }
            List<Food> foodList;
            foodList = li.ViewAllFoodDetails(type);
            if (foodList != null)
            {
                ViewBag.FoodList = foodList;
            }
            else
            {
                ViewBag.message = "Food Details Not Available";
            }
            return Json(new { foodList }, JsonRequestBehavior.AllowGet);
            //return View("TakeFoodOrder", ViewBag.FoodList);
        }
        [HttpGet]
        public ActionResult ViewAllFoodItem(string type)
        {
            try
            {
                List<Food> foodList;
                foodList=li.ViewAllFoodDetails(type);
                if (foodList!=null)
                {
                    ViewBag.FoodList=foodList;
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
            // Dictionary<string, object> LiquorDictionary = new Dictionary<string, object>();
         
            return View();
        }

         
        [HttpPost]
        public ActionResult TakeFoodOrders(FoodOrder FoodOrderData, FoodOrderList[] ExList)
        {
            try
            {
                lockobj.AcquireWriterLock(-1);
                string uname =User.Identity.Name;
                int SubOrderId = 0;
                if (ExList!=null)
                {
                    Session["ExList"]=ExList;
                    Dictionary<string, object> FoodOrderDictionary = new Dictionary<string, object>();
                    Dictionary<string, object> FoodOrderListDictionary = new Dictionary<string, object>();
                    FoodOrderDictionary.Add("UserName", uname);
                    FoodOrderDictionary.Add("MembershipNo", FoodOrderData.MembershipNo);
                    FoodOrderDictionary.Add("TotalGST", FoodOrderData.TotalGST);
                    FoodOrderDictionary.Add("Status", "UnPaid");
                    FoodOrderDictionary.Add("TableNo", FoodOrderData.TableNo);
                    FoodOrderDictionary.Add("TotalAmount", FoodOrderData.TotalAmount);
                    FoodOrderDictionary.Add("GrossAmount", FoodOrderData.GrossAmount);

                    int OrderId = li.CreateFoodOrderDetails(FoodOrderDictionary);
                    //FoodOrderDictionary.Add("OrderId", FoodOrderData.OrderId);

                    FoodOrderListDictionary.ContainsKey("FoodName");
                    FoodOrderListDictionary.ContainsKey("Quantity");
                    FoodOrderListDictionary.ContainsKey("Price");
                    FoodOrderListDictionary.ContainsKey("GST");
                    FoodOrderListDictionary.ContainsKey("OrderId");
                    FoodOrderListDictionary.ContainsKey("Status");

                    if (OrderId>0)
                    {
                        FoodOrderList[] List;
                        List=(FoodOrderList[])Session["ExList"];
                        if (List!=null)
                        {
                            if (List.Length>0&&List!=null)
                            {
                                for (int i = 0; i<List.Length; i++)
                                {
                                    FoodOrderListDictionary["FoodName"]=List[i].FoodName;
                                    FoodOrderListDictionary["Quantity"]=List[i].Quantity;
                                    FoodOrderListDictionary["Price"]=List[i].Price;
                                    FoodOrderListDictionary["GST"]=List[i].GST;
                                    FoodOrderListDictionary["OrderId"]=OrderId;
                                    FoodOrderListDictionary["Status"]="Processing";

                                    SubOrderId = li1.CreateFoodOrderlist(FoodOrderListDictionary);
                                    if (SubOrderId>0)
                                    {
                                        ViewBag.message="Food Ordered successfully";
                                    }
                                }
                            }
                        }
                    }
                }               
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            lockobj.ReleaseWriterLock();
            return RedirectToAction("TakeFoodOrder");
           // return View("TakeFoodOrder");

        }


        public JsonResult DisplayCompletedOrders() 
        {

            List<KitchenOrder> KitchenOrderlist=IKitchenOrder.DisplayCompletedOrders();
            return Json(KitchenOrderlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ServeFood(string[] OrderList)
        {
            //string MembershipNo = OrderList[0];
            int OrderNo = Convert.ToInt32(OrderList[0]);
            string Item = OrderList[1];
            string response = IKitchenOrder.ServeOrder(OrderNo, Item);
            return Json(JsonRequestBehavior.AllowGet);
        }


    }
}
