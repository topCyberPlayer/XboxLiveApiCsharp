using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace XblApp.Test
{
    public class BaseTestClass
    {
        //The equivalent JSON would be:
        //{
        //  "Key1": "Value1",
        //  "Nested": {
        //    "Key1": "NestedValue1",
        //    "Key2": "NestedValue2"
        //  }
        //}
        internal readonly Dictionary<string, string> inMemorySettings = new()
        {
            {"Key1", "Value1"},
            {"ConnectionStrings:MsSqlConnection", "Server=(localdb)\\mssqllocaldb;Database=XblApp;Trusted_Connection=True;MultipleActiveResultSets=true"},
            {"Nested:Key2", "NestedValue2"}
        };

        internal readonly IConfiguration _config;

        public BaseTestClass()
        {
            // test against this configuration
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
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
