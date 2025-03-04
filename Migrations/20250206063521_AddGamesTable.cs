using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBA.Players.Charts.Migrations
{
    /// <inheritdoc />
    public partial class AddGamesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGame_Game_GameId",
                table: "PlayerGame");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGame_Players_PlayerId",
                table: "PlayerGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "PlayerGame",
                newName: "PlayerGames");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGame_GameId",
                table: "PlayerGames",
                newName: "IX_PlayerGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGames",
                table: "PlayerGames",
                columns: new[] { "PlayerId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGames_Games_GameId",
                table: "PlayerGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGames_Players_PlayerId",
                table: "PlayerGames",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGames_Games_GameId",
                table: "PlayerGames");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGames_Players_PlayerId",
                table: "PlayerGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGames",
                table: "PlayerGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "PlayerGames",
                newName: "PlayerGame");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGames_GameId",
                table: "PlayerGame",
                newName: "IX_PlayerGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame",
                columns: new[] { "PlayerId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Game_GameId",
                table: "PlayerGame",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Players_PlayerId",
                table: "PlayerGame",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
