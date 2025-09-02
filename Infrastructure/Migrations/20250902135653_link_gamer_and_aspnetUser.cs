using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class link_gamer_and_aspnetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Gamers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Gamers_UserId",
                table: "Gamers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamers_AspNetUsers_UserId",
                table: "Gamers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamers_AspNetUsers_UserId",
                table: "Gamers");

            migrationBuilder.DropIndex(
                name: "IX_Gamers_UserId",
                table: "Gamers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Gamers");

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
    }
}
