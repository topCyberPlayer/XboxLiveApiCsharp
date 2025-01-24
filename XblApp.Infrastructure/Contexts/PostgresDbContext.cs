using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace XblApp.Database.Contexts
{
    public class PostgresDbContext : XblAppDbContext
    {
        public PostgresDbContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgresSqlConnection"));
        }
    }
}
