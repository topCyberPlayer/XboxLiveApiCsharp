using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XblApp.Domain.Entities;
using XblApp.Infrastructure.Data;

namespace XblApp.Test
{
    public class TotalAchievementsTests
    {
        private readonly IConfiguration configuration;

        internal readonly Dictionary<string, string> inMemorySettings = new()
        {
            {"Key1", "Value1"},
            {"ConnectionStrings:MsSqlConnection", "Server=(localdb)\\mssqllocaldb;Database=XblApp;Trusted_Connection=True;MultipleActiveResultSets=true"},
            {"Nested:Key2", "NestedValue2"}
        };

        public TotalAchievementsTests()
        {
            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [Fact]
        public void TotalAchievements_ShouldBeCalculatedCorrectly()
        {
            using var context = new MsSqlDbContext(configuration);
            var a = context.Games.ToList();
            context.Games.AddRange(
                new Game
                {
                    GameId = 19,
                    GameName = "Stray",
                    Achievements = new List<Achievement>
                    {
                        new Achievement()
                        {
                            AchievementId = 81,
                            Name = "Неудачный прыжок",
                            Description = "Упадите внутрь города",
                            Gamerscore = 10,
                            IsSecret = false
                        },
                        new Achievement()
                        {
                            AchievementId = 210,
                            Name = "Не один",
                            Description = "Познакомиться с B-12",
                            Gamerscore = 40,
                            IsSecret = false
                        },
                        new Achievement()
                        {
                            AchievementId = 310,
                            Name = "Язык проглотил",
                            Description = "Попросите B-12 перевести, что говорит робот",
                            Gamerscore = 20,
                            IsSecret = false
                        }
                    }
                });

            context.SaveChanges();
        }
    }
}
