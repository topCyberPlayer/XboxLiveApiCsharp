﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XblApp.Database.Configurations;
using XblApp.Database.Models;
using XblApp.Domain.Entities;

namespace XblApp.Database.Contexts
{
    public class XblAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public XblAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Gamer> Gamers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<TokenOAuth> OAuthTokens { get; set; }
        public DbSet<TokenXau> XauTokens { get; set; }
        public DbSet<TokenXsts> XstsTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GamerConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new AchievementConfiguration());
            modelBuilder.ApplyConfiguration(new GamerGameConfiguration());
            modelBuilder.ApplyConfiguration(new GamerAchievementConfiguration());

            var adminTeam = new IdentityRole("adminTeam");
            adminTeam.NormalizedName = "adminteam";

            var gamerTeam = new IdentityRole("gamerTeam");
            gamerTeam.NormalizedName = "gamerteam";

            var moderatorTeam = new IdentityRole("moderatorTeam");
            moderatorTeam.NormalizedName = "moderatorteam";

            modelBuilder.Entity<IdentityRole>().HasData(adminTeam, gamerTeam, moderatorTeam);
        }
    }
}
