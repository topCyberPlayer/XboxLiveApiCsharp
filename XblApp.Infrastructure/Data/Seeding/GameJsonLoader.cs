using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.Infrastructure.XboxLiveServices.Models;

namespace XblApp.Infrastructure.Data.Seeding
{
    public class GameJsonLoader : AbstractJsonLoader
    {
        public static IEnumerable<Game> LoadGames(string fileDir, string fileSearchString)
        {
            var filePath = GetJsonFilePath(fileDir, fileSearchString);
            GameJson? jsonDecoded = JsonSerializer.Deserialize<GameJson>(File.ReadAllText(filePath));

            return CreateGame(jsonDecoded);
        }

        private static IEnumerable<Game> CreateGame(GameJson gameJson)
        {
            long gamerJsonId = long.Parse(gameJson.Xuid);
            
            List<Game> games = new List<Game>();

            foreach (Title? title in gameJson.Titles)
            {               
                Game game = new Game
                {
                    GameId = long.Parse(title.TitleId),
                    GameName = title.Name,
                    TotalGamerscore = title.Achievement.TotalGamerscore,
                    TotalAchievements = title.Achievement.TotalAchievements,
                    GamerLinks = new List<GamerGame>()
                    {
                        new GamerGame()
                        {
                            GamerId = gamerJsonId,
                            CurrentAchievements = title.Achievement.CurrentAchievements,
                            CurrentGamerscore = title.Achievement.CurrentGamerscore,
                        }
                    }
                };

                games.Add(game);
            }

            return games;
        }
    }
}
