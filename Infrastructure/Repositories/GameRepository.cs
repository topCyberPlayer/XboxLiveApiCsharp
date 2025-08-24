using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XblApp.Domain.Entities;
using XblApp.Domain.Entities.JsonModels;
using XblApp.Domain.Interfaces.Repository;
using XblApp.Infrastructure.Contexts;

namespace XblApp.Infrastructure.Repositories
{
    public class GameRepository(ApplicationDbContext context) : BaseRepository(context), IGameRepository
    {
        public async Task<IEnumerable<TKey>> GetInclude_GamerGameLinks_Async<TKey>(
            Expression<Func<Game, TKey>> selectExpression) =>
            await context.Games
            .AsNoTracking()
            .Include(x => x.GamerGameLinks)
            .Select(selectExpression)
            .ToListAsync();

        public async Task<TKey?> GetInclude_GamerGame_Achievement_GamerAchievement_Async<TKey>(
            Expression<Func<Game, TKey>> selectExpression,
            Expression<Func<Game, bool>> filterExpression) =>
            await context.Games
            .AsNoTracking()
            .Include(x => x.GamerGameLinks)
            .Include(a => a.AchievementLinks)
            .Include(a => a.GamerAchievementLinks)
            .Where(filterExpression)
            .Select(selectExpression)
            .FirstOrDefaultAsync();

        public async Task SaveOrUpdateGamesAsync(GameJson gameJson)
        {
            var gamerId = long.Parse(gameJson.Xuid);

            foreach (var title in gameJson.Games)
            {
                var game = await context.Games
                    .Include(g => g.GamerGameLinks) // Загружаем связи игры с игроками
                    .FirstOrDefaultAsync(g => g.GameId == long.Parse(title.GameId));

                if (game == null)
                {
                    game = new Game
                    {
                        GameId = long.Parse(title.GameId),
                        GameName = title.Name,
                        TotalAchievements = title.Achievement?.TotalAchievements ?? 0,
                        TotalGamerscore = title.Achievement?.TotalGamerscore ?? 0,
                        ReleaseDate = title.Detail?.ReleaseDate.HasValue == true
                            ? DateOnly.FromDateTime(title.Detail.ReleaseDate.Value)
                            : null,
                        Description = title.Detail?.Description
                    };

                    context.Games.Add(game);
                }
                else
                {
                    // Обновляем данные об игре
                    game.GameName = title.Name;
                    game.TotalAchievements = title.Achievement?.TotalAchievements ?? game.TotalAchievements;
                    game.TotalGamerscore = title.Achievement?.TotalGamerscore ?? game.TotalGamerscore;
                    game.ReleaseDate = title.Detail?.ReleaseDate.HasValue == true
                        ? DateOnly.FromDateTime(title.Detail.ReleaseDate.Value)
                        : game.ReleaseDate;
                    game.Description = title.Detail?.Description ?? game.Description;
                }

                // Проверяем, есть ли уже связь между игроком и этой игрой
                var gamerGame = game.GamerGameLinks.FirstOrDefault(gg => gg.GamerId == gamerId);

                if (gamerGame == null)
                {
                    gamerGame = new GamerGame
                    {
                        GamerId = gamerId,
                        GameId = game.GameId,
                        LastTimePlayed = title.TitleHistory?.LastTimePlayed ?? DateTimeOffset.UtcNow,
                        CurrentAchievements = title.Achievement?.CurrentAchievements ?? 0,
                        CurrentGamerscore = title.Achievement?.CurrentGamerscore ?? 0
                    };

                    game.GamerGameLinks.Add(gamerGame); // Добавляем связь в навигационное свойство
                }
                else
                {
                    // Обновляем статистику игрока по этой игре
                    gamerGame.LastTimePlayed = title.TitleHistory?.LastTimePlayed ?? gamerGame.LastTimePlayed;
                    gamerGame.CurrentAchievements = title.Achievement?.CurrentAchievements ?? gamerGame.CurrentAchievements;
                    gamerGame.CurrentGamerscore = title.Achievement?.CurrentGamerscore ?? gamerGame.CurrentGamerscore;
                }
            }

            await context.SaveChangesAsync();
        }

    }
}
