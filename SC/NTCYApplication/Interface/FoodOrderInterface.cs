using NTCYApplication.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
 public interface FoodOrderInterface 
    {
        int CreateFoodOrderDetails(Dictionary<string, object> FoodOrderDictionary);
        string UpdateFoodOrderDetails(Dictionary<string, object> FoodOrderDictionary);
        Dictionary<string, object> SelectFoodOrder(int OrderId);
        string DeleteFood(int OrderId);
        List<Food> ViewAllFoodDetails(string type);  
        string CompleteFoodOrder(Dictionary<string, object> FoodOrderIDictionary);
        void InsertIntoSubFoodOrderList(Dictionary<string, object> foodOrderDictionary);
    }
}
