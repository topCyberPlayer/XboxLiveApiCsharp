using ConsoleApp.API.Provider.Achievements;
using ConsoleApp.API.Provider.Profile;
using ConsoleApp.API.Provider.TittleHub;
using ConsoleApp.Authentication;

namespace ConsoleApp.API
{
    internal class XboxLiveClient
    {
        private AuthenticationLow _auth_mgr;

        public ProfileLow profileProvider;
        public AchievementsProvider achievementsProvider;
        public TittleHubLow tittleHubProvider;

        public XboxLiveClient(AuthenticationLow auth_mgr)
        {
            _auth_mgr = auth_mgr;

            profileProvider = new ProfileLow(auth_mgr);
            achievementsProvider = new AchievementsProvider(auth_mgr);
            tittleHubProvider = new TittleHubLow(auth_mgr);
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
