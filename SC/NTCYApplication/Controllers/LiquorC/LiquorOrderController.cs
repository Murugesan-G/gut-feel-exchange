using NTCYApplication.Interfaces;
using NTCYApplication.Models.Liquor;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.LiquorC
{
    [Authorize(Roles = "BarWaiter, Admin,Management")]
    public class LiquorOrderController : Controller
    {
        LiquorOrderInterface li = new LiquorOrder();
        LiquorOrderListInterface li1 = new LiquorOrderList();
        Dictionary<string, List<Liquor>> LiquorDetails = new Dictionary<string, List<Liquor>>();
        private static ReaderWriterLock lockobj = new ReaderWriterLock();

        [HttpGet]
        public ActionResult TakeLiquorOrder()
        {
            try
            {
                //    List<Liquor> LiquorList;
                //    LiquorList=li.ViewAllLiquorDetails();
                //    if (LiquorList!=null)
                //    {
                //        ViewBag.LiquorList=LiquorList;
                //    }
                //    else
                //    {
                //        ViewBag.message="Liquor Details Not Available";
                //    }
                //}
                //catch (Exception e)
                //{
                //    ViewBag.message=e.Message;
                //    ViewBag.innerEx=e.InnerException.Message;
                //}

                //return View();

                LiquorDetails = li.ViewLiquorDetails();
            List<Liquor> ListLiquor = new List<Liquor>();
            List<Liquor> ListLiquorM = new List<Liquor>();

                ListLiquor = LiquorDetails["ListLiquor"];
                ListLiquorM = LiquorDetails["ListLiquorM"];
          

            if (ListLiquor == null || ListLiquor.Count == 0)
            {
                ViewBag.message = "Liquor Details Not Available";

            }
            else
            {
                ViewBag.ListLiquor = ListLiquor;
               // ViewBag.OrderId = FoodList[0].OrderId;
            }
            if (ListLiquorM == null || ListLiquorM.Count == 0)
            {
                ViewBag.message1 = "Details Not Available";
            }
            else
            {
                ViewBag.ListLiquorM = ListLiquorM;
               // ViewBag.OrderId1 = ListLiquorM[0].OrderId;
            }
           
        }
            catch (Exception e)
            {
               
                ViewBag.message=e.Message;
                //ViewBag.innerEx=e.InnerException.Message;
            }
          

            return View();
}

        [HttpPost]
        public ActionResult TakeLiquorOrder(LiquorOrder LiquorOrderData, LiquorOrderList[] ExList)
        {
            try
            {
                lockobj.AcquireWriterLock(-1);
                if (ExList!=null)
                {
                    string uname = User.Identity.Name;
                    Session["ExList"]=ExList;
                    Dictionary<string, object> LiquorOrderDictionary = new Dictionary<string, object>();
                    Dictionary<string, object> LiquorOrderListDictionary = new Dictionary<string, object>();
                    LiquorOrderDictionary.Add("UserName", uname);
                    LiquorOrderDictionary.Add("MembershipNo", LiquorOrderData.MembershipNo);
                    LiquorOrderDictionary.Add("TotalGST", LiquorOrderData.TotalGST);
                    LiquorOrderDictionary.Add("Status", "UnPaid");
                    LiquorOrderDictionary.Add("TableNo", LiquorOrderData.TableNo);
                    LiquorOrderDictionary.Add("WaiterName", LiquorOrderData.WaiterName);
                    LiquorOrderDictionary.Add("TotalAmount", LiquorOrderData.TotalAmount);
                    LiquorOrderDictionary.Add("GrossAmount", LiquorOrderData.GrossAmount);
                    int OrderId = li.CreateLiquorOrderDetails(LiquorOrderDictionary);


                    LiquorOrderListDictionary.ContainsKey("OrderId");
                    LiquorOrderListDictionary.ContainsKey("LiquorName");
                    LiquorOrderListDictionary.ContainsKey("Quantity");
                    LiquorOrderListDictionary.ContainsKey("Price");
                    LiquorOrderListDictionary.ContainsKey("GST");
                    LiquorOrderListDictionary.ContainsKey("Type");
                    LiquorOrderListDictionary.ContainsKey("LiquorId");
                    if (OrderId>0)
                    {
                        LiquorOrderList[] List;
                        List=(LiquorOrderList[])Session["ExList"];
                        if (List!=null)
                        {
                            if (List.Length>0&&List!=null)
                            {
                                for (int i = 0; i<List.Length; i++)
                                {
                                    LiquorOrderListDictionary["LiquorName"]=List[i].LiquorName;
                                    LiquorOrderListDictionary["Quantity"]=List[i].Quantity;
                                    LiquorOrderListDictionary["Price"]=List[i].Price;
                                    LiquorOrderListDictionary["GST"]=List[i].GST;
                                    LiquorOrderListDictionary["OrderId"]=OrderId;
                                    LiquorOrderListDictionary["Type"]=List[i].Type;
                                    LiquorOrderListDictionary["LiquorId"]=List[i].LiquorId;
                                    int SubOrderId = li1.CreateLiquorOrderList(LiquorOrderListDictionary);
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
            LiquorDetails = li.ViewLiquorDetails();
            List<Liquor> ListLiquor = new List<Liquor>();
            List<Liquor> ListLiquorM = new List<Liquor>();

            ListLiquor = LiquorDetails["ListLiquor"];
            ListLiquorM = LiquorDetails["ListLiquorM"];


            if (ListLiquor == null || ListLiquor.Count == 0)
            {
                ViewBag.message = "Liquor Details Not Available";

            }
            else
            {
                ViewBag.ListLiquor = ListLiquor;
                // ViewBag.OrderId = FoodList[0].OrderId;
            }
            if (ListLiquorM == null || ListLiquorM.Count == 0)
            {
                ViewBag.message1 = "Details Not Available";
            }
            else
            {
                ViewBag.ListLiquorM = ListLiquorM;
                // ViewBag.OrderId1 = ListLiquorM[0].OrderId;
            }

        
            
             return View();

        }
        public JsonResult DisplayCompletedOrders()
        {

            List<LiquorOrder> KitchenOrderlist = li.DisplayCompletedOrders();
            return Json(KitchenOrderlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LiqourServedStatus(int SubOrderId, string MembershipNo)
        {
            int suborderId = SubOrderId;
            //string membershipno = MembershipNo;
            Dictionary<string, object> LiquorServedDictionary = new Dictionary<string, object>();
            LiquorServedDictionary.Add("SubOrderId", suborderId);
           // LiquorServedDictionary.Add("MembershipNo", membershipno);

            int response = li.ServedStatus(LiquorServedDictionary);
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}