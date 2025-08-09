using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectRowName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2279d679-7e65-43be-a610-1c781a9ec02e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3986b10a-b925-47dd-b6ea-fbde89f1de75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a418837-7fd1-4f11-a910-c08cf38b645f");

            migrationBuilder.RenameColumn(
                name: "CrearedAt",
                table: "AspNetUsers",
                newName: "CreatedAt");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02d7c2ab-1761-4387-8783-ceff8b9c8e7f", null, "adminTeam", "adminteam" },
                    { "605fdcb8-a279-4962-bad0-e05dbb783609", null, "moderatorTeam", "moderatorteam" },
                    { "67d0ecdd-5312-494e-8062-2a244f490d50", null, "gamerTeam", "gamerteam" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02d7c2ab-1761-4387-8783-ceff8b9c8e7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "605fdcb8-a279-4962-bad0-e05dbb783609");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67d0ecdd-5312-494e-8062-2a244f490d50");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AspNetUsers",
                newName: "CrearedAt");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2279d679-7e65-43be-a610-1c781a9ec02e", null, "moderatorTeam", "moderatorteam" },
                    { "3986b10a-b925-47dd-b6ea-fbde89f1de75", null, "gamerTeam", "gamerteam" },
                    { "4a418837-7fd1-4f11-a910-c08cf38b645f", null, "adminTeam", "adminteam" }
                });
        }
    }
}
