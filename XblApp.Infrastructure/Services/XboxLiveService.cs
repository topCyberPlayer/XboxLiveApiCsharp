using System.Text.Json;
using XblApp.Application;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Infrastructure.Services
{
    public class XboxLiveService : IXboxLiveService
    {
        private readonly HttpClient _httpClient;

        public XboxLiveService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Gamer> GetGamerProfileAsync(string gamertag)
        {
            var response = await _httpClient.GetAsync($"https://api.xboxlive.com/gamertag/{gamertag}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var gamerDto = JsonSerializer.Deserialize<GamerDTO>(content);

            return new Gamer
            {
                GamerId = gamerDto.GamerId,
                Gamertag = gamerDto.Gamertag,
                Gamerscore = gamerDto.Gamerscore,
                Bio = gamerDto.Bio,
                Location = gamerDto.Location
            };
        }
    }
}
