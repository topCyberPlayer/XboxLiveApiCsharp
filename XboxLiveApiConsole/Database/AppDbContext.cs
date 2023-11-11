using ConsoleApp.API.Provider;
using ConsoleApp.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<OAuth2TokenResponse> OAuth2TokenResponses { get; set; } //Будущее название таблицы в БД (OAuth2TokenResponses)
        //public DbSet<XAUResponse> XAUResponses { get; set; }

        public DbSet<XSTSResponse> XSTSResponses { get; set; }
        //public DbSet<ProfileUser> ProfileUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=XboxDB;Trusted_Connection=True;");
        }
    }
}
