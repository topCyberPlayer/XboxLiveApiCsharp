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
        /// Проверка логику сохранения достижений из json-файла
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SaveAchievementsAsync_ReadJson()
        {
            List<Achievement> achievements = JsonLoader<AchievementJson, Achievement>.LoadJsonFile("../../../../", "Achievements.json").ToList();

            using (var context = CreateContext())
            {
                var achievementRepository = new AchievementRepository(context);

                await achievementRepository.SaveOrUpdateAchievementsAsync(achievements);
            }
        }
    }
}
