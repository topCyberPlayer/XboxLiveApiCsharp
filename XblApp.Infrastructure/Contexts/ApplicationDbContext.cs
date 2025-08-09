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
    /// <summary>
    /// Нужен чтобы Миграцию можно было сделать
    /// </summary>
    public class XblAppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "XblApp.API");
            //todo Попытаться сделать миграцию классическим способом - connectionString указывалась в DependencyInjection
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("SqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

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
                Id = Guid.NewGuid().ToString(),
                Name = "adminTeam",
                NormalizedName = "ADMINTEAM"
            };

            IdentityRole gamerTeam = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "gamerTeam",
                NormalizedName = "GAMERTEAM"
            };

            IdentityRole moderatorTeam = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "moderatorTeam",
                NormalizedName = "MODERATORTEAM"
            };

            modelBuilder.Entity<IdentityRole>().HasData(adminTeam, gamerTeam, moderatorTeam);
        }
    }
}
