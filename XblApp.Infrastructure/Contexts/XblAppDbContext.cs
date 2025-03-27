using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using XblApp.Database.Configurations;
using XblApp.Database.Models;
using XblApp.Domain.Entities;

namespace XblApp.Database.Contexts
{
    public class XblAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public XblAppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Gamer> Gamers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<GamerAchievement> GamerAchievements { get; set; }
        public DbSet<XboxOAuthToken> XboxOAuthTokens { get; set; }
        public DbSet<XboxLiveToken> XboxLiveTokens { get; set; }
        public DbSet<XboxUserToken> XboxUserTokens { get; set; }

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

            var adminTeam = new IdentityRole("adminTeam");
            adminTeam.NormalizedName = "adminteam";

            var gamerTeam = new IdentityRole("gamerTeam");
            gamerTeam.NormalizedName = "gamerteam";

            var moderatorTeam = new IdentityRole("moderatorTeam");
            moderatorTeam.NormalizedName = "moderatorteam";

            modelBuilder.Entity<IdentityRole>().HasData(adminTeam, gamerTeam, moderatorTeam);
        }
    }
}
