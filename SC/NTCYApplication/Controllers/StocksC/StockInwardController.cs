using NTCYApplication.Interfaces;
using NTCYApplication.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace NTCYApplication.Controllers
{
    [Authorize(Roles = "Admin,Management,StoreManager")]
    public class StockInwardController : Controller
    {

        StockInwardInterface IStockInward = new StockInward();
        StockInwardSubTableInterface IStockInwardSub = new StockInwardSubTable();
        //
        // GET: /StockInward/



        public ActionResult AddStockInward()
        {
            string query =IStockInward.GetGrnNo();

            string number="";

            string no = query;

            if (no.Length==1)
            {
                number=("000"+no);
            }
            else if (no.Length==2)
            {
                number=("00"+no);
            }
            else if (no.Length==3)
            {
                number=("0"+no);
            }          
            else
            {
                number=(""+no);
            }

            string currYear =DateTime.Now.ToString("yy");
            int nextYear = int.Parse(currYear.ToString())+1;
            ViewBag.finalGrnNo = "NTCY/"+currYear+"-"+nextYear+"/"+number;

            return View();
        }


        [HttpPost]
        public ActionResult AddStockInward(StockInward StockInwardData, StockInwardSubTable[] ExList) 
        {
            try
            {
                if (ExList!=null)
                {
                    Session["ExList"]=ExList;
                    Dictionary<string, object> StockInwardDictionary = new Dictionary<string, object>();
                    Dictionary<string, object> StockInwardSubTableIDictionary = new Dictionary<string, object>();
                    StockInwardDictionary.Add("GrnNo", StockInwardData.GrnNo);
                    StockInwardDictionary.Add("GrnDate", StockInwardData.GrnDate);
                    StockInwardDictionary.Add("PurchaseOrder", StockInwardData.PurchaseOrder);
                    StockInwardDictionary.Add("PurchaseOrderDate", StockInwardData.PurchaseOrderDate);
                    StockInwardDictionary.Add("DeliveryChallan", StockInwardData.DeliveryChallan);
                    StockInwardDictionary.Add("Deliverydate", StockInwardData.Deliverydate);
                    StockInwardDictionary.Add("Supplier", StockInwardData.Supplier);
                    StockInwardDictionary.Add("TotalAmount", StockInwardData.TotalAmount);
                    StockInwardDictionary.Add("TotalTax", StockInwardData.TotalTax);
                    StockInwardDictionary.Add("TotalDiscount", StockInwardData.TotalDiscount);
                    StockInwardDictionary.Add("NetAmount", StockInwardData.NetAmount);

                    int GrnId = IStockInward.AddStockDetails(StockInwardDictionary);
                    StockInwardSubTableIDictionary.ContainsKey("GrnId");
                    StockInwardSubTableIDictionary.ContainsKey("LiquorId");
                    StockInwardSubTableIDictionary.ContainsKey("ItemName");
                    StockInwardSubTableIDictionary.ContainsKey("PurchaseOrderRate");
                    StockInwardSubTableIDictionary.ContainsKey("PurchaseOrderQty");
                    StockInwardSubTableIDictionary.ContainsKey("MRP");
                    StockInwardSubTableIDictionary.ContainsKey("SupplierName");
                    StockInwardSubTableIDictionary.ContainsKey("TaxAmount");
                    StockInwardSubTableIDictionary.ContainsKey("TaxPercentage");
                    StockInwardSubTableIDictionary.ContainsKey("DiscountAmount");
                    StockInwardSubTableIDictionary.ContainsKey("DiscountPercentage");
                    StockInwardSubTableIDictionary.ContainsKey("RejectedQty");
                    StockInwardSubTableIDictionary.ContainsKey("AcceptedQty");

                    if (GrnId>0)
                    {
                        StockInwardSubTable[] List;
                        List=(StockInwardSubTable[])Session["ExList"];
                        if (List!=null)
                        {
                            if (List.Length>0&&List!=null)
                            {
                                for (int i = 0; i<List.Length; i++)
                                {
                                    StockInwardSubTableIDictionary["GrnId"]=GrnId;
                                    StockInwardSubTableIDictionary["LiquorId"]=List[i].LiquorId;
                                    StockInwardSubTableIDictionary["ItemName"]=List[i].ItemName;
                                    StockInwardSubTableIDictionary["PurchaseOrderRate"]=List[i].PurchaseOrderRate;
                                    StockInwardSubTableIDictionary["PurchaseOrderQty"]=List[i].PurchaseOrderQty;
                                    StockInwardSubTableIDictionary["MRP"]=List[i].MRP;
                                  //  StockInwardSubTableIDictionary["SupplierName"]=List[i].SupplierName;
                                    StockInwardSubTableIDictionary["TaxAmount"]=List[i].TaxAmount;
                                    StockInwardSubTableIDictionary["TaxPercentage"]=List[i].TaxPercentage;
                                    StockInwardSubTableIDictionary["DiscountAmount"]=List[i].DiscountAmount;
                                    StockInwardSubTableIDictionary["DiscountPercentage"]=List[i].DiscountPercentage;
                                    StockInwardSubTableIDictionary["RejectedQty"]=List[i].RejectedQty;
                                    int acceptedQty= List[i].PurchaseOrderQty -List[i].RejectedQty;
                                    StockInwardSubTableIDictionary["AcceptedQty"]=acceptedQty;
                                    if (List[i].RejectedRemarks==null||List[i].RejectedRemarks=="")
                                    {
                                        List[i].RejectedRemarks="NA";
                                        StockInwardSubTableIDictionary["RejectedRemarks"]=List[i].RejectedRemarks;
                                    }
                                    else
                                    {
                                        StockInwardSubTableIDictionary["RejectedRemarks"]=List[i].RejectedRemarks;
                                    }                                   
                                    IStockInwardSub.AddStockSubTableDetails(StockInwardSubTableIDictionary);
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
           

            return RedirectToAction("AddStockInward");
        }
       
        public ActionResult ViewAllStockInward()
        {
            try
            {
                List<StockInward> StockInwardList;
                StockInwardList=IStockInward.ViewAllStockDetails();
                if (StockInwardList!=null)
                {
                    ViewBag.StockInwardList=StockInwardList;
                }
                else
                {
                    ViewBag.message="Stock Details Not Available";
                }

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
           

            return View();
        }

        public JsonResult ViewDetailStock(int GrnId)
        {
            List<StockInwardSubTable> DetailStockList;
            DetailStockList = IStockInwardSub.ViewDetailStock(GrnId);
          
            return Json(DetailStockList,JsonRequestBehavior.AllowGet);
        }
         public ActionResult DeleteStockDetail(int GrnId)
        {
            try
            {
                int response = 0;
                response=IStockInward.DeleteStockDetail(GrnId);
              
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            
            return View();
        }

        [HttpPost]
        public JsonResult GetStockInward(string Prefix,string LName)
        {

            StockInwardInterface IStockInward = new StockInward();
            // Generate Member List
            List<StockInward> StockInwardList = new List<StockInward>();
            DataSet ds = IStockInward.GetStockInwards(Prefix,LName);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                StockInwardList.Add(new StockInward
                {

                    GrnNo = dr["GrnNo"].ToString(),

                });


            }
            return Json(StockInwardList, JsonRequestBehavior.AllowGet);
        }



    }
}
