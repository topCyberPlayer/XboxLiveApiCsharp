using System.Reflection;
using XblApp.Infrastructure.Data.Seeding;

namespace XblApp.Test
{
    public class TestGamerJsonLoader
    {
        [Fact]
        public void TestGamerLoadOk()
        {
            //SETUP
            const string searchFile = "Gamers.json";
            var testDataDir = GetTestDataDir();

            //ATTEMPT
            var gamers = GamerJsonLoader.LoadGamers(testDataDir, searchFile);

            //VERIFY
            Assert.Equal(2, gamers.Count());
        }

        public static string GetTestDataDir(string alternateTestDir = null, Assembly callingAssembly = null)
        {
            alternateTestDir = alternateTestDir ?? "TestData";
            return Path.Combine(Path.GetFullPath(GetCallingAssemblyTopLevelDir(callingAssembly ?? Assembly.GetCallingAssembly()) + Path.DirectorySeparatorChar + alternateTestDir));
        }

        public static string GetCallingAssemblyTopLevelDir(Assembly callingAssembly = null)
        {
            string text = $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}";
            string location = (callingAssembly ?? Assembly.GetCallingAssembly()).Location;
            int num = location.IndexOf(text, StringComparison.OrdinalIgnoreCase);
            if (num <= 0)
            {
                throw new Exception("Did not find '" + text + "' in the assembly. Do you need to provide the callingAssembly parameter?");
            }

            return location.Substring(0, num);
        }
    }
}