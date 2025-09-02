using Domain.DTO;
using Domain.Requests;
using Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;

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

        [Fact]
        public async Task POST_ShouldOk()
        {
            using (var scope = factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                GameRequest game = new()
                {
                    GameId = 1,
                    GameName = "Test Game",
                    TotalAchievements = 10,
                    TotalGamerscore = 100,
                    ReleaseDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Description = "This is a test game.",
                    Achievements = new List<AchievementRequest>
                    {
                        new AchievementRequest
                        {
                            AchievementId = 991,
                            Name = "First Achievement",
                            Description = "Unlock the first achievement.",
                            Gamerscore = 10,
                            IsSecret = false
                        },
                        new AchievementRequest
                        {
                            AchievementId = 992,
                            Name = "Secret Achievement",
                            Description = "Unlock the secret achievement.",
                            Gamerscore = 20,
                            IsSecret = true
                        }
                    }
                };

                string? jsonSer = JsonSerializer.Serialize(game);
                StringContent? content = new StringContent(jsonSer, Encoding.UTF8, "application/json");

                // Act
                HttpResponseMessage? response = await _client.PostAsync($"api/game", content);

                // Assert: статус ответа
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Assert: контент
                string jsonDesiar = await response.Content.ReadAsStringAsync();
                Assert.False(string.IsNullOrWhiteSpace(jsonDesiar));

                GameDTO? gameResp = JsonSerializer.Deserialize<GameDTO>(jsonDesiar, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Assert.NotNull(gameResp);

                // Проверяем что вернулся именно GameDTO
                Assert.IsType<GameDTO>(gameResp);

                // Проверяем отдельные свойства (пример)
                Assert.True(gameResp.GameId > 0);
                Assert.False(string.IsNullOrWhiteSpace(game.GameName));
            }
        }
    }
}
