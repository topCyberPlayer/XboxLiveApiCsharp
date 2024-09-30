using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GameRepository : BaseService, IGameRepository
    {
        public GameRepository(XblAppDbContext context) : base(context)
        {
            
        }

        private GameDTO CastToGameDTO(Game game)
        {
            GameDTO gameDTO = new GameDTO()
            {
                GameId = game.GameId,
                GameName = game.GameName,
                TotalAchievements = game.TotalAchievements,
                TotalGamerscore = game.TotalGamerscore,
                TotalGamers = game.GamerLinks.Select(a => a.Gamer).Count()
            };

            return gameDTO;
        }

        public async Task<List<GameDTO>> GetAllGamesAsync()
        {
            List<Game> gameColl = await _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks)
                .ToListAsync();

            List<GameDTO> gameCollDTO = gameColl.Select(game => CastToGameDTO(game)).ToList();

            return gameCollDTO;
        }

        public async Task<GameDTO> GetGameAsync(string gameName)
        {
            Game? game = await _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks)
                .FirstOrDefaultAsync(g => g.GameName == gameName);

            GameDTO gameDTO = CastToGameDTO(game);

            return gameDTO;
        }
    }
}
