using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.Infrastructure.XboxLiveServices.Models;
using XblApp.XboxLiveService;

namespace XblApp.Database.Seeding
{
    public static class GamerJsonLoader
    {
        public static IEnumerable<Gamer> LoadGamers(string fileDir, string fileSearchString)
        {
            string filePath = AbstractJsonLoader.GetJsonFilePath(fileDir, fileSearchString);
            string jsonContent = File.ReadAllText(filePath);
            GamerJson jsonDecoded = JsonSerializer.Deserialize<GamerJson>(jsonContent);

            return GamerService.MapToGamer(jsonDecoded);
        }
    }
}
