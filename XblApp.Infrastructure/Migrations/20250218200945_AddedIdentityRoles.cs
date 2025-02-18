using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "nba",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "154a496b-980e-4e60-9086-ae46be82ddc1", null, "gamerTeam", "gamerteam" },
                    { "ddebf89d-163d-4c1f-8130-23a4997daef9", null, "moderatorTeam", "moderatorteam" },
                    { "fb753122-a37e-4ad7-a5ed-d53cdbb655e1", null, "adminTeam", "adminteam" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "nba",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "154a496b-980e-4e60-9086-ae46be82ddc1");

            migrationBuilder.DeleteData(
                schema: "nba",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddebf89d-163d-4c1f-8130-23a4997daef9");

            migrationBuilder.DeleteData(
                schema: "nba",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb753122-a37e-4ad7-a5ed-d53cdbb655e1");
        }
    }
}
