using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations.MsSql
{
    /// <inheritdoc />
    public partial class InitialDbMsSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NewShemaName");

            migrationBuilder.CreateTable(
                name: "Gamers",
                schema: "NewShemaName",
                columns: table => new
                {
                    GamerId = table.Column<long>(type: "bigint", nullable: false),
                    Gamertag = table.Column<string>(type: "VARCHAR(12)", maxLength: 12, nullable: false),
                    Gamerscore = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "VARCHAR(70)", maxLength: 70, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamers", x => x.GamerId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                schema: "NewShemaName",
                columns: table => new
                {
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    GameName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    TotalAchievements = table.Column<int>(type: "int", nullable: false),
                    TotalGamerscore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "GamerGame",
                schema: "NewShemaName",
                columns: table => new
                {
                    GamerId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    CurrentAchievements = table.Column<int>(type: "int", nullable: false),
                    CurrentGamerscore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamerGame", x => new { x.GamerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GamerGame_Gamers_GamerId",
                        column: x => x.GamerId,
                        principalSchema: "NewShemaName",
                        principalTable: "Gamers",
                        principalColumn: "GamerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamerGame_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "NewShemaName",
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamerGame_GameId",
                schema: "NewShemaName",
                table: "GamerGame",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamerGame",
                schema: "NewShemaName");

            migrationBuilder.DropTable(
                name: "Gamers",
                schema: "NewShemaName");

            migrationBuilder.DropTable(
                name: "Games",
                schema: "NewShemaName");
        }
    }
}
