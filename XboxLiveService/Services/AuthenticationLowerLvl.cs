using System.Collections.Specialized;
using System.Text.Json;
using System.Text;
using System.Web;
using XboxLiveService.Models;

namespace XboxLiveService.Services
{
    public class AuthenticationLowerLvl
    {
        private string _clientId;
        private string _clientSecret;
        private string _redirectUri;
        private HttpClient _httpClient;
        private string _defaultScopes { get { return string.Join(" ", "Xboxlive.signin", "Xboxlive.offline_access"); } }

        public OAuthTokenModel OAuthToken;
        public XauTokenModel XauToken;
        public XstsTokenModel XstsToken;

        public AuthenticationLowerLvl(IConfiguration configuration, HttpClient httpClient)
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
        /// <param name="authorization_code"></param>
        /// <returns></returns>
        public async Task<OAuthTokenModel> RequestOauth2Token(string authorization_code)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"code", authorization_code},
                {"scope", _defaultScopes},
                {"redirect_uri", _redirectUri}
            };

            return await RequestOauth2Token(data);
        }

        /// <summary>
        /// 2. Authenticate via access token and receive user token.
        /// </summary>
        /// <param name="relying_party"></param>
        /// <param name="use_compact_ticket"></param>
        /// <returns></returns>
        public async Task<XauTokenModel> RequestXauToken(string relying_party = "http://auth.xboxlive.com")
        {
            const string base_address = "https://user.auth.xboxlive.com/user/authenticate";

            var data = new
            {
                RelyingParty = relying_party,
                TokenType = "JWT",
                Properties = new
                {
                    AuthMethod = "RPS",
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = $"d={OAuthToken.AccessToken}",
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            _httpClient.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(base_address, content);
            XauTokenModel result = await ConvertTo<XauTokenModel>(response);

            return result;
        }

        /// <summary>
        /// 3. Authorize via user token and receive final X token
        /// </summary>
        /// <param name="relying_party"></param>
        /// <returns></returns>
        public async Task<XstsTokenModel> RequestXstsToken(string relying_party = "http://xboxlive.com")
        {
            const string base_address = "https://xsts.auth.xboxlive.com/xsts/authorize";

            var data = new
            {
                RelyingParty = relying_party,
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string>() { XauToken.Token },
                    SandboxId = "RETAIL"
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            _httpClient.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(base_address, content);
            XstsTokenModel result = await ConvertTo<XstsTokenModel>(response);

            return result;
        }

        #endregion

        /// <summary>
        /// 1.1. Refresh OAuth2 token
        /// </summary>
        /// <returns></returns>
        public async Task<OAuthTokenModel> RefreshOauth2Token()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"scope", _defaultScopes},
                {"refresh_token", OAuthToken.RefreshToken}
            };

            return await RequestOauth2Token(data);
        }

        /// <summary>
        /// 1.2. Request/Refresh OAuth token
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<OAuthTokenModel> RequestOauth2Token(Dictionary<string, string> data)
        {
            const string baseAddress = "https://login.live.com/oauth20_token.srf";

            data.Add("client_id", _clientId);
            data.Add("client_secret", _clientSecret);

            HttpResponseMessage response = await _httpClient.PostAsync(baseAddress, new FormUrlEncodedContent(data));
            OAuthTokenModel result = await ConvertTo<OAuthTokenModel>(response);

            return result;
        }

        private async Task<T> ConvertTo<T>(HttpResponseMessage httpResponse)
        {
            T? result = default;

            string tmpResult = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
                result = await httpResponse.Content.ReadFromJsonAsync<T>();

            if (typeof(T) == typeof(OAuthTokenModel))
            {
                OAuthTokenModel? a = result as OAuthTokenModel;
                a.Issued = DateTime.UtcNow;
                a.Expires = DateTime.UtcNow.AddSeconds(a.ExpiresIn);
            }

            return result;
        }
    }
}
