﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XblApp.Database.Contexts;

#nullable disable

namespace XblApp.Database.Migrations
{
    [DbContext(typeof(MsSqlDbContext))]
    partial class MsSqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("nba")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", "nba");

                    b.HasData(
                        new
                        {
                            Id = "fb753122-a37e-4ad7-a5ed-d53cdbb655e1",
                            Name = "adminTeam",
                            NormalizedName = "adminteam"
                        },
                        new
                        {
                            Id = "154a496b-980e-4e60-9086-ae46be82ddc1",
                            Name = "gamerTeam",
                            NormalizedName = "gamerteam"
                        },
                        new
                        {
                            Id = "ddebf89d-163d-4c1f-8130-23a4997daef9",
                            Name = "moderatorTeam",
                            NormalizedName = "moderatorteam"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", "nba");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", "nba");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", "nba");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", "nba");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", "nba");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Achievement", b =>
                {
                    b.Property<long>("AchievementId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateUnlock")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<int>("Gamerscore")
                        .HasColumnType("int");

                    b.Property<bool>("IsSecret")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AchievementId");

                    b.HasIndex("GameId");

                    b.ToTable("Achievements", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Game", b =>
                {
                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalAchievements")
                        .HasColumnType("int");

                    b.Property<int>("TotalGamerscore")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.ToTable("Games", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Gamer", b =>
                {
                    b.Property<long>("GamerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gamerscore")
                        .HasColumnType("int");

                    b.Property<string>("Gamertag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GamerId");

                    b.ToTable("Gamers", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.GamerAchievement", b =>
                {
                    b.Property<long>("GamerId")
                        .HasColumnType("bigint");

                    b.Property<long>("AchievementId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateUnlocked")
                        .HasColumnType("datetime2");

                    b.HasKey("GamerId", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.ToTable("GamerAchievement", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.GamerGame", b =>
                {
                    b.Property<long>("GamerId")
                        .HasColumnType("bigint");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<int>("CurrentAchievements")
                        .HasColumnType("int");

                    b.Property<int>("CurrentGamerscore")
                        .HasColumnType("int");

                    b.HasKey("GamerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("GamerGame", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.TokenOAuth", b =>
                {
                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthenticationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AspNetUserId");

                    b.ToTable("OAuthTokens", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.TokenXau", b =>
                {
                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("IssueInstant")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NotAfter")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uhs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AspNetUserId");

                    b.ToTable("XauTokens", "nba");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.TokenXsts", b =>
                {
                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgeGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gamertag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssueInstant")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NotAfter")
                        .HasColumnType("datetime2");

                    b.Property<string>("Privileges")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPrivileges")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Userhash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Xuid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AspNetUserId");

                    b.ToTable("XstsTokens", "nba");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Achievement", b =>
                {
                    b.HasOne("XblApp.Domain.Entities.Game", "GameLink")
                        .WithMany("AchievementLinks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.GamerAchievement", b =>
                {
                    b.HasOne("XblApp.Domain.Entities.Achievement", "AchievementLink")
                        .WithMany("GamerLinks")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XblApp.Domain.Entities.Gamer", "GamerLink")
                        .WithMany("AchievementLinks")
                        .HasForeignKey("GamerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AchievementLink");

                    b.Navigation("GamerLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.GamerGame", b =>
                {
                    b.HasOne("XblApp.Domain.Entities.Game", "GameLink")
                        .WithMany("GamerLinks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XblApp.Domain.Entities.Gamer", "GamerLink")
                        .WithMany("GameLinks")
                        .HasForeignKey("GamerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameLink");

                    b.Navigation("GamerLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Achievement", b =>
                {
                    b.Navigation("GamerLinks");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Game", b =>
                {
                    b.Navigation("AchievementLinks");

                    b.Navigation("GamerLinks");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Gamer", b =>
                {
                    b.Navigation("AchievementLinks");

                    b.Navigation("GameLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
