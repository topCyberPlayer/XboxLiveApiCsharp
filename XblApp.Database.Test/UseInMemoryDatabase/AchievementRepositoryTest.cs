using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Database.Seeding;
using XblApp.Domain.Entities;
using XblApp.DTO.JsonModels;

namespace XblApp.Database.Test.UseInMemoryDatabase
{
    public class AchievementRepositoryTest
    {
        private enum EnumGamerProfiles : long
        {
            HnS_top = 2533274912896954
           ,
        }

        private readonly DbContextOptions<XblAppDbContext> _options;

        public AchievementRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<XblAppDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        private XblAppDbContext CreateContext()
        {
            return new XblAppDbContext(_options);
        }

        /// <summary>
        /// Проверка логики сохранения достижений из json-файла
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SaveAchievementsAsync_ReadJson()
        {
            AchievementJson? achievementJson = JsonLoader<AchievementJson>.LoadJsonFile("../../../../", "Achievements.json");
            List<Achievement?> achievements = achievementJson.MapTo();

            using (var context = CreateContext())
            {
                AchievementRepository? achievementRepository = new(context);

                await achievementRepository.SaveOrUpdateAchievementsAsync(achievements);
            }

            using (var context = CreateContext())
            {
                AchievementRepository? achievementRepository = new(context);
                List<Achievement?> result = await achievementRepository.GetAllAchievementsAsync();

                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
        }

        [Theory]
        [InlineData((long)EnumGamerProfiles.HnS_top)]
        public async Task SaveGamerAchievementsAsync_ReadJson(long xuid)
        {
            AchievementJson gamerAchievementJson = JsonLoader<AchievementJson>.LoadJsonFile("../../../../", "Achievements.json");
            List<GamerAchievement?> gamerAchievements = gamerAchievementJson.MapTo(xuid);

            using (var context = CreateContext())
            {
                AchievementRepository? achievementRepository = new(context);

                await achievementRepository.SaveOrUpdateGamerAchievementsAsync(gamerAchievements);
            }

            using (var context = CreateContext())
            {
                AchievementRepository? achievementRepository = new(context);
                List<GamerAchievement> result = await achievementRepository.GetGamerAchievementsAsync(xuid);

                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
        }
    }
}
