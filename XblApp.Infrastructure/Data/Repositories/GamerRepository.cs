using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GamerRepository : IGamerRepository
    {
        private readonly XblAppDbContext _context;

        public GamerRepository(XblAppDbContext context)
        {
            _context = context;
        }

        public async Task<Gamer> GetGamerProfile(long id)
        {
            Gamer? result = await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .FirstAsync(x => x.GamerId == id);
            return result;
        }

        public async Task<Gamer> GetGamerProfile(string gamertag)
        {
            Gamer? result = await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .FirstOrDefaultAsync(x => x.Gamertag == gamertag);
            return result;
        }

        public async Task<List<Gamer>> GetAllGamerProfilesAsync()
        {
            List<Gamer> gamerQuery = await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .ToListAsync();

            return gamerQuery;
        }

        public async Task<Gamer?> GetGamesForGamerAsync(string gamertag)
        {
            return await _context.Gamers
                .Include(g => g.GameLinks)
                .ThenInclude(gg => gg.Game)
                .FirstOrDefaultAsync(g => g.Gamertag == gamertag);
        }

        public void SaveGamer(Gamer gamer)
        {
            //Gamer gamer = new()
            //{
            //    GamerId = gamerDto.GamerId,
            //    Gamertag = gamerDto?.Gamertag,
            //    Gamerscore = gamerDto.Gamerscore,
            //    Location = gamerDto?.Location,
            //    Bio = gamerDto?.Bio
            //};

            _context.Gamers.Add(gamer);
            _context.SaveChanges();
        }
    }
}
