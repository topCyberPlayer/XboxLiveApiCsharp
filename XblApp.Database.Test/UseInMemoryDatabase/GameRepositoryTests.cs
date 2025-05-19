using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Database.Seeding;
using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

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
        /// Проверка логику сохранения игр из json-файла
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SaveGameAsync_ShouldUpdateGame_WhenGameExists_ReadJson()
        {
            // Arrange
            GamerJson? gamerJson = JsonLoader<GamerJson>.LoadJsonFile("../../../../", "GamerProfiles.json");

            GameJson? gamesJson1 = JsonLoader<GameJson>.LoadJsonFile("../../../../", "Games1.json");

            GameJson? gamesJson2 = JsonLoader<GameJson>.LoadJsonFile("../../../../", "Games2.json");

            //gamers[0].ApplicationUserId = "0";
            //gamers[1].ApplicationUserId = "1";

            using (XblAppDbContext context = CreateContext())
            {
                GamerRepository gamerRepository = new(context);
                GameRepository gameRepository = new(context);

                await gamerRepository.SaveOrUpdateGamersAsync(gamerJson);
                await gameRepository.SaveOrUpdateGamesAsync(gamesJson1);
                await gameRepository.SaveOrUpdateGamesAsync(gamesJson2);
            }

            // Act
            using (var context = CreateContext())
            {
                GameRepository repository = new(context);

                // Assert
                List<Game>? result = await repository.GetAllGamesAndGamerGameAsync();

                Game? game = result.FirstOrDefault(g => g.GameName == "Sniper Elite 5");

                Assert.NotNull(game?.GameName);
                Assert.Equal(2, game.GamerGameLinks.Count); // Проверяем количество общих игроков у игры
            }
        }
    }
}
