using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XblApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedNewProprtiesToGameAndGamerGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "DateUnlock",
                table: "Achievements");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Games",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastTimePlayed",
                table: "GamerGame",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<long>(
                name: "GameId",
                table: "Achievements",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LastTimePlayed",
                table: "GamerGame");

            migrationBuilder.AlterColumn<long>(
                name: "GameId",
                table: "Achievements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUnlock",
                table: "Achievements",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b7abfef-fd91-4077-8dd6-df857ceaab75", null, "gamerTeam", "gamerteam" },
                    { "3bebb3ad-9fb0-413c-ae70-d102373d3fd8", null, "adminTeam", "adminteam" },
                    { "c09c11de-fa15-4ce3-8f1d-f2e9a34296da", null, "moderatorTeam", "moderatorteam" }
                });
        }
    }
}
