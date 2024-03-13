using System.Text;
using System.Text.Json;

namespace WebApp.Services
{
    public class AuthenticationService
    {
        private readonly string _apiProfileServiceUrl;
        private readonly HttpClient _client;
        private AuthenticationServiceXbl _authServXbl;
        private AuthenticationServiceDb _authServDb;
        

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
        /// <param name="userName"></param>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task ZeroStart(string userName, string authorizationCode)
        {
            _authServXbl.OAuthToken = await _authServXbl.RequestOauth2Token(authorizationCode);
            _authServXbl.XauToken = await _authServXbl.RequestXauToken();
            _authServXbl.XstsToken = await _authServXbl.RequestXstsToken();

            _authServDb.SaveToDb(userName, _authServXbl.XstsToken);
        }

        public async Task<HttpResponseMessage> GetProfile(string gamertag, string userId)
        {
            string authorizationCode = _authServDb.GetAuthorizationHeaderValue(userId);
            var gameData = new { Gamertag = gamertag, AuthorizationCode = authorizationCode };
            string? json = JsonSerializer.Serialize(gameData);
            StringContent? content = new StringContent(json, Encoding.UTF8, "application/json");
            string requestUri = _apiProfileServiceUrl + "/api/Profile/GetProfileByGamertag";
            HttpResponseMessage response = await _client.PostAsync(requestUri, content);

            return response;
        }
    }
}
