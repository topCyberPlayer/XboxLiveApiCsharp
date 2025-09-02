using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_GamerConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
