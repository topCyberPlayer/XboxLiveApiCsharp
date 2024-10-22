using XblApp.Infrastructure.Data;
using XblApp.Infrastructure.Data.Seeding;
using XblApp.Test;

namespace XblApp.UI.Test.Infrastructure.Seeding
{
    public class TestSeedDatabase : BaseTestClass
    {
        [Fact]
        public async Task SeedDatabaseIfNoGamersAsyncEmptyDatabase_Test()
        {
            //SETUP
            using (var context = new MsSqlDbContext(_config))
            {
                //context.Database.EnsureClean();

                var wwwrootDir = GetPathToDir("..\\XblApp\\wwwroot");

                //ATTEMPT
                await context.SeedDatabaseIfNoGamersAsync(wwwrootDir);

                //VERIFY
                Assert.Equal(2, context.Gamers.Count());
            }
        }

        [Fact]
        public async Task SeedDatabaseIfNoGamesAsyncEmptyDatabase_Test()
        {
            //SETUP
            using (var context = new MsSqlDbContext(_config))
            {
                //context.Database.EnsureClean();

                var wwwrootDir = GetPathToDir("..\\XblApp\\wwwroot");

                //ATTEMPT
                await context.SeedDatabaseIfNoGamesAsync(wwwrootDir);

                //VERIFY
                Assert.Equal(2, context.Gamers.Count());
            }
        }
    }
}
