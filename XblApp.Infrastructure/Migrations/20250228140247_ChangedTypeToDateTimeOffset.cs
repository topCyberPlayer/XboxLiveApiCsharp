using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTypeToDateTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "050cd894-38e3-48fb-bb8a-a40e331717cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27185955-d12d-468a-bf6e-033d048c9938");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c412d18-4325-4917-b1cf-d67fe7f3b05f");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Games",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateUnlocked",
                table: "GamerAchievement",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34e296f1-55bf-4c76-ad78-c417002b9490", null, "moderatorTeam", "moderatorteam" },
                    { "3c5fb8f3-8bc3-460a-bb91-cef29388843d", null, "gamerTeam", "gamerteam" },
                    { "5e8f70f9-5146-45b1-820f-f7a69255cbe0", null, "adminTeam", "adminteam" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34e296f1-55bf-4c76-ad78-c417002b9490");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5fb8f3-8bc3-460a-bb91-cef29388843d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e8f70f9-5146-45b1-820f-f7a69255cbe0");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Games",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUnlocked",
                table: "GamerAchievement",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "050cd894-38e3-48fb-bb8a-a40e331717cc", null, "gamerTeam", "gamerteam" },
                    { "27185955-d12d-468a-bf6e-033d048c9938", null, "adminTeam", "adminteam" },
                    { "8c412d18-4325-4917-b1cf-d67fe7f3b05f", null, "moderatorTeam", "moderatorteam" }
                });
        }
    }
}
