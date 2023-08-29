using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCY_mvc4.Dictionaries
{
    public class StockAdjustmentDict
    {
        public Dictionary<string, object> StockAdjustmentDictionary { get; set; }
        DataSet ds = new DataSet();


        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            StockAdjustmentDictionary = new Dictionary<string, object>();
            StockAdjustmentDictionary.Add("StockId", ds.Tables[0].Rows[0]["StockId"].ToString());
            StockAdjustmentDictionary.Add("LiquorId", ds.Tables[0].Rows[0]["LiquorId"].ToString());
            StockAdjustmentDictionary.Add("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
            StockAdjustmentDictionary.Add("Date", ds.Tables[0].Rows[0]["Date"].ToString());
            StockAdjustmentDictionary.Add("Qty_Bottles", ds.Tables[0].Rows[0]["Qty_Bottles"].ToString());
            StockAdjustmentDictionary.Add("Qty_Pegs", ds.Tables[0].Rows[0]["Qty_Pegs"].ToString());
            StockAdjustmentDictionary.Add("PegAmount", ds.Tables[0].Rows[0]["PegAmount"].ToString());
            StockAdjustmentDictionary.Add("BottleAmount", ds.Tables[0].Rows[0]["BottleAmount"].ToString());
            StockAdjustmentDictionary.Add("LiquorName", ds.Tables[0].Rows[0]["LiquorName"].ToString());
            StockAdjustmentDictionary.Add("GrnNo", ds.Tables[0].Rows[0]["GrnNo"].ToString());
            StockAdjustmentDictionary.Add("CurrentStockBottles", ds.Tables[0].Rows[0]["CurrentStockBottles"].ToString());
            StockAdjustmentDictionary.Add("CurrentStockPegs", ds.Tables[0].Rows[0]["CurrentStockPegs"].ToString());
            StockAdjustmentDictionary.Add("Remarks", ds.Tables[0].Rows[0]["Remarks"].ToString());
            return StockAdjustmentDictionary;
        }
    }
}