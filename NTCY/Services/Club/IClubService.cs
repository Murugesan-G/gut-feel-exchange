using NTCY.Entities;
using NTCY.Models.Users;
using NTCY.Models.Club;
using NTCY.Models;

namespace NTCY.Services.Club
{
    public interface IClubService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<ClubDTO> GetAll();
        ClubDTO GetById(int clubId);
        void Add(Models.Club.Club club);
        void Update(int clubId, Models.Club.Club clubMaster);
        void Delete(int clubId);
    }
}
