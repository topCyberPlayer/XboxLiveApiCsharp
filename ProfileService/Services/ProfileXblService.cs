using System.Collections.Specialized;
using System.Web;
using DomainModel.Profiles;

namespace ProfileService.Services
{
    public class ProfileXblService
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
        public const string PROFILE_URL = "https://profile.xboxlive.com";
        private string _AUTH_URL;
        private HttpClient _httpClient;

        public ProfileXblService(IConfiguration configuration, HttpClient httpClient)
        {
            _AUTH_URL = configuration["ConnectionStrings:AuthenticationApp"];
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetProfileByXuid(string xuid)
        {
            string baseAddress = PROFILE_URL + $"/users/xuid({xuid})/profile/settings";

            return await GetProfileBase(baseAddress);
        }

        public async Task<HttpResponseMessage> GetProfileByGamertag(string gamertag)
        {
            string baseAddress = PROFILE_URL + $"/users/gt({gamertag})/profile/settings";

            return await GetProfileBase(baseAddress);
        }

        private async Task<HttpResponseMessage> GetProfileBase(string baseAddress)
        {
            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["settings"] = DEF_SCOPES;
            uriBuilder.Query = query.ToString();

            _httpClient.DefaultRequestHeaders.Add("x-xbl-contract-version", "3");
            _httpClient.DefaultRequestHeaders.Add("Authorization", await GetAuthorizationHeaderValue());

            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.ToString());

            return response;
        }

        private async Task<string> GetAuthorizationHeaderValue()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_AUTH_URL+ "/api/authentication/getAuthorizationHeaderValue");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return string.Empty;
            }
        }

        private async Task<T> ProcessRespone<T>(HttpResponseMessage httpResponse)
        {
            T? result = default;

            if (httpResponse.IsSuccessStatusCode)
            {
                string responseData = await httpResponse.Content.ReadAsStringAsync();
                result = await httpResponse.Content.ReadFromJsonAsync<T>();
                //return Ok(JsonSerializer.Deserialize<T>(responseData));
                return result;
            }

            return result;
            //else
            //{
            //    //return //StatusCode((int)response.StatusCode, "Ошибка при выполнении запроса к XboxLive");
            //}

            
            //string tmpResult = await httpResponse.Content.ReadAsStringAsync();
            
            //if (httpResponse.IsSuccessStatusCode)
            //    result = await httpResponse.Content.ReadFromJsonAsync<T>();

            //return result;
        }
    }
}
