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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamertag"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("HnS l top l")]
        public async Task GetGamerProfileAsync_ShouldReturnValidProfile(string gamertag)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IXboxLiveGamerService service = scope.ServiceProvider.GetRequiredService<IXboxLiveGamerService>();

            var result = await service.GetGamerProfileAsync(gamertag);

            Assert.NotNull(result); // Коллекция не должна быть null
            Assert.NotEmpty(result); // Должна содержать хотя бы один элемент
            Assert.All(result, profile =>
            {
                Assert.NotNull(profile);
                Assert.False(string.IsNullOrEmpty(profile.Gamertag), "Gamertag не должен быть пустым");
                Assert.Equal(gamertag, profile.Gamertag); // Проверяем, что каждый объект содержит ожидаемый gamertag
            });
        }
    }
}
