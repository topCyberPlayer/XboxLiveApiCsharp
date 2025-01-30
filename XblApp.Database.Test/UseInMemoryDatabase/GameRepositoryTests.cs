using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Database.Seeding;
using XblApp.Domain.Entities;

namespace XblApp.Database.Test.UseInMemoryDatabase
{
    public class GameRepositoryTests
    {
        private readonly DbContextOptions<MsSqlDbContext> _options;

        public GameRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<MsSqlDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        private MsSqlDbContext CreateContext()
        {
            // Создаем новый контекст для каждой "сессии"
            return new MsSqlDbContext(_options);
        }

        [Fact]
        public async Task SaveGameAsync_ShouldAddNewGame_WhenGameDoesNotExist()
        {
            // Arrange
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                var games = new List<Game>
                {
                    new Game
                    {
                        GameId = 1,
                        GameName = "Test Game",
                        TotalAchievements = 5,
                        TotalGamerscore = 100
                    }
                };

                // Act
                await repository.SaveGameAsync(games);
            }

            // Assert (в новом контексте)
            using (var context = CreateContext())
            {
                var savedGame = context.Games.FirstOrDefault(g => g.GameId == 1);
                Assert.NotNull(savedGame);
                Assert.Equal("Test Game", savedGame.GameName);
                Assert.Equal(5, savedGame.TotalAchievements);
                Assert.Equal(100, savedGame.TotalGamerscore);
            }
        }

        [Fact]
        public async Task SaveGameAsync_ShouldUpdateGame_WhenGameExists()
        {
            // Arrange
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                await repository.SaveGameAsync(new List<Game>
                {
                    new Game
                    {
                        GameId = 1,
                        GameName = "Old Game",
                        TotalAchievements = 3,
                        TotalGamerscore = 50
                    }
                });

                var updatedGame1 = context.Games.FirstOrDefault(g => g.GameId == 1);
                Assert.NotNull(updatedGame1);
                Assert.Equal("Old Game", updatedGame1.GameName);
            }

            // Act
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                await repository.SaveGameAsync(new List<Game>
                {
                    new Game
                    {
                        GameId = 1,
                        GameName = "Updated Game",
                        TotalAchievements = 10,
                        TotalGamerscore = 200
                    }
                });
            }

            // Assert (в новом контексте)
            using (var context = CreateContext())
            {
                var updatedGame2 = context.Games.FirstOrDefault(g => g.GameId == 1);
                Assert.NotNull(updatedGame2);
                Assert.Equal("Updated Game", updatedGame2.GameName);
                Assert.Equal(10, updatedGame2.TotalAchievements);
                Assert.Equal(200, updatedGame2.TotalGamerscore);
            }
        }

        [Fact]
        public async Task SaveGameAsync_ShouldUpdateGame_WhenGameExists_ReadJson()
        {
            List<Game> games = GameJsonLoader.LoadGames("DataForTest", "TitleHub.json").ToList();

            // Act
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                await repository.SaveGameAsync(games);
            }
        }
    }
}
