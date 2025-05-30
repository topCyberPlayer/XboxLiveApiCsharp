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
            await context.Gamers
            .AsNoTracking()
            .Include(a => a.GamerGameLinks)
                .ThenInclude(b => b.GameLink)
            .FirstAsync(x => x.GamerId == id);

        public async Task<Gamer?> GetGamerProfileAsync(string gamertag) =>
            await context.Gamers
                .Where(x => x.Gamertag == gamertag)
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        public async Task<List<Gamer>> GetAllGamerProfilesAsync() =>
            await context.Gamers
                .AsNoTracking()
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .ToListAsync();

        public async Task<Gamer?> GetGamesForGamerAsync(string gamertag) =>
            await context.Gamers
                .Where(x => x.Gamertag == gamertag)
                .AsNoTracking()
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .FirstOrDefaultAsync();

        public async Task<bool> IsGamertagLinkedToUserAsync(string gamertag) =>
            await context.Gamers.AnyAsync(g => g.Gamertag == gamertag);
        

        public async Task SaveOrUpdateGamersAsync(GamerJson gamerJson)
        {
            foreach (var profile in gamerJson.ProfileUsers)
            {
                // Ищем существующего игрока в БД
                Gamer? gamer = await context.Gamers.FirstOrDefaultAsync(g => g.GamerId == profile.GamerId);

                if (gamer == null)
                {
                    // Если игрока нет – создаем нового
                    gamer = new Gamer
                    {
                        ApplicationUserId = profile.ApplicationUserId,
                        GamerId = profile.GamerId,
                        Gamertag = profile.Gamertag,
                        Gamerscore = profile.Gamerscore,
                        Bio = profile.Bio,
                        Location = profile.Location
                    };

                    context.Gamers.Add(gamer);
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

            await context.SaveChangesAsync(); // Сохраняем в БД
        }
    }
}
