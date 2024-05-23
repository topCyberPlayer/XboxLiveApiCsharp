using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XblApp.Domain.Entities;

namespace XblApp.Infrastructure.Data
{
    public static class DatabaseStartupHelpers
    {
        public static async Task<WebApplication> SetupDatabaseAsync(this WebApplication app)
        {
            // Initialize the database with seed data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetRequiredService<IWebHostEnvironment>();
                var context = scope.ServiceProvider.GetRequiredService<XblAppDbContext>();

                try
                {
                    var arePendingMigrations = context.Database.GetPendingMigrations().Any();
                    await context.Database.MigrateAsync();
                    await context.SeedDatabaseIfNoGamersAsync(env.WebRootPath);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

            return app;
        }
    }

    public static class SetupHelpers
    {
        private const string SeedDataSearchName = "Apress books*.json";
        public const string SeedFileSubDirectory = "seedData";

        public static async Task<int> SeedDatabaseIfNoGamersAsync(this XblAppDbContext context, string dataDirectory)
        {
            var numGamers = context.Gamers.Count();
            if (numGamers == 0)
            {
                var gamers = GamerJsonLoader.LoadGamers(Path.Combine(dataDirectory, SeedFileSubDirectory), SeedDataSearchName).ToList();
                await context.Gamers.AddRangeAsync(gamers);
                await context.SaveChangesAsync();
            }
            return numGamers;
        }
    }

    public static class GamerJsonLoader
    {
        // Add initial gamers
        private static readonly Gamer[] _gamers =
        [
            new Gamer { GamerId = 1, Gamertag = "Gamer1", Gamerscore = 1000, Bio = "Bio1", Location = "Location1" },
            new Gamer { GamerId = 2, Gamertag = "Gamer2", Gamerscore = 2000, Bio = "Bio2", Location = "Location2" }
        ];

        // Add initial games
        private static readonly Game[] _games =
        {
            new Game { GameId = 1, GameName = "Game1", TotalAchievements = 50, TotalGamerscore = 1000 },
            new Game { GameId = 2, GameName = "Game2", TotalAchievements = 100, TotalGamerscore = 2000 }
        };


        // Add initial gamer-game links
        private static readonly GamerGame[] _gamerGame = 
        [
            new GamerGame { GamerId = 1, GameId = 1, CurrentAchievements = 30, CurrentGamerscore = 600 },
            new GamerGame { GamerId = 1, GameId = 2, CurrentAchievements = 50, CurrentGamerscore = 1000 },
            new GamerGame { GamerId = 2, GameId = 1, CurrentAchievements = 50, CurrentGamerscore = 1000 }
        ];

        public static IEnumerable<Gamer> LoadGamers(string fileDir, string fileSearchString)
        {

        }
    }
}
