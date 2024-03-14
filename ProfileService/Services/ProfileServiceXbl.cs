using ProfileService.Profiles;
using System.Collections.Specialized;
using System.Web;

namespace ProfileService.Services
{
    public class ProfileServiceXbl
    {
        private string DEF_SCOPES
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
        private string _AUTH_URL;
        private readonly HttpClient _client;

        public ProfileServiceXbl(IConfiguration configuration, HttpClient client)
        {
            _AUTH_URL = configuration["ConnectionStrings:AuthenticationApp"];
            _client = client;
            //_client.BaseAddress = new Uri(_AUTH_URL);
        }

        public async Task<HttpResponseMessage> GetProfileByXuid(string xuid, string authorizationCode)
        {
            string baseAddress = _PROFILE_URL + $"/users/xuid({xuid})/profile/settings";

            return await GetProfileBase(baseAddress, authorizationCode);
        }

        public async Task<HttpResponseMessage> GetProfileByGamertag(string gamertag, string authorizationCode)
        {
            string baseAddress = _PROFILE_URL + $"/users/gt({gamertag})/profile/settings";

            return await GetProfileBase(baseAddress, authorizationCode);
        }

        private async Task<HttpResponseMessage> GetProfileBase(string baseAddress, string authorizationCode)
        {
            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["settings"] = DEF_SCOPES;
            uriBuilder.Query = query.ToString();

            _client.DefaultRequestHeaders.Add("x-xbl-contract-version", "3");
            _client.DefaultRequestHeaders.Add("Authorization", authorizationCode);

            HttpResponseMessage response = await _client.GetAsync(uriBuilder.ToString());

            return response;
        }

        public async Task<string> ProcessRespone(HttpResponseMessage httpResponse)
        {
            string? result = default;

            if (httpResponse.IsSuccessStatusCode)
            {
                result = await httpResponse.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
