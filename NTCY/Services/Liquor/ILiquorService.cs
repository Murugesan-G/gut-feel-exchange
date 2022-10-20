using NTCY.Entities;
using NTCY.Models;
using NTCY.Models.Foods;
using NTCY.Models.LiquorDetails;
using NTCY.Models.Users;

namespace NTCY.Services.LiquorDetails
{
    public interface ILiquorService
    {
        public List<LiquorCategoryDet> GetLiquorCategories();
        public IEnumerable<LiquorDet> GetAll();
        public LiquorDet GetById(int liquorId);
        public void Update(int liquorId, LiquorDet liquor);
        public void Delete(int liquorId);
    }
}
