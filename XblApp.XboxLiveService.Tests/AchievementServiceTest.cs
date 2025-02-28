using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.XboxLiveService.Tests
{
    public class AchievementServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AchievementServiceTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(2533274912896954)]
        public async Task GetAchievementsXboxoneRecentProgressAndInfoAsyncTest_(long xuid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService>();

            List<Achievement> result = await service.GetAchievementsX1RecentProgressAndInfoAsync(xuid);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(2533274912896954)]
        public async Task GetAchievementsTest_(long xuid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService>();

            var result = await service.GetAchievements(xuid);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(2533274912896954, 1386529057)]//HnS l top l, Battlefield™ 1
        public async Task GetAchievementsX1GameprogressTest(long xuid, long titleid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService>();

            var result = await service.GetAchievementsX1Gameprogress(xuid, titleid);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
