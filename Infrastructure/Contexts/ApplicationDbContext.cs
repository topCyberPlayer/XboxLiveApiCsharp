using Domain.Entities;
using Domain.Entities.XblAuth;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gamer> Gamers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<GamerAchievement> GamerAchievements { get; set; }
        public DbSet<XboxAuthToken> XboxOAuthTokens { get; set; }
        public DbSet<XboxXauToken> XboxLiveTokens { get; set; }
        public DbSet<XboxXstsToken> XboxUserTokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GamerConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new AchievementConfiguration());
            modelBuilder.ApplyConfiguration(new GamerGameConfiguration());
            modelBuilder.ApplyConfiguration(new GamerAchievementConfiguration());
            modelBuilder.ApplyConfiguration(new XboxOAuthTokenConfiguration());
            modelBuilder.ApplyConfiguration(new XboxLiveTokenConfiguration());
            modelBuilder.ApplyConfiguration(new XboxUserTokenConfiguration());
        }
    }
}
