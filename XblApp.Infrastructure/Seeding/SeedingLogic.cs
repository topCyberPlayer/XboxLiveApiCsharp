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
                List<Game> games = new List<Game>()
                {
                    new Game()
                    {
                        GameId = 1,
                        GameName = "Stray",
                        Achievements = new List<Achievement>()
                        {
                            new Achievement()
                            {
                                AchievementId = 1,
                                Name = "Неудачный прыжок",
                                Description = "Упадите внутрь города",
                                Gamerscore = 10,
                                IsSecret = false
                            },
                            new Achievement()
                            {
                                AchievementId = 2,
                                Name = "Не один",
                                Description = "Познакомиться с B-12",
                                Gamerscore = 40,
                                IsSecret = false
                            },
                            new Achievement()
                            {
                                AchievementId = 3,
                                Name = "Язык проглотил",
                                Description = "Попросите B-12 перевести, что говорит робот",
                                Gamerscore = 20,
                                IsSecret = false
                            }
                        }
                    },
                    new Game()
                    {
                        GameId = 3,
                        GameName = "Gears of War",
                        Achievements = new List<Achievement>()
                        {
                            new Achievement()
                            {
                                AchievementId = 11,
                                Name = "Prison Breakout",
                                Description = "Complete tutorial on any skill level",
                                Gamerscore = 10,
                                IsSecret = false
                            },
                            new Achievement()
                            {
                                AchievementId = 21,
                                Name = "Complete Act 1 on Casual",
                                Description = "Complete Act 1 on Casual Difficulty",
                                Gamerscore = 10,
                                IsSecret = false
                            }
                        }
                    },
                };
                await context.Games.AddRangeAsync(games);

                List<Gamer> gamers = new List<Gamer>()
                {
                    new Gamer() { GamerId = 1, Gamertag = "Player 1", Gamerscore = 23540 },
                    new Gamer() { GamerId = 2, Gamertag = "Player 2", Gamerscore = 8200 }
                };
                await context.Gamers.AddRangeAsync(gamers);

                await context.SaveChangesAsync();
            }
        }
    }
}
