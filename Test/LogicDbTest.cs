using DataLayer.Context;
using DataLayer.Logic;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Models;

namespace Test
{
    public class LogicDbTest
    {
        private static readonly Dictionary<string, string> _myConfiguration = new()
        {
            {"ConnectionStrings:MsSqlConnection", "Server=(localdb)\\mssqllocaldb;Database=XblAppN;Trusted_Connection=True;MultipleActiveResultSets=true"},
            {"Nested:Key2", "NestedValue2"}
        };

        //The equivalent JSON would be:
        //{
        //  "Key1": "Value1",
        //  "Nested": {
        //    "Key1": "NestedValue1",
        //    "Key2": "NestedValue2"
        //  }
        //}

        private readonly IConfigurationRoot? _configuration;

        public LogicDbTest()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(_myConfiguration)
                .Build();
        }

        [Fact]
        public void Test1()
        {
            using (var context = new MsSqlDbContext(_configuration))
            {
                List<GamerModelDb> gamers = context.Gamers
                    .Include(a => a.GameLinks)
                        .ThenInclude(b => b.Game)
                    .ToList();

                var a2 = gamers.Select(g => new GamerModelDto()
                {
                    GamerId = g.GamerId,
                    Gamertag = g.Gamertag,
                    CurrentAchievements = g.GameLinks.Sum(a => a.CurrentAchievements),
                    CurrentGames = g.GameLinks.Select(a => a.Game).Count()
                });

                //VERIFY
                Assert.Equal(2, gamers.Count);
                //Assert.Equal(2, gamers.SelectMany(a => a.GameLinks.Select(b => b.Game)).Count());
            }
        }
    }
}
