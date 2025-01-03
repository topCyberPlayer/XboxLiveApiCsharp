﻿using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.XboxLiveServices
{
    public class GameService : BaseService, IXboxLiveGameService
    {
        private string TitleHubSettings_SCOPES => string.Join(",",
                    TitleHubSettings.ACHIEVEMENT,
                    TitleHubSettings.ALTERNATE_TITLE_ID,
                    TitleHubSettings.CONTENT_BOARD,
                    TitleHubSettings.SERVICE_CONFIG_ID,
                    TitleHubSettings.STATS,
                    TitleHubSettings.GAME_PASS,
                    TitleHubSettings.IMAGE,
                    TitleHubSettings.DETAIL,
                    TitleHubSettings.FRIENDS_WHO_PLAYED,
                    TitleHubSettings.PRODUCT_ID);

        public GameService(IHttpClientFactory factory) : base(factory) { }

        public async Task<GameDTO> GetTitleHistoryAsync(string gamertag, string authorizationHeaderValue, int maxItems)
        {
            string relativeUrl = $"/users/gt({gamertag})/titles/titlehistory";

            return await GetTitleHistoryBaseAsync(relativeUrl, authorizationHeaderValue, maxItems);
        }

        public async Task<GameDTO> GetTitleHistoryAsync(long xuid, string authorizationHeaderValue, int maxItems)
        {
            string relativeUrl = $"/users/xuid({xuid})/titles/titlehistory";

            return await GetTitleHistoryBaseAsync(relativeUrl, authorizationHeaderValue, maxItems);
        }

        private async Task<GameDTO> GetTitleHistoryBaseAsync(string relativeUrl, string authorizationHeaderValue, int maxItems)
        {
            HttpClient client = factory.CreateClient("gameService");

            IDictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "decoration", TitleHubSettings_SCOPES },
                { "maxItems", maxItems.ToString() },
            };

            string? uri = QueryHelpers.AddQueryString(relativeUrl, queryParams);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", authorizationHeaderValue);

            HttpResponseMessage response = await client.GetAsync(uri);

            GameJson result = await DeserializeJson<GameJson>(response);

            return MapToGameDTO(result);
        }

        public static GameDTO MapToGameDTO(GameJson result)
        {
            return new GameDTO
            {
                GamerId = long.Parse(result.Xuid),
                Titles = result.Titles.Select(t => new TitleDTO
                {
                    TitleId = long.Parse(t.TitleId),
                    ProductId = t.ProductId,
                    ProductIds = t.ProductIds,
                    GameName = t.Name,
                    Type = t.Type,
                    DisplayImage = t.DisplayImage,
                    IsBundle = t.IsBundle,
                    Achievement = new AchievementDTO
                    {
                        CurrentAchievements = t.Achievement.CurrentAchievements,
                        CurrentGamerscore = t.Achievement.CurrentGamerscore,
                        TotalAchievements = t.Achievement.TotalAchievements,
                        TotalGamerscore = t.Achievement.TotalGamerscore,
                        ProgressPercentage = t.Achievement.ProgressPercentage,
                    },
                    Stats = new StatsDTO
                    {
                        
                    }
                    // Map other necessary properties
                }).ToList()
            };
        }

    }
}
