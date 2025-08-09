using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Infrastructure.Contexts;
using XblApp.Infrastructure.Models;
using XblApp.Domain.Entities;

namespace XblApp.Infrastructure.Extensions
{
    public static class DatabaseStartupExtensions
    {
        public static async Task<WebApplication> SetupApplicationDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            XblAppDbContext context = services.GetRequiredService<XblAppDbContext>();
            UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            try
            {
                //if (context.Database.GetPendingMigrations().Any())
                //    await context.Database.MigrateAsync();

                //await context.SeedDbDefaultUserAsync(userManager);
                //await context.SeedDbGamersAndGamesAsync();
            }
            catch (Exception ex)
            {
                // логгирование
                throw;
            }

            return app;
        }

        public static async Task SeedDbDefaultUserAsync(this XblAppDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Users.Any())
            {
                ApplicationUser user = new()
                {
                    UserName = "Aa",
                    Email = "admin@booble.net",
                    CreatedAt = DateTime.UtcNow,
                };

                string? password = "Biba#Boba34";

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);

                await userManager.AddToRoleAsync(user, "adminTeam");
            }
        }

        public static async Task SeedDbGamersAndGamesAsync(this XblAppDbContext context)
        {
            if (!context.Games.Any() && !context.Gamers.Any())
            {
                Gamer gamer1 = new()
                {

                };

                Gamer gamer2 = new()
                {

                };

                List<Game> games = new List<Game>()
                {
                    new Game()
                    {
                        
                    },
                    new Game()
                    {
                        
                    }
                };
                await context.Games.AddRangeAsync(games);

                await context.SaveChangesAsync();
            }
        }
    }

}
