using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data;

public class WebSiteContext : IdentityDbContext
{
    public WebSiteContext(DbContextOptions<WebSiteContext> options) : base(options)   { }

    public DbSet<TokenOAuthModelDb> TokenOAuth { get; set; }

    public DbSet<TokenXauModelDb> TokenXau { get; set; }

    public DbSet<TokenXstsModelDb> TokenXsts { get; set; }
}
