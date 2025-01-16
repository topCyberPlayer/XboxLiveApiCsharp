﻿using System.Net.Http.Headers;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;

namespace XblApp.XboxLiveService
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

        public async Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag, string authorizationHeaderValue, int maxItems)
        {
            string relativeUrl = $"/users/gt({gamertag})/titles/titlehistory";

            return await GetGamesForGamerBaseAsync(relativeUrl, authorizationHeaderValue, maxItems);
        }

        public async Task<List<Game>> GetGamesForGamerProfileAsync(long xuid, string authorizationHeaderValue, int maxItems)
        {
            string relativeUrl = $"/users/xuid({xuid})/titles/titlehistory";

            return await GetGamesForGamerBaseAsync(relativeUrl, authorizationHeaderValue, maxItems);
        }

        private async Task<List<Game>> GetGamesForGamerBaseAsync(string relativeUrl, string authorizationHeaderValue, int maxItems)
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

            return MapToGame(result);
        }

        public static List<Game> MapToGame(GameJson gameJson)
        {
            return gameJson.Titles
                .Select(title => new Game
                {
                    GameId = long.TryParse(title.TitleId, out var gameId) ? gameId : throw new FormatException($"Invalid TitleId format for Game: {title.Name}"),
                    GameName = title.Name ?? throw new ArgumentException("Game name cannot be null."),
                    TotalAchievements = title.Achievement?.TotalAchievements ?? 0,
                    TotalGamerscore = title.Achievement?.TotalGamerscore ?? 0,
                    GamerLinks = new List<GamerGame>()
                    {
                        new GamerGame
                        {
                            GamerId = long.TryParse(gameJson.Xuid., out var gamerId) ? gamerId : throw new FormatException($"Invalid GamerId format for Game: {title.Name}"),
                            CurrentAchievements = title.Achievement.CurrentAchievements,
                            CurrentGamerscore = title.Achievement.CurrentGamerscore
                        }
                    }
                })
                .ToList();
        }
    }
}
