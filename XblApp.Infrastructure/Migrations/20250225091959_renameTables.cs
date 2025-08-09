using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XauTokens_OAuthTokens_UserIdFK",
                table: "XauTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_XstsTokens_XauTokens_UhsIdFK",
                table: "XstsTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_XstsTokens",
                table: "XstsTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_XauTokens",
                table: "XauTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OAuthTokens",
                table: "OAuthTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2df76980-9583-4b62-b381-00ea350c5787");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fda2af7-a5c2-48a1-8d82-8fa3804a9efa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7e1ddaf-ff12-48e9-b8fb-b2c08a47158e");

            migrationBuilder.RenameTable(
                name: "XstsTokens",
                newName: "XboxUserTokens");

            migrationBuilder.RenameTable(
                name: "XauTokens",
                newName: "XboxLiveTokens");

            migrationBuilder.RenameTable(
                name: "OAuthTokens",
                newName: "XboxOAuthTokens");

            migrationBuilder.RenameIndex(
                name: "IX_XstsTokens_UhsIdFK",
                table: "XboxUserTokens",
                newName: "IX_XboxUserTokens_UhsIdFK");

            migrationBuilder.RenameIndex(
                name: "IX_XauTokens_UserIdFK",
                table: "XboxLiveTokens",
                newName: "IX_XboxLiveTokens_UserIdFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XboxUserTokens",
                table: "XboxUserTokens",
                column: "Xuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XboxLiveTokens",
                table: "XboxLiveTokens",
                column: "UhsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XboxOAuthTokens",
                table: "XboxOAuthTokens",
                column: "UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b7abfef-fd91-4077-8dd6-df857ceaab75", null, "gamerTeam", "gamerteam" },
                    { "3bebb3ad-9fb0-413c-ae70-d102373d3fd8", null, "adminTeam", "adminteam" },
                    { "c09c11de-fa15-4ce3-8f1d-f2e9a34296da", null, "moderatorTeam", "moderatorteam" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_XboxLiveTokens_XboxOAuthTokens_UserIdFK",
                table: "XboxLiveTokens",
                column: "UserIdFK",
                principalTable: "XboxOAuthTokens",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XboxUserTokens_XboxLiveTokens_UhsIdFK",
                table: "XboxUserTokens",
                column: "UhsIdFK",
                principalTable: "XboxLiveTokens",
                principalColumn: "UhsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XboxLiveTokens_XboxOAuthTokens_UserIdFK",
                table: "XboxLiveTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_XboxUserTokens_XboxLiveTokens_UhsIdFK",
                table: "XboxUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_XboxUserTokens",
                table: "XboxUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_XboxOAuthTokens",
                table: "XboxOAuthTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_XboxLiveTokens",
                table: "XboxLiveTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b7abfef-fd91-4077-8dd6-df857ceaab75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bebb3ad-9fb0-413c-ae70-d102373d3fd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c09c11de-fa15-4ce3-8f1d-f2e9a34296da");

            migrationBuilder.RenameTable(
                name: "XboxUserTokens",
                newName: "XstsTokens");

            migrationBuilder.RenameTable(
                name: "XboxOAuthTokens",
                newName: "OAuthTokens");

            migrationBuilder.RenameTable(
                name: "XboxLiveTokens",
                newName: "XauTokens");

            migrationBuilder.RenameIndex(
                name: "IX_XboxUserTokens_UhsIdFK",
                table: "XstsTokens",
                newName: "IX_XstsTokens_UhsIdFK");

            migrationBuilder.RenameIndex(
                name: "IX_XboxLiveTokens_UserIdFK",
                table: "XauTokens",
                newName: "IX_XauTokens_UserIdFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XstsTokens",
                table: "XstsTokens",
                column: "Xuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OAuthTokens",
                table: "OAuthTokens",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XauTokens",
                table: "XauTokens",
                column: "UhsId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2df76980-9583-4b62-b381-00ea350c5787", null, "gamerTeam", "gamerteam" },
                    { "7fda2af7-a5c2-48a1-8d82-8fa3804a9efa", null, "moderatorTeam", "moderatorteam" },
                    { "c7e1ddaf-ff12-48e9-b8fb-b2c08a47158e", null, "adminTeam", "adminteam" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_XauTokens_OAuthTokens_UserIdFK",
                table: "XauTokens",
                column: "UserIdFK",
                principalTable: "OAuthTokens",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XstsTokens_XauTokens_UhsIdFK",
                table: "XstsTokens",
                column: "UhsIdFK",
                principalTable: "XauTokens",
                principalColumn: "UhsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
