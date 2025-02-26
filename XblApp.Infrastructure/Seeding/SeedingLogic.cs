using Microsoft.AspNetCore.Identity;
using XblApp.Database.Contexts;
using XblApp.Database.Models;
using XblApp.Domain.Entities;

namespace XblApp.Database.Seeding
{
    public static class SeedingLogic
    {
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

                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);

                await userManager.AddToRoleAsync(user, "adminTeam");
            }
        }

        public static async Task SeedDbGamersAndGamesAsync(this XblAppDbContext context)
        {
            if (!context.Games.Any() && !context.Gamers.Any())
            {
                //List<Game> games = GameJsonLoader.LoadGames("Seeding\\seedData", "Games.json").ToList();
                //await context.Games.AddRangeAsync(games);

                //List<Gamer> gamers = GamerJsonLoader.LoadGamers("Seeding\\seedData", "Gamers.json").ToList();
                //await context.Gamers.AddRangeAsync(gamers);

                //await context.SaveChangesAsync();
            }
        }
    }
}
