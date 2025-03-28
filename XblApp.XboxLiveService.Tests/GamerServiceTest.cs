using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService.Tests
{
    public class GamerServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GamerServiceTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamertag"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("DraftChimera239")]
        public async Task GetGamerProfileAsync_ShouldReturnValidProfile(string gamertag)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveGamerService service = scope.ServiceProvider.GetRequiredService<IXboxLiveGamerService>();

            GamerJson result = await service.GetGamerProfileAsync(gamertag);

            Assert.NotNull(result.ProfileUsers); // Коллекция не должна быть null
            Assert.NotEmpty(result.ProfileUsers); // Должна содержать хотя бы один элемент
            Assert.All(result.ProfileUsers, profile =>
            {
                Assert.NotNull(profile);
                Assert.False(string.IsNullOrEmpty(profile.Gamertag), "Gamertag не должен быть пустым");
                Assert.Equal(gamertag, profile.Gamertag); // Проверяем, что каждый объект содержит ожидаемый gamertag
            });
        }
    }
}
