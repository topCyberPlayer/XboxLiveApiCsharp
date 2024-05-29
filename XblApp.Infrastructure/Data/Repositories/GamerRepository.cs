using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GamerRepository : IGamerRepository
    {
        private readonly XblAppDbContext _context;

        public GamerRepository(XblAppDbContext context)
        {
            _context = context;
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

        public async Task<List<Game>> GetGamesForGamerAsync(string gamertag)
        {
            List<Game> games = await _context.Gamers
                .Where(g => g.Gamertag == gamertag)
                .SelectMany(g => g.GameLinks)
                .Include(gg => gg.Game)
                .Select(gg => gg.Game)
                .ToListAsync();

            return games;
        }

        public Gamer GetGamerProfile(long id)
        {
            Gamer? result = _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .First(x => x.GamerId == id);
            return result;
        }

        public Gamer GetGamerProfile(string gamertag)
        {
            Gamer? result = _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .FirstOrDefault(x => x.Gamertag == gamertag);
            return result;
        }

        public void SaveGamer(GamerDTO gamerDto)
        {
            Gamer gamer = new()
            {
                GamerId = gamerDto.GamerId,
                Gamertag = gamerDto?.Gamertag,
                Gamerscore = gamerDto.Gamerscore,
                Location = gamerDto?.Location,
                Bio = gamerDto?.Bio
            };

            _context.Gamers.Add(gamer);
            _context.SaveChanges();
        }
    }
}
