using NTCY.Entities;
using NTCY.Models.Foods;
using NTCY.Models.Users;

namespace NTCY.Services.FoodService
{
    public interface IFoodService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<FoodDTO> GetAll();
        FoodDTO GetById(int foodId);
        void Add(Food food);
        void Update(int foodId, Food food);
        void Delete(int foodId);
    }
}
