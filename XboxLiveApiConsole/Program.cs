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
                AuthenticationLow authLow = new AuthenticationLow(session);

                AuthenticationHigh authHigh = new AuthenticationHigh(authLow, new StorageLocal());
                
                XboxLiveClient xblClient = new XboxLiveClient(authLow);

                await authHigh.ReadTokens();

                TitleHubResponse titleHub = await xblClient.tittleHubProvider.GetTitleHistory(xblClient.Xuid);
                //await authLogic.SaveToken(titleHub);

                ProfileResponse profile = await xblClient.profileProvider.GetProfileByXuid(xblClient.Xuid);
                //await authLogic.SaveToken(profile);
            }
        }
    }
}