﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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
                    services.AddScoped<IXboxLiveGamerService, XboxLiveGamerServiceMock>();
                    services.AddScoped<IXboxLiveGameService, XboxLiveGameServiceMock>();
                });
            });
        }

        [Theory]
        [InlineData(12345)]
        public async Task UpdateProfileAsync_ShouldUseMockGameService(long xuid)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            GamerProfileUseCase useCase = scope.ServiceProvider.GetRequiredService<GamerProfileUseCase>();

            // Act
            var result = await useCase.UpdateProfileAsync(xuid);

            // Assert
            Assert.NotNull(result);
            //Assert.Equal("Mock Game", result.Games.First().Name);
        }
    }

    public class XboxLiveGamerServiceMock : IXboxLiveGamerService
    {
        public Task<List<Gamer>> GetGamerProfileAsync(string gamertag)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gamer>> GetGamerProfileAsync(long xuid)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
