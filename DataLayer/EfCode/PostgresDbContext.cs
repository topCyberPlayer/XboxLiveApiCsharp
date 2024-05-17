using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.EfCode
{
    public class PostgresDbContext : XblAppDbContext
    {
        public PostgresDbContext(IConfiguration configuration) : base(configuration)
        {   
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString(""));
        }
    }
}
