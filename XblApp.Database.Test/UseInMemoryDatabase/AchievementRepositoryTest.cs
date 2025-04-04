using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        /// <summary>
        /// Этот класс используется в основном для ЗАПИСИ во временную БД (или в реальную)
        /// </summary>
        public AchievementRepositoryTest()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            bool useInMemory = false; // false - для работы с реальной БД

            var optionsBuilder = new DbContextOptionsBuilder<XblAppDbContext>();

            if (useInMemory)
            {
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
            }
            else
            {
                string connectionString = config.GetConnectionString("MsSqlConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

            _options = optionsBuilder.Options;
        }

        private XblAppDbContext CreateContext()
        {
            return new XblAppDbContext(_options);
        }

        /// <summary>
        /// Проверка логики сохранения достижений из json-файла в таблицу Achievement
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SaveAchievementsAsync_ReadJson()
        {
            AchievementX1Json? achievementJson = JsonLoader<AchievementX1Json>.LoadJsonFile("../../../../", "Achievements.json");

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

        /// <summary>
        /// Проверка логики сохранения достижений из json-файла в таблицу GamerAchievement
        /// </summary>
        /// <param name="gamertag"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("DraftChimera239")]
        public async Task SaveGamerAchievementsAsync_ReadJson(string gamertag)
        {
            AchievementX1Json gamerAchievementJson = JsonLoader<AchievementX1Json>.LoadJsonFile("../../../../", "Achievements.json");

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
