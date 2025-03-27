using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGamerAchievement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievement_Achievements_AchievementId",
                table: "GamerAchievement");

            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievement_Gamers_GamerId",
                table: "GamerAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamerAchievement",
                table: "GamerAchievement");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34e296f1-55bf-4c76-ad78-c417002b9490");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5fb8f3-8bc3-460a-bb91-cef29388843d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e8f70f9-5146-45b1-820f-f7a69255cbe0");

            migrationBuilder.RenameTable(
                name: "GamerAchievement",
                newName: "GamerAchievements");

            migrationBuilder.RenameIndex(
                name: "IX_GamerAchievement_AchievementId",
                table: "GamerAchievements",
                newName: "IX_GamerAchievements_AchievementId");

            migrationBuilder.AddColumn<string>(
                name: "LockedDescription",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUnlocked",
                table: "GamerAchievements",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<bool>(
                name: "IsUnlocked",
                table: "GamerAchievements",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievements_Achievements_AchievementId",
                table: "GamerAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "AchievementId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievements_Gamers_GamerId",
                table: "GamerAchievements",
                column: "GamerId",
                principalTable: "Gamers",
                principalColumn: "GamerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievements_Achievements_AchievementId",
                table: "GamerAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_GamerAchievements_Gamers_GamerId",
                table: "GamerAchievements");

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

            migrationBuilder.DropColumn(
                name: "LockedDescription",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "IsUnlocked",
                table: "GamerAchievements");

            migrationBuilder.RenameTable(
                name: "GamerAchievements",
                newName: "GamerAchievement");

            migrationBuilder.RenameIndex(
                name: "IX_GamerAchievements_AchievementId",
                table: "GamerAchievement",
                newName: "IX_GamerAchievement_AchievementId");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateUnlocked",
                table: "GamerAchievement",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamerAchievement",
                table: "GamerAchievement",
                columns: new[] { "GamerId", "AchievementId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34e296f1-55bf-4c76-ad78-c417002b9490", null, "moderatorTeam", "moderatorteam" },
                    { "3c5fb8f3-8bc3-460a-bb91-cef29388843d", null, "gamerTeam", "gamerteam" },
                    { "5e8f70f9-5146-45b1-820f-f7a69255cbe0", null, "adminTeam", "adminteam" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievement_Achievements_AchievementId",
                table: "GamerAchievement",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "AchievementId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamerAchievement_Gamers_GamerId",
                table: "GamerAchievement",
                column: "GamerId",
                principalTable: "Gamers",
                principalColumn: "GamerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
