using ConsoleApp.API.Provider.Achievements;
using ConsoleApp.API.Provider.Profile;
using ConsoleApp.API.Provider.TittleHub;
using ConsoleApp.Authentication;

namespace ConsoleApp.API
{
    internal class XboxLiveClient
    {
        private AuthenticationManager _auth_mgr;

        public ProfileProvider profileProvider;
        public AchievementsProvider achievementsProvider;
        public TittleHubProvider tittleHubProvider;

        public XboxLiveClient(AuthenticationManager auth_mgr)
        {
            _auth_mgr = auth_mgr;

            profileProvider = new ProfileProvider(auth_mgr);
            achievementsProvider = new AchievementsProvider(auth_mgr);
            tittleHubProvider = new TittleHubProvider(auth_mgr);
        }

        public string Xuid
        {
            get
            {
                return _auth_mgr.XstsToken.Xuid;
            }
        }
    }
}
