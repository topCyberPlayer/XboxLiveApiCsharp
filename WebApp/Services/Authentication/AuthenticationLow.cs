using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace WebApp.Services.Authentication
{
    internal class AuthenticationLow
    {
        private string[] DEFAULT_SCOPES = { "Xboxlive.signin", "Xboxlive.offline_access" };
        private string _clientId;
        private string _clientSecret;

        public string redirectUrl = "http://localhost:8080/auth/callback";
        public readonly IHttpClientFactory _httpClientFactory;
        //public TokenOauth2Response? OAuth;
        //public TokenXauResponse UserToken;
        //public TokenXstsResponse XstsToken;

        private string DEF_SCOPES { get { return string.Join(" ", DEFAULT_SCOPES); } }

        public AuthenticationLow(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            this._clientId = "";//configuration["Authentication:Microsoft:ClientId"];
            this._clientSecret = "";//configuration["Authentication:Microsoft:ClientSecret"];
        }

        public string GenerateAuthorizationUrl()
        {
            string baseAddress = "https://login.live.com/oauth20_authorize.srf";

            UriBuilder uriBuilder = new UriBuilder(baseAddress);
            
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["client_id"] = this._clientId;
            query["response_type"] = "code";
            query["approval_prompt"] = "auto";
            query["scope"] = DEF_SCOPES;
            query["redirect_uri"] = redirectUrl;
            uriBuilder.Query = query.ToString();

            string result = uriBuilder.ToString();

            return result;
        }

        public async Task<TokenOauth2Response> RequestOauth2Token(string authorization_code)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"code", authorization_code},
                {"scope", DEF_SCOPES},
                {"redirect_uri", redirectUrl}
            };

            return await Oauth2TokenRequest(data);
        }

        /// <summary>
        /// 1.1. Refresh OAuth2 token
        /// </summary>
        /// <returns></returns>
        public async Task<TokenOauth2Response> RefreshOauth2Token(string refreshToken)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"scope", DEF_SCOPES},
                {"refresh_token", refreshToken}
            };

            return await Oauth2TokenRequest(data);
        }

        /// <summary>
        /// 1.2. Refresh OAuth2 token
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TokenOauth2Response> Oauth2TokenRequest(Dictionary<string, string> data)
        {
            const string baseAddress = "https://login.live.com/oauth20_token.srf";

            data.Add("client_id", this._clientId);
            data.Add("client_secret", this._clientSecret);

            using (var clientSession = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await clientSession.PostAsync(baseAddress, new FormUrlEncodedContent(data));
                string tmpResult = await response.Content.ReadAsStringAsync();
                TokenOauth2Response result = await ConvertTo<TokenOauth2Response>(response);

                return result;
            }
        }

        /// <summary>
        /// 2. Authenticate via access token and receive user token.
        /// </summary>
        /// <param name="relying_party"></param>
        /// <param name="use_compact_ticket"></param>
        /// <returns></returns>
        public async Task<TokenXauResponse> RequestXauToken(TokenOauth2Response tokenOAuth2Response, string relying_party = "http://auth.xboxlive.com")
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
                    RpsTicket = $"d={tokenOAuth2Response.AccessToken}",
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            using (var clientSession = _httpClientFactory.CreateClient())
            {
                clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await clientSession.PostAsync(base_address, content);

                TokenXauResponse result = await ConvertTo<TokenXauResponse>(response);

                return result;
            }
        }

        /// <summary>
        /// 3. Authorize via user token and receive final X token
        /// </summary>
        /// <param name="relying_party"></param>
        /// <returns></returns>
        public async Task<TokenXstsResponse> RequestXstsToken(TokenXauResponse xauResponse, string relying_party = "http://xboxlive.com")
        {
            const string base_address = "https://xsts.auth.xboxlive.com/xsts/authorize";

            var data = new
            {
                RelyingParty = relying_party,
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string>() { xauResponse.Token },
                    SandboxId = "RETAIL"
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            using (var clientSession = _httpClientFactory.CreateClient())
            {
                clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await clientSession.PostAsync(base_address, content);

                TokenXstsResponse result = await ConvertTo<TokenXstsResponse>(response);

                return result;
            }
        }

        public async Task<T> ConvertTo<T>(HttpResponseMessage httpResponse) //where T: OAuth2TokenResponse, XAUResponse, XSTSResponse
        {
            T? result = default;

            if (httpResponse.IsSuccessStatusCode)
                result = await httpResponse.Content.ReadFromJsonAsync<T>();

            if (typeof(T) == typeof(TokenOauth2Response))
            {
                TokenOauth2Response? a = result as TokenOauth2Response;
                a.Issued = DateTime.UtcNow;
                a.Expires = DateTime.UtcNow.AddSeconds(a.ExpiresIn);
            }

            return result;
        }

        /// <summary>
        /// Авторизация через браузер. Результат работы - authorization_code, который получаем из браузера и подставляем в Request_tokens
        /// </summary>
        /// <param name="_auth_mgr"></param>
        /// <returns></returns>
        public async ValueTask<string> GetAuthCodeFromBrowser()
        {
            string auth_url = GenerateAuthorizationUrl();

            using (HttpListener http = new HttpListener())
            {
                http.Prefixes.Add(redirectUrl + "/");
                http.Start();

                Process.Start(new ProcessStartInfo(auth_url) { UseShellExecute = true });

                string authorization_code = await HandleOAuth2Redirect(http);

                return authorization_code;
            }
        }

        /// <summary>
        /// Возвращает authorization_code, который получаем из браузера
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        private async ValueTask<string> HandleOAuth2Redirect(HttpListener http)
        {
            var context = await http.GetContextAsync();

            Uri uri = context.Request.Url;

            context.Response.OutputStream.Close();

            string code = uri.Query.Substring(uri.Query.IndexOf("=") + 1);

            return code;
        }
    }
}
