using ConsoleApp.Authentication;
using System.Collections.Specialized;
using System.Web;

namespace ConsoleApp.API.Provider.TittleHub
{
    internal class TittleHubProvider : BaseProvider
    {
        private string TitleHubSettings_SCOPES
        {
            get
            {
                return string.Join(",",
                    TitleHubSettings.ACHIEVEMENT,
                    TitleHubSettings.ALTERNATE_TITLE_ID,
                    TitleHubSettings.CONTENT_BOARD,
                    TitleHubSettings.SERVICE_CONFIG_ID,
                    TitleHubSettings.STATS,
                    TitleHubSettings.GAME_PASS,
                    TitleHubSettings.IMAGE,
                    TitleHubSettings.DETAIL,
                    TitleHubSettings.FRIENDS_WHO_PLAYED,
                    TitleHubSettings.PRODUCT_ID
                    );
            }
        }
        private const string TITLEHUB_URL = "https://titlehub.xboxlive.com";

        public TittleHubProvider(AuthenticationManager authMgr) : base(authMgr) { }

        public async Task<TitleHubResponse> GetTitleHistory(string xuid, int maxItems = 5)
        {
            string baseAddress = TITLEHUB_URL + $"/users/xuid({xuid})/titles/titlehistory/decoration/{TitleHubSettings_SCOPES}";

            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["maxItems"] = maxItems.ToString();
            uriBuilder.Query = query.ToString();

            _authMgr.clientSession.DefaultRequestHeaders.Clear();
            _authMgr.clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "2");
            _authMgr.clientSession.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            _authMgr.clientSession.DefaultRequestHeaders.Add("Authorization", _authMgr.XstsToken.AuthorizationHeaderValue);

            HttpResponseMessage response = await _authMgr.clientSession.GetAsync(uriBuilder.ToString());
            //string tmpResult = await response.Content.ReadAsStringAsync();
            TitleHubResponse profileUser = await _authMgr.ConvertTo<TitleHubResponse>(response);

            return profileUser;
        }
    }

    
}
