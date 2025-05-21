using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService.XboxLiveServices
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

        public async Task<GameJson> GetGamesForGamerProfileAsync(string gamertag)
        {
            string relativeUrl = $"/users/gt({gamertag})/titles/titlehistory/decoration/{DefScopes}";

            GameJson result = await GetGamesForGamerBaseAsync(relativeUrl);

            return result;
        }

        public async Task<GameJson> GetGamesForGamerProfileAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/titles/titlehistory/decoration/{DefScopes}";

            GameJson result = await GetGamesForGamerBaseAsync(relativeUrl);

            return result;
        }

        private async Task<GameJson> GetGamesForGamerBaseAsync(string relativeUrl)
        {
            HttpClient client = factory.CreateClient("GameService");

            GameJson result = await SendRequestAsync<GameJson>(client, relativeUrl);

            return result;
        }
    }
}
