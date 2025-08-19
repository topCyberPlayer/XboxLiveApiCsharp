using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdToConstInIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "218be8dd-1635-48f7-8c89-237d710d21d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fc2f4d6-667a-4641-9076-d623d2b39b0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e861ffaf-8411-43b4-b60d-ec8a9fb4b3e0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-adminTeam", null, "adminTeam", "ADMINTEAM" },
                    { "role-gamerTeam", null, "gamerTeam", "GAMERTEAM" },
                    { "role-moderatorTeam", null, "moderatorTeam", "MODERATORTEAM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-adminTeam");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-gamerTeam");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-moderatorTeam");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "218be8dd-1635-48f7-8c89-237d710d21d1", null, "gamerTeam", "GAMERTEAM" },
                    { "3fc2f4d6-667a-4641-9076-d623d2b39b0e", null, "adminTeam", "ADMINTEAM" },
                    { "e861ffaf-8411-43b4-b60d-ec8a9fb4b3e0", null, "moderatorTeam", "MODERATORTEAM" }
                });
        }
    }
}
