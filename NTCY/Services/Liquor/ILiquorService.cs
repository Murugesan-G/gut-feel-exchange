using NTCY.Entities;
using NTCY.Models.Foods;
using NTCY.Models.LiquorDetails;
using NTCY.Models.Users;

namespace NTCY.Services.LiquorDetails
{
    public interface ILiquorService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<LiquorDTO> GetAll();
        LiquorDTO GetById(int liquorId);
        void Add(Liquor liquor);
        void Update(int liquorId, Liquor liquor);
        void Delete(int liquorId);
    }
}
