using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

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
        public async Task GetAchievementsTest_(long xuid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService>();

            AchievementJson result = await service.GetAchievementsAsync(xuid);

            Assert.NotNull(result.Achievements);
            Assert.NotEmpty(result.Achievements);
        }

        /// <summary>
        /// Загружаю из Xbox Live достижения для игры и игрока
        /// </summary>
        /// <param name="xuid"></param>
        /// <param name="titleid"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(2533274912896954, 552499398)]//HnS l top l, Gears of  War 4
        [InlineData(2533274912896954, 1386529057)]//HnS l top l, Battlefield™ 1
        public async Task GetAchievementsX1GameprogressTest(long xuid, long titleid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService>();

            AchievementJson achievements = await service.GetAchievementsAsync(xuid, titleid);

            Assert.NotNull(achievements.Achievements);
            Assert.NotEmpty(achievements.Achievements);
        }
    }
}
