using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Database.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(XblAppDbContext context) : base(context) { }

        public async Task<List<Game>> GetAllGamesAsync() =>
            await _context.Games
            .AsNoTracking()
            .Include(x => x.GamerLinks)
            .ToListAsync();

        public async Task<Game> GetGameAsync(string gameName) =>
            await _context.Games
            .AsNoTracking()
            .Include(x => x.GamerLinks)
            .Where(g => g.GameName == gameName)
            .FirstOrDefaultAsync();


        public async Task SaveOrUpdateGamesAsync(List<Game> games)
        {
            foreach (Game? game in games)
            {
                var existingGame = await _context.Games
                    .Include(g => g.GamerLinks)
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
                    existingGame.GamerLinks.First().CurrentGamerscore = game.GamerLinks.First().CurrentGamerscore;
                    existingGame.GamerLinks.First().CurrentAchievements = game.GamerLinks.First().CurrentAchievements;
                }
            }

            await _context.SaveChangesAsync();

        }
    }
}
