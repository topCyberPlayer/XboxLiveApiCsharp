using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.EfCode
{
    public class XblAppDbContext : DbContext //IdentityDbContext
    {
        //public XblAppDbContext(DbContextOptions<XblAppDbContext> options) : base(options) { }

        protected readonly IConfiguration Configuration;
        public XblAppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Gamer> Gamers { get; set; }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamerGame>()
                .HasKey(x => new {x.GamerId, x.GameId});
        }
    }
}
