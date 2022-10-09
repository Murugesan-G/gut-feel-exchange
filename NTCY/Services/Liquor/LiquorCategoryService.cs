using AutoMapper;
using NTCY.Database;
using NTCY.Entities;
using NTCY.Exceptions;
using NTCY.Models.LiquorDetails;
using NTCY.Models.Users;
using NTCY.Utils;

namespace NTCY.Services.LiquorDetails
{
    public class LiquorCategoryService : ILiquorCategoryService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        public LiquorCategoryService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public void Add(LiquorCategory liquorCategory)
        {
            if (_context.LiquorCategory.Any(x => x.CategoryName == liquorCategory.CategoryName))
                throw new AppException("LiquorCategory '" + liquorCategory.CategoryName + "' is already exists");

            var liquorDTO = _mapper.Map<LiquorCategoryDTO>(liquorCategory);

            _context.LiquorCategory.Add(liquorDTO);
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

        public void Delete(int liquorCategoryId)
        {
            var liqCatg = getLiquorCategory(liquorCategoryId);
            _context.LiquorCategory.Remove(liqCatg);
            _context.SaveChanges();
        }

        public IEnumerable<LiquorCategoryDTO> GetAll()
        {
            return _context.LiquorCategory;
        }

        public LiquorCategoryDTO GetById(int liquorCategoryId)
        {
            return getLiquorCategory(liquorCategoryId);
        }

        public void Update(int liquorCategoryId, LiquorCategory liquorCategory)
        {
            var liqCatgdto = getLiquorCategory(liquorCategoryId);

            if (liquorCategory.CategoryName != liquorCategory.CategoryName && _context.LiquorCategory.Any(x => x.CategoryName == liquorCategory.CategoryName))
                throw new AppException("Liquor Category Name '" + liquorCategory.CategoryName + "' is already exists");

            _mapper.Map(liquorCategory, liqCatgdto);
            _context.LiquorCategory.Update(liqCatgdto);
            _context.SaveChanges();
        }
        private LiquorCategoryDTO getLiquorCategory(int liquorCategoryId)
        {
            var liqcatg = _context.LiquorCategory.FirstOrDefault(m => m.LiquorCatId == liquorCategoryId);
            if (liqcatg == null) throw new KeyNotFoundException("Liquor Category Details not found");
            return liqcatg;
        }
    }
}
