using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Context
{
    public abstract class XblAppDbContext : DbContext //IdentityDbContext
    {
        //public XblAppDbContext(DbContextOptions<XblAppDbContext> options) : base(options) { }

        protected readonly IConfiguration Configuration;
        public XblAppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<GamerModelDb> Gamers { get; set; }

        public DbSet<GameModelDb> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamerGameModelDb>()
                .HasKey(x => new {x.GamerId, x.GameId});
        }
    }
}
