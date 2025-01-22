using XblApp.Domain.Entities;

namespace XblApp.Test.RepositoryTest
{
    public class TotalAchievementsTests : BaseRepository
    {
        [Fact]
        public void TotalAchievements_ShouldBeCalculatedCorrectly()
        {
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
