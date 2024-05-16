using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.GamerServices
{
    public class GamerService
    {
        private XblAppDbContext _dbContext;

        public GamerService(XblAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<GamerDto> FindByGamertag(string gamertag)
        {
            IQueryable<GamerDto> gamerQuery = _dbContext.Gamers
                .AsNoTracking()
                .Where(g => g.Gamertag == gamertag)
                .Select(g => new GamerDto
                {
                    GamerId = g.GamerId,
                    Gamertag = g.Gamertag,
                    Gamerscore = g.Gamerscore
                });

            return gamerQuery;
        }

        public IQueryable<GamerDto> GetAllGamers()
        {
            IQueryable<GamerDto> gamerQuery = _dbContext.Gamers
                .AsNoTracking()
                .Select(g => new GamerDto()
                {
                    GamerId = g.GamerId,
                    Gamertag = g.Gamertag,
                    Gamerscore = g.Gamerscore,
                    GamesCount = g.GameLinks.Count()
                });

            return gamerQuery;
        }
    }
}
