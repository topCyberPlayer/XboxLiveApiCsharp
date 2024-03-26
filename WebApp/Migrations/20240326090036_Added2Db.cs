using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Added2Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TokenOAuth",
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
                    table.PrimaryKey("PK_TokenOAuth", x => x.AspNetUserId);
                });

            migrationBuilder.CreateTable(
                name: "TokenXau",
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
                    table.PrimaryKey("PK_TokenXau", x => x.AspNetUserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenOAuth");

            migrationBuilder.DropTable(
                name: "TokenXau");
        }
    }
}
