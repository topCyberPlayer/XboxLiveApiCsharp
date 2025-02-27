using Microsoft.AspNetCore.WebUtilities;
using XblApp.Domain.Interfaces;
using XblApp.DTO.JsonModels;

namespace XblApp.XboxLiveService
{
    public class AchievementService : BaseService, IXboxLiveAchievementService
    {
        public AchievementService(IHttpClientFactory factory) : base(factory) { }

        public async Task<List<Domain.Entities.Achievement>> GetAchievements(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";

            var result = await GetAchievementBaseAsync<AchievementJson2>(relativeUrl);

            return MapToAchievements(result);
        }

        public async Task<List<Domain.Entities.Achievement>> GetAchievementsX1Gameprogress(long xuid, long titleId)
        {
            string relativeUrl = $"users/xuid({xuid})/achievements";

            Dictionary<string, string> queryParams = new()
            {
                { "titleId", titleId.ToString() },
            };

            string? uri = QueryHelpers.AddQueryString(relativeUrl, queryParams);

            AchievementJson2? result = await GetAchievementBaseAsync<AchievementJson2>(uri);

            return MapToAchievements(result);
        }

        /// <summary>
        /// Возвращает только 32 элемента
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public async Task<List<Domain.Entities.Achievement>> GetAchievementsXboxoneRecentProgressAndInfoAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/history/titles";

            var result = await GetAchievementBaseAsync<AchievementJson>(relativeUrl);

            return MapToAchievements(result);
        }

        private async Task<T> GetAchievementBaseAsync<T>(string relativeUrl)
        {
            try
            {
                HttpClient client = factory.CreateClient("AchievementService");

                T result = await SendRequestAsync<T>(client, relativeUrl);

                return result;
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

        private List<Domain.Entities.Achievement> MapToAchievements(AchievementJson2 achievements) =>
            achievements.Titles
            .Select(t => new Domain.Entities.Achievement
            {
                AchievementId = long.TryParse(t.Id, out long result) ? result : throw new FormatException($"Ошибка при парсинге {nameof(t.Id)}"),
                GameId = t.TitleAssociations[0].Id,
                Name = t.Name,
            })
            .ToList();
    }
}
