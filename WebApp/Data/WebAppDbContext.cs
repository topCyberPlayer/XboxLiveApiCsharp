using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Profile;

namespace WebApp.Data;

public partial class WebAppDbContext :  IdentityDbContext<IdentityUser> //DbContext
{
    public WebAppDbContext()
    {
    }

    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<ProfileUserModelDb> ProfileUsers { get; set; }

    public virtual DbSet<TokenOauth2Table> TokenOauth2s { get; set; }

    public virtual DbSet<TokenXstTable> TokenXsts { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //=> optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebAppDB;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movie");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProfileUserModelDb>(entity =>
        {
            entity.Property(e => e.ProfileUserId).ValueGeneratedNever();
            entity.Property(e => e.AspNetUsersId).HasMaxLength(450);
            entity.Property(e => e.Bio).HasMaxLength(50);
            entity.Property(e => e.DateTimeUpdate).HasColumnType("datetime");
            entity.Property(e => e.Gamertag).HasMaxLength(50);
        });

        modelBuilder.Entity<TokenOauth2Table>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_OAuth2TokenResponses");

            entity.ToTable("TokenOAuth2");

            entity.Property(e => e.AspNetUserId).HasMaxLength(450);
            entity.Property(e => e.Scope).HasMaxLength(450);
            entity.Property(e => e.TokenType).HasMaxLength(450);
        });

        modelBuilder.Entity<TokenXstTable>(entity =>
        {
            entity.HasKey(e => e.Xuid).HasName("PK_XSTSResponses");
        });

        base.OnModelCreating(modelBuilder);
    }
}
