using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using XblApp.Domain.DTO;
using XblApp.Infrastructure.Contexts;

namespace XblApp.API.Test.Controllers
{
    public class GameControllerTest(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Get_ShouldOk()
        {
            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Act
                HttpResponseMessage? response = await _client.GetAsync($"api/game");

                // Assert: статус ответа
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Assert: контент
                string json = await response.Content.ReadAsStringAsync();
                Assert.False(string.IsNullOrWhiteSpace(json));

                IEnumerable<GameDTO>? games = JsonSerializer.Deserialize<IEnumerable<GameDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Assert.NotNull(games);

                // Доп. проверка — список не пуст
                Assert.NotEmpty(games);

                // Например, проверим хотя бы одно поле
                Assert.All(games, g => Assert.False(string.IsNullOrEmpty(g.GameName)));
            }
        }

        [Theory]
        [InlineData(2)]
        public async Task GetById_ShouldOk(long gameId)
        {
            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Act
                HttpResponseMessage? response = await _client.GetAsync($"api/game/{gameId}");

                // Assert: статус ответа
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Assert: контент
                string json = await response.Content.ReadAsStringAsync();
                Assert.False(string.IsNullOrWhiteSpace(json));

                GameDTO? game = JsonSerializer.Deserialize<GameDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Assert.NotNull(game);

                // Проверяем что вернулся именно GameDTO
                Assert.IsType<GameDTO>(game);

                // Проверяем отдельные свойства (пример)
                Assert.True(game.GameId > 0);
                Assert.False(string.IsNullOrWhiteSpace(game.GameName));
            }
        }
    }
}
