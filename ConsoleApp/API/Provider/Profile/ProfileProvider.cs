using ConsoleApp.Authentication;
using System.Collections.Specialized;
using System.Web;

namespace ConsoleApp.API.Provider.Profile
{
    internal class ProfileProvider : BaseProvider
    {
        private string DEF_SCOPES
        {
            get
            {
                return string.Join(",",
                    ProfileSettings.ACCOUNT_TIER,
                    ProfileSettings.APP_DISPLAY_NAME,
                    ProfileSettings.APP_DISPLAYPIC_RAW,
                    ProfileSettings.BIOGRAPHY,
                    ProfileSettings.GAME_DISPLAYPIC_RAW,
                    ProfileSettings.GAME_DISPLAY_NAME,
                    ProfileSettings.GAMERSCORE,
                    ProfileSettings.GAMERTAG,
                    ProfileSettings.PUBLIC_GAMERPIC,
                    ProfileSettings.MODERN_GAMERTAG,
                    ProfileSettings.MODERN_GAMERTAG_SUFFIX,
                    ProfileSettings.PREFERRED_COLOR,
                    ProfileSettings.LOCATION,
                    ProfileSettings.REAL_NAME,
                    ProfileSettings.REAL_NAME_OVERRIDE,
                    ProfileSettings.IS_QUARANTINED,
                    ProfileSettings.TENURE_LEVEL,
                    ProfileSettings.SHOW_USER_AS_AVATAR,
                    ProfileSettings.UNIQUE_MODERN_GAMERTAG,
                    ProfileSettings.XBOX_ONE_REP,
                    ProfileSettings.WATERMARKS);
            }
        }
        private const string PROFILE_URL = "https://profile.xboxlive.com";

        public ProfileProvider(AuthenticationManager authMgr) : base(authMgr)
        {
        }

        public async Task<ProfileResponse> GetProfileByXuid(string xuid)
        {
            string baseAddress = PROFILE_URL + $"/users/xuid({xuid})/profile/settings";

            return await GetProfileBase(baseAddress);
        }

        public async Task<ProfileResponse> GetProfileByGamertag(string gamertag)
        {
            string baseAddress = PROFILE_URL + $"/users/gt({gamertag})/profile/settings";

            return await GetProfileBase(baseAddress);
        }

        private async Task<ProfileResponse> GetProfileBase(string baseAddress)
        {
            UriBuilder uriBuilder = new UriBuilder(baseAddress);

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["settings"] = DEF_SCOPES;
            uriBuilder.Query = query.ToString();

            _authMgr.clientSession.DefaultRequestHeaders.Clear();
            _authMgr.clientSession.DefaultRequestHeaders.Add("x-xbl-contract-version", "3");
            _authMgr.clientSession.DefaultRequestHeaders.Add("Authorization", _authMgr.XstsToken.AuthorizationHeaderValue);

            HttpResponseMessage response = await _authMgr.clientSession.GetAsync(uriBuilder.ToString());
            //string tmpResult = await response.Content.ReadAsStringAsync();
            ProfileResponse profileUser = await _authMgr.ConvertTo<ProfileResponse>(response);

            return profileUser;
        }
    }
}
