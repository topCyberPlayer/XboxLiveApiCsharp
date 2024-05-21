using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Context
{
    public class MsSqlDbContext : XblAppDbContext
    {
        public MsSqlDbContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("NewShemaName");//Не все БД поддерживают "Имя Схемы"
            //У вас может быть две таблицы с одинаковым именем, но разными именами схемы: например,таблица Books с именем схемы Display отличается от таблицы Books с именем схемы Order.

            base.OnModelCreating(modelBuilder);
        }
    }
}
