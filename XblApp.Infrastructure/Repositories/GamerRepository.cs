using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.JsonModels;

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

        public async Task<Gamer?> GetGamerProfileAsync(string gamertag) =>
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

        public async Task<Gamer?> GetGamesForGamerAsync(string gamertag) =>
            await _context.Gamers
                .Where(x => x.Gamertag == gamertag)
                .AsNoTracking()
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .FirstOrDefaultAsync();

        public async Task SaveOrUpdateGamersAsync(GamerJson gamerJson)
        {
            foreach (var profile in gamerJson.ProfileUsers)
            {
                if (!long.TryParse(profile.ProfileId, out long gamerId))
                {
                    // Пропускаем, если ID некорректный
                    continue;
                }

                // Ищем существующего игрока в БД
                Gamer? gamer = await _context.Gamers.FirstOrDefaultAsync(g => g.GamerId == gamerId);

                if (gamer == null)
                {
                    // Если игрока нет – создаем нового
                    gamer = new Gamer
                    {
                        GamerId = gamerId,
                        Gamertag = profile.Gamertag,
                        Gamerscore = profile.Gamerscore,
                        Bio = profile.Bio,
                        Location = profile.Location
                    };

                    _context.Gamers.Add(gamer);
                }
                else
                {
                    // Если игрок уже есть – обновляем данные
                    gamer.Gamertag = profile.Gamertag;
                    gamer.Gamerscore = profile.Gamerscore;
                    gamer.Bio = profile.Bio;
                    gamer.Location = profile.Location;
                }
            }

            await _context.SaveChangesAsync(); // Сохраняем в БД
        }

    }
}
