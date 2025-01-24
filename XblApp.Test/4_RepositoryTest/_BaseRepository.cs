using Microsoft.Extensions.Configuration;
using XblApp.Database.Contexts;

namespace XblApp.Test.RepositoryTest
{
    public class BaseRepository
    {
        internal MsSqlDbContext context;

        internal readonly Dictionary<string, string> inMemorySettings = new()
        {
            {"Key1", "Value1"},
            {"ConnectionStrings:MsSqlConnection", "Server=(localdb)\\mssqllocaldb;Database=XblApp;Trusted_Connection=True;MultipleActiveResultSets=true"},
            {"Nested:Key2", "NestedValue2"}
        };

        public BaseRepository()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            context = new(configuration);
        }
    }
}
