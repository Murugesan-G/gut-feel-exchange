using NTCY.Entities;

namespace NTCY.Utils
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public int? ValidateToken(string token);
    }
}
