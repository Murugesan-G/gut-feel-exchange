using NTCYApplication.Models.Stocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    interface StockAdjustmentInterface
    {
        int AddStockAdjustmentDetails(Dictionary<string, object> StockAdjustmentIDictionary);
        Dictionary<string, object> SelectStockAdjustment(int StockId);
        string UpdateStockDetails(Dictionary<string, object> StockAdjustmentIDictionary);
        List<StockAdjustment> ViewAllStockAdjustmentDetails();
        DataSet GetLiquorStockDetails(int LiquorId);
    }
}
