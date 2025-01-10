using XblApp.Domain.Entities;

namespace XblApp.Infrastructure.Data.Seeding
{
    public static class SeedingLogic
    {
        public static async Task<int> SeedDatabaseIfNoGamersAsync(this XblAppDbContext context)
        {
            //const string SeedDataSearchName = "Gamers.json";

            var numGamers = context.Gamers.Count();
            if (numGamers == 0)
            {
                //List<Gamer> gamers = GamerJsonLoader.LoadGamers(Path.Combine(dataDirectory, SeedFileSubDirectory), SeedDataSearchName).ToList();
                List<Gamer> gamers = new List<Gamer>()
                {
                    new Gamer()
                    {
                        Gamertag = "Player 1",
                        Gamerscore = 23540,
                        GamerId = 1,
                        Location = "Moscow"
                    },
                    new Gamer()
                    {
                        Gamertag = "Player 2",
                        Gamerscore = 8200,
                        GamerId = 2,
                        Location = "Berlin"
                    }
                };
                await context.Gamers.AddRangeAsync(gamers);
                numGamers = await context.SaveChangesAsync();
            }
            return numGamers;
        }

        public static async Task<int> SeedDatabaseIfNoGamesAsync(this XblAppDbContext context)
        {
            var numGames = context.Games.Count();
            if (numGames == 0)
            {
                List<Game> games = new List<Game>()
                {
                    new Game()
                    {
                        GameName = "Game 1",
                        TotalAchievements = 50,
                        TotalGamerscore = 1000,
                        GameId = 1,
                        GamerLinks = new List<GamerGame>()
                        {
                            new GamerGame()
                            {
                                GamerId = 1,
                                CurrentAchievements = 32,
                                CurrentGamerscore = 760,
                            }
                        }
                    },
                    new Game()
                    {
                        GameName = "Game 2 Arcade",
                        TotalAchievements = 10,
                        TotalGamerscore = 200,
                        GameId = 2,
                        GamerLinks = new List<GamerGame>()
                        {
                            new GamerGame()
                            {
                                GamerId = 1,
                                CurrentAchievements = 8,
                                CurrentGamerscore = 180,
                            },
                            new GamerGame()
                            {
                                GamerId = 2,
                                CurrentAchievements = 4,
                                CurrentGamerscore = 60,
                            }
                        }
                    },
                    new Game()
                    {
                        GameName = "Game 3",
                        TotalAchievements = 60,
                        TotalGamerscore = 1250,
                        GameId = 3
                    },
                };
                await context.Games.AddRangeAsync(games);
                numGames = await context.SaveChangesAsync();
            }
            return numGames;
        }
    }
}
