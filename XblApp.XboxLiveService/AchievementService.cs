using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
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

            var result = await GetAchievementBaseAsync(relativeUrl);

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

            AchievementJson2? result = await GetAchievementBaseAsync(uri);

            return MapToAchievements(result);
        }

        /// <summary>
        /// Возвращает только 32 элемента
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public async Task<List<Domain.Entities.Achievement>> GetAchievementsX1RecentProgressAndInfoAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/history/titles";

            var result = await GetAchievementBaseAsync(relativeUrl);

            return MapToAchievements(result);
        }

        private async Task<AchievementJson2> GetAchievementBaseAsync(string relativeUrl)
        {
            try
            {
                HttpClient client = factory.CreateClient("AchievementService");

                AchievementJson2 result = await SendPaginatedRequestAsync(client, relativeUrl);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AchievementJson2> SendPaginatedRequestAsync(HttpClient client, string baseUri)
        {
            string? continuationToken = default;
            List<TitleB> collAchivos = [];
            do
            {
                // Формируем URL с continuationToken, если он есть
                string requestUri = string.IsNullOrEmpty(continuationToken)
                    ? baseUri
                    : $"{baseUri}&continuationToken={continuationToken}";

                using HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                AchievementJson2? collJsonAchivos = await response.Content.ReadFromJsonAsync<AchievementJson2>()
                    ?? throw new InvalidOperationException($"Failed to deserialize response content to type {typeof(AchievementJson2).Name}.");
                
                if (collJsonAchivos.Titles != null)
                    collAchivos.AddRange(collJsonAchivos.Titles);

                // Получаем continuationToken для следующего запроса
                continuationToken = collJsonAchivos.PagingInfos.ContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken)); // Повторяем, пока есть токен

            AchievementJson2 result = new()
            {
                Titles = collAchivos
            };

            return result;
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
                AchievementId = long.TryParse(t.TitleId, out long result) ? result : throw new FormatException($"Ошибка при парсинге {nameof(t.TitleId)}"),
                GameId = t.TitleAssociations[0].Id,
                Name = t.Name,
            })
            .ToList();
    }
}
