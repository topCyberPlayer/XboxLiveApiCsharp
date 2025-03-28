using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixedGamerAchievement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GamerAchievements",
                table: "GamerAchievements");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1434f07f-7d2c-41d2-bce8-6969aa290d8c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15b6e164-79f2-4009-8204-d8b918cd989b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35ee0885-f50d-4f18-9d6d-94a346a8518d");

            migrationBuilder.AddColumn<long>(
                name: "GameId",
                table: "GamerAchievements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "GameId",
                table: "Achievements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamerAchievements",
                table: "GamerAchievements",
                columns: new[] { "GamerId", "GameId", "AchievementId" });

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
                name: "IX_GamerAchievements_GameId",
                table: "GamerAchievements",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievements_Games_GameId",
                table: "GamerAchievements",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievements_Games_GameId",
                table: "GamerAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamerAchievements",
                table: "GamerAchievements");

            migrationBuilder.DropIndex(
                name: "IX_GamerAchievements_GameId",
                table: "GamerAchievements");

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

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GamerAchievements");

            migrationBuilder.AlterColumn<long>(
                name: "GameId",
                table: "Achievements",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamerAchievements",
                table: "GamerAchievements",
                columns: new[] { "GamerId", "AchievementId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1434f07f-7d2c-41d2-bce8-6969aa290d8c", null, "moderatorTeam", "moderatorteam" },
                    { "15b6e164-79f2-4009-8204-d8b918cd989b", null, "gamerTeam", "gamerteam" },
                    { "35ee0885-f50d-4f18-9d6d-94a346a8518d", null, "adminTeam", "adminteam" }
                });
        }
    }
}
