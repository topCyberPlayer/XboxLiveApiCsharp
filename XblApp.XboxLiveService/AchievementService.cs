using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService
{
    public class AchievementService : BaseService, IXboxLiveAchievementService
    {
        public AchievementService(IHttpClientFactory factory) : base(factory) { }

        public async Task<AchievementJson> GetAchievementsAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";

            AchievementJson? result = await GetAchievementBaseAsync(relativeUrl, xuid);

            return result;
        }

        public async Task<AchievementJson> GetAchievementsAsync(long xuid, long titleId)
        {
            string relativeUrl = $"users/xuid({xuid})/achievements";

            Dictionary<string, string> queryParams = new()
            {
                { "titleId", titleId.ToString() },
            };

            string? uri = QueryHelpers.AddQueryString(relativeUrl, queryParams);

            AchievementJson? result = await GetAchievementBaseAsync(uri, xuid);

            return result;
        }


        //public async Task<List<Domain.Entities.Achievement>> GetAchievementsX1RecentProgressAsync(long xuid)
        //{
        //    string relativeUrl = $"/users/xuid({xuid})/history/titles";

        //    HttpClient client = factory.CreateClient("AchievementService");

        //    AchievementJson result = await SendPaginatedRequestAsync<AchievementJson, TitleA>(client, relativeUrl);

        //    return MapToAchievements(result);
        //}

        private async Task<AchievementJson> GetAchievementBaseAsync(string relativeUrl, long xuid)
        {
            HttpClient client = factory.CreateClient("AchievementService");

            AchievementJson result = await SendPaginatedRequestAsync(client, relativeUrl);
            result.Xuid = xuid;

            return result;
        }

        private async Task<AchievementJson> SendPaginatedRequestAsync(HttpClient client, string baseUri)
        {
            string? continuationToken = default;
            AchievementJson result = new() { Achievements = new List<AchievementInnerJson>() }; // Создаем объект сразу
            do
            {
                // Формируем URL с continuationToken, если он есть
                string requestUri = string.IsNullOrEmpty(continuationToken)
                    ? baseUri
                    : $"{baseUri}&continuationToken={continuationToken}";

                using HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                AchievementJson? collJsonAchivos = await response.Content.ReadFromJsonAsync<AchievementJson>()
                    ?? throw new InvalidOperationException($"Failed to deserialize response content to type {typeof(AchievementJson).Name}.");

                if (collJsonAchivos.Achievements != null)
                    ((List<AchievementInnerJson>)result.Achievements!).AddRange(collJsonAchivos.Achievements); // Добавляем напрямую

                // Получаем continuationToken для следующего запроса
                continuationToken = collJsonAchivos.PagingInfos.ContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken)); // Повторяем, пока есть токен

            return result;
        }
    }
}
