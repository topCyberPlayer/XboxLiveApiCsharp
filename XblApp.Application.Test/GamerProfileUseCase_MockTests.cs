using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application.Test
{
    public class GamerProfileUseCase_MockTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GamerProfileUseCase_MockTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    //services.RemoveAll<IXboxLiveGameService>();
                    //services.AddSingleton<IXboxLiveGameService, XboxLiveGameServiceMock>();

                    //или
                    services.AddScoped<IXboxLiveGameService, XboxLiveGameServiceMock>();
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
        public Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag)
        {
            throw new NotImplementedException();
        }

        public Task<List<Game>> GetGamesForGamerProfileAsync(long xuid)
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
