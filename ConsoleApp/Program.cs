using ConsoleApp.API;
using ConsoleApp.API.Provider.Profile;
using ConsoleApp.API.Provider.TittleHub;
using ConsoleApp.Authentication;
using ConsoleApp.Storages;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient session = new HttpClient())
            {
                AuthenticationManager authMgr = new AuthenticationManager(session);

                AuthenticationLogic authLogic = new AuthenticationLogic(authMgr, new StorageLocal());
                
                XboxLiveClient xblClient = new XboxLiveClient(authMgr);

                await authLogic.Start();

                TitleHubResponse titleHub = await xblClient.tittleHubProvider.GetTitleHistory(xblClient.Xuid);
                //await authLogic.SaveToken(titleHub);

                ProfileResponse profile = await xblClient.profileProvider.GetProfileByXuid(xblClient.Xuid);
                //await authLogic.SaveToken(profile);
            }
        }
    }
}