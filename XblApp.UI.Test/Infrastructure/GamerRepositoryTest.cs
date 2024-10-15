using Microsoft.EntityFrameworkCore;
using XblApp.Infrastructure.Data;
using XblApp.Shared.DTOs;
using XblApp.Test;

namespace XblApp.UI.Test.Infrastructure
{
    public class GamerRepositoryTest : BaseTestClass
    {
        [Fact]
        public async Task TestGetGamesForGamerAsync()
        {
            //using (var context = new MsSqlDbContext(_config))
            //{
            //    var games = await context.Gamers
            //        .Where(g => g.Gamertag == "RiotGran")
            //        .Select(g => new GamerGameDTO
            //        {
            //            GamerId = g.GamerId,
            //            Gamertag = g.Gamertag,
            //            Games = g.GameLinks.Select(gg => new GameDTO
            //            {
            //                GameId = gg.Game.GameId,
            //                GameName = gg.Game.GameName,
            //                TotalAchievements = gg.Game.TotalAchievements,
            //                TotalGamerscore = gg.Game.TotalGamerscore,
            //                CurrentAchievements = gg.CurrentAchievements,
            //                CurrentGamerscore = gg.CurrentGamerscore,
            //            }).ToList()
            //        })
            //        .FirstOrDefaultAsync();
            //}
        }
    }
}
