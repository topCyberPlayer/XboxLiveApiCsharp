using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using XblApp.Application.UseCases;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Test.UseCaseTest
{
    public class GamerProfileUseCaseTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GamerProfileUseCaseTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<IXboxLiveGameService>();
                    services.AddSingleton<IXboxLiveGameService, XboxLiveGameServiceMock>();
                });
            });
        }

        [Fact]
        public async Task UpdateProfileAsync_ShouldUseMockGameService()
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            var result = await useCase.UpdateProfileAsync(12345);

            // Assert
            Assert.NotNull(result);
            //Assert.Equal("Mock Game", result.Games.First().Name);
        }
    }

    public class XboxLiveGameServiceMock : IXboxLiveGameService
    {
        public Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag, string authorizationHeaderValue, int maxItems = 5)
        {
            throw new NotImplementedException();
        }

        public Task<List<Game>> GetGamesForGamerProfileAsync(long xuid, string authorizationHeaderValue, int maxItems = 5)
        {
            List<Game> result = new List<Game>()
            {
                new Game()
                {

                }
            };

            return Task.FromResult(result);
        }
    }
}
