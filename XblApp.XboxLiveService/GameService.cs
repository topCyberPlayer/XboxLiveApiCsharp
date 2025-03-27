using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.DTO.JsonModels;

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
            , TitleHubSettings.DETAIL
            //,TitleHubSettings.FRIENDS_WHO_PLAYED
            //,TitleHubSettings.PRODUCT_ID
        );

        public GameService(IHttpClientFactory factory) : base(factory) { }

        public async Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag)
        {
            string relativeUrl = $"/users/gt({gamertag})/titles/titlehistory/decoration/{DefScopes}";

            return await GetGamesForGamerBaseAsync(relativeUrl);
        }

        public async Task<List<Game>> GetGamesForGamerProfileAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/titles/titlehistory/decoration/{DefScopes}";

            return await GetGamesForGamerBaseAsync(relativeUrl);
        }

        private async Task<List<Game>> GetGamesForGamerBaseAsync(string relativeUrl)
        {
            HttpClient client = factory.CreateClient("GameService");

            GameJson result = await SendRequestAsync<GameJson>(client, relativeUrl);

            return result.MapTo();
        }

        
    }
}
