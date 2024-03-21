using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using System.Web;
using WebApp.Models;

namespace WebApp.Services
{
    public class AuthenticationServiceXbl
    {
        private string _clientId;
        private string _clientSecret;
        private string _redirectUri;
        private readonly IHttpClientFactory _httpClientFactory;
        private string _defaultScopes { get { return string.Join(" ", "Xboxlive.signin", "Xboxlive.offline_access"); } }

        public AuthenticationServiceXbl(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _clientId = configuration["Authentication:Microsoft:ClientId"];
            _clientSecret = configuration["Authentication:Microsoft:ClientSecret"];
            _redirectUri = configuration["ConnectionStrings:RedirectUrl"];
            _httpClientFactory = httpClientFactory;
        }

        public string GenerateAuthorizationUrl()
        {
            string baseAddress = "https://login.live.com/oauth20_authorize.srf";

            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["client_id"] = _clientId;
            query["response_type"] = "code";
            query["approval_prompt"] = "auto";
            query["scope"] = _defaultScopes;
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
        public async Task<HttpResponseMessage> RequestOauth2Token(string authorizationCode)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"code", authorizationCode},
                {"scope", _defaultScopes},
                {"redirect_uri", _redirectUri}
            };

            return await RequestRefreshOauthToken(data);
        }

        /// <summary>
        /// 2. Authenticate via access token and receive user token.
        /// </summary>
        /// <param name="relying_party"></param>
        /// <param name="use_compact_ticket"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> RequestXauToken(TokenOAuthModelXbl tokenOAuth)
        {
            const string relyingParty = "http://auth.xboxlive.com";
            HttpClient httpClient = _httpClientFactory.CreateClient();
            const string base_address = "https://user.auth.xboxlive.com/user/authenticate";

            var data = new
            {
                RelyingParty = relyingParty,
                TokenType = "JWT",
                Properties = new
                {
                    AuthMethod = "RPS",
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = $"d={tokenOAuth.AccessToken}",
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            httpClient.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await httpClient.PostAsync(base_address, content);
            //TokenXauModelXbl result = await ProcessRespone<TokenXauModelXbl>(response);

            return response;
        }

        /// <summary>
        /// 3. Authorize via user token and receive final X token
        /// </summary>
        /// <param name="relying_party"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> RequestXstsToken(TokenXauModelXbl tokenXau)
        {
            const string relyingParty = "http://xboxlive.com";
            HttpClient httpClient = _httpClientFactory.CreateClient();
            const string base_address = "https://xsts.auth.xboxlive.com/xsts/authorize";

            var data = new
            {
                RelyingParty = relyingParty,
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string>() { tokenXau.Token },
                    SandboxId = "RETAIL"
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            httpClient.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(base_address, content);
            //TokenXstsModelXbl result = await ProcessRespone<TokenXstsModelXbl>(response);

            return response;
        }

        #endregion

        /// <summary>
        /// Refresh OAuth2 token
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> RefreshOauth2Token(string refreshToken)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"scope", _defaultScopes},
                {"refresh_token", refreshToken}
            };

            return await RequestRefreshOauthToken(data);
        }

        /// <summary>
        /// 1.2. Request/Refresh OAuth token
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> RequestRefreshOauthToken(Dictionary<string, string> data)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            const string baseAddress = "https://login.live.com/oauth20_token.srf";

            data.Add("client_id", _clientId);
            data.Add("client_secret", _clientSecret);

            HttpResponseMessage response = await httpClient.PostAsync(baseAddress, new FormUrlEncodedContent(data));
            //TokenOAuthModelXbl result = await ProcessRespone<TokenOAuthModelXbl>(response);

            return response;
        }

        public async Task<T> ProcessRespone<T>(HttpResponseMessage httpResponse)
        {
            T? result = default;

            if (httpResponse.IsSuccessStatusCode)
            {
                string tmpResult = await httpResponse.Content.ReadAsStringAsync();
                result = await httpResponse.Content.ReadFromJsonAsync<T>();
            }

            return result;
        }
    }
}
