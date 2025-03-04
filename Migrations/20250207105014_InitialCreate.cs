using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBA.Players.Charts.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "PlayerGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "PlayerGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "PlayerGames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "PlayerGames",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PlayerGames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "PlayerGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUserName",
                table: "PlayerGames",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "PlayerGames");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "PlayerGames");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "PlayerGames");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "PlayerGames");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PlayerGames");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "PlayerGames");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserName",
                table: "PlayerGames");
        }
    }
}
