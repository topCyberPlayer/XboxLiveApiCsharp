using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GamerRepository : BaseService, IGamerRepository
    {
        public GamerRepository(XblAppDbContext context) : base(context)
        {
            
        }

        private GamerDTO CastToGamerDTO(Gamer gamer)
        {
            GamerDTO gamerDTO = new GamerDTO()
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Gamerscore = gamer.Gamerscore,
                Bio = gamer.Bio,
                Location = gamer.Location,
                CurrentGamesCount = gamer.GameLinks.Select(x => x.Game).Count(),
                CurrentAchievementsCount = gamer.GameLinks.Sum(x => x.CurrentAchievements)
            };

            return gamerDTO;
        }

        public async Task<GamerDTO> GetGamerProfileAsync(long id)
        {
            Gamer? gamer = await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .FirstAsync(x => x.GamerId == id);

            GamerDTO gamerDTO = CastToGamerDTO(gamer);

            return gamerDTO;
        }

        public async Task<GamerDTO> GetGamerProfileAsync(string gamertag)
        {
            Gamer? gamer = await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .FirstOrDefaultAsync(x => x.Gamertag == gamertag);

            GamerDTO gamerDTO = CastToGamerDTO(gamer);

            return gamerDTO;
        }

        public async Task<List<GamerDTO>> GetAllGamerProfilesAsync()
        {
            List<Gamer> gamerColl = await _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .ToListAsync();

            List<GamerDTO> gamerCollDTO = gamerColl.Select(gamer => CastToGamerDTO(gamer)).ToList();

            return gamerCollDTO;
        }

        public async Task<GamerGameDTO> GetGamesForGamerAsync(string gamertag)
        {
            Gamer? gamer = await _context.Gamers
                .Include(g => g.GameLinks)
                .ThenInclude(gg => gg.Game)
                .FirstOrDefaultAsync(g => g.Gamertag == gamertag);

            GamerGameDTO gamerGameDTO = new GamerGameDTO()
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Games = gamer.GameLinks.Select(gg => new AbcDTO
                {
                    GameId = gg.Game.GameId,
                    GameName = gg.Game.GameName,
                    TotalAchievements = gg.Game.TotalAchievements,
                    TotalGamerscore = gg.Game.TotalGamerscore,

                    CurrentAchievements = gg.CurrentAchievements,
                    CurrentGamerscore = gg.CurrentGamerscore
                }).ToList()
            };

            return gamerGameDTO;
        }

        public async Task SaveGamerAsync(GamerDTO gamerDTO)
        {
            // Ищем геймера в базе данных по его идентификатору
            Gamer? existingGamer = await _context.Gamers.FindAsync(gamerDTO.GamerId);

            if (existingGamer != null)
            {
                // Если геймер уже существует, обновляем его данные
                existingGamer.Gamertag = gamerDTO.Gamertag;
                existingGamer.Gamerscore = gamerDTO.Gamerscore;
                existingGamer.Bio = gamerDTO.Bio;
                existingGamer.Location = gamerDTO.Location;

                _context.Gamers.Update(existingGamer);
            }
            else
            {
                // Если геймера нет в базе данных, добавляем нового
                Gamer newGamer = new Gamer()
                {
                    GamerId = gamerDTO.GamerId,
                    Gamertag = gamerDTO.Gamertag,
                    Gamerscore = gamerDTO.Gamerscore,
                    Bio = gamerDTO.Bio,
                    Location = gamerDTO.Location,
                };

                _context.Gamers.Add(newGamer);
            }

            await _context.SaveChangesAsync();
        }

    }
}
