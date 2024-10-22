using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XblApp.Infrastructure.Data.Migrations.MsSql
{
    /// <inheritdoc />
    public partial class AddedAuthTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OAuthTokens",
                schema: "nba",
                columns: table => new
                {
                    AspNetUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresIn = table.Column<int>(type: "int", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthenticationToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuthTokens", x => x.AspNetUserId);
                });

            migrationBuilder.CreateTable(
                name: "XauTokens",
                schema: "nba",
                columns: table => new
                {
                    AspNetUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IssueInstant = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotAfter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uhs = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XauTokens", x => x.AspNetUserId);
                });

            migrationBuilder.CreateTable(
                name: "XstsTokens",
                schema: "nba",
                columns: table => new
                {
                    AspNetUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IssueInstant = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotAfter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userhash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gamertag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Privileges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPrivileges = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XstsTokens", x => x.AspNetUserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OAuthTokens",
                schema: "nba");

            migrationBuilder.DropTable(
                name: "XauTokens",
                schema: "nba");

            migrationBuilder.DropTable(
                name: "XstsTokens",
                schema: "nba");
        }
    }
}
