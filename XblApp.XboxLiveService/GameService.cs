using Microsoft.AspNetCore.WebUtilities;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;

namespace XblApp.XboxLiveService
{
    public class GameService : BaseService, IXboxLiveGameService
    {
        private static readonly string DefScopes = string.Join(",",
            TitleHubSettings.ACHIEVEMENT
            //,TitleHubSettings.ALTERNATE_TITLE_ID
            //,TitleHubSettings.CONTENT_BOARD
            //,TitleHubSettings.SERVICE_CONFIG_ID
            //,TitleHubSettings.STATS
            //,TitleHubSettings.GAME_PASS
            //,TitleHubSettings.IMAGE
            //,TitleHubSettings.DETAIL
            //,TitleHubSettings.FRIENDS_WHO_PLAYED
            //,TitleHubSettings.PRODUCT_ID
        );

        public GameService(IHttpClientFactory factory) : base(factory) { }

        public async Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag, int maxItems)
        {
            string relativeUrl = $"/users/gt({gamertag})/titles/titlehistory/decoration/{DefScopes}";

            return await GetGamesForGamerBaseAsync(relativeUrl, maxItems);
        }

        public async Task<List<Game>> GetGamesForGamerProfileAsync(long xuid, int maxItems)
        {
            string relativeUrl = $"/users/xuid({xuid})/titles/titlehistory/decoration/{DefScopes}";

            return await GetGamesForGamerBaseAsync(relativeUrl, maxItems);
        }

        private async Task<List<Game>> GetGamesForGamerBaseAsync(string relativeUrl, int maxItems)
        {
            HttpClient client = factory.CreateClient("GameService");

            Dictionary<string, string> queryParams = new()
            {
                { "maxItems", maxItems.ToString() },
            };

            //string? uri = QueryHelpers.AddQueryString(relativeUrl, queryParams);

            GameJson result = await SendRequestAsync<GameJson>(client, relativeUrl);

            List<Game> games = MapToGame(result);

            return games;
        }

        public static List<Game> MapToGame(GameJson gameJson) =>
            gameJson.Titles
                .Select(title => new Game
                {
                    GameId = long.TryParse(title.TitleId, out long gameId) ? gameId : throw new FormatException($"Invalid TitleId format for Game: {title.Name}"),
                    GameName = title.Name ?? throw new ArgumentException("Game name cannot be null."),
                    TotalAchievements = title.Achievement?.TotalAchievements ?? 0,
                    TotalGamerscore = title.Achievement?.TotalGamerscore ?? 0,
                    GamerLinks = new List<GamerGame>()
                    {
                        new GamerGame
                        {
                            GamerId = long.TryParse(gameJson.Xuid, out var gamerId) ? gamerId : throw new FormatException($"Invalid GamerId format for Game: {title.Name}"),
                            CurrentAchievements = title.Achievement.CurrentAchievements,
                            CurrentGamerscore = title.Achievement.CurrentGamerscore
                        }
                    }
                })
                .ToList();
    }
}
