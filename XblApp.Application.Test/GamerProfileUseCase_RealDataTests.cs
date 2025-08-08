using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Application.XboxLiveUseCases;
using XblApp.Domain.Entities.JsonModels;
using XblApp.UI;

namespace XblApp.Application.Test
{
    public class GamerProfileUseCase_RealDataTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private enum EnumGamerProfiles : long
        {
            HnS_top = 2533274912896954
           ,DraftChimera239 = 2535419791913541
        }

        private enum EnumGames : long
        {
            Battlefiled1 = 1386529057
           ,AC_Rev= 1431505017
           ,Gears5 = 374923716
           ,GOW_UE = 1475571605
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
            Assert.NotNull(result.Games);
        }

        /// <summary>
        /// Проверяю загрузку достижений для GamerProfile и игры
        /// </summary>
        /// <param name="gamerId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [Theory]
        [InlineData((long)EnumGamerProfiles.DraftChimera239)]
        public async Task GetAndSaveAchievements_Test(long gamerId)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            await useCase.GetAndSaveAchievements(gamerId);
        }

        /// <summary>
        /// Проверяю все 3 предыдущих пункта вместе: загрузка профиля, игр, достижений
        /// </summary>
        /// <param name="gamerId"></param>
        /// <returns></returns>
        [Theory]
        [InlineData((long)EnumGamerProfiles.DraftChimera239)]
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
