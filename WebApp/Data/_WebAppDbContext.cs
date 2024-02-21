using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data;

public class WebSiteContext : IdentityDbContext
{
    public WebSiteContext(DbContextOptions<WebSiteContext> options) : base(options)   { }

    public DbSet<TokenXstsModelDb> TokenXsts { get; set; }
}
