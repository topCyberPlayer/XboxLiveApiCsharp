using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using XblApp.Domain.Interfaces;
using XblApp.DTO.JsonModels;

namespace XblApp.XboxLiveService
{
    public class AchievementService : BaseService, IXboxLiveAchievementService
    {
        public AchievementService(IHttpClientFactory factory) : base(factory) { }

        public async Task<List<Domain.Entities.Achievement>> GetAchievementsAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";

            AchievementJson2? result = await GetAchievementBaseAsync(relativeUrl);

            return MapToAchievements(result);
        }

        public async Task<List<Domain.Entities.Achievement>> GetAchievementsAsync(long xuid, long titleId)
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


        //public async Task<List<Domain.Entities.Achievement>> GetAchievementsX1RecentProgressAsync(long xuid)
        //{
        //    string relativeUrl = $"/users/xuid({xuid})/history/titles";

        //    HttpClient client = factory.CreateClient("AchievementService");

        //    AchievementJson result = await SendPaginatedRequestAsync<AchievementJson, TitleA>(client, relativeUrl);

        //    return MapToAchievements(result);
        //}

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
            AchievementJson2 result = new() { Titles = new List<TitleB>() }; // Создаем объект сразу
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
                    ((List<TitleB>)result.Titles!).AddRange(collJsonAchivos.Titles); // Добавляем напрямую

                // Получаем continuationToken для следующего запроса
                continuationToken = collJsonAchivos.PagingInfos.ContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken)); // Повторяем, пока есть токен

            return result;
        }

        private List<Domain.Entities.Achievement> MapToAchievements(AchievementJson2 achievements) =>
            achievements.Titles
            .Select(t => new Domain.Entities.Achievement
            {
                AchievementId = long.TryParse(t.TitleId, out long outAchievementId) ? outAchievementId : -1,
                GameId = t.TitleAssociations[0].Id,
                Name = t.Name,
                Description = t.Description,
                LockedDescription = t.LockedDescription,
                Gamerscore = int.TryParse(t.Rewards[0].Value, out int outGamerscore) ? outGamerscore : - 1,
                IsSecret = t.IsSecret,
            })
            .ToList();
    }
}
