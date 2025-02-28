using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Database.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(XblAppDbContext context) : base(context) { }

        public async Task<List<(string, int, int, int)>> GetAllGamesAsync2()
        {
            var games = await _context.Games
                .Select(game => new ValueTuple<string, int, int, int>
                (
                    game.GameName,
                    game.TotalAchievements,
                    game.TotalGamerscore,
                    game.GamerGameLinks.Count // Количество игроков, связанных с игрой
                ))
                .ToListAsync();

            return games;
        }

        public async Task<List<Game>> GetAllGamesAsync() =>
            await _context.Games
            .AsNoTracking()
            .Include(x => x.GamerGameLinks)
            .ToListAsync();

        public async Task<Game> GetGameAsync(string gameName) =>
            await _context.Games
            .AsNoTracking()
            .Include(x => x.GamerGameLinks)
            .Where(g => g.GameName == gameName)
            .FirstOrDefaultAsync();


        public async Task SaveOrUpdateGamesAsync(List<Game> games)
        {
            foreach (Game? game in games)
            {
                var existingGame = await _context.Games
                    .Include(g => g.GamerGameLinks)
                    .FirstOrDefaultAsync(g => g.GameId == game.GameId);

                if (existingGame == null)
                {
                    // Если игры нет в БД, добавляем новую
                    _context.Games.Add(game);
                }
                else
                {
                    // Обновляем данные
                    existingGame.GameName = game.GameName;
                    existingGame.TotalAchievements = game.TotalAchievements;
                    existingGame.TotalGamerscore = game.TotalGamerscore;

                    // Обновляем связи GamerGame
                    foreach (var newGamerGame in game.GamerGameLinks)
                    {
                        var existingGamerGame = existingGame.GamerGameLinks
                            .FirstOrDefault(gg => gg.GamerId == newGamerGame.GamerId);

                        if (existingGamerGame != null)
                        {
                            // Обновляем статистику игрока
                            existingGamerGame.CurrentAchievements = newGamerGame.CurrentAchievements;
                            existingGamerGame.CurrentGamerscore = newGamerGame.CurrentGamerscore;
                        }
                        else
                        {
                            // Добавляем новую связь
                            existingGame.GamerGameLinks.Add(newGamerGame);
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

        }
    }
}
