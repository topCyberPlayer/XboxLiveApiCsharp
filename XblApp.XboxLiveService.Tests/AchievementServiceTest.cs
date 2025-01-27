using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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

        [Fact]
        public async Task Abc()
        {
            long xuid = 2533274912896954;
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveAchievementService service = scope.ServiceProvider.GetRequiredService<IXboxLiveAchievementService>();

            var result = service.GetAchievementsXboxoneRecentProgressAndInfoAsync(xuid);
        }
    }
}
