using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GameRepository : BaseService, IGameRepository
    {
        public GameRepository(XblAppDbContext context) : base(context) { }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return _context.Games.AsNoTracking().ToList();
        }

        public async Task<Game> GetGameAsync(string gameName)
        {
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(g => g.GameName == gameName);
        }

        public async Task SaveGameAsync(Game game)
        {
            // Ищем игру в базе данных по идентификатору
            Game? existingGame = await _context.Games.FindAsync(game.GameId);

            if (existingGame != null)
            {
                // Если игра уже существует, обновляем его данные
                existingGame.GameName = game.GameName;
                existingGame.TotalGamerscore = game.TotalGamerscore;
                existingGame.TotalAchievements = game.TotalAchievements;
                existingGame.GamerLinks = new List<GamerGame>()
                {
                    new GamerGame()
                    {
                        GameId = game.GameId,
                        GamerId = game.GamerLinks.Select(g => g.GameId).FirstOrDefault(),
                        CurrentAchievements = game.GamerLinks.Select(g => g.CurrentAchievements).FirstOrDefault(),
                        CurrentGamerscore = game.Achievement.CurrentGamerscore,
                    }
                };

                _context.Games.Update(existingGame);
            }
            else
            {
                // Если игры нет в базе данных, добавляем новую
                Game newGame = new Game()
                {
                    GameId = game.TitleId,
                    GameName = game.GameName,
                    TotalAchievements = game.Achievement.TotalAchievements,
                    TotalGamerscore = game.Achievement.TotalGamerscore,
                    GamerLinks = new List<GamerGame>()
                    {
                        new GamerGame()
                        {
                            GameId = game.TitleId,
                            GamerId = gamerId,
                            CurrentAchievements = game.Achievement.CurrentAchievements,
                            CurrentGamerscore = game.Achievement.CurrentGamerscore,
                        }
                    }
                };

                _context.Games.Add(newGame);
            }

            await _context.SaveChangesAsync();
        }
    }
}
