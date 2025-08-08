using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.Entities.JsonModels;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.UI;

namespace XblApp.XboxLiveService.Tests
{
    public class AchievementServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        public enum X1Games : long
        {
            GoW_UE = 1475571605,
            Gears5 = 374923716
        }

        public enum X360Games : long
        {
            LaraCroft_GoL = 1480657497,
            AC_Rev = 1431505017
        }

        public enum Gamers : long
        {
            DraftChimera239 = 2535419791913541
        }

        private readonly WebApplicationFactory<Program> _factory;

        public AchievementServiceTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData((long)Gamers.DraftChimera239)]
        public async Task GetAllAchievementsForGamerAsync_X1_Test(long xuid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService<AchievementX1Json> service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService<AchievementX1Json>>();

            AchievementX1Json result = await service.GetAllAchievementsForGamerAsync(xuid);

            Assert.NotNull(result.Achievements);
            Assert.NotEmpty(result.Achievements);
        }

        [Theory]
        [InlineData((long)Gamers.DraftChimera239)]
        public async Task GetAllAchievementsForGamerAsync_X360_Test(long xuid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService<AchievementX360Json> service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService<AchievementX360Json>>();

            AchievementX360Json result = await service.GetAllAchievementsForGamerAsync(xuid);

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
        [InlineData((long)Gamers.DraftChimera239, (long)X1Games.GoW_UE)]
        public async Task GetAchievementsForOneGameAsync_X1_Test(long xuid, long titleid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService<AchievementX1Json> service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService<AchievementX1Json>>();

            AchievementX1Json achievements = await service.GetAchievementsForOneGameAsync(xuid, titleid);

            Assert.NotNull(achievements.Achievements);
            Assert.NotEmpty(achievements.Achievements);
        }

        [Theory]
        [InlineData((long)Gamers.DraftChimera239, (long)X360Games.LaraCroft_GoL)]
        public async Task GetAchievementsForOneGameAsync_X360_Test(long xuid, long titleid)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService<AchievementX360Json> service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService<AchievementX360Json>>();

            AchievementX360Json achievements = await service.GetAchievementsForOneGameAsync(xuid, titleid);

            Assert.NotNull(achievements.Achievements);
            Assert.NotEmpty(achievements.Achievements);
        }
    }
}
