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
   public interface FoodOrderListInterface
    {
        int CreateFoodOrderlist(Dictionary<string, object> FoodOrderListDictionary);
        List<FoodOrderList> ViewAllFoodOrderDetails();
    }
}
