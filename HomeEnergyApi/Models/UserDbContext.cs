using Microsoft.EntityFrameworkCore;

namespace HomeEnergyApi.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeUsageData> HomeUsageDatas { get; set; }
        public DbSet<UtilityProvider> UtilityProviders { get; set; }
        public DbSet<HomeUtilityProvider> HomeUtilityProviders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}