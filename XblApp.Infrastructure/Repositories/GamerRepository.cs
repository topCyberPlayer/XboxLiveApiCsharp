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
            .Include(a => a.GameLinks)
                .ThenInclude(b => b.Game)
            .FirstAsync(x => x.GamerId == id);

        public async Task<Gamer> GetGamerProfileAsync(string gamertag) =>
            await _context.Gamers
            .Where(x => x.Gamertag == gamertag)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        public async Task<List<Gamer>> GetAllGamerProfilesAsync() =>
            await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .ToListAsync();

        public async Task<Gamer> GetGamesForGamerAsync(string gamertag) =>
            await _context.Gamers
            .Where(x => x.Gamertag == gamertag)
            .AsNoTracking()
            .Include(a => a.GameLinks)
                .ThenInclude(b => b.Game)
            .FirstOrDefaultAsync();

        public async Task SaveGamerAsync(List<Gamer> gamers)
        {
            foreach (Gamer gamer in gamers)
            {
                // Ищем геймера в базе данных по его идентификатору
                Gamer? existingGamer = await _context.Gamers.FindAsync(gamer.GamerId);

                // Если геймер уже существует, обновляем его данные
                if (existingGamer != null)
                {
                    existingGamer.Gamertag = gamer.Gamertag;
                    existingGamer.Gamerscore = gamer.Gamerscore;
                    existingGamer.Bio = gamer.Bio;
                    existingGamer.Location = gamer.Location;

                    _context.Gamers.Update(existingGamer);
                }
                else
                {
                    // Если геймера нет в базе данных, добавляем нового
                    Gamer newGamer = new Gamer()
                    {
                        GamerId = gamer.GamerId,
                        Gamertag = gamer.Gamertag,
                        Gamerscore = gamer.Gamerscore,
                        Bio = gamer.Bio,
                        Location = gamer.Location,
                    };

                    _context.Gamers.Add(newGamer);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
