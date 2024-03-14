using System.Text;
using System.Text.Json;
using WebApp.Models;

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

        public async Task RefreshTokens(string userId, TokenOAuthModelXbl tokenOAuth)
        {
            _authServXbl.OAuthToken = await _authServXbl.RefreshOauth2Token(tokenOAuth);
            _authServXbl.XauToken = await _authServXbl.RequestXauToken();
            _authServXbl.XstsToken = await _authServXbl.RequestXstsToken();

            _authServDb.SaveToDb(userId, _authServXbl.XstsToken);
        }

        public async Task<HttpResponseMessage> GetProfile(string gamertag, string userId)
        {
            if(IsDateExperid(userId))
            {
                await RefreshTokens(userId);
            }

            string authorizationCode = _authServDb.GetAuthorizationHeaderValue(userId);
            var gameData = new { Gamertag = gamertag, AuthorizationCode = authorizationCode };
            string? json = JsonSerializer.Serialize(gameData);
            StringContent? content = new StringContent(json, Encoding.UTF8, "application/json");
            string requestUri = _apiProfileServiceUrl + "/api/Profile/GetProfileByGamertag";
            HttpResponseMessage response = await _client.PostAsync(requestUri, content);

            return response;
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
