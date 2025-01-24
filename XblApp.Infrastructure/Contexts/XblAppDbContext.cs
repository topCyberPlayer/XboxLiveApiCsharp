using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XblApp.Domain.Entities;

namespace XblApp.Database.Contexts
{
    public class XblAppDbContext : IdentityDbContext
    {
        protected readonly IConfiguration Configuration;
        public XblAppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Gamer> Gamers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<TokenOAuth> OAuthTokens { get; set; }
        public DbSet<TokenXau> XauTokens { get; set; }
        public DbSet<TokenXsts> XstsTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamerGame>()
                .HasKey(x => new { x.GamerId, x.GameId });
            modelBuilder.Entity<GamerAchievement>()
                .HasKey(x => new { x.GamerId, x.AchievementId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
