using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data
{
    /// <inheritdoc />
    public partial class AddedToTokenXstsModelDbColumnsAspNetUserIdAndAbc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "TokenXsts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abc",
                table: "TokenXsts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "TokenXsts");

            migrationBuilder.DropColumn(
                name: "Abc",
                table: "TokenXsts");
        }
    }
}
