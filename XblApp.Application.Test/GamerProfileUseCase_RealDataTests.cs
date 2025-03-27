using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.Entities;

namespace XblApp.Application.Test
{
    public class GamerProfileUseCase_RealDataTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private enum EnumGamerProfiles : long
        {
            HnS_top = 2533274912896954
           ,
        }

        private enum EnumGames : long
        {
            Battlefiled1 = 1386529057
           ,
        }

        public GamerProfileUseCase_RealDataTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => { });
            });
        }

        /// <summary>
        /// Тест не проходит потому что при сохранении в БД не указывается ASPNETUserId
        /// </summary>
        /// <param name="gamerId"></param>
        /// <returns></returns>
        [Theory]
        [InlineData((long)EnumGamerProfiles.HnS_top)]
        public async Task GetAndSaveGamerProfile_Test(long gamerId)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            List<Gamer> result = await useCase.GetAndSaveGamerProfile(gamerId);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData((long)EnumGamerProfiles.HnS_top)]
        public async Task GetAndSaveGames_Test(long gamerId)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            List<Game> result = await useCase.GetAndSaveGames(gamerId);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData((long)EnumGamerProfiles.HnS_top, (long)EnumGames.Battlefiled1)]
        public async Task GetAndSaveAchievements_Test(long gamerId, long gameId)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            await useCase.GetAndSaveAchievements(gamerId, gameId);
        }
    }
}
