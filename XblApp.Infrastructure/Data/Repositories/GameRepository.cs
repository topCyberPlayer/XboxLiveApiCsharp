using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly XblAppDbContext _context;

        public GameRepository(XblAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks)
                .ToListAsync();
        }

        public async Task<Game?> GetGameAsync(string gameName)
        {
            return await _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks)
                .FirstOrDefaultAsync(g => g.GameName == gameName);
        }
    }
}
