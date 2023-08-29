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
    interface StockInwardInterface
    {
        int AddStockDetails(Dictionary<string, object> StockInwardIDictionary);
        Dictionary<string, object> SelectStock(int GrnId);
        string UpdateStockDetails(Dictionary<string, object> StockInwardIDictionary);
        List<StockInward> ViewAllStockDetails();
        //List<StockInward> ViewDetailStock(int GrnId);
        string GetGrnNo();
        int DeleteStockDetail(int GrnId);
        DataSet GetStockInwards(string Prefix,string LName);
    }
}
