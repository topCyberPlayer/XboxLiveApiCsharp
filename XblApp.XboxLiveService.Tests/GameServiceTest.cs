using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService.Tests
{
    public class GameServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GameServiceTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData((long)AchievementServiceTest.Gamers.DraftChimera239)]
        public async Task GetAllAchievementsForGamerAsync_X1_Test(long xuid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveGameService service = scope.ServiceProvider.GetRequiredService<IXboxLiveGameService>();

            GameJson result = await service.GetGamesForGamerProfileAsync(xuid);

            Assert.NotNull(result.Games);
            Assert.NotEmpty(result.Games);
        }
    }
}
