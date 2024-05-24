using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using XblApp.Application;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;

namespace XblApp.Infrastructure.XboxLiveServices
{
    public class GamerService : IXboxLiveGamerService
    {
        private static string DefScopes
        {
            get
            {
                return string.Join(",",
                    ProfileSettings.ACCOUNT_TIER,
                    ProfileSettings.APP_DISPLAY_NAME,
                    ProfileSettings.APP_DISPLAYPIC_RAW,
                    ProfileSettings.BIOGRAPHY,
                    ProfileSettings.GAME_DISPLAYPIC_RAW,
                    ProfileSettings.GAME_DISPLAY_NAME,
                    ProfileSettings.GAMERSCORE,
                    ProfileSettings.GAMERTAG,
                    ProfileSettings.PUBLIC_GAMERPIC,
                    ProfileSettings.MODERN_GAMERTAG,
                    ProfileSettings.MODERN_GAMERTAG_SUFFIX,
                    ProfileSettings.PREFERRED_COLOR,
                    ProfileSettings.LOCATION,
                    ProfileSettings.REAL_NAME,
                    ProfileSettings.REAL_NAME_OVERRIDE,
                    ProfileSettings.IS_QUARANTINED,
                    ProfileSettings.TENURE_LEVEL,
                    ProfileSettings.SHOW_USER_AS_AVATAR,
                    ProfileSettings.UNIQUE_MODERN_GAMERTAG,
                    ProfileSettings.XBOX_ONE_REP,
                    ProfileSettings.WATERMARKS);
            }
        }
        private const string _PROFILE_URL = "https://profile.xboxlive.com";

        private readonly HttpClient _httpClient;

        public GamerService(HttpClient httpClient)
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

        public async Task<Gamer> GetGamerProfileAsync(long xuid)
        {
            string baseAddress = _PROFILE_URL + $"/users/xuid({xuid})/profile/settings";

            var result = await GetProfileBase(baseAddress, authorizationCode);

            return null;
        }

        private async Task<HttpResponseMessage> GetProfileBase(string baseAddress, string authorizationCode)
        {
            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["settings"] = DefScopes;
            uriBuilder.Query = query.ToString();

            _httpClient.DefaultRequestHeaders.Add("x-xbl-contract-version", "3");
            _httpClient.DefaultRequestHeaders.Add("Authorization", authorizationCode);

            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.ToString());

            return response;
        }
    }
}
