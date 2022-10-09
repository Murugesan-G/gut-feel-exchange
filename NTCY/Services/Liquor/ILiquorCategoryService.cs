using NTCY.Entities;
using NTCY.Models.Foods;
using NTCY.Models.LiquorDetails;
using NTCY.Models.Users;

namespace NTCY.Services.LiquorDetails
{
    public interface ILiquorCategoryService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<LiquorCategoryDTO> GetAll();
        LiquorCategoryDTO GetById(int liquorCategoryId);
        void Add(LiquorCategory liquorCategory);
        void Update(int liquorCategoryId, LiquorCategory liquorCategory);
        void Delete(int liquorCategoryId);
    }
}
