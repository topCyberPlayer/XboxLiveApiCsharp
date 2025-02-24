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


        public async Task SaveGameAsync(List<Game> games)
        {
            foreach (Game game in games)
            {
                // Ищем игру в базе данных по идентификатору
                Game? existingGame = await _context.Games
                    .Include(g => g.GamerLinks) // Загружаем связанные записи
                    .FirstOrDefaultAsync(g => g.GameId == game.GameId);

                List<GamerGame> gamerLinks = new List<GamerGame>();

                foreach (var gamerGame in game.GamerLinks)
                {
                    gamerLinks.Add(new GamerGame
                    {
                        GameId = game.GameId,
                        GamerId = gamerGame.GamerId,
                        CurrentAchievements = gamerGame.CurrentAchievements,
                        CurrentGamerscore = gamerGame.CurrentGamerscore
                    });
                }

                if (existingGame != null)
                {
                    existingGame.GameName = game.GameName;
                    existingGame.TotalGamerscore = game.TotalGamerscore;
                    existingGame.TotalAchievements = game.TotalAchievements;
                    existingGame.GamerLinks = gamerLinks;

                    _context.Games.Update(existingGame);
                }
                else
                {
                    _context.Games.Add(new Game
                    {
                        GameId = game.GameId,
                        GameName = game.GameName,
                        TotalAchievements = game.TotalAchievements,
                        TotalGamerscore = game.TotalGamerscore,
                        GamerLinks = gamerLinks
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
