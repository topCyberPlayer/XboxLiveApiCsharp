using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.Interfaces;

namespace XblApp.XboxLiveService.Tests
{
    public class GamerServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GamerServiceTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("HnS l top l")]
        public async Task Abc(string gamertag)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveGameService service = scope.ServiceProvider.GetRequiredService<IXboxLiveGameService>();

            var result = service.GetGamesForGamerProfileAsync(gamertag);
        }
    }
}
