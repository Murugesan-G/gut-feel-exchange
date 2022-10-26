using NTCY.Entities;
using NTCY.Models;
using NTCY.Models.Club;
using NTCY.Models.Users;

namespace NTCY.Services.Club
{
    public interface IClubCommitteeService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<ClubCommDTO> GetAll();
        ClubCommitteeDTO GetById(int comiteeId);
        void Add(ClubCommittee clubComitee);
        void Update(int comiteeId, ClubCommittee clubComitee);
        void Delete(int comiteeId);
        public List<ClubDetails> GetClubDetails();
    }
}
