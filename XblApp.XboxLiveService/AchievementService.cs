using XblApp.Domain.Interfaces;
using XblApp.DTO.JsonModels;

namespace XblApp.XboxLiveService
{
    public class AchievementService : BaseService, IXboxLiveAchievementService
    {
        public AchievementService(IHttpClientFactory factory) : base(factory)
        {
        }

        public async Task<List<Domain.Entities.Achievement>> GetAchievements(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";

            return await GetAchievementBaseAsync2(relativeUrl);
        }

        /// <summary>
        /// Возвращает только 32 элемента
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        private async Task<List<Domain.Entities.Achievement>> GetAchievementBaseAsync2(string relativeUrl)
        {
            try
            {
                HttpClient client = factory.CreateClient("AchievementService");

                AchievementJson2 result = await SendRequestAsync<AchievementJson2>(client, relativeUrl);

                return MapToAchievements2(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Domain.Entities.Achievement>> GetAchievementsXbox360All(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/titleachievements";

            return await GetAchievementBaseAsync(relativeUrl);
        }

        public Task<List<Domain.Entities.Achievement>> GetAchievementsXbox360All(string gamertag)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domain.Entities.Achievement>> GetAchievementsXbox360RecentProgressAndInfoAsync(long xuid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Domain.Entities.Achievement>> GetAchievementsXboxoneRecentProgressAndInfoAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/history/titles";

            return await GetAchievementBaseAsync(relativeUrl);
        }

        private async Task<List<Domain.Entities.Achievement>> GetAchievementBaseAsync(string relativeUrl)
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

        private List<Domain.Entities.Achievement> MapToAchievements(AchievementJson achievement) =>
            achievement.Titles
            .Select(t => new Domain.Entities.Achievement
            {
                AchievementId = t.TitleId,
                //DateUnlock = DateTimeOffset.Parse(t.LastUnlock)
            })
            .ToList();

        private List<Domain.Entities.Achievement> MapToAchievements2(AchievementJson2 achievements) =>
            achievements.Titles
            .Select(t => new Domain.Entities.Achievement
            {
                AchievementId = long.TryParse(t.Id, out long result) ? result : throw new FormatException("sd"),
                GameId = long.TryParse(t.ProductId, out long productId) ? productId : throw new FormatException("sd"),

            })
            .ToList();

    }
}
