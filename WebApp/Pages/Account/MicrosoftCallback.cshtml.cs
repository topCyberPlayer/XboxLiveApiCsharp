using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApp.Pages.Account
{
    public class MicrosoftCallbackModel : PageModel
    {
        private string? _accessToken;
        private readonly IHttpClientFactory _httpClientFactory;
        

        public MicrosoftCallbackModel(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //Сохранить в БД(ProfileUser)

            var authenticateResult = await HttpContext.AuthenticateAsync("Microsoft");

            if (authenticateResult.Succeeded)
            {
                IDictionary<string, string?> items = authenticateResult.Properties.Items;

                _accessToken = authenticateResult.Properties.GetTokenValue("access_token");
                //XAUResponse xauResponse = await RequestUserToken();
                
                
                // Получите информацию о пользователе
                var user = authenticateResult.Principal;
            }
            else
            {
                // Обработка неудачной аутентификации
            }

            // Перенаправьте пользователя обратно на главную страницу или другую страницу.
            return RedirectToPage("/Profiles/CurrentUserProfile");
        }

        /// <summary>
        /// 2. Authenticate via access token and receive user token.
        /// </summary>
        /// <param name="relying_party"></param>
        /// <param name="use_compact_ticket"></param>
        /// <returns></returns>
        private async Task<XAUResponse> RequestUserToken(string relying_party = "http://auth.xboxlive.com")
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
                    RpsTicket = $"d={_accessToken}",
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            using (var clientSession = _httpClientFactory.CreateClient())
            {
                clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await clientSession.PostAsync(base_address, content);

                XAUResponse result = await ConvertTo<XAUResponse>(response);

                return result;
            }
        }

        /// <summary>
        /// 3. Authorize via user token and receive final X token
        /// </summary>
        /// <param name="relying_party"></param>
        /// <returns></returns>
        private async Task<XSTSResponse> RequestXstsToken(string relying_party = "http://xboxlive.com")
        {
            const string base_address = "https://xsts.auth.xboxlive.com/xsts/authorize";

            var data = new
            {
                RelyingParty = relying_party,
                TokenType = "JWT",
                Properties = new
                {
                    UserTokens = new List<string>() { null },//UserToken.Token },
                    SandboxId = "RETAIL"
                }
            };

            string jsonData = JsonSerializer.Serialize(data);

            using (var clientSession = _httpClientFactory.CreateClient())
            {
                clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await clientSession.PostAsync(base_address, content);

                XSTSResponse result = await ConvertTo<XSTSResponse>(response);

                return result;
            }
        }

        private async Task<T> ConvertTo<T>(HttpResponseMessage httpResponse) //where T: OAuth2TokenResponse, XAUResponse, XSTSResponse
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

            return result;
        }
    }

    public class OAuth2TokenResponse
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("authentication_token")]
        public string? AuthenticationToken { get; set; }

        [JsonPropertyName("issued")]
        public DateTime? Issued { get; set; }

        [JsonPropertyName("expires")]
        public DateTime? Expires { get; set; }
    }

    public class XTokenResponse
    {
        [JsonPropertyName("IssueInstant")]
        public DateTime IssueInstant { get; set; }

        [JsonPropertyName("NotAfter")]
        public DateTime NotAfter { get; set; }

        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }

    public class XAUResponse : XTokenResponse
    {
        [JsonPropertyName("DisplayClaims")]
        public XAUDisplayClaims DisplayClaims { get; set; }
    }

    public class XSTSResponse : XTokenResponse
    {
        [JsonPropertyName("DisplayClaims")]
        public XSTSDisplayClaims DisplayClaims { get; set; }
        public string Xuid { get { return DisplayClaims.Xui[0]["xid"]; } set { } }
        public string Userhash { get { return DisplayClaims.Xui[0]["uhs"]; } }
        public string Gamertag { get { return DisplayClaims.Xui[0]["gtg"]; } }
        public string AgeGroup { get { return DisplayClaims.Xui[0]["agg"]; } }
        public string Privileges { get { return DisplayClaims.Xui[0]["prv"]; } }
        public string UserPrivileges { get { return DisplayClaims.Xui[0]["usr"]; } }
        public string AuthorizationHeaderValue { get { return $"XBL3.0 x={Userhash};{Token}"; } }
    }

    public class XAUDisplayClaims
    {
        [JsonPropertyName("xui")]
        public List<Dictionary<string, string>> Xui { get; set; }
    }

    public class XSTSDisplayClaims
    {
        [JsonPropertyName("xui")]
        public List<Dictionary<string, string>> Xui { get; set; }
    }
}
