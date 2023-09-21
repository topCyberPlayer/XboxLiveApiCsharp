using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfileUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileUser",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gamertag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gamerscore = table.Column<int>(type: "int", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileUser", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileUser");
        }
    }
}
