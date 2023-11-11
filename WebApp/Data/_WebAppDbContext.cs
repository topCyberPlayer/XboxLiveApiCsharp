using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Services.Authentication;
using WebApp.Services.ProfileUser;

namespace WebApp.Data;

public class WebAppDbContext :  IdentityDbContext
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)   { }

    public DbSet<ProfileUserModelDb> ProfileUsers { get; set; }

    public DbSet<TokenOauth2ModelDb> TokenOauth2s { get; set; }

    public DbSet<TokenXstsModelDb> TokenXsts { get; set; }
}
