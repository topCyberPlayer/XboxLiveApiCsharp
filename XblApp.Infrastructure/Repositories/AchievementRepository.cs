using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.JsonModels;

namespace XblApp.Database.Repositories
{
    public class AchievementRepository : BaseRepository, IAchievementRepository
    {
        public AchievementRepository(XblAppDbContext context) : base(context)
        {
        }

        public async Task<List<GamerAchievement>> GetGamerAchievementsAsync(long xuid)
        {
            return await _context.GamerAchievements.Where(x => x.GamerId == xuid).ToListAsync();
        }

        public async Task<List<Achievement>> GetAchievementsAsync(string gameName)
        {
            return await _context.Achievements.Where(x => x.Name == gameName).ToListAsync();
        }

        public async Task<List<Achievement?>> GetAllAchievementsAsync()
        {
            return await _context.Achievements.ToListAsync();
        }

        public async Task SaveAchievementsAsync(AchievementJson achievementJson)
        {
            long gamerId = achievementJson.Xuid;

            foreach (AchievementInnerJson achJson in achievementJson.Achievements)
            {
                // Получаем идентификатор игры
                var gameId = achJson.TitleAssociations.FirstOrDefault()?.Id ?? 0;
                if (gameId == 0) continue; // Пропускаем некорректные записи

                // Проверяем, существует ли игра
                var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
                if (game == null)
                {
                    // Пропускаем, если игры нет
                    continue;
                }

                // Проверяем, существует ли достижение в БД
                Achievement? achievement = await _context.Achievements
                    .FirstOrDefaultAsync(a => a.AchievementId == long.Parse(achJson.TitleId) && a.GameId == gameId);

                if (achievement == null)
                {
                    // Если нет, создаем новое достижение
                    achievement = new Achievement
                    {
                        AchievementId = long.Parse(achJson.TitleId),
                        GameId = gameId,
                        Name = achJson.Name,
                        Description = achJson.Description,
                        LockedDescription = achJson.LockedDescription,
                        Gamerscore = achJson.Rewards.FirstOrDefault()?.Value is string value ? int.Parse(value) : 0,
                        IsSecret = achJson.IsSecret
                    };

                    _context.Achievements.Add(achievement);
                    await _context.SaveChangesAsync(); // Сохраняем, чтобы получить ID
                }

                // Проверяем, существует ли запись в GamerAchievement
                var gamerAchievement = await _context.GamerAchievements
                    .FirstOrDefaultAsync(ga => ga.GamerId == gamerId && ga.AchievementId == achievement.AchievementId);

                if (gamerAchievement == null)
                {
                    // Создаем новую связь между игроком и достижением
                    gamerAchievement = new GamerAchievement
                    {
                        GamerId = gamerId,
                        GameId = gameId,
                        AchievementId = achievement.AchievementId,
                        IsUnlocked = achJson.ProgressState == "Achieved",
                        DateUnlocked = achJson.Progression?.TimeUnlocked
                    };

                    _context.GamerAchievements.Add(gamerAchievement);
                }
                else
                {
                    // Обновляем статус, если он изменился
                    gamerAchievement.IsUnlocked = achJson.ProgressState == "Achieved";
                    gamerAchievement.DateUnlocked = achJson.Progression?.TimeUnlocked;
                }
            }

            await _context.SaveChangesAsync(); // Финальное сохранение всех изменений
        }

    }
}
