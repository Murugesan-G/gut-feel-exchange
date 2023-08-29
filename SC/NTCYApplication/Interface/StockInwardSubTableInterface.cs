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
    interface StockInwardSubTableInterface
    {
        string AddStockSubTableDetails(Dictionary<string, object> StockInwardSubTableIDictionary);
        Dictionary<string, object> SelectStockSubTable(int StockSubId);
        string UpdateStockSubDetails(Dictionary<string, object> StockInwardIDictionary);
        List<StockInwardSubTable> ViewAllStockSubTableDetails();
        List<StockInwardSubTable> ViewDetailStock(int GrnId);
    }
}
