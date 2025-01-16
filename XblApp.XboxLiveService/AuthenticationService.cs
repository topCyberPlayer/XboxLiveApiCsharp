using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;

namespace XblApp.XboxLiveService
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly AuthenticationConfig _config;
        private readonly HttpClient _authClient;
        private readonly HttpClient _userTokenClient;
        private readonly HttpClient _xstsTokenClient;

        private static readonly string DefScopes = string.Join(" ", "Xboxlive.signin", "Xboxlive.offline_access");

        public AuthenticationService(IHttpClientFactory factory, IOptions<AuthenticationConfig> config) : base(factory)
        {
            _config = config.Value;
            _authClient = factory.CreateClient("AuthServiceAuthToken");
            _userTokenClient = factory.CreateClient("AuthServiceUserToken");
            _xstsTokenClient = factory.CreateClient("AuthServiceXstsToken");
        }

        public string GenerateAuthorizationUrl()
        {
            const string baseAddress = "https://login.live.com/oauth20_authorize.srf";

            Dictionary<string, string> queryParameters = new()
            {
                { "scope", DefScopes },
                { "client_id", _config.ClientId },
                { "redirect_uri", _config.RedirectUri },
                { "response_type", "code" },
                { "approval_prompt", "auto" }
            };

            return QueryHelpers.AddQueryString(baseAddress, queryParameters);
        }

        public async Task<TokenOAuth> RequestOauth2Token(string authorizationCode)
        {
            Dictionary<string, string> data = new()
            {
                {"grant_type", "authorization_code"},
                {"code", authorizationCode},
                {"scope", DefScopes},
                {"redirect_uri", _config.RedirectUri},
                {"client_id", _config.ClientId},
                {"client_secret", _config.ClientSecret}
            };

            TokenOAuthJson resultJson = await SendFormUrlEncodedRequestAsync<TokenOAuthJson>(_authClient, data);

            return MapToTokenOAuth(resultJson);
        }

        public async Task<TokenXau> RequestXauToken(TokenOAuth tokenOAuth)
        {
            var data = new
            {
                RelyingParty = "http://auth.xboxlive.com",
                TokenType = "JWT",
                Properties = new
                {
                    AuthMethod = "RPS",
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = $"d={tokenOAuth.AccessToken}"
                }
            };

            TokenXauJson result = await SendJsonRequestAsync<TokenXauJson>(_userTokenClient, data);

            return MapToTokenXau(result);
        }

        public async Task<TokenXsts> RequestXstsToken(TokenXau tokenXau)
        {
            var data = new
            {
                RelyingParty = "http://xboxlive.com",
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string> { tokenXau.Token },
                    SandboxId = "RETAIL"
                }
            };

            TokenXstsJson result = await SendJsonRequestAsync<TokenXstsJson>(_xstsTokenClient, data);

            return MapToTokenXsts(result);
        }

        public async Task<TokenOAuth> RefreshOauth2Token(TokenOAuth expiredTokenOAuth)
        {
            var data = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "scope", DefScopes },
                { "refresh_token", expiredTokenOAuth.RefreshToken },
                { "client_id", _config.ClientId },
                { "client_secret", _config.ClientSecret }
            };

            TokenOAuthJson resultJson = await SendFormUrlEncodedRequestAsync<TokenOAuthJson>(_authClient, data);

            return MapToTokenOAuth(resultJson);
        }

        private async Task<T> SendFormUrlEncodedRequestAsync<T>(HttpClient client, Dictionary<string, string> data)
        {
            using var response = await client.PostAsync(string.Empty, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new InvalidOperationException("Failed to deserialize response");
        }

        private async Task<T> SendJsonRequestAsync<T>(HttpClient client, object data)
        {
            string jsonData = JsonSerializer.Serialize(data);
            StringContent content = new(jsonData, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(string.Empty, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new InvalidOperationException("Failed to deserialize response");
        }

        private static TokenOAuth MapToTokenOAuth(TokenOAuthJson json) =>
            new()
            {
                UserId = json.UserId,
                TokenType = json.TokenType,
                ExpiresIn = json.ExpiresIn,
                Scope = json.Scope,
                AccessToken = json.AccessToken,
                RefreshToken = json.RefreshToken,
                AuthenticationToken = json.AuthenticationToken
            };

        private static TokenXau MapToTokenXau(TokenXauJson json) =>
            new()
            {
                Token = json.Token,
                NotAfter = json.NotAfter,
                Uhs = json.Uhs,
                IssueInstant = json.IssueInstant
            };

        private static TokenXsts MapToTokenXsts(TokenXstsJson json) =>
            new()
            {
                Gamertag = json.Gamertag,
                Token = json.Token,
                IssueInstant = json.IssueInstant,
                AgeGroup = json.AgeGroup,
                NotAfter = json.NotAfter,
                Privileges = json.Privileges,
                Userhash = json.Userhash,
                UserPrivileges = json.UserPrivileges,
                Xuid = json.Xuid
            };
    }

    public class AuthenticationConfig
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RedirectUri { get; set; }
    }
}