using NTCYApplication.Models.Food;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NTCYApplication.Interfaces
{
    public interface KitchenOrderInterface
    {
        List<KitchenOrder> ViewAllKitchenOrder();
        string EditKitchenOrder(Dictionary<string, object> FoodDictionary);
        List<KitchenOrder> ViewCompletedOrders();
        List<KitchenOrder> DisplayCompletedOrders();
        
        string ServeOrder(int OrderNo,string Item);

    }
}
