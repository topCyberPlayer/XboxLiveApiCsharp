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
            //Я запращиваю сперва профиль игрока, затем его игры, значит игрок будет всегда существовать в БД.
            var gamerJsonId = long.Parse(gameJson.Xuid);
            //Gamer? gamer = context.Gamers.FirstOrDefault(x => x.GamerId == gamerJsonId);
            List<Game> games = new List<Game>();

            foreach (Title? title in gameJson.Titles)
            {               
                Game game = new Game
                {
                    GameId = long.Parse(title.TitleId),
                    GameName = title.Name,
                    TotalGamerscore = title.Achievement.TotalGamerscore,
                    TotalAchievements = title.Achievement.TotalAchievements,
                };

                //GamerGame gamerGame = new GamerGame()
                //{
                //    //GameId = long.Parse(title.TitleId),
                //    GamerId = gamerJsonId,
                //    CurrentAchievements = title.Achievement.CurrentAchievements,
                //    CurrentGamerscore = title.Achievement.CurrentGamerscore,
                //    Game = game
                //};


                game.GamerLinks = new List<GamerGame>();

                game.GamerLinks.Add(new GamerGame
                {
                    GameId = long.Parse(title.TitleId),
                    GamerId = gamerJsonId,
                    CurrentAchievements = title.Achievement.CurrentAchievements,
                    CurrentGamerscore = title.Achievement.CurrentGamerscore,
                    //Game = game
                });

                games.Add(game);
            }

            return games;
        }
    }
}
