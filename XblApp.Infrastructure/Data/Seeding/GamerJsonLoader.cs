using System.Text.Json;
using XblApp.Domain.Entities;
using XblApp.Infrastructure.XboxLiveServices.Models;

namespace XblApp.Infrastructure.Data.Seeding
{

    public class GamerJsonLoader : AbstractJsonLoader
    {
        public static IEnumerable<Gamer> LoadGamers(string fileDir, string fileSearchString)
        {
            var filePath = GetJsonFilePath(fileDir, fileSearchString);
            var jsonDecoded = JsonSerializer.Deserialize<GamerJson>(File.ReadAllText(filePath));

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
}
