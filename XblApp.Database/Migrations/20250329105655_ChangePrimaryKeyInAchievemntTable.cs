using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKeyInAchievemntTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievements_Achievements_AchievementId",
                table: "GamerAchievements");

            migrationBuilder.DropIndex(
                name: "IX_GamerAchievements_AchievementId",
                table: "GamerAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36e9b274-219d-4463-8aab-8949e3e36784");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dc71307-abea-4656-a411-07c6c0ca945b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9c759db-f493-48f0-a2f9-803e8301d26f");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                columns: new[] { "AchievementId", "GameId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35300e45-45b3-4c97-aa4d-0d09b6f1e4a5", null, "gamerTeam", "gamerteam" },
                    { "81ae25ba-547b-41ce-bc23-8c6cb740e198", null, "moderatorTeam", "moderatorteam" },
                    { "a79024cd-bddb-4a53-b419-429816e4b47f", null, "adminTeam", "adminteam" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamerAchievements_AchievementId_GameId",
                table: "GamerAchievements",
                columns: new[] { "AchievementId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievements_Achievements_AchievementId_GameId",
                table: "GamerAchievements",
                columns: new[] { "AchievementId", "GameId" },
                principalTable: "Achievements",
                principalColumns: new[] { "AchievementId", "GameId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievements_Achievements_AchievementId_GameId",
                table: "GamerAchievements");

            migrationBuilder.DropIndex(
                name: "IX_GamerAchievements_AchievementId_GameId",
                table: "GamerAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                column: "AchievementId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36e9b274-219d-4463-8aab-8949e3e36784", null, "adminTeam", "adminteam" },
                    { "8dc71307-abea-4656-a411-07c6c0ca945b", null, "moderatorTeam", "moderatorteam" },
                    { "a9c759db-f493-48f0-a2f9-803e8301d26f", null, "gamerTeam", "gamerteam" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamerAchievements_AchievementId",
                table: "GamerAchievements",
                column: "AchievementId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievements_Achievements_AchievementId",
                table: "GamerAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "AchievementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
