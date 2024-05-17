using DataLayer.Models;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace ServiceLayer.GamerServices
{
    public class GamerServiceDb
    {
        private XblAppDbContext _dbContext;

        public GamerServiceDb(XblAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<GamerModelDto> FindByGamertag(string gamertag)
        {
            IQueryable<GamerModelDto> gamerQuery = _dbContext.Gamers
                .AsNoTracking()
                .Where(g => g.Gamertag == gamertag)
                .Select(g => new GamerModelDto
                {
                    GamerId = g.GamerId,
                    Gamertag = g.Gamertag,
                    Gamerscore = g.Gamerscore
                });

            return gamerQuery;
        }

        public IQueryable<GamerModelDto> GetAllGamers()
        {
            IQueryable<GamerModelDto> gamerQuery = _dbContext.Gamers
                .AsNoTracking()
                .Select(g => new GamerModelDto()
                {
                    GamerId = g.GamerId,
                    Gamertag = g.Gamertag,
                    Gamerscore = g.Gamerscore,
                    GamesCount = g.GameLinks.Count()
                });

            return gamerQuery;
        }

        public GamerModelDb AddGamer(GamerModelXblPre gamerXbl)
        {
            long gamerIdXbl = long.Parse(gamerXbl.GamerId);

            GamerModelDb? gamer = _dbContext.Gamers.Where(g => g.GamerId == gamerIdXbl).FirstOrDefault();

            if (gamer == null)
            {
                gamer = new GamerModelDb()
                {
                    GamerId = gamerIdXbl,
                    Gamertag = gamerXbl.Gamertag,
                    Gamerscore = int.Parse(gamerXbl.Gamerscore),
                    Bio = gamer.Bio,
                    Location = gamer.Location
                };

                _dbContext.Gamers.Add(gamer);
            }
            else
            {
                gamer.Gamertag = gamerXbl.Gamertag;
                gamer.Gamerscore = int.Parse(gamerXbl.Gamerscore);
                //gamer.Bio = gamerXbl.Settings.Bio;
            }

            _dbContext.SaveChanges();


        }

        public GameModelDb AddGame()
        {

        }

        public GamerGameModelDb ConnectGamerAndGame(GameModelDb game)
        {
            _dbContext.
        }
    }
}
