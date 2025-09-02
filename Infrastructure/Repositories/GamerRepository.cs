using Domain.Entities;
using Domain.Entities.JsonModels;
using Domain.Interfaces.Repository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GamerRepository(ApplicationDbContext context) : BaseRepository(context), IGamerRepository
    {
        public async Task<TKey> GetInclude_GamerAchievement_Achievement_Async<TKey>(
            Expression<Func<Gamer, bool>> filterExpression, 
            Expression<Func<Gamer, TKey>> selectExpression) =>
            await context.Gamers
                .AsNoTracking()
                .Where(filterExpression)
                .Include(a => a.GamerAchievementLinks)
                    .ThenInclude(b => b.AchievementLink)
                .Select(selectExpression)
                .FirstOrDefaultAsync();



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


        public async Task SaveOrUpdateGamersAsync(GamerJson gamerJsonColl, string applicationUserId)
        {
            ProfileUser? gamerJson = gamerJsonColl.ProfileUsers.FirstOrDefault();
            
            // Ищем существующего игрока в БД
            Gamer? gamer = await context.Gamers.FirstOrDefaultAsync(g => g.GamerId == gamerJson.GamerId);

            if (gamer == null)
            {
                gamer = new Gamer
                {
                    ApplicationUserId = applicationUserId,
                    GamerId = gamerJson.GamerId,
                    Gamertag = gamerJson.Gamertag,
                    Gamerscore = gamerJson.Gamerscore,
                    Bio = gamerJson.Bio,
                    Location = gamerJson.Location
                };

                context.Gamers.Add(gamer);
            }
            else
            {
                gamer.Gamertag = gamerJson.Gamertag;
                gamer.Gamerscore = gamerJson.Gamerscore;
                gamer.Bio = gamerJson.Bio;
                gamer.Location = gamerJson.Location;
            }

            await context.SaveChangesAsync(); // Сохраняем в БД
        }

        public async Task SaveOrUpdateGamersAsync(Gamer gamerOutter, string applicationUserId)
        {
            Gamer? gamer = await context.Gamers.FirstOrDefaultAsync(g => g.GamerId == gamerOutter.GamerId);

            if (gamer == null)
            {
                gamer = new Gamer
                {
                    ApplicationUserId = applicationUserId,
                    GamerId = gamerOutter.GamerId,
                    Gamertag = gamerOutter.Gamertag,
                    Gamerscore = gamerOutter.Gamerscore,
                    Bio = gamerOutter.Bio,
                    Location = gamerOutter.Location
                };

                context.Gamers.Add(gamer);
            }
            else
            {
                gamer.Gamertag = gamerOutter.Gamertag;
                gamer.Gamerscore = gamerOutter.Gamerscore;
                gamer.Bio = gamerOutter.Bio;
                gamer.Location = gamerOutter.Location;
            }

            await context.SaveChangesAsync();
        }
    }
}
