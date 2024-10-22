using XblApp.Infrastructure.Data;
using XblApp.Infrastructure.Data.Repositories;
using XblApp.Infrastructure.XboxLiveServices;
using XblApp.Infrastructure.XboxLiveServices.Models;
using XblApp.Shared.DTOs;
using XblApp.Test;

namespace XblApp.UI.Test.Infrastructure
{

    public class GameRepositoryTest : BaseTestClass
    {
        private GameRepository _gr;
        private const string searchFile = "Games.json";

        public GameRepositoryTest()
        {
            
        }

        [Fact]
        public async void SaveGamesAsync_Test()
        {
            GameJson? resultJson = GetXJson<GameJson>(searchFile);
            GameDTO resultDto = GameService.MapToGameDTO(resultJson);

            using (var context = new MsSqlDbContext(_config))
            {
                GameRepository gr = new GameRepository(context);
                await _gr.SaveGamesAsync(resultDto);
            }    
        }
    }
}
