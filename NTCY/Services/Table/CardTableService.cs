using AutoMapper;
using NTCY.Database;
using NTCY.Entities;
using NTCY.Exceptions;
using NTCY.Models.Club;
using NTCY.Models.Table;
using NTCY.Models.Users;
using NTCY.Utils;

namespace NTCY.Services.Table
{
    public class CardTableService : ICardTableService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public CardTableService(DataContext context,IJwtUtils jwtUtils,IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
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
        public void Add(CardTable table)
        {
            if (_context.CardTable.Any(x => x.TableName == table.TableName))
                throw new AppException("Table  '" + table.TableName + "' is already exists");

            var cardTableDTO = _mapper.Map<CardTableDTO>(table);

            _context.CardTable.Add(cardTableDTO);
            _context.SaveChanges();
        }

        public void Delete(int tableId)
        {
            var cardTable = getCardTable(tableId);
            _context.CardTable.Remove(cardTable);
            _context.SaveChanges();
        }

        public IEnumerable<CardTableDTO> GetAll()
        {
            return _context.CardTable;
        }

        public CardTableDTO GetById(int tableId)
        {
            return getCardTable(tableId);
        }

        public void Update(int tableId, CardTable table)
        {
            var cardTableDto = getCardTable(tableId);

            _mapper.Map(table, cardTableDto);
            _context.CardTable.Update(cardTableDto);
            _context.SaveChanges();
        }
        private CardTableDTO getCardTable(int tableId)
        {
            var cardTable = _context.CardTable.FirstOrDefault(m => m.TableNo == tableId);
            if (cardTable == null) throw new KeyNotFoundException("Table not found");
            return cardTable;
        }
    }
}
