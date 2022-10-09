using AutoMapper;
using NTCY.Database;
using NTCY.Entities;
using NTCY.Exceptions;
using NTCY.Models.Foods;
using NTCY.Models.RoomDetails;
using NTCY.Models.Users;
using NTCY.Utils;

namespace NTCY.Services.RoomDetail
{
    public class RoomDetails : IRoomDetails
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public RoomDetails(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public void Add(Room room)
        {
            if (_context.RoomDetails.Any(x => x.RoomName == room.RoomName))
                throw new AppException("Room '" + room.RoomName + "' is already exists");

            var rooomDTO = _mapper.Map<RoomDTO>(room);

            _context.RoomDetails.Add(rooomDTO);
            _context.SaveChanges();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public void Delete(int roomId)
        {
            var room = getRoom(roomId);
            _context.RoomDetails.Remove(room);
            _context.SaveChanges();
        }

        public IEnumerable<RoomDTO> GetAll()
        {
            return _context.RoomDetails;
        }

        public RoomDTO GetById(int roomId)
        {
            return getRoom(roomId);
        }

        public void Update(int roomId, Room room)
        {
            var roomdto = getRoom(roomId);

            if (room.RoomName != room.RoomName && _context.RoomDetails.Any(x => x.RoomName == room.RoomName))
                throw new AppException("Room '" + room.RoomName + "' is already exists");

            _mapper.Map(room, roomdto);
            _context.RoomDetails.Update(roomdto);
            _context.SaveChanges();
        }
        private RoomDTO getRoom(int roomId)
        {
            var room = _context.RoomDetails.FirstOrDefault(m => m.RoomId == roomId);
            if (room == null) throw new KeyNotFoundException("Room Details not found");
            return room;
        }
    }
}
