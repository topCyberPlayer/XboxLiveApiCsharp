using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XblApp.Infrastructure.Data.Migrations.MsSql
{
    /// <inheritdoc />
    public partial class AchievementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievement",
                schema: "nba",
                columns: table => new
                {
                    AchievementId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gamerscore = table.Column<int>(type: "int", nullable: false),
                    IsSecret = table.Column<bool>(type: "bit", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_Achievement_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "nba",
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamerAchievement",
                schema: "nba",
                columns: table => new
                {
                    GamerId = table.Column<long>(type: "bigint", nullable: false),
                    AchievementId = table.Column<long>(type: "bigint", nullable: false),
                    DateUnlocked = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamerAchievement", x => new { x.GamerId, x.AchievementId });
                    table.ForeignKey(
                        name: "FK_GamerAchievement_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalSchema: "nba",
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamerAchievement_Gamers_GamerId",
                        column: x => x.GamerId,
                        principalSchema: "nba",
                        principalTable: "Gamers",
                        principalColumn: "GamerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_GameId",
                schema: "nba",
                table: "Achievement",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamerAchievement_AchievementId",
                schema: "nba",
                table: "GamerAchievement",
                column: "AchievementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamerAchievement",
                schema: "nba");

            migrationBuilder.DropTable(
                name: "Achievement",
                schema: "nba");
        }
    }
}
