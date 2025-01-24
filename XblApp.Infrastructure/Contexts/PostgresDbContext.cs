using Microsoft.EntityFrameworkCore;

namespace XblApp.Database.Contexts
{
    public class PostgresDbContext : XblAppDbContext
    {
        public PostgresDbContext(DbContextOptions<XblAppDbContext> options) : base(options)
        {
        }
    }
}
