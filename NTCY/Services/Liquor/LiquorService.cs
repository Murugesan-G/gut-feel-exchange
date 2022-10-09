using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NTCY.Database;
using NTCY.Entities;
using NTCY.Exceptions;
using NTCY.Models.LiquorDetails;
using NTCY.Models.Users;
using NTCY.Utils;

namespace NTCY.Services.LiquorDetails
{
    public class LiquorService : ILiquorService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        public LiquorService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public void Add(Liquor liquor)
        {
            if (_context.Liquor.Any(x => x.LiquorName == liquor.LiquorName))
                throw new AppException("Liquor '" + liquor.LiquorName + "' is already exists");

            var liquorDTO = _mapper.Map<LiquorDTO>(liquor);

            _context.Liquor.Add(liquorDTO);
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

        public void Delete(int liquorId)
        {
            var liquor = getLiquor(liquorId);
            _context.Liquor.Remove(liquor);
            _context.SaveChanges();
        }

        public IEnumerable<LiquorDTO> GetAll()
        {
            return _context.Liquor;
        }

        public LiquorDTO GetById(int liquorId)
        {
            return getLiquor(liquorId);
        }

        public void Update(int liquorId, Liquor liquor)
        {
            var liqdto = getLiquor(liquorId);

            if (liquor.LiquorName != liquor.LiquorName && _context.Liquor.Any(x => x.LiquorName == liquor.LiquorName))
                throw new AppException("Liquor Name '" + liquor.LiquorName + "' is already exists");

            _mapper.Map(liquor, liqdto);
            _context.Liquor.Update(liqdto);
            _context.SaveChanges();
        }

        private LiquorDTO getLiquor(int liquorId)
        {
            var liquour = _context.Liquor.FirstOrDefault(m => m.LiquorId == liquorId);
            if (liquour == null) throw new KeyNotFoundException("Liquor Details not found");
            return liquour;
        }
    }
}
