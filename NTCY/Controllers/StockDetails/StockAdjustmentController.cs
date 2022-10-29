using Microsoft.AspNetCore.Mvc;
using NTCY.Models.StockDetails;
using Microsoft.AspNetCore.Mvc.Abstractions;
using NTCY.Services.StockDetails;
using System.Data;
using NTCY.Services.LiquorDetails;

namespace NTCY.Controllers.StockDetails
{
    public class StockAdjustmentController : Controller
    {
        [HttpGet]
        public IActionResult AddStockAdjustment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStockAdjustment(StockAdjustment[] ExList)
        {
            IStockService stockAdjustmentService = new StockService();
            Dictionary<string, object> StockAdjustmentDictionary = new Dictionary<string, object>();

            StockAdjustment[] stockAdjustment;
            stockAdjustment = (StockAdjustment[]) ExList;

            if (ExList != null)
            {
                if (ExList.Length > 0)
                {                    
                    for (int i = 0; i < ExList.Length; i++)
                    {
                        StockAdjustmentDictionary["LiquorId"] = stockAdjustment[i].LiquorId;
                        StockAdjustmentDictionary["@LiquorName"] = stockAdjustment[i].@LiquorName;
                        StockAdjustmentDictionary["UserId"] = stockAdjustment[i].UserId;
                        StockAdjustmentDictionary["GrnNo"] = stockAdjustment[i].GrnNo;
                        StockAdjustmentDictionary["Date"] = stockAdjustment[i].Date;
                        StockAdjustmentDictionary["CurrentStockBottles"] = stockAdjustment[i].CurrentStockBottles;
                        StockAdjustmentDictionary["CurrentStockPegs"] = stockAdjustment[i].CurrentStockPegs;
                        StockAdjustmentDictionary["Qty_Bottles"] = stockAdjustment[i].Qty_Bottles;
                        StockAdjustmentDictionary["Qty_Pegs"] = stockAdjustment[i].Qty_Pegs;
                        StockAdjustmentDictionary["Add_Sub"] = stockAdjustment[i].Add_Sub;
                        StockAdjustmentDictionary["PegAmount"] = stockAdjustment[i].PegAmount;
                        StockAdjustmentDictionary["BottleAmount"] = stockAdjustment[i].BottleAmount;
                        StockAdjustmentDictionary["Remarks"] = stockAdjustment[i].Remarks;
                    }
                    stockAdjustmentService.AddStockAdjustmentDetails(StockAdjustmentDictionary);
                }
            }

            return RedirectToAction("AddStockAdjustment");
        }

        public JsonResult GetLiquors(string Prefix)
        {
            ILiquorService liquorService = new LiquorService();
            List<NTCY.Models.LiquorDet> liquorlist = new List<NTCY.Models.LiquorDet>();
            DataSet ds = liquorService.GetByName(Prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                liquorlist.Add(new NTCY.Models.LiquorDet
                {
                    LiquorId = Convert.ToInt32(dr["LiquorId"]),
                    LiquorName = dr["LiquorName"].ToString()
                });
            }
            return Json(liquorlist);
        }
    }
}
