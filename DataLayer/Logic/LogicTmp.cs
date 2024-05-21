using DataLayer.Models;
using ServiceLayer.Models;

namespace DataLayer.Logic
{
    public class LogicTmp : IStorage
    {
        // Примеры данных для геймеров
        private static readonly List<GamerModelDb> _gamers =
        [
            new GamerModelDb
            {
                GamerId = 100,
                Gamertag = "GamerOne",
                Gamerscore = 1500,
                Bio = "Loves FPS games",
                Location = "USA"
            },
            new GamerModelDb
            {
                GamerId = 200,
                Gamertag = "GamerTwo",
                Gamerscore = 3000,
                Bio = "Adventure games enthusiast",
                Location = "Canada"
            }
        ];

        // Примеры данных для игр
        private static readonly List<GameModelDb> _games =
        [
            new GameModelDb
            {
                GameId = 1000,
                GameName = "Game One",
                TotalAchievements = 50,
                TotalGamerscore = 1000
            },
            new GameModelDb
            {
                GameId = 2000,
                GameName = "Game Two",
                TotalAchievements = 75,
                TotalGamerscore = 1500
            }
        ];

        // Примеры данных для связей между геймерами и играми
        private static readonly List<GamerGameModelDb> _gamerGameLinks =
        [
            new GamerGameModelDb
            {
                GamerId = 100,
                GameId = 1000,
                CurrentAchievements = 30,
                CurrentGamerscore = 600,
                //Gamer = _gamers.First(g => g.Gamertag == "GamerOne"), //_gamers[0],
                //Game = _games.First(g => g.GameName == "Game One")//_games[0]
            },
            new GamerGameModelDb
            {
                GamerId = 100,
                GameId = 2000,
                CurrentAchievements = 5,
                CurrentGamerscore = 20,
                //Gamer = _gamers.First(g => g.Gamertag == "GamerOne"),
                //Game = _games.First(g => g.GameName == "Game Two")
            },
            new GamerGameModelDb
            {
                GamerId = 200,
                GameId = 2000,
                CurrentAchievements = 40,
                CurrentGamerscore = 800,
                //Gamer = _gamers.First(g => g.Gamertag == "GamerTwo"),
                //Game = _games.First(g => g.GameName == "Game Two")
            }
        ];

        public LogicTmp()
        {
            // Создание словарей для быстрого доступа к геймерам и играм по их ID
            Dictionary<int, GamerModelDb> gamerDict = _gamers.ToDictionary(g => g.GamerId);
            Dictionary<int, GameModelDb> gameDict = _games.ToDictionary(g => g.GameId);
            
            // Установление связей между геймерами и играми
            foreach (GamerGameModelDb link in _gamerGameLinks)
            {
                var a1 = gamerDict.TryGetValue(link.GamerId, out GamerModelDb? gamer1);
                var b1 = gameDict.TryGetValue(link.GameId, out GameModelDb? game1);

                if (gamerDict.TryGetValue(link.GamerId, out GamerModelDb? gamer) 
                    && gameDict.TryGetValue(link.GameId, out GameModelDb? game))
                {
                    link.Gamer = gamer;
                    link.Game = game;
                    //gamer.GameLinks = new List<GamerGameModelDb>();
                    gamer.GameLinks.Add(link);
                }
            }
        }

        public GamerModelDto FindByGamertag(string gamertag)
        {
            IEnumerable<GamerModelDb> gamer = _gamers.Where(x => x.Gamertag.Equals(gamertag));

            IEnumerable<GamerModelDto> result = gamer.Select(gamer => new GamerModelDto
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Gamerscore = gamer.Gamerscore,
                CurrentGames = gamer.GameLinks.Count,
                CurrentAchievements = gamer.GameLinks.Sum(link => link.CurrentAchievements)
            });

            return result.First();
        }

        public IEnumerable<GamerModelDto> GetAllGamers()
        {
            IEnumerable<GamerModelDto> result = _gamers.Select(gamer => new GamerModelDto
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Gamerscore = gamer.Gamerscore,
                CurrentGames = gamer.GameLinks.Count,
                CurrentAchievements = gamer.GameLinks.Sum(link => link.CurrentAchievements)
            });

            return result;
        }
    }
}
