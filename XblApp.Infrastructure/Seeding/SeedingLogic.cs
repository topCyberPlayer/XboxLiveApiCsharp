using XblApp.Database.Contexts;
using XblApp.Domain.Entities;

namespace XblApp.Database.Seeding
{
    public static class SeedingLogic
    {
        public static async Task SeedDatabase(this XblAppDbContext context)
        {
            if (!context.Games.Any() && !context.Gamers.Any())
            {
                List<Game> games = GameJsonLoader.LoadGames("Seeding\\seedData", "Games.json").ToList();
                await context.Games.AddRangeAsync(games);

                List<Gamer> gamers = GamerJsonLoader.LoadGamers("Seeding\\seedData", "Gamers.json").ToList();
                await context.Gamers.AddRangeAsync(gamers);

                await context.SaveChangesAsync();
            }
        }
    }
}
