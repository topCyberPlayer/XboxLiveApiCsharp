﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XblApp.Database.Contexts;

#nullable disable

namespace XblApp.Database.Migrations
{
    [DbContext(typeof(XblAppDbContext))]
    [Migration("20250228140247_ChangedTypeToDateTimeOffset")]
    partial class ChangedTypeToDateTimeOffset
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
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

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5e8f70f9-5146-45b1-820f-f7a69255cbe0",
                            Name = "adminTeam",
                            NormalizedName = "adminteam"
                        },
                        new
                        {
                            Id = "3c5fb8f3-8bc3-460a-bb91-cef29388843d",
                            Name = "gamerTeam",
                            NormalizedName = "gamerteam"
                        },
                        new
                        {
                            Id = "34e296f1-55bf-4c76-ad78-c417002b9490",
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

                    b.ToTable("AspNetRoleClaims", (string)null);
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

                    b.ToTable("AspNetUserClaims", (string)null);
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

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
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

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("XblApp.Database.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

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

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Achievement", b =>
                {
                    b.Property<long>("AchievementId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("GameId")
                        .HasColumnType("bigint");

                    b.Property<int>("Gamerscore")
                        .HasColumnType("int");

                    b.Property<bool>("IsSecret")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("AchievementId");

                    b.HasIndex("GameId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Game", b =>
                {
                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateOnly?>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<int>("TotalAchievements")
                        .HasColumnType("int");

                    b.Property<int>("TotalGamerscore")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Gamer", b =>
                {
                    b.Property<long>("GamerId")
                        .HasColumnType("bigint");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Gamerscore")
                        .HasColumnType("int");

                    b.Property<string>("Gamertag")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("GamerId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Gamers");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.GamerAchievement", b =>
                {
                    b.Property<long>("GamerId")
                        .HasColumnType("bigint");

                    b.Property<long>("AchievementId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateUnlocked")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("GamerId", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.ToTable("GamerAchievement");
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

                    b.Property<DateTimeOffset>("LastTimePlayed")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("GamerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("GamerGame");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.XboxLiveToken", b =>
                {
                    b.Property<string>("UhsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("IssueInstant")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NotAfter")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIdFK")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UhsId");

                    b.HasIndex("UserIdFK")
                        .IsUnique();

                    b.ToTable("XboxLiveTokens");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.XboxOAuthToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthenticationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfExpiry")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("XboxOAuthTokens");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.XboxUserToken", b =>
                {
                    b.Property<string>("Xuid")
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

                    b.Property<string>("UhsIdFK")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserPrivileges")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Userhash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Xuid");

                    b.HasIndex("UhsIdFK")
                        .IsUnique();

                    b.ToTable("XboxUserTokens");
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
                    b.HasOne("XblApp.Database.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("XblApp.Database.Models.ApplicationUser", null)
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

                    b.HasOne("XblApp.Database.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("XblApp.Database.Models.ApplicationUser", null)
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
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("GameLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Gamer", b =>
                {
                    b.HasOne("XblApp.Database.Models.ApplicationUser", null)
                        .WithOne()
                        .HasForeignKey("XblApp.Domain.Entities.Gamer", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("XblApp.Domain.Entities.GamerAchievement", b =>
                {
                    b.HasOne("XblApp.Domain.Entities.Achievement", "AchievementLink")
                        .WithMany("GamerLinks")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XblApp.Domain.Entities.Gamer", "GamerLink")
                        .WithMany("GamerAchievementLinks")
                        .HasForeignKey("GamerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AchievementLink");

                    b.Navigation("GamerLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.GamerGame", b =>
                {
                    b.HasOne("XblApp.Domain.Entities.Game", "GameLink")
                        .WithMany("GamerGameLinks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XblApp.Domain.Entities.Gamer", "GamerLink")
                        .WithMany("GamerGameLinks")
                        .HasForeignKey("GamerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameLink");

                    b.Navigation("GamerLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.XboxLiveToken", b =>
                {
                    b.HasOne("XblApp.Domain.Entities.XboxOAuthToken", "XboxOAuthTokenLink")
                        .WithOne("XboxLiveTokenLink")
                        .HasForeignKey("XblApp.Domain.Entities.XboxLiveToken", "UserIdFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("XboxOAuthTokenLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.XboxUserToken", b =>
                {
                    b.HasOne("XblApp.Domain.Entities.XboxLiveToken", "XboxLiveToken")
                        .WithOne("UserTokenLink")
                        .HasForeignKey("XblApp.Domain.Entities.XboxUserToken", "UhsIdFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("XboxLiveToken");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Achievement", b =>
                {
                    b.Navigation("GamerLinks");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Game", b =>
                {
                    b.Navigation("AchievementLinks");

                    b.Navigation("GamerGameLinks");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.Gamer", b =>
                {
                    b.Navigation("GamerAchievementLinks");

                    b.Navigation("GamerGameLinks");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.XboxLiveToken", b =>
                {
                    b.Navigation("UserTokenLink");
                });

            modelBuilder.Entity("XblApp.Domain.Entities.XboxOAuthToken", b =>
                {
                    b.Navigation("XboxLiveTokenLink");
                });
#pragma warning restore 612, 618
        }
    }
}
