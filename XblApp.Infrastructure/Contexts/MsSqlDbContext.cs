using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace XblApp.Database.Contexts
{
    public class MsSqlDbContext : XblAppDbContext
    {
        public MsSqlDbContext(DbContextOptions<XblAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Не все БД поддерживают "Имя Схемы"
            //У вас может быть две таблицы с одинаковым именем, но разными именами схемы:
            //например,таблица Books с именем схемы Display отличается от таблицы Books с именем схемы Order.
            modelBuilder.HasDefaultSchema("nba");
            base.OnModelCreating(modelBuilder);
        }
    }
}
