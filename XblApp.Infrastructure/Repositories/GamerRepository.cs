using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.DTO;

namespace XblApp.Database.Repositories
{
    public class GamerRepository : BaseRepository, IGamerRepository
    {
        public GamerRepository(XblAppDbContext context) : base(context) { }

        public async Task<Gamer> GetGamerProfileAsync(long id) =>
            await _context.Gamers
            .AsNoTracking()
            .Include(a => a.GamerGameLinks)
                .ThenInclude(b => b.GameLink)
            .FirstAsync(x => x.GamerId == id);

        public async Task<Gamer> GetGamerProfileAsync(string gamertag) =>
            await _context.Gamers
                .Where(x => x.Gamertag == gamertag)
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        public async Task<List<Gamer>> GetAllGamerProfilesAsync() =>
            await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .ToListAsync();

        public async Task<Gamer> GetGamesForGamerAsync(string gamertag) =>
            await _context.Gamers
                .Where(x => x.Gamertag == gamertag)
                .AsNoTracking()
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .FirstOrDefaultAsync();

        public async Task SaveOrUpdateGamersAsync(List<Gamer> gamers)
        {
            foreach (Gamer gamer in gamers)
            {
                // Ищем геймера в базе данных по его идентификатору
                Gamer? existingGamer = await _context.Gamers.FindAsync(gamer.GamerId);

                if (existingGamer == null)
                    _context.Gamers.Add(gamer);
                else
                {
                    _context.Entry(existingGamer).CurrentValues.SetValues(gamer);
                }    
            }

            await _context.SaveChangesAsync();
        }
    }
}
