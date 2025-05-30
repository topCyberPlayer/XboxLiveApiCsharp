using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLinks : Migration
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
                keyValue: "35300e45-45b3-4c97-aa4d-0d09b6f1e4a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81ae25ba-547b-41ce-bc23-8c6cb740e198");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a79024cd-bddb-4a53-b419-429816e4b47f");

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
                    { "35300e45-45b3-4c97-aa4d-0d09b6f1e4a5", null, "gamerTeam", "gamerteam" },
                    { "81ae25ba-547b-41ce-bc23-8c6cb740e198", null, "moderatorTeam", "moderatorteam" },
                    { "a79024cd-bddb-4a53-b419-429816e4b47f", null, "adminTeam", "adminteam" }
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
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
