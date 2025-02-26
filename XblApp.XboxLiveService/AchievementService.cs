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

        public async Task<List<Achievement>> GetAchievements(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";

            return await GetAchievementBaseAsync(relativeUrl);
        }

        public async Task<List<Achievement>> GetAchievementsXbox360All(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/titleachievements";

            return await GetAchievementBaseAsync(relativeUrl);
        }

        public Task<List<Achievement>> GetAchievementsXbox360All(string gamertag)
        {
            throw new NotImplementedException();
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
