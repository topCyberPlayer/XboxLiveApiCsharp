using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace DataLayer.Logic
{
    public class LogicDb : IStorage
    {
        private XblAppDbContext _context;

        public LogicDb(XblAppDbContext dbContext)
        {
            _context = dbContext;
        }

        public GamerModelDto? FindByGamertag(string gamertag)
        {
            GamerModelDto? gamerQuery = _context.Gamers
                .AsNoTracking()
                .Where(g => g.Gamertag == gamertag)
                .Select(g => new GamerModelDto
                {
                    GamerId = g.GamerId,
                    Gamertag = g.Gamertag,
                    Gamerscore = g.Gamerscore,
                    CurrentAchievements = g.GameLinks.Sum(a => a.CurrentAchievements),
                    CurrentGames = g.GameLinks.Select(a => a.Game).Count()
                }).
                FirstOrDefault();

            return gamerQuery;
        }

        public IEnumerable<GamerModelDto> GetAllGamers()
        {
            IQueryable<GamerModelDto> gamerQuery = _context.Gamers
                .AsNoTracking()
                .Include(a => a.GameLinks)
                    .ThenInclude(b => b.Game)
                .Select(g => new GamerModelDto()
                {
                    GamerId = g.GamerId,
                    Gamertag = g.Gamertag,
                    Gamerscore = g.Gamerscore,
                    CurrentAchievements = g.GameLinks.Sum(a => a.CurrentAchievements),
                    CurrentGames = g.GameLinks.Select(a => a.Game).Count()
                });

            return gamerQuery;
        }

        public GamerModelDb AddGamer(GamerModelXblPre gamerXbl)
        {
            long gamerIdXbl = long.Parse(gamerXbl.GamerId);

            GamerModelDb? gamer = _context.Gamers.Where(g => g.GamerId == gamerIdXbl).FirstOrDefault();

            if (gamer == null)
            {
                gamer = new GamerModelDb()
                {
                    //GamerId = gamerIdXbl,
                    //Gamertag = gamerXbl.Gamertag,
                    //Gamerscore = int.Parse(gamerXbl.Gamerscore),
                    Bio = gamer.Bio,
                    Location = gamer.Location
                };

                _context.Gamers.Add(gamer);
            }
            else
            {
                //gamer.Gamertag = gamerXbl.Gamertag;
                //gamer.Gamerscore = int.Parse(gamerXbl.Gamerscore);
                //gamer.Bio = gamerXbl.Settings.Bio;
            }

            _context.SaveChanges();

            return gamer;
        }

        //public GameModelDb AddGame()
        //{

        //}

        //public GamerGameModelDb ConnectGamerAndGame(GameModelDb game)
        //{

        //}
    }
}
