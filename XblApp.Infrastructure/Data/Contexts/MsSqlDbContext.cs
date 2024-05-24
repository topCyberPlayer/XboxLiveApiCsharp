using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace XblApp.Infrastructure.Data
{
    public class MsSqlDbContext : XblAppDbContext
    {
        public MsSqlDbContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection"));
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("nba");//Не все БД поддерживают "Имя Схемы"
            //У вас может быть две таблицы с одинаковым именем, но разными именами схемы: например,таблица Books с именем схемы Display отличается от таблицы Books с именем схемы Order.

            base.OnModelCreating(modelBuilder);
        }
    }
}
