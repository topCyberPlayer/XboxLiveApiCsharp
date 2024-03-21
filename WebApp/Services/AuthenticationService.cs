using System.Net.Http.Headers;
using WebApp.Models;
using WebApp.Pages.Profile;

namespace WebApp.Services
{
    public class AuthenticationService
    {
        private readonly string _apiProfileServiceUrl;
        private readonly HttpClient _client;
        private AuthenticationServiceXbl _authServXbl;
        private AuthenticationServiceDb _authServDb;

        private TokenOAuthModelXbl _tokenOAuth;
        private TokenXauModelXbl _tokenXau;
        private TokenXstsModelXbl _tokenXsts;


        public AuthenticationService(IConfiguration configuration,
            HttpClient client, 
            AuthenticationServiceXbl authServXbl, 
            AuthenticationServiceDb authServDb)
        {
            _apiProfileServiceUrl = configuration["ConnectionStrings:ProfileServiceUrl"];
            _client = client;
            _authServXbl = authServXbl;
            _authServDb = authServDb;
        }

        /// <summary>
        /// Need authorizationCode
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task RequestTokens(string userId, string authorizationCode)
        {
            HttpResponseMessage responseOAuth = await _authServXbl.RequestOauth2Token(authorizationCode);

            await ProcessTokens(responseOAuth, userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenOAuth"></param>
        /// <returns></returns>
        public async Task RefreshTokens(string userId, string tokenOAuth)
        {
            HttpResponseMessage responseOAuth = await _authServXbl.RefreshOauth2Token(tokenOAuth);

            await ProcessTokens(responseOAuth, userId);
        }

        private async Task ProcessTokens(HttpResponseMessage responseOAuth, string userId)
        {
            if (responseOAuth.IsSuccessStatusCode)
            {
                _tokenOAuth = await _authServXbl.ProcessRespone<TokenOAuthModelXbl>(responseOAuth);

                HttpResponseMessage responseXau = await _authServXbl.RequestXauToken(_tokenOAuth);

                if (responseXau.IsSuccessStatusCode)
                {
                    _tokenXau = await _authServXbl.ProcessRespone<TokenXauModelXbl>(responseXau);

                    HttpResponseMessage responseXsts = await _authServXbl.RequestXstsToken(_tokenXau);

                    if (responseXsts.IsSuccessStatusCode)
                    {
                        _tokenXsts = await _authServXbl.ProcessRespone<TokenXstsModelXbl>(responseXsts);

                        _authServDb.SaveToDb(userId, _tokenXsts);
                    }
                }
            }
        }

        public async Task<(ProfileViewModel, HttpResponseMessage)> GetProfileByGamertag(string gamertag, string userId)
        {
            if (IsDateExperid(userId))
            {
                string refreshToken = _authServDb.GetRefreshToken(userId);

                await RefreshTokens(userId, refreshToken);
            }

            string authorizationCode = _authServDb.GetAuthorizationHeaderValue(userId);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("XBL3.0", authorizationCode);

            ProfileViewModel? result = default;
            string requestUri = _apiProfileServiceUrl + $"/api/Profile/GetProfileByGamertag?gamertag={gamertag}";
            HttpResponseMessage response = await _client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ProfileViewModel>();

            }
            return new(result, response);
        }

        public async Task<(ProfileViewModel, HttpResponseMessage)> GetProfileByGamertagTest(string gamertag)
        {
            ProfileViewModel? result = default;
            string requestUri = _apiProfileServiceUrl + $"/api/Profile/GetProfileByGamertagTest?gamertag={gamertag}";
            HttpResponseMessage response = await _client.GetAsync(requestUri);

            if(response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ProfileViewModel>();
                
            }
            return new(result, response);

        }

        /// <summary>
        /// True - дата истекла, False - не истекла
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool IsDateExperid(string userId)
        {
            DateTime? dateNow = DateTime.UtcNow;

            DateTime? dateDb = _authServDb.IsDateExpired(userId);

            return dateNow>dateDb ? true : false;
        }
    }
}
