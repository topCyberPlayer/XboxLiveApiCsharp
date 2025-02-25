using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class RebuildXboxTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: "3a346554-a353-4aa5-a05c-d9ec3cc54fe7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e3a67d3-12b4-4a19-aaa3-328dcd059c1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3320729-ff31-4d2b-a475-503b7b6a3591");

            migrationBuilder.DropColumn(
                name: "Uhs",
                table: "XauTokens");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "OAuthTokens");

            migrationBuilder.RenameColumn(
                name: "AspNetUserId",
                table: "XstsTokens",
                newName: "UhsIdFK");

            migrationBuilder.RenameColumn(
                name: "AspNetUserId",
                table: "XauTokens",
                newName: "UserIdFK");

            migrationBuilder.AlterColumn<string>(
                name: "Xuid",
                table: "XstsTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UhsId",
                table: "XauTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OAuthTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfIssue",
                table: "OAuthTokens",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfExpiry",
                table: "OAuthTokens",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_XstsTokens",
                table: "XstsTokens",
                column: "Xuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XauTokens",
                table: "XauTokens",
                column: "UhsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OAuthTokens",
                table: "OAuthTokens",
                column: "UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2df76980-9583-4b62-b381-00ea350c5787", null, "gamerTeam", "gamerteam" },
                    { "7fda2af7-a5c2-48a1-8d82-8fa3804a9efa", null, "moderatorTeam", "moderatorteam" },
                    { "c7e1ddaf-ff12-48e9-b8fb-b2c08a47158e", null, "adminTeam", "adminteam" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_XstsTokens_UhsIdFK",
                table: "XstsTokens",
                column: "UhsIdFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XauTokens_UserIdFK",
                table: "XauTokens",
                column: "UserIdFK",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_XstsTokens_UhsIdFK",
                table: "XstsTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_XauTokens",
                table: "XauTokens");

            migrationBuilder.DropIndex(
                name: "IX_XauTokens_UserIdFK",
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

            migrationBuilder.DropColumn(
                name: "UhsId",
                table: "XauTokens");

            migrationBuilder.RenameColumn(
                name: "UhsIdFK",
                table: "XstsTokens",
                newName: "AspNetUserId");

            migrationBuilder.RenameColumn(
                name: "UserIdFK",
                table: "XauTokens",
                newName: "AspNetUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Xuid",
                table: "XstsTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Uhs",
                table: "XauTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfIssue",
                table: "OAuthTokens",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfExpiry",
                table: "OAuthTokens",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OAuthTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "OAuthTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XstsTokens",
                table: "XstsTokens",
                column: "AspNetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XauTokens",
                table: "XauTokens",
                column: "AspNetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OAuthTokens",
                table: "OAuthTokens",
                column: "AspNetUserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a346554-a353-4aa5-a05c-d9ec3cc54fe7", null, "adminTeam", "adminteam" },
                    { "7e3a67d3-12b4-4a19-aaa3-328dcd059c1b", null, "moderatorTeam", "moderatorteam" },
                    { "f3320729-ff31-4d2b-a475-503b7b6a3591", null, "gamerTeam", "gamerteam" }
                });
        }
    }
}
