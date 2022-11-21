using Microsoft.AspNetCore.Mvc;
using NTCY.Models.StockDetails;
using NTCY.Services.StockDetails;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NTCY.Controllers.StockDetails
{
    public class StockInwardDetailsController : Controller
    {
        [HttpGet]
        public IActionResult AddStockInward()
        {
            IStockService stockInwardService = new StockService();
            string sGRN = stockInwardService.GetGrnNo();
            string number = "";

            string no = sGRN;

            if (no.Length == 1) { number = ("000" + no); }
            else if (no.Length == 2) { number = ("00" + no); }
            else if (no.Length == 3) { number = ("0" + no); }
            else { number = ("" + no); }

            string currYear = DateTime.Now.ToString("yy");
            int nextYear = int.Parse(currYear.ToString()) + 1;
            ViewBag.finalGrnNo = "NTCY/" + currYear + "-" + nextYear + "/" + number;
            ViewBag.GrnDate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            ViewBag.Deliverydate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            ViewBag.PurchaseOrderDate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            return View();
        }
        [HttpPost]
        public ActionResult AddStockInward(StockInwardCommon model)
        {

            IStockService stockInwardService = new StockService();
            //int iGrnNo = stockInwardService.AddStockInwardDetails(StockInwardData);

            StockInwardSub[] lStockInwardSub;
            //lStockInwardSub = (StockInwardSub[]) ExList;

            //if(ExList!=null)
            //{
            //    if(ExList.Length>0)
            //    {
            //        Dictionary<string, object> StockInwardSubDictionary = new Dictionary<string, object>();
            //        for (int i=0; i<ExList.Length; i++)
            //        {
            //            StockInwardSubDictionary["GrnId"] = iGrnNo;
            //            StockInwardSubDictionary["LiquorId"] = lStockInwardSub[i].LiquorId;
            //            StockInwardSubDictionary["PurchaseOrderRate"] = lStockInwardSub[i].PurchaseOrderRate;
            //            StockInwardSubDictionary["PurchaseOrderQty"] = lStockInwardSub[i].PurchaseOrderQty;
            //            StockInwardSubDictionary["MRP"] = lStockInwardSub[i].MRP;
            //            StockInwardSubDictionary["TaxAmount"] = lStockInwardSub[i].TaxAmount;
            //            StockInwardSubDictionary["TaxPercentage"] = lStockInwardSub[i].TaxPercentage;
            //            StockInwardSubDictionary["DiscountAmount"] = lStockInwardSub[i].DiscountAmount;
            //            StockInwardSubDictionary["DiscountPercentage"] = lStockInwardSub[i].DiscountPercentage;
            //            StockInwardSubDictionary["RejectedQty"] = lStockInwardSub[i].RejectedQty;
            //            StockInwardSubDictionary["AcceptedQty"] = lStockInwardSub[i].AcceptedQty;
            //            if (lStockInwardSub[i].RejectedRemarks == null || lStockInwardSub[i].RejectedRemarks == "")
            //            {
            //                StockInwardSubDictionary["RejectedRemarks"] = "NA";
            //            }
            //            else
            //            {
            //                StockInwardSubDictionary["RejectedRemarks"] = lStockInwardSub[i].RejectedRemarks;
            //            }
            //        }
            //        stockInwardService.AddStockInwardSubDetails(StockInwardSubDictionary, iGrnNo);
            //    }
            //}            
            return RedirectToAction("AddStockInward");
            //return Json(ExList);
        }
    }
}
