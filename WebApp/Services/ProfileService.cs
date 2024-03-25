using System.Net.Http.Headers;
using WebApp.Pages.Profile;

namespace WebApp.Services
{
    public class ProfileService : AuthenticationService
    {
        ProfileViewModel? result = default;

        public ProfileService(IConfiguration configuration,
            HttpClient httpClient2,
            AuthenticationServiceXbl authServXbl,
            AuthenticationServiceDb authServDb) : base(configuration, httpClient2, authServXbl, authServDb)
        {
            _xServiceUrl = configuration["ConnectionStrings:ProfileServiceUrl"];
        }

        public Task<ProfileViewModel> GetProfileByGamertag(string userId, string gamertag)
        {
            string requestUri = _xServiceUrl + $"/api/Profile/GetProfileByGamertag?gamertag={gamertag}";

            return base.GetBaseMethod<ProfileViewModel>(userId, requestUri);
        }
        //public async Task<(ProfileViewModel, HttpResponseMessage)> GetProfileByGamertag(string gamertag, string userId)
        //{
        //    if (IsDateExperid(userId))
        //    {
        //        string refreshToken = _authServDb.GetRefreshToken(userId);

        //        await RefreshTokens(userId, refreshToken);
        //    }

        //    string authorizationCode = _authServDb.GetAuthorizationHeaderValue(userId);

        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("XBL3.0", authorizationCode);

        //    string requestUri = _xServiceUrl + $"/api/Profile/GetProfileByGamertag?gamertag={gamertag}";
        //    HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        result = await response.Content.ReadFromJsonAsync<ProfileViewModel>();
        //    }
        //    return new(result, response);
        //}

        //public async Task<(ProfileViewModel, HttpResponseMessage)> GetProfileByGamertagTest(string gamertag)
        //{
        //    string requestUri = _xServiceUrl + $"/api/Profile/GetProfileByGamertagTest?gamertag={gamertag}";
        //    HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        result = await response.Content.ReadFromJsonAsync<ProfileViewModel>();
        //    }
        //    return new(result, response);
        //}
    }
}
