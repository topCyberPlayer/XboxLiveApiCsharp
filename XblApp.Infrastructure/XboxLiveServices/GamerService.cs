using System.Collections.Specialized;
using System.Web;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.XboxLiveServices
{
    public class GamerService : BaseService, IXboxLiveGamerService
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
        private const string ProfileUrl = "https://profile.xboxlive.com";

        private readonly HttpClient _httpClient;

        public GamerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GamerDTO> GetGamerProfileAsync(string gamertag, string authorizationCode)
        {
            string baseAddress = ProfileUrl + $"/users/gamertag({gamertag})/profile/settings";

            return await GetProfileBase(baseAddress, authorizationCode);
        }

        public async Task<GamerDTO> GetGamerProfileAsync(long xuid, string authorizationCode)
        {
            string baseAddress = ProfileUrl + $"/users/xuid({xuid})/profile/settings";

            return await GetProfileBase(baseAddress, authorizationCode);
        }

        private async Task<GamerDTO> GetProfileBase(string baseAddress, string authorizationCode)
        {
            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["settings"] = DefScopes;
            uriBuilder.Query = query.ToString();

            _httpClient.DefaultRequestHeaders.Add("x-xbl-contract-version", "3");
            _httpClient.DefaultRequestHeaders.Add("Authorization", authorizationCode);

            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.ToString());

            GamerJson result = await DeserializeJson<GamerJson>(response);

            return new GamerDTO
            {

            };
        }
    }
}
