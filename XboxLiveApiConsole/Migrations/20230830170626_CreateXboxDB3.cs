using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateXboxDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ProfileUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expires",
                table: "OAuth2TokenResponses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Issued",
                table: "OAuth2TokenResponses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "XSTSResponses",
                columns: table => new
                {
                    Xuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IssueInstant = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotAfter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XSTSResponses", x => x.Xuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XSTSResponses");

            migrationBuilder.DropColumn(
                name: "Expires",
                table: "OAuth2TokenResponses");

            migrationBuilder.DropColumn(
                name: "Issued",
                table: "OAuth2TokenResponses");

            migrationBuilder.CreateTable(
                name: "ProfileUsers",
                columns: table => new
                {
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HostId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSponsoredUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileUsers", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileUserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setting_ProfileUsers_ProfileUserProfileId",
                        column: x => x.ProfileUserProfileId,
                        principalTable: "ProfileUsers",
                        principalColumn: "ProfileId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Setting_ProfileUserProfileId",
                table: "Setting",
                column: "ProfileUserProfileId");
        }
    }
}
