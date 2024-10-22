using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using System.Web;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.XboxLiveServices
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly string? _clientId;
        private readonly string? _clientSecret;
        private readonly string? _redirectUri;
        
        private static string DefaultScopes => string.Join(" ", "Xboxlive.signin", "Xboxlive.offline_access");

        public AuthenticationService(IConfiguration configuration, IHttpClientFactory factory) : base(factory)
        {
            _clientId = configuration["Authentication:Microsoft:ClientId"];
            _clientSecret = configuration["Authentication:Microsoft:ClientSecret"];
            _redirectUri = configuration["ConnectionStrings:RedirectUrl"];
        }

        public string GenerateAuthorizationUrl()
        {
            string baseAddress = "https://login.live.com/oauth20_authorize.srf";

            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["client_id"] = _clientId;
            query["response_type"] = "code";
            query["approval_prompt"] = "auto";
            query["scope"] = DefaultScopes;
            query["redirect_uri"] = _redirectUri;
            uriBuilder.Query = query.ToString();

            string result = uriBuilder.ToString();

            return result;
        }

        #region Request

        /// <summary>
        /// 1. 
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task<TokenOAuthDTO> RequestOauth2Token(string authorizationCode)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"code", authorizationCode},
                {"scope", DefaultScopes},
                {"redirect_uri", _redirectUri}
            };

            HttpResponseMessage response = await RequestRefreshOauthToken(data);

            if (!response.IsSuccessStatusCode)
            {

            }

            TokenOAuthJson resultJson = await DeserializeJson<TokenOAuthJson>(response);

            return new TokenOAuthDTO
            {
                UserId = resultJson.UserId,
                TokenType = resultJson.TokenType,
                ExpiresIn = resultJson.ExpiresIn,
                Scope = resultJson.Scope,
                AccessToken = resultJson.AccessToken,
                RefreshToken = resultJson.RefreshToken,
                AuthenticationToken = resultJson.AuthenticationToken
            };
        }

        /// <summary>
        /// 2. Authenticate via access token and receive user token.
        /// </summary>
        /// <param name="relying_party"></param>
        /// <param name="use_compact_ticket"></param>
        /// <returns></returns>
        public async Task<TokenXauDTO> RequestXauToken(TokenOAuthDTO tokenOAuth)
        {
            HttpClient httpClient = factory.CreateClient("authServiceUserToken");

            var data = new
            {
                RelyingParty = "http://auth.xboxlive.com",
                TokenType = "JWT",
                Properties = new
                {
                    AuthMethod = "RPS",
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = $"d={tokenOAuth.AccessToken}",
                }
            };

            string jsonData = JsonSerializer.Serialize(data);
            StringContent content = new(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(string.Empty, content);

            if (!response.IsSuccessStatusCode)
            {

            }

            TokenXauJson result = await DeserializeJson<TokenXauJson>(response);

            return new TokenXauDTO
            {
                Token = result.Token,
                NotAfter = result.NotAfter,
                Uhs = result.Uhs,
                IssueInstant = result.IssueInstant,
            };
        }

        /// <summary>
        /// 3. Authorize via user token and receive final X token
        /// </summary>
        /// <param name="relying_party"></param>
        /// <returns></returns>
        public async Task<TokenXstsDTO> RequestXstsToken(TokenXauDTO tokenXau)
        {
            HttpClient httpClient = factory.CreateClient("authServiceXstsToken");

            var data = new
            {
                RelyingParty = "http://xboxlive.com",
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string>() { tokenXau.Token },
                    SandboxId = "RETAIL"
                }
            };

            string jsonData = JsonSerializer.Serialize(data);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(string.Empty, content);

            if (!response.IsSuccessStatusCode)
            {

            }

            TokenXstsJson result = await DeserializeJson<TokenXstsJson>(response);

            return new TokenXstsDTO
            {
                Gamertag = result.Gamertag,
                Token = result.Token,
                IssueInstant = result.IssueInstant,
                AgeGroup = result.AgeGroup, 
                NotAfter = result.NotAfter,
                Privileges = result.Privileges,
                Userhash = result.Userhash,
                UserPrivileges = result.UserPrivileges,
                Xuid = result.Xuid,
            };
        }

        #endregion

        /// <summary>
        /// Refresh OAuth2 token
        /// </summary>
        /// <returns></returns>
        public async Task<TokenOAuthDTO> RefreshOauth2Token(TokenOAuthDTO expiredTokenOAuthDTO)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"scope", DefaultScopes},
                {"refresh_token", expiredTokenOAuthDTO.RefreshToken}
            };

            HttpResponseMessage response = await RequestRefreshOauthToken(data);

            TokenOAuthJson result = await DeserializeJson<TokenOAuthJson>(response);

            return new TokenOAuthDTO
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken,
                TokenType = result.TokenType,
                AuthenticationToken = result.AuthenticationToken,
                ExpiresIn = result.ExpiresIn,
                Scope = result.Scope,
                UserId = result.UserId,
            };
        }

        /// <summary>
        /// 1.2. Request/Refresh OAuth token
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> RequestRefreshOauthToken(Dictionary<string, string> data)
        {
            HttpClient httpClient = factory.CreateClient("authServiceAuthToken");

            data.Add("client_id", _clientId);
            data.Add("client_secret", _clientSecret);

            HttpResponseMessage response = await httpClient.PostAsync(string.Empty, new FormUrlEncodedContent(data));

            return response;
        }
    }
}