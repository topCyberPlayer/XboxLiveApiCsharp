
using WebApp.Models;

namespace WebApp.Services
{
    public class GameService : AuthenticationService
    {
        List<GameViewModel> _games = new List<GameViewModel>()
        {
            new GameViewModel()
            {
                Xuid = "1",
                TitleName = "gears+of+war",
                TitleId = "111",
                TotalAchievements = 57,
                TotalGamerscore = 1250,
                CurrentAchievements = 49,
                CurrentGamerscore = 900,
                Description = "Booo"

            },
            new GameViewModel()
            {
                Xuid = "1",
                TitleName = "bulletstorm",
                TitleId = "222",
                TotalAchievements = 50,
                TotalGamerscore = 1000,
                CurrentAchievements = 8,
                CurrentGamerscore = 125,
                Description = "Piu Pau"
            }
        };

        public GameService(IConfiguration configuration, HttpClient httpClient2, AuthenticationServiceXbl authServXbl, AuthenticationServiceDb authServDb) : base(httpClient2, authServXbl, authServDb)
        {
            _xServiceUrl = configuration["ConnectionStrings:TittleHubServiceUrl"];
        }

        public GameViewModel GetGame(string gameName)
        {
            //return base.GetBaseMethod<TittleHubViewModel>(userId, requestUri);

            return _games.Where(x => x.TitleName == gameName).FirstOrDefault();
        }

        /// <summary>
        /// Возвращает все игры связанные с Gamertag'ом по xuid
        /// </summary>
        /// <param name="xuid"></param>
        /// <returns></returns>
        public IEnumerable<GameViewModel> GetAllGames(string xuid)
        {
            //string requestUri = _xServiceUrl + $"/api/TitleHub/GetTitleHistory?xuid={xuid}&maxItems={maxItems}";

            return _games.Where(x => x.Xuid == xuid);
        }
    }
}
