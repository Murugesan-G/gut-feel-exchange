using NTCYApplication.Models.Liquor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTCYApplication.Interfaces
{
    public interface LiquorOrderInterface
    {
        int CreateLiquorOrderDetails(Dictionary<string, object> LiquorOrderDictionary);
        string TakeOrder(Dictionary<string, object> FoodIDictionary);
        string UpdateLiquorOrderDetails(Dictionary<string, object> FoodIDictionary);
        Dictionary<string, object> SelectLiquorOrder(int FoodId);
        string DeleteLiquorOrder(int FoodId);
        List<LiquorOrder> TakeLiquorOrder();
        Dictionary<string, List<Liquor>> ViewLiquorDetails();
        List<LiquorOrder> DisplayCompletedOrders();

        int ServedStatus(Dictionary<string, object> LiquorServedDictionary); 

    }
}