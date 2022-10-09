using NTCY.Entities;
using NTCY.Models.Table;
using NTCY.Models.Users;

namespace NTCY.Services.Table
{
    public interface ICardTableService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<CardTableDTO> GetAll();
        CardTableDTO GetById(int tableId);
        void Add(CardTable table);
        void Update(int tableId, CardTable table);
        void Delete(int tableId);
    }
}
