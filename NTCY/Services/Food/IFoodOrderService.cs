using NTCY.Models.Foods;

namespace NTCY.Services.FoodOrderS
{
    public interface IFoodOrderService
    {
        public List<Models.Foods.Food> ViewAllFoodDetails(string type);
        public List<Models.Foods.FoodOrder> ViewAllFoodOrder();
        public List<Models.Foods.FoodOrder> ViewCompletedOrders();
    }
}
