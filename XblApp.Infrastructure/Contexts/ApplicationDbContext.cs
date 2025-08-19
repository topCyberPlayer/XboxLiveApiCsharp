using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using XblApp.Domain.Entities;
using XblApp.Domain.Entities.XblAuth;
using XblApp.Infrastructure.Configurations;
using XblApp.Infrastructure.Models;

namespace XblApp.Infrastructure.Contexts
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

            IdentityRole? adminTeam = new() 
            { 
                Id = "role-adminTeam",
                Name = "adminTeam",
                NormalizedName = "ADMINTEAM"
            };

            IdentityRole gamerTeam = new()
            {
                Id = "role-gamerTeam",
                Name = "gamerTeam",
                NormalizedName = "GAMERTEAM"
            };

            IdentityRole moderatorTeam = new()
            {
                Id = "role-moderatorTeam",
                Name = "moderatorTeam",
                NormalizedName = "MODERATORTEAM"
            };

            modelBuilder.Entity<IdentityRole>().HasData(adminTeam, gamerTeam, moderatorTeam);
        }
    }
}
