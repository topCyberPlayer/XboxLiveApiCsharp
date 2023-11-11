﻿// <auto-generated />
using ConsoleApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230822124344_CreateXboxDB2")]
    partial class CreateXboxDB2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConsoleApp.API.Provider.ProfileUser", b =>
                {
                    b.Property<string>("ProfileId")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("HostId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "hostId");

                    b.Property<bool>("IsSponsoredUser")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "isSponsoredUser");

                    b.HasKey("ProfileId");

                    b.ToTable("ProfileUsers");
                });

            modelBuilder.Entity("ConsoleApp.API.Provider.Setting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("ProfileUserProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "value");

                    b.HasKey("Id");

                    b.HasIndex("ProfileUserProfileId");

                    b.ToTable("Setting");

                    b.HasAnnotation("Relational:JsonPropertyName", "settings");
                });

            modelBuilder.Entity("ConsoleApp.Authentication.OAuth2TokenResponse", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "user_id");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "access_token");

                    b.Property<string>("AuthenticationToken")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "authentication_token");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "expires_in");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "refresh_token");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "scope");

                    b.Property<string>("TokenType")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "token_type");

                    b.HasKey("UserId");

                    b.ToTable("OAuth2TokenResponses");
                });

            modelBuilder.Entity("ConsoleApp.API.Provider.Setting", b =>
                {
                    b.HasOne("ConsoleApp.API.Provider.ProfileUser", null)
                        .WithMany("Settings")
                        .HasForeignKey("ProfileUserProfileId");
                });

            modelBuilder.Entity("ConsoleApp.API.Provider.ProfileUser", b =>
                {
                    b.Navigation("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}