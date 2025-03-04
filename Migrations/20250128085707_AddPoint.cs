using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBA.Players.Charts.Migrations
{
    /// <inheritdoc />
    public partial class AddPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame");

            migrationBuilder.DropIndex(
                name: "IX_PlayerGame_PlayerId",
                table: "PlayerGame");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "PlayerGame",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame",
                columns: new[] { "PlayerId", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "PlayerGame");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGame_PlayerId",
                table: "PlayerGame",
                column: "PlayerId");
        }
    }
}
