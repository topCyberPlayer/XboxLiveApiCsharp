using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Domain.Entities;
using XblApp.DTO;

namespace XblApp.Database.Test.UseRealDatabase
{
    public class AchievementRepositoryTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        /// <summary>
        /// Методы в этом классе в основном используются для ЧТЕНИЯ из БД
        /// </summary>
        /// <param name="factory"></param>
        public AchievementRepositoryTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => { });
            });
        }

        [Theory]
        [InlineData("DraftChimera239")]
        public async Task SaveAchievementsAsync(string gamertag)
        {
            // Arrange
            IServiceScope scope = _factory.Services.CreateScope();
            XblAppDbContext context = scope.ServiceProvider.GetRequiredService<XblAppDbContext>();

            // Используем тот же _context, что и в приложении
            AchievementRepository achievementRepository = new(context);

            List<GamerAchievement> result = await achievementRepository.GetGamerAchievementsAsync(gamertag);

            GamerGameAchievementDTO gamerGameAchievement = GamerGameAchievementDTO.CastTo(result);

            //List<GamerGameAchievementDTO> a = result.Select(a => new GamerGameAchievementDTO()
            //{
            //    GamerId = a.GamerId,
            //    //Gamertag = a.GamerLink.Gamertag,
            //    GameAchievements = new List<GameAchievementDTO2>()
            //    {
            //        new GameAchievementDTO2()
            //        {
            //            GameId = a.GameId,
            //            GameName = a.GameLink.GameName,
            //            Achievements = new List<GamerAchievementInnerDTO>
            //            {
            //                new GamerAchievementInnerDTO()
            //                {
            //                    Name = a.AchievementLink.Name,
            //                    Score = a.AchievementLink.Gamerscore,
            //                    Description = a.AchievementLink.Description,
            //                    IsUnlocked = a.IsUnlocked
            //                }
            //            }
            //        }
            //    }
            //}).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
