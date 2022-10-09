using NTCY.Entities;
using NTCY.Models.Club;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NTCY.Models.Foods;

namespace NTCY.Database
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<MemberDTO> Members { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClubDTO> Club { get; set; }
        public DbSet<ClubCommitteeDTO> ClubCommittee { get; set; }
        public DbSet<CardTableDTO> CardTable { get; set; }
        public DbSet<FoodDTO> Foods { get; set; }
        public DbSet<RoomDTO> RoomDetails { get; set; }
        public DbSet<LiquorCategoryDTO> LiquorCategory { get; set; }
        public DbSet<LiquorDTO> Liquor { get; set; }

    }
}
