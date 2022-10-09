using NTCY.Entities;
using NTCY.Models.Foods;
using NTCY.Models.RoomDetails;
using NTCY.Models.Users;

namespace NTCY.Services.RoomDetail
{
    public interface IRoomDetails
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<RoomDTO> GetAll();
        RoomDTO GetById(int roomId);
        void Add(Room room);
        void Update(int roomId, Room room);
        void Delete(int roomId);
    }
}
