using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.Infrastructure.Services.Models;

namespace XblApp.Infrastructure.Data.Seeding
{
    public abstract class AbstractJsonLoader
    {
        internal static string GetJsonFilePath(string fileDir, string searchPattern)
        {
            var fileList = Directory.GetFiles(fileDir, searchPattern);

            if (fileList.Length == 0)
                throw new FileNotFoundException(
                    $"Could not find a file with the search name of {searchPattern} in directory {fileDir}");

            //If there are many then we take the most recent
            return fileList.ToList().OrderBy(x => x).Last();
        }
    }

    public class GamerJsonLoader : AbstractJsonLoader
    {
        public static IEnumerable<Gamer> LoadGamers(string fileDir, string fileSearchString)
        {
            var filePath = GetJsonFilePath(fileDir, fileSearchString);
            var jsonDecoded = JsonSerializer.Deserialize<GamerlJson>(File.ReadAllText(filePath));

            return jsonDecoded.ProfileUsers.Select(x => CreateGamers(x));
        }

        private static Gamer CreateGamers(ProfileUser profileUserJson)
        {
            var gamer = new Gamer
            {
                GamerId = long.Parse(profileUserJson.ProfileId),
                Gamertag = profileUserJson.Gamertag,
                Gamerscore = profileUserJson.Gamerscore,
                Location = profileUserJson.Location,
                Bio = profileUserJson.Bio                
            };

            return gamer;
        }
    }

    public  class GameJsonLoader : AbstractJsonLoader
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
                    GameId = gamerJsonId,
                    GameName = title.Name,
                    TotalGamerscore = title.Achievement.TotalGamerscore,
                    TotalAchievements = title.Achievement.TotalAchievements,
                };

                GamerGame gamerGame = new GamerGame()
                {
                    GameId = long.Parse(title.TitleId),
                    GamerId = gamerJsonId,
                    CurrentAchievements = title.Achievement.CurrentAchievements,
                    CurrentGamerscore = title.Achievement.CurrentGamerscore,
                    Game = game
                };


                //game.GamerLinks = new List<GamerGame>();

                //game.GamerLinks.Add(new GamerGame
                //{
                //    GameId = long.Parse(title.TitleId),
                //    GamerId = gamerJsonId,
                //    CurrentAchievements = title.Achievement.CurrentAchievements,
                //    CurrentGamerscore = title.Achievement.CurrentGamerscore,
                //    Game = game
                //});

                games.Add(game);
            }

            return games;
        }
    }
}
