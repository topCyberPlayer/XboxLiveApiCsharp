using Microsoft.EntityFrameworkCore;
using ProfileService.Profiles;

namespace ProfileService.Data
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<ProfileModelDb> Profiles { get; set; }
    }
}
