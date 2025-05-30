using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateOfIssueAndDateOfExpiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Uhs",
                table: "XauTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "XauTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateOfExpiry",
                table: "OAuthTokens",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateOfIssue",
                table: "OAuthTokens",
                type: "datetimeoffset",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "DateOfExpiry",
                table: "OAuthTokens");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "OAuthTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Uhs",
                table: "XauTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "XauTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
