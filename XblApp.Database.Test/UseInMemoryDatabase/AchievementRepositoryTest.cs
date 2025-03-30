using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Database.Seeding;
using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

namespace XblApp.Database.Test.UseInMemoryDatabase
{
    public class AchievementRepositoryTest
    {
        private enum EnumGamerProfiles : long
        {
            HnS_top = 2533274912896954
           ,DraftChimera239 = 2535419791913541
        }

        private readonly DbContextOptions<XblAppDbContext> _options;
        private readonly bool _useInMemory;

        /// <summary>
        /// Этот класс используется в основном для ЗАПИСИ во временную БД
        /// </summary>
        public AchievementRepositoryTest()
        {
            _useInMemory = false; // false - для работы с реальной БД

            var optionsBuilder = new DbContextOptionsBuilder<XblAppDbContext>();

            if (_useInMemory)
            {
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
            }
            else
            {
                string connectionString = "Server=localhost;Database=RealXblAppDb;User Id=sa;Password=YourStrong!Pass;";
                optionsBuilder.UseSqlServer(connectionString);
            }

            _options = optionsBuilder.Options;
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

            using (var context = CreateContext())
            {
                AchievementRepository? achievementRepository = new(context);

                await achievementRepository.SaveAchievementsAsync(achievementJson);
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
        [InlineData("DraftChimera239")]
        public async Task SaveGamerAchievementsAsync_ReadJson(string gamertag)
        {
            AchievementJson gamerAchievementJson = JsonLoader<AchievementJson>.LoadJsonFile("../../../../", "Achievements.json");

            using (var context = CreateContext())
            {
                AchievementRepository? achievementRepository = new(context);

                await achievementRepository.SaveAchievementsAsync(gamerAchievementJson);
            }

            using (var context = CreateContext())
            {
                AchievementRepository? achievementRepository = new(context);
                List<GamerAchievement> result = await achievementRepository.GetGamerAchievementsAsync(gamertag);

                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
        }
    }
}
