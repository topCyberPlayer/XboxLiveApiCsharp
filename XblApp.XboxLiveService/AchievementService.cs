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

        public Task<List<Achievement>> GetAchievementsXbox360RecentProgressAndInfoAsync(long xuid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Achievement>> GetAchievementsXboxoneRecentProgressAndInfoAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/history/titles";

            return await GetAchievementBaseAsync(relativeUrl);
        }

        private async Task<List<Achievement>> GetAchievementBaseAsync(string relativeUrl)
        {
            try
            {
                HttpClient client = factory.CreateClient("AchievementService");

                client.DefaultRequestHeaders.Add("MS-CV", "eK4nT8CkJzEpWF3j.2");

                AchievementJson result = await SendRequestAsync<AchievementJson>(client, relativeUrl);

                return MapToAchievements(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<Achievement> MapToAchievements(AchievementJson achievement) =>
            achievement.Titles
            .Select(t => new Achievement
            {
                AchievementId = t.TitleId,
                //DateUnlock = DateTimeOffset.Parse(t.LastUnlock)
            })
            .ToList();

    }
}
