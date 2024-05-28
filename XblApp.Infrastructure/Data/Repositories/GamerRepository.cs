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
            var gamerQuery = await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .ToListAsync();

            return gamerQuery;
        }

        public Gamer GetGamerProfile(long id)
        {
            Gamer? result = _context.Gamers.Find(id);
            return result;
        }

        public Gamer GetGamerProfile(string gamertag)
        {
            Gamer? result = _context.Gamers.Find(gamertag);
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
