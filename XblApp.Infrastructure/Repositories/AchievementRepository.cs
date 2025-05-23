﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<GamerAchievement>> GetGamerAchievementsAsync(string gamertag)
        {
            return await context.GamerAchievements
                .Include(a => a.GamerLink)
                .Include(a => a.GameLink)
                .Include(a => a.AchievementLink)
                .Where(ga => ga.GamerLink.Gamertag == gamertag)
                .ToListAsync();
         }

        public async Task<List<Achievement>> GetAchievementsAsync(string gameName)
        {
            return await context.Achievements
                .Where(x => x.GameLink.GameName == gameName).ToListAsync();
        }

        public async Task<List<Achievement?>> GetAllAchievementsAsync()
        {
            return await context.Achievements.ToListAsync();
        }

        public async Task SaveAchievementsAsync(AchievementX360Json achievementJson)
        {
            long gamerId = achievementJson.GamerId;

            foreach (AchievementX360InnerJson achJson in achievementJson.Achievements)
            {
                // Получаем идентификатор игры
                long gameId = achJson.GameId;
                if (gameId == 0) continue; // Пропускаем некорректные записи

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
                //Game? game = await context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
                //if (game == null) continue;

                long achievementId = long.Parse(achJson.AchievementId);

                // Проверяем, существует ли достижение в БД
                Achievement? achievementDb = await context.Achievements
                    .FirstOrDefaultAsync(a => a.AchievementId == achievementId && a.GameId == gameId);

                if (achievementDb == null)
                {
                    // Если нет, создаем новое достижение
                    achievementDb = new Achievement
                    {
                        AchievementId = achievementId,
                        GameId = gameId,
                        Name = achJson.Name,
                        Description = achJson.Description,
                        LockedDescription = achJson.LockedDescription,
                        Gamerscore = achJson.Rewards.FirstOrDefault()?.Value is string value ? int.Parse(value) : 0,
                        IsSecret = achJson.IsSecret
                    };

                    context.Achievements.Add(achievementDb);
                    //await context.SaveChangesAsync(); // Сохраняем, чтобы получить ID
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

            await context.SaveChangesAsync(); // Финальное сохранение всех изменений
        }
    }
}
