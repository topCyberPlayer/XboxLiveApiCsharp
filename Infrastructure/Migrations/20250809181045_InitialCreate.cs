using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    GameName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TotalAchievements = table.Column<int>(type: "integer", nullable: false),
                    TotalGamerscore = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "XboxOAuthTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TokenType = table.Column<string>(type: "text", nullable: true),
                    ExpiresIn = table.Column<int>(type: "integer", nullable: false),
                    Scope = table.Column<string>(type: "text", nullable: true),
                    AccessToken = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    AuthenticationToken = table.Column<string>(type: "text", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateOfExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XboxOAuthTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamers",
                columns: table => new
                {
                    GamerId = table.Column<long>(type: "bigint", nullable: false),
                    Gamertag = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Gamerscore = table.Column<int>(type: "integer", nullable: false),
                    Bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamers", x => x.GamerId);
                    table.ForeignKey(
                        name: "FK_Gamers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LockedDescription = table.Column<string>(type: "text", nullable: true),
                    Gamerscore = table.Column<int>(type: "integer", nullable: false),
                    IsSecret = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => new { x.AchievementId, x.GameId });
                    table.ForeignKey(
                        name: "FK_Achievements_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XboxLiveTokens",
                columns: table => new
                {
                    UhsId = table.Column<string>(type: "text", nullable: false),
                    IssueInstant = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NotAfter = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    UserIdFK = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XboxLiveTokens", x => x.UhsId);
                    table.ForeignKey(
                        name: "FK_XboxLiveTokens_XboxOAuthTokens_UserIdFK",
                        column: x => x.UserIdFK,
                        principalTable: "XboxOAuthTokens",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamerGame",
                columns: table => new
                {
                    GamerId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    LastTimePlayed = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CurrentAchievements = table.Column<int>(type: "integer", nullable: false),
                    CurrentGamerscore = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamerGame", x => new { x.GamerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GamerGame_Gamers_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamers",
                        principalColumn: "GamerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamerGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamerAchievements",
                columns: table => new
                {
                    GamerId = table.Column<long>(type: "bigint", nullable: false),
                    AchievementId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    DateUnlocked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsUnlocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamerAchievements", x => new { x.GamerId, x.GameId, x.AchievementId });
                    table.ForeignKey(
                        name: "FK_GamerAchievements_Achievements_AchievementId_GameId",
                        columns: x => new { x.AchievementId, x.GameId },
                        principalTable: "Achievements",
                        principalColumns: new[] { "AchievementId", "GameId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamerAchievements_Gamers_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamers",
                        principalColumn: "GamerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamerAchievements_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId");
                });

            migrationBuilder.CreateTable(
                name: "XboxUserTokens",
                columns: table => new
                {
                    Xuid = table.Column<string>(type: "text", nullable: false),
                    IssueInstant = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NotAfter = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Userhash = table.Column<string>(type: "text", nullable: true),
                    Gamertag = table.Column<string>(type: "text", nullable: true),
                    AgeGroup = table.Column<string>(type: "text", nullable: true),
                    Privileges = table.Column<string>(type: "text", nullable: true),
                    UserPrivileges = table.Column<string>(type: "text", nullable: true),
                    UhsIdFK = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XboxUserTokens", x => x.Xuid);
                    table.ForeignKey(
                        name: "FK_XboxUserTokens_XboxLiveTokens_UhsIdFK",
                        column: x => x.UhsIdFK,
                        principalTable: "XboxLiveTokens",
                        principalColumn: "UhsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "218be8dd-1635-48f7-8c89-237d710d21d1", null, "gamerTeam", "GAMERTEAM" },
                    { "3fc2f4d6-667a-4641-9076-d623d2b39b0e", null, "adminTeam", "ADMINTEAM" },
                    { "e861ffaf-8411-43b4-b60d-ec8a9fb4b3e0", null, "moderatorTeam", "MODERATORTEAM" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_GameId",
                table: "Achievements",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamerAchievements_AchievementId_GameId",
                table: "GamerAchievements",
                columns: new[] { "AchievementId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_GamerAchievements_GameId",
                table: "GamerAchievements",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamerGame_GameId",
                table: "GamerGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamers_ApplicationUserId",
                table: "Gamers",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XboxLiveTokens_UserIdFK",
                table: "XboxLiveTokens",
                column: "UserIdFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XboxUserTokens_UhsIdFK",
                table: "XboxUserTokens",
                column: "UhsIdFK",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GamerAchievements");

            migrationBuilder.DropTable(
                name: "GamerGame");

            migrationBuilder.DropTable(
                name: "XboxUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Gamers");

            migrationBuilder.DropTable(
                name: "XboxLiveTokens");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "XboxOAuthTokens");
        }
    }
}
