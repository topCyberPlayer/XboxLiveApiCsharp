using System.Reflection;
using XblApp.Infrastructure.Data.Seeding;

namespace XblApp.Test
{
    public class TestGamerJsonLoader : TestData
    {
        [Fact]
        public void TestGamerLoadOk()
        {
            //SETUP
            const string searchFile = "Gamers.json";
            var testDataDir = TestData.GetTestDataDir();

            //ATTEMPT
            var gamers = GamerJsonLoader.LoadGamers(testDataDir, searchFile);

            //VERIFY
            Assert.Equal(2, gamers.Count());
        }

        
    }
}