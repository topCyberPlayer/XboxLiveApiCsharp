using DataLayer.EfClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfCode
{
    public class EfCoreContext : IdentityDbContext
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options)
        {
            
        }

        public DbSet<Gamer> Gamers { get; set; }

        public DbSet<Game> Games { get; set; }
    }
}
