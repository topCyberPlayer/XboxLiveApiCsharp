using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Database.Repositories
{
    public class AchievementRepository : BaseRepository, IAchievementRepository
    {
        public AchievementRepository(XblAppDbContext context) : base(context)
        {
        }

        public Task<Achievement> GetAchievementsAsync(string gameName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Achievement>> GetAllAchievementsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveOrUpdateAchievementsAsync(List<Achievement> achievements)
        {
            List<long> achievementIds = achievements.Select(a => a.AchievementId).ToList();

            Dictionary<long, Achievement> existingAchievements = await _context.Achievements
                .Where(a => achievementIds.Contains(a.AchievementId))
                .ToDictionaryAsync(a => a.AchievementId);

            foreach (var achievement in achievements)
            {
                if (existingAchievements.TryGetValue(achievement.AchievementId, out var existingAchievement))
                {
                    // Обновляем существующее достижение
                    _context.Entry(existingAchievement).CurrentValues.SetValues(achievement);
                }
                else
                {
                    // Добавляем новое достижение
                    _context.Achievements.Add(achievement);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveOrUpdateGamerAchievementsAsync(List<GamerAchievement> gamerAchievements)
        {
            List<long>? achievementIds = gamerAchievements.Select(ga => ga.AchievementId).ToList();
            List<long>? gamerIds = gamerAchievements.Select(ga => ga.GamerId).ToList();

            // Загружаем уже существующие записи
            var existingGamerAchievements = await _context.GamerAchievements
                .Where(ga => achievementIds.Contains(ga.AchievementId) && gamerIds.Contains(ga.GamerId))
                .ToDictionaryAsync(ga => new { ga.GamerId, ga.AchievementId });

            foreach (var gamerAchievement in gamerAchievements)
            {
                var key = new { gamerAchievement.GamerId, gamerAchievement.AchievementId };

                if (existingGamerAchievements.TryGetValue(key, out var existingGamerAchievement))
                {
                    // Обновляем существующую запись
                    _context.Entry(existingGamerAchievement).CurrentValues.SetValues(gamerAchievement);
                }
                else
                {
                    // Добавляем новую запись
                    _context.GamerAchievements.Add(gamerAchievement);
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}
