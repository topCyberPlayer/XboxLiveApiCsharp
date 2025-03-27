using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.DTO.JsonModels;

namespace XblApp.XboxLiveService
{
    public class AchievementService : BaseService, IXboxLiveAchievementService
    {
        public AchievementService(IHttpClientFactory factory) : base(factory) { }

        public async Task<List<Achievement>> GetAchievementsAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";

            AchievementJson? result = await GetAchievementBaseAsync(relativeUrl);
            
            return result.MapTo();
        }

        public async Task<(List<Achievement>,List<GamerAchievement>)> GetAchievementsAsync(long xuid, long titleId)
        {
            string relativeUrl = $"users/xuid({xuid})/achievements";

            Dictionary<string, string> queryParams = new()
            {
                { "titleId", titleId.ToString() },
            };

            string? uri = QueryHelpers.AddQueryString(relativeUrl, queryParams);

            AchievementJson? achievementJson = await GetAchievementBaseAsync(uri);

            List<Achievement> achievementColl = achievementJson.MapTo();
            List<GamerAchievement> gamerAchievementColl = achievementJson.MapTo(xuid);

            return (achievementColl, gamerAchievementColl);
        }


        //public async Task<List<Domain.Entities.Achievement>> GetAchievementsX1RecentProgressAsync(long xuid)
        //{
        //    string relativeUrl = $"/users/xuid({xuid})/history/titles";

        //    HttpClient client = factory.CreateClient("AchievementService");

        //    AchievementJson result = await SendPaginatedRequestAsync<AchievementJson, TitleA>(client, relativeUrl);

        //    return MapToAchievements(result);
        //}

        private async Task<AchievementJson> GetAchievementBaseAsync(string relativeUrl)
        {
            try
            {
                HttpClient client = factory.CreateClient("AchievementService");

                AchievementJson result = await SendPaginatedRequestAsync(client, relativeUrl);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AchievementJson> SendPaginatedRequestAsync(HttpClient client, string baseUri)
        {
            string? continuationToken = default;
            AchievementJson result = new() { Titles = new List<TitleB>() }; // Создаем объект сразу
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

                if (collJsonAchivos.Titles != null)
                    ((List<TitleB>)result.Titles!).AddRange(collJsonAchivos.Titles); // Добавляем напрямую

                // Получаем continuationToken для следующего запроса
                continuationToken = collJsonAchivos.PagingInfos.ContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken)); // Повторяем, пока есть токен

            return result;
        }

        
    }
}
