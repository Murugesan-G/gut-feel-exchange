using AutoMapper;
using NTCY.Database;
using NTCY.Entities;
using NTCY.Exceptions;
using NTCY.Models.Foods;
using NTCY.Models.Users;
using NTCY.Utils;

namespace NTCY.Services.FoodService
{
    public class FoodService : IFoodService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public FoodService(DataContext context,IJwtUtils jwtUtils,IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public void Add(Food food)
        {
            if (_context.Foods.Any(x => x.FoodCode == food.FoodCode))
                throw new AppException("Food '" + food.FoodCode + "' is already exists");

            var tableDTO = _mapper.Map<FoodDTO>(food);

            _context.Foods.Add(tableDTO);
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

        public void Delete(int foodId)
        {
            var food = getFood(foodId);
            _context.Foods.Remove(food);
            _context.SaveChanges();
        }

        public IEnumerable<FoodDTO> GetAll()
        {
            return _context.Foods;
        }

        public FoodDTO GetById(int foodId)
        {
            return getFood(foodId);
        }

        public void Update(int foodId, Food food)
        {
            var fooddto = getFood(foodId);

            if (food.FoodCode != food.FoodCode && _context.Foods.Any(x => x.FoodCode == food.FoodCode))
                throw new AppException("Food '" + food.FoodCode + "' is already exists");

            _mapper.Map(food, fooddto);
            _context.Foods.Update(fooddto);
            _context.SaveChanges();
        }
        private FoodDTO getFood(int foodId)
        {
            var food = _context.Foods.FirstOrDefault(m => m.FoodId == foodId);
            if (food == null) throw new KeyNotFoundException("Food Details not found");
            return food;
        }
    }
}
