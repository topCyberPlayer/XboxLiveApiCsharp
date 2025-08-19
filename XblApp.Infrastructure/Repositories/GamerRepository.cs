using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XblApp.Domain.Entities;
using XblApp.Domain.Entities.JsonModels;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Infrastructure.Contexts;

namespace XblApp.Infrastructure.Repositories
{
    public class GamerRepository(ApplicationDbContext context) : BaseRepository(context), IGamerRepository
    {
        public async Task<IEnumerable<TKey>> GetInclude_GamerGame_Game_Async<TKey>(
            Expression<Func<Gamer, TKey>> selectExpression) =>
            await context.Gamers
                .AsNoTracking()
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .Select(selectExpression)
                .ToListAsync();

        public async Task<TKey?> GetInclude_GamerGame_Game_Async<TKey>(
            Expression<Func<Gamer, bool>> filterExpression,
            Expression<Func<Gamer, TKey>> selectExpression) =>
            await context.Gamers
                .AsNoTracking()
                .Where(filterExpression)
                .Include(a => a.GamerGameLinks)
                    .ThenInclude(b => b.GameLink)
                .Select(selectExpression)
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
