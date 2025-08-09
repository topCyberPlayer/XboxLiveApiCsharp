using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuplicateCodeFromAchievementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Games_GameId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievements_Games_GameId",
                table: "GamerAchievements");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "230e4cd5-4e2f-4fd8-9cf8-5f88d8e5ed16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41b4a451-983f-4533-af54-7730d1835829");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c1ba626-5fb1-41a3-8eb3-5d1346b5f3e3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "016bf06b-435a-43cd-a45c-61525e7c9c58", null, "moderatorTeam", "moderatorteam" },
                    { "1e849029-d612-449e-8c16-de57c3a77657", null, "gamerTeam", "gamerteam" },
                    { "52cfa602-8761-4a6f-b0ef-b1daf96e824a", null, "adminTeam", "adminteam" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Games_GameId",
                table: "Achievements",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievements_Games_GameId",
                table: "GamerAchievements",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Games_GameId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievements_Games_GameId",
                table: "GamerAchievements");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "016bf06b-435a-43cd-a45c-61525e7c9c58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e849029-d612-449e-8c16-de57c3a77657");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52cfa602-8761-4a6f-b0ef-b1daf96e824a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "230e4cd5-4e2f-4fd8-9cf8-5f88d8e5ed16", null, "gamerTeam", "gamerteam" },
                    { "41b4a451-983f-4533-af54-7730d1835829", null, "adminTeam", "adminteam" },
                    { "6c1ba626-5fb1-41a3-8eb3-5d1346b5f3e3", null, "moderatorTeam", "moderatorteam" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Games_GameId",
                table: "Achievements",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievements_Games_GameId",
                table: "GamerAchievements",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
