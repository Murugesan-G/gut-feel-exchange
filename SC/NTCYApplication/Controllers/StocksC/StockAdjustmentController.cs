using NTCYApplication.Interfaces;
using NTCYApplication.Models;
using NTCYApplication.Models.Liquor;
using NTCYApplication.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
namespace NTCYApplication.Controllers
{
    [Authorize(Roles = "Admin,Management,StoreManager")]
    public class StockAdjustmentController : Controller
    {
        StockAdjustmentInterface li = new StockAdjustment();
        //StockInwardInterface li1 = new StockInward();
        //
        // GET: /StockAdjustment/

        public ActionResult AddStockAdjustment()
        {
            return View();
        }

        public ActionResult InsertStockAdjustment(StockAdjustment[] Exlist)
        {
            if (Exlist != null)
            {
                Session["Exlist"] = Exlist;
                //Dictionary<string, object> StockInwardDictionary = new Dictionary<string, object>();
                Dictionary<string, object> StockAdjustmentDictionary = new Dictionary<string, object>();
                //StockInwardDictionary.Add("GrnNo", StockInwardData.GrnNo);
                //StockInwardDictionary.Add("GrnDate", StockInwardData.GrnDate);
                //StockInwardDictionary.Add("Remarks", StockInwardData.Remarks);

                //int GrnId = li1.AddStockDetailsOfStockAdjustment(StockInwardDictionary);

                //StockAdjustmentDictionary.ContainsKey("LiquorName");
                //StockAdjustmentDictionary.ContainsKey("Qty_Bottles");
                //StockAdjustmentDictionary.ContainsKey("Qty_Pegs");
                //StockAdjustmentDictionary.ContainsKey("BottleAmount");
                //StockAdjustmentDictionary.ContainsKey("PegAmount");

                //if (StockId > 0)
                //{
                StockAdjustment[] List;
                List = (StockAdjustment[])Session["ExList"];
                if (List != null)
                {
                    if (List.Length > 0 && List != null)
                    {
                        for (int i = 0; i < List.Length; i++)
                        {
                            StockAdjustmentDictionary["LiquorId"] = List[i].LiquorId;
                            StockAdjustmentDictionary["UserId"] = List[i].UserId;
                            StockAdjustmentDictionary["Date"] = List[i].Date;
                            StockAdjustmentDictionary["LiquorName"] = List[i].LiquorName;
                            StockAdjustmentDictionary["Qty_Bottles"] = List[i].Qty_Bottles;
                            StockAdjustmentDictionary["Qty_Pegs"] = List[i].Qty_Pegs;
                            StockAdjustmentDictionary["BottleAmount"] = List[i].BottleAmount;
                            StockAdjustmentDictionary["PegAmount"] = List[i].PegAmount;
                            StockAdjustmentDictionary["GrnNo"] = List[i].GrnNo;
                            StockAdjustmentDictionary["CurrentStockBottles"] = List[i].CurrentStockBottles;
                            StockAdjustmentDictionary["CurrentStockPegs"] = List[i].CurrentStockPegs;
                            StockAdjustmentDictionary["Flag"] = List[i].Flag;
                            if(List[i].Remarks==null ||List[i].Remarks=="")
                            {
                                StockAdjustmentDictionary["Remarks"]="NA";
                            }
                            else
                            {
                                StockAdjustmentDictionary["Remarks"]=List[i].Remarks;
                            }                          

                            int StockId = li.AddStockAdjustmentDetails(StockAdjustmentDictionary);
                        }
                    }
                }
            }
            //}
            return RedirectToAction("AddStockAdjustment");
        }

        //public ActionResult GetStockAdjustment()
        //{
        //    List<StockAdjustment> StockAdjustmentList;
        //    StockAdjustmentList = li.ViewAllStockAdjustmentDetails();
        //    if (StockAdjustmentList != null)
        //    {
        //        ViewBag.StockAdjustmentList = StockAdjustmentList;
        //    }
        //    else
        //    {
        //        ViewBag.message = "Stock Adjustment Details Not Avaiable";
        //    }

        //    return RedirectToAction("AddStockAdjustment");
        //}

        public JsonResult GetLiquors(string Prefix)
        {

            LiquorInterface iLiquor = new Liquor();
            // Generate Member List
            List<Liquor> liquorlist = new List<Liquor>();
            DataSet ds = iLiquor.GetLiquors(Prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                liquorlist.Add(new Liquor
                {

                    LiquorId = Convert.ToInt32(dr["LiquorId"]),
                    LiquorName = dr["LiquorName"].ToString()
                });


            }
            return Json(liquorlist, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ViewAllStockAdjustment()
        {
            List<StockAdjustment> StockAdjustmentList;
            StockAdjustmentList = li.ViewAllStockAdjustmentDetails();
            if (StockAdjustmentList != null)
            {
                ViewBag.StockAdjustmentList = StockAdjustmentList;
            }
            else
            {
                ViewBag.message = "Stock Details Not Available";
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetLiquorsDetails(int LiquorId)
        {
            StockAdjustmentInterface iStock = new StockAdjustment();

            // Generate Member List
            List<StockAdjustment> ListStockAdj = new List<StockAdjustment>();

            DataSet ds = iStock.GetLiquorStockDetails(LiquorId);
            //  ListStockAdj = iStock.GetLiquorStockDetails(LiquorId);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                ListStockAdj.Add(new StockAdjustment
                {

                    LiquorName = dr["LiquorName"].ToString(),
                    CurrentStockBottles = float.Parse(dr["CurrentStockBottles"].ToString()),
                    CurrentStockPegs = float.Parse(dr["CurrentStockPegs"].ToString()),
                    SellingPricePerBottle = Convert.ToDouble(dr["SellingPricePerBottle"]),
                    SellingPricePerPeg = Convert.ToDouble(dr["SellingPricePerPeg"])
                });

            }
            return Json(new { ListStockAdj });

        }

    }
}
