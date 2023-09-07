using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class WebAppContext : IdentityDbContext
    {
        public WebAppContext (DbContextOptions<WebAppContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
