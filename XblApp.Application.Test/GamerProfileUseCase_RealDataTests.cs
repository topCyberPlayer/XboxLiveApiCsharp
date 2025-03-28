using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.JsonModels;

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
        /// Проверяю загрузку GamerProfile из XboxLive и сохранение его в БД
        /// </summary>
        /// <param name="gamerId"></param>
        /// <returns></returns>
        [Theory]
        [InlineData((long)EnumGamerProfiles.HnS_top)]
        public async Task GetAndSaveGamerProfile_Test(long gamerId)
        {
            string applicationUserId = Guid.NewGuid().ToString();

            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            GamerJson result = await useCase.GetAndSaveGamerProfile(gamerId);
            result.ProfileUsers.FirstOrDefault().ApplicationUserId = applicationUserId;

            // Assert
            Assert.NotNull(result.ProfileUsers);
        }

        /// <summary>
        /// Проверяю загрузку всех игр связанных с GamerProfile
        /// </summary>
        /// <param name="gamerId"></param>
        /// <returns></returns>
        [Theory]
        [InlineData((long)EnumGamerProfiles.HnS_top)]
        public async Task GetAndSaveGames_Test(long gamerId)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            GameJson result = await useCase.GetAndSaveGames(gamerId);

            // Assert
            Assert.NotNull(result.Titles);
        }

        /// <summary>
        /// Проверяю загрузку достижений для GamerProfile и игры
        /// </summary>
        /// <param name="gamerId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Проверяю все 3 предыдущих пункта вместе: загрузка профиля, игр, достижений
        /// </summary>
        /// <param name="gamerId"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async Task UpdateProfile_Test(long gamerId)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            await useCase.UpdateProfileAsync(gamerId);
        }
    }
}
