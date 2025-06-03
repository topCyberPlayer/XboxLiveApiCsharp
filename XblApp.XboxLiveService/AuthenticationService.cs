using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService
{
    public class AuthenticationService : BaseService, IXboxLiveAuthenticationService
    {
        private readonly AuthenticationConfig _config;

        private static readonly string DefScopes = string.Join(" ", "Xboxlive.signin", "Xboxlive.offline_access");

        public AuthenticationService(IHttpClientFactory factory, IOptions<AuthenticationConfig> config) : base(factory)
        {
            _config = config.Value;
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

        public async Task<OAuthTokenJson> RequestOauth2Token(string authorizationCode)
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

            OAuthTokenJson resultJson = await SendFormUrlEncodedRequestAsync<OAuthTokenJson>(data);

            return resultJson;
        }

        public async Task<XauTokenJson> RequestXauToken(OAuthTokenJson token)
        {
            var data = new
            {
                RelyingParty = "http://auth.xboxlive.com",
                TokenType = "JWT",
                Properties = new
                {
                    AuthMethod = "RPS",
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = $"d={token.AccessToken}"
                }
            };

            HttpClient userTokenClient = factory.CreateClient("AuthServiceUserToken");
            XauTokenJson result = await SendRequestAsync<XauTokenJson>(userTokenClient, data);

            return result;
        }

        public async Task<XstsTokenJson> RequestXstsToken(XauTokenJson token)
        {
            var data = new
            {
                RelyingParty = "http://xboxlive.com",
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string> { token.Token },
                    SandboxId = "RETAIL"
                }
            };

            HttpClient xstsTokenClient = factory.CreateClient("AuthServiceXstsToken");

            XstsTokenJson result = await SendRequestAsync<XstsTokenJson>(xstsTokenClient, data);

            return result;
        }

        public async Task<OAuthTokenJson> RefreshOauth2Token(XboxAuthToken expiredTokenOAuth)
        {
            var data = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "scope", DefScopes },
                { "refresh_token", expiredTokenOAuth.RefreshToken },
                { "client_id", _config.ClientId },
                { "client_secret", _config.ClientSecret }
            };

            OAuthTokenJson resultJson = await SendFormUrlEncodedRequestAsync<OAuthTokenJson>(data);

            return resultJson;
        }

        private async Task<T> SendFormUrlEncodedRequestAsync<T>(Dictionary<string, string> data)
        {
            HttpClient client = factory.CreateClient("AuthServiceAuthToken");

            using var response = await client.PostAsync(string.Empty, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new InvalidOperationException("Failed to deserialize response");
        }

        private async Task<T> SendRequestAsync<T>(HttpClient client, object data)
        {
            string jsonData = JsonSerializer.Serialize(data);
            StringContent content = new(jsonData, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(string.Empty, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new InvalidOperationException("Failed to deserialize response");
        }
    }

    public class AuthenticationConfig
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RedirectUri { get; set; }
    }
}