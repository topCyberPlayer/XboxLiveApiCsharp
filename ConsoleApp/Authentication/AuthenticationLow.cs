using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;

namespace ConsoleApp.Authentication
{
    internal class AuthenticationLow
    {
        private string[] DEFAULT_SCOPES = { "Xboxlive.signin", "Xboxlive.offline_access" };
        private string _clientId;
        private string _clientSecret;

        public string redirectUrl = "http://localhost:8080/auth/callback";
        public HttpClient clientSession;
        public OAuth2TokenResponse? OAuth;
        public XAUResponse UserToken;
        public XSTSResponse XstsToken;

        private string DEF_SCOPES { get { return string.Join(" ", DEFAULT_SCOPES); } }

        public AuthenticationLow(HttpClient session)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            this.clientSession = session;
            this._clientId = configuration["Authentication:Microsoft:ClientId"];
            this._clientSecret = configuration["Authentication:Microsoft:ClientSecret"];
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

        public async Task<OAuth2TokenResponse> RequestOauth2Token(string authorization_code)
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
        public async Task<OAuth2TokenResponse> RefreshOauth2Token()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"scope", DEF_SCOPES},
                {"refresh_token", OAuth?.RefreshToken}
            };

            return await Oauth2TokenRequest(data);
        }

        /// <summary>
        /// 1.2. Refresh OAuth2 token
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<OAuth2TokenResponse> Oauth2TokenRequest(Dictionary<string, string> data)
        {
            const string baseAddress = "https://login.live.com/oauth20_token.srf";

            data.Add("client_id", this._clientId);
            data.Add("client_secret", this._clientSecret);

            HttpResponseMessage response = await clientSession.PostAsync(baseAddress, new FormUrlEncodedContent(data));
            string tmpResult = await response.Content.ReadAsStringAsync();
            OAuth2TokenResponse result = await ConvertTo<OAuth2TokenResponse>(response);

            return result;
        }

        /// <summary>
        /// 2. Authenticate via access token and receive user token.
        /// </summary>
        /// <param name="relying_party"></param>
        /// <param name="use_compact_ticket"></param>
        /// <returns></returns>
        public async Task<XAUResponse> RequestXauToken(string relying_party = "http://auth.xboxlive.com")
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
                    RpsTicket = $"d={OAuth.AccessToken}",
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await clientSession.PostAsync(base_address, content);
            string tmpResult = await response.Content.ReadAsStringAsync();
            XAUResponse result = await ConvertTo<XAUResponse>(response);

            return result;
        }

        /// <summary>
        /// 3. Authorize via user token and receive final X token
        /// </summary>
        /// <param name="relying_party"></param>
        /// <returns></returns>
        public async Task<XSTSResponse> RequestXstsToken(string relying_party = "http://xboxlive.com")
        {
            const string base_address = "https://xsts.auth.xboxlive.com/xsts/authorize";

            var data = new
            {
                RelyingParty = relying_party,
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string>() { UserToken.Token },
                    SandboxId = "RETAIL"
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await clientSession.PostAsync(base_address, content);
            string tmpResult = await response.Content.ReadAsStringAsync();
            XSTSResponse result = await ConvertTo<XSTSResponse>(response);

            return result;
        }

        public async Task<T> ConvertTo<T>(HttpResponseMessage httpResponse) //where T: OAuth2TokenResponse, XAUResponse, XSTSResponse
        {
            T? result = default;

            if (httpResponse.IsSuccessStatusCode)
                result = await httpResponse.Content.ReadFromJsonAsync<T>();

            if (typeof(T) == typeof(OAuth2TokenResponse))
            {
                OAuth2TokenResponse? a = result as OAuth2TokenResponse;
                a.Issued = DateTime.UtcNow;
                a.Expires = DateTime.UtcNow.AddSeconds(a.ExpiresIn);
            }

            //if(typeof(T) == typeof(XTokenResponse))
            //{
            //    XTokenResponse? a = result as XTokenResponse;
            //    a.UserId = 
            //}

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
