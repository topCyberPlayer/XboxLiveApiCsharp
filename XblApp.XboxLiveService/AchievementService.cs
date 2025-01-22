using System.Net.Http.Headers;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.XboxLiveService.Models;

namespace XblApp.XboxLiveService
{
    public class AchievementService : BaseService, IXboxLiveAchievementService
    {
        public AchievementService(IHttpClientFactory factory) : base(factory)
        {
        }

        public Task<List<Achievement>> GetAchievementsXbox360RecentProgressAndInfo(long xuid, string authorizationHeaderValue)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Achievement>> GetAchievementsXboxoneRecentProgressAndInfo(long xuid, string authorizationHeaderValue)
        {
            string relativeUrl = $"/users/xuid({xuid})/history/titles";

            return await GetAchievementBaseAsync(relativeUrl, authorizationHeaderValue);
        }

        private async Task<List<Achievement>> GetAchievementBaseAsync(string relativeUrl, string authorizationHeaderValue)
        {
            try
            {
                HttpClient client = factory.CreateClient("AchievementService");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("XBL3.0", authorizationHeaderValue);

                AchievementJson result = await SendRequestAsync<AchievementJson>(client, relativeUrl);

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
