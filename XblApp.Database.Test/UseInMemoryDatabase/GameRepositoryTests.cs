using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Database.Seeding;
using XblApp.Domain.Entities;

namespace XblApp.Database.Test.UseInMemoryDatabase
{
    public class GameRepositoryTests
    {
        private readonly DbContextOptions<XblAppDbContext> _options;

        public GameRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<XblAppDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        /// <summary>
        /// Создаем новый контекст для каждой "сессии"
        /// </summary>
        /// <returns></returns>
        private XblAppDbContext CreateContext()
        {
            return new XblAppDbContext(_options);
        }

        /// <summary>
        /// Проверяю логику сохранения новой игры в БД (такой игры в БД еще нет)
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="gameName"></param>
        /// <param name="totalAchivos"></param>
        /// <param name="totalGs"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1, "Test Game", 5, 100)]
        public async Task SaveGameAsync_ShouldAddNewGame_WhenGameDoesNotExist(long gameId, string gameName, int totalAchivos, int totalGs)
        {
            // Arrange
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                var games = new List<Game>
                {
                    new Game
                    {
                        GameId = gameId,
                        GameName = gameName,
                        TotalAchievements = totalAchivos,
                        TotalGamerscore = totalGs
                    }
                };

                // Act
                await repository.SaveOrUpdateGamesAsync(games);
            }

            // Assert (в новом контексте)
            using (var context = CreateContext())
            {
                Game? savedGame = context.Games.FirstOrDefault(g => g.GameId == 1);
                Assert.NotNull(savedGame);
                Assert.Equal(gameName, savedGame.GameName);
                Assert.Equal(totalAchivos, savedGame.TotalAchievements);
                Assert.Equal(totalGs, savedGame.TotalGamerscore);
            }
        }

        /// <summary>
        /// Проверяю логику сохранения уже существующей игры в БД
        /// </summary>
        /// <returns></returns>
        [Theory]
        [InlineData(1, "Updated Game", 10, 200)]
        public async Task SaveGameAsync_ShouldUpdateGame_WhenGameExists(long gameId, string gameName, int totalAchivos, int totalGs)
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
                        GameName = "Old Game",
                        TotalAchievements = 3,
                        TotalGamerscore = 50
                    }
                };

                await repository.SaveOrUpdateGamesAsync(games);
            }

            // Act
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                var games = new List<Game>
                {
                    new Game
                    {
                        GameId = gameId,
                        GameName = gameName,
                        TotalAchievements = totalAchivos,
                        TotalGamerscore = totalGs
                    }
                };

                await repository.SaveOrUpdateGamesAsync(games);
            }

            // Assert (в новом контексте)
            using (var context = CreateContext())
            {
                var updatedGame2 = context.Games.FirstOrDefault(g => g.GameId == gameId);
                
                Assert.NotNull(updatedGame2);
                Assert.Equal(gameName, updatedGame2.GameName);
                Assert.Equal(totalAchivos, updatedGame2.TotalAchievements);
                Assert.Equal(totalGs, updatedGame2.TotalGamerscore);
            }
        }

        /// <summary>
        /// Проверка логики загрузки игр из json-файла
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SaveGameAsync_ShouldUpdateGame_WhenGameExists_ReadJson()
        {
            // Arrange
            List<Gamer> gamers = GamerJsonLoader.LoadGamers("DataForTest", "Profile.json").ToList();
            List<Game> games1 = GameJsonLoader.LoadGames("DataForTest", "TitleHub1.json").ToList();
            List<Game> games2 = GameJsonLoader.LoadGames("DataForTest", "TitleHub2.json").ToList();

            gamers[0].ApplicationUserId = "0";
            gamers[1].ApplicationUserId = "1";

            using (var context = CreateContext())
            {
                var gamerRepository = new GamerRepository(context);
                var gameRepository = new GameRepository(context);

                await gamerRepository.SaveOrUpdateGamersAsync(gamers);
                await gameRepository.SaveOrUpdateGamesAsync(games1);
                await gameRepository.SaveOrUpdateGamesAsync(games2);
            }

            // Act
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                // Assert
                var result = await repository.GetAllGamesAsync2();

                var game = result.FirstOrDefault(g => g.Item1 == "Sniper Elite 5");

                Assert.NotNull(game.Item1);
                Assert.Equal(2, game.Item4); // Проверяем количество общих игроков у игры
            }
        }

        [Fact]
        public async Task GetGameAsyncTest()
        {
            // Arrange
            List<Game> games = GameJsonLoader.LoadGames("DataForTest", "TitleHub.json").ToList();

            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                await repository.SaveOrUpdateGamesAsync(games);
            }

            // Act
            using (var context = CreateContext())
            {
                var repository = new GameRepository(context);

                // Assert
                List<Game>? result = await repository.GetAllGamesAsync();
                Assert.NotNull(result);
                Assert.Equal(5, result.Count());
            }
        }
    }
}
