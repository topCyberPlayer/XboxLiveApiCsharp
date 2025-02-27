using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.DTO.JsonModels;

namespace XblApp.Database.Seeding
{
    public static class GameJsonLoader
    {
        public static IEnumerable<Game> LoadGames(string fileDir, string fileSearchString)
        {
            string filePath = AbstractJsonLoader.GetJsonFilePath(fileDir, fileSearchString);
            string jsonContent = File.ReadAllText(filePath);
            GameJson? jsonDecoded = JsonSerializer.Deserialize<GameJson>(jsonContent);

            return GameJson.MapToGame(jsonDecoded);//todo метод MapToGame перенести
        }
    }
}
