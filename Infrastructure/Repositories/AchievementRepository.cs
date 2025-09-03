using Domain.Entities;
using Domain.Entities.JsonModels;
using Domain.Interfaces.IRepository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AchievementRepository(ApplicationDbContext context) : BaseRepository(context), IAchievementRepository
    {

        public async Task<IEnumerable<TKey?>> GetAllAchievementsAsync<TKey>(
            Expression<Func<Achievement, TKey>> selectExpression)
        {
            return await context.Achievements
                .Select(selectExpression)
                .ToListAsync();
        }

        public async Task<IEnumerable<TKey?>> GetAchievementsAsync<TKey>(
            Expression<Func<Achievement, bool>> filterExpression,
            Expression<Func<Achievement, TKey>> selectExpression)
        {
            return await context.Achievements
                .Where(filterExpression)
                .Select(selectExpression)
                .ToListAsync();
        }

        public async Task SaveAchievementsAsync(AchievementX360Json achievementJson)
        {
            long gamerId = achievementJson.GamerId;

            foreach (AchievementX360InnerJson achJson in achievementJson.Achievements)
            {
                // Получаем идентификатор игры
                long gameId = achJson.GameId;
                if (gameId == 0) continue; // Пропускаем некорректные записи

                // Проверяем, существует ли игра
                bool isExist = await context.Games.AnyAsync(g => g.GameId == gameId);
                if (!isExist) continue;

                // Проверяем, существует ли достижение в БД
                Achievement? achievementDb = await context.Achievements
                    .FirstOrDefaultAsync(a => a.AchievementId == achJson.AchievementId && a.GameId == achJson.GameId);

                if (achievementDb == null)
                {
                    achievementDb = new Achievement
                    {
                        AchievementId = achJson.AchievementId,
                        GameId = gameId,
                        Name = achJson.Name,
                        Description = achJson.Description,
                        LockedDescription = achJson.LockedDescription,
                        Gamerscore = achJson.Gamerscore,
                        IsSecret = achJson.IsSecret,
                    };

                    context.Achievements.Add(achievementDb);
                }

                // Проверяем, существует ли запись в GamerAchievement
                GamerAchievement? gamerAchievementDb = await context.GamerAchievements
                    .FirstOrDefaultAsync(ga => ga.GamerId == gamerId
                                             && ga.GameId == gameId
                                             && ga.AchievementId == achJson.AchievementId);

                if (gamerAchievementDb == null)
                {
                    gamerAchievementDb = new GamerAchievement
                    {
                        GamerId = gamerId,
                        GameId = gameId,
                        AchievementId = achJson.AchievementId,
                        IsUnlocked = achJson.Unlocked,
                        DateUnlocked = achJson.Unlocked ? achJson.TimeUnlocked : null
                    };

                    context.GamerAchievements.Add(gamerAchievementDb);
                }
                else
                {
                    // обновляем статус если уже есть
                    gamerAchievementDb.IsUnlocked = achJson.Unlocked;
                    gamerAchievementDb.DateUnlocked = achJson.Unlocked ? achJson.TimeUnlocked : null;

                    context.GamerAchievements.Update(gamerAchievementDb);
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task SaveAchievementsAsync(AchievementX1Json achievementJson)
        {
            long gamerId = achievementJson.GamerId;

            foreach (AchievementInnerJson achJson in achievementJson.Achievements)
            {
                // Получаем идентификатор игры
                long gameId = achJson.TitleAssociations.FirstOrDefault()?.Id ?? 0;
                if (gameId == 0) continue; // Пропускаем некорректные записи

                // Проверяем, существует ли игра
                bool isExist = await context.Games.AnyAsync(g => g.GameId == gameId);
                if (!isExist) continue;//Исключительная ситуация. Обычно перед сохранением достижения, игра уже находится в БД.

                long achievementId = long.Parse(achJson.AchievementId);

                // Проверяем, существует ли достижение в БД
                Achievement? achievementDb = await context.Achievements
                    .FirstOrDefaultAsync(a => a.AchievementId == achievementId && a.GameId == gameId);

                if (achievementDb == null)
                {
                    int gamerscore = default;
                    if (achJson.AchievementType == "Persistent")
                    {
                        if(int.TryParse(achJson.Rewards.FirstOrDefault()?.Value, out int gs))
                            gamerscore = gs;
                    }

                    // Если нет, создаем новое достижение
                    achievementDb = new Achievement
                    {
                        AchievementId = achievementId,
                        GameId = gameId,
                        Name = achJson.Name,
                        Description = achJson.Description,
                        LockedDescription = achJson.LockedDescription,
                        Gamerscore = gamerscore,
                        IsSecret = achJson.IsSecret
                    };

                    context.Achievements.Add(achievementDb);
                }

                // Проверяем, существует ли запись в GamerAchievement
                GamerAchievement? gamerAchievementDb = await context.GamerAchievements
                    .FirstOrDefaultAsync(ga =>
                    ga.GamerId == gamerId
                    && ga.GameId == gameId
                    && ga.AchievementId == achievementId);

                if (gamerAchievementDb == null)
                {
                    // Создаем новую связь между игроком и достижением
                    gamerAchievementDb = new GamerAchievement
                    {
                        GamerId = gamerId,
                        GameId = gameId,
                        AchievementId = achievementId,
                        IsUnlocked = achJson.ProgressState == "Achieved",
                        DateUnlocked = achJson.Progression?.TimeUnlocked
                    };

                    context.GamerAchievements.Add(gamerAchievementDb);
                }
                else
                {
                    // Обновляем статус, если он изменился
                    gamerAchievementDb.IsUnlocked = achJson.ProgressState == "Achieved";
                    gamerAchievementDb.DateUnlocked = achJson.Progression?.TimeUnlocked;
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
