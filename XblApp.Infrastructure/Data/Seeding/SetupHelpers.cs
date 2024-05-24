namespace XblApp.Infrastructure.Data.Seeding
{
    public static class SetupHelpers
    {
        
        private const string SeedFileSubDirectory = "seedData";

        public static async Task<int> SeedDatabaseIfNoGamersAsync(this XblAppDbContext context, string dataDirectory)
        {
            const string SeedDataSearchName = "Gamers.json";

            var numGamers = context.Gamers.Count();
            if (numGamers == 0)
            {
                var gamers = GamerJsonLoader.LoadGamers(Path.Combine(dataDirectory, SeedFileSubDirectory), SeedDataSearchName).ToList();
                await context.Gamers.AddRangeAsync(gamers);
                await context.SaveChangesAsync();
            }
            return numGamers;
        }

        public static async Task<int> SeedDatabaseIfNoGamesAsync(this XblAppDbContext context, string dataDirectory)
        {
            const string SeedDataSearchName = "Games.json";

            var numGames = context.Gamers.Count();
            if (numGames == 0)
            {
                var games = GameJsonLoader.LoadGames(Path.Combine(dataDirectory, SeedFileSubDirectory), SeedDataSearchName).ToList();
                await context.Games.AddRangeAsync(games);
                await context.SaveChangesAsync();
            }
            return numGames;
        }
    }
}
