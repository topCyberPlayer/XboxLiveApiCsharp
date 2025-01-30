using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.Infrastructure.XboxLiveServices.Models;
using XblApp.XboxLiveService;

namespace XblApp.Database.Seeding
{
    public static class GameJsonLoader
    {
        public static IEnumerable<Game> LoadGames(string fileDir, string fileSearchString)
        {
            string filePath = AbstractJsonLoader.GetJsonFilePath(fileDir, fileSearchString);
            string jsonContent = File.ReadAllText(filePath);
            GameJson? jsonDecoded = JsonSerializer.Deserialize<GameJson>(jsonContent);

            return GameService.MapToGame(jsonDecoded);
        }
    }
}
