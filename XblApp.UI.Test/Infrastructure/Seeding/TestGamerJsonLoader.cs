using XblApp.Infrastructure.Data.Seeding;
using XblApp.Test;

namespace XblApp.UI.Test.Infrastructure.Seeding
{
    public class TestGamerJsonLoader : BaseTestClass
    {
        [Fact]
        public void TestGamerLoadOk()
        {
            //SETUP
            const string searchFile = "Gamers.json";
            var testDataDir = GetPathToDir();

            //ATTEMPT
            var gamers = GamerJsonLoader.LoadGamers(testDataDir, searchFile);

            //VERIFY
            Assert.Equal(2, gamers.Count());
        }


    }
}