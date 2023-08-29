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
    public interface FoodInterface
    {
        string CreateFoodDetails(Dictionary<string, object> FoodIDictionary);
        string UpdateFoodDetails(Dictionary<string, object> FoodIDictionary);
        Dictionary<string, object> SelectFood(int FoodId);
        string DeleteFood(int FoodId);
        List<Food> ViewAllFoodDetails();
    }
}